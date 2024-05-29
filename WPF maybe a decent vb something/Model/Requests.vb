Public Class Requests

    Public Property ID As Integer
    Public Property EmployeeID As Integer
    Public Property EmpName As String
    Public Property Type As String
    Public Property Status As String
    Public Property Time As Date
    Public Property Reason As String
    Public Shared Property RequestRepo As RequestRepository = New RequestRepository()

    Public Sub New()
        Me.ID = 0
        Me.EmployeeID = 0
        Me.EmpName = ""
        Me.Type = ""
        Me.Status = ""
        Me.Time = Date.Now
        Me.Reason = ""
    End Sub

    Public Sub New(ID As Integer, EmployeeID As Integer, EmpName As String, Type As String, Status As String, Time As Date, Reason As String)
        Me.ID = ID
        Me.EmployeeID = EmployeeID
        Me.EmpName = EmpName
        Me.Type = Type
        Me.Status = Status
        Me.Time = Time
        Me.Reason = Reason
    End Sub

    Public Function Create()
        Try
            Return Create(Me)
        Catch ex As Exception
            MsgBox("Failed to create request")
            Return False
        End Try
    End Function

    Public Shared Function Create(Request As Requests) As Boolean
        Try
            Return RequestRepo.Create(Request)
        Catch ex As Exception
            MsgBox("Failed to create request")
            Return False
        End Try
    End Function
    Public Shared Function GetRequest(ID As Integer) As Requests
        Try
            Dim request As Requests = RequestRepo.GetRequest(ID)
            Return request
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetAllRequests() As List(Of Requests)

        Try
            Dim requests As List(Of Requests) = RequestRepo.GetAllRequests()
            Return requests
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function UpdateRequest(Request As Requests) As Boolean
        Try
            If Not RequestRepo.Update(Request) Then
                Throw New Exception("Failed to update request")
            End If
            Return True
        Catch ex As Exception
            Throw New Exception("Failed to update request")
        End Try
    End Function

    Public Function Delete(ID As Integer) As Boolean
        Try
            If Not RequestRepo.Delete(ID) Then
                Throw New Exception("Failed to delete request")
            End If
            Return True
        Catch ex As Exception
            Throw New Exception("Failed to delete request")
        End Try
    End Function

    Public Function GetRequestByEmployeeID(ID As Integer) As List(Of Requests)
        Try
            Dim requests As List(Of Requests) = RequestRepo.GetRequestByEmployeeID(ID)
            Return requests
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
