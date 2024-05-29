Public Class EmployeeEdit

    Private _selectedUser As Employee

    Private _contentGrid As Grid

    Public Sub New(selectedUser As Employee, contentGrid As Grid)

        _selectedUser = selectedUser
        _contentGrid = contentGrid
        InitializeComponent()
        FirstName.Text = selectedUser.FirstName
        LastName.Text = selectedUser.LastName
        Department.Text = selectedUser.Department
        Salary.Text = selectedUser.Salary
        DateOfHired.SelectedDate = selectedUser.DateHired




    End Sub
    Private Sub EditUser_Click(sender As Object, e As RoutedEventArgs)
        ' save 
        _selectedUser.FirstName = FirstName.Text
        _selectedUser.LastName = LastName.Text
        _selectedUser.Department = Department.Text
        _selectedUser.Salary = Decimal.Parse(Salary.Text)
        _selectedUser.DateHired = DateOfHired.SelectedDate.GetValueOrDefault()




        SaveChanges()
    End Sub


    Private Sub SaveChanges()

        Dim emp As New EmployeeRepository()
        Dim success As Boolean = emp.Update(_selectedUser)

        If success Then
            MessageBox.Show("User updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
            Dim employeeView As New EmployeeDGV(_contentGrid)
            _contentGrid.Children.Clear()
            _contentGrid.Children.Add(employeeView)
        Else
            MessageBox.Show("Failed to update user. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)

        End If

    End Sub
End Class
