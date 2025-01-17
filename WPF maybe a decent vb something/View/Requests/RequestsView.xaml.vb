﻿Imports System.Collections.ObjectModel
Imports System.Diagnostics.Metrics

Public Class RequestsView
    Public Sub SearchButton_Click(sender As Object, e As RoutedEventArgs)
        Throw New NotImplementedException()
    End Sub

    Public Sub New()
        InitializeComponent()
        Dim requestRepo As New RequestRepository()
        Dim requests As List(Of Requests) = requestRepo.GetAllRequests()
        requestsDataGrid.ItemsSource = requests
    End Sub

    Public Sub ApproveBtn_Click(sender As Object, e As RoutedEventArgs)
        Dim request As Requests = CType(requestsDataGrid.SelectedItem, Requests)
        request.Status = "Approved"
        Dim requestRepo As New RequestRepository()
        requestRepo.Update(request)
        Dim requests As List(Of Requests) = requestRepo.GetAllRequests()
        requestsDataGrid.ItemsSource = requests
    End Sub

    Public Sub DenyBtn_Click(sender As Object, e As RoutedEventArgs)
        Dim request As Requests = CType(requestsDataGrid.SelectedItem, Requests)
        request.Status = "Denied"
        Dim requestRepo As New RequestRepository()
        requestRepo.Update(request)
        Dim requests As List(Of Requests) = requestRepo.GetAllRequests()
        requestsDataGrid.ItemsSource = requests
    End Sub
End Class
