Imports System.Data
Imports Proyecto_xoMobile_Packs


Public Class InventarioCliente

    Dim SQL_QUERY As String
    Dim SQL_RESULT As Integer
    Dim SQL_DT As DataTable
    Shared objCe As New ceClient

#Region " INVENTARIO DEL CLIENTE "
    Public Function agregarItem(ByVal comItem As ItemCO, ByVal rlayer As rLayerHandler) As Integer
        With comItem
            SQL_QUERY = "" _
        & " INSERT INTO [CINVENTARIO_FISICO]([idRuta],[idCliente],[idProducto],[um],[cantidad],[importeSinIva],[importe],[iva],[fechaEmision],[estado],[idusuario],[idRubro],[trqt],[litm])  " _
        & " VALUES ('" & id_glo_ruta.ToString & "','" & id_glo_cliente.ToString() & "','" & .idProducto & "','" & .um & "','" & .cantidad & "','" & .importeSinIva & "','" & .importe & "','" & .iva & "',getdate(),'" & .estado & "','" & id_glo_usuario.ToString & "','" & .idRubro & "','" & .trqt & "','" & .litm & "')"
        End With
        SQL_RESULT = objCe.SetExecute(SQL_QUERY, rlayer)
        Return SQL_RESULT
    End Function

    Public Function getInventario(ByVal idCliente As String, ByVal rlayer As rLayerHandler)
        SQL_QUERY = _
        "SELECT * FROM [CINVENTARIO_FISICO] WHERE idCliente =  " & idCliente
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
        Return SQL_DT
    End Function

    Public Function clearItems(ByVal idcliente As String, ByVal rlayer As rLayerHandler) As Integer
        SQL_QUERY = "DELETE FROM [CINVENTARIO_FISICO] WHERE idCliente = " & idcliente
        SQL_RESULT = objCe.SetExecute(SQL_QUERY, rlayer)
        Return SQL_RESULT
    End Function

#End Region

End Class
