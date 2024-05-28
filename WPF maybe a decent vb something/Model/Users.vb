Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.ApplicationServices
Public Class Users
    ' Properties
    Public Property ID As Integer
    Public Property Username As String
    Public Property Password As String
    Public Property Email As String
    Public Property created_at As DateTime
    Public Property updated_at As DateTime
    Public Property deleted_at As DateTime
    Public Property IsActive As Boolean
    Public Property Role As String


    Public Shared Property UserRepo As UserRepository = New UserRepository()

    ' Constructor
    Public Sub New()
        Me.ID = 0
        Me.Username = ""
        Me.Password = ""
        Me.Email = ""
        Me.IsActive = False
        Me.created_at = DateTime.Now
        Me.updated_at = DateTime.Now
        Me.deleted_at = DateTime.Now
        Me.Role = ""
    End Sub

    Public Sub New(ID As Integer, username As String, password As String, email As String, isActive As Boolean, Role As String)
        Me.ID = ID
        Me.Username = username
        Me.Password = password
        Me.Email = email
        Me.IsActive = isActive
        Me.created_at = DateTime.Now
        Me.updated_at = DateTime.Now
        Me.Role = Role
    End Sub

    ' Create a new user overrideable
    Public Overridable Function Create() As Boolean
        Return Create(Me)
    End Function



    Public Shared Function Create(User As Users) As Boolean
        Try
            If Not UserRepo.Create(User) Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ' Read user information
    Public Shared Function GetUser(ID As Integer) As Users
        Try
            Dim user As Users = UserRepo.GetUser(ID)
            Return user
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    ' Validate user credentials
    Public Shared Function ValidateCredentials(username As String, password As String) As Users
        Try
            ' Validate user credentials
            Dim user As Users = UserRepo.ValidateCredentials(username, password)
            If user Is Nothing Then
                Return Nothing
            End If

            Return user
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ' Get all users
    Public Function GetAllUsers() As List(Of Users)
        Try
            Dim users As List(Of Users) = UserRepo.GetAllUsers()
            Return users
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function isAdmin() As Boolean
        MsgBox(Me.Role)
        If Me.Role = "admin" Then
            Return True
        End If
        Return False
    End Function

End Class
