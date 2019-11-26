using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmployeeService" in both code and config file together.
    
      [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        Employee GetEmployee(int id);

        [OperationContract]
        void EditEmployee(Employee e);
    }

        [DataContract]
        public class Employee
        {
            [DataMember]
            public int EmployeeKey { get; set; }
            [DataMember]
            public int PersonKey { get; set; }
            [DataMember]
            public string LastName { get; set; }
            [DataMember]
            public string FirstName { get; set; }
            [DataMember]
            public string SocialSecurityNumber { get; set; }
            [DataMember]
            public DateTime HireDate { get; set; }
            [DataMember]
            public string Status { get; set; }
            [DataMember]
            public decimal MonthlySalary { get; set; }
        }
    }



