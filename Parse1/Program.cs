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
            //Examples.SellerSearch();
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
             StreamWriter file = new StreamWriter("WriteLines23.txt", append: true);
            //StringUtilities.ExportString("StringOut.txt");


            int NumberOfPages2Look = 20;

            List<string> InputRankParameters = new List<string>()
            { "Название", "Артикул",   "Отзывы","К-во видео", 
              "К-во вопросов", "Рейтинг","Цена после скидки", "Цена до скидки"};


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
            file.Write(BasicRowStrng + "\r\n");

            ////для работы CardParcer
            //driver.Navigate().GoToUrl("https://www.ozon.ru/product/teplyy-pol-elektricheskiy-kvadrat-tepla-pod-plitku-1-m2-s-programmiruemym-termoregulyatorom-473585169/?asb=4dOTkwqnIvkawVZRB4arlnwT7p9fMdr9ESn%252F9AyHrhTV1iSx6RN6sMeViU0iGshR&asb2=nWrU6dFq9rREurtmdnXqxF_PM_cTnPbQhI6P61xuDOq6b-jlXP7J5YpBE5SEThgrugwFbwjIiVir3nmM2lUTTF89Da54ukAS60oVF_4J9z6bWjPNp3wkkQm150gfEVM-sRu-zu6AyV94plAs-mznng&keywords=%D1%82%D0%B5%D0%BF%D0%BB%D1%8B%D0%B9+%D0%BF%D0%BE%D0%BB+%D1%8D%D0%BB%D0%B5%D0%BA%D1%82%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9&sh=6rmZRwAAAA");
            //Thread.Sleep(1000);
            ////парсим карточку
            //string Page1 = Pages.CardParser(driver, InputParameters, ref TStringUtilities.CardCounter, file);
            //file.WriteLine(Page1 + "\r\n");

            ////для работы PageParser
            //driver.Navigate().GoToUrl("https://www.ozon.ru/category/elektricheskie-teplye-poly-10217/?category_was_predicted=true&from_global=true&text=%D1%82%D0%B5%D0%BF%D0%BB%D1%8B%D0%B9+%D0%BF%D0%BE%D0%BB+%D1%8D%D0%BB%D0%B5%D0%BA%D1%82%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9");
            //Thread.Sleep(3000);

            //// эту строку я руками копировал, сохранял в файл и считывал через ExportString
            //string TotalParceData = Pages.ParseTotal(driver, InputParameters, 2, file);


            //File.WriteAllText("WriteText11.txt", BasicRowStrng);

            ////Начал выводить по каждой странице, проверить формирование драйвера
            //Console.WriteLine();

            //
            //TODO: выводить данные каждой карточки каждой итерации в файл путём дополнения к файлу,см ссылку
            //Можно ли вообще так сделать?...
            //Добавить шапку таблицы к итоговому файлу
            //Прибраться


        }
    }
}
