using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneSample
{
    class MyClass
    {
        int[] a;
    }

    class MyClass2: ICloneable
    {
        MyClass a1;

        public object Clone()
        {
            MyClass2 myClone = new MyClass2();
            //Clone object
            return myClone;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyClass2 myClass2 = new MyClass2();            
        }
    }
}
