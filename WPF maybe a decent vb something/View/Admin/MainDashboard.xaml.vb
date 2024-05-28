Public Class MainDashboard
    Private Sub RadioButton_Checked(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub RadioButton_Checked_1(sender As Object, e As RoutedEventArgs)

    End Sub
    Private Sub ShowEmployeeView(sender As Object, e As RoutedEventArgs)
        Dim employee As New EmployeeView()
        ContentGrid.Children.Clear()
        ContentGrid.Children.Add(employee)
    End Sub
End Class
