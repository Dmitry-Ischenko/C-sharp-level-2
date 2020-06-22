using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebAPIService.App_Start
{

    public static class InitDB
    {
        private static SqlDataAdapter adapterEmployee;
        private static SqlDataAdapter adapterDepartment;
        public static DataTable dtEmployee { get; set; }
        private static DataTable dtDepartment { get; set; }
        private static SqlConnection connection;

        public static void Load()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Database1.mdf;Integrated Security=True";
            connection = new SqlConnection(connectionString);
            adapterEmployee = new SqlDataAdapter();
            adapterDepartment = new SqlDataAdapter();
            #region adapterEmployee
            SqlCommand command = new SqlCommand(
                "select Employee.Id,FirstName,LastName,Birthday,Department = Department.Name " +
                "from Employee INNER JOIN Department ON Department.id=Department_id",
                connection);
            adapterEmployee.SelectCommand = command;
            dtEmployee = new DataTable();
            adapterEmployee.Fill(dtEmployee);
            //insert
            command = new SqlCommand(@"EXEC InsertEmployee @FirstName, @LastName, @Birthday, @Department; SET @Id = @@IDENTITY;", connection);
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50, "FirstName");
            command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50, "LastName");
            command.Parameters.Add("@Birthday", SqlDbType.Date, 50, "Birthday");
            command.Parameters.Add("@Department", SqlDbType.NVarChar, 50, "Department");
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
            // delete
            command = new SqlCommand("DELETE FROM Employee WHERE Id = @Id", connection);
            param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            adapterEmployee.DeleteCommand = command;
            #endregion
        }
    }
}