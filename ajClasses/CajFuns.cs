//cut for git

using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ajClasses {
    public class CajFuns {
        //================================================================================
        static public void trace(string msg) {
            Debug.WriteLine(msg);
        }
        //================================================================================
        static public long getTimeMs() {
            return DateTimeOffset.Now.Ticks / TimeSpan.TicksPerMillisecond;
            //return DateTimeOffset.Now.ToUnixTimeMilliseconds(); //.Net 4.0
        }
        //================================================================================
        static public string getTimeStamp(bool withMs = true, string separator = ":") {
            return
                DateTime.Now.Hour.ToString("D2") + separator +
                DateTime.Now.Minute.ToString("D2") + separator +
                DateTime.Now.Second.ToString("D2") +
                (withMs ? "." + DateTime.Now.Millisecond.ToString("D3") : "") +
                ". ";
        }
        //================================================================================
        static public void appExit() {
            if (System.Windows.Forms.Application.MessageLoop) {
                System.Windows.Forms.Application.Exit();
            }
            System.Environment.Exit(0);
        }
        //================================================================================
        static public string getAppPath() {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\";
        }
        //================================================================================
        ///<summary>
        ///Find folder include "..\\..\\" path for developer purposes. 
        ///</summary>
        static public string findDataFolder(string folderName) {
            string appPath = getAppPath();
            string dataDir = appPath + folderName;
            if (!System.IO.Directory.Exists(dataDir)) {
                dataDir = appPath + "..\\..\\" + folderName; //dev path.
                if (System.IO.Directory.Exists(dataDir)) {
                    dataDir = System.IO.Path.GetFullPath(dataDir);
                } else {
                    dataDir = "";
                }
            }
            if (dataDir != "") {
                if (!dataDir.EndsWith("\\", StringComparison.InvariantCulture) &&
                    !dataDir.EndsWith("/", StringComparison.InvariantCulture)) {
                    dataDir += "\\";
                }
            }
            return dataDir;
        }
        //================================================================================
        /// newLine - "\r\n" or "\n"
        /// encoding - Encoding.*
        static public void detectFilesFormat(string path,
                                             out string newLine,
                                             out Encoding encoding,
                                             int maxReadBytesPerLine = 10000) {
            if (maxReadBytesPerLine < 10) maxReadBytesPerLine = 10;
            newLine = Environment.NewLine;
            encoding = Encoding.ASCII; //common text by default, without BOM
            var bom = new byte[4];

            try {
                using (var fs = File.OpenRead(path)) {
                    //find new line charater(s):
                    char c0 = '\0';
                    for (int i = 0; i < maxReadBytesPerLine; i++) {
                        int b = fs.ReadByte();
                        if (b == -1) break;
                        char c1 = (char)b;
                        if (c1 == '\n') {
                            if (c0 == '\r') {
                                newLine = "\r\n"; //Windows
                            } else {
                                newLine = "\n"; //Unix
                            }
                            if (i > 4) { //wait bom
                                return;
                            }
                        }
                        c0 = c1;
                        if (i < 4) {
                            bom[i] = (byte)b;
                        } else if (i == 4) {
                            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) encoding = Encoding.UTF7;
                            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) encoding = Encoding.UTF8;
                            if (bom[0] == 0xff && bom[1] == 0xfe) encoding = Encoding.Unicode; //UTF-16LE
                            if (bom[0] == 0xfe && bom[1] == 0xff) encoding = Encoding.BigEndianUnicode; //UTF-16BE
                            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) encoding = Encoding.UTF32;
                        }
                    }
                }
            } catch {
                //ok, np. use default values
            }
            return;
        }
        //================================================================================
    }
}
