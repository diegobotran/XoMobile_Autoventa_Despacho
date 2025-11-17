Imports System.Data

'--- DEPURADA
Public Class BitacoraBL

    Public Function registrarOperacion(ByVal Operacion As Integer, ByVal idCliente As String, Optional ByVal lat As String = "0", Optional ByVal longi As String = "0") As Boolean
        Dim oBitacora As New Bitacora
        Dim resultado As Integer
        '--- Default values if no GPS is captured.
        If lat = "" Then lat = "0"
        If longi = "" Then longi = "0"
        If Len(lat) > 15 Then lat = lat.Substring(0, 15)
        If Len(longi) > 15 Then longi = longi.Substring(0, 15)

        '--- Identificar si la operacion involucra al cliente generico
        If idCliente = 0 Then idCliente = id_glo_clienteGenerico

        '--- Almacenar el registro de bitacora
        resultado = oBitacora.agregar(Operacion.ToString(), idCliente, lat, longi)
        If resultado = 1 Then
            Return True
        Else
            Throw New Exception("ERROR: No se pudo grabar la operación en la bitacora.")
            Return False
        End If
    End Function



    Public Function isOperacionRealizada_xo(ByVal tOperacion As String) As Boolean
        Dim oBitacora As New Bitacora
        Dim dtBitacora As New DataTable
        dtBitacora = oBitacora.isOperacionRealizada_xo(tOperacion)
        If dtBitacora.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

End Class
