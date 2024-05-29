Imports System.Collections.ObjectModel

Public Class EmployeeDGV
    Public Sub New()
        InitializeComponent()
        LoadEmployees()
    End Sub

    Private Sub LoadEmployees()

        Dim userRepo As New EmployeeRepository()

        Dim employees As List(Of Employee) = userRepo.GetAllEmps()
        membersDataGrid.ItemsSource = New ObservableCollection(Of Employee)(employees)
    End Sub


End Class
