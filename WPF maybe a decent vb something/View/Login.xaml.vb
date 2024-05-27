Public Class Login
    Public Sub Mouse_down(sender As Object, e As MouseButtonEventArgs)
        Dim window As Window = Window.GetWindow(sender)
        window.DragMove()
    End Sub
    Private Sub btnMinimize_Click(sender As Object, e As RoutedEventArgs)
        Me.WindowState = WindowState.Minimized
    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub
    Private Sub btnLogin_Click(sender As Object, e As RoutedEventArgs)
        Dim username As String = txtUser.Text
        Dim password As String = txtPass.Password
        If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(password) Then
            txtError.Text = "Please enter username and password."
            txtError.Visibility = Visibility.Visible
            Return
        End If

        If Users.ValidateCredentials(username, password) Then
            Dim MainDashboard = New MainDashboard
            MainDashboard.Show()
            Me.Close()


        Else
            txtPass.Password = ""
            txtError.Text = "Invalid username or password."
            txtError.Visibility = Visibility.Visible
        End If
    End Sub

End Class
