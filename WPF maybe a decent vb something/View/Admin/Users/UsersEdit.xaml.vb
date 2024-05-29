Imports Microsoft.VisualBasic.ApplicationServices

Public Class UsersEdit
    Private _selectedUser As Users
    Private _contentGrid As Grid


    Public Sub New(selectedUser As Users, contentGrid As Grid)

        _selectedUser = selectedUser
        _contentGrid = contentGrid

        InitializeComponent()
        UsernameTextBox.Text = selectedUser.Username

        EmailTextBox.Text = selectedUser.Email
        IsActiveCheckBox.IsChecked = selectedUser.IsActive
    End Sub
    Private Sub EditUser_Click(sender As Object, e As RoutedEventArgs)

        _selectedUser.Username = UsernameTextBox.Text
        _selectedUser.Email = EmailTextBox.Text
        _selectedUser.IsActive = IsActiveCheckBox.IsChecked.GetValueOrDefault()

        SaveChanges()
    End Sub


    Private Sub SaveChanges()

        Dim userRepo As New UserRepository()
        Dim success As Boolean = userRepo.Update(_selectedUser)

        If success Then
            MessageBox.Show("User updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
            Dim userView As New UsersView(_contentGrid)
            _contentGrid.Children.Clear()
            _contentGrid.Children.Add(userView)

        Else
            MessageBox.Show("Failed to update user. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End If

    End Sub
End Class
