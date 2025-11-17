Imports System.Data
Imports Proyecto_xoMobile_Packs


Public Class DespachoBL

    Dim oDespacho As New DespachoDT


    Public Function getDespachos(Optional ByVal idCliente As String = "idCliente", Optional ByRef rlayer As rLayerHandler = Nothing) As DataTable
        Dim dtDespacho As New DataTable
        If (id_glo_aplicacion = "cambio") Then
            dtDespacho = oDespacho.getDespachosCambio(idCliente, rlayer)
        Else
            dtDespacho = oDespacho.getDespachos(idCliente, rlayer)
        End If

        rlayer.evaluaTabla(dtDespacho)
        rlayer.evaluarError(False)
        Return dtDespacho
    End Function

    Public Function marcarPedidoDespacho(ByVal idDespacho As String, ByVal despachado As Boolean, ByVal cliente As ClienteCO) As rLayerHandler

        If tipoRuta <> "16" Then Return Nothing
        Dim rLayer As New rLayerHandler
        oDespacho.updatePedidoDespachado(idDespacho, rLayer, despachado)

        '--- Evaluar error y enviar mensaje
        rLayer.evaluarError()

        '--- Crear un motivo unico para despachos no realizados en el proceso de atencion normal
        If co_glo_despacho Then
            actualizarMotivoDespacho(cliente, "-1", idDespacho)
        End If

        Return rLayer
    End Function

    Public Function actualizarMotivoDespacho(ByVal cliente As ClienteCO, ByVal idMotivo As String, Optional ByVal idDespacho As String = Nothing) As rLayerHandler

        Dim rLayer As New rLayerHandler
        If idDespacho Is Nothing Then
            oDespacho.updateTodosMotivoDespacho(cliente.codigo, rLayer, idMotivo)
        Else
            oDespacho.updateMotivoDespacho(cliente.codigo, rLayer, idMotivo, idDespacho)
        End If


        '--- Evaluar error y enviar mensaje
        rLayer.evaluarError()

        Return rLayer
    End Function

    Public Function confirmarDespachos() As rLayerHandler
        Dim rLayer As New rLayerHandler
        Dim oBitacora As New BitacoraBL

        '--- Actualiza el estado
        oDespacho.setConfirmarDespacho(rLayer)

        '--- Registra si hubo algun mensaje de error
        rLayer.evaluarError()
        If rLayer.codigo = 0 Then

            '--- Actualiza la operacion en bitacora - [Despacho Confirmado]
            oBitacora.registrarOperacion(48, id_glo_cliente)
            co_glo_confirma_despacho = True
        End If
        Return rLayer
    End Function

    Public Function ObtenerPedidoById(ByRef comDocumento As documentoCO) As rLayerHandler
        Dim rLayer As New rLayerHandler
        Dim dtDespacho As New DataTable
        Dim oUtil As New UtilitarioBL

        '--- Obtener el raw data del pedido
        dtDespacho = oDespacho.getPedidoById(comDocumento.idPedido, rLayer)

        '--- Evaluar error y enviar mensaje
        rLayer.evaluarError()
        If Not dtDespacho Is Nothing Then
            If dtDespacho.Rows.Count > 0 Then
                rLayer.codigo = 0

                'prueba de datos
                'MsgBox(comDocumento.isContado)


                '--- Asignar la informacion raw data al objeto 
                comDocumento.idEncabezado = dtDespacho.Rows(0).Item("id_EncFactura").ToString
                comDocumento.idCliente = dtDespacho.Rows(0).Item("idCliente").ToString
                comDocumento.importe = dtDespacho.Rows(0).Item("importe").ToString
                comDocumento.moneda = dtDespacho.Rows(0).Item("moneda").ToString
                comDocumento.importeDesto = oUtil.isDecimal(dtDespacho.Rows(0).Item("importeDesto").ToString)
                comDocumento.ttipo = dtDespacho.Rows(0).Item("ttipo").ToString
                comDocumento.condicion = dtDespacho.Rows(0).Item("condicion").ToString
                comDocumento.idPedido = dtDespacho.Rows(0).Item("id_Pedido").ToString
                comDocumento.noEntrega = dtDespacho.Rows(0).Item("noEntrega").ToString
                comDocumento.noPedido = dtDespacho.Rows(0).Item("noPedido").ToString
                Return rLayer
            Else
                rLayer.codigo = 1
                rLayer.texto = "Ocurrio un error al tratar de recuperar el pedido. "
            End If
        Else
            rLayer.codigo = 1
            rLayer.texto = "No se encontro el codigo " & comDocumento.idPedido & " de pedido en el listado de despachos."
        End If

        '--- Evaluar error y enviar mensaje
        rLayer.evaluarError()
        Return rLayer
    End Function

    Public Function ObtenerPedidoDetalleById(ByVal idPedido As String) As DataTable
        Dim oDespacho As New DespachoDT
        Dim dtItemPedido As New DataTable
        Dim rlayer As New rLayerHandler
        dtItemPedido = oDespacho.getPedidoDetalleById(idPedido, rlayer)
        Return dtItemPedido
    End Function

    Public Function crearListaAgregados(ByVal dtInventario As DataTable, ByRef listaDetalle As Windows.Forms.ListView, ByVal cliente As ClienteCO, Optional ByRef comDespacho As documentoCO = Nothing) As Boolean
        '------------------------------------------------------------------------------------------------------------------------
        '-DESCRIPCION: En esta funcion se pobla la lista de articulos agregados que sirve como detalle de articulos del despacho
        '-             Tambien si el articulo cambio de cantidad entonces se recalcula el precion y los descuentos
        '------------------------------------------------------------------------------------------------------------------------
        Dim comProducto As New ProductoCO
        Dim objProductoBL As New ProductoBL
        Dim objDocumento As New DocumentoBL
        Dim objUtil As New UtilitarioBL


        comDespacho.importePedido = 0
        comDespacho.importeDesto = 0
        comDespacho.importeDespacho = 0
        comDespacho.destoDespacho = 0
        comDespacho.destoPedido = 0

        '--- Limpiar lista de articulos agregados
        listaDetalle.Items.Clear()


        '--- Recorrer todas las filas del detalle del pedido
        For i As Integer = 0 To dtInventario.Rows.Count - 1

            Dim item As New ItemCO
            Dim drow As DataRow = dtInventario.Rows(i)

            '--- Definir el IVA
            item.iva = 1 + co_glo_porcentajeIVA

            '--- Obtener una instancia del producto
            comProducto = objProductoBL.getDetalleDelProducto(drow("idProducto").ToString())

            '--- Descuento del pedido
            item.importeDesto = objUtil.isDecimal(drow("importeDesto").ToString()) + objUtil.isDecimal(drow("importeDestoPP").ToString())
            item.porcentajeDesto = objUtil.isDecimal(drow("porcentajeDesto").ToString()) + objUtil.isDecimal(drow("porcentajeDestoPP").ToString())
            item.importeDestoFEL = objUtil.isDecimal(drow("importeDesto").ToString())
            item.porcentajeDestoFEL = objUtil.isDecimal(drow("porcentajeDesto").ToString())
            item.unidadesCaja = comProducto.unidadesCaja

            Dim lvi As New ListViewItem(drow("litm").ToString())                '0

            '--- Solo mostrar codigo y descripcion de los productos liquido
            If drow("idRubro").ToString() = "L" Then
                lvi.SubItems.Add(drow("idProducto").ToString())                 '1
                lvi.SubItems.Add(Trim(comProducto.descripcion))                 '2
            Else
                lvi.SubItems.Add("")                                            '1
                lvi.SubItems.Add("")                                            '2
            End If

            lvi.SubItems.Add(drow("idRubro").ToString())                        '3
            lvi.SubItems.Add(drow("precio").ToString())                         '4
            comDespacho.importePedido += objUtil.isDecimal(drow("importe").ToString())

            '--- Desglosar cajas y unidades
            Try
                Dim trqt() As String = Split(drow("trqt").ToString().ToString, "/")
                lvi.SubItems.Add(trqt(0).ToString)                              '5
                lvi.SubItems.Add(trqt(1).ToString)                              '6
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
            lvi.SubItems.Add(drow("precio").ToString())                         '11
            lvi.SubItems.Add(drow("precio").ToString())                         '12
            lvi.SubItems.Add(drow("litros").ToString())                         '13            
            lvi.SubItems.Add(item.porcentajeDesto)                              '14
            lvi.SubItems.Add(item.importeDesto)                                 '15
            lvi.SubItems.Add(objUtil.isDecimal(drow("importe").ToString()) + objUtil.isDecimal(item.importeDesto))  '16
            lvi.SubItems.Add(drow("canDespacho").ToString())                                                        '17

            '--- Acumular Descuento del pedido 
            comDespacho.destoPedido += objUtil.isDecimal(item.importeDesto)

            '--- Recalcular precio
            Dim unDespacho As String = "0"
            Dim cjDespacho As String = "0"

            '--- Si el pedido es diferente entonces recalcula precios
            If drow("isDiferente").ToString() <> "0" Then
                Select Case drow("idRubro").ToString()

                    Case Is = "L"

                        Dim comfactura As New documentoCO

                        '--- Precio del producto liquido
                        item.un_liquido = drow("canDespacho")
                        item.unidadesCaja = comProducto.unidadesCaja
                        unDespacho = item.un_liquido
                        objProductoBL.obtenerImporteCondicion("PRECIO", cliente, drow("idProducto").ToString(), item)

                        '--- Aqui tengo que poner DESCTA o DESC segun corresponda la condicion
                        'quiza un select a zcampos2 con la condicion en comfactura.condicion

                        objProductoBL.recalculaDescuento("DESC", cliente, comProducto, item, comDespacho, comfactura.porcentajeDestoAdicional)
                        lvi.SubItems.Add((item.importeLiquido + item.importeLiquidoIva) + item.importeDesto)         '18
                        lvi.SubItems.Add(item.importeLiquido)                                                        '19
                        objProductoBL.convertirUnidadesAcajas(comProducto, unDespacho, cjDespacho)
                        lvi.SubItems.Add(cjDespacho & "/" & unDespacho)                                              '20
                        lvi.SubItems.Add(item.precioUnitarioLiquido * (item.iva))                                    '21
                        lvi.SubItems.Add((item.importeLiquido + item.importeLiquidoIva))                             '22
                        lvi.SubItems.Add(item.importeDesto)                                                          '23


                    Case Is = "E"
                        item.un_liquido = drow("canDespacho")
                        item.unidadesCaja = comProducto.unidadesCaja
                        unDespacho = item.un_liquido
                        objProductoBL.obtenerImporteCondicion("ENVASE", cliente, drow("idProducto").ToString(), item)
                        lvi.SubItems.Add((item.importeEnvase + item.importeEnvaseIva))                               '18 
                        lvi.SubItems.Add(item.importeEnvase)                                                         '19
                        objProductoBL.convertirUnidadesAcajas(comProducto, unDespacho, cjDespacho)
                        lvi.SubItems.Add(cjDespacho & "/" & unDespacho)                                              '20
                        lvi.SubItems.Add(item.precioUnitarioEnvase * (item.iva))                                     '21
                        lvi.SubItems.Add((item.importeEnvase + item.importeEnvaseIva))                               '22
                        lvi.SubItems.Add("")                                                                         '23


                    Case Is = "C"
                        item.un_caja = drow("canDespacho")
                        unDespacho = item.un_caja
                        objProductoBL.obtenerImporteCondicion("ENVASE", cliente, drow("idProducto").ToString(), item, True)
                        lvi.SubItems.Add((item.importeCaja + item.importeCajaIva))                                  '18
                        lvi.SubItems.Add(item.importeCaja)                                                          '19
                        objProductoBL.convertirUnidadesAcajas(comProducto, unDespacho, cjDespacho)
                        lvi.SubItems.Add(cjDespacho & "/" & unDespacho)                                             '20
                        lvi.SubItems.Add(item.precioUnitarioCaja * (item.iva))                                      '21
                        lvi.SubItems.Add((item.importeCaja + item.importeCajaIva))                                  '22
                        lvi.SubItems.Add("")                                                                        '23
                    Case Else
                End Select

                '--- Acumular el descuento del despacho No importa si es PP o contado.
                comDespacho.destoDespacho += item.importeDesto
            Else

                '--- No hay diferencia de unidades entre el pedido y el despacho
                lvi.SubItems.Add(drow("importe").ToString() + objUtil.isDecimal(item.importeDesto))   '18
                lvi.SubItems.Add(drow("importeSinIva").ToString)                                      '19
                objProductoBL.convertirUnidadesAcajas(comProducto, unDespacho, cjDespacho)
                lvi.SubItems.Add(cjDespacho & "/" & unDespacho)                                       '20
                lvi.SubItems.Add(drow("precio").ToString())                                           '21
                lvi.SubItems.Add(drow("importe").ToString())                                          '22
                lvi.SubItems.Add(objUtil.isDecimal(item.importeDesto))                                '23

            End If

            '--- Calcular el importe del despacho
            comDespacho.importeDespacho += objUtil.isDecimal(item.importeLiquido + item.importeLiquidoIva + item.importeEnvase + item.importeEnvaseIva + item.importeCaja + item.importeCajaIva)
            listaDetalle.Items.Add(lvi)

            '--- Valores que dependen del pedido diferente
            If drow("isDiferente").ToString() <> "0" Then
                listaDetalle.Items(listaDetalle.Items.Count - 1).BackColor = xoWarning
                comDespacho.importeDesto = objUtil.isDecimal(comDespacho.importeDesto) + objUtil.isDecimal(item.importeDesto)
                comDespacho.importeDestoPP = comDespacho.importeDesto
            Else
                comDespacho.importeDesto = objUtil.isDecimal(comDespacho.importeDesto) + objUtil.isDecimal(item.importeDesto)
                comDespacho.importeDestoPP = comDespacho.importeDesto '--'
            End If
        Next
        Return True
    End Function

    Public Function crearListaProductos(ByVal dtProductos As DataTable, ByRef lstProductos As Windows.Forms.ListView, ByVal cliente As ClienteCO, ByVal comPedido As documentoCO) As Boolean

        '--- Agregar filas a la lista
        lstProductos.Items.Clear()
        Dim objProducto As New ProductoBL
        Dim comProducto As New ProductoCO
        Dim objDocumento As New DocumentoBL
        Dim comfactura As New documentoCO

        'Dim codigo = New ColumnHeader()         '0
        'Dim descripcion = New ColumnHeader()    '1
        'Dim pedido = New ColumnHeader()         '2
        'Dim despacho = New ColumnHeader()       '3
        'Dim importeP = New ColumnHeader()       '4
        'Dim importeD = New ColumnHeader()       '5
        'Dim descuentoP = New ColumnHeader()     '6
        'Dim descuentoD = New ColumnHeader()     '7

        For i As Integer = 0 To dtProductos.Rows.Count - 1
            Dim drow As DataRow = dtProductos.Rows(i)
            Dim unidades As String = drow("canDespacho").ToString()
            Dim cajas As String = "0"
            'Dim lvi As New ListViewItem(drow("codigo").ToString())
            Dim lvi As New ListViewItem(drow("id_producto").ToString())
            Dim item As New ItemCO

            comProducto = objProducto.getDetalleDelProducto(drow("idProducto").ToString())  '-- 0
            lvi.SubItems.Add(Trim(drow("descripcion").ToString()))                          '-- 1
            lvi.SubItems.Add(Trim(drow("trqt").ToString()))                                 '-- 2
            objProducto.convertirUnidadesAcajas(comProducto, unidades, cajas)
            lvi.SubItems.Add(cajas & "/" & unidades)                                        '-- 3


            '--- Precio del producto liquido pedido
            lvi.SubItems.Add(drow("importe").ToString())                                    '-- 4

            '--- Precio del producto liquido despacho [Cuando es diferente]
            If comPedido.isDiferente = True Then
                item.un_liquido = drow("canDespacho").ToString()
                item.unidadesCaja = comProducto.unidadesCaja
                objProducto.obtenerImporteCondicion("PRECIO", cliente, drow("idProducto").ToString(), item)
                lvi.SubItems.Add((item.importeLiquido + item.importeLiquidoIva))            '-- 5
            Else
                lvi.SubItems.Add("0")                                                       '-- 5
            End If


            '--- Descuento del pedido
            If comPedido.isContado Then
                lvi.SubItems.Add(drow("descuento").ToString())                                  '--6
            Else
                lvi.SubItems.Add(comPedido.importeDestoPP)
            End If


            '--- Descuento del despacho [Cuando es diferente]
            If comPedido.isDiferente = True Then
                comfactura.importeLiquido = item.importeLiquido + item.importeLiquidoIva
                If drow("tipoPago").ToString = "CONTADO" Then
                    '--- Agregar si aplica descuento de contado
                    objDocumento.aplicaDescuentoContado(cliente, comfactura, False)
                Else

                    '--- Colocar como descuento el DPP
                    item.importeDesto = comPedido.importeDestoPP
                End If


                '---Recalcular el descuento
                objProducto.descuentoCategoria("DESC", cliente, comProducto, item, comfactura.porcentajeDestoAdicional)
            End If
            lvi.SubItems.Add(item.importeDesto)
            lstProductos.Items.Add(lvi)                                                         '--7

            '--- Colorea lineas diferentes 
            If (drow("isDiferente").ToString <> "0") Then
                lstProductos.Items(lstProductos.Items.Count - 1).ImageIndex = 2
                lstProductos.Items(lstProductos.Items.Count - 1).BackColor = xoWarning
            End If
        Next
        lstProductos.Items.Item(0).Selected() = True
    End Function
End Class
