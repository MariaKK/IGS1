using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace IGS1Consol
{

    class Program
    {
      
      
        class Game : GameWindow
        {
            public float dx = 0.003f;//приращение на которое двигается прямоугольник пооси X
            public float dy = 0.003f;//приращение на которое двигается прямоугольник по оси Y
            public static Point point1 = new Point();//первая вершина прямоугольника
            public static Point point2 = new Point();//вторая вершина прямоугольника
            public static Point point3 = new Point();//третья вершина прямоугольника
            public static Point point4 = new Point();//четвертая вершина прямоугольника
            public Rectangle rectangle = new Rectangle(point1, point2, point3, point4);//объект прмямоугольник
            public Circle circle = new Circle();//объект окружность


            static float NextFloat(Random random) //метод генерации случайного значения для успановки координат
            {
                double mantissa = (random.NextDouble() * 2.0) - 1.0;
                double exponent = Math.Pow(2.0, random.Next(-1, 1));
                return (float)(mantissa * exponent);
            }

            void Setrectangle()//метод установки прямоугольника случайного размера и расположения
            {
                Random random=new Random();
                float width = NextFloat(random);
                float height = NextFloat(random);
                point1.X = NextFloat(random);
                point1.Y = NextFloat(random);
                point2.X = point1.X+width;
                point2.Y = point1.Y;
                point3.X = point2.X;
                point3.Y = point2.Y+height;
                point4.X = point1.X;
                point4.Y = point3.Y;
                rectangle.width = width;
                rectangle.height = height;
            }
            void Setrandcircle()//метод установки круга случайного размера и расположения
            {
                Random random = new Random();
                circle.X = NextFloat(random);
                circle.Y = NextFloat(random);
                circle.Radius = NextFloat(random);

            }

            public Game()
                : base(800, 800, GraphicsMode.Default, "OpenTK Quick Start Sample")
            {
                VSync = VSyncMode.On;
            }

            protected override void OnLoad(EventArgs e)
                {
                    base.OnLoad(e);
                    GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);//установка цвета фона
                    GL.Enable(EnableCap.DepthTest);
                    Setrectangle();//установка прямоугольника
                    Setrandcircle();//установка окружности
                    if (Checkall()){ Console.WriteLine("пересеклося"); Exit(); }//если фигуры пересеклись при начальной установке на плоскости выйти
                }

            protected override void OnResize(EventArgs e)//тут отрисовка картики при изменении размеров окна
            {
                base.OnResize(e);
                GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
                Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
                GL.MatrixMode(MatrixMode.Projection);
            }

            protected override void OnUpdateFrame(FrameEventArgs e) // при обновлении кадра
            {
                base.OnUpdateFrame(e);
                
            }

      

            protected override void OnRenderFrame(FrameEventArgs e)// при рендеринге кадра
            {
                base.OnRenderFrame(e);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.MatrixMode(MatrixMode.Modelview);
                Color4 red = new Color4(1f, 0f, 0f, 0.9f);
                DrawRandRectangle(red);//рисуем сгенерированный прямоугольник соответвующего цвета
                Color4 green = new Color4(0f, 1f, 0f, 0f);
                DrawRandomCircle(green);//рисуем сгенерированную акружность соответвующего цвета
                SwapBuffers();
            }

            public void DrawRandomCircle(Color4 c)//метод отрисовки рандомной окружности выбранного цвета
            {
                GL.Begin(PrimitiveType.TriangleFan);
                GL.Color4(c);
                GL.Vertex2(circle.X, circle.Y);
                for (int i = 0; i < 360; i++)
                {
                    GL.Color4(c);
                    GL.Vertex2(circle.X + Math.Cos(i) * circle.Radius, circle.Y + Math.Sin(i) * circle.Radius);
                }
                GL.End();

            }
            public void DrawRandRectangle(Color4 c)//метод отрисовки рандомного прямоугольника выбранного цвета
            {

                GL.Begin(PrimitiveType.Quads);
                GL.Color4(c);
                GL.Vertex2(rectangle.ver1.X, rectangle.ver1.Y);
                GL.Vertex2(rectangle.ver2.X, rectangle.ver2.Y);
                GL.Vertex2(rectangle.ver3.X, rectangle.ver3.Y);
                GL.Vertex2(rectangle.ver4.X, rectangle.ver4.Y);
                GL.End();

            }


            protected override void OnKeyDown(KeyboardKeyEventArgs e)//при нажатии клавиш двигаем прямоугольник 
            {
                base.OnKeyDown(e);
                switch (e.Key)
                {
                    case Key.Down: //action
                        {
                            Move("down");
                        }
                        break;
                    case Key.Right:
                        {
                            Move("right");
                        }//action
                        break;
                    case Key.Up:
                        {
                            Move("up");
                        }
                        break;
                    case Key.Left:
                        {
                            Move("left");
                        }
                        break;
                    case Key.Escape:
                        {
                            Exit();
                        }
                        break;
                }
            }

            private void Move(string where) //метод реализации движения
            {
                switch (where)
                {
                    case "down": //action
                        {
                            Console.WriteLine("нажато вниз");
                            rectangle.ver1.Y = rectangle.ver1.Y - dy;
                            rectangle.ver2.Y = rectangle.ver2.Y - dy;
                            rectangle.ver3.Y = rectangle.ver3.Y - dy;
                            rectangle.ver4.Y = rectangle.ver4.Y - dy;
                            if (Checkall())//проверка на пересечение
                             { Console.WriteLine("пересеклося"); Exit(); } //если пересеклось выходим
                        }
                        break;
                    case "right"://аналогично
                        {
                            Console.WriteLine("нажато вправо");
                            rectangle.ver1.X = rectangle.ver1.X + dx;
                            rectangle.ver2.X = rectangle.ver2.X + dx;
                            rectangle.ver3.X = rectangle.ver3.X + dx;
                            rectangle.ver4.X = rectangle.ver4.X + dx;
                            if (Checkall())
                             { Console.WriteLine("пересеклося"); Exit();}

                        }

                        break;
                    case "up"://аналогично
                        {
                            Console.WriteLine("нажато вверх");
                            rectangle.ver1.Y = rectangle.ver1.Y + dy;
                            rectangle.ver2.Y = rectangle.ver2.Y + dy;
                            rectangle.ver3.Y = rectangle.ver3.Y + dy;
                            rectangle.ver4.Y = rectangle.ver4.Y + dy;
                            if (Checkall())
                            { Console.WriteLine("пересеклося"); Exit();}
                        }
                        break;
                    case "left"://аналогично
                        {
                            Console.WriteLine("нажато влево");
                            rectangle.ver1.X = rectangle.ver1.X - dx;
                            rectangle.ver2.X = rectangle.ver2.X - dx;
                            rectangle.ver3.X = rectangle.ver3.X - dx;
                            rectangle.ver4.X = rectangle.ver4.X - dx;
                            if (Checkall())
                            { Console.WriteLine("пересеклося"); Exit();}
                        }
                        break;
                }
            }

         
          

            public static bool Intersect(Rectangle r, Circle c)//проверка объектов на пересечение для 1 вершины прямоугольника и прил сторон
            {

                float testX = c.X;
                float testY = c.Y;

                if (testX < r.ver1.X)
                    testX = r.ver1.X;
                if (testX > (r.ver1.X + r.width))
                    testX = (r.ver1.X + r.width);

                if (testY < r.ver1.Y)
                    testY = r.ver1.Y;
                if (testY > (r.ver1.Y + r.height))
                    testY = (r.ver1.Y + r.height);

                return ((c.X - testX) * (c.X - testX) + (c.Y - testY) * (c.Y - testY)) < c.Radius * c.Radius;
            }
            public static bool Intersect2(Rectangle r, Circle c)//проверка объектов на пересечение для 2 вершины прямоугольника и прил сторон
            {

                float testX = c.X;
                float testY = c.Y;

                if (testX < r.ver2.X)
                    testX = r.ver2.X;
                if (testX > (r.ver2.X - r.width))
                    testX = (r.ver2.X - r.width);

                if (testY < r.ver2.Y)
                    testY = r.ver2.Y;
                if (testY > (r.ver2.Y + r.height))
                    testY = (r.ver2.Y + r.height);

                return ((c.X - testX) * (c.X - testX) + (c.Y - testY) * (c.Y - testY)) < c.Radius * c.Radius;
            }
            public static bool Intersect3(Rectangle r, Circle c)//проверка объектов на пересечение для 3 вершины прямоугольника и прил сторон
            {

                float testX = c.X;
                float testY = c.Y;

                if (testX < r.ver3.X)
                    testX = r.ver3.X;
                if (testX > (r.ver3.X - r.width))
                    testX = (r.ver3.X - r.width);

                if (testY < r.ver3.Y)
                    testY = r.ver3.Y;
                if (testY > (r.ver3.Y - r.height))
                    testY = (r.ver3.Y - r.height);

                return ((c.X - testX) * (c.X - testX) + (c.Y - testY) * (c.Y - testY)) < c.Radius * c.Radius;
            }
            public static bool Intersect4(Rectangle r, Circle c)//проверка объектов на пересечение для 4 вершины прямоугольника и прил сторон
            {

                float testX = c.X;
                float testY = c.Y;

                if (testX < r.ver4.X)
                    testX = r.ver4.X;
                if (testX > (r.ver4.X + r.width))
                    testX = (r.ver4.X + r.width);

                if (testY < r.ver4.Y)
                    testY = r.ver4.Y;
                if (testY > (r.ver4.Y - r.height))
                    testY = (r.ver4.Y - r.height);

                return ((c.X - testX) * (c.X - testX) + (c.Y - testY) * (c.Y - testY)) < c.Radius * c.Radius;
            }

            bool Checkall()//проверка объектов на пересечение для всех вершин
            {
                bool first = Intersect(rectangle, circle);
                bool second = Intersect2(rectangle, circle);
                bool third = Intersect3(rectangle, circle);
                bool four = Intersect4(rectangle, circle);
                return (first || second || third || four);
            }


        

            [STAThread]
            static void Main()
            {
                // The 'using' idiom guarantees proper resource cleanup.
                // We request 30 UpdateFrame events per second, and unlimited
                // RenderFrame events (as fast as the computer can handle).
                using (Game game = new Game())
                {
                    game.Run(30.0);
                
                }
            }
        }
    }
}
