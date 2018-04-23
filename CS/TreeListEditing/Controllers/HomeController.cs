using System;
using System.Web.Mvc;
using TreeListEditing.Models;

namespace TreeListEditing.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View(EmployeesDataProvider.GetData());
        }

        [ValidateInput(false)]
        public ActionResult TreeListPartial() {
            return PartialView("TreeListPartial", EmployeesDataProvider.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TreeListPartialAddNew(Employee employee) {
            if(ModelState.IsValid) {
                try {
                    EmployeesDataProvider.AddEmployee(employee);
                }
                catch(Exception e) {
                    ViewData["EditNodeError"] = e.Message;
                }
            }
            else
                ViewData["EditNodeError"] = "Please, correct all errors.";
            return PartialView("TreeListPartial", EmployeesDataProvider.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TreeListPartialUpdate(Employee employee) {
            if(ModelState.IsValid) {
                try {
                    EmployeesDataProvider.UpdateEmployee(employee);
                }
                catch(Exception e) {
                    ViewData["EditNodeError"] = e.Message;
                }
            }
            else
                ViewData["EditNodeError"] = "Please, correct all errors.";
            return PartialView("TreeListPartial", EmployeesDataProvider.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TreeListPartialMove(int employeeID, int? supervisorID) {
            EmployeesDataProvider.MoveEmployee(employeeID, supervisorID);
            return PartialView("TreeListPartial", EmployeesDataProvider.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TreeListPartialDelete(int employeeID) {
            try {
                EmployeesDataProvider.DeleteEmployee(employeeID);
            }
            catch(Exception e) {
                ViewData["EditNodeError"] = e.Message;
            }
            return PartialView("TreeListPartial", EmployeesDataProvider.GetData());
        }
    }
}