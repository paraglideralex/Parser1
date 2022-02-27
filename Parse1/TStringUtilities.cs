using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse1
{
    class TStringUtilities
    {
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
            string OP = "1 795 ₽ 3 780 ₽";
            //OP= OP.Replace(' ');
            List<string> Prices = new List<string>();
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

            for (int i=0;i< SizeInput-1;i++)
            {
                    indexes[i] = WhereToFind.IndexOf(InputParameters[i]);
                if (indexes[i] == -1)
                {
                    indexes[i] = SizeInput - 1;
                }
                    FoundData.Add(WhatToFind[indexes[i]]);
            }

            //List<string> Prices = new List<string>();
            return FoundData;
        }




    }
}
