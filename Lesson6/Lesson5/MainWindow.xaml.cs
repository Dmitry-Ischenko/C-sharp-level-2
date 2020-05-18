using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Lesson5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<EmployeeV2> listEmployee = new ObservableCollection<EmployeeV2>();
        ObservableCollection<string> listDepart = new ObservableCollection<string>();
        public MainWindow()
        {
            InitializeComponent();
            DepartmentClass.AddElement += DepartmentClass_AddElement;
            DepartmentClass.DeleteElement += DepartmentClass_DeleteElement;
            DataGrid.ItemsSource = listEmployee;
            ListBox.ItemsSource = listDepart;
            dataGridComboBox.ItemsSource = listDepart;             
            #region заполняем данными по умолчанию
            Random rand = new Random();
            string[] firstName =
                        {
                            "Алан","Георгий","Константин","Роман",
                            "Александр","Герман","Лев","Ростислав",
                            "Алексей","Глеб","Леонид","Руслан",
                            "Альберт","Гордей","Макар","Рустам",
                            "Анатолий","Григорий","Максим","Савва",
                            "Андрей","Давид","Марат","Савелий",
                            "Антон","Дамир","Марк","Святослав",
                            "Арсен","Даниил","Марсель","Семен",
                            "Арсений","Демид","Матвей","Сергей",
                            "Артем","Демьян","Мирон","Станислав",
                            "Артемий","Денис","Мирослав","Степан",
                            "Артур","Дмитрий","Михаил","Тамерлан",
                            "Богдан","Евгений","Назар","Тимофей",
                            "Борис","Егор","Никита","Тимур",
                            "Вадим","Елисей","Николай","Тихон",
                            "Валентин","Захар","Олег","Федор",
                            "Валерий","Иван","Павел","Филипп",
                            "Василий","Игнат","Петр","Шамиль",
                            "Виктор","Игорь","Платон","Эдуард",
                            "Виталий","Илья","Прохор","Эльдар",
                            "Владимир","Ильяс","Рамиль","Эмиль",
                            "Владислав","Камиль","Ратмир","Эрик",
                            "Всеволод","Карим","Ринат","Юрий",
                            "Вячеслав","Кирилл","Роберт","Ян",
                            "Геннадий","Клим","Родион","Ярослав"
                        };
            string[] lastName = {
                "Смирнов","Орехов","Денисов","Белоусов","Авдеев","Лазарев","Горшков","Кузьмин",
                "Иванов","Ефремов","Громов","Федотов","Зыков","Медведев","Чернов","Кудрявцев",
                "Кузнецов","Исаев","Фомин","Дорофеев","Бирюков","Ершов","Овчинников","Баранов",
                "Соколов","Евдокимов","Давыдов","Егоров","Шарапов","Никитин","Селезнёв","Куликов",
                "Попов","Калашников","Мельников","Матвеев","Никонов","Соболев","Панфилов","Алексеев",
                "Лебедев","Кабанов","Щербаков","Бобров","Щукин","Рябов","Копылов","Степанов",
                "Козлов","Носков","Блинов","Дмитриев","Дьячков","Поляков","Михеев","Яковлев",
                "Новиков","Юдин","Колесников","Калинин","Одинцов","Цветков","Галкин","Сорокин",
                "Морозов","Кулагин","Карпов","Анисимов","Сазонов","Данилов","Назаров","Сергеев",
                "Петров","Лапин","Афанасьев","Петухов","Якушев","Жуков","Лобанов","Романов",
                "Волков","Прохоров","Власов","Антонов","Красильников","Фролов","Лукин","Захаров",
                "Соловьёв","Нестеров","Маслов","Тимофеев","Гордеев","Журавлёв","Беляков","Борисов",
                "Васильев","Харитонов","Исаков","Никифоров","Самойлов","Николаев","Потапов","Королёв",
                "Зайцев","Агафонов","Тихонов","Веселов","Князев","Крылов","Некрасов","Герасимов",
                "Павлов","Муравьёв","Аксёнов","Филиппов","Беспалов","Максимов","Хохлов","Пономарёв",
                "Семёнов","Ларионов","Гаврилов","Марков","Уваров","Сидоров","Жданов","Григорьев",
                "Голубев","Федосеев","Родионов","Большаков","Шашков","Осипов","Ситников",
                "Виноградов","Зимин","Котов","Суханов","Наумов","Сысоев","Симонов",
                "Богданов","Пахомов","Горбунов","Миронов","Шилов","Фомичёв","Мишин",
                "Воробьёв","Шубин","Кудряшов","Ширяев","Воронцов","Русаков","Фадеев",
                "Фёдоров","Игнатов","Быков","Александров","Ермаков","Стрелков","Комиссаров",
                "Михайлов","Филатов","Зуев","Коновалов","Дроздов","Гущин","Мамонтов",
                "Беляев","Крюков","Третьяков","Шестаков","Игнатьев","Тетерин","Носов",
                "Тарасов","Рогов","Савельев","Казаков","Савин","Колобов","Гуляев",
                "Белов","Кулаков","Панов","Ефимов","Логинов","Субботин","Шаров",
                "Комаров","Терентьев","Рыбаков","Бобылёв","Сафонов","Фокин","Устинов",
                "Орлов","Молчанов","Суворов","Доронин","Капустин","Блохин","Вишняков",
                "Киселёв","Владимиров","Абрамов","Белозёров","Кириллов","Селиверстов","Евсеев",
                "Макаров","Артемьев","Воронов","Рожков","Моисеев","Пестов","Лаврентьев",
                "Андреев","Гурьев","Мухин","Самсонов","Елисеев","Кондратьев","Брагин",
                "Ковалёв","Зиновьев","Архипов","Мясников","Кошелев","Силин","Константинов",
                "Ильин","Гришин","Трофимов","Лихачёв","Костин","Меркушев","Корнилов",
                "Гусев","Кононов","Мартынов","Буров","Горбачёв","Лыткин",
                "Титов","Дементьев","Емельянов","Туров"
            };
            string[] deplist = 
                {
                "Департамент Зверюшек",
                "Департамент Гикбрейнс",
                "Департамент Индусов",
                "Департамент Болезний",
                "Департамент Программистов",
                "Курлык"
            };
            int countItem = rand.Next(10, 100);
            for (int i = 0; i < countItem; i++)
            {
                listEmployee.Add(
                    new EmployeeV2 (
                        firstName[rand.Next(0,firstName.Length)],
                        lastName[rand.Next(0,lastName.Length)],
                        //(int year, int month, int day);
                        new DateTime(
                            rand.Next(1970,2010),
                            rand.Next(1,13),
                            rand.Next(01,29)
                        ),
                        deplist[rand.Next(0,deplist.Length)]
                        )
                    );
            }
            #endregion

        }

        private void DepartmentClass_DeleteElement(string e)
        {
            listDepart.Remove(e);
        }

        private void DepartmentClass_AddElement(string e)
        {
            listDepart.Add(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox.Text.Length >0)
            {
                DepartmentClass.Add(TextBox.Text);
                TextBox.Clear();
            }
        }

        private void Button_delete_click(object sender, RoutedEventArgs e)
        {
            if (ListBox.SelectedValue != null)
            {
                DepartmentClass.Delete(ListBox.SelectedValue.ToString());
            }
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedValue is EmployeeV2 z)
            {
                listEmployee.Remove(z);
            }
        }

        private void SaveFileDialog(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "myDateBase";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "xml documents (.xml)|*.xml";
            if (dlg.ShowDialog() == true)
            {
                string filename = dlg.FileName;
                XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<EmployeeV2>));
                try
                {
                    using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                    {
                        formatter.Serialize(fs, listEmployee);

                        Console.WriteLine("Объект сериализован");
                    }
                }
                catch { }
            }
        }

        private void OpenFileDialog(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "myDateBase";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "xml documents (.xml)|*.xml";
            if (dlg.ShowDialog() == true)
            {
                string filename = dlg.FileName;
                try
                {
                    using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                    {
                        XmlSerializer formatter = new XmlSerializer(typeof(EmployeeV2[]));
                        listEmployee.Clear();
                        listDepart.Clear();
                        DepartmentClass.Clear();
                        foreach (var emp in (EmployeeV2[])formatter.Deserialize(fs))
                        {
                            listEmployee.Add(emp);
                        }
                    }
                }
                catch { }

            }
        }
    }
}
