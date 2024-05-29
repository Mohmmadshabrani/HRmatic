Imports System.Collections.ObjectModel
Imports System.Diagnostics.Metrics

Public Class RequestsView
    Public Sub ApproveBtn_Click(sender As Object, e As RoutedEventArgs)
        Throw New NotImplementedException()
    End Sub

    Public Sub DenyBtn_Click(sender As Object, e As RoutedEventArgs)
        Throw New NotImplementedException()
    End Sub

    Public Sub SearchButton_Click(sender As Object, e As RoutedEventArgs)
        Throw New NotImplementedException()
    End Sub

    Public Sub New()
        InitializeComponent()
        Dim requestRepo As New RequestRepository()
        Dim requests As List(Of Requests) = requestRepo.GetAllRequests()
        requestsDataGrid.ItemsSource = requests
    End Sub


End Class
