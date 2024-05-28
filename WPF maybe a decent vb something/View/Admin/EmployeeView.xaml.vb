Imports System.Diagnostics.Metrics

Public Class EmployeeView
    Public Sub New()
        InitializeComponent()
        Dim members As New List(Of Member)()


        Dim converter As New BrushConverter()

        members.Add(New Member With {
    .Number = "1",
    .Character = "J",
    .BgColor = DirectCast(converter.ConvertFromString("#1098AD"), Brush),
    .Name = "John Doe",
    .Position = "Coach",
    .Email = "john.doe@gmail.com",
.Phone = "415-954-1475"
})
        members.Add(New Member With {
    .Number = "2",
    .Character = "R",
    .BgColor = DirectCast(converter.ConvertFromString("#1E88E5"), Brush),
    .Name = "Reza Alavi",
    .Position = "Administrator",
    .Email = "reza110@hotmail.com",
    .Phone = "254-451-7893"
})
        members.Add(New Member With {
    .Number = "3",
    .Character = "D",
    .BgColor = DirectCast(converter.ConvertFromString("#FF8F00"), Brush),
    .Name = "Dennis Castillo",
    .Position = "Coach",
    .Email = "deny.cast@gmail.com",
    .Phone = "125-520-0141"
})
        members.Add(New Member With {
    .Number = "4",
    .Character = "G",
    .BgColor = DirectCast(converter.ConvertFromString("#FF5252"), Brush),
    .Name = "Gabriel Cox",
    .Position = "Coach",
    .Email = "coxcox@gmail.com",
    .Phone = "808-635-1221"
})
        members.Add(New Member With {
    .Number = "5",
    .Character = "L",
    .BgColor = DirectCast(converter.ConvertFromString("#0CA678"), Brush),
    .Name = "Lena Jones",
    .Position = "Manager",
    .Email = "lena.offi@hotmail.com",
    .Phone = "320-658-9174"
})

        ' Add more members as needed...

        membersDataGrid.ItemsSource = members

    End Sub
    Private Sub DataGrid_RowEditEnding(sender As Object, e As DataGridRowEditEndingEventArgs)
        If e.EditAction = DataGridEditAction.Commit Then
            Dim viewModel As EmployeeViewModel = DirectCast(DataContext, EmployeeViewModel)
            Dim editedUser As Users = DirectCast(e.Row.Item, Users)
            viewModel.SaveUser(editedUser)
        End If
    End Sub
End Class
