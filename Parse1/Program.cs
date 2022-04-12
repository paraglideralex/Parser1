using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Parse1
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();//счётчик времени выполнения
            stopwatch.Start();
            TExamples Examples = new TExamples();
            TPages Pages = new TPages();
            TStringUtilities StringUtilities = new TStringUtilities();
            TProductDataCollection ProductDataCollection = new TProductDataCollection();
            TFunctions Functions = new TFunctions();
            //Examples.SellerSearch();
            //Examples.FirstTry();
            //Examples.Properties();
            //Examples.FindElem1();
            //Examples.Attributes();

            //Здесь основная работа          

            FileStream file1 = new FileStream("test1.txt", FileMode.Create); //создаем файловый поток
            StreamWriter writer = new StreamWriter(file1); //создаем «потоковый писатель» и связываем его с файловым потоком        

            //формируем шапку таблицы вывода и потребные данные карточек на вывод
            List<string> InputRankParameters = ProductDataCollection.InputRankParameters;
            List<string> InputParameters = ProductDataCollection.TeplyPolMat;

            //Делаем шапку таблицы
            List<string> BasicRow = InputRankParameters;
            BasicRow.AddRange(InputParameters);
            //Делаем шапку таблицы строкой и переходим на другую строку
            string BasicRowStrng = "";
            foreach (string e in BasicRow)
            {
                BasicRowStrng += e + "\t";
            }

            //Создаём драйвер без вывода в консоль
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            IWebDriver driver = new ChromeDriver(driverService, new ChromeOptions());

            //IWebDriver driver = new ChromeDriver();

            writer.WriteLine(BasicRowStrng);

            //для работы CardParcer
            //driver.Navigate().GoToUrl("https://www.ozon.ru/product/teplyy-pol-s-termoregulyatorom-2-kv-m-263618863/?asb=wFT%252Bi6gjz%252BDT8LinRO2HuHClXUa9ePVUp8t2nN85iYZWDJgXlZM6TjgB2e8AOv4E&asb2=QIZNVPsoheu8AQsoouz5RYXkdZcbKKVcuxiG6dzZZSBcaxod92stnOgATG_kcTwuj_MgbQp41z8Too7xGMJROOJfOxGTf7n7PVEfe-Gk5ojctxEY2WUXgFNieILzHlAYIY1qStlQlG7pUXCu4KFA8Q&keywords=%D1%82%D0%B5%D0%BF%D0%BB%D1%8B%D0%B9+%D0%BF%D0%BE%D0%BB+%D1%8D%D0%BB%D0%B5%D0%BA%D1%82%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9&sh=krUDOXjgSQ");//пустой
            //driver.Navigate().GoToUrl("https://www.ozon.ru/product/udlinitel-defender-g418-1-8-m-4-rozetki-206376835/?asb=IQkpVt75XRnQV1xuTSZdkJlt8sdrmvOfEwJ9llWq25U%253D&asb2=wLlIWHT3DG2KQhgzzUaKPRsg0PkeoSSRDUr67lCFBb1QrG9SH_nVH4OWAsb_P2i9&keywords=%D1%83%D0%B4%D0%BB%D0%B8%D0%BD%D0%B8%D1%82%D0%B5%D0%BB%D1%8C&sh=sD75h9CcbA");
            //driver.Navigate().GoToUrl("https://www.ozon.ru/product/zt102-tsifrovoy-multimetr-tester-zotek-zoyi-406919019/?advert=RTPMAYonJIeRojXnDRhiEcLX0wmDfNJ5lnIZ8XQTcmuLhK4TxHi1TvsNCJK6oNxrRlOqUOrHeDNTGWsLNQQRwj28UWMCgtS38OnQ5fu2vDe476q2S-96uDCFiY-iPaRSY-X8a-dT4sdygPUYFs2D_b97PJUIgNV1l0tRzm626OK7KotrD5ndxizkt-wOStdVj2Kfz7mlB634NlM06eQWS-leY2IIWK5XFMWGnJrrHat0bgk3puFG0li3JQAW1YNzpwZZF6DJI_WFWGUSFBBJX5_VI7FPiJSeWHoYBxOMCDWI0pI6V4XrmSG2_WxUqS8p-NljAmYypvlX95VejwkD3fwl7VL7Pq3P_45CX3YXU5_J91jr0O9tEUt-gxKd-LIgzCdVzEpBVS9flxY74_VXqvDTkXRLTrbpDdUVszyPc7ZWDqgOnMNRynRluJ3wj7J2v-diLO-coIvhpnz8saM3sM3AcSfCiUuZ7uNzzykwGD7K3VK79J05yaURF_0J4wVKUhhWfghl0RPr_M9IlP4OWKvVYPNlBXUpNzGec-MIa9pq3ONWht3ivOLr4GpqLf4uhQaoeyLTVQlwa8EqqLc1NQ&keywords=%D0%BC%D1%83%D0%BB%D1%8C%D1%82%D0%B8%D0%BC%D0%B5%D1%82%D1%80&sh=d187t9AMtQ");
            //driver.Navigate().GoToUrl("https://www.ozon.ru/product/greyushchiy-kabel-v-beton-75-8m-750vt-541522880/?sh=sD75h95Lhw");
    

            //    Thread.Sleep(1000);
            ////парсим карточку
            //string Page1 = Pages.CardParser(driver, InputParameters, ref TStringUtilities.CardCounter, writer);
            //writer.Close(); //закрываем поток. Не закрыв поток, в файл ничего не запишется 


            //Console.WriteLine("Время работы программы:  " + Functions.TimeOutput(one, two)[0] + " минут, " + Functions.TimeOutput(one, two)[1] + " секунд");
            //Console.WriteLine("Среднее время на карточку:  " + Functions.TimeOutput(one, two)[2] / TStringUtilities.CardCounter + " секунд");
            //Console.ReadKey();
            //driver.Close();

            ////для работы PageParser
            //driver.Navigate().GoToUrl("https://www.ozon.ru/category/elektricheskie-teplye-poly-10217/?category_was_predicted=true&from_global=true&text=%D1%82%D0%B5%D0%BF%D0%BB%D1%8B%D0%B9+%D0%BF%D0%BE%D0%BB+%D1%8D%D0%BB%D0%B5%D0%BA%D1%82%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9");
            //string PageParceData = Pages.PageParser(driver, InputParameters, writer);
            //writer.Close(); //закрываем поток. Если не закрыть поток, в файл ничего не запишется 
            //Console.ReadKey();
            //Thread.Sleep(3000);

            // Для работы ParseTotal
            driver.Navigate().GoToUrl("https://www.ozon.ru/category/elektricheskie-teplye-poly-10217/leeil-87277852/?from_global=true&text=%D1%82%D0%B5%D0%BF%D0%BB%D1%8B%D0%B9+%D0%BF%D0%BE%D0%BB");
            Thread.Sleep(3000);
            string TotalParceData = Pages.ParseTotal(driver, InputParameters, 10, 117, writer); // эту строку я руками копировал, сохранял в файл и считывал через ExportString
            writer.Close();
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;          
            Functions.TimeOutput(ts);
            Console.ReadKey();
            driver.Close();

            //TODO: 

        }
    }
}
