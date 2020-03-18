using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ajClasses {
    public class CajTextFinder {
        //================================================================================
        public class Options {
            public string path = "";
            public string filePattern = "";
            public bool isWithSubdirs = false;
            public string find = "";
            public string replace = "";
            public bool isReplaceRequired = false;
            public Action<int,int,string> onProgress = null; //currentFile, totalFiles, filePath
            public Action<string> onError = null; //description
            public Action<List<FoundResult>> onSuccess = null;
        }
        //================================================================================
        public class FoundResult {
            public string filePath = "";
            public int line = 0;
            public int pos = 0;
            public string originalLine = "";            
        }
        //================================================================================
        private Options options;
        private Object lockAddResult = new Object();

        private List<FoundResult> aResults = new List<FoundResult>();
        private int iErrors = 0;
        private int iLines = 0;
        private int iFiles = 0;
        private int iTotalFiles = 0;

        private Thread thMain = null;
        private volatile bool isStopRequired = false;
        //================================================================================
        public CajTextFinder(Options options) {
            this.options = options;

            thMain = new Thread(start);
            thMain.Start();
        }
        //================================================================================
        private void start() {
            try {
                aResults.Clear();
                iErrors = 0;
                iFiles = 0;
                iTotalFiles = 0;
                iLines = 0;
                isStopRequired = false;

                if (!Directory.Exists(options.path)) {
                    if (options.onError != null) {
                        options.onError("path not found");
                    }
                    return;
                }
                if (options.find == "") {
                    if (options.onError != null) {
                        options.onError("find string is empty");
                    }
                    return;
                }

                SearchOption so;
                if (options.isWithSubdirs) {
                    so = SearchOption.AllDirectories;
                } else {
                    so = SearchOption.TopDirectoryOnly;
                }

                var aFiles = Directory.GetFiles(options.path, options.filePattern, so);
                iTotalFiles = aFiles.Length;

                Parallel.ForEach(aFiles, checkFile);

                end();
            } catch (ThreadAbortException) { 
                //ok np
            } catch (Exception ex) {
                Debug.WriteLine(ex);
                if (options.onError != null) {
                    options.onError(ex.Message);
                }
            }
        }
        //================================================================================
        private void end() {
            //sort comparing filePath > line > pos:
            aResults.Sort((a, b) => {
                if (a.filePath == b.filePath) {
                    if (a.line == b.line) {
                        return a.pos - b.pos;
                    }
                    return a.line - b.line;
                }
                return a.filePath.CompareTo(b.filePath);
            });

            //done:
            if (options.onSuccess != null) {
                options.onSuccess(aResults);
            }
        }
        //================================================================================
        public void stop() {
            try {
                if (thMain != null && thMain.IsAlive) {
                    isStopRequired = true;
                    //abort thread if it's just finding without replacing:
                    if (!options.isReplaceRequired) {
                        thMain.Abort(); //don't wait scanning large files
                        end(); //anyway return already found results
                    }
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex);
            }
        }
        //================================================================================
        public string getResultStatus() {
            return "Found = " + aResults.Count +
                   ". Total files = " + iFiles + ". Total lines: " + iLines +
                   (iErrors > 0 ? ". Errors: " + iErrors : "") +
                   ".";
        }
        //================================================================================
        private void checkFile(string filePath, ParallelLoopState pls) {
            try {
                if (isStopRequired) return;

                var aLines = File.ReadAllLines(filePath);
                int findLen = options.find.Length;
                int replaceLen = options.replace.Length;

                bool wasReplace = false;
                string originalLine;
                for (int line = 0; line < aLines.Length; line++) {
                    originalLine = aLines[line];
                    int pos = 0;
                    for (; ; ) {
                        //find:
                        pos = aLines[line].IndexOf(options.find, pos);
                        if (pos == -1) break;
                        //add found result:
                        var res = new FoundResult();
                        res.filePath = filePath;
                        res.originalLine = originalLine;
                        res.line = line + 1;
                        res.pos = pos + 1;
                        lock (lockAddResult) {
                            aResults.Add(res);
                        }
                        //replace?:
                        if (options.isReplaceRequired) {
                            wasReplace = true;
                            aLines[line] = aLines[line].Substring(0, pos) + options.replace +
                                           aLines[line].Substring(pos + findLen);
                            pos += replaceLen;
                        } else {
                            pos += findLen;
                        }
                    }
                }

                //save?:
                if (wasReplace) {
                    string newLine = "";
                    Encoding encoding;
                    CajFuns.detectFilesFormat(filePath, out newLine, out encoding);
                    var sOut = String.Join(newLine, aLines);
                    File.WriteAllText(filePath, sOut, encoding);
                }

                //after all checks:
                iFiles++;
                iLines += aLines.Length;

                //progress?:
                if (options.onProgress != null) {
                    options.onProgress(iFiles, iTotalFiles, filePath);
                }

                //break?:
                if (isStopRequired) {
                    pls.Break(); //prevent new iterations to be started,
                                 //.Stop() doesn't really stop current iterations, nn.
                }
            } catch (Exception ex) {
                iErrors++;
                Debug.WriteLine(ex);
            }
        }
        //================================================================================
    }
}
