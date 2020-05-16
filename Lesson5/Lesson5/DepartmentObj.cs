using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    static class DepartmentObj
    {
        static Dictionary<int, string> _dep = new Dictionary<int, string>();
        public delegate void depUpdate(KeyValuePair<int, string> e);
        public static event depUpdate DepUpdate;
        internal static int Add(string TValue)
        {
            if (!_dep.ContainsValue(TValue))
            {
                _dep.Add(_dep.Count, TValue);
                DepUpdate?.Invoke(new KeyValuePair<int, string>(_dep.Count - 1, TValue));
                return _dep.Count - 1;
            } else
            {
                foreach(var select in _dep)
                {
                    if (select.Value.Equals(TValue)) return select.Key;
                }
                return 0;
            }
        }

        internal static string GetValue(int TKey)
        {
            if (_dep.ContainsKey(TKey))
            {
                return _dep[TKey];
            } else
            {
                return "Null";
            }
        }
    }
}
