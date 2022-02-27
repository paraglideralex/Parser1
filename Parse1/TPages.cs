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
    class TPages
    {
        public IWebDriver DriverCreation()
        {
             IWebDriver driver = new ChromeDriver();
            return driver;
        }

        public void Navigation(IWebDriver driver)
        {
            // Переходим по запросу
            //IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.ozon.ru/category/elektricheskie-teplye-poly-10217/?category_was_predicted=true&from_global=true&text=%D1%82%D0%B5%D0%BF%D0%BB%D1%8B%D0%B9+%D0%BF%D0%BE%D0%BB+%D1%8D%D0%BB%D0%B5%D0%BA%D1%82%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9");
        }
        /// <summary>
        /// эти 3 проги показывают, что единожды созданный драйвер можно перетаскивать из функции в функцию
        /// </summary>
        public void GoTo()
        {
            IWebDriver driver = DriverCreation();
            Navigation(driver);
        }
        /// <summary>
        /// Парсер отдельной карточки, субпрога
        /// </summary>
        /// <param name="driver">созданный ранее гугл-драйвер</param>
        public string CardParser(IWebDriver driver, List<string> InputParameters)
        {
            //перед этим мы: загуглили запрос, перешли, прошли по первой карточке
            // Переходим по запросу

            TStringUtilities StringUtilities = new TStringUtilities();

            //Начинаем работу
            //Парсим заголовок по тегу h1
            IWebElement Head1 = driver.FindElement(By.TagName("h1"));
            String Head1Text = Head1.Text;

            //Парсим код товара 
            //IWebElement Code = driver.FindElement(By.CssSelector("#layoutPage > div.f5z > div.container.f6z > div:nth-child(2) > div > div > div.j4k.jk6.k6j.k8j > span"));
            IWebElement Code = driver.FindElement(By.XPath("//span[@class='iy5 yi5'][contains(.,'Код товара: ')]"));
            String CodeText = Code.Text;
            //Thread.Sleep(100);

            //Парсим параметры вовлечённости
            IWebElement Rewiews = driver.FindElement(By.XPath("(//div[@class='ui-d7'][contains(.,' отзывов')])[1]"));
            string RewiewsText = Rewiews.Text;
            //Thread.Sleep(100);
            IWebElement Video = driver.FindElement(By.XPath("(//div[@class='ui-d7'][contains(.,' видео')])[1]"));
            string VideoText = Video.Text;
            //Thread.Sleep(100);
            IWebElement Questions = driver.FindElement(By.XPath("(//div[@class='ui-d7'][contains(.,' вопроса')])[1]"));
            string QuestionsText = Questions.Text;
          
            //Парсим цены
            //для этого читаем весь див с ценами и ценой в кредит
            IWebElement Prices = driver.FindElement(By.ClassName("qj5"));
            string PricesText = Prices.Text;

            StringUtilities.ClearPrices(PricesText);

            //Из него выводим только последнюю строку

            int[] p = { 2 };
            PricesText = StringUtilities.SelectedRows(PricesText, p);

            //ебёмся с рейтингом
            // классы он упорно не видит
            // пробовал парсить весь body, без результата, там нету текста с оценками
            // пробовал парсить класс с пустым именем, оценок нет
            //Thread.Sleep(100);
            //IWebElement Rate = driver.FindElement(By.ClassName("x7j"));
            // IWebElement Rate = driver.FindElement(By.XPath("//div[contains(@class, '')]"));
            //string RateText = Rate.Text;
            //IWebElement Rate1 = driver.FindElement(By.TagName("body"));
            //string RateText1 = Rate1.Text;
            //IList<IWebElement> oCheckBox = driver.FindElements(By.ClassName("x7j"));

            //вычленяем названия параметров товара
            IList<IWebElement> ParameterNamesCol = driver.FindElements(By.ClassName("pi1"));
            //string t = oCheckBox.Text;
            List<string> ParameterNames = new List<string>();
            foreach (IWebElement s in ParameterNamesCol)
            {
                //var yy = s.FindElement(By.ClassName("ui-p8"));
                //Console.WriteLine(yy.Text);
                string Text = s.Text;
                Text = Text.Replace("\r\n", "\t");
                ParameterNames.Add(Text);
            }
            string ParameterNamesStr = "";
            foreach (string s in ParameterNames)
            {
                ParameterNamesStr += s + "\t";
            }

            ////вычленяем параметры товара
            IList<IWebElement> ParameterCol = driver.FindElements(By.ClassName("i1p"));
            //string t = oCheckBox.Text;
            List<string> Parameters = new List<string>();
            foreach (IWebElement s in ParameterCol)
            {
                //var yy = s.FindElement(By.ClassName("ui-p8"));
                //Console.WriteLine(yy.Text);
                string Text = s.Text;
                Text = Text.Replace("\r\n", "\t");
                Parameters.Add(Text);
            }
            string ParameterStr = "";
            foreach (string s in Parameters)
            {
                ParameterStr += s + "\t";
            }

            string CardCollection = Head1Text + "\t" + CodeText + "\t" + RewiewsText + "\t" + VideoText + "\t" + QuestionsText + "\t" + PricesText +"\t" + ParameterStr + "\t" + ParameterNamesStr;

             File.WriteAllText("WriteText.txt", CardCollection);

            // Выделяем нужные параметры для внесения в таблицу
            List<string> AAa = StringUtilities.ChooseParameters(InputParameters, ParameterNames, Parameters);









            return CardCollection;

            Console.WriteLine();


        }

    }
}
