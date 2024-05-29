Public Class EmployeeEdit
    Public Sub ShowEmployeeAddView(Optional ByVal employee As Employee = Nothing,
        Optional ByVal isEdit As Boolean = False
    )
        Dim employeeAddView As New EmployeeAdd(employee, isEdit)
        employeeAddView.ShowDialog()
    End Sub)
End Class
