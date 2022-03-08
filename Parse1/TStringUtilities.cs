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
    class TStringUtilities
    {
        public static int CardCounter = 1;
        /// <summary>
        /// Находит из многих строк с разделителями нужные по номерам строк и выписывает их в строку с табуляционным разделителем
        /// </summary>
        /// <param name="Initial"></param>
        /// <returns></returns>
        public string SelectedRows(string Initial, int[]Rows)
        {
            string[] separators = new string[] { "\t", "\r\n" };
            string[] fil = Initial.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            int Size = fil.GetLength(0);
            int Size1 = Rows.GetLength(0);
            string Ans = "";

            for (int i=0;i<Size1;i++)
            {
                if (i< Size1-1)
                {
                    Ans = Ans + fil[Rows[i]] + "\t";
                }
                else
                {
                    Ans = Ans + fil[Rows[i]];
                }
            }
            return Ans;
        }
        /// <summary>
        /// Преобразует строку со спарсенными ценами в список цен до- и после скидки без пробелов и рублей. Если скидки нет, то поле "до" пустое
        /// </summary>
        /// <param name="OldPrices">Строка со спарсенными ценами</param>
        /// <returns></returns>
        public List<string> ClearPrices(string OldPrices)
        {
            //Начальный список из 2 цен, равных 0
            string[] PricesString = new string[2];
            PricesString[0] = "";
            PricesString[1] = "";

            int Rows = 0;
            foreach (char Char1 in OldPrices)
            {
                if (Char1 == '₽')
                {
                    Rows++;//переносим на следующую строку
                }
                if (char.IsDigit(Char1) == true) //выводим только цифры
                {
                    PricesString[Rows] += Char1;
                }

            }
            //Получаем стринговый список цен после и до скидки, либо 0 вместо "до"
            List<string> Prices = PricesString.ToList<string>();

            //List<string> Prices = new List<string>();
            return Prices;
        }
        /// <summary>
        /// Находит сначала индексы вхождения заданных нужных параметров карточки в спарсенные с карты параметры, затем выводит соответствующие им значения запрашиваемых параметров
        /// </summary>
        /// <param name="InputParameters">Задаваемый в Program список параметров в шапке таблицы</param>
        /// <param name="WhereToFind">Спарсенный список фактических имеющихся параметров карточки</param>
        /// <param name="WhatToFind">Спарсенный список фактических значений параметров карточки</param>
        /// <returns></returns>
        public List<string> ChooseParameters (List<string> InputParameters, List<string> WhereToFind, List<string> WhatToFind)
        {
            int SizeInput = InputParameters.Count;
            int SizeParsing = WhereToFind.Count;
            int[] indexes = new int[SizeInput];
            List<string> FoundData = new List<string>();

            for (int i=0;i< SizeInput;i++)
            {
                indexes[i] = WhereToFind.IndexOf(InputParameters[i]);
                    if (indexes[i] == -1)
                    {
                        FoundData.Add("!НЕТ ДАННЫХ!");
                    }
                    else
                    {
                        FoundData.Add(WhatToFind[indexes[i]]);
                    }
                    
            }

            //List<string> Prices = new List<string>();
            return FoundData;
        }
        /// <summary>
        /// Выводим числовую часть из строки, находящуюся перед словесной по первой букве первого слова
        /// </summary>
        /// <param name="BasicString"></param>
        /// <param name="Letter"></param>
        /// <returns></returns>
        public string GetStringBeforeLetters (string BasicString, string Letter)
        {
            //Отсекаем числовую часть по первому вхождению буквы "Letter" в первом слове
            int IndexOff = BasicString.IndexOf(Letter);
            String NewString = "";
            for (int i = 0; i < IndexOff - 1; i++)
            {
                NewString += BasicString[i]; // записываем всё, что левее буквы
            }
            return NewString;
        }

        public void TesingConcat()
        {
            string a = "";
            string b = "word";
            string c = string.Concat(a, b);

            string d = "word\r\npush\tafter";

            string e = string.Concat(c, d);

            for (int i=0;i<10;i++)
            {
                e = string.Concat(e, d);
            }

            File.WriteAllText("Concat.txt", e);
        }

        public void ExportString (string Name)
        {
            StreamReader Reader = new StreamReader(Name);
            string II = Reader.ReadToEnd();

            string[] separators = new string[] { "\r\n" };
//string[] separators = new string[] { "\t", "\r\n" };
            string[] fil = II.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            File.WriteAllLines("WriteLines.txt", fil);


        }

        




    }
}
