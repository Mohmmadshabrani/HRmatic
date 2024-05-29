
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports Org.BouncyCastle.Asn1.Utilities

Public Class RequestRepository
    Public Shared connectionString As String
    Public Sub New()
        connectionString = ConfigurationManager.ConnectionStrings("HRmatic").ConnectionString
    End Sub
    Public Function Create(request As Requests) As Boolean
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "INSERT INTO Requests(EmpID, Type, Status, Time, Reason) VALUES(@EmployeeID, @Type, @Status, @Time, @Reason)"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@EmpID", request.EmployeeID)
                    cmd.Parameters.AddWithValue("@Type", request.Type)
                    cmd.Parameters.AddWithValue("@Status", request.Status)
                    cmd.Parameters.AddWithValue("@Time", request.Time)
                    cmd.Parameters.AddWithValue("@Reason", request.Reason)
                    cmd.ExecuteNonQuery()
                End Using
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Using
        Return False
    End Function

    Public Function GetRequest(ID As Integer) As Requests
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "SELECT * FROM Requests WHERE ID = @ID"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ID", ID)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return New Requests() With {
                                .ID = reader("ID"),
                                .EmployeeID = reader("EmpID"),
                                .Type = reader("Type"),
                                .Status = reader("Status"),
                                .Time = reader("Time"),
                                .Reason = reader("Reason")
                            }
                        Else
                            Return Nothing
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MsgBox("Failed to get request")
                Return Nothing
            End Try
        End Using
    End Function

    Public Function GetAllRequests() As List(Of Requests)
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "SELECT * FROM Requests"
                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        Dim requests As New List(Of Requests)
                        While reader.Read()
                            Dim Emp As Employee = Employee.GetEmp(reader("EmpID"))
                            Dim empName As String = "Unknown Employee"
                            If Emp IsNot Nothing Then
                                empName = Emp.FirstName & " " & Emp.LastName
                            End If

                            requests.Add(New Requests() With {
                            .ID = reader("ID"),
                            .EmployeeID = reader("EmpID"),
                            .Type = reader("Type"),
                            .Status = reader("Status"),
                            .Time = reader("Time"),
                            .Reason = reader("Reason"),
                            .EmpName = empName
                        })
                        End While
                        Return requests
                    End Using
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
                Return Nothing
            End Try
        End Using
    End Function


    Public Function Update(request As Requests) As Boolean
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "UPDATE Requests SET EmpID = @EmployeeID, Type = @Type, Status = @Status, Time = @Time, Reason = @Reason WHERE ID = @ID"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ID", request.ID)
                    cmd.Parameters.AddWithValue("@EmployeeID", request.EmployeeID)
                    cmd.Parameters.AddWithValue("@Type", request.Type)
                    cmd.Parameters.AddWithValue("@Status", request.Status)
                    cmd.Parameters.AddWithValue("@Time", request.Time)
                    cmd.Parameters.AddWithValue("@Reason", request.Reason)
                    cmd.ExecuteNonQuery()
                End Using
                Return True
            Catch ex As Exception
                MsgBox("Failed to update request")
                Return False
            End Try
        End Using
    End Function

    Public Function Delete(ID As Integer) As Boolean
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "DELETE FROM Requests WHERE ID = @ID"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ID", ID)
                    cmd.ExecuteNonQuery()
                End Using
                Return True
            Catch ex As Exception
                MsgBox("Failed to delete request")
                Return False
            End Try
        End Using
    End Function

    Public Function GetRequestByEmployeeID(EmployeeID As Integer) As List(Of Requests)
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "SELECT * FROM Requests WHERE EmpID = @EmployeeID"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        Dim requests As New List(Of Requests)
                        While reader.Read()
                            requests.Add(New Requests() With {
                                .ID = reader("ID"),
                                .EmployeeID = reader("EmpID"),
                                .Type = reader("Type"),
                                .Status = reader("Status"),
                                .Time = reader("Time"),
                                .Reason = reader("Reason")
                            })
                        End While
                        Return requests
                    End Using
                End Using
            Catch ex As Exception
                MsgBox("Failed to get requests")
                Return Nothing
            End Try
        End Using
    End Function

End Class
