using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Parse1
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime one = DateTime.Now;//счётчик времени выполнения
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
            //Thread.Sleep(1000);
            ////парсим карточку
            //string Page1 = Pages.CardParser(driver, InputParameters, ref TStringUtilities.CardCounter, writer);
            //writer.Close(); //закрываем поток. Не закрыв поток, в файл ничего не запишется 
            //DateTime two = DateTime.Now;

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
            driver.Navigate().GoToUrl("https://www.ozon.ru/category/multimetry-i-testery-10080/?category_was_predicted=true&from_global=true&text=%D0%BC%D1%83%D0%BB%D1%8C%D1%82%D0%B8%D0%BC%D0%B5%D1%82%D1%80");
            Thread.Sleep(3000);
            string TotalParceData = Pages.ParseTotal(driver, InputParameters, 1, 25, writer); // эту строку я руками копировал, сохранял в файл и считывал через ExportString
            writer.Close();
            DateTime two = DateTime.Now;

            Console.WriteLine("Спарсил суммарно карточек: " + TStringUtilities.CardCounter);
            Console.WriteLine("Время работы программы:  " + Functions.TimeOutput(one, two)[0] + " минут, " + Functions.TimeOutput(one, two)[1] + " секунд");
            Console.WriteLine("Среднее время на карточку:  " + (float)Functions.TimeOutput(one, two)[2] / (float)TStringUtilities.CardCounter + " секунд");
            Console.WriteLine("txt-файл для прямого копирования в эксель сохранён в Parser1/bin/Debug/test1.txt");
            Console.WriteLine("Нажмите любую клавишу для выхода");
            Console.ReadKey();
            driver.Close();

            //TODO: 

        }
    }
}
