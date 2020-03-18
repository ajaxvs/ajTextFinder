namespace ajTextFinder.app.view {
    partial class FajMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FajMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyMenu1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.butStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkReplace = new System.Windows.Forms.CheckBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.txtReplace = new System.Windows.Forms.TextBox();
            this.lblResults = new System.Windows.Forms.Label();
            this.txtFilePattern = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkWithSubdirs = new System.Windows.Forms.CheckBox();
            this.butLocatePath = new System.Windows.Forms.Button();
            this.lstResults = new System.Windows.Forms.ListView();
            this.file = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.line = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.originalLine = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.notifyMenu1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1182, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStop,
            this.mnuAbout,
            this.toolStripSeparator1,
            this.mnuExit});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // mnuStop
            // 
            this.mnuStop.Name = "mnuStop";
            this.mnuStop.Size = new System.Drawing.Size(125, 26);
            this.mnuStop.Text = "Stop";
            this.mnuStop.Click += new System.EventHandler(this.mnuStop_Click);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(125, 26);
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(122, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(125, 26);
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.notifyMenu1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // notifyMenu1
            // 
            this.notifyMenu1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.notifyMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nmExit});
            this.notifyMenu1.Name = "notifyMenu1";
            this.notifyMenu1.Size = new System.Drawing.Size(103, 28);
            // 
            // nmExit
            // 
            this.nmExit.Name = "nmExit";
            this.nmExit.Size = new System.Drawing.Size(102, 24);
            this.nmExit.Text = "Exit";
            this.nmExit.Click += new System.EventHandler(this.nmExit_Click);
            // 
            // butStart
            // 
            this.butStart.Location = new System.Drawing.Point(1042, 615);
            this.butStart.Name = "butStart";
            this.butStart.Size = new System.Drawing.Size(128, 48);
            this.butStart.TabIndex = 7;
            this.butStart.Text = "Start";
            this.butStart.UseVisualStyleBackColor = true;
            this.butStart.Click += new System.EventHandler(this.butStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Path:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Find text:";
            // 
            // chkReplace
            // 
            this.chkReplace.AutoSize = true;
            this.chkReplace.Location = new System.Drawing.Point(20, 151);
            this.chkReplace.Name = "chkReplace";
            this.chkReplace.Size = new System.Drawing.Size(86, 21);
            this.chkReplace.TabIndex = 5;
            this.chkReplace.Text = "Replace:";
            this.chkReplace.UseVisualStyleBackColor = true;
            this.chkReplace.CheckedChanged += new System.EventHandler(this.chkReplace_CheckedChanged);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(250, 45);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(850, 22);
            this.txtPath.TabIndex = 1;
            // 
            // txtFind
            // 
            this.txtFind.Location = new System.Drawing.Point(250, 123);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(920, 22);
            this.txtFind.TabIndex = 4;
            // 
            // txtReplace
            // 
            this.txtReplace.Location = new System.Drawing.Point(250, 151);
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.Size = new System.Drawing.Size(920, 22);
            this.txtReplace.TabIndex = 6;
            // 
            // lblResults
            // 
            this.lblResults.Location = new System.Drawing.Point(20, 615);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(1008, 48);
            this.lblResults.TabIndex = 10;
            this.lblResults.Text = "...";
            this.lblResults.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtFilePattern
            // 
            this.txtFilePattern.Location = new System.Drawing.Point(250, 72);
            this.txtFilePattern.Name = "txtFilePattern";
            this.txtFilePattern.Size = new System.Drawing.Size(920, 22);
            this.txtFilePattern.TabIndex = 2;
            this.txtFilePattern.Text = "*.*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "File Pattern:";
            // 
            // chkWithSubdirs
            // 
            this.chkWithSubdirs.AutoSize = true;
            this.chkWithSubdirs.Location = new System.Drawing.Point(20, 99);
            this.chkWithSubdirs.Name = "chkWithSubdirs";
            this.chkWithSubdirs.Size = new System.Drawing.Size(108, 21);
            this.chkWithSubdirs.TabIndex = 3;
            this.chkWithSubdirs.Text = "With subdirs";
            this.chkWithSubdirs.UseVisualStyleBackColor = true;
            // 
            // butLocatePath
            // 
            this.butLocatePath.Location = new System.Drawing.Point(1106, 45);
            this.butLocatePath.Name = "butLocatePath";
            this.butLocatePath.Size = new System.Drawing.Size(64, 22);
            this.butLocatePath.TabIndex = 0;
            this.butLocatePath.Text = "...";
            this.butLocatePath.UseVisualStyleBackColor = true;
            this.butLocatePath.Click += new System.EventHandler(this.butLocatePath_Click);
            // 
            // lstResults
            // 
            this.lstResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.file,
            this.line,
            this.pos,
            this.originalLine});
            this.lstResults.Location = new System.Drawing.Point(13, 194);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(1157, 406);
            this.lstResults.TabIndex = 12;
            this.lstResults.UseCompatibleStateImageBehavior = false;
            // 
            // file
            // 
            this.file.Tag = "";
            this.file.Text = "file";
            this.file.Width = 150;
            // 
            // line
            // 
            this.line.Text = "line";
            // 
            // pos
            // 
            this.pos.Text = "pos";
            // 
            // originalLine
            // 
            this.originalLine.Text = "original line text";
            this.originalLine.Width = 850;
            // 
            // FajMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 675);
            this.Controls.Add(this.lstResults);
            this.Controls.Add(this.butLocatePath);
            this.Controls.Add(this.chkWithSubdirs);
            this.Controls.Add(this.txtFilePattern);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.txtReplace);
            this.Controls.Add(this.txtFind);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.chkReplace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.butStart);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FajMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FajMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FajMain_FormClosing);
            this.Load += new System.EventHandler(this.FajMain_Load);
            this.Resize += new System.EventHandler(this.FajMain_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.notifyMenu1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip notifyMenu1;
        private System.Windows.Forms.ToolStripMenuItem nmExit;
        private System.Windows.Forms.Button butStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkReplace;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.TextBox txtReplace;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.TextBox txtFilePattern;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkWithSubdirs;
        private System.Windows.Forms.Button butLocatePath;
        private System.Windows.Forms.ToolStripMenuItem mnuStop;
        private System.Windows.Forms.ListView lstResults;
        private System.Windows.Forms.ColumnHeader file;
        private System.Windows.Forms.ColumnHeader line;
        private System.Windows.Forms.ColumnHeader pos;
        private System.Windows.Forms.ColumnHeader originalLine;
    }
}