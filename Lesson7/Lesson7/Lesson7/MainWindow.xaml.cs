using System;
using System.Collections.Generic;
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
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Lesson7
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection;
        SqlDataAdapter adapterEmployee;
        SqlDataAdapter adapterDepartment;
        DataTable dtEmployee;
        DataTable dtDepartment;
        ObservableCollection<string> listDepart = new ObservableCollection<string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitDB(object sender, RoutedEventArgs e)
        {

        }

        private void ClearDB(object sender, RoutedEventArgs e)
        {

        }

        private void InitWindow(object sender, RoutedEventArgs e)
        {
            string connectionString =
                @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Database1.mdf;Integrated Security=True";
            connection = new SqlConnection(connectionString);
            adapterEmployee = new SqlDataAdapter();
            adapterDepartment = new SqlDataAdapter();
            //          select Employee.Id,FirstName,LastName,Birthday,Department = Department.Name from Employee
            //INNER JOIN Department ON Employee.id = Department.id;
            SqlCommand command = new SqlCommand(
                "select Employee.Id,FirstName,LastName,Birthday,Department = Department.Name " +
                "from Employee INNER JOIN Department ON Employee.id = Department.id",
                connection);
            adapterEmployee.SelectCommand = command;
            
            dtEmployee = new DataTable();
            adapterEmployee.Fill(dtEmployee);
            DataGrid.DataContext = dtEmployee.DefaultView;
            //insert
            command = new SqlCommand(@"EXEC InsertEmployee @FirstName, @LastName, @Birthday, @Department; SET @Id = @@IDENTITY;", connection);
            command.Parameters.Add("@FirstName",SqlDbType.NVarChar,50, "FirstName");
            command.Parameters.Add("@LastName", SqlDbType.NVarChar,50, "LastName");
            command.Parameters.Add("@Birthday", SqlDbType.Date, 50, "Birthday");
            command.Parameters.Add("@Department", SqlDbType.NVarChar,50, "Department");
            SqlParameter param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.Direction = ParameterDirection.Output;
            adapterEmployee.InsertCommand = command;
            //update
            command = new SqlCommand(@"EXEC UpdateEmployee @Id, @FirstName, @LastName, @Birthday, @Department", connection);
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50, "FirstName");
            command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50, "LastName");
            command.Parameters.Add("@Birthday", SqlDbType.Date, 50, "Birthday");
            command.Parameters.Add("@Department", SqlDbType.NVarChar, 50, "Department");
            param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            adapterEmployee.UpdateCommand = command;
            //          select* from Department
            command = new SqlCommand("select * from Department", connection);
            adapterDepartment.SelectCommand = command;
            //insert Department table
            //command = new SqlCommand
            dtDepartment = new DataTable();
            adapterDepartment.Fill(dtDepartment);
            DataDepartment.DataContext = dtDepartment;
            foreach (DataRow row in dtDepartment.Rows)
            {
                listDepart.Add(row[dtDepartment.Columns[1]].ToString());
                Console.WriteLine(row[dtDepartment.Columns[1]]);
            }
            dataGridComboBox.ItemsSource = listDepart;
            dtEmployee.RowChanged += DtEmployee_RowChanged;
            dtEmployee.ColumnChanged += DtEmployee_ColumnChanged;
            //dtEmployee.
            //EXEC InsertEmployee N'Генадий',N'Ушаков',N'2008-03-28',N'Орешков';
        }

        private void DtEmployee_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            //try
            //{
            //    adapterEmployee.Update(dtEmployee);
            //}
            //catch (Exception z)
            //{
            //    Console.WriteLine(z.ToString());
            //}

        }

        private void DtEmployee_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                adapterEmployee.Update(dtEmployee);
            }
            catch (Exception z)
            {
                Console.WriteLine(z.ToString());
            }
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {

        }

        private void OnDeleteDataDepartment(object sender, RoutedEventArgs e)
        {

        }
    }
}
