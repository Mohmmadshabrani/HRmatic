Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class UserRepository
    Public Shared connectionString As String

    Public Sub New()
        connectionString = ConfigurationManager.ConnectionStrings("HRmatic").ConnectionString
    End Sub
    ' Create a new user
    Public Function Create(user As Users) As Boolean
        user.Password = HashPassword(user.Password)
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "INSERT INTO Users (Username, Password, Email, IsActive) VALUES (@Username, @Password, @Email, @IsActive)"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Username", user.Username)
                    cmd.Parameters.AddWithValue("@Password", user.Password)
                    cmd.Parameters.AddWithValue("@Email", user.Email)
                    cmd.Parameters.AddWithValue("@IsActive", user.IsActive)
                    cmd.ExecuteNonQuery()
                End Using
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Using
    End Function

    ' Read user information
    Public Function GetUser(ID As Integer) As Users
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "SELECT * FROM Users WHERE ID = @ID"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ID", ID)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return New Users() With {
                                .ID = reader("ID"),
                                .Username = reader("Username"),
                                .Password = reader("Password"),
                                .Email = reader("Email"),
                                .IsActive = reader("IsActive")
                            }
                        Else
                            Return Nothing
                        End If
                    End Using
                End Using
            Catch ex As Exception
                ' Handle exceptions
                Return Nothing
            End Try
        End Using
    End Function

    ' Update user information
    Public Function Update(user As Users) As Boolean
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "UPDATE Users SET Username = @Username, Password = @Password, Email = @Email, IsActive = @IsActive WHERE ID = @ID"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ID", user.ID)
                    cmd.Parameters.AddWithValue("@Username", user.Username)
                    cmd.Parameters.AddWithValue("@Password", user.Password)
                    cmd.Parameters.AddWithValue("@Email", user.Email)
                    cmd.Parameters.AddWithValue("@IsActive", user.IsActive)
                    cmd.ExecuteNonQuery()
                End Using
                Return True
            Catch ex As Exception
                ' Handle exceptions
                Return False
            End Try
        End Using
    End Function

    ' Delete user
    Public Function Delete(ID As Integer) As Boolean
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "DELETE FROM Users WHERE ID = @ID"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ID", ID)
                    cmd.ExecuteNonQuery()
                End Using
                Return True
            Catch ex As Exception
                ' Handle exceptions
                Return False
            End Try
        End Using
    End Function

    ' Validate user credentials
    Public Function ValidateCredentials(username As String, password As String) As Boolean
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Username", username)
                    cmd.Parameters.AddWithValue("@Password", HashPassword(password))
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    Return count > 0
                End Using
                conn.Close()
            Catch ex As Exception
                Return False
            End Try
        End Using
    End Function

    Public Shared Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Dim builder As New StringBuilder()
            For Each b As Byte In hash
                builder.Append(b.ToString("x2"))
            Next
            Return builder.ToString()
        End Using
    End Function
    ' Get all users
    Public Function GetAllUsers() As List(Of Users)
        Dim users As New List(Of Users)()
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "SELECT * FROM Users"
                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            users.Add(New Users() With {
                                .ID = reader("ID"),
                                .Username = reader("Username"),
                                .Password = reader("Password"),
                                .Email = reader("Email"),
                                .IsActive = reader("IsActive")
                            })
                        End While
                    End Using
                End Using
            Catch ex As Exception
                ' Handle exceptions
            End Try
        End Using
        Return users
    End Function
End Class
