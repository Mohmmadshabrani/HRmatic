Imports System.Data.SqlClient

Public Class Employee : Inherits Users
    ' Properties
    Public Property EmployeeID As Integer
    Public Property FirstName As String
    Public Property LastName As String
    Public Property Department As String
    Public Property Salary As Decimal
    Public Property DateHired As DateTime
    Public Property DateResigned As DateTime
    Public Shared Property EmployeeRepo As EmployeeRepository = New EmployeeRepository()

    ' Constructor
    Public Sub New()
        MyBase.New()
        Me.EmployeeID = 0
        Me.FirstName = ""
        Me.LastName = ""
        Me.Department = ""
        Me.Salary = 0
        Me.DateHired = DateTime.Now
        Me.DateResigned = DateTime.Now
    End Sub
    Public Sub New(EmployeeID As Integer, FirstName As String, LastName As String,
                   Department As String, Salary As Decimal, DateHired As DateTime,
                   DateResigned As DateTime?, ID As Integer, username As String,
                   password As String, email As String, isActive As Boolean, Role As String)

        MyBase.New(ID, username, password, email, isActive, Role)
        Me.EmployeeID = EmployeeID
        Me.FirstName = FirstName
        Me.LastName = LastName
        Me.Department = Department
        Me.Salary = Salary
        Me.DateHired = DateHired
        Me.DateResigned = DateResigned
    End Sub

    ' create a new employee
    Public Overloads Shared Function Create(emp As Employee) As Boolean
        Try
            ' Insert employee into database
            If Not EmployeeRepo.Create(emp) Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ' create a new employee overrideable
    Public Overrides Function Create() As Boolean
        Return Create(Me)
    End Function

    ' read employee information
    Public Function GetEmp(ID As Integer) As Employee
        Try
            Dim emp As Employee = EmployeeRepo.GetEmp(ID)
            Return emp
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Shared Function getAllEmps() As List(Of Employee)
        Dim empsRepo As EmployeeRepository = New EmployeeRepository()
        Try
            Dim emps As List(Of Employee) = empsRepo.GetAllEmps()
            Return emps
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function searchEmps(search As String) As List(Of Employee)
        Dim empsRepo As EmployeeRepository = New EmployeeRepository()
        Try
            Dim emps As List(Of Employee) = empsRepo.SearchEmps(search)
            Return emps
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
