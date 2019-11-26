using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmployeeService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EmployeeService.svc or EmployeeService.svc.cs at the Solution Explorer and start debugging.
    public class EmployeeService : IEmployeeService
    {
        EmployeeDB db = new EmployeeDB();

        public Employee GetEmployee(int id)
        {
            Employee e = db.GetEmployeeById(id);
            return e;
        }

        public void EditEmployee(Employee e)
        {
            db.EditEmployee(e);
        }
    }
}
