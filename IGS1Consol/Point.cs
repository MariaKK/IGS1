using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGS1Consol
{

    public class Point//класс для хранения и использования точек
    {
        public float X;//координата X


        public float Y;//координата Y

        public Point()//конструтор по умолчанию
        {
     
        }
        public Point(float x, float y)//конструктор с инициализацией позиции

        {
            X = x;
            Y = y;
        }
        
    }

}
