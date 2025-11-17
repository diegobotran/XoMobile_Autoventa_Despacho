Imports System.Data

Public Class Usuario
    '---OK
    Dim SQL_QUERY As String
    Dim SQL_RESULT As Integer
    Dim SQL_DT As DataTable
    Dim objCe As New ceClient

    Public Function getListado() As DataTable
        SQL_QUERY = " SELECT * FROM rusuario "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function
End Class
