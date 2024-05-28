Imports System.Windows.Threading

Public Class TimerEmp
    Private timer As DispatcherTimer
    Private startTime As DateTime
    Private pausedDuration As TimeSpan
    Private pauseStartTime As DateTime
    Private empId As Integer
    Private isPaused As Boolean

    Public Sub New(empId As Integer)
        InitializeComponent()
        Me.empId = empId
        InitializeTimer()
        pausedDuration = TimeSpan.Zero
        isPaused = False
    End Sub

    Private Sub InitializeTimer()
        timer = New DispatcherTimer()
        timer.Interval = TimeSpan.FromSeconds(1)
        AddHandler timer.Tick, AddressOf Timer_Tick
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs)
        Dim elapsedTime As TimeSpan
        If isPaused Then
            elapsedTime = pauseStartTime - startTime - pausedDuration
        Else
            elapsedTime = DateTime.Now - startTime - pausedDuration
        End If
        CircularProgressBar.Value = elapsedTime.TotalSeconds
        txtTimer.Text = elapsedTime.ToString("mm\:ss")
    End Sub

    Private Sub StartButton_Click(sender As Object, e As RoutedEventArgs)
        If isPaused Then
            pausedDuration += DateTime.Now - pauseStartTime
        Else
            startTime = DateTime.Now
            pausedDuration = TimeSpan.Zero
        End If
        isPaused = False
        SaveTimerRecord("start")
        timer.Start()

        StartButton.IsEnabled = False
        StartButton.Visibility = True




    End Sub

    Private Sub PauseButton_Click(sender As Object, e As RoutedEventArgs)
        If Not isPaused Then
            pauseStartTime = DateTime.Now
            isPaused = True
            timer.Stop()
            SaveTimerRecord("pause")
            StartButton.IsEnabled = True
            StartButton.Visibility = False


        End If
    End Sub

    Private Sub StopButton_Click(sender As Object, e As RoutedEventArgs)
        If Not isPaused Then
            pausedDuration += DateTime.Now - startTime
        End If
        timer.Stop()
        SaveTimerRecord("stop")
        CircularProgressBar.Value = 0
        txtTimer.Text = "00:00"
        pausedDuration = TimeSpan.Zero
        isPaused = False
    End Sub

    Private Sub SaveTimerRecord(status As String)
        Dim duration As TimeSpan = If(status = "start", TimeSpan.Zero, pausedDuration)
        Dim userRepo As New UserRepository()
        userRepo.SaveTimerRecord(empId, DateTime.Now, status)



    End Sub
End Class
