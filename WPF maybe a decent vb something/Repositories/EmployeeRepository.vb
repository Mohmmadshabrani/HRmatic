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
        Using conn As New MySqlConnection(connectionString)
            Try
                If Users.Create(emp) Then
                    conn.Open()
                    Dim query As String = "INSERT INTO Employee(ID, FirstName, LastName, Salary, Department, DateResigned, DateHired) VALUES(@ID, @FirstName, @LastName, @Salary, @Department, @DateResigned, @DateHired)"
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
                Else
                    Return False
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
                Dim query As String = "SELECT * FROM Employee WHERE ID = @ID"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ID", ID)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim user As Users = Users.GetUser(ID)
                            Return New Employee() With {
                                .EmployeeID = reader("ID"),
                                .FirstName = reader("FirstName"),
                                .LastName = reader("LastName"),
                                .Salary = reader("Salary"),
                                .Department = reader("Department"),
                                .DateResigned = reader("DateResigned"),
                                .DateHired = reader("DateHired"),
                                .Username = user.Username,
                                .Email = user.Email,
                                .IsActive = user.IsActive,
                                .Role = user.Role
                            }
                        End If
                    End Using
                End Using
            Catch ex As Exception
                Return Nothing
            End Try
            Return Nothing
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
    ' Get all users
    Public Function GetAllEmps() As List(Of Employee)
        Dim Emps As New List(Of Employee)()
        Using conn As New MySqlConnection(connectionString)
            Try

                conn.Open()
                Dim query As String = "SELECT * FROM Employee"
                Using cmd As New MySqlCommand(query, conn)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()

                            Dim user As Users = Users.GetUser(reader("ID"))


                            Emps.Add(New Employee() With {
                                .EmployeeID = reader("ID"),
                                .FirstName = reader("FirstName").ToString(),
                                .LastName = reader("LastName").ToString(),
                                .Salary = Convert.ToDecimal(reader("Salary")),
                                .Department = reader("Department").ToString(),
                                .DateHired = Convert.ToDateTime(reader("DateHired")),
                                .DateResigned = If(IsDBNull(reader("DateResigned")), Nothing, Convert.ToDateTime(reader("DateResigned"))),
                                .Username = user.Username,
                                .Email = user.Email,
                                .IsActive = user.IsActive,
                                .Role = user.Role
                            })
                        End While
                    End Using
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
                ' line of the error
                MsgBox(ex.StackTrace)
            End Try
        End Using


        Return Emps
    End Function
    ' userRepo.SearchUsers
    Public Function SearchEmps(searchValue As String) As List(Of Employee)
        Dim Emps As New List(Of Employee)()
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "SELECT * FROM Employee WHERE ID LIKE @searchValue OR FirstName LIKE @searchValue OR LastName LIKE @searchValue OR Department LIKE @searchValue"
                MsgBox(query)
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@searchValue", "%" & searchValue & "%")
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim user As Users = Users.GetUser(reader("ID"))
                            Emps.Add(New Employee() With {
                                .EmployeeID = reader("ID"),
                                .FirstName = reader("FirstName"),
                                .LastName = reader("LastName"),
                                .Salary = reader("Salary"),
                                .Department = reader("Department"),
                                .DateResigned = reader("DateResigned"),
                                .DateHired = reader("DateHired"),
                                .Username = user.Username,
                                .Email = user.Email,
                                .IsActive = user.IsActive,
                                .Role = user.Role
                            })
                        End While
                    End Using
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Return Emps
    End Function

End Class
