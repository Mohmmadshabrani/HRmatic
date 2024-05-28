Public Class Login
    Public Sub Mouse_down(sender As Object, e As MouseButtonEventArgs)
        Dim window As Window = window.GetWindow(sender)
        window.DragMove()
    End Sub
    Private Sub btnMinimize_Click(sender As Object, e As RoutedEventArgs)
        Me.WindowState = WindowState.Minimized
    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub
    Public Sub btnLogin_Click(sender As Object, e As RoutedEventArgs)
        Dim username As String = txtUser.Text
        Dim password As String = txtPass.Password
        If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(password) Then
            txtError.Text = "Please enter username and password."
            txtError.Visibility = Visibility.Visible
            Return
        End If

        Dim user As Users = Users.ValidateCredentials(username, password)

        If user Is Nothing Then
            txtPass.Password = ""
            txtError.Text = "Invalid username or password."
            txtError.Visibility = Visibility.Visible
        ElseIf user.isAdmin() Then
            Dim MainDashboard = New MainDashboard
            MainDashboard.Show()
            Me.Close()
        Else

            Dim employeeDashboard = New EmpDashboard(user)
            employeeDashboard.Show()
            Me.Close()
        End If
    End Sub

    Private Sub TxtPass_KeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Enter Then
            btnLogin_Click(btnLogin, New RoutedEventArgs())
        End If
    End Sub


End Class
