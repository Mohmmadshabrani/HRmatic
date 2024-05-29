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
        Dim empEdit As New EmployeeEdit()
        empEdit.ShowEmployeeAddView()
    End Sub

    Private Sub EditButton_Click(sender As Object, e As RoutedEventArgs)
        Dim emp As Employee = CType(employeesDataGrid.SelectedItem, Employee)
        Dim employeeAddView As New UsersAdd()
        Dim contentControl As New ContentControl()

        contentControl.Content = employeeAddView
        _contentGrid.Children.Clear()
        _contentGrid.Children.Add(contentControl)
    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As RoutedEventArgs)
        Dim emp As Employee = CType(employeesDataGrid.SelectedItem, Employee)
        Dim empRepo As New EmployeeRepository()
        empRepo.Delete(emp.EmployeeID)
        Dim Emps As List(Of Employee) = Employee.getAllEmps()
        Me.employeesDataGrid.ItemsSource = Emps
    End Sub

    Private Sub ShowEmployeeAddView(sender As Object, e As RoutedEventArgs)
        Dim empEdit As New EmployeeEdit()
        empEdit.ShowEmployeeAddView()
    End Sub
End Class
