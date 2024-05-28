Imports System.Data.SqlClient
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

    Public Shared Property userRepo As UserRepository = New UserRepository()

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
    End Sub

    Public Sub New(ID As Integer, username As String, password As String, email As String, isActive As Boolean)
        Me.ID = ID
        Me.Username = username
        Me.Password = password
        Me.Email = email
        Me.IsActive = isActive
        Me.created_at = DateTime.Now
        Me.updated_at = DateTime.Now
    End Sub

    ' Create a new user
    Public Function Create() As Boolean
        Try
            ' Insert user into database
            If Not userRepo.Create(Me) Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ' Read user information
    Public Function Read(ID As Integer) As Users
        Try
            Dim user As Users = userRepo.GetUser(ID)
            Return user
        Catch ex As Exception
            ' Handle exceptions
            Return Nothing
        End Try
    End Function

    ' Update user information
    Public Function Update() As Boolean
        Try
            Return True
        Catch ex As Exception
            ' Handle exceptions
            Return False
        End Try
    End Function

    ' Delete user
    Public Function Delete(ID As Integer) As Boolean
        Try
            Return True
        Catch ex As Exception
            ' Handle exceptions
            Return False
        End Try
    End Function

    ' Additional functions

    ' Validate user credentials
    Public Shared Function ValidateCredentials(username As String, password As String) As Boolean
        Try
            ' Validate user credentials
            If Not userRepo.ValidateCredentials(username, password) Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ' Get all users
    Public Function GetAllUsers() As List(Of Users)
        Try
            Dim users As New List(Of Users)()
            ' Populate list of users from database result
            Return users
        Catch ex As Exception
            Return Nothing
        End Try
    End Function




End Class
