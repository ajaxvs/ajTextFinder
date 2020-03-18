/*
 * User: ajaxvs
 * Date: 21.11.2018
 * Time: 12:11
 */
using System;

namespace ajClasses 
{
    public class CajCooldown {
        //================================================================================
        private Action fun = null;
        private long delay = 0;
        private long lastTime = 0;
        //================================================================================
        public CajCooldown(long delay, Action fun) {
            if (fun == null) {
                //throw new Exception("cooldown fun can't be null"); //no. it can.
            }
            
            this.fun = fun;
            this.delay = delay;
            
            resetTime();
        }
        //================================================================================
        public bool isReady() {
            return (CajFuns.getTimeMs() - lastTime > delay);
        }
        //================================================================================
        public void resetTime() {
            lastTime = -delay - 1;
        }
        //================================================================================
        public bool call() {
            long ct = CajFuns.getTimeMs();
            if (ct - lastTime > delay) {
                lastTime = ct;
                if (fun != null) {
                    fun();
                }
                return true;
            }
            return false;
        }
        //================================================================================
    }
}
