Imports System.Data

Public Class Bitacora
    Dim SQL_QUERY As String
    Dim SQL_RESULT As Integer
    Dim SQL_DT As DataTable
    Dim objCe As New ceClient

    '---ok
    Public Function agregar(ByVal idOperacion As String, ByVal idCliente As String, ByVal latitud As String, ByVal longitud As String) As Integer
        SQL_QUERY = " INSERT INTO [CBITACORA]([idRuta],[idCliente],[idusuario],[tOperacion],[fechaEmision],[latitud],[longitud])  " _
        + "VALUES (" + id_glo_ruta.ToString() + "," + idCliente + "," + id_glo_usuario.ToString() + "," + idOperacion + ",GETDATE()" + ",'" + latitud + "','" + longitud + "')"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function isOperacionRealizada_xo(ByVal toperacion As String) As DataTable
        SQL_QUERY = " SELECT * FROM [CBITACORA] WHERE tOperacion =  " + toperacion
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function
End Class
