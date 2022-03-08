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
            TExamples Examples = new TExamples();
            TPages Pages = new TPages();
            TStringUtilities StringUtilities = new TStringUtilities();

            //Examples.Tread1();

            //Examples.FirstTry();
            //Examples.Properties();
            //Examples.FindElem1();
            //Examples.Attributes();
            //Examples.FindElem2();
            //Здесь основная работа
            //Examples.FindElem3();
            //Pages.GoTo();
            //StringUtilities.TesingConcat();

            Examples.Tryin2Xpath();

            int NumberOfPages2Look = 20;

            List<string> InputRankParameters = new List<string>()
            { "Название", "Артикул",   "Отзывы","К-во видео", 
              "К-во вопросов", "Цена до скидки", "Цена после скидки"};


            List <string> InputParameters = new List<string>() 
            { "Площадь обогрева, кв.м", "Макс. мощность, Вт",   "Толщина, мм", 
              "Страна-изготовитель",  "Длина, м",             "Ширина, м"  ,
              "Размеры, мм",            "Вид обогрева",         "Особенности"};

            //Делаем шапку таблицы
            List<string> BasicRow = InputRankParameters;
            BasicRow.AddRange(InputParameters);
            //Делаем шапку таблицы строкой и переходим на другую строку
            string BasicRowStrng = "";
            foreach (string e in BasicRow)
            {
                BasicRowStrng += e + "\t";
            }
            BasicRowStrng += "\r\n";
            IWebDriver driver = new ChromeDriver();


            ////для работы CardParcer
            //driver.Navigate().GoToUrl("https://www.ozon.ru/product/teplyy-pol-pod-plitku-2-m2-s-termoregulyatorom-nagrevatelnyy-mat-2m-kv-264666953/?asb=XOKlGIya6fr6ea1xwwqeRo8Nr5oXzdfECCPhhipsvzk%253D&asb2=rHQ4Qjv_HZESKDDEEPXHEOTn9Wz5YuooDrX6mny_HIM1cn31RrGgEcz6LdRLaPOE&keywords=%D1%82%D0%B5%D0%BF%D0%BB%D1%8B%D0%B9+%D0%BF%D0%BE%D0%BB+%D1%8D%D0%BB%D0%B5%D0%BA%D1%82%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9&sh=QX9lTgAAAA");
            //Thread.Sleep(1000);
            ////парсим карточку
            //string Page1 = Pages.CardParser(driver, InputParameters);


            //для работы PageParser
            driver.Navigate().GoToUrl("https://www.ozon.ru/category/elektricheskie-teplye-poly-10217/?category_was_predicted=true&from_global=true&text=%D1%82%D0%B5%D0%BF%D0%BB%D1%8B%D0%B9+%D0%BF%D0%BE%D0%BB+%D1%8D%D0%BB%D0%B5%D0%BA%D1%82%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9");
            Thread.Sleep(3000);

            string TotalParceData = Pages.ParseTotal(driver, InputParameters, 5);


            File.WriteAllText("WriteText11.txt", BasicRowStrng);

            //Начал выводить по каждой странице, проверить формирование драйвераКласс
            Console.WriteLine();



        }
    }
}
