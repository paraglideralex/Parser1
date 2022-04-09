using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse1
{
    internal class TProductDataCollection
    {
        public List<string> InputRankParameters = new List<string>()
            { "Название", "Артикул", "Ссылка",  "Отзывы","К-во видео",
              "К-во вопросов", "Рейтинг","Цена после скидки", "Цена до скидки"};


        public List<string> TeplyPolMat = new List<string>()
            { "Площадь обогрева, кв.м", "Макс. мощность, Вт",   "Толщина, мм",
              "Страна-изготовитель",  "Длина, м",             "Ширина, м"  ,
              "Размеры, мм",            "Вид обогрева",         "Особенности"};
    }
}
