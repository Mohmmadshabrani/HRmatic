Imports System.Diagnostics.Metrics
Imports Microsoft.VisualBasic.ApplicationServices

Public Class UsersView
    Private _contentGrid As Grid
    Public Sub New()
        InitializeComponent()


    End Sub
    Public Sub New(contentGrid As Grid)
        InitializeComponent()
        Dim userRepo As New UserRepository()
        Dim users As List(Of Users) = userRepo.GetAllUsers()
        membersDataGrid.ItemsSource = users
        _contentGrid = contentGrid
    End Sub
    ' add new user switch to EmoloyeeAdd
    Private Sub ShowEmployeeAddView(sender As Object, e As RoutedEventArgs)
        Dim employeeAddView As New UsersAdd(_contentGrid)
        Dim contentControl As New ContentControl()

        contentControl.Content = employeeAddView
        _contentGrid.Children.Clear()
        _contentGrid.Children.Add(contentControl)
    End Sub

    Private Sub DataGrid_RowEditEnding(sender As Object, e As DataGridRowEditEndingEventArgs)
        If e.EditAction = DataGridEditAction.Commit Then
            Dim viewModel As EmployeeViewModel = DirectCast(DataContext, EmployeeViewModel)
            Dim editedUser As Users = DirectCast(e.Row.Item, Users)
            viewModel.SaveUser(editedUser)
        End If
    End Sub
    Private Sub EditButton_Click(sender As Object, e As RoutedEventArgs)

        Dim selectedUser As Users = CType(membersDataGrid.SelectedItem, Users)


        If selectedUser IsNot Nothing Then

            Dim editUserView As New UsersEdit(selectedUser, _contentGrid)
            Dim contentControl As New ContentControl()

            contentControl.Content = editUserView
            _contentGrid.Children.Clear()
            _contentGrid.Children.Add(contentControl)


        Else
            MessageBox.Show("Please select a user to edit.", "No User Selected", MessageBoxButton.OK, MessageBoxImage.Information)
        End If
    End Sub
    Private Sub DeleteButton_Click(sender As Object, e As RoutedEventArgs)

        Dim result As MessageBoxResult = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Deletion", MessageBoxButton.YesNo)

        If result = MessageBoxResult.Yes Then

            Dim selectedUser As Users = CType(membersDataGrid.SelectedItem, Users)

            If selectedUser IsNot Nothing Then

                Dim userRepository As New UserRepository()
                Dim success As Boolean = userRepository.Delete(selectedUser.ID)

                If success Then

                    RefreshDataGrid()
                Else

                    MessageBox.Show("Failed to delete user.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                End If
            Else

                MessageBox.Show("No user selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            End If
        End If
    End Sub
    ' Search for user by username or email
    Private Sub SearchButton_Click(sender As Object, e As RoutedEventArgs)
        Dim searchValue As String = SearchBox.Text
        Dim userRepo As New UserRepository()
        Dim users As List(Of Users) = userRepo.SearchUsers(searchValue)
        membersDataGrid.ItemsSource = users
    End Sub




    Private Sub RefreshDataGrid()
        Dim userRepo As New UserRepository()
        Dim users As List(Of Users) = userRepo.GetAllUsers()
        membersDataGrid.ItemsSource = users
    End Sub
End Class
