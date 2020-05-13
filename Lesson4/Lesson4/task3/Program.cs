using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Выполнил Ищенко Дмитрий
            //3. * Дан фрагмент программы:
            //а) Свернуть обращение к OrderBy с использованием лямбда - выражения.
            //б) *Развернуть обращение к OrderBy с использованием делегата Func<KeyValuePair<string,int>, int>

            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"four",4 },
                {"two",2 },
                { "one",1 },
                {"three",3 },
            };
            var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
            Console.WriteLine("----------------\na):");
            //a)
            var a = dict.OrderBy(pair=>pair.Value);
            foreach (var pair in a)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
            Console.WriteLine("----------------\nb):");
            //b)
            //var b = dict.OrderBy(Func(pair) pair.Value);
            Console.ReadKey();
        }
    }
}
