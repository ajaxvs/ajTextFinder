using System;
using System.Threading;
using ajClasses;
using ajTextFinder.app.view;

namespace ajTextFinder.app {
    class CajApp {
        //================================================================================
        public const string appName = "ajTextFinder";
        public const string appAbout = "v 20-05-15" +
                                       "\r\n\r\n(c) ajaxvs@gmail.com";
        //================================================================================
        static private FajMain frmMain = null;
        static private string dataDir = "";
        static private bool isStarted = false;

        static private CajConfigBase config;
        //================================================================================
        public CajApp() { }
        //================================================================================
        static public FajMain start() {
            if (isStarted) throw new Exception("app is started");
            isStarted = true;

            frmMain = new FajMain();

            test0();
            init();
            frmMain.loadConfig(config);

            return frmMain;
        }
        //================================================================================
        static private void test0() {
            //trace("test0");
        }
        //================================================================================
        static public void trace(Object msg) {
            string s = CajFuns.getTimeStamp(false) + msg.ToString();
            CajFuns.trace(s);
        }
        //================================================================================
        static public void exit() {
            CajFuns.appExit();
        }
        //================================================================================
        static private void init() {
            dataDir = CajFuns.findDataFolder("data");
            config = new CajConfigBase(dataDir + "config.ini", trace);
        }
        //================================================================================
    }
}
