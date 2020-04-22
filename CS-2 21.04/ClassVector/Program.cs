//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvanovCo
{
    namespace ClassVector
    {
        public class Vector
        {
            //инкапсулируем поля
            private double x, y;

            ////конструктор структуры
            public Vector(double _x, double y)
            {
                x = _x;
                this.y = y;
            }

            public Vector()
            {
                x = y = 0;
            }

            //методы
            public void SetX(double value)
            {
                x = value;
            }

            public double GetX()
            {
                return x;
            }

            //свойства
            public string X
            {
                //акцессоры доступа
                get
                {
                    return x.ToString();
                }
                set
                {
                    x = System.Convert.ToDouble(value);
                }
            }

        }

        public class Vector3 : Vector
        {
            double z;

            public void SetZ(double z)
            {
                this.z = z;
            }


        }


        class Program
        {


            static void Main(string[] args)
            {


                IvanovCo.ClassVector.Vector3 vector3 = new ClassVector.Vector3();

                vector3.X = "10";
                vector3.SetX(20);
                vector3.SetZ(30);

                Vector pos = new Vector(), dir = new Vector();
                pos = new Vector(10, 0);
                dir = new Vector(0, 0);
                pos.SetX(5);
                pos.X = "5";
                pos = dir;
                //double x = pos.GetX();
                string x = pos.X;
                dir.SetX(5);

            }
        }
    }

}