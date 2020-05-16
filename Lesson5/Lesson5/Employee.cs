using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        private int _department;
        public Employee()
        {
            FirstName = "";
            LastName = "";
            Birthday = DateTime.Now;
            _department = 0;
        }
        public Employee(string FirstName,string LastName, DateTime Birthday, string department)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Birthday = Birthday;
            _department = DepartmentObj.Add(department);
        }
        public string Department
        {
            get
            {
                return DepartmentObj.GetValue(_department);
            }
            set
            {
                _department = DepartmentObj.Add(value);
            }
        }
    }
}
