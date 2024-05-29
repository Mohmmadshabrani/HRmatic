Imports System.Globalization
Public Class StatusToVisibilityConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
        Dim status As String = TryCast(value, String)
        Dim condition As String = TryCast(parameter, String)

        If Not String.IsNullOrEmpty(status) AndAlso Not String.IsNullOrEmpty(condition) Then
            If status.Equals(condition, StringComparison.OrdinalIgnoreCase) Then
                Return Visibility.Visible
            End If
        End If

        Return Visibility.Collapsed
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New NotSupportedException()
    End Function
End Class
