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
        /// <param name="InputParameters">список переменных, которые хотим найти на карточке и вывести</param>
        public string CardParser(IWebDriver driver, List<string> InputParameters, ref int CardCounter, StreamWriter file)
        {
            //
            CardCounter++;
            Console.WriteLine("********");
            Console.WriteLine("Карточка ## " + CardCounter);
            Console.WriteLine("********");
            //
            //перед этим мы: загуглили запрос, перешли, прошли по первой карточке
            // Переходим по запросу

            TStringUtilities StringUtilities = new TStringUtilities();

            //Начинаем работу
            //Парсим заголовок по тегу h1
            
            Found1:
            Console.WriteLine("");
            Thread.Sleep(5000);
            try
            {
                IWebElement Head = driver.FindElement(By.TagName("h1"));
            }
            catch (OpenQA.Selenium.NoSuchElementException e)
            {
                //driver.Navigate().Refresh();
                Console.WriteLine("Неполадки с поиском элементов на странице, скорее всего нужна КАПЧА, перезагружаюсь....");
                goto Found1;
            }
            IWebElement Head1 = driver.FindElement(By.TagName("h1"));
            String Head1Text = Head1.Text;

            //Парсим код товара 
            //IWebElement Code = driver.FindElement(By.XPath("//span[@class='jv3 vj3'][contains(.,'Код')]")); исходный

            //IWebElement Code2 = driver.FindElement(By.XPath("//div[text() = 'Код ']"));// нет
            //button
            //IWebElement Code3 = driver.FindElement(By.XPath("//div[. = 'Код']"));//нет

            // IWebElement Code4 = driver.FindElement(By.XPath("//*[contains(., 'Код')]"));
            //string CodeTextPrev4 = Code4.Text;//вообще весь
            //IWebElement Code5 =  driver.FindElement(By.XPath("//div[contains(text(),'Код')]"));//нет
            //string CodeTextPrev5 = Code5.Text;
            //IWebElement Code6 = driver.FindElement(By.XPath("//div[][. = 'Код']"));//неправильный икс пасс
            //string CodeTextPrev5 = Code6.Text;
            string CodeTextPrev = driver.FindElement(By.CssSelector("span[data-widget='webDetailSKU']")).Text;
            // string CodeTextPrev2 = Code2.Text;
            // string CodeTextPrev3 = Code3.Text;
            ////вычленяем параметры товара
            ///webVideosCount




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
            
            

            string RewiewsText = "0";
            //Thread.Sleep(100);
            try
            {
                //RewiewsText = driver.FindElement(By.XPath("(//div[@class='ui-f'][contains(.,' отзы')])[1]")).Text;//ui-d7//ui-e8
                RewiewsText = driver.FindElement(By.CssSelector("div[data-widget='webReviewProductScore']")).Text;
            }
            catch (OpenQA.Selenium.NoSuchElementException e)
            {
                //IWebElement Video = new IWebElement();
                RewiewsText = "0";
            }

            string RewiewsTextNum = StringUtilities.GetStringBeforeLetters(RewiewsText, "о");


            string VideoText = "0";
            //Thread.Sleep(100);
            try
            {
                //IWebElement Video = ;
                // VideoText = driver.FindElement(By.XPath("(//div[@class='ui-f'][contains(.,' виде')])[1]")).Text;//
                VideoText = driver.FindElement(By.CssSelector("div[data-widget='webVideosCount']")).Text; // 
            }
            catch (OpenQA.Selenium.NoSuchElementException e)
            {
                //IWebElement Video = new IWebElement();
                 VideoText = "0";
            }
            
            string VideoTextNum = StringUtilities.GetStringBeforeLetters(VideoText, "в");
            //Thread.Sleep(100);

            //IWebElement Questions = driver.FindElement(By.XPath("(//div[@class='ui-f'][contains(.,' вопро')])[1]"));
            string QuestionsText = driver.FindElement(By.CssSelector("div[data-widget='webQuestionCount']")).Text;
            //string QuestionsText = Questions.Text;
            string QuestionsTextNum = StringUtilities.GetStringBeforeLetters(QuestionsText, "в");

            //Парсим цены
            //для этого читаем весь див с ценами и ценой в кредит
            //IWebElement Prices = driver.FindElement(By.ClassName("k3o"));//lk3//
            IWebElement Prices = driver.FindElement(By.CssSelector("div[data-widget='webPrice']"));    
            string PricesText = Prices.Text;
            //Считаем количество элементов в диве
            string[] separators = new string[] { "\t", "\r\n" };
            string[] fil = PricesText.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            int Size = fil.GetLength(0);
            //Из него выводим только последнюю строку
            //Если можно в кредит, то там 3 строки, если нельзя - одна
            int[] p = { Size-1 };
            PricesText = StringUtilities.SelectedRows(PricesText, p);
            List<string> PricesNumbers = StringUtilities.ClearPrices(PricesText);

            //Выводим рейтинг
            string Rate = driver.FindElement(By.CssSelector("div[data-widget='webCurrentSeller']")).Text;

            //string Rate1 = driver.FindElement(By.TagName("span")).FindElement(By.TagName("strong")).Text;
            string RateNew = StringUtilities.FindWhatUNeed(Rate);
            RateNew.Replace(" ", "");
            string RateText = StringUtilities.GetStringBeforeLetters(RateNew, "и");
            //вычленяем названия параметров товара

            List<string> ParameterNames1 = new List<string>();
            List<string> Parameters1 = new List<string>();
            string Data = driver.FindElement(By.Id("section-characteristics")).Text;
            string[] DataArr = StringUtilities.ToStringArray(Data);
            int DataLength = DataArr.GetLength(0);
            for (int i=1; i<DataLength-1; i++)
            {
                if (i%2!=0)
                {
                    ParameterNames1.Add(DataArr[i]);
                }
                else
                {
                    Parameters1.Add(DataArr[i]);
                }
            }

            string ParameterNamesStr1 = "";
            foreach (string s in ParameterNames1)
            {
                ParameterNamesStr1 += s + "\t";
            }

            string ParameterStr1 = "";
            foreach (string s in Parameters1)
            {
                ParameterStr1 += s + "\t";
            }

            //IList<IWebElement> ParameterNamesCol = driver.FindElements(By.ClassName("lj9"));
            ////string t = oCheckBox.Text;
            //List<string> ParameterNames = new List<string>();
            
            //foreach (IWebElement s in ParameterNamesCol)
            //{
            //    //var yy = s.FindElement(By.ClassName("ui-p8"));
            //    //Console.WriteLine(yy.Text);
            //    string Text = s.Text;
            //    Text = Text.Replace("\r\n", "\t");
            //    ParameterNames.Add(Text);
            //}
            //string ParameterNamesStr = "";
            //foreach (string s in ParameterNames)
            //{
            //    ParameterNamesStr += s + "\t";
            //}

            //////вычленяем параметры товара
            //IList<IWebElement> ParameterCol = driver.FindElements(By.ClassName("l9j"));
            ////string t = oCheckBox.Text;
            //List<string> Parameters = new List<string>();
            //foreach (IWebElement s in ParameterCol)
            //{
            //    //var yy = s.FindElement(By.ClassName("ui-p8"));
            //    //Console.WriteLine(yy.Text);
            //    string Text = s.Text;
            //    Text = Text.Replace("\r\n", "\t");
            //    Parameters.Add(Text);
            //}
            //string ParameterStr = "";
            //foreach (string s in Parameters)
            //{
            //    ParameterStr += s + "\t";
            //}

            //Выделяем нужные атрибуты для внесения
            List<string> Attributes = new List<string>();
            Attributes.Add(Head1Text);
            Attributes.Add(CodeText);
            Attributes.Add(RewiewsTextNum);
            Attributes.Add(VideoTextNum);
            Attributes.Add(QuestionsTextNum);
            Attributes.Add(RateText);
            Attributes.AddRange(PricesNumbers);

            // Выделяем нужные параметры для внесения в таблицу
            List<string> ParametersOut = StringUtilities.ChooseParameters(InputParameters, ParameterNames1, Parameters1);
            //Делаем список всех параметров на вывод
            List<string> Output = Attributes;
            Output.AddRange(ParametersOut);
            //Делаем из списка строку
            string OutputString = "";
            foreach (string e in Output)
            {
                OutputString += e + "\t";
            }
            file.WriteLineAsync(OutputString+"\r\n");
            return OutputString;

            Console.WriteLine();
            


        }
        /// <summary>
        /// Субпрога, парсит одну страницу с совокупностью карточек, на вывод - одна строка со всеми карточками, разбитыми построчно
        /// </summary>
        /// <param name="driver">созданный драйвер</param>
        /// <param name="InputParameters">список параметров для CardParser</param>
        /// <returns></returns>
        public string PageParser(IWebDriver driver, List<string> InputParameters, StreamWriter file)
        {
            TPages Pages = new TPages();
            string PageString = "";
          
            //Находим координаты всех карточек на странице
            //Сначала находим див со ссылкой на страницу
            IList<IWebElement> ClickList = driver.FindElements(By.ClassName("yh5"));
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
                //Console.WriteLine("Парсим карточку # " + j);
                // используя сохранённые ссылки, можно гулять по всей странице, ниже это сделано вручную
                driver.Navigate().GoToUrl(ListOfReferences2Cards[j]);
                Thread.Sleep(1000);
                string CardParserString = Pages.CardParser(driver, InputParameters, ref TStringUtilities.CardCounter, file);
                PageString += CardParserString + "\r\n";

                foreach (char Ch in CardParserString)
                {
                    read.Write(Ch);
                }
                read.Write("\r\n");

                driver.Navigate().Back();
                Thread.Sleep(100);
                driver.Navigate().Refresh();
                Thread.Sleep(100);
            }

            File.WriteAllText("WriteText333.txt", PageString);

            ///https://automated-testing.info/t/obshhij-algoritm-resheniya-element-is-not-attached-to-page-document/13003/5
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


            return PageString;
        }
        /// <summary>
        /// Главная функция парсинга выбранного количества страниц и вывода
        /// </summary>
        /// <param name="driver">созданный драйвер</param>
        /// <param name="InputParameters">Параметры с карточки на вывод и на вход субпрогам</param>
        /// <param name="NumberOfPages">Количество страниц для парсинга</param>
        /// <returns></returns>
        public string ParseTotal(IWebDriver driver, List<string> InputParameters, int NumberOfPages, StreamWriter file)
        {
            TPages Pages = new TPages();
            string TotalParceData = "";

            for (int i = 0; i < NumberOfPages; i++)
            {
                //OpenQA.Selenium.NoSuchElementException
                string Card = Pages.PageParser(driver, InputParameters, file);
                Card += "\r\n";
                TotalParceData = string.Concat(TotalParceData, Card);
                // обновляем каждый виток, чтобы не выдавало ошибку
                Found:
                Console.WriteLine("");
                Thread.Sleep(5000);
                try
                {
                    IWebElement NextPage = driver.FindElement(By.XPath("//div[@class='ui-f'][contains(.,'Дальше')]"));
                    NextPage.Click();
                }
                catch (OpenQA.Selenium.NoSuchElementException e)
                {
                    driver.Navigate().Refresh();
                    driver.Navigate().Back();
                    Console.WriteLine("Неполадки с поиском кнопки Далее, перезагружаюсь....");
                    goto Found;
                }
                Thread.Sleep(1000);

            }

            File.WriteAllText("WriteTextTotal.txt", TotalParceData);
            return TotalParceData;
        }

    }
}
