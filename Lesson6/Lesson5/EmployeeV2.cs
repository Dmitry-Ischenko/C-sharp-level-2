using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    class EmployeeV2
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        private string _department;


        public EmployeeV2()
        {
            FirstName = "";
            LastName = "";
            Birthday = DateTime.Now;
            _department = "";
            DepartmentClass.DeleteElement += deleteDep;
        }

        private void deleteDep(string e)
        {
            if (e == _department) _department = "";
        }

        public EmployeeV2(string FirstName, string LastName, DateTime Birthday, string department): this ()
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Birthday = Birthday;
            _department = department;
            DepartmentClass.Add(department);
        }
        public string Department
        {
            get
            {
                return _department;
            }
            set
            {
                _department = value;
                DepartmentClass.Add(value);
            }
        }
    }
}
