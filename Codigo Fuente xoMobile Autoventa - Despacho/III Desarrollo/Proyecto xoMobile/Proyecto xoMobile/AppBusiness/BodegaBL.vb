Imports System.Data
Imports Proyecto_xoMobile_Packs

Public Class BodegaBL
    Public Function DevolverEnvase(ByVal devolucion As inventarioCO) As Boolean
        Dim exito As Boolean = False
        Dim dtBodega As New BodegaDT
        Dim objProducto As New ProductoDT
        Dim dtProducto As New DataTable
        exito = dtBodega.agregarItem(devolucion)
        If Not exito Then
            Throw New Exception("No se pudo agregar el producto " + devolucion.idProducto + " al formato de devolucion")
        End If
        Return exito
    End Function
    Public Function getArticuloDevuelto(ByRef dtEnvaseDevuelto As DataTable, ByVal Inventario As inventarioCO) As Boolean
        Dim Bodega As New BodegaDT
        dtEnvaseDevuelto = Bodega.obtenerEnvaseDevuelto(Inventario)
        If dtEnvaseDevuelto.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function eliminarEnvaseDevueltoOnError(ByVal Inventario As inventarioCO) As Integer
        Dim Bodega As New BodegaDT
        Bodega.eliminarEnvaseDevuelto(Inventario)
    End Function
    Public Function devolverProducto(ByRef devolucion As inventarioCO, ByVal dtProductos As DataTable, ByVal dgDevolucion As System.Windows.Forms.DataGrid, ByVal tipoliquidacion As Integer) As Boolean

        Dim lstDetalle As New Windows.Forms.ListView

        Dim objCliente As New ClienteBL
        Dim objProducto As New ProductoBL
        Dim objInventario As New InventarioBL
        Dim objRuta As New RutaBL

        Dim Fel As New Generador
        Dim cliente As New ClienteCO
        Dim producto As New ProductoCO
        Dim OrdenVenta As New documentoCO

        Dim aplicaCargo As Boolean


        If alertaTeoricoVsFisico(dtProductos, dgDevolucion) = MsgBoxResult.Ok Then
            id_glo_cliente = id_glo_clienteGenerico
            'presentarReporte = True

            For l As Integer = 0 To dtProductos.Rows.Count() - 1
                Dim item As New ItemCO '- Necesito una nueva instancia por cada iteracion
                devolucion.estado = 0
                devolucion.unidadesTeorico = dgDevolucion.Item(l, 2)
                devolucion.unidadesResumen = dgDevolucion.Item(l, 3)
                devolucion.unidadesFisico = dgDevolucion.Item(l, 4)
                devolucion.unidadesDiferencia = dgDevolucion.Item(l, 5)
                devolucion.tTipo = tipoliquidacion
                devolucion.idProducto = dgDevolucion.Item(l, 0)
                devolucion.importe = 0

                If tipoliquidacion = 1 Then
                    devolucion.unidadesRotura = dgDevolucion.Item(l, 6)

                    '--- Agregar movimientos de inventario
                    objInventario.actualizarInventario(0, -1, "DEVOLUCION", devolucion)
                End If


                '---Generar Cargos por faltante
                If devolucion.unidadesDiferencia < 0 Then
                    aplicaCargo = True
                    devolucion.unidadesDiferencia = devolucion.unidadesDiferencia * -1

                    '--- Obtener el precio del articulo
                    producto = objProducto.getDetalleDelProducto(devolucion.idProducto)
                    cliente = objCliente.getDetalleDelCliente(id_glo_clienteGenerico)
                    Select Case producto.ttipo
                        Case 1
                            '--- Precio por explosion de materiales
                            item = objProducto.explosionarMaterial(cliente, producto, 0, devolucion.unidadesDiferencia, 0, 0, 0, item)
                        Case 2
                            '--- Precio por condicion
                            item.un_liquido = devolucion.unidadesDiferencia
                            item.un_envase = item.un_liquido
                            item.unidadesCaja = producto.unidadesCaja
                            objProducto.obtenerImporteCondicion("ENVASE", cliente, devolucion.idProducto, item, False)
                        Case 3
                            '--- Precio por condicion
                            item.un_caja = devolucion.unidadesDiferencia
                            item.unidadesCaja = producto.unidadesCaja
                            objProducto.obtenerImporteCondicion("ENVASE", cliente, devolucion.idProducto, item, True)
                    End Select


                    '--- Crear listado de items agregados
                    If Not crearListadoItemsAgregados(lstDetalle, producto, item, tipoliquidacion) Then

                    End If

                    If tipoliquidacion = 0 Then
                        '--- Liquidacion envase
                        OrdenVenta.importe = OrdenVenta.importe + item.importeEnvase + item.importeEnvaseIva + item.importeCaja + item.importeCajaIva
                    Else
                        '--- Liquidacion producto
                        OrdenVenta.importe = OrdenVenta.importe + item.importeLiquido + item.importeLiquidoIva + item.importeEnvase + item.importeEnvaseIva + item.importeCaja + item.importeCajaIva
                    End If
                    OrdenVenta.estado = 1
                End If

                '--- Grabar devolucion de bodega
                If Not DevolverEnvase(devolucion) Then
                    eliminarEnvaseDevueltoOnError(devolucion)
                    Throw New Exception("Error al procesar una de las lineas")
                End If
            Next

            '--- Crear factura por faltantes
            If aplicaCargo Then
                crearFacturaPorFaltante(cliente, OrdenVenta, lstDetalle, tipoliquidacion)
                '--- Confirmar la operacion comercial
                objRuta.confirmarOperacionComercial()

            End If

            '--- Agregar envases del producto terminado al documento de devolucion
            If tipoliquidacion = 1 Then
                agregaEnvasesPT()
            End If

            If (id_glo_fel = "X") Then

                If (Fel.generador(OrdenVenta, cliente)) Then
                    MessageBox.Show("DOCUMENTO GENERADO")
                Else
                    MessageBox.Show("NO FUE POSIBLE GENERAR DOCUMENTO ELECTRONICO, FAVOR DE REVISAR")
                End If

            End If


            MsgBox("Presione <ENTER> para imprimir el ticket de control.")
            desplegarReporte(tipoliquidacion)
            Cursor.Current = Cursors.Default
            MsgBox("El conteo en bodega se ha realizado con exito.")
            confirmaDevolucion(tipoliquidacion)
        End If
    End Function
    Public Function desplegarReporte(ByVal tipoLiquidacion As Integer) As Boolean
        Try
            Dim objReport As New printReporte

            If tipoLiquidacion = 0 Then
                If Not objReport.liquidacion_Envase() Then MsgBox("Impresora no disponible")
            Else
                If Not objReport.liquidacion_ProductoTerminado() Then MsgBox("Impresora no disponible")
            End If
            Return True

        Catch ex As Exception
            MsgBox("Error al imprimir. " + ex.Message())
            Return False
        End Try
    End Function
    Private Function alertaTeoricoVsFisico(ByVal dtProductos As DataTable, ByVal dgDevolucion As System.Windows.Forms.DataGrid) As MsgBoxResult
        Dim msg As String = ""
        Dim title As String = "Pre-Liquidacion"
        Dim style As MsgBoxStyle = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.OkCancel
        Dim response As MsgBoxResult
        Dim diferencias As Integer

        For l As Integer = 0 To dtProductos.Rows.Count() - 1
            diferencias = dgDevolucion.Item(l, 5)
            If diferencias <> 0 Then
                msg = "Continuar con diferencias en el conteo. Presione <Ok> para generar la factura y recibo de cargo."
            End If
        Next
        If msg <> "" Then
            response = MsgBox(msg, style, title)
        Else
            response = MsgBox("Realmete desea confirmar la liquidacion.", style, title)
        End If
        Return response
    End Function
    Private Function crearListadoItemsAgregados(ByRef lstDetalle As Windows.Forms.ListView, ByVal producto As ProductoCO, ByVal item As ItemCO, ByVal tipoLiquidacion As Integer) As Boolean
        '--- Agregar la linea de producto liquido

        Dim objUtilBL As New UtilitarioBL
        If producto.ttipo = 1 Then
            Dim lvi As New ListViewItem(producto.codigo.ToString())                                        '0  - Correlativo 
            lvi.SubItems.Add(producto.codigo.ToString())                                                   '1  - idProducto Base
            lvi.SubItems.Add(producto.descripcion)                                                       '2  - Descripcion producto
            lvi.SubItems.Add("L")                                                                        '3  - Tipo de producto [L,E,C]
            lvi.SubItems.Add(objUtilBL.isDecimal(item.precioVentaLiquido))                               '4  - Precio de venta
            lvi.SubItems.Add(0)                                                                          '5  - Cajas ingresadas
            lvi.SubItems.Add(item.un_liquido)                                              '6  - Unidades ingresadas
            lvi.SubItems.Add(item.importeLiquido + item.importeLiquidoIva)                               '7  - Importe
            lvi.SubItems.Add(item.un_caja)                                                               '8  - Unidades caja Total
            lvi.SubItems.Add(item.un_liquido)                                              '09 - Unidades Liquido Total
            lvi.SubItems.Add(producto.codigo.ToString())                                                   '10 - idProducto 
            lvi.SubItems.Add(item.precioUnitarioLiquido)                                                 '11 - Precio sin iva
            lvi.SubItems.Add(item.importeLiquido)                                                        '12 - importe sin iva
            lvi.SubItems.Add(producto.litrosUnidad * (item.un_liquido))                                  '13 - Litros
            lvi.SubItems.Add(item.porcentajeDesto)                                                       '14 - % Descuento
            lvi.SubItems.Add(item.importeDesto)                                                          '15 - Importe Descuento
            'lvi.SubItems.Add(0)                                                          '15 - Importe Descuento
            'lvi.SubItems.Add((item.importeLiquido + item.importeLiquidoIva) + 0)         '16 - Sub total
            lvi.SubItems.Add((item.importeLiquido + item.importeLiquidoIva) + item.importeDesto)         '16 - Sub total
            lvi.SubItems.Add((item.valorIvaDesto))                                                       '17 - Iva del descuento
            lvi.SubItems.Add(item.importeLiquido + item.importeLiquidoIva)                               '18  - Importe para igualar Despacho
            lstDetalle.Items.Add(lvi)
        End If

        '--- Agregar la linea de producto envase
        If producto.idEnvase Or producto.ttipo = 2 Then
            Dim lviE As New ListViewItem(producto.codigo)                                               '0  - Correlativo 
            lviE.SubItems.Add(producto.codigo)                                                          '1  - idProducto Base
            lviE.SubItems.Add(producto.descripcion)                                                       '2  - Descripcion producto
            lviE.SubItems.Add("E")                                                                        '3  - Tipo de producto [L,E,C]
            lviE.SubItems.Add(objUtilBL.isDecimal(item.precioVentaEnvase))                                '4  - Precio de venta
            lviE.SubItems.Add(0)                                                                          '5  - Cajas ingresadas
            lviE.SubItems.Add(item.un_envase)                                              '6  - Unidades ingresadas
            lviE.SubItems.Add(item.importeEnvase + item.importeEnvaseIva)                                 '7  - Importe
            lviE.SubItems.Add(item.un_caja)                                                               '8  - Unidades caja Total
            lviE.SubItems.Add(item.un_envase)                                              '9 - Unidades Liquido Total
            If producto.ttipo = 2 Then
                lviE.SubItems.Add(producto.codigo)                                                          '10 - idProducto 
            Else
                lviE.SubItems.Add(producto.idEnvase)                                                          '10 - idProducto 
            End If

            lviE.SubItems.Add(item.precioUnitarioEnvase)                                                  '11 - Precio sin iva
            lviE.SubItems.Add(item.importeEnvase)                                                         '12 - importe sin iva
            lviE.SubItems.Add(0)                                                                          '13 - Litros
            lviE.SubItems.Add(0)                                                                          '14 - % Descuento
            lviE.SubItems.Add(0)                                                                          '15 - Monto Descuento
            lviE.SubItems.Add(0)                                                                          '16 - Sub total
            lviE.SubItems.Add(0)                                                                          '17 - iva del descuento
            lviE.SubItems.Add(item.importeEnvase + item.importeEnvaseIva)                                 '18  - Importe para igualar Despacho
            lstDetalle.Items.Add(lviE)
        End If

        If item.un_caja > 0 Then
            Dim lviC As New ListViewItem(producto.codigo)                                               '0  - Correlativo
            lviC.SubItems.Add(producto.codigo)                                                          '1  - idProducto Base
            lviC.SubItems.Add(producto.descripcion)                                                     '2  - Descripcion producto
            lviC.SubItems.Add("C")                                                                      '3  - Tipo de producto [L,E,C]
            lviC.SubItems.Add(objUtilBL.isDecimal(item.precioVentaCaja))                                '4  - Precio de venta
            lviC.SubItems.Add("")                                                                       '5  - Cajas ingresadas
            lviC.SubItems.Add(item.un_caja)                                                             '6  - Unidades ingresadas
            lviC.SubItems.Add(item.importeCaja + item.importeCajaIva)                                   '7  - Importe
            lviC.SubItems.Add("")                                                                       '8  - Unidades caja Total
            lviC.SubItems.Add(item.un_caja)                                                             '09 - Unidades Liquido Total
            lviC.SubItems.Add(producto.codigo)                                                          '10 - idProducto 
            lviC.SubItems.Add(item.precioUnitarioCaja)                                                  '11 - Precio sin iva
            lviC.SubItems.Add(item.importeCaja)                                                         '12 - importe sin iva
            lviC.SubItems.Add(0)                                                                        '13 - Litros
            lviC.SubItems.Add(0)                                                                        '14 - % Descuento
            lviC.SubItems.Add(0)                                                                        '15 - Monto Descuento
            lviC.SubItems.Add(0)                                                                        '16 - Sub total
            lviC.SubItems.Add(0)                                                                        '17 - iva del descuento
            lviC.SubItems.Add(item.importeCaja + item.importeCajaIva)                                   '18  - Importe para igualar Despacho
            lstDetalle.Items.Add(lviC)
        End If
    End Function
    Private Function crearFacturaPorFaltante(ByVal cliente As ClienteCO, ByVal ordenVenta As documentoCO, ByVal lstdetalle As Windows.Forms.ListView, ByVal tipoLiquidacion As Integer) As Boolean

        '--- Obtener informacion complementaria para el pago de la orden de venta

        Dim ovComplemento As New documentoCO
        Dim recibo As New documentoCO
        Dim objInventario As New InventarioBL
        Dim objDocumento As New DocumentoBL
        Dim objCliente As New ClienteBL
        Dim bodega As New BodegaBL


        '--- Tipo de la orden de venta
        If tipoLiquidacion = 0 Then
            ordenVenta.ttipo = "ZTAE"
        Else
            ordenVenta.ttipo = "ZTAP"
        End If

        ordenVenta.estado = 1
        objDocumento.crearOrdenVenta(lstdetalle, ordenVenta, cliente)
        ovComplemento = objDocumento.getFactura(ordenVenta.idEncabezado)
        ordenVenta.serie = ovComplemento.serie
        ordenVenta.numero = ovComplemento.numero
        recibo.importe = ordenVenta.importe
        recibo.importeDesto = 0
        recibo.importeDestoEnv = 0
        recibo.doTipo = ordenVenta.ttipo
        recibo.estado = 1
        recibo.idEncCxcRelacionada = 0
        recibo.idEncFacturaRelacionada = ordenVenta.idEncabezado
        recibo.dgPagos = objCliente.getViasPago(True, True)
        recibo.det_dTipo = ordenVenta.ttipo
        recibo.serie = ordenVenta.serie
        recibo.numero = ordenVenta.numero
        recibo.moneda = co_glo_moneda
        recibo.dgPagos.Rows(0).Item(0) = "E"
        recibo.dgPagos.Rows(0).Item(2) = ordenVenta.importe
        recibo.dgPagos.Rows(0).Item(3) = ""
        recibo.dgPagos.Rows(0).Item(5) = "-1"
        objDocumento.crearRecibo(recibo, ordenVenta, cliente)
        bodega.imprimeFacturaDeCargo(recibo.idEncabezado, ordenVenta.idEncabezado)

        '--- Vincular la factura con el recibo
        ordenVenta = objDocumento.getFactura(ordenVenta.idEncabezado)
        ordenVenta.idReciboRelacionado = recibo.idEncabezado
        ordenVenta.fechaEmision = "fechaEmision"
        ordenVenta.fechaAnula = "null"
        ordenVenta.fechaVence = "null"
        objDocumento.actualizarFactura(ordenVenta)

        '--- Agregar movimientos de inventario
        objInventario.actualizarInventario(ordenVenta.idEncabezado, -1, ordenVenta.ttipo)

    End Function
    Private Sub agregaEnvasesPT()
        Dim bodega As New BodegaDT
        Dim dtBodega As New DataTable
        Dim inventario As New inventarioCO
        dtBodega = bodega.getEnvasePT()
        For i = 0 To dtBodega.Rows.Count - 1
            inventario.estado = dtBodega.Rows(i).Item("estado")
            inventario.idBodega = dtBodega.Rows(i).Item("idbodega")
            inventario.idProducto = dtBodega.Rows(i).Item("id_Producto")
            inventario.unidadesTeorico = dtBodega.Rows(i).Item("unidadesTeorico")
            inventario.unidadesFisico = dtBodega.Rows(i).Item("unidadesFisico")
            inventario.unidadesResumen = dtBodega.Rows(i).Item("unidadesResumen")
            inventario.unidadesDiferencia = dtBodega.Rows(i).Item("Diferencia")
            inventario.importe = dtBodega.Rows(i).Item("importe").ToString
            inventario.tTipo = dtBodega.Rows(i).Item("tTipo")
            inventario.unidadesRotura = dtBodega.Rows(i).Item("unidadesRotura")
            bodega.agregarItem(inventario)
        Next
    End Sub
    Public Sub imprimeFacturaDeCargo(ByVal idRecibo As String, ByVal idfactura As String)
        Dim objImpresion As New ImpresionBL
        Dim objDocumento As New DocumentoBL
        objImpresion.imprimeFactura(idfactura, idRecibo, False)
        objImpresion.imprimeRecibo(idRecibo, False)
        objDocumento.numeroImpresiones(idfactura, "FACTURA", "1")
        objDocumento.numeroImpresiones(idRecibo, "RECIBO", "1")
    End Sub

    Public Sub confirmaDevolucion(ByVal tipoLiquidacion As Integer)
        Dim objBitacora As New BitacoraBL

        '--- Bitacora Confirmacion 
        If tipoLiquidacion = 0 Then
            objBitacora.registrarOperacion(36, id_glo_cliente)
        Else
            objBitacora.registrarOperacion(37, id_glo_cliente)
        End If
        xo_restringeVenta = True
        xo_restringeCobro = True
        xo_restringeAnulacion = True

    End Sub
End Class

