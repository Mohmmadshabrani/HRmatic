Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Public Class EmployeeViewModel
    Implements INotifyPropertyChanged

    Private _users As ObservableCollection(Of Users)
    Public Property Users As ObservableCollection(Of Users)
        Get
            Return _users
        End Get
        Set(ByVal value As ObservableCollection(Of Users))
            _users = value
            OnPropertyChanged()
        End Set
    End Property

    Public Sub New()
        LoadUsers()
    End Sub

    Private Sub LoadUsers()
        Dim userRepo As New UserRepository()
        Dim userList = userRepo.GetAllUsers()
        Users = New ObservableCollection(Of Users)(userList)
    End Sub
    Public Sub SaveUser(user As Users)
        Dim userRepo As New UserRepository()
        If user.ID = 0 Then
            userRepo.Create(user)
        Else
            userRepo.Update(user)
        End If
        LoadUsers()
    End Sub
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Protected Sub OnPropertyChanged(<CallerMemberName> Optional ByVal name As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub
End Class
