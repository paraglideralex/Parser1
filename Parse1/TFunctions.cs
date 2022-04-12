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

        public void TimeOutput (TimeSpan Range)
        {
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                Range.Hours, Range.Minutes, Range.Seconds,
                Range.Milliseconds / 10);
            double Total = Convert.ToDouble(Range.TotalSeconds);
            Console.WriteLine("Спарсил суммарно карточек: " + TStringUtilities.CardCounter);
            Console.WriteLine("Время работы парсера: " + elapsedTime);
            Console.WriteLine("Среднее время на карточку:  " + Total / (double)TStringUtilities.CardCounter + " секунд");
        }

    }
}
