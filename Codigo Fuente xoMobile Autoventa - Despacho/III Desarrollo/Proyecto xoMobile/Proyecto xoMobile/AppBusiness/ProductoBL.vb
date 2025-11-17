Imports System.Data
Imports Proyecto_xoMobile_Packs

'--- ok
Public Class ProductoBL

    Dim objProductoDT As New ProductoDT
    Dim INVENTARIO As New InventarioBL
    Dim objUtilBL As New UtilitarioBL
    Dim objRlayer As New rLayerHandler
    Dim dtGeneric As New DataTable


    Public Function ObtenerListadoProductos(ByVal tipoLista As String, ByVal cliente As ClienteCO, Optional ByVal idDespacho As String = "") As DataTable
        Dim objRlayer As New rLayerHandler
        Dim dtProductos As New DataView
        Select Case tipoLista
            Case Is = "venta"

                '--- Determinar si es necesario cargar de la base de datos
                'If (glo_productos_venta_dt Is Nothing Or glo_productos_venta_dt.Rows.Count = 0) Then
                If (glo_productos_venta_dt.Rows.Count <= 0) Then
                    glo_productos_venta_dt = objProductoDT.getListadoVenta(objRlayer)
                    If Not objRlayer.evaluaTabla(glo_productos_venta_dt) Then
                        If objRlayer.codigo = 100 Then MsgBox(objRlayer.texto)
                        Return Nothing
                    End If
                    filtrarLocks(glo_productos_venta_dt, glo_productos_locks_dt)

                End If
                'dtProductos = glo_productos_venta_dt.DefaultView
                'dtProductos.RowFilter = ("locked = 0")
                'glo_productos_venta_dt = dtProductos.Copy
                Return glo_productos_venta_dt

            Case Is = "despacho"
                If id_glo_aplicacion = "cambio" Then
                    glo_productos_venta_dt = objProductoDT.getListadoCambio(idDespacho, objRlayer)
                Else
                    glo_productos_venta_dt = objProductoDT.getListadoDespacho(idDespacho, objRlayer)
                End If

                If Not objRlayer.evaluaTabla(glo_productos_venta_dt) Then
                    If objRlayer.codigo = 100 Then MsgBox(objRlayer.texto)
                    Return Nothing
                End If
                Return glo_productos_venta_dt

            Case Is = "Inventario"
                Dim objCliente As New ClienteBL

                glo_dt_productos = objProductoDT.getListadoInventarioGiro(objRlayer)
                If objRlayer.evaluaTabla(glo_dt_productos) Then

                Else
                    glo_dt_productos = objProductoDT.getListadoInventario(objRlayer)
                    If Not objRlayer.evaluaTabla(glo_dt_productos) Then

                        '--- Obtener el listado completo de productos para inventario
                        'glo_dt_productos = objProductoDT.getListadoInventarioDeprecated(objRlayer)
                        If Not objRlayer.evaluaTabla(glo_dt_productos) Then
                            If objRlayer.codigo = 100 Then MsgBox(objRlayer.texto)
                            Return Nothing
                        End If
                    Else

                        '--- Filtrar por segmento
                        glo_dt_productos = objCliente.filtrarSegmento(glo_dt_productos, cliente)
                        If Not objRlayer.evaluaTabla(glo_dt_productos) Then
                            glo_dt_productos = objProductoDT.getListadoInventarioDeprecated(objRlayer)
                            If Not objRlayer.evaluaTabla(glo_dt_productos) Then
                                If objRlayer.codigo = 100 Then MsgBox(objRlayer.texto)
                                Return Nothing
                            End If
                        End If
                    End If
                End If
                Return glo_dt_productos
            Case Else
                Return Nothing
        End Select
    End Function

    'Public Sub getDescuento(ByVal ttipo As String, ByVal idCliente As String, ByVal idProducto As String, ByRef item As ItemCO, ByVal dtAllzcampos As DataTable)

    '    Dim dtSecuenciaAcceso, dtCliente, dtAlterCliente, dtProducto, dtAlterProducto, dtZcampos As New DataTable
    '    Dim oCliente As New ClienteDT
    '    Dim oProducto As New ProductoDT
    '    Dim tabla, acc As String
    '    Dim dvZcampos As New DataView

    '    '--- Peor de los casos (Ejecutar el procedimiento y que los valores no cambien)
    '    item.porcentajeDesto = 0
    '    item.importeDesto = 0
    '    item.precioUnitario = 0

    '    dtSecuenciaAcceso = objProductoDT.getSecuenciaAcceso(ttipo)
    '    dtCliente = oCliente.getDetalleZsearch(idCliente)
    '    dtProducto = oProducto.getDetalleZsearch(idProducto)


    '    For i As Integer = 0 To dtSecuenciaAcceso.Rows.Count() - 1
    '        acc = dtSecuenciaAcceso.Rows(i).Item("acc")
    '        tabla = dtSecuenciaAcceso.Rows(i).Item("tabla")
    '        dvZcampos = dtAllzcampos.DefaultView
    '        dvZcampos.RowFilter = ("secuencia = " + acc + "and tabla = " + tabla)
    '        dtZcampos = dvZcampos.ToTable

    '        'Por cada secuencia nueva reiniciar los datos del cliente y producto
    '        dtAlterCliente = dtCliente.Copy
    '        dtAlterProducto = dtProducto.Copy

    '        If dtZcampos.Rows.Count() > 0 Then

    '            '--- Recorrido de columnas
    '            For j As Integer = 0 To dtZcampos.Columns.Count() - 1

    '                'Determinar campos filtro nullo
    '                If dtZcampos.Rows(0).Item(j).ToString() = Nothing Then

    '                    'Buscar esta columna en atributos del cliente
    '                    For k As Integer = 0 To dtCliente.Columns.Count() - 1
    '                        If dtCliente.Columns(k).ColumnName.ToString = dtZcampos.Columns(j).ColumnName.ToString Then
    '                            dtAlterCliente.Rows(0).Item(k) = "%"
    '                            k = dtCliente.Columns.Count()
    '                        End If
    '                    Next

    '                    'Buscar esta columna en atributos del producto
    '                    For k As Integer = 0 To dtProducto.Columns.Count() - 1
    '                        If dtProducto.Columns(k).ColumnName.ToString = dtZcampos.Columns(j).ColumnName.ToString Then
    '                            dtAlterProducto.Rows(0).Item(k) = "%"
    '                            k = dtProducto.Columns.Count()
    '                        End If
    '                    Next
    '                End If
    '            Next
    '            dvZcampos = dtZcampos.DefaultView
    '            Dim filtro As String = "id_cliente like'%" + dtAlterCliente.Rows(0).Item("id_cliente").ToString() + "%' and id_producto like '%" + dtAlterProducto.Rows(0).Item("id_producto").ToString() + "%' and categoriaP like '" + Trim(dtAlterProducto.Rows(0).Item("categoriaP").ToString()) + "' and categoriaC like '" + Trim(dtAlterCliente.Rows(0).Item("categoriaC").ToString()) + "'"
    '            dvZcampos.RowFilter = (filtro)

    '            If dvZcampos.ToTable.Rows.Count > 0 Then
    '                If ttipo = "DESC" Or ttipo = "DESCTA" Then
    '                    item.porcentajeDesto = objUtilBL.isDecimal(dvZcampos.ToTable.Rows(0).Item("importe"))
    '                Else
    '                    item.precioUnitario = objUtilBL.isDecimal(dvZcampos.ToTable.Rows(0).Item("importe").ToString() / dvZcampos.ToTable.Rows(0).Item("factor").ToString())
    '                    item.precioUm = dvZcampos.ToTable.Rows(0).Item("um").ToString()
    '                End If
    '            End If
    '        End If
    '    Next
    'End Sub

    Public Function getDetalleDelProducto(ByVal idProducto As String) As ProductoCO
        Dim dtProducto As New DataTable
        Dim comProducto As New ProductoCO
        dtProducto = objProductoDT.getDetalle(idProducto)
        If dtProducto.Rows.Count > 0 Then
            comProducto.codigo = (dtProducto.Rows(0).Item("id_producto").ToString)
            comProducto.idEnvase = dtProducto.Rows(0).Item("idEnvase").ToString
            comProducto.idCaja = dtProducto.Rows(0).Item("idCaja").ToString
            comProducto.descripcion = dtProducto.Rows(0).Item("descripcion").ToString
            comProducto.categoria = dtProducto.Rows(0).Item("categoria").ToString
            comProducto.litrosUnidad = dtProducto.Rows(0).Item("litrosUnidad").ToString
            comProducto.unidadesCaja = dtProducto.Rows(0).Item("unidadesCaja").ToString
            comProducto.ventaContado = dtProducto.Rows(0).Item("ventaContado").ToString
            comProducto.ttipo = dtProducto.Rows(0).Item("ttipo").ToString
            comProducto.cantidadActual = dtProducto.Rows(0).Item("cantidadActual").ToString
            comProducto.cantidadActualE = dtProducto.Rows(0).Item("cantidadActualE").ToString
            comProducto.cantidadActualC = dtProducto.Rows(0).Item("cantidadActualC").ToString
        Else
            comProducto.codigo = idProducto
            comProducto.idEnvase = ""
            comProducto.idCaja = ""
            comProducto.descripcion = "Producto no encontrado en el maestro"
            comProducto.categoria = ""
            comProducto.litrosUnidad = "0"
            comProducto.unidadesCaja = "0"
            comProducto.ventaContado = ""
            comProducto.ttipo = ""
            comProducto.cantidadActual = 0
            comProducto.cantidadActualE = 0
            comProducto.cantidadActualC = 0
        End If
        Return comProducto
    End Function

    'Public Function explosion(ByVal cliente As ClienteCO, ByVal producto As ProductoCO, ByVal cjLiquido As Integer, ByRef unLiquido As Integer, ByRef cjEnvase As Integer, ByRef unEnvase As Integer, ByRef unCaja As Integer) As Boolean

    '    Dim item As New ItemCO
    '    Dim dtZcampos As New DataTable

    '    '--- Explosion de cajas vacias
    '    unCaja = unCaja + cjLiquido + cjEnvase

    '    '--- Explosion de unidades liquido.
    '    unLiquido = (producto.unidadesCaja * cjLiquido) + unLiquido

    '    '--- Explosion de envases       
    '    If id_glo_aplicacion = "nc" Then
    '        cjEnvase = cjEnvase + cjLiquido
    '        unEnvase = (producto.unidadesCaja * cjEnvase) + unEnvase
    '    Else
    '        cjEnvase = cjLiquido
    '        unEnvase = unLiquido
    '    End If



    '    '--- Precio del liquido
    '    dtZcampos = getZcampos("PRECIO")
    '    getDescuento("PRECIO", cliente.codigo, producto.codigo, item, dtZcampos)
    '    If item.precioUm = "UN" Then
    '        'producto.precioLiquido = objUtilBL.isDecimal(((cjLiquido * producto.unidadesCaja) + unLiquido) * item.precioUnitario)
    '        producto.precioLiquido = objUtilBL.isDecimal((unLiquido) * item.precioUnitario)
    '    Else
    '        'producto.precioLiquido = objUtilBL.isDecimal(cjLiquido * item.precioUnitario + unLiquido * item.precioUnitario / producto.unidadesCaja)
    '        producto.precioLiquido = objUtilBL.isDecimal(unLiquido * item.precioUnitario / producto.unidadesCaja)
    '    End If
    '    producto.precioUnitarioLiquido = objUtilBL.isDecimal(item.precioUnitario)

    '    '--- Precio del envase y caja
    '    dtZcampos = getZcampos("ENVASE")
    '    If producto.idEnvase <> 0 Then
    '        '--- Obtener el precio unitario Envase
    '        getDescuento("ENVASE", cliente.codigo, producto.idEnvase, item, dtZcampos)
    '        If item.precioUm = "UN" Then
    '            'producto.precioEnvase = objUtilBL.isDecimal(((cjEnvase * producto.unidadesCaja) + unEnvase) * item.precioUnitario)
    '            producto.precioEnvase = objUtilBL.isDecimal((unEnvase) * item.precioUnitario)
    '        Else
    '            'producto.precioEnvase = objUtilBL.isDecimal(cjEnvase * item.precioUnitario + unEnvase * item.precioUnitario / producto.unidadesCaja)
    '            producto.precioEnvase = objUtilBL.isDecimal((unEnvase) * item.precioUnitario / producto.unidadesCaja)
    '        End If
    '        producto.precioUnitarioEnvase = objUtilBL.isDecimal(item.precioUnitario)
    '    End If

    '    If producto.idCaja <> 0 Then
    '        '--- Obtener el precio unitario Caja Vacia
    '        getDescuento("ENVASE", cliente.codigo, producto.idCaja, item, dtZcampos)
    '        producto.precioCaja = objUtilBL.isDecimal(unCaja * item.precioUnitario)
    '        producto.precioUnitarioCaja = objUtilBL.isDecimal(item.precioUnitario)
    '    End If

    '    '--- Determinar si se recolecto algun precio
    '    If producto.precioLiquido <> 0 Or producto.precioEnvase <> 0 Or producto.precioCaja <> 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

    Public Function getZcampos(ByVal ttipo As String) As DataTable
        Dim dtzcampos As DataTable
        Dim dvZcampos As New DataView
        Dim oProducto As New ProductoDT
        dtzcampos = oProducto.getZcampos(ttipo)
        Return dtzcampos
    End Function

    Public Function convertirUnidadesAcajas(ByVal producto As ProductoCO, ByRef unidades As Integer, ByRef cajas As String, Optional ByVal cajasCompromiso As Integer = 0, Optional ByVal skipCaja As Boolean = True) As Boolean
        Dim orval_unidades As Integer = unidades
        Dim orval_cajas As String = cajas

        Dim lCaja As Integer = 0
        unidades = Math.Abs(unidades)

        Try
            If producto.unidadesCaja = 1 Then
                cajas += unidades
                unidades = 0
                Return True
            End If
            lCaja = Int(unidades / producto.unidadesCaja)
            cajas = lCaja + cajas
            unidades = unidades - (producto.unidadesCaja * (lCaja))

            '--- Identificar si hay inventario de cajas
            If (producto.cantidadActualC - cajasCompromiso) - cajas <= 0 And Not skipCaja Then
                unidades = orval_unidades
                cajas = orval_cajas
            End If
            Return True
        Catch ex As Exception
            unidades = "0"
            cajas = "0"
        End Try
    End Function

    Public Function mitadMasUno(ByVal producto As ProductoCO, ByRef unidades As String, ByRef cajaPlastica As String, Optional ByVal cajasCompromiso As Integer = 0, Optional ByVal cajasLiquido As String = "0") As Boolean
        '--- Regla mitad + 1       
        If (unidades >= (producto.unidadesCaja / 2) + 1) And (cajaPlastica <= 0) Then
            cajaPlastica += 1
        Else
            '--- No se va a agregar ninguna caja
            Return False
        End If

        If producto.cantidadActualC <= (cajasLiquido + cajaPlastica + cajasCompromiso) Then
            cajaPlastica -= 1
        End If
        Return True
    End Function

    Private Function filtrarLocks(ByVal dtProductos As DataTable, ByVal dtLocks As DataTable)

        Dim cliente As New ClienteCO
        Dim oCliente As New ClienteBL
        Dim clienteDT As New ClienteDT
        Dim filtro As String = ""
        Dim dvCliente As New DataView
        Dim dtcliente As New DataTable
        Dim objRlayer As New rLayerHandler

        '--- Obtener los atributos del cliente
        dtcliente = clienteDT.getDetalleCliente(id_glo_cliente, objRlayer)
        If Not objRlayer.evaluaTabla(dtcliente) Then
            If objRlayer.codigo = 100 Then MsgBox(objRlayer.texto)
            Return Nothing
        End If

        If dtLocks.Rows.Count() <= 0 Then Return True

        For i = 0 To dtLocks.Rows.Count() - 1

            If Not dtLocks.Rows(i).Item("sociedad").Equals("") Then
                filtro = " sociedad LIKE '" & dtLocks.Rows(i).Item("sociedad") & "'"
            Else
                filtro = " sociedad LIKE '%' "
            End If

            If Not dtLocks.Rows(i).Item("region").Equals("") Then
                filtro = filtro & " and region  LIKE '" & dtLocks.Rows(i).Item("region") & "'"
            Else
                filtro = filtro & " and region LIKE '%' "
            End If

            If Not dtLocks.Rows(i).Item("grupoVentas").Equals("") Then
                filtro = filtro & " and grupoVentas  LIKE '" & dtLocks.Rows(i).Item("grupoVentas") & "'"
            Else
                filtro = filtro & " and grupoVentas LIKE '%' "
            End If
            If Not dtLocks.Rows(i).Item("ruta").Equals("") Then
                filtro = filtro & " and ruta  = " & dtLocks.Rows(i).Item("ruta") & ""
            Else
                filtro = filtro & " and ruta <> 0 "
            End If
            If Not dtLocks.Rows(i).Item("ramo").Equals("") Then
                filtro = filtro & " and ramo  LIKE '" & dtLocks.Rows(i).Item("ramo") & "'"
            Else
                filtro = filtro & " and ramo LIKE '%' "
            End If
            If Not dtLocks.Rows(i).Item("categoria").Equals("") Then
                filtro = filtro & " and categoria  LIKE '" & dtLocks.Rows(i).Item("categoria") & "'"
            Else
                filtro = filtro & " and categoria LIKE '%' "
            End If
            If Not dtLocks.Rows(i).Item("idcliente").Equals("") Then
                filtro = filtro & " and id_cliente  = " & dtLocks.Rows(i).Item("idcliente") & ""
            Else
                filtro = filtro & " and id_cliente <> 0 "
            End If
            If Not dtLocks.Rows(i).Item("ramo5").Equals("") Then
                filtro = filtro & " and ramo5  = '" & dtLocks.Rows(i).Item("ramo5") & "'"
            Else
                filtro = filtro & " and ramo5 <> '' "
            End If
            If Not dtLocks.Rows(i).Item("tipodi").Equals("") Then
                filtro = filtro & " and tipodi  = 'NUEVO' "
            Else
                filtro = filtro & " and tipodi <> '' "
            End If
            Dim foundRows() As DataRow

            '--- Determina si el bloqueo actual (i) aplica al cliente
            foundRows = dtcliente.Select(filtro)

            '--- Si aplica entonces buscar el material en el listado y cambiar su estado lock
            If foundRows.Length > 0 Then
                Dim Dr() As DataRow
                '--- Puede ser que DR no tenga filas y se dispara la excepcion pero el listado de materiales debe mostrarse.
                Dr = dtProductos.Select("codigo = '" & dtLocks.Rows(i).Item("idproducto") & "'")
                If Dr.Length <> 0 Then
                    Dr(0)("locked") = 1
                    '-- Se desactivo este linea por solicitud de preventa ya que era molesto que desplegara todos los productos por instrucciones de Marco Robles
                Else
                    'MsgBox("Un material no fue bloqueado.")
                    'MsgBox("El codigo " & dtLocks.Rows(i).Item("idproducto").ToString() & " no esta en el maestro de materiales. ")
                End If
            End If
        Next

        'For i = 0 To dtProductos.Rows.Count() - 1
        'MsgBox(dtProductos.Rows(i).Item("codigo").ToString() + " " + dtProductos.Rows(i).Item("locked").ToString())
        'Next

        Return True
    End Function


    '--- xoMobile 2.0
    Public Function explosionarMaterial(ByVal cliente As ClienteCO, ByVal producto As ProductoCO, ByVal cajas_liquido As Integer, ByVal unidades_liquido As Integer, ByVal cajas_envase As Integer, ByVal unidades_envase As Integer, ByVal unidades_caja As Integer, Optional ByRef item2 As ItemCO = Nothing) As ItemCO
        Dim item As New ItemCO

        '--- Inicializar
        item.un_liquido = (producto.unidadesCaja * cajas_liquido) + unidades_liquido
        item.un_caja = unidades_caja + cajas_liquido
        item.un_envase = item.un_liquido
        item.unidadesCaja = producto.unidadesCaja

        '--- Precio del liquido
        If obtenerImporteCondicion("PRECIO", cliente, producto.codigo, item) Then


            '--- Precio del envase
            If producto.idEnvase <> 0 Then
                obtenerImporteCondicion("ENVASE", cliente, producto.idEnvase, item)
            End If

            '--- Precio de la caja
            If producto.idCaja <> 0 Then
                obtenerImporteCondicion("ENVASE", cliente, producto.idCaja, item, True)
            End If
        End If

        '--- Determinar si se recolecto algun precio
        If item.precioUnitarioCaja <> 0 Or item.precioUnitarioEnvase <> 0 Or item.precioUnitarioLiquido <> 0 Then
            Return item
        Else
            item2 = item
            Return Nothing
        End If
    End Function

    '--- xoMobile 2.0
    Public Function obtenerImporteCondicion(ByVal condicion As String, ByVal cliente As ClienteCO, ByVal id_producto As String, ByRef item As ItemCO, Optional ByVal isCaja As Boolean = False) As Boolean

        Dim productoDT As New ProductoDT
        Dim dtGeneric As New DataTable
        Dim iva As Decimal = (1 + co_glo_porcentajeIVA)
        Dim objRlayer As New rLayerHandler

        '--- Obtener el importe de la condicon ("ENVASE | PRECIO")
        dtGeneric = productoDT.getPrecio(condicion, cliente.listaPrecio, id_producto, objRlayer)
        objRlayer.evaluaTabla(dtGeneric)
        If objRlayer.conError And id_producto <> 0 Then
            MsgBox("No se encontro el precio del producto: " & id_producto & vbCrLf & ", Lista precio del cliente:" & cliente.listaPrecio & vbCrLf & vbCrLf & "@ProductoBL[obtenerImporteCondicion]", MsgBoxStyle.Critical, "Precio del articulo")
            Return False
        End If

        item.precioUm = dtGeneric.Rows(0).Item("um").ToString()
        item.precioUnitario = objUtilBL.isDecimal(dtGeneric.Rows(0).Item("importe") / dtGeneric.Rows(0).Item("factor"))

        Select Case condicion
            Case Is = "PRECIO"
                If item.precioUm = "UN" Then
                    item.importeLiquido = objUtilBL.isDecimal(item.un_liquido * item.precioUnitario)
                Else
                    item.importeLiquido = objUtilBL.isDecimal(item.un_liquido * item.precioUnitario / item.unidadesCaja)
                End If
                item.precioUnitarioLiquido = objUtilBL.isDecimal(item.precioUnitario)
                item.importeLiquidoIva = objUtilBL.isDecimal(item.importeLiquido * co_glo_porcentajeIVA)
                item.precioVentaLiquido = objUtilBL.isDecimal(item.precioUnitarioLiquido * iva)


            Case Is = "ENVASE"
                If Not isCaja Then
                    If item.precioUm = "UN" Then
                        item.importeEnvase = objUtilBL.isDecimal(item.un_liquido * item.precioUnitario)
                    Else
                        item.importeEnvase = objUtilBL.isDecimal(item.un_liquido * item.precioUnitario / item.unidadesCaja)
                    End If
                    item.precioUnitarioEnvase = objUtilBL.isDecimal(item.precioUnitario)
                    item.importeEnvaseIva = objUtilBL.isDecimal(item.importeEnvase * co_glo_porcentajeIVA)
                    item.precioVentaEnvase = objUtilBL.isDecimal(item.precioUnitarioEnvase * iva)
                Else
                    item.importeCaja = objUtilBL.isDecimal(item.un_caja * item.precioUnitario)
                    item.precioUnitarioCaja = objUtilBL.isDecimal(item.precioUnitario)
                    item.importeCajaIva = objUtilBL.isDecimal(item.importeCaja * co_glo_porcentajeIVA)
                    item.precioVentaCaja = objUtilBL.isDecimal(item.precioUnitarioCaja * iva)
                End If
            Case Else
        End Select
        Return True
    End Function




    Public Function recalculaDescuento(ByVal condicion As String, ByVal cliente As ClienteCO, ByVal producto As ProductoCO, ByVal Item As ItemCO, ByVal comDespacho As documentoCO, ByVal porcentajeDestoAdicional As String)
        '--------------------------------------------------------------------------
        '--- DESCRIPCION: Recalcula el descuento de un producto en un objeto 'item'
        '--------------------------------------------------------------------------

        Dim productoDT As New ProductoDT

        '--- Obtener el importe del descuento
        'dtGeneric = productoDT.getDescuento(condicion, cliente, producto.categoria, objRlayer)
        'objRlayer.evaluaTabla(dtGeneric)
        'If Not objRlayer.conError Then
        '    '---Si el documento es de contado entonces recuepra el nuevo porcentaje de lo contrario no se actualiza en 'item'
        '    If comDespacho.tipoPago = "CONTADO" Then Item.porcentajeDesto += objUtilBL.isDecimal(dtGeneric.Rows(0).Item("importe"))
        'End If

        '--- Actualizar los valores del descuento
        Item.porcentajeDesto += objUtilBL.isDecimal(porcentajeDestoAdicional)
        Item.importeDesto = objUtilBL.isDecimal((Item.porcentajeDesto / 100) * (Item.importeLiquido + Item.importeLiquidoIva))
        Item.valorIvaDesto = objUtilBL.isDecimal(Item.importeDesto - (Item.importeDesto / (1 + co_glo_porcentajeIVA)))
        Return True
    End Function

    Public Function descuentoCategoria(ByVal condicion As String, ByVal cliente As ClienteCO, ByVal producto As ProductoCO, ByRef item As ItemCO, Optional ByVal pctDestoAdicional As Integer = 0) As Boolean
        Dim productoDT As New ProductoDT
        Dim dtGeneric As New DataTable
        Dim iva As Decimal = (1 + co_glo_porcentajeIVA)
        Dim objRlayer As New rLayerHandler

        item.porcentajeDesto = pctDestoAdicional
        item.porcentajeDestoPP = 0
        item.importeDesto = 0
        item.importeDestoPP = 0
        item.valorIvaDesto = 0

        '--- El descuento no aplica si 
        If id_glo_cliente = id_glo_clienteGenerico Then Return False

        '--- Obtener el importe de la condicon descuento
        dtGeneric = productoDT.getDescuento(condicion, cliente, producto.categoria, objRlayer)
        objRlayer.evaluaTabla(dtGeneric)

        If Not objRlayer.conError Then
            '---Si hay descuento entonces añadirlo
            item.porcentajeDesto += objUtilBL.isDecimal(dtGeneric.Rows(0).Item("importe"))
        End If
        item.porcentajeDestoPP = item.porcentajeDesto
        item.importeDesto = objUtilBL.isDecimal((item.porcentajeDesto / 100) * (item.importeLiquido + item.importeLiquidoIva))
        item.importeDestoPP = item.importeDesto
        item.valorIvaDesto = objUtilBL.isDecimal(item.importeDestoPP - (item.importeDestoPP / (1 + co_glo_porcentajeIVA)))
        Return True
    End Function

    '--- xoMobile 2.0
    Public Function obtenerBom(ByVal idProducto As String) As DataTable
        dtGeneric = objProductoDT.getBom(idProducto, objRlayer)
        objRlayer.evaluaTabla(dtGeneric)
        If objRlayer.conError Then
            Return Nothing
        Else
            Return dtGeneric
        End If
    End Function

    Private Function ejecutarDescuentoManual(ByRef item As ItemCO, ByVal producto As ProductoCO)

        '--- No continuar si la aplicacion es Despacho
        If id_glo_aplicacion = 16 Then Return True
        Dim objProductoBL As New ProductoBL

        '--- identificar si el descuento es manual o automatico
        Dim maximo As Decimal
        If objProductoBL.getDescuentoManual(maximo, producto.codigo) Then
            Dim frmDm As New frmDescuentoManual
            frmDm.maximo = maximo
            frmDm.lblArticulo.Text = producto.descripcion
            frmDm.lblMaximo.Text = "Hasta un " + frmDm.maximo.ToString + " %."
            frmDm.ShowDialog()
            item.porcentajeDesto = frmDm.txtDescuento.Text * -1
            item.porcentajeDestoPP = item.porcentajeDesto
            item.importeDesto = objUtilBL.isDecimal((item.porcentajeDesto / 100) * (item.importeLiquido + item.importeLiquidoIva))
            item.importeDestoPP = item.importeDesto
            item.valorIvaDesto = objUtilBL.isDecimal(item.importeDestoPP - (item.importeDestoPP / (1 + co_glo_porcentajeIVA)))
            Return True
        End If
        Return False

    End Function

    Public Function getDescuentoManual(ByRef maximo As Decimal, ByVal idproducto As String) As Boolean
        Dim oProducto As New ProductoDT
        Dim dtDescuento As DataTable
        Dim rlayer As New rLayerHandler
        dtDescuento = oProducto.getDescuentoManual(idproducto, rlayer)
        If dtDescuento.Rows.Count > 0 Then
            maximo = Math.Abs(objUtilBL.isDecimal(dtDescuento.Rows(0).Item("importe").ToString))
            If maximo > 0 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
End Class

