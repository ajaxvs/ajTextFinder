/*
 * User: ajaxvs
 * Date: 03.04.2018
 * 
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ajClasses {
    /*
     * 
     */
    /// <summary>
    /// Simple config. 
    /// Optional auto flush after each save.
    ///  
    /// File struct:config.ini:
    /// <code>
    /// param1=value1
    /// param2=value2
    /// </code>
    /// </summary>
    public class CajConfigBase {
        //================================================================================
        private string path;
        private Action<string> funLog = null;
        private Dictionary<string, string> aConfig;
        //================================================================================
        public CajConfigBase(string path, Action<string> funLog) {
            this.path = path;
            this.funLog = funLog;

            loadFile();
        }
        //================================================================================
        private void loadFile() {
            try {
                aConfig = new Dictionary<string, string>();

                if (!File.Exists(path)) return; //new empty config file.

                string text = System.IO.File.ReadAllText(path);
                string[] aList = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                for (int i = 0; i < aList.Length; i++) {
                    string s = aList[i];
                    if (s.StartsWith("//", StringComparison.InvariantCulture)) {
                        s = ""; //comment line, ignore
                    }
                    if (s != "") {
                        int pos = s.IndexOf("=", StringComparison.InvariantCulture);
                        if (pos > 0) {
                            string key = s.Substring(0, pos);
                            string value = s.Substring(pos + 1);
                            aConfig[key] = value;
                        }
                    }
                }
            } catch (Exception ex) {
                if (funLog != null) funLog("can't load config: " + ex.Message);
            }
        }
        //================================================================================
        public void write(string key, string value, bool needFlush = true) {
            aConfig[key] = value;
            if (needFlush) {
                flush();
            }
        }
        //================================================================================
        public void writeInt(string key, int value, bool needFlush = true) {
            write(key, value.ToString(), needFlush);
        }
        //================================================================================
        public string read(string key) {
            try {
                return aConfig[key];
            } catch (Exception ex) {
                //it's ok, just not found a key.
                Debug.WriteLine("config read: not found: " + key + " " + ex.Message);
                return "";
            }
        }
        //================================================================================
        /// only int. returns default value for incorrect formats including double.
        public int readInt(string key, int defaultValue) {
            string value = read(key);
            if (value != "") {
                int i;
                if (int.TryParse(value, out i)) {
                    return i;
                }
            }

            return defaultValue;
        }
        //================================================================================
        public long readLong(string key, long defaultValue) {
            string value = read(key);
            if (value != "") {
                long i;
                if (long.TryParse(value, out i)) {
                    return i;
                }
            }

            return defaultValue;
        }
        //================================================================================
        public bool readBoolean(string key, bool defaultValue) {
            string value = read(key);
            if (value != "") {
                bool b;
                if (Boolean.TryParse(value, out b)) {
                    return b;
                }
            }

            return defaultValue;
        }
        //================================================================================
        /// <summary>
        /// Converts System.Windows.Forms.Keys string key to int value
        /// </summary>
        public int readKeyCode(string key, int defaultValue) {
            string value = read(key);
            if (value == "") {
                return defaultValue;
            }

            try {
                int u = (int)Enum.Parse(typeof(System.Windows.Forms.Keys), value);
                return u;
            } catch {
                return defaultValue;
            }
        }
        //================================================================================
        /// <summary>
        ///Adds or changes values from optionalConfig to this config.
        ///Doesn't save new config to file.
        /// </summary>
        /// <param name="optionalConfig"></param>
        /// <returns>total overriden values</returns>
        public int overrideWith(CajConfigBase optionalConfig) {
            int totalOverrides = 0;

            foreach (var pair in optionalConfig.aConfig) {
                bool isNew = false;
                if (this.aConfig.ContainsKey(pair.Key)) {
                    if (this.aConfig[pair.Key] != pair.Value) {
                        isNew = true;
                    }
                } else {
                    isNew = true;
                }
                if (isNew) {
                    this.aConfig[pair.Key] = pair.Value;
                    totalOverrides++;
                }
            }

            return totalOverrides;
        }
        //================================================================================
        /// <summary>
        /// Adds value to this config without saving to file.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void overridePair(string key, string value) {
            this.aConfig[key] = value;
        }
        //================================================================================
        public bool containsKey(string key) {
            return aConfig.ContainsKey(key);
        }
        //================================================================================
        public void getAll(Action<string, string> pairCallback) {
            foreach (var pair in aConfig) {
                pairCallback(pair.Key, pair.Value);
            }
        }
        //================================================================================
        public void flush() {
            try {
                //format:
                string s = "";
                foreach (KeyValuePair<string, string> line in aConfig) {
                    s += line.Key + "=" + line.Value + "\n";
                }
                //save to temp file:
                string tmpPath = path + ".tmp";
                System.IO.File.WriteAllText(tmpPath, s);
                //if it's ok then rename:
                System.IO.File.Delete(path);
                System.IO.File.Move(tmpPath, path);
            } catch (Exception ex) {
                if (funLog != null) funLog("can't save config file: " + ex.Message);
            }
        }
        //================================================================================		
    }
}
