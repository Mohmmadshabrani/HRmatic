
Imports System.Runtime.InteropServices
Imports System.Windows.Interop

Public Class MainDashboard

    Public Sub New()
        InitializeComponent()
        Dim dashboardView As New DashboardView()
        ContentGrid.Children.Add(dashboardView)
    End Sub


    <DllImport("user32.dll")>
    Public Shared Function SendMessage(hWnd As IntPtr, wMsg As Integer, wParam As Integer, lParam As Integer) As IntPtr
    End Function

    Private Sub pnl_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        Dim helper As New WindowInteropHelper(Me)
        SendMessage(helper.Handle, 161, 2, 0)
    End Sub

    Private Sub pnl_MouseEnter(sender As Object, e As MouseEventArgs)
        Me.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight
    End Sub
    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub
    Private Sub btnMaximize_Click(sender As Object, e As RoutedEventArgs)
        Me.WindowState = WindowState.Maximized
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As RoutedEventArgs)
        Me.WindowState = WindowState.Minimized
    End Sub
    Private Sub ShowHomeView(sender As Object, e As RoutedEventArgs)
        Dim homeView As New DashboardView()
        ContentGrid.Children.Clear()
        ContentGrid.Children.Add(homeView)
    End Sub

    Private Sub ShowUsersView(sender As Object, e As RoutedEventArgs)
        Dim employeeView As New UsersView(ContentGrid)
        ContentGrid.Children.Clear()
        ContentGrid.Children.Add(employeeView)
    End Sub
    Private Sub ShowEmployeeView(sender As Object, e As RoutedEventArgs)
        Dim employeeView As New UsersView(ContentGrid)
        ContentGrid.Children.Clear()
        ContentGrid.Children.Add(employeeView)
    End Sub

    Private Sub ShowDashboardView(sender As Object, e As RoutedEventArgs)
        Dim dashboardView As New DashboardView()
        ContentGrid.Children.Clear()
        ContentGrid.Children.Add(dashboardView)
    End Sub

    Public Sub ShowRequestsView(sender As Object, e As RoutedEventArgs)
        Dim requestsView As New RequestsView()
        ContentGrid.Children.Clear()
        ContentGrid.Children.Add(requestsView)
    End Sub
    Private Sub ShowEmployeeDGV(sender As Object, e As RoutedEventArgs)
        Dim employeeView As New EmployeeDGV()
        ContentGrid.Children.Clear()
        ContentGrid.Children.Add(employeeView)
    End Sub


    Private Sub ShowEmployeesView(sender As Object, e As RoutedEventArgs)
        Dim employeeView As New EmployeeDGV(ContentGrid)
        ContentGrid.Children.Clear()
        ContentGrid.Children.Add(employeeView)
    End Sub


End Class
