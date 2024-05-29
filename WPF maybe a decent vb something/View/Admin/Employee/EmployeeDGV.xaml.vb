Imports Mysqlx.XDevAPI.Common

Public Class EmployeeDGV
    Private _contentGrid As Grid

    Public Sub New()
        InitializeComponent()
        Dim Emps As List(Of Employee) = Employee.getAllEmps()
        Me.employeesDataGrid.ItemsSource = Emps


    End Sub
    Public Sub New(contentGrid As Grid)
        InitializeComponent()
        Dim userRepo As New UserRepository()
        Dim Emps As List(Of Employee) = Employee.getAllEmps()
        employeesDataGrid.ItemsSource = Emps
        _contentGrid = contentGrid
    End Sub

    Private Sub SearchButton_Click(sender As Object, e As RoutedEventArgs)
        Dim empRepo As New EmployeeRepository()
        Dim Emps As List(Of Employee) = empRepo.SearchEmps(SearchBox.Text)
        Me.employeesDataGrid.ItemsSource = Emps
    End Sub

    Private Sub AddEmployee_Click(sender As Object, e As RoutedEventArgs) Handles btnAdd.Click

    End Sub

    Private Sub EditButton_Click(sender As Object, e As RoutedEventArgs)

        Dim selectedUser As Users = CType(employeesDataGrid.SelectedItem, Users)


        If selectedUser IsNot Nothing Then

            Dim editUserView As New EmployeeEdit(selectedUser, _contentGrid)
            Dim contentControl As New ContentControl()

            contentControl.Content = editUserView
            _contentGrid.Children.Clear()
            _contentGrid.Children.Add(contentControl)


        Else
            MessageBox.Show("Please select a user to edit.", "No User Selected", MessageBoxButton.OK, MessageBoxImage.Information)
        End If
    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As RoutedEventArgs)
        Dim emp As Employee = CType(employeesDataGrid.SelectedItem, Employee)
        Dim empRepo As New EmployeeRepository()
        empRepo.Delete(emp.EmployeeID)
        Dim Emps As List(Of Employee) = Employee.getAllEmps()
        Me.employeesDataGrid.ItemsSource = Emps
    End Sub

    Private Sub ShowEmployeeAddView(sender As Object, e As RoutedEventArgs)

        Dim employeeAddView As New EmployeeAdd(_contentGrid)
        Dim contentControl As New ContentControl()

        contentControl.Content = employeeAddView
        _contentGrid.Children.Clear()
        _contentGrid.Children.Add(contentControl)
    End Sub

    Private Sub DeleteEmployee_Click(sender As Object, e As RoutedEventArgs)
        Dim Result As MessageBoxResult = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Deletion", MessageBoxButton.YesNo)
        If Result = MessageBoxResult.Yes Then
            Dim emp As Employee = CType(employeesDataGrid.SelectedItem, Employee)
            If emp IsNot Nothing Then
                Dim empRepo As New EmployeeRepository()
                Dim success As Boolean = empRepo.Delete(emp.EmployeeID)
                If success Then
                    Dim Emps As List(Of Employee) = Employee.getAllEmps()
                    Me.employeesDataGrid.ItemsSource = Emps
                Else
                    MessageBox.Show("Failed to delete user.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                End If
            Else
                MessageBox.Show("No user selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            End If
        End If




    End Sub


End Class
