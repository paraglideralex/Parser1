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
    class TExamples
    {
        public void Navigation()
        {
            //We have four Navigation commands

            //GoToUrl
            //Back
            //Forward
            //Refresh
            IWebDriver WebDriver = new ChromeDriver();
            WebDriver.Navigate().GoToUrl("https://toolsqa.com");

        }

        public void Properties()
        {

            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.ozon.ru/category/elektricheskie-teplye-poly-10217/?category_was_predicted=true&from_global=true&text=%D1%82%D0%B5%D0%BF%D0%BB%D1%8B%D0%B9+%D0%BF%D0%BE%D0%BB+%D1%8D%D0%BB%D0%B5%D0%BA%D1%82%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9");

            //IWebElement element = driver.FindElement(By.XPath("anyLink"));

            IWebElement element1 = driver.FindElement(By.XPath("(//span[contains(.,'Тёплый пол электрический')])[3]"));
            String linkText = element1.Text;
            String tagName = element1.TagName;
            String attValue = element1.GetAttribute("id"); //This will return "SubmitButton"


            Console.WriteLine("Так выглядит работа " + "linkText = element1.Text");
            Console.WriteLine(linkText);
            Console.WriteLine("Так выглядит работа " + "linkText = element1.TagName");
            Console.WriteLine(tagName);
            Console.WriteLine("Так выглядит работа " + "element1.GetAttribute(id)");
            Console.WriteLine(attValue);
            //driver.FindElement(By.Id("UserName")).Clear();
        }

        public void Attributes()
        {
            //Attributes carry additional information about an HTML element 
            //and come in name = "value" pairs.Example: < div class=”my-class”></div>. 
            //Here we have a div tag and it has a class attribute with a value of my-class. 
            //Property is a representation of an attribute in the HTML DOM(Document Object Model ) tree.
            //So the attribute in the example above would have a property named className with a value of my-class.
            //Attributes are defined by HTML while Properties are defined by DOM.

            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.ozon.ru/category/elektricheskie-teplye-poly-10217/?category_was_predicted=true&from_global=true&text=%D1%82%D0%B5%D0%BF%D0%BB%D1%8B%D0%B9+%D0%BF%D0%BE%D0%BB+%D1%8D%D0%BB%D0%B5%D0%BA%D1%82%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9");

            //IWebElement element = driver.FindElement(By.XPath("anyLink"));

            //IWebElement element1 = driver.FindElement(By.XPath("(//span[contains(.,'пол')])[3]"));
            //String linkText = element1.Text;
            //String tagName = element1.TagName;
            //String attValue = element1.GetAttribute("id"); //This will return "SubmitButton"

            IWebElement element = driver.FindElement(By.XPath("(//span[contains(.,'Тёплый пол электрический под плитку 1 м2 с терморегулятором')])[3]"));
            //ph8 h9p
            var t = element.GetAttribute("oc3 co4 oc4 c6o f-tsBodyL o2h");


            Console.WriteLine();
        }
        /// <summary>
        /// идёт на Озон, вводит нужный запрос, переходит
        /// </summary>
        public void FirstTry()
        {
            //Загрузили Селениум через управление пакетами NUGet
            //Добавили пространства имён

            //using OpenQA.Selenium;
            //using OpenQA.Selenium.Chrome;
            //using System.Threading;

            //Загрузили файл Хром-драйвера в папку bin/debug
            //создали Хром-драйвер
            IWebDriver WebDriver = new ChromeDriver();

            //шлём драйвер на нужный нам сайт
            WebDriver.Url = @"https://www.ozon.ru/";

            Thread.Sleep(100);

            //находим поля для ввода
            WebDriver.FindElement(By.XPath("//input[contains(@placeholder,'Искать на Ozon')]"));

            Thread.Sleep(250);
            //Console.WriteLine("Введите поисковой запрос");
            //string Seasrch = Console.ReadLine();
            WebDriver.FindElement(By.XPath("//input[contains(@placeholder,'Искать на Ozon')]")).SendKeys("тёплый пол электрический");

            Task.Delay(250);
            // названия классов часто меняются
            WebDriver.FindElement(By.XPath("//button[contains(@class,'xp6')]")).Click();

            Task.Delay(2500);

            //У меня именно ElementS, потому что много элементов
            // WebDriver.FindElements(By.XPath("//button[contains(@class,'sq6')]").GetAttribute("textContent");

            var links = WebDriver.FindElements(By.XPath(".//h2/a"));
        }

        public void FindElem()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.ozon.ru/category/elektricheskie-teplye-poly-10217/?category_was_predicted=true&from_global=true&text=%D1%82%D0%B5%D0%BF%D0%BB%D1%8B%D0%B9+%D0%BF%D0%BE%D0%BB+%D1%8D%D0%BB%D0%B5%D0%BA%D1%82%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9");
            // по айди
            IWebElement element = driver.FindElement(By.Id("stickyHeader"));
            string ByID = element.GetAttribute("id");
            //по имени - не актуально
            // IWebElement element2 = driver.FindElement(By.Name("firstname"));

            // по классу - не работает с пробелами
            //IWebElement parentElement = driver.FindElement(By.ClassName("tile - hover - target o2hh"));
            //IWebElement parentElement2 = driver.FindElement(By.ClassName("ph8 h9p"));
            IWebElement parentElement = driver.FindElement(By.ClassName("hs3"));
            //затем можно обращаться к свойствам уже найденного элемента, ниже - код с инета
            //IWebElement parentElement = driver.FindElement(By.ClassName("button"));
            //IWebElement childElement = parentElement.FindElement(By.Id("submit"));
            //childElement.Submit();

            //По имени тега, говорят, непопулярно
            //IWebElement TagElement = driver.FindElement(By.TagName("hs3"));

            //По имени ссылки - полному или частичному
            //после ссылки должны стоять теги к ней, как в примере, иначе не работает
            //IWebElement refelement = driver.FindElement(By.PartialLinkText("/product/"));

            //Так мы работаем со всеми элементами
            var NparentElement = driver.FindElements(By.ClassName("p9h"));

            IList<IWebElement> oCheckBox = driver.FindElements(By.ClassName("p9h"));
            List<string> Cards = new List<string>();
            // This will tell you the number of checkboxes are present
            int Size = oCheckBox.Count;

            // Start the loop from first checkbox to last checkboxe
            for (int i = 0; i < Size; i++)
            {
                // Store the checkbox name to the string variable, using 'Value' attribute
                // String Value = oCheckBox.ElementAt(i).GetAttribute("value");
                //IWebElement childElement1 = oCheckBox.ElementAt(i).FindElement(By.ClassName("tile-hover-target o2h"));
                //IWebElement childElement3 = oCheckBox.ElementAt(i).FindElement(By.TagName("span"));

                //IWebElement childElement2 = oCheckBox.ElementAt(i).FindElement(By.XPath("(//span[contains(.,'пол')])[3]"));
                //string y = childElement2.GetAttribute("id");

                //String Value = childElement2.Text;
                String Value = oCheckBox.ElementAt(i).Text;


                Cards.Add(Value);
                Cards.Add("\r\n");
                Cards.Add("#####");
                Cards.Add("\r\n");
                Console.WriteLine(Value);

            }

            TConverter Converter = new TConverter();

            Converter.ConvertFromList(Cards);




            IWebElement childElement = parentElement.FindElement(By.Id("submit"));
            childElement.Submit();

            //IWebElement element2 = driver.FindElement(By.

            Console.WriteLine("Так выглядит работа " + "FindElement(By.Id(stickyHeader))");
            Console.WriteLine(ByID);




        }
        /// <summary>
        /// Вычленяет дивы карточек и выводит из них весь текст. Работает, но нужно много операций с текстом
        /// </summary>
        public void FindElem1()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.ozon.ru/category/elektricheskie-teplye-poly-10217/?category_was_predicted=true&from_global=true&text=%D1%82%D0%B5%D0%BF%D0%BB%D1%8B%D0%B9+%D0%BF%D0%BE%D0%BB+%D1%8D%D0%BB%D0%B5%D0%BA%D1%82%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9");
            // по айди
            IWebElement element = driver.FindElement(By.Id("stickyHeader"));
            string ByID = element.GetAttribute("id");
            //по имени - не актуально
            // IWebElement element2 = driver.FindElement(By.Name("firstname"));

            // по классу - не работает с пробелами
            //IWebElement parentElement = driver.FindElement(By.ClassName("tile - hover - target o2hh"));
            //IWebElement parentElement2 = driver.FindElement(By.ClassName("ph8 h9p"));
            IWebElement parentElement = driver.FindElement(By.ClassName("hs3"));
            //затем можно обращаться к свойствам уже найденного элемента, ниже - код с инета
            //IWebElement parentElement = driver.FindElement(By.ClassName("button"));
            //IWebElement childElement = parentElement.FindElement(By.Id("submit"));
            //childElement.Submit();

            //По имени тега, говорят, непопулярно
            //IWebElement TagElement = driver.FindElement(By.TagName("hs3"));

            //По имени ссылки - полному или частичному
            //после ссылки должны стоять теги к ней, как в примере, иначе не работает
            //IWebElement refelement = driver.FindElement(By.PartialLinkText("/product/"));

            //Так мы работаем со всеми элементами
            var NparentElement = driver.FindElements(By.ClassName("p9h"));

            IList<IWebElement> oCheckBox = driver.FindElements(By.ClassName("p9h"));
            List<string> Cards = new List<string>();
            List<string> Cards1 = new List<string>();
            // This will tell you the number of checkboxes are present
            int Size = oCheckBox.Count;

            // Start the loop from first checkbox to last checkboxe
            for (int i = 0; i < Size; i++)
            {
                // Store the checkbox name to the string variable, using 'Value' attribute
                // String Value = oCheckBox.ElementAt(i).GetAttribute("value");
                //IWebElement childElement1 = oCheckBox.ElementAt(i).FindElement(By.ClassName("tile-hover-target o2h"));
                //IWebElement childElement3 = oCheckBox.ElementAt(i).FindElement(By.TagName("span"));

                //IWebElement childElement2 = oCheckBox.ElementAt(i).FindElement(By.XPath("(//span[contains(.,'пол')])[3]"));
                //string y = childElement2.GetAttribute("id");

                //String Value = childElement2.Text;
                String Value = oCheckBox.ElementAt(i).Text;
                //String Value1 = oCheckBox.ElementAt(i).GetAttribute("tile-hover-target o2h");
                IWebElement Value2 = oCheckBox.ElementAt(i).FindElement(By.XPath("//span[@class='ui-p7 ui-q ui-q2']"));
                //string Price = Value2.GetAttribute("ui - p7 ui - q ui - q2");
                string Price = Value2.Text;


                Cards.Add(Value);
                Cards.Add("\r\n");
                Cards.Add("#####");
                Cards.Add("\r\n");
                Console.WriteLine(Value);

                Cards1.Add(Price);
                Console.WriteLine(Price);
            }

            TConverter Converter = new TConverter();

            Converter.ConvertFromList(Cards);

        }
        /// <summary>
        /// Пробовал выводить только цену по икс пасс - безуспешно
        /// </summary>
        public void FindElem2()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.ozon.ru/category/elektricheskie-teplye-poly-10217/?category_was_predicted=true&from_global=true&text=%D1%82%D0%B5%D0%BF%D0%BB%D1%8B%D0%B9+%D0%BF%D0%BE%D0%BB+%D1%8D%D0%BB%D0%B5%D0%BA%D1%82%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9");

            //IWebElement parentElement2 = driver.FindElement(By.ClassName("ph8 h9p"));
            //var NparentElement = driver.FindElements(By.XPath("//span[@class='ui-p7 ui-q ui-q2'][contains(.,'₽')]"));
            IList<IWebElement> oCheckBox = driver.FindElements(By.XPath("//span[contains(.,'₽')]"));
            int Size = oCheckBox.Count;

            // Start the loop from first checkbox to last checkboxe
            for (int i = 0; i < Size; i++)
            {

                String Value = oCheckBox.ElementAt(i).Text;
                Console.WriteLine(Value);

            }

            Console.WriteLine();

        }
        /// <summary>
        /// Вроде пошло, выделил дивы с карточками, потом выделил цены через текст дивов с ценами, заступорился на выделении текстовых описаний, ничем пока не берёт. Возможно, придётся через много вложенных FindElement
        /// </summary>
        public void FindElem3()
        {
            IWebDriver driver = new ChromeDriver();
            //driver.Navigate().GoToUrl("https://www.ozon.ru/category/elektricheskie-teplye-poly-10217/?category_was_predicted=true&from_global=true&text=%D1%82%D0%B5%D0%BF%D0%BB%D1%8B%D0%B9+%D0%BF%D0%BE%D0%BB+%D1%8D%D0%BB%D0%B5%D0%BA%D1%82%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9");
            //Thread.Sleep(1000);


            driver.Navigate().GoToUrl("https://www.ozon.ru/category/elektricheskie-teplye-poly-10217/?category_was_predicted=true&from_global=true&text=%D1%82%D0%B5%D0%BF%D0%BB%D1%8B%D0%B9+%D0%BF%D0%BE%D0%BB+%D1%8D%D0%BB%D0%B5%D0%BA%D1%82%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9");
            Thread.Sleep(1000);

            // нашли дивы с карточками
            IList<IWebElement> oCheckBox = driver.FindElements(By.ClassName("hm3"));

            //вычленяем из дивов дивы с ценами
            List<string> Prices = new List<string>();
            foreach (IWebElement s in oCheckBox)
            {
                var yy = s.FindElement(By.ClassName("ui-p8"));
                //Console.WriteLine(yy.Text);
                string Text = yy.Text;
                Text = Text.Replace("\r\n", "\t");
                Prices.Add(Text);
            }
            //Записываем цены в файл для проверки
            File.WriteAllLines("Read1.txt", Prices);

            ////////
            /////вычленяем из дивов дивы с ценами
            List<string> Names = new List<string>();
            foreach (IWebElement s in oCheckBox)
            {
                var zz = s.FindElements(By.TagName("span"));
                foreach (IWebElement r in zz)
                {
                    Console.WriteLine(r.Text);
                }

            }
            //Converter.ConvertFromList(Cards);
            Console.WriteLine();

        }

        public void Tread1()
        {
            for (int i=0;i<1000;i++)
            {
                Console.WriteLine(i);
                if (i==200)
                {
                    return;
                }
            }

        }
        /// <summary>
        /// Здесь я тренируюсь строить икс пассы по разным методикам на примере страницы с полом
        /// </summary>
        public void Tryin2Xpath ()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.ozon.ru/product/teplyy-pol-pod-plitku-0-5-m2-s-termoregulyatorom-193001786/?sh=vlf6ogAAAA");

            //1) Xpath Contains() function
            //tag_name[contains(@attribute,'value_of_attribute')]
            //IWebElement Contains1 = driver.FindElement(By.XPath("//div[contains(@class='ui-f', '76 вопросов')]"));
            IWebElement Contains2 = driver.FindElement(By.XPath("(//div[@class='ui-f'][contains(.,' вопро')])[1]"));
            //input[contains(@id, "userN")]
            //string Contains1Text = Contains1.Text;
            string Contains2Text = Contains2.Text;

            //2)XPath Starts-with() function
            ////tag_name[starts-with(@attribute,'Part_of_Attribute_value')]
            ///

            //3)XPath Text() function
            //This function uses the text of the web element for locating the element on the webpage. This function is quite useful if your element contains the text, for example, labels, which always contain a static text.
            ////tag_name[text()='Text of the element']
            //label[text()=”Email”]
            //IWebElement Contains3 = driver.FindElement(By.XPath("//div[text()=' вопросов')]"));
            //string Contains3Text = Contains3.Text;

            // тут мы смогли найти элемент по абсолютному икс-пассу ("в хроме опция Copy full XPath") без имён классов, что очень и очень хорошо
            IWebElement Contains3 = driver.FindElement(By.XPath("/ html / body / div[1] / div / div[1] / div[4] / div[2] / div / div / div[2] / div / div[1] / div[3] / div / a / div / div"));
            string Contains3Text = Contains3.Text;

            // цены
            IWebElement Contains4 = driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div[4]/div[3]/div[2]/div[2]/div/div/div/div[1]/div/div/div[2]"));
            string Contains4Text = Contains4.Text;

            // атрибуты
            IWebElement Contains5 = driver.FindElement(By.TagName("dt"));
            string Contains5Text = Contains5.Text;


            IList<IWebElement> ParameterCol = driver.FindElements(By.TagName("dd"));
            //string t = oCheckBox.Text;
            List<string> Parameters = new List<string>();
            for (int i=0;i<ParameterCol.Count;i++)
            {
                foreach (var s1 in Parameters)
                {
                    if (ParameterCol[i].Text == s1)
                    {
                        i++;
                    }

                }
                    //var yy = s.FindElement(By.ClassName("ui-p8"));
                    //Console.WriteLine(yy.Text);
                    string Text = ParameterCol[i].Text;
                Text = Text.Replace("\r\n", "\t");
                Parameters.Add(Text);
            }
            string ParameterStr = "";
            foreach (string s in Parameters)
            {
                ParameterStr += s + "\t";
            }
            //*[@id="section-characteristics"]/div[2]/div/div[1]/dl[2]

            //string Price = Value2.Text;
            string y = "";

        }

    }
}
