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
            IWebElement Code = driver.FindElement(By.XPath("//span[@class='rj7'][contains(.,'Код')]"));
            string CodeTextPrev = Code.Text;
            string CodeText = "";

            foreach (char Char1 in CodeTextPrev)
            {
                if (char.IsDigit(Char1) == true) //выводим только цифры
                {
                    CodeText += Char1;
                }

            }
            //CodeText = CodeText.Replace("Код товара: ", "");//оставили только цифры
            //Thread.Sleep(100);

            //Парсим параметры вовлечённости
            IWebElement Rewiews = driver.FindElement(By.XPath("(//div[@class='ui-d7'][contains(.,' отзы')])[1]"));
            string RewiewsText = Rewiews.Text;
            string RewiewsTextNum = StringUtilities.GetStringBeforeLetters(RewiewsText, "о");

            //Thread.Sleep(100);
            IWebElement Video = driver.FindElement(By.XPath("(//div[@class='ui-d7'][contains(.,' виде')])[1]"));
            string VideoText = Video.Text;
            string VideoTextNum = StringUtilities.GetStringBeforeLetters(VideoText, "в");
            //Thread.Sleep(100);
            IWebElement Questions = driver.FindElement(By.XPath("(//div[@class='ui-d7'][contains(.,' вопро')])[1]"));
            string QuestionsText = Questions.Text;
            string QuestionsTextNum = StringUtilities.GetStringBeforeLetters(QuestionsText, "в");
            //Парсим цены
            //для этого читаем весь див с ценами и ценой в кредит
            IWebElement Prices = driver.FindElement(By.ClassName("j6u"));
            string PricesText = Prices.Text;

            //Из него выводим только последнюю строку

            int[] p = { 2 };
            PricesText = StringUtilities.SelectedRows(PricesText, p);

            List<string> PricesNumbers = StringUtilities.ClearPrices(PricesText);

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
            IList<IWebElement> ParameterNamesCol = driver.FindElements(By.ClassName("t1i"));
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
            IList<IWebElement> ParameterCol = driver.FindElements(By.ClassName("it2"));
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

            string CardCollection = Head1Text + "\t" + CodeText + "\t" + RewiewsText + "\t" + VideoText + "\t" + QuestionsText + "\t" + PricesText + "\t" + ParameterStr + "\t" + ParameterNamesStr;


            //Выделяем нужные атрибуты для внесения
            List<string> Attributes = new List<string>();
            Attributes.Add(Head1Text);
            Attributes.Add(CodeText);
            Attributes.Add(RewiewsTextNum);
            Attributes.Add(VideoTextNum);
            Attributes.Add(QuestionsTextNum);
            Attributes.AddRange(PricesNumbers);

            // Выделяем нужные параметры для внесения в таблицу
            List<string> ParametersOut = StringUtilities.ChooseParameters(InputParameters, ParameterNames, Parameters);
            //Делаем список всех параметров на вывод
            List<string> Output = Attributes;
            Output.AddRange(ParametersOut);
            //Делаем из списка строку
            string OutputString = "";
            foreach (string e in Output)
            {
                OutputString += e + "\t";
            }
            return OutputString;

            Console.WriteLine();

        }

        public string PageParser(IWebDriver driver, List<string> InputParameters)
        {
            TPages Pages = new TPages();
            string PageString = "";

            ///https://automated-testing.info/t/obshhij-algoritm-resheniya-element-is-not-attached-to-page-document/13003/5

            //Находим координаты всех карточек на странице
            // IList<IWebElement> oCheckBox = driver.FindElements(By.ClassName("h3p"));
            //IList<IWebElement> oCheckBox = driver.FindElements(By.XPath("(//div[contains(@class,'h3p')])[117]")); 
            // IList<IWebElement> oCheckBox = driver.FindElements(By.TagName("a"));
            //Сначала находим див со ссылкой на страницу
            IList<IWebElement> ClickList = driver.FindElements(By.ClassName("hw"));
            int i = 0;
            List<string> ListOfReferences2Cards = new List<string>();
            //Для каждого дива находим его заголовок "а" и принадлежащий ему аттрибут - ссылку href, сохраняем
            foreach (IWebElement click in ClickList)
            {
                IWebElement ee = ClickList[i].FindElement(By.TagName("a"));
                string oo = ee.GetAttribute("href");
                ListOfReferences2Cards.Add(oo);
                i++;
            }

            int NumberOfCards = ClickList.Count;
            StreamWriter read = new StreamWriter("RarePoints.txt");

            for (int j=0;j<NumberOfCards; j++)
            {
                // используя сохранённые ссылки, можно гулять по всей странице, ниже это сделано вручную
                driver.Navigate().GoToUrl(ListOfReferences2Cards[j]);
                Thread.Sleep(2000);
                string CardParserString = Pages.CardParser(driver, InputParameters);
                PageString += CardParserString + "\r\n";

                foreach (char Ch in CardParserString)
                {
                    read.Write(Ch);
                }
                read.Write("\r\n");

                driver.Navigate().Back();
                Thread.Sleep(200);
                driver.Navigate().Refresh();
                Thread.Sleep(500);
            }

            File.WriteAllText("WriteText333.txt", PageString);


            // используя сохранённые ссылки, можно гулять по всей странице, ниже это сделано вручную
            driver.Navigate().GoToUrl(ListOfReferences2Cards[0]);
            Thread.Sleep(5000);
            string H = Pages.CardParser(driver, InputParameters);
            PageString+=H+ "\r\n";
            driver.Navigate().Back();
            driver.Navigate().Refresh();

            //try
            //{

            //}
            //IWebElement ee1 = ClickList[1].FindElement(By.TagName("a"));
            //string oo1 = ee1.GetAttribute("href");
            driver.Navigate().GoToUrl(ListOfReferences2Cards[1]);
            Thread.Sleep(3000);
            string H1 = Pages.CardParser(driver, InputParameters);
            PageString += H1 + "\r\n";
            driver.Navigate().Back();
            driver.Navigate().Refresh();

            driver.Navigate().GoToUrl(ListOfReferences2Cards[2]);
            Thread.Sleep(3000);
            string H2 = Pages.CardParser(driver, InputParameters);
            PageString += H2 + "\r\n";
            driver.Navigate().Back();
            driver.Navigate().Refresh();
            File.WriteAllText("WriteText222.txt", PageString);




            List<string> yy = new List<string>();
            foreach (IWebElement click in ClickList)
            {
                string ByID = click.GetAttribute("href");
                yy.Add(ByID);
            }
            ClickList[0].Click();
            Thread.Sleep(3000);
            driver.Navigate().Back();
            driver.Navigate().Refresh();
            driver.Close();

            //IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.ozon.ru/category/elektricheskie-teplye-poly-10217/?category_was_predicted=true&from_global=true&text=%D1%82%D0%B5%D0%BF%D0%BB%D1%8B%D0%B9+%D0%BF%D0%BE%D0%BB+%D1%8D%D0%BB%D0%B5%D0%BA%D1%82%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9");
            Thread.Sleep(3000);
            ClickList[0].Click();
            Thread.Sleep(3000);
            driver.Navigate().Back();
            driver.Navigate().Refresh();
            driver.Close();

            //bool staleElement = true;
            //for (int i = 0; i < 4; i++)
            //{


            //    while (staleElement)
            //    {
            //        try
            //        {
            //            ClickList = driver.FindElements(By.ClassName("q3h"));
            //            ClickList[i].Click();
            //            staleElement = false;
            //        }
            //        catch (StaleElementReferenceException e)
            //        {
            //            staleElement = true;//might be chance for infinity--coz (if the try-block keep on -failing.it won't resolve the issue,--> My Percepton in one case.)
            //        }
            //    }
            //    Thread.Sleep(1000);
            //    driver.Navigate().Back();
            //    driver.Navigate().Refresh();
            //    Thread.Sleep(1000);

            //}
            ////IList<IWebElement> ClickList = driver.FindElements(By.ClassName("q3h"));
            ////IList<IWebElement> ClickList = new List<IWebElement>();
            ////foreach (IWebElement s in oCheckBox)
            ////{
            ////    ClickList.Add(s.FindElement(By.TagName("span")));
            ////}

            ////ClickList[0].Click();
            ////Thread.Sleep(3000);
            ////driver.Navigate().Back();
            ////driver.Navigate().Refresh();
            ////ClickList = driver.FindElements(By.ClassName("q3h"));
            ////Thread.Sleep(3000);
            ////ClickList[1].Click();
            ////Thread.Sleep(3000);
            ////driver.Navigate().Back();

            return "";
        }
    }
}
