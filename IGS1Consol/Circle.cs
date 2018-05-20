using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGS1Consol
{
    class Circle//класс круг
    {
        public float X, Y, Radius;//координаты круга
        public Circle()//конструктор по умолчанию
        {
        }
        public Circle(float x, float y,float rad)//конструктор с инициализацией стартовой позиции

        {
            X = x;
            Y = y;
            Radius = rad;
        }
    }
}
