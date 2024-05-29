
Public Class DashboardView
    Private _contentGrid As Grid


    Public Sub New(contentGrid As Grid)
        InitializeComponent()
        _contentGrid = contentGrid
        Dim Emps As List(Of Employee) = Employee.getAllEmps()
        txtNumberOfEmployees.Text = (Emps.Count.ToString() & " Employees")
        lstEmployees.ItemsSource = Emps
        lstTopEmployees.ItemsSource = Emps
    End Sub

    Public Sub AddEmp_Click()
        Dim employeeAddView As New EmployeeAdd(_contentGrid)
        Dim contentControl As New ContentControl()

        contentControl.Content = employeeAddView
        _contentGrid.Children.Clear()
        _contentGrid.Children.Add(contentControl)
    End Sub

    Public Sub Logout_Click()
        Throw New NotImplementedException()
    End Sub



End Class
