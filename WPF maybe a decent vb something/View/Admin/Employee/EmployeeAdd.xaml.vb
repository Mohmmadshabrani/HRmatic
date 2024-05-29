Public Class EmployeeAdd
    Private _contentGrid As Grid
    Public Sub New(contentGrid As Grid)
        InitializeComponent()
        _contentGrid = contentGrid
        ' pass word  showing as dots

    End Sub

    Private Sub AddEmployee(sender As Object, e As RoutedEventArgs) Handles AddUser.Click

        Dim emp As New Employee()
        ' check text boxes for empty
        If String.IsNullOrEmpty(FirstName.Text) Or String.IsNullOrEmpty(LastName.Text) Or String.IsNullOrEmpty(Department.Text) Or String.IsNullOrEmpty(Salary.Text) Or String.IsNullOrEmpty(Username.Text) Or String.IsNullOrEmpty(Password.Text) Or String.IsNullOrEmpty(Email.Text) Then
            MessageBox.Show("Please fill out all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If
        emp.FirstName = FirstName.Text
        emp.LastName = LastName.Text
        emp.Department = Department.Text
        emp.Salary = Decimal.Parse(Salary.Text)
        emp.DateHired = DateOfHired.SelectedDate.GetValueOrDefault()
        ' username
        emp.Username = Username.Text
        ' password
        emp.Password = Password.Text
        ' email
        emp.Email = Email.Text
        ' is active
        emp.IsActive = isActive.IsChecked.GetValueOrDefault()



        Dim success As Boolean = Employee.Create(emp)

        If success Then
            MessageBox.Show("Employee added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
            Dim employeeView As New EmployeeDGV(_contentGrid)
            _contentGrid.Children.Clear()
            _contentGrid.Children.Add(employeeView)

        Else
            MessageBox.Show("Failed to add employee. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End If
    End Sub
End Class
