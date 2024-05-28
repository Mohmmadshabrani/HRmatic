Imports System.Windows.Threading

Public Class TimerEmp
    Private timer As DispatcherTimer
    Private startTime As DateTime
    Private elapsedTime As TimeSpan

    Public Sub New()
        InitializeComponent()
        InitializeTimer()
    End Sub

    Private Sub InitializeTimer()
        timer = New DispatcherTimer()
        timer.Interval = TimeSpan.FromSeconds(1)
        AddHandler timer.Tick, AddressOf Timer_Tick
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs)
        elapsedTime = DateTime.Now - startTime
        CircularProgressBar.Value = elapsedTime.TotalSeconds

        txtTimer.Text = elapsedTime.ToString("mm\:ss")


        If CircularProgressBar.Value >= CircularProgressBar.Maximum Then
            timer.Stop()
        End If
    End Sub

    Private Sub StartButton_Click(sender As Object, e As RoutedEventArgs)
        startTime = DateTime.Now - elapsedTime
        timer.Start()
    End Sub

    Private Sub PauseButton_Click(sender As Object, e As RoutedEventArgs)
        timer.Stop()
    End Sub

    Private Sub StopButton_Click(sender As Object, e As RoutedEventArgs)
        timer.Stop()
        CircularProgressBar.Value = 0
        txtTimer.Text = "00:00"
        elapsedTime = TimeSpan.Zero
    End Sub
End Class
