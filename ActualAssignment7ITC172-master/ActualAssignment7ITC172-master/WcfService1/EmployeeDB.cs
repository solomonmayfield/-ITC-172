using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace WcfService1
{
    public class EmployeeDB
    {
        private SqlConnection connect;

        public EmployeeDB()
        {
            connect = new SqlConnection
                ("Data source=localhost;initial catalog=communityAssist;integrated security=true");
        }

        public Employee GetEmployeeById(int id)
        {
            string sql = "Select PersonLastName, PersonFirstName, EmployeeHireDate, "
                + "EmployeeSSNumber, p.PersonKey, EmployeeStatus, EmployeeMonthlySalary "
                + "From Person p "
                + "Inner Join Employee e "
                + "On p.PersonKey=e.PersonKey "
                + "where EmployeeKey=@key";
            SqlCommand cmd = new SqlCommand(sql, connect);
            cmd.Parameters.AddWithValue("@key", id);
            Employee emp = new Employee();
            SqlDataReader reader = null;
          
            connect.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                emp.EmployeeKey = id;
                emp.PersonKey = int.Parse(reader["PersonKey"].ToString());
                emp.FirstName = reader["PersonFirstName"].ToString();
                emp.SocialSecurityNumber = reader["EmployeeSSNumber"].ToString();
                emp.LastName = reader["PersonLastName"].ToString();
                emp.HireDate = (DateTime)reader["EmployeeHireDate"];
                emp.Status = reader["EmployeeStatus"].ToString();
                emp.MonthlySalary = decimal.Parse(reader["EmployeeMonthlySalary"].ToString());


            }
            reader.Close();
            connect.Close();
            return emp;
        }

        public void EditEmployee(Employee emp)
        {
            string personSql = "Update Person "
                + "Set PersonLastName= @lastName, "
                + "PersonFirstName=@FirstName "
                +" Where personkey = @person";
            SqlCommand personCmd = new SqlCommand(personSql, connect);
            personCmd.Parameters.AddWithValue("@LastName", emp.LastName);
            personCmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
            personCmd.Parameters.AddWithValue("@person", emp.PersonKey);

            string employeeSql = "Update Employee "
                + "Set EmployeeHireDate=@hiredate, "
                + "EmployeeSSNumber=@ss "
                + "EmployeeMonthlySalary = @salary "
                + "EmployeeStatus=@status "
                + "Where EmployeeKey = @key";
            SqlCommand employeeCmd = new SqlCommand(employeeSql, connect);
            employeeCmd.Parameters.AddWithValue("@hiredate", emp.HireDate);
            employeeCmd.Parameters.AddWithValue("@ss", emp.SocialSecurityNumber);
            employeeCmd.Parameters.AddWithValue("@salary", emp.MonthlySalary);
            employeeCmd.Parameters.AddWithValue("@status", emp.Status);

            SqlTransaction tran;
            connect.Open();
            tran = connect.BeginTransaction();
            personCmd.Transaction = tran;
            employeeCmd.Transaction = tran;
            try
            {
                personCmd.ExecuteNonQuery();
                employeeCmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch 
            {
                tran.Rollback();
            }
            connect.Close();

            
        }
    }
}