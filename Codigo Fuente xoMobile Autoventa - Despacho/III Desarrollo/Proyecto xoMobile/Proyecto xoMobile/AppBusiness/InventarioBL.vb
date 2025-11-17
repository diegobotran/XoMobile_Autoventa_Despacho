Imports System.Data
Imports Proyecto_xoMobile_Packs

Public Class InventarioBL
    Dim oInventario As New Inventario
    Dim rLayer As New rLayerHandler

    Public Function confirmar(ByVal sd As String) As Boolean

        Dim rLayer As New rLayerHandler
        oInventario.confirmar(sd, rLayer)

        '--- Evaluar error y enviar mensaje
        rLayer.evaluarError()
        If rLayer.conError Then Return False Else Return True
    End Function

    Public Function actualizarInventario(ByVal idFactura As String, ByVal tipo As Integer, ByVal origen As String, Optional ByVal devolucion As inventarioCO = Nothing) As Boolean
        Dim objProducto As New ProductoBL
        Dim objDocumentoBL As New DocumentoBL
        Dim dtDetalle, dtBom As New DataTable
        Dim comItem As New ItemCO
        Dim producto As New ProductoCO

        Select Case origen
            Case Is = "ODV", "ZTAP", "CD"
                Try
                    If tipo > 0 Then origen = "ANULA-" & origen
                    dtDetalle = objDocumentoBL.getFacturaDetalle(idFactura)
                    For i As Integer = 0 To dtDetalle.Rows.Count - 1

                        comItem.idProducto = dtDetalle.Rows(i).Item("idProducto")
                        comItem.um = dtDetalle.Rows(i).Item("um")
                        comItem.cantidad = dtDetalle.Rows(i).Item("cantidad") * tipo

                        '--- Determinar si el producto tiene BOM
                        dtBom = objProducto.obtenerBom(comItem.idProducto)
                        'If Not dtBom Is Nothing Then
                        '    oInventario.setMovimiento(comItem, origen, rLayer)
                        '    For j As Integer = 0 To dtBom.Rows.Count - 1
                        '        comItem.idProducto = dtBom.Rows(j).Item("idexplosion")
                        '        comItem.um = dtDetalle.Rows(i).Item("um")
                        '        comItem.cantidad = dtDetalle.Rows(i).Item("cantidad") * tipo
                        '        oInventario.setMovimiento(comItem, origen, rLayer)
                        '    Next
                        'Else
                        oInventario.setMovimiento(comItem, origen, rLayer)
                        'End If

                        '--- Aqui cual sera la accion de control?
                        rLayer.evaluarError()
                    Next
                    Return True
                Catch ex As Exception
                    Return False
                End Try

            Case Is = "DEVOLUCION"

                producto = objProducto.getDetalleDelProducto(devolucion.idProducto)

                '--- Unidades Liquido
                comItem.idProducto = devolucion.idProducto
                comItem.um = "UN"
                comItem.cantidad = devolucion.unidadesFisico * tipo
                oInventario.setMovimiento(comItem, origen, rLayer)

                '--- Unidades Envase
                If producto.idEnvase <> "0" Then
                    comItem.idProducto = producto.idEnvase
                    comItem.um = "UN"
                    comItem.cantidad = devolucion.unidadesFisico * tipo
                    oInventario.setMovimiento(comItem, origen, rLayer)
                End If
                

                '--- Aqui cual sera la accion de control?
                rLayer.evaluarError()
        End Select
    End Function

    'Public Function consultarActual(ByVal incluirEnvase As Boolean) As DataTable
    '    Dim mensaje As New messageCollection
    '    Dim dtGeneric As New DataTable
    '    Dim dvGeneric As New DataView
    '    dtGeneric = oInventario.getInventarioActual(rLayer)
    '    rLayer.evaluaTabla(dtGeneric)
    '    If rLayer.conError Then
    '        Throw New Exception("Ya no hay mas producto en el camion.")
    '    End If
    '    dvGeneric = dtGeneric.DefaultView
    '    If incluirEnvase Then
    '        dvGeneric.RowFilter = "cantidadActual > 0 "
    '    Else
    '        dvGeneric.RowFilter = "cantidadActual > 0 and ttipo = 1"
    '    End If
    '    dtGeneric = dvGeneric.ToTable

    '    Return dtGeneric
    'End Function

    Public Function consultarEnvase(ByVal throwException As Boolean) As DataTable
        Dim dtGeneric As New DataTable
        Dim mensaje As New messageCollection
        dtGeneric = oInventario.getInventarioEnvase(rLayer)
        rLayer.evaluaTabla(dtGeneric)
        If rLayer.conError Then
            If throwException Then Throw New Exception("No se ha recibido envase.")
        End If
        Return dtGeneric
    End Function

    'Public Function consultarVenta() As DataTable
    '    Dim dtGeneric As New DataTable
    '    Dim mensaje As New messageCollection
    '    dtGeneric = oInventario.getInventarioVenta(rLayer)
    '    rLayer.evaluaTabla(dtGeneric)
    '    If rLayer.conError Then
    '        Throw New Exception("No hay ventas :(")
    '    End If
    '    Return dtGeneric
    'End Function

    Public Function consultarMovimientoLiquidacion() As DataTable
        Dim dtGeneric As New DataTable
        Dim mensaje As New messageCollection
        dtGeneric = oInventario.getMovimientos(rLayer)
        rLayer.evaluaTabla(dtGeneric)
        If rLayer.conError Then
            Throw New Exception("No hay movimientos de inventario :(")
        End If
        Return dtGeneric
    End Function

    Public Function consultarMovimientoLiquidacion2() As DataTable
        Dim dtGeneric As New DataTable
        Dim mensaje As New messageCollection
        dtGeneric = oInventario.getMovimientos2(rLayer)
        rLayer.evaluaTabla(dtGeneric)
        If rLayer.conError Then
            Throw New Exception("No hay movimientos de inventario :(")
        End If
        Return dtGeneric
    End Function

End Class
