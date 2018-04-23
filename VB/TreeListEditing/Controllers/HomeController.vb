Imports Microsoft.VisualBasic
Imports System
Imports System.Web.Mvc
Imports TreeListEditing.Models

Namespace TreeListEditing.Controllers
	Public Class HomeController
		Inherits Controller
		Public Function Index() As ActionResult
			Return View(EmployeesDataProvider.GetData())
		End Function

		<ValidateInput(False)> _
		Public Function TreeListPartial() As ActionResult
			Return PartialView("TreeListPartial", EmployeesDataProvider.GetData())
		End Function

		<HttpPost, ValidateInput(False)> _
		Public Function TreeListPartialAddNew(ByVal employee As Employee) As ActionResult
			If ModelState.IsValid Then
				Try
					EmployeesDataProvider.AddEmployee(employee)
				Catch e As Exception
					ViewData("EditNodeError") = e.Message
				End Try
			Else
				ViewData("EditNodeError") = "Please, correct all errors."
			End If
			Return PartialView("TreeListPartial", EmployeesDataProvider.GetData())
		End Function

		<HttpPost, ValidateInput(False)> _
		Public Function TreeListPartialUpdate(ByVal employee As Employee) As ActionResult
			If ModelState.IsValid Then
				Try
					EmployeesDataProvider.UpdateEmployee(employee)
				Catch e As Exception
					ViewData("EditNodeError") = e.Message
				End Try
			Else
				ViewData("EditNodeError") = "Please, correct all errors."
			End If
			Return PartialView("TreeListPartial", EmployeesDataProvider.GetData())
		End Function

		<HttpPost, ValidateInput(False)> _
		Public Function TreeListPartialMove(ByVal employeeID As Integer, ByVal supervisorID As Nullable(Of Integer)) As ActionResult
			EmployeesDataProvider.MoveEmployee(employeeID, supervisorID)
			Return PartialView("TreeListPartial", EmployeesDataProvider.GetData())
		End Function

		<HttpPost, ValidateInput(False)> _
		Public Function TreeListPartialDelete(ByVal employeeID As Integer) As ActionResult
			Try
				EmployeesDataProvider.DeleteEmployee(employeeID)
			Catch e As Exception
				ViewData("EditNodeError") = e.Message
			End Try
			Return PartialView("TreeListPartial", EmployeesDataProvider.GetData())
		End Function
	End Class
End Namespace