Public Class RequestEmployee

    Private userId As Integer
    Public Sub New(userId As Integer)
        InitializeComponent()
        Me.userId = userId
    End Sub
    Private Sub RequestBtn(sender As Object, e As RoutedEventArgs)
        ' Check if ComboBox and DatePicker are not null
        If Type.SelectedItem IsNot Nothing AndAlso DateOfRequest.SelectedDate.HasValue Then
            ' Get all texts
            Dim reasonType As String = DirectCast(Type.SelectedItem, ComboBoxItem).Content.ToString()
            Dim reasonText As String = Reason.Text
            Dim dateOfRequests As DateTime = DateOfRequest.SelectedDate.Value
            MessageBox.Show(reasonType & " " & reasonText & " " & dateOfRequests)
            Dim Requestinfo As New Requests()
            Requestinfo.EmployeeID = userId

            Requestinfo.Type = reasonType
            Requestinfo.Status = "Pending"
            Requestinfo.Time = dateOfRequests
            Requestinfo.Reason = reasonText


            Dim req As New RequestRepository()

            Dim success As Boolean = req.Create(Requestinfo)

            If success Then
                MessageBox.Show("Request submitted successfully.")
            Else
                MessageBox.Show("Failed to submit request.")
            End If
        Else
            MessageBox.Show("Please select a type and date.")
        End If
    End Sub


End Class
