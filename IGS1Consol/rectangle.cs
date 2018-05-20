using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IGS1Consol
{

    public class Rectangle //класс прямоугольник
    {
        public Point ver1;//вершина 1
        public Point ver2;//вершина 2
        public Point ver3;//вершина 3
        public Point ver4;//вершина 4
        public float width;//ширина
        public float height;//высота
        public Rectangle()//конструктор по умолчанию
        {

        }

        public Rectangle(Point first, Point second, Point third, Point fourth)//конструктор с инициализацией

        {
            ver1 = first;
            ver2 = second;
            ver3 = third;
            ver4 = fourth;
        }





    }
}


