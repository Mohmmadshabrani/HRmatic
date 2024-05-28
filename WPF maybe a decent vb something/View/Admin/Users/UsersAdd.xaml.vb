Imports System.Windows
Imports System.Windows.Controls
Imports Microsoft.VisualBasic.ApplicationServices

Public Class UsersAdd
    Private ReadOnly userRepository As New UserRepository()

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub AddUser_Click(sender As Object, e As RoutedEventArgs)

        Dim username As String = UsernameTextBox.Text
        Dim password As String = PasswordTextBox.Text
        Dim email As String = EmailTextBox.Text
        Dim isActive As Boolean = IsActiveCheckBox.IsChecked.GetValueOrDefault()

        Dim newUser As New Users() With {
            .Username = username,
            .Password = password,
            .Email = email,
            .IsActive = isActive
        }


        Dim success As Boolean = Users.Create(newUser)

        If success Then
            MessageBox.Show("User added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information)


        Else
            MessageBox.Show("Failed to add user. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End If
    End Sub
End Class
