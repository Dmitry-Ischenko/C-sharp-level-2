using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    static class DepartmentClass
    {
        static private List<string> _coll = new List<string>();
        public delegate void Update(string e);
        static public event Update AddElement;
        static public event Update DeleteElement;
        static public void Add(string element)
        {
            if (_coll.Count >0)
            {
                if (!_coll.Contains(element))
                {
                    _coll.Add(element);
                    AddElement?.Invoke(element);
                } 
            } else { 
                _coll.Add(element);
                AddElement?.Invoke(element);
            }
        }
        static public void Delete(string element)
        {
            Console.WriteLine("step1");
            if (_coll.Count>0)
            {
                Console.WriteLine("step2");
                Console.WriteLine(_coll.Contains(element));
                if (_coll.Contains(element))
                {
                    _coll.Remove(element);
                    Console.WriteLine("step3");
                    DeleteElement?.Invoke(element);
                }
            }
        }
        static public void Clear()
        {
            _coll.Clear();
        }
    }
}
