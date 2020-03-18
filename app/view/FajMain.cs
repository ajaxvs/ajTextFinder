using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ajClasses;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace ajTextFinder.app.view {
    public partial class FajMain : Form {
        //================================================================================
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static private extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static private extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        static private extern IntPtr SetActiveWindow(IntPtr hWnd);
        //================================================================================
        private CajConfigBase config;
        private CajTextFinder textFinder = null;
        private List<CajTextFinder.FoundResult> aResults = null;
        //================================================================================
        public FajMain() {
            InitializeComponent();

            this.Text = CajApp.appName;
            this.notifyIcon1.Text = CajApp.appName;
            mnuStop.Enabled = false;

            //lstResults.Columns - use design mode.            
            lstResults.FullRowSelect = true;
            lstResults.MultiSelect = false;            
            lstResults.UseCompatibleStateImageBehavior = false;
            lstResults.View = System.Windows.Forms.View.Details;
            lstResults.VirtualMode = true;
            lstResults.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(lstResults_RetrieveVirtualItem);
            lstResults.MouseDoubleClick += lstResults_MouseDoubleClick;
        }
        //================================================================================
        private void hideSafe() {
            BeginInvoke(new MethodInvoker(delegate {
                Hide();
            }));
        }
        //================================================================================
        private void FajMain_Load(object sender, EventArgs e) {
            //.
        }
        //================================================================================
        private void FajMain_Resize(object sender, EventArgs e) {
            if (WindowState == FormWindowState.Minimized) {
                Hide();
            }
        }
        //================================================================================
        private bool isFormClosing = false;
        private void FajMain_FormClosing(object sender, FormClosingEventArgs e) {
            if (isFormClosing) return;
            isFormClosing = true;
            destroyNotifyIcon();
            CajApp.exit();
        }
        //================================================================================
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) {
            this.Show();
            this.WindowState = FormWindowState.Normal;

            SetForegroundWindow(this.Handle);
            SetActiveWindow(this.Handle);
            SetFocus(this.Handle);
        }
        //================================================================================
        private void destroyNotifyIcon() {
            if (notifyIcon1 != null) {
                notifyIcon1.Visible = false;
                notifyIcon1 = null;
            }
        }
        //================================================================================
        private void mnuAbout_Click(object sender, EventArgs e) {
            MessageBox.Show(CajApp.appAbout, CajApp.appName + " :: About",
                            MessageBoxButtons.OK, MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
        }
        //================================================================================
        private void mnuExit_Click(object sender, EventArgs e) {
            CajApp.exit();
        }
        //================================================================================
        private void nmExit_Click(object sender, EventArgs e) {
            CajApp.exit();
        }
        //================================================================================
        private void setResult(string s) {
            Invoke(new Action(() => {
                lblResults.Text = CajFuns.getTimeStamp(false) + "Results: " +
                                  Environment.NewLine + s;
            }));
        }
        //================================================================================
        public void loadConfig(CajConfigBase config) {
            this.config = config;
            txtPath.Text = config.read("txtPath");
            txtFilePattern.Text = config.read("txtFilePattern");
            chkWithSubdirs.Checked = config.readBoolean("chkWithSubdirs", true);
            txtFind.Text = config.read("txtFind");
            txtReplace.Text = config.read("txtReplace");
            chkReplace.Checked = config.readBoolean("chkReplace", false);
            chkReplace_CheckedChanged(null, null);
        }
        //================================================================================
        private void butStart_Click(object sender, EventArgs e) {
            //finding text only, not RegExp. it's much slower.

            if (txtFilePattern.Text == "") {
                txtFilePattern.Text = "*.*";
            }
            if (config == null) return;

            config.write("txtPath", txtPath.Text);
            config.write("txtFilePattern", txtFilePattern.Text);
            config.write("chkWithSubdirs", chkWithSubdirs.Checked.ToString());
            config.write("txtFind", txtFind.Text);
            config.write("txtReplace", txtReplace.Text);
            config.write("chkReplace", chkReplace.Checked.ToString());

            setResult("finding files...");
            butStart.Text = "In process...";
            butStart.Enabled = false;
            mnuStop.Enabled = true;
            lstResults.Items.Clear();

            var options = new CajTextFinder.Options() {
                path = txtPath.Text,
                filePattern = txtFilePattern.Text,
                isWithSubdirs = chkWithSubdirs.Checked,
                find = txtFind.Text,
                replace = txtReplace.Text,
                isReplaceRequired = chkReplace.Checked,
                onProgress = this.onCheckProgress,
                onError = this.onCheckError,
                onSuccess = this.onCheckSuccess
            };

            textFinder = new CajTextFinder(options);
        }
        //================================================================================
        private CajCooldown cdCheckProgress = new CajCooldown(100, null); //10 fps. enough for anyone
        private void onCheckProgress(int currentFile, int totalFiles, string filePath) {
            if (!cdCheckProgress.isReady()) return;
            cdCheckProgress.call();

            if (totalFiles == 0) return;
            int p = (int)(currentFile * 100.0f / totalFiles);
            setResult(currentFile + " / " + totalFiles + " (" + p + "%)");
        }
        //================================================================================
        private void onCheckError(string err) {
            setResult(err);
            onCheckFinish();
        }
        //================================================================================
        private void onCheckSuccess(List<CajTextFinder.FoundResult> aResults) {
            this.aResults = aResults;

            if (textFinder == null) {
                setResult("logic error: textFinder is null");
                return;
            }

            //first, because ListView is too slow:
            Invoke(new Action(() => {
                setResult(textFinder.getResultStatus() + " (Wait...)");
            }));

            Invoke(new Action(() => {
                lstResults.VirtualListSize = 0; //it works.
                lstResults.BeginUpdate();
                lstResults.VirtualListSize = aResults.Count;
                setResult(textFinder.getResultStatus());
            }));

            onCheckFinish();
        }
        //================================================================================
        private void lstResults_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e) {
            if (e == null) {
                Debug.WriteLine("RetrieveVirtualItemEventArgs is null");
                return;
            }

            if (e.ItemIndex < 0 || e.ItemIndex >= aResults.Count) {
                Debug.WriteLine("incorrect ItemIndex = " + e.ItemIndex);
                return;
            }

            var res = aResults[e.ItemIndex];
            string[] s = new string[4];
            //GetFileName() == path.IndexOfAny(InvalidPathChars) + path.substring:
            //https://github.com/microsoft/referencesource/blob/master/mscorlib/system/io/path.cs
            //so it's pretty fast and doesn't need additional field for fileName only:
            s[0] = Path.GetFileName(res.filePath);
            s[1] = res.line.ToString();
            s[2] = res.pos.ToString();
            s[3] = res.originalLine;

            e.Item = new ListViewItem(s);
        }
        //================================================================================
        private void onCheckFinish() {
            Invoke(new Action(() => {
                butStart.Text = "Start";
                butStart.Enabled = true;
                mnuStop.Enabled = false;
                lstResults.EndUpdate();
            }));
        }
        //================================================================================
        private void lstResults_MouseDoubleClick(object sender, MouseEventArgs e) {
            try {
                if (aResults == null) return;

                var aSelected = lstResults.SelectedIndices;
                if (aSelected == null) return;

                int i = aSelected[0];
                if (i == -1) return;
                if (i >= aResults.Count) return;

                //open with default app:
                System.Diagnostics.Process.Start(aResults[i].filePath);
            } catch (Exception ex) {
                Debug.WriteLine(ex);
            }
        }
        //================================================================================
        private void butLocatePath_Click(object sender, EventArgs e) {
            var fbd = new FolderBrowserDialog();
            string path = txtPath.Text;
            if (path != "" && Directory.Exists(path)) {
                fbd.SelectedPath = path;
            }
            fbd.ShowNewFolderButton = false;
            fbd.Description = "Select Path";
            var res = fbd.ShowDialog(this);

            if (res == DialogResult.OK) {
                path = fbd.SelectedPath;
                if (!string.IsNullOrWhiteSpace(path)) {
                    txtPath.Text = path;
                }
            }
        }
        //================================================================================
        private void mnuStop_Click(object sender, EventArgs e) {
            if (textFinder != null) {
                setResult("stop...");
                textFinder.stop();
            }
        }
        //================================================================================
        private void chkReplace_CheckedChanged(object sender, EventArgs e) {
            txtReplace.Enabled = chkReplace.Checked;
        }
        //================================================================================
    }
}
