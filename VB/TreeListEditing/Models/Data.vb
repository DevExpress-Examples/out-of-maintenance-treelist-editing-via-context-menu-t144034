Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Web

Namespace TreeListEditing.Models
	Public Class Employee
		Public Sub New()
		End Sub
		Public Sub New(ByVal employeeID As Integer, ByVal supervisorID As Nullable(Of Integer), ByVal firstName As String, ByVal middleName As String, ByVal lastName As String, ByVal title As String)
            Me.EmployeeID = employeeID
            Me.SupervisorID = supervisorID
            Me.FirstName = firstName
            Me.MiddleName = middleName
            Me.LastName = lastName
            Me.Title = title
		End Sub

		Private privateEmployeeID As Integer
		<Key> _
		Public Property EmployeeID() As Integer
			Get
				Return privateEmployeeID
			End Get
			Set(ByVal value As Integer)
				privateEmployeeID = value
			End Set
		End Property

		Private privateSupervisorID As Nullable(Of Integer)
		Public Property SupervisorID() As Nullable(Of Integer)
			Get
				Return privateSupervisorID
			End Get
			Set(ByVal value As Nullable(Of Integer))
				privateSupervisorID = value
			End Set
		End Property

		Private privateFirstName As String
		<Required(ErrorMessage := "First Name is required")> _
		Public Property FirstName() As String
			Get
				Return privateFirstName
			End Get
			Set(ByVal value As String)
				privateFirstName = value
			End Set
		End Property

		Private privateMiddleName As String
		Public Property MiddleName() As String
			Get
				Return privateMiddleName
			End Get
			Set(ByVal value As String)
				privateMiddleName = value
			End Set
		End Property

		Private privateLastName As String
		<Required(ErrorMessage := "Last Name is required")> _
		Public Property LastName() As String
			Get
				Return privateLastName
			End Get
			Set(ByVal value As String)
				privateLastName = value
			End Set
		End Property

		Private privateTitle As String
		<Required(ErrorMessage := "Title is required")> _
		Public Property Title() As String
			Get
				Return privateTitle
			End Get
			Set(ByVal value As String)
				privateTitle = value
			End Set
		End Property
	End Class

	Public NotInheritable Class EmployeesDataProvider
		Private Sub New()
		End Sub
		Public Shared Function GetData() As List(Of Employee)
			If HttpContext.Current.Session("Employees") Is Nothing Then
				Dim list As List(Of Employee) = New List(Of Employee)()

				list.Add(New Employee(1, Nothing, "David", "Jordan", "Adler", "Vice President"))
				list.Add(New Employee(2, 1, "Michael", "Christopher", "Alcamo", "Associate Vice President"))
				list.Add(New Employee(3, 1, "Eric", "Zachary", "Berkowitz", "Associate Vice President"))
				list.Add(New Employee(4, 2, "Amy", "Gabrielle", "Altmann", "Business Manager"))
				list.Add(New Employee(5, 3, "Kyle", "", "Bernardo", "Acting Director"))
				list.Add(New Employee(6, 2, "Mark", "Sydney", "Atlas", "Executive Director"))
				list.Add(New Employee(7, 3, "Meredith", "", "Berman", "Manager"))
				list.Add(New Employee(8, 3, "Liz", "", "Bice", "Controller"))

				HttpContext.Current.Session("Employees") = list
			End If
			Return CType(HttpContext.Current.Session("Employees"), List(Of Employee))
		End Function

		Public Shared Sub AddEmployee(ByVal newEmployee As Employee)
			Dim list As List(Of Employee) = GetData()
			newEmployee.EmployeeID = list.Count + 1
			list.Add(newEmployee)
		End Sub
		Public Shared Sub UpdateEmployee(ByVal employeeInfo As Employee)
			Dim employee As Employee = GetEmployeeByID(employeeInfo.EmployeeID)
			employee.FirstName = employeeInfo.FirstName
			employee.MiddleName = employeeInfo.MiddleName
			employee.LastName = employeeInfo.LastName
			employee.Title = employeeInfo.Title
		End Sub
		Public Shared Sub MoveEmployee(ByVal employeeID As Integer, ByVal newSupervisorID As Nullable(Of Integer))
			Dim newParentID As Integer = Convert.ToInt32(newSupervisorID)
			Dim employee As Employee = GetEmployeeByID(employeeID)
			If employee.SupervisorID = newParentID OrElse IsParent(employeeID, newParentID) Then
				Return
			End If
			employee.SupervisorID = newParentID
		End Sub
		Public Shared Sub DeleteEmployee(ByVal employeeID As Integer)
			Dim employee As Employee = GetEmployeeByID(employeeID)
			DeleteEmployees(employee)
		End Sub

		Private Shared Function GetEmployeeByID(ByVal employeeID As Integer) As Employee
			Return GetData().Where(Function(e) e.EmployeeID = employeeID).First()
		End Function
		Private Shared Function IsParent(ByVal parentID As Integer, ByVal childID As Integer) As Boolean
			Dim employee As Employee
			Dim employeeID As Integer = childID
			Do While employeeID <> 0
				employee = GetEmployeeByID(employeeID)
				If employee.EmployeeID = parentID Then
					Return True
				End If
				employeeID = CInt(Fix(If(employee.SupervisorID.HasValue, employee.SupervisorID, 0)))
			Loop
			Return False
		End Function
        Private Shared Sub DeleteEmployees(ByVal employee As Employee)
            Dim childrenWithParents As List(Of Employee) = GetData().Where(Function(e) e.SupervisorID.HasValue).ToList()
            Dim children As List(Of Employee) = childrenWithParents.Where(Function(e) e.SupervisorID.Value = employee.EmployeeID).ToList()
            If children IsNot Nothing Then
                For Each child As Employee In children
                    DeleteEmployees(child)
                Next child
            End If
            GetData().Remove(employee)
        End Sub
	End Class
End Namespace