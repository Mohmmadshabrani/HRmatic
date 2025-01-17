﻿Imports System.Runtime.InteropServices
Imports System.Windows.Interop

Public Class EmpDashboard
    Private userId As Integer
    Public Sub New(userId As Integer)
        InitializeComponent()
        Me.userId = userId
    End Sub
    Public Shared Function SendMessage(hWnd As IntPtr, wMsg As Integer, wParam As Integer, lParam As Integer) As IntPtr
        End Function

    Private Sub pnl_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        Dim helper As New WindowInteropHelper(Me)
        SendMessage(helper.Handle, 161, 2, 0)
    End Sub

    Private Sub pnl_MouseEnter(sender As Object, e As MouseEventArgs)
            Me.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight
        End Sub
    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub
    Private Sub btnMaximize_Click(sender As Object, e As RoutedEventArgs)
            Me.WindowState = WindowState.Maximized
        End Sub

        Private Sub btnMinimize_Click(sender As Object, e As RoutedEventArgs)
            Me.WindowState = WindowState.Minimized
        End Sub




    Private Sub ShowTimer(sender As Object, e As RoutedEventArgs)
        Dim TimerEmp As New TimerEmp(userId)
        ContentGrid.Children.Clear()
        ContentGrid.Children.Add(TimerEmp)
    End Sub
    Private Sub ShowRequest(sender As Object, e As RoutedEventArgs)
        Dim RequestEmployee As New RequestEmployee(userId)
        ContentGrid.Children.Clear()
        ContentGrid.Children.Add(RequestEmployee)
    End Sub





End Class
