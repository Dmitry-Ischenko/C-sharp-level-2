using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Выполнил Ищенко Дмитрий
            //Задача 1: https://docs.google.com/document/d/1HhA-e_m96nONNjBpHaIZHtDiSX2jRLKxvvRtaTPgzAk/edit#heading=h.1mrcu09
            //Построить три  класса(базовый  и  2  потомка),  описывающих работников  с почасовой  оплатой(один  из  потомков)  
            //  и фиксированной оплатой(второй потомок):
            //  а) Описать в базовом классе абстрактный метод для расчета среднемесячной заработной платы.
            //    Для «повременщиков» формула для расчета такова: «среднемесячная заработная плата = 20.8 * 8 * почасовая ставка»;
            //      для работников  с фиксированной  оплатой: «среднемесячная заработная плата = фиксированная месячная оплата»;
            //  b) Создать на базе абстрактного класса массив сотрудников и заполнить его;
            //  c) *Реализовать интерфейсы для возможности сортировки массива, используя Array.Sort();
            //  d) *Создать класс, содержащий массив сотрудников, и реализовать возможность вывода данных с использованием foreach.
            WorkersBase[] workersArrey = { new WorkersHR(200), new WorkersHR(400), new WorkersFP(30000),new WorkersHR(500), new WorkersFP(120000) };
            foreach (WorkersBase get in workersArrey)
                Console.WriteLine(get.Pay);
            Array.Sort(workersArrey);
            Console.WriteLine("-------------");
            foreach (WorkersBase get in workersArrey)
                Console.WriteLine(get.Pay);
            Console.WriteLine("-------------");
            ArrayWorkers workersArreyClass = new ArrayWorkers(10);
            foreach (WorkersBase serch in workersArreyClass)
            Console.ReadKey();
        }
    }
    public class ArrayWorkers: IEnumerator
    {
        WorkersBase[] workersArrey;
        int position = -1;
        public ArrayWorkers (int count)
        {
            workersArrey = new WorkersBase[count];
        }
        public WorkersBase this[int index]
        {
            get
            {
                return workersArrey[index];
            }
            set
            {
                workersArrey[index] = value;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public WorkersBase Current
        {
            get
            {
                try
                {
                    return workersArrey[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            position++;
            return (position< workersArrey.Length);
        }

        public void Reset()
        {
            position = -1;
        }
    }
    public abstract class WorkersBase : IComparable, IEnumerable
    {
        public double Pay { get; protected set; }
        public WorkersBase(double _hourlyRate)
        {
            Pay = _hourlyRate;
        }
        public abstract double Payroll();

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            WorkersBase otherWorkersBase = obj as WorkersBase;
            if (otherWorkersBase != null)
                return this.Pay.CompareTo(otherWorkersBase.Pay);
            else
                throw new ArgumentException("Object is not a WorkersBase");
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
    public class WorkersHR : WorkersBase
    {
        public WorkersHR(double _hourlyRate) : base(_hourlyRate)
        {

        }
        public override double Payroll()
        {
            return 20.8 * 8 * Pay;
        }
    }
    public class WorkersFP : WorkersBase
    {
        public WorkersFP(double _hourlyRate) : base(_hourlyRate)
        {

        }
        public override double Payroll()
        {
            return Pay;
        }
    }
}
