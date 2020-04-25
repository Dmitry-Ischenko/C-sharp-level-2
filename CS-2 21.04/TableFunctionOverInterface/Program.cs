using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableFunctionOverInterface
{
    interface IMathFunc
    {
        double F(double X);
    }


    class TableFunc
    {

        public void Table(double a, double b, double h, IMathFunc func)
        {
            double x = a;
            while (x <= b)
            {
                Console.WriteLine("{0,10}{1,10}", x, func.F(x));
                x += h;
            }
        }

    }


    class F1 : IMathFunc
    {
        public double F(double X)
        {
            return X * X;
        }
    }

    class F2 : IMathFunc
    {
        public double F(double X)
        {
            return X * X * X;
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            TableFunc table = new TableFunc();
            F2 f2 = new F2();
            table.Table(1, 5, 1, f2);
        }
    }
}
