using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using OpenQA.Selenium.Support.UI;
using System.Globalization;


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
        /// <param name="CardCounter">абсолютный счётчик спарсенного числа карточек</param>
        /// <param name="file">текстовый поток для вывода в файл</param>
        /// <returns>строка с параметрами одной карточки</returns>
        public string CardParser(IWebDriver driver, List<string> InputParameters, ref int CardCounter, StreamWriter file)
        {
            //делаем автоматическое ожидание появления элементов
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            TFunctions Functions = new TFunctions();
            TStringUtilities StringUtilities = new TStringUtilities();
            //
            CardCounter++;
            Console.WriteLine("---Карточка #" + CardCounter+"---");
            Console.Write("Прогресс: ");

            //перед этим мы: загуглили запрос, перешли, прошли по первой карточке
            //Начинаем работу
            string URL = driver.Url;
            //Проверяем наличие и статус атрибутов товара, есть ли отзывы, видео итд
            List<string> BasicList = new List<string>();
            IList<IWebElement> Basic = driver.FindElements(By.CssSelector("div[data-widget='column']"));
            var BasicData = driver.FindElement(By.CssSelector("div[data-widget='column']")).Text;

            for (int i=0; i<2; i++)
            {
                BasicList.Add(Basic[i].Text);
            }
            string BasicString = "";
            foreach (string s in BasicList)
            {
                foreach (char c in s)
                {
                    BasicString += c;
                }

            }
            bool VideoIsOrNot = StringUtilities.FindInList(BasicString, "видео");
            bool QuestionIsOrNot = StringUtilities.FindInList(BasicString, "Задать");
            bool ReviewIsOrNot = StringUtilities.FindInList(BasicString, "Оставить");


        //Парсим заголовок по тегу h1
        Found1:
            
            try
            {
                IWebElement Head = driver.FindElement(By.TagName("h1"));
            }
            catch (OpenQA.Selenium.NoSuchElementException e)
            {
                //driver.Navigate().Refresh();
                Console.WriteLine("Неполадки с поиском элементов на странице, скорее всего нужна КАПЧА, перезагружаюсь....");
                Console.WriteLine("");            
                driver.Navigate().Refresh();
                Thread.Sleep(10000);
                goto Found1;
                
            }
            IWebElement Head1 = driver.FindElement(By.TagName("h1"));
            string Head1Text = Head1.Text;
            Console.Write(TStringUtilities.OutputSeparator+"Название"+ TStringUtilities.OutputSeparator);

        //Парсим код товара 
        Found3:
            string CodeTextPrev = "";
            try
            {
                CodeTextPrev = driver.FindElement(By.CssSelector("span[data-widget='webDetailSKU']")).Text;
            }
            catch (OpenQA.Selenium.NoSuchElementException e)
            {
                Console.WriteLine("Неполадки с поиском АРТИКУЛА, перезагружаюсь....");
                Console.WriteLine("");
                //driver.Navigate().Refresh();
                Thread.Sleep(10000);
                goto Found3;
            }
            
            string CodeText = StringUtilities.OnlyDigits(CodeTextPrev);

            Console.Write("Артикул"+ TStringUtilities.OutputSeparator);
            //Парсим параметры вовлечённости     
            string RewiewsText = "0";
            string RewiewsTextNum = "";
            //Thread.Sleep(100);

            if (ReviewIsOrNot == false)//если есть отзывы - парсим
            {
                try
                {
                    //RewiewsText = driver.FindElement(By.XPath("(//div[@class='ui-f'][contains(.,' отзы')])[1]")).Text;//ui-d7//ui-e8
                    RewiewsText = driver.FindElement(By.CssSelector("div[data-widget='webReviewProductScore']")).Text;
                    RewiewsTextNum = StringUtilities.GetStringBeforeLetters(RewiewsText, "о");
                }
                catch (OpenQA.Selenium.NoSuchElementException e)
                {
                    //IWebElement Video = new IWebElement();
                    RewiewsText = "0";
                }
            }
            else // если нет - приравниваем к нулю
            {
                RewiewsText = "0";
                RewiewsTextNum = "0";
            }

            //string RewiewsTextNum = StringUtilities.GetStringBeforeLetters(RewiewsText, "о");
            Console.Write("Отзывы"+ TStringUtilities.OutputSeparator);

            string VideoText = "0";
            string VideoTextNum = "";
            if (VideoIsOrNot == true)
            {
                try
                {
                    // VideoText = driver.FindElement(By.XPath("(//div[@class='ui-f'][contains(.,' виде')])[1]")).Text;//
                    VideoText = driver.FindElement(By.CssSelector("div[data-widget='webVideosCount']")).Text; // 
                    VideoTextNum = StringUtilities.GetStringBeforeLetters(VideoText, "в");
                }
                catch (OpenQA.Selenium.NoSuchElementException e)
                {
                    //IWebElement Video = new IWebElement();
                    VideoText = "0";
                }             
            }
            else
            {
                VideoText = "0";
                VideoTextNum = "0";
            }
            Console.Write("Видео"+ TStringUtilities.OutputSeparator);

            string QuestionsText = "0";
            string QuestionsTextNum = "";
            //Thread.Sleep(100);
            if (QuestionIsOrNot == false)
            {
                try
                {
                    // VideoText = driver.FindElement(By.XPath("(//div[@class='ui-f'][contains(.,' виде')])[1]")).Text;//
                    QuestionsText = driver.FindElement(By.CssSelector("div[data-widget='webQuestionCount']")).Text;
                    QuestionsTextNum = StringUtilities.GetStringBeforeLetters(QuestionsText, "в");
                }
                catch (OpenQA.Selenium.NoSuchElementException e)
                {
                    //IWebElement Video = new IWebElement();
                    QuestionsText = "0";
                }
            }
            else
            {
                QuestionsText = "0";
                QuestionsTextNum = "0";
            }

            Console.Write("Вопросы"+ TStringUtilities.OutputSeparator);

            //Парсим цены
            //для этого читаем весь див с ценами и ценой в кредит
            string PricesText = "";
            FoundPrice:
            try
            {
                PricesText = driver.FindElement(By.CssSelector("div[data-widget$='webPrice']:not([data-widget$='webBestPrice'])")).Text;
            }
            catch(OpenQA.Selenium.NoSuchElementException e)
            {
                Console.WriteLine("Неполадки с поиском ЦЕН, скроллю вниз....");
                Functions.ScrollDown(driver, 5000);
                //driver.Navigate().Refresh();
                Thread.Sleep(300);
                goto FoundPrice;
            }

            //Считаем количество элементов в диве
            string[] separators = new string[] { "\t", "\r\n" };
            string[] fil = PricesText.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            //если есть пункт "Лучшая цена" - вырезаем его
            string[] PricesText1 = StringUtilities.PriceOnlyNumbers(fil);
            int Size = PricesText1.GetLength(0);
            List<string> PricesNumbers = new List<string>();
            if (fil[0].Equals("Товар закончился"))
            {                             
                PricesNumbers.Add(StringUtilities.OnlyDigits(PricesText1[0]));
                PricesNumbers.Add("Товар закончился"); //если закончился в цену пишем фактическую, а в цену до скидки пишем "закончился"
            }
            else
            {
                //Если можно в кредит, то там 3 строки, если нельзя - одна
                int[] p = { Size - 1 };//если нет, то пишем цены до и после скидки, или же просто цену
                PricesText = StringUtilities.SelectedRows(PricesText, p);
                PricesNumbers = StringUtilities.ClearPrices(PricesText);
            }
            

            Console.Write("Цены"+ TStringUtilities.OutputSeparator);

            //Выводим рейтинг
            var Rate = driver.FindElement(By.CssSelector("div[data-widget='webReviewProductScore']"));
            var Rate5 = Rate.FindElement(By.CssSelector("div[style^='width']"));//параметр ширины - средняя оценка в процентах
            var Rate1 = Rate5.GetAttribute("style");//спарсили процентное значение
            var RateText1 = "";
            foreach (char c in Rate1)
            {
                if (char.IsDigit(c)||c.Equals('.'))
                {
                    RateText1 += c;
                }
                
            }
            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            var Rating = (double)5/(double)100 * double.Parse(RateText1, formatter);//вывели точный рейтинг по процентам от 5

            string RateText = Rating.ToString();
            RateText = RateText.Replace(',', '.');//поменяли на точку для унификации с остальными значениями, которые тоже все через точку
            Console.Write("Рейтинг"+ TStringUtilities.OutputSeparator);

            //Вычленяем бренд товара
            //Он может быть представлен либо в виде фотки, либо текстом
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            //Сначала ищем по фотке с title, такие чаще встречаются
            string BrandText = "";
            try
            {
                var BrandText1 = driver.FindElement(By.CssSelector("img[title^='Все товары бренда']"));
                string BrandText2 = BrandText1.GetAttribute("title");
                BrandText = BrandText2.Substring(18);
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {
                Console.WriteLine("Не найден бренд с фотографией, пробуем по словам");

                BrandText = "";
            }
            //Если за 7 секунд найти не удалось, значит бренд таекстовый - ищем бренд без картинки
            if (BrandText == "")
            { 
                try
                {
                    var BrandText1 = driver.FindElement(By.CssSelector("div[data-widget='webBrand']")); 
                    var BrandText2 = BrandText1.FindElement(By.TagName("a")).Text;
                    BrandText = BrandText2.Substring(18);
                }
                catch (OpenQA.Selenium.NoSuchElementException)
                {
                    Console.WriteLine("За 10 секунд не найден бренд, скорее всего его нет");

                    BrandText = "БРЕНД НЕ НАЙДЕН";
                }
            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            //вычленяем продавца товара
            string SellerText = "";
            try
            {
                var Seller1 = driver.FindElement(By.CssSelector("div[data-widget='webCurrentSeller']"));
                var Seller2 = Seller1.FindElements(By.TagName("a"));
                SellerText = Seller2[1].GetAttribute("title");
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {
                Console.WriteLine("Продавец не найден");

                SellerText = "ПРОДАВЕЦ НЕ НАЙДЕН";
            }



            //вычленяем названия параметров товара
            List<string> ParameterNames1 = new List<string>();
            List<string> Parameters1 = new List<string>();
            Functions.ScrollDown(driver, 5000); //скроллим к параметрам
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

        Found2:
            IList<IWebElement> DataNames = new List<IWebElement>();
            IList<IWebElement> DataMeanings = new List<IWebElement>();
            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                //есть 2 поля с данным ксс-селектором - вначале (краткое) и в конце (полное)
                IList<IWebElement> Data1 = driver.FindElements(By.CssSelector("div[data-widget='webCharacteristics']"));
                //выбираем которое в конце
                IWebElement Data2 = Data1[1];
                DataNames = Data2.FindElements(By.TagName("dt")); //выбираем названия характеристик по тегу
                DataMeanings = Data2.FindElements(By.TagName("dd")); //выбираем сами характеристики по тегу
            }
            catch (System.ArgumentOutOfRangeException e)
            {              
                Console.WriteLine("Неполадки с поиском ГРУППЫ ХАРАКТЕРИСТИК, скроллю вниз и перезагружаюсь....");
                Functions.ScrollDown(driver, 5000);
                Thread.Sleep(100);
                goto Found2;
            }

            // если характеристики не успели заполниться, откатываемся назад, и так до полной прогрузки страницы
            foreach (IWebElement W in DataNames)
            {
                if ((W.Text).Equals(""))
                {
                    goto Found2;
                }
                else
                {
                 ParameterNames1.Add(W.Text);
                }                           
            }

            foreach (IWebElement W in DataMeanings)
            {
                if ((W.Text).Equals(""))
                {
                    goto Found2;
                }
                else
                {
                    Parameters1.Add(W.Text);
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
            Console.Write("Параметры"+TStringUtilities.OutputSeparator);
            //Выделяем нужные атрибуты для внесения
            List<string> Attributes = new List<string>();
            Attributes.Add(Head1Text);
            Attributes.Add(CodeText);
            Attributes.Add(URL);
            Attributes.Add(BrandText);
            Attributes.Add(SellerText);
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

            file.WriteLineAsync(OutputString);//+"\r\n");
            Console.Write("ГОТОВО");
            Console.WriteLine();
            return OutputString;
            
        }
        /// <summary>
        /// Субпрога, парсит одну страницу с совокупностью карточек, на вывод - одна строка со всеми карточками, разбитыми построчно
        /// </summary>
        /// <param name="driver">созданный драйвер</param>
        /// <param name="InputParameters">список параметров для CardParser</param>
        /// <param name="file">текстовый поток для вывода в файл</param>
        /// <param name="NumOfCards">ограничение по количеству карточек для парсинга</param>
        /// <returns>строка со всеми карточками страницы</returns>
        public string PageParser(IWebDriver driver, List<string> InputParameters, StreamWriter file, int NumOfCards)
        {
            TPages Pages = new TPages();
            TFunctions Functions = new TFunctions();
            string PageString = "";
            Thread.Sleep(200);
            Functions.ScrollDown(driver, 5000);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            //Functions.ScrollDown(driver, 5000);
            //Thread.Sleep(2000);
            
            String CurrentPageURL = driver.Url;
            //Находим координаты всех карточек на странице
            //Сначала находим див со ссылкой на страницу
            IList<IWebElement> ClickList0 = driver.FindElements(By.CssSelector("a[class*='tile-hover-target']"));//класс, содержащий в себе "tile.."
            //для каждой карточки их 2 (ссылка на фотке и на тексте с названием)
            IList<IWebElement> ClickList = new List<IWebElement>();
            int counter = 0;
            foreach (var v in ClickList0)
            {
                counter++;
                if(counter%2==0)
                    {
                    ClickList.Add(v);//выбрали одну из 2
                    }
            }

            List<string> ListOfReferences2Cards = new List<string>();
            //Для каждого дива находим его заголовок "а" и принадлежащий ему аттрибут - ссылку href, сохраняем
            foreach (IWebElement click in ClickList)
            {
                string oo = click.GetAttribute("href");
                ListOfReferences2Cards.Add(oo);
                //i++;
            }

            int NumberOfCards = ClickList.Count;

            for (int j=0;j<NumberOfCards; j++)
            {
                Point1:
                try
                {
                    driver.Navigate().GoToUrl(ListOfReferences2Cards[j]);
                }
                catch (OpenQA.Selenium.WebDriverException e)
                {
                    Console.WriteLine("Не удалось перейти на страницу карточки, пропускаем её");
                    Console.WriteLine(e.Message);
                    j++;
                    goto Point1;

                }

                string CardParserString = Pages.CardParser(driver, InputParameters, ref TStringUtilities.CardCounter, file);
                PageString += CardParserString + "\r\n";

                if (NumOfCards<=TStringUtilities.CardCounter)
                {
                    return PageString; //выходим из функции, если спарсили заданное количество карточек
                }

            }

            //возвращаемся на страницу с карточками, чтобы перейти по кнопке Далее
            driver.Navigate().GoToUrl(CurrentPageURL);
            File.WriteAllText("WriteText333.txt", PageString);


            return PageString;
        }
        /// <summary>
        /// Главная функция парсинга выбранного количества страниц/карточек и вывода
        /// </summary>
        /// <param name="driver">созданный драйвер</param>
        /// <param name="InputParameters">Параметры с карточки на вывод и на вход субпрогам</param>
        /// <param name="NumberOfPages">Количество страниц для парсинга</param>
        /// <returns>строка со всеми карточками</returns>
        public string ParseTotal(IWebDriver driver, List<string> InputParameters, int NumberOfPages, int NumOfCards, StreamWriter file)
        {
            TPages Pages = new TPages();
            string TotalParceData = "";
            //string ClickList0 = driver.FindElement(By.CssSelector("div[data-widget='fulltextResultsHeader']")).Text;//Проверяем, сколько всего товароd
            for (int i = 0; i < NumberOfPages; i++)
            {
                //OpenQA.Selenium.NoSuchElementException
                string Card = Pages.PageParser(driver, InputParameters, file, NumOfCards);
                Card += "\r\n";
                TotalParceData = string.Concat(TotalParceData, Card);

                if (NumOfCards <= TStringUtilities.CardCounter)
                {
                    File.WriteAllText("WriteTextTotal.txt", TotalParceData);
                    Console.WriteLine("");
                    Console.WriteLine("==================================");
                    Console.WriteLine("==================================");
                    Console.WriteLine("Закончил парсить по количеству карточек");
                    Console.WriteLine("==================================");
                    Console.WriteLine("==================================");
                    return TotalParceData;
                }

            // обновляем каждый виток, чтобы не выдавало ошибку
            Found:
                Console.WriteLine("");
                Thread.Sleep(2000);
                Console.WriteLine("*************************************");
                Console.WriteLine("*************************************");
                Console.WriteLine("Считал " + (i+1) + " cтраницу, перехожу на " + (i + 2));
                Console.WriteLine("*************************************");
                Console.WriteLine("*************************************");
                try
                {
                    //IWebElement NextPage = driver.FindElement(By.XPath("//div[@class='ui-f'][contains(.,'Дальше')]"));
                    IWebElement NextPage = driver.FindElement(By.LinkText("Дальше"));
                    NextPage.Click();
                }
                catch (OpenQA.Selenium.NoSuchElementException e)
                {
                    driver.Navigate().Refresh();
                    Thread.Sleep(6000);                  
                    driver.Navigate().Back();
                    Console.WriteLine("Неполадки с поиском кнопки Далее, перезагружаюсь....");
                    goto Found;
                }
                Thread.Sleep(2000);
            }

            File.WriteAllText("WriteTextTotal.txt", TotalParceData);
            Console.WriteLine("");
            Console.WriteLine("==================================");
            Console.WriteLine("==================================");
            Console.WriteLine("Закончил парсить по количеству страниц");
            Console.WriteLine("==================================");
            Console.WriteLine("==================================");
            return TotalParceData;
        }

    }
}
