Imports System.Data
Imports Proyecto_xoMobile_Packs

Public Class InventarioCliBL
    Shared objUtilBL As New UtilitarioBL
    Shared inventario As New InventarioCliente

#Region " INVENTARIO FISICO DEL CLIENTE "
    Public Function agregarInventarioFisico(ByVal lstDetalle As Windows.Forms.ListView) As Boolean
        Dim comItem As New ItemCO
        Dim comProducto As New ProductoCO
        Dim objProductoBL As New ProductoBL
        Dim objRlayer As New rLayerHandler

        '--- Eliminar todos los items inventariados a este clinte
        inventario.clearItems(id_glo_cliente, objRlayer)

        '--- Agregar los nuevos datos
        Try
            For i As Integer = 0 To lstDetalle.Items.Count - 1
                If Not lstDetalle.Items(i).SubItems(10).Text = "" Then
                    comItem.idProducto = lstDetalle.Items.Item(i).SubItems(10).Text
                    comProducto = objProductoBL.getDetalleDelProducto(comItem.idProducto)
                    If comProducto Is Nothing Then
                        MsgBox("Uno o mas componentes del producto " & comItem.idProducto & " no se cargo en el sistema.")
                        GoTo nextIteration
                    End If
                    comItem.NoItem = i + 1
                    comItem.idProducto = lstDetalle.Items.Item(i).SubItems(10).Text
                    comItem.cj = objUtilBL.isDecimal(lstDetalle.Items.Item(i).SubItems(5).Text)
                    comItem.un = objUtilBL.isDecimal(lstDetalle.Items.Item(i).SubItems(6).Text)
                    comItem.cantidad = objUtilBL.isDecimal(comProducto.unidadesCaja) * objUtilBL.isDecimal(comItem.cj) + objUtilBL.isDecimal(comItem.un)
                    comItem.um = "UN"
                    comItem.precio = (lstDetalle.Items.Item(i).SubItems(11).Text)
                    comItem.importe = lstDetalle.Items.Item(i).SubItems(7).Text
                    comItem.importeSinIva = (lstDetalle.Items.Item(i).SubItems(12).Text)
                    comItem.iva = co_glo_porcentajeIVA
                    comItem.tipoVenta = "I"
                    comItem.idRubro = (lstDetalle.Items.Item(i).SubItems(3).Text)
                    comItem.trqt = comItem.cj + "/" + comItem.un
                    comItem.litm = lstDetalle.Items.Item(i).SubItems(0).Text
                    comItem.estado = 0

                    '--- Agregar el item 
                    inventario.agregarItem(comItem, objRlayer)

                End If
NextIteration:

            Next
            Return True
        Catch ex As Exception
            MsgBox("No se pudo agregar un item al listado de productos." + ex.Message())
            Return False
        End Try
    End Function

    Public Function listarInventarioFisico(ByVal idCliente As String, ByVal rlayer As rLayerHandler) As DataTable
        Dim dtInventario As New DataTable
        dtInventario = inventario.getInventario(idCliente, rlayer)
        If Not rlayer.evaluaTabla(dtInventario) Then
            If rlayer.codigo = 100 Then Return Nothing
        End If
        Return dtInventario
    End Function

    Public Function llenaListado(ByVal dtInventario As DataTable, ByRef lstDetalle As Windows.Forms.ListView) As Boolean
        Dim comProducto As New ProductoCO
        Dim objProductoBL As New ProductoBL

        For i As Integer = 0 To dtInventario.Rows.Count - 1
            Dim drow As DataRow = dtInventario.Rows(i)
            comProducto = objProductoBL.getDetalleDelProducto(drow("idProducto").ToString())
            Dim lvi As New ListViewItem(drow("litm").ToString()) '0
            lvi.SubItems.Add(drow("idProducto").ToString())                     '1
            lvi.SubItems.Add(comProducto.descripcion)                           '2
            lvi.SubItems.Add(drow("idRubro").ToString())                        '3
            lvi.SubItems.Add(drow("importe").ToString())                        '4

            Try
                Dim trqt() As String = Split(drow("trqt").ToString().ToString, "/")
                lvi.SubItems.Add((trqt(0)).ToString)                            '5
                lvi.SubItems.Add((trqt(1)).ToString)                            '6
                lvi.SubItems.Add(drow("importe").ToString())                    '7
                lvi.SubItems.Add((trqt(0)).ToString)                            '8
                lvi.SubItems.Add((trqt(1)).ToString)                            '9
            Catch ex As Exception
                lvi.SubItems.Add("0")                                           '5
                lvi.SubItems.Add(drow("cantidad").ToString())                   '6
                lvi.SubItems.Add(drow("importe").ToString())                    '7
                lvi.SubItems.Add("0")                                           '8
                lvi.SubItems.Add(drow("cantidad").ToString())                   '9
            End Try
            lvi.SubItems.Add(drow("idProducto").ToString())                     '10
            lvi.SubItems.Add(drow("importeSinIva").ToString())                  '11
            lvi.SubItems.Add(drow("importeSinIva").ToString())                  '12
            lvi.SubItems.Add("0")                                               '13
            lvi.SubItems.Add("")                                                '14
            lvi.SubItems.Add("")                                                '15
            lvi.SubItems.Add("")                                                '16
            lstDetalle.Items.Add(lvi)
        Next
        Return True
    End Function


#End Region

End Class
