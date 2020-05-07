using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDisposeSample
{
    class MyClass: IDisposable
    {
        int[] a;
        List<int> b;

        public void Dispose()
        {
            //освобождать выделенные нам ресурсы
        }

        void Danger()
        {
            unsafe
            {

            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyClass my = new MyClass();
            my = null;//GarbageCollector
            my.Dispose();

        }
    }
}
