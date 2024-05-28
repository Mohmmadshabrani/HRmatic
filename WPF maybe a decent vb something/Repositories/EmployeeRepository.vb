Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class EmployeeRepository
    Public Shared connectionString As String

    Public Sub New()
        connectionString = ConfigurationManager.ConnectionStrings("HRmatic").ConnectionString
    End Sub
    ' Create a new user
    Public Function Create(emp As Employee) As Boolean
        emp.Password = HashPassword(emp.Password)
        Using conn As New MySqlConnection(connectionString)
            Try
                If Users.Create(emp) Then
                    conn.Open()
                    Dim query As String = "INSERT INTO Users (Username, Password, Email, IsActive) VALUES (@Username, @Password, @Email, @IsActive)"
                    Using cmd As New MySqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@Username", emp.Username)
                        cmd.Parameters.AddWithValue("@Password", emp.Password)
                        cmd.Parameters.AddWithValue("@Email", emp.Email)
                        cmd.Parameters.AddWithValue("@IsActive", emp.IsActive)
                        cmd.ExecuteNonQuery()
                    End Using
                End If
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Using
    End Function

    ' Read user information
    Public Function GetEmp(ID As Integer) As Employee
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "SELECT * FROM Employee Join users on users.id =  WHERE ID = @ID"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ID", ID)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim user As Users = Users.GetUser(reader("ID"))
                            Return New Employee() With {
                                .FirstName = reader("FirstName"),
                                .LastName = reader("LastName"),
                                .Salary = reader("Salary"),
                                .Department = reader("Department"),
                                .DateResigned = reader("DateResigned"),
                                .DateHired = reader("DateHired"),
                                .EmployeeID = reader("ID")
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
    Public Function Update(emp As Employee) As Boolean
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "UPDATE Employee SET FirstName = @FirstName, LastName = @LastName, Salary = @Salary, Department = @Department, DateResigned = @DateResigned, DateHired = @DateHired WHERE ID = @ID"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ID", emp.EmployeeID)
                    cmd.Parameters.AddWithValue("@FirstName", emp.FirstName)
                    cmd.Parameters.AddWithValue("@LastName", emp.LastName)
                    cmd.Parameters.AddWithValue("@Salary", emp.Salary)
                    cmd.Parameters.AddWithValue("@Department", emp.Department)
                    cmd.Parameters.AddWithValue("@DateResigned", emp.DateResigned)
                    cmd.Parameters.AddWithValue("@DateHired", emp.DateHired)
                    cmd.ExecuteNonQuery()
                End Using
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Using
    End Function

    ' Delete user
    Public Function Delete(ID As Integer) As Boolean
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "DELETE FROM Employee WHERE ID = @ID"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ID", ID)
                    cmd.ExecuteNonQuery()
                End Using
                Return True
            Catch ex As Exception
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
        Using sha256 As SHA256 = sha256.Create()
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
    ' userRepo.SearchUsers
    Public Function SearchUsers(searchValue As String) As List(Of Users)
        Dim users As New List(Of Users)()
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "SELECT * FROM Users WHERE Username LIKE @SearchValue OR Email LIKE @SearchValue"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@SearchValue", "%" & searchValue & "%")
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            users.Add(New Users() With {
                                .ID = reader("ID"),
                                .Username = reader("Username"),
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
