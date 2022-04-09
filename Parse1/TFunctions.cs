using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;

namespace Parse1
{
    internal class TFunctions
    {
        public void ScrollDown(IWebDriver driver, int PixelsDown)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scroll(0, "+PixelsDown+");");
        }

        public int[] TimeOutput (DateTime Initial, DateTime Finish)
        {
            TimeSpan result = Finish - Initial;
            int result1 = Convert.ToInt32(result.Seconds.ToString());
            int munutes = result1 / 60;
            int seconds = result1 % 60;
            
            return new int[] { munutes, seconds, result1 };
        }

    }
}
