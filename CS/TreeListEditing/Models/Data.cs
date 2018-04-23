using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TreeListEditing.Models {
    public class Employee {
        public Employee() {
        }
        public Employee(int employeeID, int? supervisorID, string firstName, string middleName, string lastName, string title) {
            EmployeeID = employeeID;
            SupervisorID = supervisorID;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Title = title;
        }

        [Key]
        public int EmployeeID { get; set; }

        public int? SupervisorID { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
    }

    public static class EmployeesDataProvider {
        public static List<Employee> GetData() {
            if(HttpContext.Current.Session["Employees"] == null) {
                List<Employee> list = new List<Employee>();
                
                list.Add(new Employee(1, null, "David", "Jordan", "Adler", "Vice President"));
                list.Add(new Employee(2, 1, "Michael", "Christopher", "Alcamo", "Associate Vice President"));
                list.Add(new Employee(3, 1, "Eric", "Zachary", "Berkowitz", "Associate Vice President"));
                list.Add(new Employee(4, 2, "Amy", "Gabrielle", "Altmann", "Business Manager"));
                list.Add(new Employee(5, 3, "Kyle", "", "Bernardo", "Acting Director"));
                list.Add(new Employee(6, 2, "Mark", "Sydney", "Atlas", "Executive Director"));
                list.Add(new Employee(7, 3, "Meredith", "", "Berman", "Manager"));
                list.Add(new Employee(8, 3, "Liz", "", "Bice", "Controller"));

                HttpContext.Current.Session["Employees"] = list;
            }
            return (List<Employee>)HttpContext.Current.Session["Employees"];
        }

        public static void AddEmployee(Employee newEmployee) {
            List<Employee> list = GetData();
            newEmployee.EmployeeID = list.Count + 1;
            list.Add(newEmployee);
        }
        public static void UpdateEmployee(Employee employeeInfo) {
            Employee employee = GetEmployeeByID(employeeInfo.EmployeeID);
            employee.FirstName = employeeInfo.FirstName;
            employee.MiddleName = employeeInfo.MiddleName;
            employee.LastName = employeeInfo.LastName;
            employee.Title = employeeInfo.Title;
        }
        public static void MoveEmployee(int employeeID, int? newSupervisorID) {
            int newParentID = Convert.ToInt32(newSupervisorID);
            Employee employee = GetEmployeeByID(employeeID);
            if(employee.SupervisorID == newParentID || IsParent(employeeID, newParentID))
                return;
            employee.SupervisorID = newParentID;
        }
        public static void DeleteEmployee(int employeeID) {
            Employee employee = GetEmployeeByID(employeeID);
            DeleteEmployees(employee);
        }

        static Employee GetEmployeeByID(int employeeID) {
            return GetData().Where(e => e.EmployeeID == employeeID).First();
        }
        static bool IsParent(int parentID, int childID) {
            Employee employee;
            int employeeID = childID;
            while(employeeID != 0) {
                employee = GetEmployeeByID(employeeID);
                if(employee.EmployeeID == parentID)
                    return true;
                employeeID = (int)(employee.SupervisorID ?? 0);
            }
            return false;
        }
        static void DeleteEmployees(Employee employee) {
            List<Employee> children = GetData().Where(e => e.SupervisorID == employee.EmployeeID).ToList();
            if(children != null) {
                foreach(Employee child in children) {
                    DeleteEmployees(child);
                }
            }
            GetData().Remove(employee);
        }
    }
}