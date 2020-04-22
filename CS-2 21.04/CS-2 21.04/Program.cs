using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_2_21._04
{

    public struct Vector
    {
        //инкапсулируем поля
        private double x, y;

        ////конструктор структуры
        public Vector(double _x, double y)
        {
            x = _x;
            this.y = y;
        }

        public Vector(string _x, string y)
        {
            x = Convert.ToDouble(_x);
            this.y = Convert.ToDouble(y);
        }

        //public Vector()
        //{
        //    x = y = 0;
        //}

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
                x = Convert.ToDouble(value);
            }
        }

    }

    class Program
    {


        static void Main(string[] args)
        {
            Vector pos=new Vector(), dir=new Vector();
            pos = new Vector(10, 0);
            dir = new Vector(0, 0);
           pos.SetX(5);
            pos.X = "5";
            //double x = pos.GetX();
            string x = pos.X;
            dir.SetX(5);

        }
    }
}
