using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableFunctionOverAbstractMethod
{

    abstract class TableFunc
    {

        public abstract double F(double x);

        public  void Table(double a, double b, double h)
        {
            Console.WriteLine("----- X ----- Y -----");
            while (a <= b)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} |", a, F(a) /*F(a)*/);
                a += h;
            }
            Console.WriteLine("---------------------");
        }

    }

     class F1: TableFunc
    {
        public override double F(double x)
        {
            return x*x*x;
        }
    }


    public delegate double Fun(double x);//double(double)
    

    class Program
    {
        public static void Table(Fun F, double a, double b, double h)
        {
            Console.WriteLine("----- X ----- Y -----");
            while (a <= b)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} |", a, F?.Invoke(a) /*F(a)*/);
                a += h;
            }
            Console.WriteLine("---------------------");
        }

        public static double MyFunc(double x)//double(double)
        {
            return x * x * x;
        }

        public static double MyFunc2(double x)
        {
            return Math.Cos(Math.Log(x));
        }

        static void Main(string[] args)
        {
            System.IO.Stream stream;

            //TableFunc table = new TableFunc();
            //table.Table(0, 10, 1);
            F1 f1 = new F1();
            f1.Table(0, 10, 1);
            

            Console.ReadKey();
        }
    }
}
