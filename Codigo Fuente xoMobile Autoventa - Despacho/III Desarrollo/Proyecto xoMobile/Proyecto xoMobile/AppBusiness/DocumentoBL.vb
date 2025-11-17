Imports System
Imports System.Diagnostics
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Data
Imports Proyecto_xoMobile_Packs



Public Class DocumentoBL

    '--- Objetos de la capa de datos
    Dim objReciboDT As New Recibo
    Dim objFacturaDT As New Factura
    Dim objNotaCreditoDT As New NotaCreditoDT

    '--- Objetos de la capa de negocios
    Dim objUtilBL As New UtilitarioBL
    Dim objClienteBL As New ClienteBL

#Region " FACTURA "

    Public Function crearOrdenVenta(ByVal lstDetalle As Windows.Forms.ListView, ByRef OrdenVenta As documentoCO, ByRef Cliente As ClienteCO) As Boolean
        Dim comItem As New ItemCO
        Dim comProducto As New ProductoCO
        Dim objProductoBL As New ProductoBL
        Dim dtZcampos As New DataTable
        Dim VImporte As Integer
        VImporte = 0

        Try
            'If OrdenVenta.condicion <> "" Then id_glo_condicion = OrdenVenta.condicion

            '--- Crear el encabezado de la orden de venta
            OrdenVenta.idEncabezado = objFacturaDT.crearEncabezado(OrdenVenta, Cliente)

            '--- Crear el detalle de la orden de venta
            For i As Integer = 0 To lstDetalle.Items.Count - 1

                If Not lstDetalle.Items(i).SubItems(10).Text = "" Then

                    '--- Obtener el detalle del producto          
                    comItem.idProducto = lstDetalle.Items.Item(i).SubItems(10).Text
                    comProducto = objProductoBL.getDetalleDelProducto(comItem.idProducto)

                    '--- Capturar la informacion del producto agregado
                    comItem.idEncabezado = OrdenVenta.idEncabezado
                    comItem.NoItem = i + 1
                    comItem.idProducto = lstDetalle.Items.Item(i).SubItems(10).Text
                    comItem.cj = objUtilBL.isDecimal(lstDetalle.Items.Item(i).SubItems(5).Text)
                    comItem.un = objUtilBL.isDecimal(lstDetalle.Items.Item(i).SubItems(6).Text)
                    comItem.cantidad = comItem.cj * comProducto.unidadesCaja + comItem.un 'objUtilBL.isDecimal(lstDetalle.Items.Item(i).SubItems(9).Text)
                    If comItem.cantidad = 0 Then
                        i += 1
                    End If
                    comItem.um = "UN"
                    comItem.precio = (lstDetalle.Items.Item(i).SubItems(4).Text) '--- PrecioVenta (4)/ sinIva (11)

                    comItem.importe = lstDetalle.Items.Item(i).SubItems(7).Text
                    comItem.importeSinIva = (lstDetalle.Items.Item(i).SubItems(12).Text)
                    comItem.iva = co_glo_porcentajeIVA

                    'CN
                    comItem.valorIvaImporte = comItem.importe - comItem.importeSinIva
                    comItem.tipoVenta = "V"
                    comItem.idRubro = (lstDetalle.Items.Item(i).SubItems(3).Text)
                    comItem.trqt = comItem.cj + "/" + comItem.un
                    comItem.litm = lstDetalle.Items.Item(i).SubItems(0).Text
                    comItem.estado = 0

                    '--- Calcular valor de descuentos para este item
                    If comItem.idRubro = "L" And id_glo_cliente <> id_glo_clienteGenerico Then
                        comItem.porcentajeDesto = objUtilBL.isDecimal(lstDetalle.Items(i).SubItems(14).Text) + objUtilBL.isDecimal(OrdenVenta.porcentajeDestoAdicional)
                        comItem.importeDesto = objUtilBL.isDecimal((comItem.porcentajeDesto / 100) * comItem.importeSinIva)
                        comItem.porcentajeDestoPP = comItem.porcentajeDesto
                        comItem.importeDestoPP = comItem.importeDesto
                    Else
                        comItem.importeDesto = 0
                        comItem.porcentajeDesto = 0
                        comItem.importeDestoPP = 0
                        comItem.porcentajeDestoPP = 0
                    End If

                    comItem.valorIvaDesto = -1 * (comItem.importeDesto - (objUtilBL.isDecimal((comItem.porcentajeDesto / 100) * comItem.importe)))

                    '--- Agregar el item al detalle de la Orden de Venta
                    objFacturaDT.agregarItem(comItem)
                    VImporte = VImporte + comItem.importe
                End If
            Next
            '            MsgBox(VImporte)
            Return True
        Catch ex As Exception
            MsgBox("Error al crear la orden de venta." + ex.Message())
            Return False
        End Try
    End Function

    Public Function credencialesServidor() As DataTable
        Dim dtCredenciales As New DataTable
        Dim oCliente As New ClienteDT
        If (id_glo_internet = False) Then
            dtCredenciales = oCliente.getCredenciales()
        Else
            dtCredenciales = oCliente.getCredencialesI()
        End If

        If dtCredenciales.Rows.Count > 0 Then
            Return dtCredenciales
        Else
            Throw New Exception("No fue posible recuperar los datos de las credenciales")
            Return Nothing
        End If
    End Function


    Public Function getCXCFEL(ByVal idRecibo As String) As DataTable
        Dim dtCredenciales As New DataTable
        Dim oCliente As New ClienteDT
        Try
            dtCredenciales = oCliente.getReferenciaFEL(idRecibo)
            If dtCredenciales.Rows.Count > 0 Then
                Return dtCredenciales
            Else
                Throw New Exception("No fue posible recuperar los datos de las credenciales")
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function UpdateTipoReceptor(ByVal tipoReceptor As String, ByVal idReceptor As String, ByVal idFactura As String) As Boolean
        Dim oCliente As New ClienteDT
        Try
            oCliente.tipoReceptor(tipoReceptor, idReceptor, idFactura)
            Return True
        Catch ex As Exception
            MessageBox.Show("No se encontraron las referencias de la factura cobrada")
            Return Nothing
        End Try
    End Function

    Public Function UpdateTipoReceptorNC(ByVal tipoReceptor As String, ByVal idReceptor As String, ByVal idNC As String) As Boolean
        Dim oCliente As New ClienteDT
        Try
            oCliente.tipoReceptorNC(tipoReceptor, idReceptor, idNC)
            Return True
        Catch ex As Exception
            Throw New Exception("No fue posible recuperar los datos de las credenciales")
            Return Nothing
        End Try
    End Function

    Public Function ListaPrecioCliente(ByVal idCliente As String) As DataTable
        Dim dtCredenciales As New DataTable
        Dim oCliente As New ClienteDT
        dtCredenciales = oCliente.getListaPrecioCliente(idCliente)
        If dtCredenciales.Rows.Count > 0 Then
            Return dtCredenciales
        Else
            Throw New Exception("No fue posible recuperar los datos de las credenciales")
            Return Nothing
        End If
    End Function

    Public Function ListaPrecioProducto(ByVal id_producto As String, ByVal listaPrecios As String) As DataTable
        Dim dtCredenciales As New DataTable
        Dim oCliente As New ClienteDT
        dtCredenciales = oCliente.getProductoListaPrecio(id_producto, listaPrecios)
        If dtCredenciales.Rows.Count > 0 Then
            Return dtCredenciales
        Else
            Throw New Exception("No fue posible recuperar los datos de las credenciales")
            Return Nothing
        End If
    End Function

    Public Function datosElectronicos(ByVal idfactura As Integer) As DataTable
        Dim dtElectronicos As New DataTable
        Dim oCliente As New ClienteDT
        dtElectronicos = oCliente.getDatosElectronicos(idfactura)
        If dtElectronicos.Rows.Count > 0 Then
            Return dtElectronicos
        Else
            MessageBox.Show("No existe FACTURA ELECTRONICA para este documento")
            Throw New Exception("No fue posible recuperar los datos de las credenciales")
            Return Nothing
        End If
    End Function

    Public Function datosElectronicosCXC(ByVal idfactura As Integer) As DataTable
        Dim dtElectronicos As New DataTable
        Dim oCliente As New ClienteDT
        dtElectronicos = oCliente.getDatosElectronicosCXC(idfactura)
        If dtElectronicos.Rows.Count > 0 Then
            Return dtElectronicos
        Else
            MessageBox.Show("No existe FACTURA ELECTRONICA para este documento")
            Throw New Exception("No fue posible recuperar los datos de las credenciales")
            Return Nothing
        End If
    End Function

    Public Function postFactura(ByVal idFactura As String, ByVal serie As String, ByVal autorizacion As String, ByVal preimpreso As String, ByVal direccionfel As String, ByVal nombre_fel As String) As Boolean
        Dim dtFacturaDetalle As New DataTable
        objFacturaDT.actualizar_FEL(idFactura, serie, autorizacion, preimpreso, direccionfel, nombre_fel)


    End Function

    Public Function postFacturaNOTACREDITO(ByVal idNc As String, ByVal serie As String, ByVal autorizacion As String, ByVal preimpreso As String, ByVal direccionfel As String, ByVal nombre_fel As String) As Boolean
        Dim dtFacturaDetalle As New DataTable
        objFacturaDT.actualizar_FELNC(idNc, serie, autorizacion, preimpreso, direccionfel, nombre_fel)
    End Function

    Public Function crearOrdenVentaDespacho(ByVal lstDetalle As Windows.Forms.ListView, ByRef OrdenVenta As documentoCO, ByRef Cliente As ClienteCO) As Boolean
        Dim comItem As New ItemCO
        Dim comProducto As New ProductoCO
        Dim objProductoBL As New ProductoBL

        Try

            '--- Grabar encabezado.
            OrdenVenta.idEncabezado = objFacturaDT.crearEncabezado(OrdenVenta, Cliente)

            '--- Crear el detalle de la orden de venta
            For i As Integer = 0 To lstDetalle.Items.Count - 1

                If Not lstDetalle.Items(i).SubItems(10).Text = "" Then
                    comItem.idEncabezado = OrdenVenta.idEncabezado
                    comItem.NoItem = i + 1
                    comItem.idProducto = lstDetalle.Items.Item(i).SubItems(10).Text
                    comProducto = objProductoBL.getDetalleDelProducto(comItem.idProducto)
                    comItem.um = "UN"
                    comItem.cantidad = objUtilBL.isDecimal(lstDetalle.Items.Item(i).SubItems(17).Text)
                    comItem.precio = (lstDetalle.Items.Item(i).SubItems(21).Text)
                    comItem.importeSinIva = (lstDetalle.Items.Item(i).SubItems(19).Text)
                    comItem.importe = lstDetalle.Items.Item(i).SubItems(22).Text
                    comItem.iva = co_glo_porcentajeIVA
                    comItem.idRubro = (lstDetalle.Items.Item(i).SubItems(3).Text)

                    '--- Descuentos

                    If comItem.idRubro = "L" And id_glo_cliente <> id_glo_clienteGenerico Then
                        Dim importeDesto As Decimal = objUtilBL.isDecimal(lstDetalle.Items.Item(i).SubItems(23).Text)
                        If OrdenVenta.isContado Then
                            comItem.valorIvaDesto = objUtilBL.isDecimal(importeDesto - (importeDesto / (1 + co_glo_porcentajeIVA)))
                            comItem.importeDesto = importeDesto - comItem.valorIvaDesto
                            comItem.porcentajeDesto = objUtilBL.isDecimal(lstDetalle.Items(i).SubItems(14).Text) + objUtilBL.isDecimal(OrdenVenta.porcentajeDestoAdicional)

                        Else
                            comItem.valorIvaDesto = objUtilBL.isDecimal(importeDesto - (importeDesto / (1 + co_glo_porcentajeIVA)))
                            comItem.importeDestoPP = importeDesto - comItem.valorIvaDesto
                            'comItem.porcentajeDestoPP = objUtilBL.isDecimalFlat(importeDesto * 100 / comItem.importe)
                            comItem.porcentajeDestoPP = objUtilBL.isDecimal(lstDetalle.Items.Item(i).SubItems(14).Text)
                        End If
                    Else
                        comItem.valorIvaDesto = 0
                        comItem.importeDesto = 0
                        comItem.porcentajeDesto = 0
                        comItem.importeDestoPP = 0
                        comItem.porcentajeDestoPP = 0
                    End If

                    comItem.tipoVenta = "V"
                    comItem.un = objUtilBL.isDecimal(lstDetalle.Items.Item(i).SubItems(17).Text).ToString
                    comItem.cj = 0
                    objProductoBL.convertirUnidadesAcajas(comProducto, comItem.un, comItem.cj)
                    comItem.trqt = comItem.cj + "/" + comItem.un
                    comItem.litm = comItem.idProducto
                    comItem.estado = 0
                    comItem.valorIvaImporte = comItem.importe - comItem.importeSinIva

                    '--- Grabar item
                    objFacturaDT.agregarItem(comItem)
                End If
            Next
            Return True
        Catch ex As Exception
            MsgBox("Error [crearOrdenVentaDespacho]: " + ex.Message())
            Return False
        End Try
    End Function

    Public Function crearOrdenVentaCambio(ByVal lstDetalle As Windows.Forms.ListView, ByRef OrdenVenta As documentoCO, ByRef Cliente As ClienteCO) As Boolean
        Dim comItem As New ItemCO
        Dim comProducto As New ProductoCO
        Dim objProductoBL As New ProductoBL

        Try

            '--- Grabar encabezado.
            OrdenVenta.idEncabezado = objFacturaDT.crearEncabezadoCambio(OrdenVenta, Cliente)

            '--- Crear el detalle de la orden de venta
            For i As Integer = 0 To lstDetalle.Items.Count - 1

                If Not lstDetalle.Items(i).SubItems(10).Text = "" Then
                    comItem.idEncabezado = OrdenVenta.idEncabezado
                    comItem.NoItem = i + 1
                    comItem.idProducto = lstDetalle.Items.Item(i).SubItems(10).Text
                    comProducto = objProductoBL.getDetalleDelProducto(comItem.idProducto)
                    comItem.um = "UN"
                    comItem.cantidad = objUtilBL.isDecimal(lstDetalle.Items.Item(i).SubItems(17).Text)
                    comItem.precio = (lstDetalle.Items.Item(i).SubItems(21).Text)
                    comItem.importeSinIva = (lstDetalle.Items.Item(i).SubItems(19).Text)
                    comItem.importe = lstDetalle.Items.Item(i).SubItems(22).Text
                    comItem.iva = co_glo_porcentajeIVA
                    comItem.idRubro = (lstDetalle.Items.Item(i).SubItems(3).Text)

                    '--- Descuentos

                    If comItem.idRubro = "L" And id_glo_cliente <> id_glo_clienteGenerico Then
                        Dim importeDesto As Decimal = objUtilBL.isDecimal(lstDetalle.Items.Item(i).SubItems(23).Text)
                        If OrdenVenta.isContado Then
                            comItem.valorIvaDesto = objUtilBL.isDecimal(importeDesto - (importeDesto / (1 + co_glo_porcentajeIVA)))
                            comItem.importeDesto = importeDesto - comItem.valorIvaDesto
                            comItem.porcentajeDesto = objUtilBL.isDecimal(lstDetalle.Items(i).SubItems(14).Text) + objUtilBL.isDecimal(OrdenVenta.porcentajeDestoAdicional)

                        Else
                            comItem.valorIvaDesto = objUtilBL.isDecimal(importeDesto - (importeDesto / (1 + co_glo_porcentajeIVA)))
                            comItem.importeDestoPP = importeDesto - comItem.valorIvaDesto
                            'comItem.porcentajeDestoPP = objUtilBL.isDecimalFlat(importeDesto * 100 / comItem.importe)
                            comItem.porcentajeDestoPP = objUtilBL.isDecimal(lstDetalle.Items.Item(i).SubItems(14).Text)
                        End If
                    Else
                        comItem.valorIvaDesto = 0
                        comItem.importeDesto = 0
                        comItem.porcentajeDesto = 0
                        comItem.importeDestoPP = 0
                        comItem.porcentajeDestoPP = 0
                    End If

                    comItem.tipoVenta = "V"
                    comItem.un = objUtilBL.isDecimal(lstDetalle.Items.Item(i).SubItems(17).Text).ToString
                    comItem.cj = 0
                    objProductoBL.convertirUnidadesAcajas(comProducto, comItem.un, comItem.cj)
                    comItem.trqt = comItem.cj + "/" + comItem.un
                    comItem.litm = comItem.idProducto
                    comItem.estado = 0
                    comItem.valorIvaImporte = comItem.importe - comItem.importeSinIva

                    '--- Grabar item
                    objFacturaDT.agregarItem(comItem)
                End If
            Next
            Return True
        Catch ex As Exception
            MsgBox("Error [crearOrdenVentaDespacho]: " + ex.Message())
            Return False
        End Try
    End Function


    Public Function existeRecibo(ByVal idRecibo As String) As Boolean
        Dim check As Boolean = False

        If (objReciboDT.cantidadRecibo(idRecibo) > 0) Then
            check = True
        Else
            check = False
        End If
        Return check
    End Function

    Public Function existeFacturaRecibo(ByVal idFactura As String, ByVal idCliente As String) As Boolean
        Dim check As Boolean = False
        Dim idRecibo As String = ""
        idRecibo = objReciboDT.getReciboFact(idFactura)
        If (idRecibo <> 0) Then
            If (objReciboDT.VerificaReciboFact(idRecibo, idFactura, idCliente)) Then
                check = True
            Else
                check = False
            End If
        Else
            check = False
        End If
        Return check
    End Function

    '--- xoMobile 2.0?


    Public Function eliminarDocumento(ByVal ttipo As String) As Boolean
        Dim oFactura As New Factura
        Dim oNotaCredito As New NotaCreditoDT
        Dim oRecibo As New Recibo
        Select Case ttipo
            Case "FACT"
                If oFactura.eliminar() = 1 Then
                    Return True
                Else
                    Return False
                End If
            Case "NC"
                If oNotaCredito.eliminar() = 1 Then
                    Return True
                Else
                    Return False
                End If
                Return True
            Case "REC"
                If oRecibo.eliminar() = 1 Then
                    Return True
                Else
                    Return False
                End If
                Return True
        End Select
        Return False
    End Function

    Public Function alcanzaPresupuesto(ByVal Cliente As ClienteCO, ByVal lstDetalle As Windows.Forms.ListView, ByRef dtZcampos As DataTable, ByVal ordenVenta As documentoCO) As Boolean
        '--- Retorna por referencia la tabla dtDescuentos la cual se llena con los descuentos normales DESC
        '--- o los descuentos de temporada alta DESCTA

        Dim comItem As New ItemCO
        Dim comProducto As New ProductoCO
        Dim objProductoBL As New ProductoBL
        Dim litrosCompra As Decimal


        If objUtilBL.isDecimal(Cliente.volPresupuesto) > 0 And Trim(Cliente.categoria) <> "08" Then
            For i As Integer = 0 To lstDetalle.Items.Count - 1
                If (lstDetalle.Items.Item(i).SubItems(3).Text) = "L" Then
                    comItem.idProducto = lstDetalle.Items.Item(i).SubItems(10).Text
                    comProducto = objProductoBL.getDetalleDelProducto(comItem.idProducto)
                    comItem.cj = objUtilBL.isDecimal(lstDetalle.Items.Item(i).SubItems(5).Text)
                    comItem.un = objUtilBL.isDecimal(lstDetalle.Items.Item(i).SubItems(6).Text)
                    litrosCompra = litrosCompra + objUtilBL.isDecimal2(comProducto.litrosUnidad) * (objUtilBL.isDecimal(comProducto.unidadesCaja) * objUtilBL.isDecimal(comItem.cj) + objUtilBL.isDecimal(comItem.un))
                End If
            Next

            If litrosCompra >= objUtilBL.isDecimal2(Cliente.volPresupuesto) Then

                '--- Obtener la nueva condicion de pago
                If (DiasCreditoPresupuesto(Cliente, id_glo_condicion, ordenVenta)) Then

                    '--- Obtener la tabla de descuentos Temporada Alta 
                    dtZcampos = objProductoBL.getZcampos("DESCTA")

                    '--- Si no existe la tabla de temporada alta obtener la tabla de descuentos Normal
                    If dtZcampos.Rows.Count() < 0 Then
                        dtZcampos = objProductoBL.getZcampos("DESC")
                        'id_glo_condicion = Cliente.condicion
                        Return False
                    End If
                Else
                    '--- Si no esta vigente la tabla de nuevas condiciones 
                    dtZcampos = objProductoBL.getZcampos("DESC")
                    'id_glo_condicion = Cliente.condicion
                    Return False
                End If
                'ordenVenta.condicion = id_glo_condicion
                Return True
            Else
                '--- Obtener la tabla de descuentos Normal
                dtZcampos = objProductoBL.getZcampos("DESC")
                id_glo_condicion = Cliente.condicion
                ordenVenta.condicion = Cliente.condicion
                Return False
            End If
        Else
            '--- Obtener la tabla de descuentos Normal
            dtZcampos = objProductoBL.getZcampos("DESC")
            id_glo_condicion = Cliente.condicion
            Return False
        End If
    End Function

    Private Function DiasCreditoPresupuesto(ByVal cliente As ClienteCO, ByRef condicion As String, ByRef ordenVenta As documentoCO) As Boolean

        '--- Devuelve la condicion de pago cuando alcanza el presupuesto
        Dim oCliente As New ClienteDT
        Dim dtRango As New DataTable
        dtRango = oCliente.getRangoDiasCredito(cliente.categoria)
        If dtRango.Rows.Count() <= 0 Then
            'Si no hay informacion devuelve la condicion original del cliente
            id_glo_condicion = cliente.condicion
            ordenVenta.condicion = cliente.condicion
            Return False
        Else
            'Devuelve la condicion del rango
            id_glo_condicion = dtRango.Rows(0).Item("idCondicion")
            ordenVenta.condicion = dtRango.Rows(0).Item("idCondicion")
            Return True
        End If
    End Function

    '--- xoMobile 2.0

    Public Function anularFactura(ByVal idFactura As String, ByRef idCliente As String, ByVal tipoAnulacion As String, ByVal motivoAnula As String) As Boolean
        Try
            Dim objBitacora As New BitacoraBL
            Dim objCliente As New ClienteBL
            Dim Fact As New Factura
            Dim Factura, NC As New documentoCO
            Dim cliente As New ClienteCO
            Dim objInventario As New InventarioBL

            '--- Crear una instancia del cliente
            cliente = objCliente.getDetalleDelCliente(idCliente)

            '--- Recuperar la factura en su estado actual
            Factura = getFactura(idFactura)
            Factura.motivoAnula = motivoAnula

            '--- Ejecuta el tipo de anulacion
            Select Case tipoAnulacion
                Case "1"
                    anularPorReimpresion(Factura, cliente, NC)
                    Fact.revertirCXCFact(Factura.serie, Factura.numero)
                Case "2"
                    'AnularFacturaPorEstado2(Factura, cliente)
                    'rutina para anular factura
                    AnularFacturaPorEstado(Factura, cliente)
                    Fact.revertirCXCFact(Factura.serie, Factura.numero)
                Case Else
                    MsgBox("Tipo de anulacion no establecido.")
                    Return True
            End Select

            '--- Bitacora Anulación de documento   
            objBitacora.registrarOperacion(21, id_glo_cliente)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    'Anula Facturas que no tienen asociado los recibos pero afectaron el inventario.
    Public Function anularFactura2(ByVal idFactura As String, ByRef idCliente As String, ByVal tipoAnulacion As String, ByVal motivoAnula As String) As Boolean
        Try
            Dim objBitacora As New BitacoraBL
            Dim objCliente As New ClienteBL
            Dim Factura, NC As New documentoCO
            Dim cliente As New ClienteCO
            Dim objInventario As New InventarioBL

            '--- Crear una instancia del cliente
            cliente = objCliente.getDetalleDelCliente(idCliente)

            '--- Recuperar la factura en su estado actual
            Factura = getFactura(idFactura)
            Factura.motivoAnula = motivoAnula

            '--- Ejecuta el tipo de anulacion
            Select Case tipoAnulacion
                Case "1"
                    'anularPorReimpresion(Factura, cliente, NC)
                Case "2"
                    'Anula la Factura aunque no tenga recibo creado porque movio inventario.
                    AnularFacturaPorEstado(Factura, cliente)
                Case Else
                    MsgBox("Tipo de anulacion no establecido.")
                    Return True
            End Select

            '--- Bitacora Anulación de documento   
            objBitacora.registrarOperacion(21, id_glo_cliente)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function





    '--- xoMobile 2.0

    Public Function anularCambio(ByVal idFactura As String, ByRef idCliente As String, ByVal tipoAnulacion As String, ByVal motivoAnula As String) As Boolean
        Try
            Dim objBitacora As New BitacoraBL
            Dim objCliente As New ClienteBL
            Dim Factura, NC As New documentoCO
            Dim cliente As New ClienteCO
            Dim objInventario As New InventarioBL

            '--- Crear una instancia del cliente
            cliente = objCliente.getDetalleDelCliente(idCliente)

            '--- Recuperar la factura en su estado actual
            Factura = getFacturaCambio(idFactura)
            Factura.motivoAnula = motivoAnula

            '--- Ejecuta el tipo de anulacion
            Select Case tipoAnulacion
                Case "1"
                    anularPorReimpresionCambio(Factura, cliente, NC)
                Case "2"
                    AnularFacturaPorEstadoCambio(Factura, cliente)
                Case Else
                    MsgBox("Tipo de anulacion no establecido.")
                    Return True
            End Select

            '--- Bitacora Anulación de documento   
            objBitacora.registrarOperacion(21, id_glo_cliente)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function anularPorReimpresion(ByVal Factura As documentoCO, ByRef cliente As ClienteCO, ByVal NC As documentoCO) As Boolean

        Dim objRuta As New RutaBL
        Dim objBitacora As New BitacoraBL
        Dim objImpresion As New ImpresionBL
        Dim ccF As New CorrelativoCO
        Dim FacturaOriginal As Integer
        Dim FacturaOriginal_numero As String
        Dim Recibo As New documentoCO

        Try

            '--- Anular la factura
            FacturaOriginal = Factura.idEncabezado
            FacturaOriginal_numero = Factura.serie + Factura.numero
            Factura.estado = 2
            Factura.usuarioAnula = id_glo_usuario
            Factura.fechaAnula = "GETDATE()"
            Factura.fechaEmision = "fechaEmision"
            Factura.fechaVence = "fechaVence"
            actualizarFactura(Factura, , False)

            '--- Copiar y recuperar el nuevo id de la factura
            Factura.idEncabezado = copiarFactura(Factura.idEncabezado)

            '--- Crear registros nuevos para la copia de factura
            objRuta.obtenerCorrelativos(Nothing, ccF)
            Factura.estado = 1
            Factura.serie = ccF.serie
            Factura.numero = ccF.actual + 1
            Factura.fechaAnula = "null"
            Factura.usuarioAnula = ""

            '--- Actualizar copia de factura con nuevos registros
            If actualizarFactura(Factura, , False) Then actualizarFacturaDetalle(FacturaOriginal, Factura.idEncabezado)

            '--- Relacionar el recibo con la factura
            Recibo = getRecibo(Factura.idReciboRelacionado)
            Recibo.idEncFacturaRelacionada = Factura.idEncabezado
            actualizarRecibo(Recibo)
            actualizarReciboDetalle(Recibo, Factura)

            '--- Relacionar la cxc con la factura
            ActualizarCuentaPorCobrar(Factura.serie, Factura.numero, FacturaOriginal_numero)

            '--- Incrementar el correlativo 
            actualizarCorrelativo("F")



            


            '--- Reimprimir el documento
            If Not objImpresion.imprimeRecibo(Factura.idReciboRelacionado, False) Then
                MsgBox("La impresora no esta disponible.")

            End If

            objImpresion.imprimeFactura(Factura.idEncabezado, Factura.idReciboRelacionado, False)


            '--- Anula la nota de credito y reimprimir
            anularNcPorReimpresion(Factura.idReciboRelacionado, cliente, Factura)

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Function anularPorReimpresionCambio(ByVal Factura As documentoCO, ByRef cliente As ClienteCO, ByVal NC As documentoCO) As Boolean

        Dim objRuta As New RutaBL
        Dim objBitacora As New BitacoraBL
        Dim objImpresion As New ImpresionBL
        Dim ccF As New CorrelativoCO
        Dim FacturaOriginal As Integer
        Dim FacturaOriginal_numero As String
        Dim Recibo As New documentoCO

        Try

            '--- Anular la factura
            FacturaOriginal = Factura.idEncabezado
            FacturaOriginal_numero = Factura.serie + Factura.numero
            Factura.estado = 2
            Factura.usuarioAnula = id_glo_usuario
            Factura.fechaAnula = "GETDATE()"
            Factura.fechaEmision = "fechaEmision"
            Factura.fechaVence = "fechaVence"
            actualizarFactura(Factura, , False)

            '--- Copiar y recuperar el nuevo id de la factura
            Factura.idEncabezado = copiarFactura(Factura.idEncabezado)

            '--- Crear registros nuevos para la copia de factura
            objRuta.obtenerCorrelativos(Nothing, ccF)
            Factura.estado = 1
            Factura.serie = ccF.serie
            Factura.numero = ccF.actual + 1
            Factura.fechaAnula = "null"
            Factura.usuarioAnula = ""

            '--- Actualizar copia de factura con nuevos registros
            If actualizarFactura(Factura, , False) Then actualizarFacturaDetalle(FacturaOriginal, Factura.idEncabezado)

            '--- Relacionar el recibo con la factura
            'Recibo = getRecibo(Factura.idReciboRelacionado)
            'Recibo.idEncFacturaRelacionada = Factura.idEncabezado
            'actualizarRecibo(Recibo)
            'actualizarReciboDetalle(Recibo, Factura)

            '--- Relacionar la cxc con la factura
            'ActualizarCuentaPorCobrar(Factura.serie, Factura.numero, FacturaOriginal_numero)

            '--- Incrementar el correlativo 
            actualizarCorrelativo("CD")

            '--- Reimprimir el documento
            'If Not objImpresion.imprimeRecibo(Factura.idReciboRelacionado, False) Then
            'MsgBox("La impresora no esta disponible.")

            'End If
            'objImpresion.imprimeFactura(Factura.idEncabezado, Factura.idReciboRelacionado, False)


            '--- Anula la nota de credito y reimprimir
            'anularNcPorReimpresion(Factura.idReciboRelacionado, cliente, Factura)

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Function AnularFacturaPorEstado(ByVal Factura As documentoCO, ByRef cliente As ClienteCO) As Boolean

        Dim objRutaBL As New RutaBL
        Dim objInventario As New InventarioBL
        Dim oDespacho As New DespachoBL
        Dim oDocumento As New DocumentoBL
        Dim despacho As New DespachoBL


        '--- Anular el encabezado de la factura
        Factura.estado = 2
        Factura.usuarioAnula = id_glo_usuario
        Factura.fechaAnula = "GETDATE()"
        Factura.fechaEmision = "fechaEmision"
        Factura.fechaVence = "fechaVence"
        objFacturaDT.actualizar(Factura, , False)


        '--- Revertir Inventario   (xoMobile 2.0)     
        objInventario.actualizarInventario(Factura.idEncabezado, 1, Factura.ttipo)


        '--- Anular recibos vinculados por facturas.
        Dim dtRecibos As New DataTable
        dtRecibos = objReciboDT.getRecibosPorFactura(Factura.idEncabezado)


        For i As Integer = 0 To dtRecibos.Rows.Count() - 1
            Dim idRecibo As String = dtRecibos.Rows(i).Item("id_encRecibo")
            Dim idCxc As String = dtRecibos.Rows(i).Item("idencCxc")

            '--- Anular Recibo
            anularRecibo(idRecibo, cliente)

            '--- Anular Recibos vinculados por encabezado de CXC
            anularReciboPorCXC(idCxc, cliente)

        Next

        '--- Cambiar el estado del despacho
        If tipoRuta = "16" Then
            despacho.marcarPedidoDespacho(Factura.idPedido, False, cliente)
        End If

        Return True
    End Function


    Private Function AnularFacturaPorEstado2(ByVal Factura As documentoCO, ByRef cliente As ClienteCO) As Boolean

        Dim objRutaBL As New RutaBL
        Dim objInventario As New InventarioBL
        Dim oDespacho As New DespachoBL
        Dim oDocumento As New DocumentoBL
        Dim despacho As New DespachoBL


        '--- Anular el encabezado de la factura
        Factura.estado = 2
        Factura.usuarioAnula = id_glo_usuario
        Factura.fechaAnula = "GETDATE()"
        Factura.fechaEmision = "fechaEmision"
        Factura.fechaVence = "fechaVence"
        objFacturaDT.actualizar(Factura, , False)


        '--- Revertir Inventario   (xoMobile 2.0)     
        objInventario.actualizarInventario(Factura.idEncabezado, 1, Factura.ttipo)


        Dim objDocumento As New DocumentoBL
        Dim Fel As New Generador
        Dim dtNotaCredito As New DataTable
        Dim tPreimpreso As Integer = 0

        dtNotaCredito = objNotaCreditoDT.getNotaCreditoPorFactura(Factura.idEncabezado)

        Try
            tPreimpreso = Len(dtNotaCredito.Rows(0).Item("preimpreso").ToString())
        Catch ex As Exception
            tPreimpreso = 0
        End Try

        Try

            If (id_glo_fel = "X") Then
                If (tPreimpreso > 0) Then
                    If (Fel.anulacion(dtNotaCredito.Rows(0).Item("serie").ToString(), dtNotaCredito.Rows(0).Item("preimpreso").ToString(), Factura.nit, DateTime.Now, "POR FALTA DE IMPRESION")) Then
                        '--- Anular Nota de credito si estuviera vinculada.
                        anularNotaCredito(Factura.idEncabezado)

                        Dim dtRecibos As New DataTable
                        dtRecibos = objReciboDT.getRecibosPorFactura(Factura.idEncabezado)


                        For i As Integer = 0 To dtRecibos.Rows.Count() - 1
                            Dim idRecibo As String = dtRecibos.Rows(i).Item("id_encRecibo")
                            Dim idCxc As String = dtRecibos.Rows(i).Item("idencCxc")

                            '--- Anular Recibo
                            anularRecibo(idRecibo, cliente)

                            '--- Anular Recibos vinculados por encabezado de CXC
                            anularReciboPorCXC(idCxc, cliente)

                        Next

                        '--- Cambiar el estado del despacho
                        If tipoRuta = "16" Then
                            despacho.marcarPedidoDespacho(Factura.idPedido, False, cliente)
                        End If
                        Return True
                    End If
                Else
                    '--- Cambiar el estado del despacho
                    If tipoRuta = "16" Then
                        despacho.marcarPedidoDespacho(Factura.idPedido, False, cliente)
                    End If

                    Dim dtRecibos As New DataTable
                    dtRecibos = objReciboDT.getRecibosPorFactura(Factura.idEncabezado)

                    For i As Integer = 0 To dtRecibos.Rows.Count() - 1
                        Dim idRecibo As String = dtRecibos.Rows(i).Item("id_encRecibo")
                        Dim idCxc As String = dtRecibos.Rows(i).Item("idencCxc")

                        '--- Anular Recibo
                        anularRecibo(idRecibo, cliente)

                        '--- Anular Recibos vinculados por encabezado de CXC
                        anularReciboPorCXC(idCxc, cliente)

                    Next
                    If tipoRuta = "16" Then
                        despacho.marcarPedidoDespacho(Factura.idPedido, False, cliente)
                    End If

                    Return True
                End If

            Else
                '--- Anular Nota de credito si estuviera vinculada.
                anularNotaCredito(Factura.idEncabezado)

                Dim dtRecibos As New DataTable
                dtRecibos = objReciboDT.getRecibosPorFactura(Factura.idEncabezado)


                For i As Integer = 0 To dtRecibos.Rows.Count() - 1
                    Dim idRecibo As String = dtRecibos.Rows(i).Item("id_encRecibo")
                    Dim idCxc As String = dtRecibos.Rows(i).Item("idencCxc")

                    '--- Anular Recibo
                    anularRecibo(idRecibo, cliente)

                    '--- Anular Recibos vinculados por encabezado de CXC
                    anularReciboPorCXC(idCxc, cliente)

                Next

                '--- Cambiar el estado del despacho
                If tipoRuta = "16" Then
                    despacho.marcarPedidoDespacho(Factura.idPedido, False, cliente)
                End If

                Return True
            End If

        Catch ex As Exception
            'anularNotaCredito(Factura.idEncabezado)
            '--- Cambiar el estado del despacho
            'If tipoRuta = "16" Then
            'despacho.marcarPedidoDespacho(Factura.idPedido, False, cliente)
            'End If

            Return False
        End Try

        




        
    End Function
    

    Private Function AnularFacturaPorEstadoCambio(ByVal Factura As documentoCO, ByRef cliente As ClienteCO) As Boolean

        Dim objRutaBL As New RutaBL
        Dim objInventario As New InventarioBL
        Dim oDespacho As New DespachoBL
        Dim oDocumento As New DocumentoBL
        Dim despacho As New DespachoBL


        '--- Anular el encabezado de la factura
        Factura.estado = 2
        Factura.usuarioAnula = id_glo_usuario
        Factura.fechaAnula = "GETDATE()"
        Factura.fechaEmision = "fechaEmision"
        Factura.fechaVence = "fechaVence"
        objFacturaDT.actualizar(Factura, , False)


        '--- Revertir Inventario   (xoMobile 2.0)     
        objInventario.actualizarInventario(Factura.idEncabezado, 1, Factura.ttipo)


        '--- Anular recibos vinculados por facturas.
        'Dim dtRecibos As New DataTable
        'dtRecibos = objReciboDT.getRecibosPorFactura(Factura.idEncabezado)


        'For i As Integer = 0 To dtRecibos.Rows.Count() - 1
        'Dim idRecibo As String = dtRecibos.Rows(i).Item("id_encRecibo")
        'Dim idCxc As String = dtRecibos.Rows(i).Item("idencCxc")

        '--- Anular Recibo
        'anularRecibo(idRecibo, cliente)

        '--- Anular Recibos vinculados por encabezado de CXC
        'anularReciboPorCXC(idCxc, cliente)

        'Next

        '--- Cambiar el estado del despacho
        If tipoRuta = "16" Then
            despacho.marcarPedidoDespacho(Factura.idPedido, False, cliente)
        End If

        Return True
    End Function

    Public Function actualizarFactura(ByVal Factura As documentoCO, Optional ByVal iscontado As Boolean = False, Optional ByVal manipulaDescuentos As Boolean = True) As Boolean
        '--- Obtener la version actual del documento 
        '--- Antes de ejecutar este metodo, sino pueden haber inconsistencias en los datos.

        Return objFacturaDT.actualizar(Factura, iscontado, manipulaDescuentos)
    End Function

    Public Function actualizarFacturaDetalle(ByVal idFacturaOriginal As String, ByVal idFacturaCopia As String) As Boolean
        '--- Obtener la version actual del documento porque sino pueden haber inconsistencias en los datos
        objFacturaDT.actualizarDetalle(idFacturaOriginal, idFacturaCopia)
    End Function

    Public Function copiarFactura(ByVal idFactura As String) As Integer
        Return objFacturaDT.setCopiaFactura(idFactura)
    End Function

    Public Sub actualizarCorrelativo(ByVal tipo As String)
        objFacturaDT.incrementCorrelativo(tipo)
    End Sub

    Public Function getContigencia(ByVal Documento As String) As String
        Return objFacturaDT.obtenerContingencia(Documento)
    End Function

    Public Sub actualizarConingencia(ByVal Documento As String, ByVal contingencia As String, ByVal tipo_receptor As String, ByVal idreceptor As String)
        objFacturaDT.actulizaContingencia(Documento, contingencia, tipo_receptor, idreceptor)
    End Sub

    Public Sub actualizarConingencia2(ByVal contingencia As String)
        objFacturaDT.actulizaNumeroAcceso2(contingencia)
    End Sub

   

    Public Sub actualizarNumeroAcceso(ByVal contingencia As String, ByVal serie As String, ByVal numero As String, ByVal cliente As String)
        objFacturaDT.actulizaNumeroAcceso(contingencia, serie, numero, cliente)
    End Sub

    Public Sub actualizarConingenciaNC(ByVal Documento As String, ByVal contingencia As String, ByVal tipo_receptor As String, ByVal idreceptor As String)
        objFacturaDT.actulizaContingencianc(Documento, contingencia, tipo_receptor, idreceptor)
    End Sub

#End Region


#Region " RECIBO "

    Public Function crearRecibo(ByVal recibo As documentoCO, ByVal v_com_ordenVenta As documentoCO, ByVal cliente As ClienteCO) As Boolean
        Dim importePagoAcumulado As Decimal = 0
        Try
            'Crear el encabezado del recibo
            recibo.idEncabezado = objReciboDT.crearEncabezado(recibo)

            'Crear las lineas de pago
            For i As Integer = 0 To recibo.dgPagos.Rows.Count() - 1
                recibo.importeViaPago = recibo.dgPagos.Rows(i).Item(2)
                If objUtilBL.isDecimal(recibo.importeViaPago) <> 0 Then
                    recibo.idViaPago = recibo.dgPagos.Rows(i).Item(0)
                    If recibo.idViaPago = "CR" Then
                        recibo.saldo = recibo.importeViaPago
                        '--- Actualizar Credito disponible en la base de datos y en el objeto
                        objClienteBL.actualizarCreditoDisponible(cliente, recibo.importeViaPago * -1)
                    Else
                        importePagoAcumulado = importePagoAcumulado + recibo.importeViaPago
                    End If
                    recibo.documento = recibo.dgPagos.Rows(i).Item(3)
                    recibo.idInstitucion = recibo.dgPagos.Rows(i).Item(5)
                    If recibo.idInstitucion = "" Then
                        recibo.idInstitucion = 0
                    End If
                    If recibo.documento = "" Then
                        recibo.documento = 0
                    End If
                    objReciboDT.crearDetallePago(recibo)
                End If
            Next

            'Crear el detalle del recibo
            recibo.importePago = importePagoAcumulado
            objReciboDT.crearDetalle(recibo, 1)
            Return True
        Catch ex As Exception
            MsgBox("ERROR al crear recibo de pago: " + ex.Message)
        End Try
    End Function

    

    Public Function anularRecibo(ByVal idRecibo As String, ByRef cliente As ClienteCO, Optional ByVal motivoAnula As String = "") As Boolean
        Dim Fel As New Generador
        Try
            Dim objRutaBL As New RutaBL
            Dim objBitacora As New BitacoraBL
            Dim Recibo As New documentoCO
            Dim bandera As New Boolean

            Dim dtNotaCreditoOrigen As New DataTable
            dtNotaCreditoOrigen = objNotaCreditoDT.getNotaCreditoPorRecibo(Recibo.idEncabezado)
            For ii As Integer = 0 To dtNotaCreditoOrigen.Rows.Count() - 1
                
            Next

            '--- Recuperar  RECIBO en su estado actual
            Recibo = getRecibo(idRecibo)
            Recibo.motivoAnula = motivoAnula
            Recibo.estado = 2
            Recibo.usuarioAnula = id_glo_usuario
            Recibo.fechaAnula = "GETDATE()"
            objReciboDT.actualizar(Recibo)

            '--- Identificar notas de credito vinculadas
            Dim dtNotaCredito As New DataTable
            Dim notaCredito As New documentoCO
            Dim idNotaCredito As String
            dtNotaCredito = objNotaCreditoDT.getNotaCreditoPorRecibo(Recibo.idEncabezado)

            For i As Integer = 0 To dtNotaCredito.Rows.Count() - 1
                Dim objCliente As New ClienteBL
                Dim clientes As New ClienteCO
                clientes = objCliente.getDetalleDelCliente(dtNotaCredito.Rows(i).Item("idcliente"))
                If (id_glo_fel = "X") Then
                    If (Fel.anulacion(dtNotaCredito.Rows(i).Item("serie").ToString(), dtNotaCredito.Rows(i).Item("preimpreso").ToString(), clientes.nit, DateTime.Now, "POR FALTA DE IMPRESION")) Then
                        idNotaCredito = dtNotaCredito.Rows(i).Item("id_encNc")
                        notaCredito = getNotaCredito(idNotaCredito)
                        notaCredito.estado = 2
                        notaCredito.usuarioAnula = id_glo_usuario
                        notaCredito.fechaAnula = "GETDATE()"
                        objNotaCreditoDT.actualizar(notaCredito)
                    Else
                        Return False
                    End If
                Else
                    idNotaCredito = dtNotaCredito.Rows(i).Item("id_encNc")
                    notaCredito = getNotaCredito(idNotaCredito)
                    notaCredito.estado = 2
                    notaCredito.usuarioAnula = id_glo_usuario
                    notaCredito.fechaAnula = "GETDATE()"
                    objNotaCreditoDT.actualizar(notaCredito)
                End If
                
            Next

            '--- Revertir CXC   
            If Not Recibo.idEncCxcRelacionada = Nothing Then
                Dim dtReciboPagos As New DataTable
                Dim importeCredito As Decimal
                Dim dtPagosCredito As New DataTable
                Dim objCliente As New ClienteBL
                Dim pagosCredito As Decimal

                dtPagosCredito = objReciboDT.getPagosCredito(idRecibo)
                dtReciboPagos = objReciboDT.getReciboPagos(Recibo.idEncabezado)

                If Trim(Recibo.idEncCxcRelacionada) <> "0" Then

                    '--- Obtener los pagos de este recibo (CXC)
                    For i As Integer = 0 To dtReciboPagos.Rows.Count - 1
                        importeCredito = dtReciboPagos.Rows(i).Item("importe") + importeCredito
                    Next

                    '--- Agregar otras formas de pago (Envase + Descuentos)
                    importeCredito = importeCredito + Recibo.importeDestoEnv + Recibo.importeDesto

                    '--- Obtener los pagos al credito de este recibo (Factura)
                    For j As Integer = 0 To dtPagosCredito.Rows.Count - 1
                        pagosCredito = pagosCredito + dtPagosCredito.Rows(j).Item("importe")
                    Next

                    '--- Actualizar el credito disponible
                    If Recibo.doTipo = "CXC" Then
                        '-- Revertir
                        objCliente.actualizarCreditoDisponible(cliente, importeCredito * -1)
                    Else
                        '-- Reintegrar
                        objCliente.actualizarCreditoDisponible(cliente, pagosCredito)
                    End If

                    '--- Revertir la CXC por el total 
                    objReciboDT.revertirCxc(importeCredito.ToString(), Recibo.idEncCxcRelacionada)
                End If

                '--- Identificar notas de credito vinculadas
                'Dim dtNotaCredito As New DataTable
                'Dim notaCredito As New documentoCO
                'Dim idNotaCredito As String
                'dtNotaCredito = objNotaCreditoDT.getNotaCreditoPorRecibo(Recibo.idEncabezado)

                'For i As Integer = 0 To dtNotaCredito.Rows.Count() - 1
                'If (Fel.anulacion(dtNotaCredito.Rows(i).Item("serie").ToString(), dtNotaCredito.Rows(i).Item("preimpreso").ToString(), cliente.nit, DateTime.Now, "POR FALTA DE IMPRESION")) Then
                'idNotaCredito = dtNotaCredito.Rows(i).Item("id_encNc")
                'notaCredito = getNotaCredito(idNotaCredito)
                'notaCredito.estado = 2
                'notaCredito.usuarioAnula = id_glo_usuario
                'notaCredito.fechaAnula = "GETDATE()"
                'objNotaCreditoDT.actualizar(notaCredito)
                'End If
                'Next
            End If

            '--- Bitacora Anulación cobro
            objBitacora.registrarOperacion(22, id_glo_cliente)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function anularNotaCredito(ByVal idFactura As String, Optional ByVal motivoAnula As String = "") As Boolean
        Dim Fel As New Generador
        Try
            Dim objRutaBL As New RutaBL
            Dim objBitacora As New BitacoraBL
            Dim Recibo As New documentoCO



            '--- Identificar notas de credito vinculadas
            Dim dtNotaCredito As New DataTable
            Dim notaCredito As New documentoCO
            Dim idNotaCredito As String
            dtNotaCredito = objNotaCreditoDT.getNotaCreditoPorFactura(idFactura)

            For i As Integer = 0 To dtNotaCredito.Rows.Count() - 1

                idNotaCredito = dtNotaCredito.Rows(i).Item("id_encNc")
                notaCredito = getNotaCredito(idNotaCredito)
                notaCredito.estado = 2
                notaCredito.usuarioAnula = id_glo_usuario
                notaCredito.fechaAnula = "GETDATE()"
                objNotaCreditoDT.actualizar(notaCredito)

            Next

            '--- Bitacora Anulación cobro
            objBitacora.registrarOperacion(22, id_glo_cliente)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function validarFormasPago(ByVal dtPagos As DataTable, ByVal dtBancos As DataTable, ByVal cliente As ClienteCO, ByVal valorEnvase As Decimal, ByVal iscontado As Boolean) As Boolean

        Dim items() As DataRow
        Dim dtBancosAlter As New DataTable
        Dim idViaPago, documento As String
        Dim importeViaPago, sumImporte As Decimal
        Dim tienePagocredito As Boolean = False

        'Aqui esta devolviendo una excepcion
        Try
            For i = 0 To dtPagos.Rows.Count - 1
                importeViaPago = objUtilBL.isDecimal(dtPagos.Rows(i).Item(2))
                If importeViaPago < 0 Then
                    MsgBox("No se permite el ingreso de montos negativos en las vias de pago.", MsgBoxStyle.Exclamation)
                    Return False
                End If


                If importeViaPago <> 0 Then
                    sumImporte = sumImporte + importeViaPago
                    dtBancosAlter = dtBancos.Clone
                    idViaPago = dtPagos.Rows(i).Item(0)
                    documento = dtPagos.Rows(i).Item(3)



                    If idViaPago <> "E" Then
                        If idViaPago <> "CR" Then
                            '--- Documento
                            If documento = "" Then
                                MsgBox("La forma de pago " + dtPagos.Rows(i).Item(1) + " requiere el ingreso de un No. de documento. ", MsgBoxStyle.Exclamation)
                                Return False
                            End If
                            '--- Institucion
                            items = dtBancos.Select("showValue='" + dtPagos.Rows(i).Item(4).ToString() + "'")
                            If items.Length <= 0 Or dtPagos.Rows(i).Item(4).ToString() = "--- Seleccione ---" Then
                                MsgBox("La forma de pago " + dtPagos.Rows(i).Item(1) + " requiere seleccionar una institucion. ", MsgBoxStyle.Exclamation)
                                Return False
                            Else
                                For Each row In items
                                    dtPagos.Rows(i).Item(5) = row.Item("dataValue").ToString
                                Next
                            End If
                            '--- Cheque
                            If idViaPago = "H" And dtPagos.Rows(i).Item(5) = 0 Then
                                MsgBox("La forma de pago " + dtPagos.Rows(i).Item(1) + " requiere seleccionar una institucion. ", MsgBoxStyle.Exclamation)
                                Return False
                            End If
                        Else
                            tienePagocredito = True
                            '--- Credito disponible
                            If objUtilBL.isDecimal(cliente.creditoDisponible) < importeViaPago Or objUtilBL.isDecimal(cliente.creditoAutorizado) < importeViaPago Then
                                MsgBox("Se exede el credito autorizado de " + FormatCurrency(cliente.creditoAutorizado, 2) + ". Su disponible es de " + FormatCurrency(cliente.creditoDisponible, 2), MsgBoxStyle.Exclamation)
                                Return False
                            End If
                            dtPagos.Rows(i).Item("documento") = ""
                        End If
                    Else
                        dtPagos.Rows(i).Item("documento") = ""
                    End If
                End If
            Next
        Catch ex As Exception
            MsgBox("Ocurrio un error, favor de verificar los items de PAGO FALTAN COMPLETAR LOS DATOS, Chesques (Bancos) ")
        End Try

        
        If Not iscontado And Not tienePagocredito And Trim(cliente.categoria) <> "08" Then
            MsgBox("Debe escribir un monto en la forma de pago credito. Si desea liquidar la factura regrese una pantalla y selecione el pago de contado", MsgBoxStyle.Exclamation)
            Return False
        End If


        If sumImporte <= 0 And valorEnvase <= 0 Then
            MsgBox("La sumatoria de pagos individuales no puede ser menor o igual que. 0 ", MsgBoxStyle.Exclamation)
            Return False
        End If
        Return True
    End Function

    Public Function actualizarRecibo(ByVal Recibo As documentoCO) As Boolean
        objReciboDT.actualizar(Recibo)
    End Function

    Public Function actualizarReciboDetalle(ByVal Recibo As documentoCO, ByVal Factura As documentoCO) As Boolean
        objReciboDT.actualizarDetalle(Recibo, Factura)
    End Function

    Private Function anularReciboPorCXC(ByVal idEncCXC As String, ByVal cliente As ClienteCO) As Boolean
        If idEncCXC = "0" Then Return False
        Dim dtRecibos As New DataTable
        dtRecibos = objReciboDT.getReciboByCXC(idEncCXC)
        For i As Integer = 0 To dtRecibos.Rows.Count - 1
            anularRecibo(dtRecibos(i).Item("id_EncRecibo").ToString, cliente)
        Next
    End Function

#End Region

#Region " VALIDAR DOCUMENTO RECIBO "

    Public Function validaRecibos(ByVal recibo As documentoCO) As Boolean
        Dim importePagoAcumulado As Decimal = 0
        Dim Cantidad As Integer
        Try
            'Crear el encabezado del recibo
            Cantidad = objReciboDT.cantRecibo(recibo)
            If (Cantidad > 1) Then
                MsgBox("Cantidad de recibos " & Cantidad)
                objReciboDT.actualizarEstado(recibo)
            End If
            Return True
        Catch ex As Exception
            MsgBox("ERROR al crear recibo de pago: " + ex.Message)
        End Try
    End Function

    Public Function validaRecibo(ByVal idRecibo As String) As Boolean
        If (objReciboDT.VerificaRecibo(idRecibo)) Then
            'Si todo se ejecuta bien 
        Else
            'Si algo se ejecuta mal
        End If
    End Function
#End Region

#Region " VALIDAR DOCUMENTO FACTURA "
    Public Function validaFactura(ByVal idFactura As String, ByVal idRecibo As String) As Boolean
        Dim check As Boolean = False
        Try
            If (objReciboDT.VerificaFactura(idFactura, idRecibo)) Then
                'Todo bien
                If (objReciboDT.VerificaRecibo(idRecibo)) Then
                    check = True
                Else
                    check = False
                End If
                Return check
            Else
                Return check
            End If
        Catch ex As Exception
            MessageBox.Show("Ocurrio un error en ValidaFactura " + ex.Message)
            Return check
        End Try


    End Function
#End Region


#Region " VALIDAR SI CONTIENE FEL "
    Public Function validaDocumentoFEL(ByVal idFactura As String) As Boolean
        Dim check As Boolean = False
        Try
            If (objReciboDT.VerificaFacturaFEL(idFactura)) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show("Ocurrio un error en ValidaFactura " + ex.Message)
            Return check
        End Try
    End Function

    Public Function numeroContingencia() As String
        Dim check As Boolean = False
        Dim respuesta As String = ""
        Try
            respuesta = objReciboDT.NumeroAccesoC
            If (respuesta <> "") Then
                Return respuesta
            Else
                Return ""
            End If
        Catch ex As Exception
            MessageBox.Show("Ocurrio un error en ValidaFactura " + ex.Message)
            Return ""
        End Try
    End Function

    Public Function validaDocumentoFELNC(ByVal idNC As String) As Boolean
        Dim check As Boolean = False
        Try
            If (objReciboDT.VerificaFacturaFELNC(idNC)) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show("Ocurrio un error en ValidaFactura " + ex.Message)
            Return check
        End Try
    End Function

    Public Function validaContingenciaFACT(ByVal idFactura As String) As Boolean
        Dim check As Boolean = False
        Try
            If (objReciboDT.VerificaFacturaContingencia(idFactura)) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show("Ocurrio un error en ValidaFactura " + ex.Message)
            Return check
        End Try
    End Function

    Public Function validaContingenciaNC(ByVal idNC As String) As Boolean
        Dim check As Boolean = False
        Try
            If (objReciboDT.VerificaFacturaContingenciaNC(idNC)) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show("Ocurrio un error en ValidaFactura " + ex.Message)
            Return check
        End Try
    End Function
#End Region

#Region " CUENTA POR COBRAR "
    Public Function ActualizarCuentaPorCobrar(ByVal nuevoSerie As String, ByVal nuevoNumero As String, ByVal idCxc As String) As Boolean
        Return objReciboDT.actualizaidCxc(nuevoSerie, nuevoNumero, idCxc)
    End Function

    Public Function pagosRealizadosCXC(ByVal id_cxc As String, ByRef montoTotal As Decimal, ByRef pago As Decimal) As rLayerHandler
        '    Retorna en las variables por referencia el monto total de la CXC,
        '    el monto total de pagos realizados (Envase + Contado) durante la jornada  
        '    de la CXC indicada en el parametro id_cxc

        Dim rLayer As New rLayerHandler
        Dim dtPagos As New DataTable

        '--- Recuperar la linea con los pagos realizados
        dtPagos = objReciboDT.getPagosRealizadosCXC(id_cxc, rLayer)

        '--- Registra si hubo algun mensaje de error
        rLayer.evaluarError()
        If Not dtPagos Is Nothing Then
            If dtPagos.Rows.Count > 0 Then
                rLayer.codigo = 0

                '--- Cambiar el valor de las variables por referencia
                montoTotal = dtPagos.Rows(0).Item("montoTotal")
                pago = dtPagos.Rows(0).Item("pago")

            Else
                rLayer.codigo = 1
                rLayer.texto = "Esta CXC no ha tenido pagos en este dia. "
            End If
        Else
            rLayer.codigo = 1
            rLayer.texto = "No se encontro el codigo " & id_cxc & " de cxc en el listado de pagos realizados."
        End If
        Return rLayer
    End Function

    Public Function getSaldoCliente(ByVal idCliente As String, ByRef saldo As Decimal) As rLayerHandler
        '    Retorna en la variable por referencia el monto total del saldo
        '    que es la suma de los recibos de ODV que sean de tipo CR

        Dim rLayer As New rLayerHandler
        Dim dtsaldo As DataTable

        '--- Recuperar la linea con los pagos realizados
        dtsaldo = objReciboDT.getPagosCredito(idCliente, rLayer)

        '--- Registra si hubo algun mensaje de error
        If rLayer.evaluaTabla(dtsaldo) Then
            saldo = objUtilBL.isDecimal(dtsaldo.Rows(0).Item("saldo").ToString)
        Else
            saldo = 0
        End If
        Return rLayer
    End Function
#End Region

#Region " NOTA DE CREDITO "

    Public Function crearNotaDeCredito(ByVal lstDetalle As Windows.Forms.ListView, ByRef NotaDeCredito As documentoCO) As Boolean
        Dim comItem As New ItemCO
        Dim comProducto As New ProductoCO
        Dim objProductoBL As New ProductoBL


        Try
            'Crear el encabezado de la nota de credito
            NotaDeCredito.idEncabezado = objNotaCreditoDT.crearEncabezado(NotaDeCredito)

            'Crear el detalle de la nota de credito
            For i As Integer = 0 To lstDetalle.Items.Count() - 1
                If Not lstDetalle.Items(i).SubItems(10).Text = "" Then

                    '--- Obtener el detalle del producto          
                    comItem.idProducto = lstDetalle.Items.Item(i).SubItems(10).Text
                    comProducto = objProductoBL.getDetalleDelProducto(comItem.idProducto)

                    '--- Capturar la informacion del producto agregado
                    comItem.idEncabezado = NotaDeCredito.idEncabezado
                    comItem.NoItem = i + 1
                    comItem.idProducto = lstDetalle.Items.Item(i).SubItems(10).Text
                    comItem.um = "UN"
                    comItem.cj = objUtilBL.isDecimal(lstDetalle.Items.Item(i).SubItems(5).Text)
                    comItem.un = objUtilBL.isDecimal(lstDetalle.Items.Item(i).SubItems(6).Text)
                    comItem.cantidad = comItem.cj * comProducto.unidadesCaja + comItem.un
                    comItem.precio = (lstDetalle.Items.Item(i).SubItems(11).Text)
                    comItem.importe = lstDetalle.Items.Item(i).SubItems(7).Text
                    comItem.importeSinIva = lstDetalle.Items.Item(i).SubItems(12).Text
                    comItem.iva = co_glo_porcentajeIVA
                    comItem.idRubro = (lstDetalle.Items.Item(i).SubItems(3).Text)
                    comItem.trqt = comItem.cj + "/" + comItem.un
                    comItem.litm = lstDetalle.Items.Item(i).SubItems(0).Text
                    comItem.estado = 0

                    '--- Calcular los descuentos para este item  
                    'Descuento Normal
                    comItem.porcentajeDesto = 0
                    comItem.importeDesto = comItem.porcentajeDesto * comItem.precio

                    'Descuento DPP
                    comItem.porcentajeDesto = 0
                    comItem.importeDestoPP = comItem.porcentajeDestoPP * comItem.precio

                    '--- Agregar el item al detalle de la Orden de Venta
                    objNotaCreditoDT.agregarItem(comItem)

                End If
            Next

            'Validar si tiene detalle el documento de lo contrario se genera anulado


            NotaDeCredito.importe = objNotaCreditoDT.actualizar(NotaDeCredito.idEncabezado)

            Return True
        Catch ex As Exception
            MsgBox("Error al crear la nota de credito" + ex.Message())
            Return False
        End Try

    End Function

    Public Function crearNotaDeCreditoNA(ByVal lstDetalle As Windows.Forms.ListView, ByRef NotaDeCredito As documentoCO) As Boolean
        Dim comItem As New ItemCO
        Dim comProducto As New ProductoCO
        Dim objProductoBL As New ProductoBL


        Try
            'Crear el encabezado de la nota de credito
            NotaDeCredito.idEncabezado = objNotaCreditoDT.crearEncabezadoNA(NotaDeCredito)

            'Crear el detalle de la nota de credito
            For i As Integer = 0 To lstDetalle.Items.Count() - 1
                If Not lstDetalle.Items(i).SubItems(10).Text = "" Then

                    '--- Obtener el detalle del producto          
                    comItem.idProducto = lstDetalle.Items.Item(i).SubItems(10).Text
                    comProducto = objProductoBL.getDetalleDelProducto(comItem.idProducto)

                    '--- Capturar la informacion del producto agregado
                    comItem.idEncabezado = NotaDeCredito.idEncabezado
                    comItem.NoItem = i + 1
                    comItem.idProducto = lstDetalle.Items.Item(i).SubItems(10).Text
                    comItem.um = "UN"
                    comItem.cj = objUtilBL.isDecimal(lstDetalle.Items.Item(i).SubItems(5).Text)
                    comItem.un = objUtilBL.isDecimal(lstDetalle.Items.Item(i).SubItems(6).Text)
                    comItem.cantidad = comItem.cj * comProducto.unidadesCaja + comItem.un
                    comItem.precio = (lstDetalle.Items.Item(i).SubItems(11).Text)
                    comItem.importe = lstDetalle.Items.Item(i).SubItems(7).Text
                    comItem.importeSinIva = lstDetalle.Items.Item(i).SubItems(12).Text
                    comItem.iva = co_glo_porcentajeIVA
                    comItem.idRubro = (lstDetalle.Items.Item(i).SubItems(3).Text)
                    comItem.trqt = comItem.cj + "/" + comItem.un
                    comItem.litm = lstDetalle.Items.Item(i).SubItems(0).Text
                    comItem.estado = 0

                    '--- Calcular los descuentos para este item  
                    'Descuento Normal
                    comItem.porcentajeDesto = 0
                    comItem.importeDesto = comItem.porcentajeDesto * comItem.precio

                    'Descuento DPP
                    comItem.porcentajeDesto = 0
                    comItem.importeDestoPP = comItem.porcentajeDestoPP * comItem.precio

                    '--- Agregar el item al detalle de la Orden de Venta
                    objNotaCreditoDT.agregarItem(comItem)

                End If
            Next

            'Validar si tiene detalle el documento de lo contrario se genera anulado


            NotaDeCredito.importe = objNotaCreditoDT.actualizar(NotaDeCredito.idEncabezado)

            Return True
        Catch ex As Exception
            MsgBox("Error al crear la nota de credito" + ex.Message())
            Return False
        End Try

    End Function

    Public Function crearNotaDeCreditoDPP(ByVal dtItems As DataTable, ByVal NotaDeCredito As documentoCO) As Boolean
        Dim comItem As New ItemCO
        Dim comProducto As New ProductoCO
        Dim objProductoBL As New ProductoBL
        Dim importeDesto As Decimal = 0

        Try
            'Crear el encabezado de la nota de credito
            NotaDeCredito.idEncabezado = objNotaCreditoDT.crearEncabezado(NotaDeCredito)

            'Crear el detalle de la nota de credito
            For i As Integer = 0 To dtItems.Rows.Count() - 1


                '--- Obtener el detalle del producto          
                comItem.idProducto = dtItems.Rows(i).Item("idProducto")
                comProducto = objProductoBL.getDetalleDelProducto(comItem.idProducto)

                '--- Capturar la informacion del producto agregado
                comItem.idEncabezado = NotaDeCredito.idEncabezado
                comItem.NoItem = i + 1
                comItem.idProducto = dtItems.Rows(i).Item("idProducto")
                comItem.um = "UN"
                comItem.cj = dtItems.Rows(i).Item("cajas")
                comItem.un = dtItems.Rows(i).Item("unidades")
                comItem.cantidad = objUtilBL.isDecimal(comProducto.unidadesCaja) * objUtilBL.isDecimal(comItem.cj) + objUtilBL.isDecimal(comItem.un)
                comItem.precio = 0
                comItem.importe = dtItems.Rows(i).Item("importe")
                comItem.importeSinIva = comItem.importe / (1 + co_glo_porcentajeIVA)
                comItem.iva = co_glo_porcentajeIVA
                comItem.idRubro = ""
                comItem.trqt = comItem.cj + "/" + comItem.un
                comItem.litm = dtItems.Rows(i).Item("idProducto")
                comItem.estado = 0
                comItem.porcentajeDesto = dtItems.Rows(i).Item("porcentajeDesto").ToString()
                importeDesto = objUtilBL.isDecimal(dtItems.Rows(i).Item("importeDesto"))
                importeDesto = Math.Abs(importeDesto)
                comItem.importeDesto = importeDesto.ToString()
                comItem.importeDestoPP = 0

                '--- Agregar el item al detalle de la nota de credito
                objNotaCreditoDT.agregarItem(comItem)

            Next
            Return True
        Catch ex As Exception
            MsgBox("Error al crear la nota de credito" + ex.Message())
            Return False
        End Try

    End Function

    Public Function crearNotaDeCreditoDPPNA(ByVal dtItems As DataTable, ByVal NotaDeCredito As documentoCO) As Boolean
        Dim comItem As New ItemCO
        Dim comProducto As New ProductoCO
        Dim objProductoBL As New ProductoBL
        Dim importeDesto As Decimal = 0

        Try
            'Crear el encabezado de la nota de credito
            NotaDeCredito.idEncabezado = objNotaCreditoDT.crearEncabezadoNA(NotaDeCredito)

            'Crear el detalle de la nota de credito
            For i As Integer = 0 To dtItems.Rows.Count() - 1


                '--- Obtener el detalle del producto          
                comItem.idProducto = dtItems.Rows(i).Item("idProducto")
                comProducto = objProductoBL.getDetalleDelProducto(comItem.idProducto)

                '--- Capturar la informacion del producto agregado
                comItem.idEncabezado = NotaDeCredito.idEncabezado
                comItem.NoItem = i + 1
                comItem.idProducto = dtItems.Rows(i).Item("idProducto")
                comItem.um = "UN"
                comItem.cj = dtItems.Rows(i).Item("cajas")
                comItem.un = dtItems.Rows(i).Item("unidades")
                comItem.cantidad = objUtilBL.isDecimal(comProducto.unidadesCaja) * objUtilBL.isDecimal(comItem.cj) + objUtilBL.isDecimal(comItem.un)
                comItem.precio = 0
                comItem.importe = dtItems.Rows(i).Item("importe")
                comItem.importeSinIva = comItem.importe / (1 + co_glo_porcentajeIVA)
                comItem.iva = co_glo_porcentajeIVA
                comItem.idRubro = ""
                comItem.trqt = comItem.cj + "/" + comItem.un
                comItem.litm = dtItems.Rows(i).Item("idProducto")
                comItem.estado = 0
                comItem.porcentajeDesto = dtItems.Rows(i).Item("porcentajeDesto").ToString()
                importeDesto = objUtilBL.isDecimal(dtItems.Rows(i).Item("importeDesto"))
                importeDesto = Math.Abs(importeDesto)
                comItem.importeDesto = importeDesto.ToString()
                comItem.importeDestoPP = 0
                '--- Agregar el item al detalle de la nota de credito
                objNotaCreditoDT.agregarItem(comItem)
            Next
            Return True
        Catch ex As Exception
            MsgBox("Error al crear la nota de Abono" + ex.Message())
            Return False
        End Try

    End Function


    Public Function actualizarNc(ByVal nc As documentoCO) As Boolean
        Return objNotaCreditoDT.actualizar(nc)
    End Function

    Public Function copiarNc(ByVal idNC As String) As Integer
        Return objNotaCreditoDT.setCopiaNC(idNC)
    End Function

    Private Function anularNcPorReimpresion(ByVal idRecibo As String, ByRef cliente As ClienteCO, ByVal factura As documentoCO) As Boolean


        Dim objRuta As New RutaBL
        Dim objBitacora As New BitacoraBL
        Dim objImpresion As New ImpresionBL

        Dim ncc As New CorrelativoCO

        Try


            '--- Identificar notas de credito vinculadas
            Dim dtNotaCredito As New DataTable
            Dim notaCredito As New documentoCO
            Dim idNotaCredito As String

            dtNotaCredito = objNotaCreditoDT.getNotaCreditoPorRecibo(idRecibo)

            For i As Integer = 0 To dtNotaCredito.Rows.Count() - 1
                idNotaCredito = dtNotaCredito.Rows(i).Item("id_encNc")
                notaCredito = getNotaCredito(idNotaCredito)
                notaCredito.estado = 2
                notaCredito.usuarioAnula = id_glo_usuario
                notaCredito.fechaAnula = "GETDATE()"
                notaCredito.fechaEmision = "fechaEmision"
                notaCredito.fechaVence = "fechaVence"
                actualizarNc(notaCredito)
                notaCredito.idEncabezado = copiarNc(notaCredito.idEncabezado)

                '--- Consumir un correlativo nuevo
                objRuta.obtenerCorrelativos(Nothing, Nothing, ncc)
                notaCredito.estado = 1
                notaCredito.serie = ncc.serie
                notaCredito.numero = ncc.actual + 1
                notaCredito.fechaAnula = Nothing
                notaCredito.idEncFacturaRelacionada = factura.idEncabezado
                notaCredito.fechaAnula = "fechaAnula"

                '--- Actualizar la Nota de credito
                If actualizarNc(notaCredito) Then actualizarNcDetalle(idNotaCredito, notaCredito.idEncabezado)

                '--- Incrementar el correlativo 
                actualizarCorrelativo("NC")

                '--- Reimprimir el documento
                objImpresion.ImprimeNotaCredito(notaCredito.idEncabezado, False, factura.serie + factura.numero)


            Next
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function actualizarNcDetalle(ByVal idNcOriginal As String, ByVal idNcCopia As String) As Boolean
        '--- Obtener la version actual del documento porque sino pueden haber inconsistencias en los datos
        objNotaCreditoDT.actualizarDetalle(idNcOriginal, idNcCopia)
    End Function

#End Region

#Region " INVENTARIO FISICO "
    Public Function agregarInventarioFisico(ByVal lstDetalle As Windows.Forms.ListView, ByVal OrdenVenta As documentoCO) As Boolean
        Dim comItem As New ItemCO
        Dim comProducto As New ProductoCO
        Dim objProductoBL As New ProductoBL
        Dim oInventario As New Inventario

        Dim dtZcampos As New DataTable
        For i As Integer = 0 To lstDetalle.Items.Count - 1
            If Not lstDetalle.Items(i).SubItems(10).Text = "" Then
                comItem.idProducto = lstDetalle.Items.Item(i).SubItems(10).Text
                comProducto = objProductoBL.getDetalleDelProducto(comItem.idProducto)
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
                comItem.tipoVenta = "V"
                comItem.idRubro = (lstDetalle.Items.Item(i).SubItems(3).Text)
                comItem.trqt = comItem.cj + "/" + comItem.un
                comItem.litm = lstDetalle.Items.Item(i).SubItems(0).Text
                comItem.estado = 0

                '--- Agregar el item 
                oInventario.agregarItem(comItem)
            End If
        Next
        Return True
    End Function
#End Region

#Region " SOPORTE A IMPRESIONES "
    Public Function getFactura(ByVal idFactura As String) As documentoCO
        Dim comfactura As New documentoCO
        Dim dtFactura As New DataTable
        dtFactura = objFacturaDT.getFactura(idFactura)
        If dtFactura.Rows.Count > 0 Then
            comfactura.idEncabezado = dtFactura.Rows(0).Item("id_encFactura").ToString()
            comfactura.ruta = dtFactura.Rows(0).Item("idRuta").ToString()
            comfactura.serie = Trim(dtFactura.Rows(0).Item("fserie").ToString())
            comfactura.numero = Trim(dtFactura.Rows(0).Item("fnumero").ToString())
            comfactura.serieFEL = Trim(dtFactura.Rows(0).Item("serie").ToString())
            comfactura.UUID = Trim(dtFactura.Rows(0).Item("numeroautorizacion").ToString())
            comfactura.preimpreso = Trim(dtFactura.Rows(0).Item("preimpreso").ToString())
            If (dtFactura.Rows(0).Item("fechaEmision").ToString = "") Then
                comfactura.fechaEmision = "null"
            Else
                comfactura.fechaEmision = dtFactura.Rows(0).Item("fechaEmision").ToString()
            End If


            If (dtFactura.Rows(0).Item("fechaVence").ToString = "") Then
                comfactura.fechaVence = "null"
            Else
                comfactura.fechaVence = dtFactura.Rows(0).Item("fechaVence").ToString()
            End If

            comfactura.idCliente = dtFactura.Rows(0).Item("idCliente").ToString()
            comfactura.nit = dtFactura.Rows(0).Item("nit").ToString()
            comfactura.importe = objUtilBL.isDecimal(dtFactura.Rows(0).Item("importe").ToString())
            comfactura.moneda = dtFactura.Rows(0).Item("moneda").ToString()
            comfactura.importeDesto = objUtilBL.isDecimal(dtFactura.Rows(0).Item("importeDesto").ToString())
            comfactura.importeDestoPP = objUtilBL.isDecimal(dtFactura.Rows(0).Item("importeDestoPP").ToString())
            comfactura.importeDestoEnv = objUtilBL.isDecimal(dtFactura.Rows(0).Item("importeDestoEnv").ToString())
            comfactura.usuarioAnula = dtFactura.Rows(0).Item("usuarioAnula").ToString()
            If (dtFactura.Rows(0).Item("fechaAnula").ToString = "") Then
                comfactura.fechaAnula = "null"
            Else
                comfactura.fechaAnula = dtFactura.Rows(0).Item("fechaAnula").ToString()
            End If
            comfactura.porcentajeIva = dtFactura.Rows(0).Item("porcentajeIva").ToString()
            comfactura.ttipo = Trim(dtFactura.Rows(0).Item("tTipo").ToString())
            comfactura.idusuario = dtFactura.Rows(0).Item("idusuario").ToString()
            comfactura.estado = dtFactura.Rows(0).Item("estado").ToString()
            comfactura.idReciboRelacionado = dtFactura.Rows(0).Item("idEncRecibo").ToString()
            comfactura.condicion = dtFactura.Rows(0).Item("condicion").ToString()
            comfactura.idPedido = dtFactura.Rows(0).Item("id_pedido").ToString()
            comfactura.nImpresiones = dtFactura.Rows(0).Item("nImpresiones").ToString()
            comfactura.noEntrega = dtFactura.Rows(0).Item("noEntrega").ToString()
            comfactura.noPedido = dtFactura.Rows(0).Item("noPedido").ToString()
            comfactura.noResolucion = dtFactura.Rows(0).Item("noResolucion").ToString()
            comfactura.fechaResolucion = dtFactura.Rows(0).Item("fechaResolucion").ToString()
            comfactura.inicial = dtFactura.Rows(0).Item("inicial").ToString()
            comfactura.final = dtFactura.Rows(0).Item("final").ToString()
            comfactura.idPedido = dtFactura.Rows(0).Item("id_pedido").ToString()
            comfactura.serieFEL = dtFactura.Rows(0).Item("serie").ToString()
            comfactura.preimpreso = dtFactura.Rows(0).Item("preimpreso").ToString()
            comfactura.UUID = dtFactura.Rows(0).Item("numeroautorizacion").ToString()
            comfactura.direccionfel = dtFactura.Rows(0).Item("direccionfel").ToString()
            comfactura.numeroacceso = dtFactura.Rows(0).Item("numeroacceso").ToString()
            comfactura.tipoReceptor = dtFactura.Rows(0).Item("tipo_receptor").ToString()
            comfactura.idreceptor = dtFactura.Rows(0).Item("idreceptor").ToString()
            comfactura.nombre_fel = dtFactura.Rows(0).Item("nombre_fel").ToString()
        Else
            Return Nothing
        End If
        Return comfactura
    End Function


    Public Function getFacturaCambio(ByVal idFactura As String) As documentoCO
        Dim comfactura As New documentoCO
        Dim dtFactura As New DataTable
        dtFactura = objFacturaDT.getFacturaCambio(idFactura)
        If dtFactura.Rows.Count > 0 Then
            comfactura.idEncabezado = dtFactura.Rows(0).Item("id_encFactura").ToString()
            comfactura.ruta = dtFactura.Rows(0).Item("idRuta").ToString()
            comfactura.serie = Trim(dtFactura.Rows(0).Item("fserie").ToString())
            comfactura.numero = Trim(dtFactura.Rows(0).Item("fnumero").ToString())

            If (dtFactura.Rows(0).Item("fechaEmision").ToString = "") Then
                comfactura.fechaEmision = "null"
            Else
                comfactura.fechaEmision = dtFactura.Rows(0).Item("fechaEmision").ToString()
            End If


            If (dtFactura.Rows(0).Item("fechaVence").ToString = "") Then
                comfactura.fechaVence = "null"
            Else
                comfactura.fechaVence = dtFactura.Rows(0).Item("fechaVence").ToString()
            End If

            comfactura.idCliente = dtFactura.Rows(0).Item("idCliente").ToString()
            comfactura.nit = dtFactura.Rows(0).Item("nit").ToString()
            comfactura.importe = objUtilBL.isDecimal(dtFactura.Rows(0).Item("importe").ToString())
            comfactura.moneda = dtFactura.Rows(0).Item("moneda").ToString()
            comfactura.importeDesto = objUtilBL.isDecimal(dtFactura.Rows(0).Item("importeDesto").ToString())
            comfactura.importeDestoPP = objUtilBL.isDecimal(dtFactura.Rows(0).Item("importeDestoPP").ToString())
            comfactura.importeDestoEnv = objUtilBL.isDecimal(dtFactura.Rows(0).Item("importeDestoEnv").ToString())
            comfactura.usuarioAnula = dtFactura.Rows(0).Item("usuarioAnula").ToString()
            If (dtFactura.Rows(0).Item("fechaAnula").ToString = "") Then
                comfactura.fechaAnula = "null"
            Else
                comfactura.fechaAnula = dtFactura.Rows(0).Item("fechaAnula").ToString()
            End If
            comfactura.porcentajeIva = dtFactura.Rows(0).Item("porcentajeIva").ToString()
            comfactura.ttipo = Trim(dtFactura.Rows(0).Item("tTipo").ToString())
            comfactura.idusuario = dtFactura.Rows(0).Item("idusuario").ToString()
            comfactura.estado = dtFactura.Rows(0).Item("estado").ToString()
            comfactura.idReciboRelacionado = dtFactura.Rows(0).Item("idEncRecibo").ToString()
            comfactura.condicion = dtFactura.Rows(0).Item("condicion").ToString()
            comfactura.idPedido = dtFactura.Rows(0).Item("id_pedido").ToString()
            comfactura.nImpresiones = dtFactura.Rows(0).Item("nImpresiones").ToString()
            comfactura.noEntrega = dtFactura.Rows(0).Item("noEntrega").ToString()
            comfactura.noPedido = dtFactura.Rows(0).Item("noPedido").ToString()
            comfactura.noResolucion = dtFactura.Rows(0).Item("noResolucion").ToString()
            comfactura.fechaResolucion = dtFactura.Rows(0).Item("fechaResolucion").ToString()
            comfactura.inicial = dtFactura.Rows(0).Item("inicial").ToString()
            comfactura.final = dtFactura.Rows(0).Item("final").ToString()
            comfactura.idPedido = dtFactura.Rows(0).Item("id_pedido").ToString()
        Else
            Return Nothing
        End If
        Return comfactura
    End Function


    Public Function getFacturaByItem(ByVal idItem As String, ByVal rowFactura As DataRow) As ItemCO
        Dim item As New ItemCO
        Dim split_trqt As String()
        item.NoItem = rowFactura.Item("item").ToString()
        item.idProducto = rowFactura.Item("idProducto").ToString()
        item.cantidad = rowFactura.Item("cantidad").ToString()
        item.um = rowFactura.Item("um").ToString()
        item.precio = rowFactura.Item("precio").ToString()
        item.importe = rowFactura.Item("importe").ToString()
        item.iva = rowFactura.Item("iva").ToString()
        item.porcentajeDesto = rowFactura.Item("porcentajeDesto").ToString()
        item.importeDesto = objUtilBL.isDecimal(rowFactura.Item("importeDesto").ToString())
        item.valorIvaDesto = objUtilBL.isDecimal(rowFactura.Item("valorIvaDesto").ToString())
        item.tipoVenta = rowFactura.Item("tipoVenta").ToString()
        item.porcentajeDestoPP = rowFactura.Item("porcentajeDestoPP").ToString()
        item.importeDestoPP = objUtilBL.isDecimal(rowFactura.Item("importeDestoPP").ToString())
        item.idRubro = rowFactura.Item("idRubro").ToString()
        If rowFactura.Item("trqt").ToString() = "" Then
            item.trqt = "e/e"
        Else
            item.trqt = rowFactura.Item("trqt").ToString()
        End If
        item.litm = rowFactura.Item("litm").ToString()
        item.estado = rowFactura.Item("estado").ToString()
        item.descripcion = rowFactura.Item("descripcion").ToString()
        item.posicionSuperior = rowFactura.Item("posicionSuperior").ToString()

        split_trqt = item.trqt.Split("/")
        item.cajas = split_trqt(0)
        item.unidades = split_trqt(1)
        Return item
        Return Nothing
    End Function

    Public Function getFacturaDetalle(ByVal idFactura As String) As DataTable
        Dim dtFacturaDetalle As New DataTable
        dtFacturaDetalle = objFacturaDT.getFacturaDetalle(idFactura)
        If dtFacturaDetalle.Rows.Count > 0 Then
            Return dtFacturaDetalle
        Else
            Throw New Exception("No fue posible recuperar los datos de la factura")
            Return Nothing
        End If
    End Function

    Public Function getPagoReciboFactura(ByVal idFactura As String, ByVal tipoPago As String, ByVal idRecibo As String) As Boolean
        Dim dtFacturaDetalle As New DataTable
        Try
            dtFacturaDetalle = objFacturaDT.getFacturaPagoEfectivo(idFactura, tipoPago, idRecibo)
            If dtFacturaDetalle.Rows.Count > 0 Then
                Return True
            Else
                'Throw New Exception("No fue posible recuperar los datos de la factura")
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function getNotaDetalle(ByVal idFactura As String) As DataTable
        Dim dtFacturaDetalle As New DataTable
        dtFacturaDetalle = objFacturaDT.getNotaDetalle(idFactura)
        If dtFacturaDetalle.Rows.Count > 0 Then
            Return dtFacturaDetalle
        Else
            Throw New Exception("No fue posible recuperar los datos de la factura")
            Return Nothing
        End If
    End Function

    Public Function getNotaCredito(ByVal idNc As String) As documentoCO
        Dim comNc As New documentoCO
        Dim dtNc As New DataTable
        dtNc = objNotaCreditoDT.getNotaDeCredito(idNc)
        If dtNc.Rows.Count > 0 Then
            comNc.idEncabezado = dtNc.Rows(0).Item("id_encNc").ToString()
            comNc.ruta = dtNc.Rows(0).Item("idRuta").ToString()
            comNc.serie = Trim(dtNc.Rows(0).Item("ncSerie").ToString())
            comNc.numero = Trim(dtNc.Rows(0).Item("ncNumero").ToString())
            comNc.idEncFacturaRelacionada = Trim(dtNc.Rows(0).Item("idEncFactura").ToString())

            If (dtNc.Rows(0).Item("fechaEmision").ToString = "") Then
                comNc.fechaEmision = "null"
            Else
                comNc.fechaEmision = dtNc.Rows(0).Item("fechaEmision").ToString()
            End If
            comNc.idCliente = dtNc.Rows(0).Item("idCliente").ToString()
            comNc.importe = dtNc.Rows(0).Item("importe").ToString()
            comNc.moneda = dtNc.Rows(0).Item("moneda").ToString()
            comNc.ttipo = dtNc.Rows(0).Item("tTipo").ToString()
            comNc.tipoDevolucion = dtNc.Rows(0).Item("tipoDevolucion").ToString()
            comNc.usuarioAnula = dtNc.Rows(0).Item("usuarioAnula").ToString()
            If (dtNc.Rows(0).Item("fechaAnula").ToString = "") Then
                comNc.fechaAnula = "null"
            Else
                comNc.fechaAnula = dtNc.Rows(0).Item("fechaAnula").ToString()
            End If

            comNc.nImpresiones = dtNc.Rows(0).Item("nImpresiones").ToString()
            comNc.porcentajeIva = dtNc.Rows(0).Item("porcentajeIva").ToString()
            comNc.idusuario = dtNc.Rows(0).Item("idusuario").ToString()
            comNc.doTipo = dtNc.Rows(0).Item("doTipo").ToString()
            comNc.estado = dtNc.Rows(0).Item("estado").ToString()
            comNc.noResolucion = dtNc.Rows(0).Item("noResolucion").ToString()
            comNc.fechaResolucion = dtNc.Rows(0).Item("fechaResolucion").ToString()
            comNc.inicial = dtNc.Rows(0).Item("inicial").ToString()
            comNc.final = dtNc.Rows(0).Item("final").ToString()
            comNc.serieFEL = dtNc.Rows(0).Item("serie").ToString()
            comNc.preimpreso = dtNc.Rows(0).Item("preimpreso").ToString()
            comNc.UUID = dtNc.Rows(0).Item("numeroautorizacion").ToString()
            comNc.direccionfel = dtNc.Rows(0).Item("direccionfel").ToString()
            comNc.numeroacceso = dtNc.Rows(0).Item("numeroacceso").ToString()
            comNc.idReciboRelacionado = Trim(dtNc.Rows(0).Item("idEncRecibo").ToString())
            comNc.tipoReceptor = Trim(dtNc.Rows(0).Item("tipo_receptor").ToString())
            comNc.idreceptor = Trim(dtNc.Rows(0).Item("idreceptor").ToString())
            comNc.nombre_fel = Trim(dtNc.Rows(0).Item("nombre_fel").ToString())
        Else
            comNc.importe = 0
            Return comNc

        End If
        Return comNc
    End Function

    Public Function actualizarDetalleNC(ByVal id_detNC, ByVal idEncNC, ByVal idProducto, ByVal Importe, ByVal ImporteSinIva) As Boolean
        Return objNotaCreditoDT.actualizarNC(id_detNC, idEncNC, idProducto, Importe, ImporteSinIva)
    End Function

    Public Function actualizarDetalleFact(ByVal id_detFACT, ByVal idEncFACT, ByVal idProducto, ByVal Importe, ByVal ImporteSinIva) As Boolean
        Return objNotaCreditoDT.actualizarFacturacion(id_detFACT, idEncFACT, idProducto, Importe, ImporteSinIva)
    End Function


    Public Function actualizarNCImporte(ByVal IdNC, ByVal vBruto) As Boolean
        Return objNotaCreditoDT.actualizarNCImporte(IdNC, vBruto)
    End Function

    Public Function actualizarFACTImporte(ByVal Fact, ByVal Diferencia, ByVal Recibo) As Boolean
        Return objNotaCreditoDT.actualizarFACTImporte(Fact, Diferencia, Recibo)
    End Function

    Public Function actualizarReciboPagos(ByVal Fact, ByVal Diferencia, ByVal ViaPago) As Boolean
        Return objNotaCreditoDT.actualizarReciboPagosImporte(Fact, Diferencia, ViaPago)
    End Function

    Public Function actualizarReciboPagos2(ByVal Fact, ByVal Diferencia, ByVal ViaPago) As Boolean
        Return objNotaCreditoDT.actualizarReciboPagosImporte2(Fact, Diferencia, ViaPago)
    End Function

    Public Function actualizarReciboImporte(ByVal Fact, ByVal vBruto) As Boolean
        Return objNotaCreditoDT.actualizarRECImporte(Fact, vBruto)
    End Function

    Public Function actualizarReciboImp(ByVal Fact, ByVal vBruto) As Boolean
        Return objNotaCreditoDT.actualizarRECImporte(Fact, vBruto)
    End Function
    '

    Public Function actualizarNCIMPORTEDESTOENV(ByVal factura, ByVal ImporteEnvase) As Boolean
        Return objNotaCreditoDT.actualizarFACTImporteEnvase(factura, ImporteEnvase)
    End Function

    Public Function diferenciaRecibo_pagos(ByVal factura, ByVal vBruto) As Decimal
        Return objNotaCreditoDT.obtenerDiferencias(factura, vBruto)
    End Function


    Public Function ObtenerRecibo_dFactura(ByVal factura) As Integer
        Return objNotaCreditoDT.obtenerRecibo_factura(factura)
    End Function

    Public Function actualizarRECIBOIMPORTEDESTOENV(ByVal recibo, ByVal ImporteEnvase) As Boolean
        Return objNotaCreditoDT.actualizarRECIBOImporteEnvase(recibo, ImporteEnvase)
    End Function

    Public Function getNotaCreditoDetalle(ByVal idNc As String) As DataTable
        Dim dtNotaCreditoDetalle As New DataTable
        dtNotaCreditoDetalle = objNotaCreditoDT.getNotaDeCreditoDetalle(idNc)
        If dtNotaCreditoDetalle.Rows.Count > 0 Then
            Return dtNotaCreditoDetalle
        Else
            Throw New Exception("No fue posible recuperar los datos de la nota de credito")
            Return Nothing
        End If
    End Function

    Public Function getNotaCreditoDetalle2(ByVal idNc As String) As DataTable
        Dim dtNotaCreditoDetalle As New DataTable
        dtNotaCreditoDetalle = objNotaCreditoDT.getNotaDeCreditoDetalle2(idNc)
        If dtNotaCreditoDetalle.Rows.Count > 0 Then
            Return dtNotaCreditoDetalle
        Else
            Throw New Exception("No fue posible recuperar los datos de la nota de credito")
            Return Nothing
        End If
    End Function

    Public Function getNotaCreditoByItem(ByVal rowFactura As DataRow) As ItemCO
        Dim item As New ItemCO
        item.NoItem = rowFactura.Item("item").ToString()
        item.idProducto = rowFactura.Item("idProducto").ToString()
        item.um = rowFactura.Item("um").ToString()
        item.cantidad = rowFactura.Item("cantidad").ToString()
        item.precio = rowFactura.Item("precio").ToString()
        item.importe = rowFactura.Item("importe").ToString()
        item.iva = rowFactura.Item("iva").ToString()
        item.porcentajeDesto = rowFactura.Item("porcentajeDesto").ToString()
        item.importeDesto = rowFactura.Item("importeDesto").ToString()
        item.idRubro = rowFactura.Item("idRubro").ToString()
        item.trqt = rowFactura.Item("trqt").ToString()
        item.litm = rowFactura.Item("litm").ToString()
        item.estado = rowFactura.Item("estado").ToString()
        item.descripcion = rowFactura.Item("descripcion").ToString()
        item.unidadesCaja = rowFactura.Item("descripcion").ToString()
        Return item
        Return Nothing
    End Function

    Public Function getRecibo(ByVal idRecibo As String) As documentoCO
        Dim comRecibo As New documentoCO
        Dim dtRecibo As New DataTable
        dtRecibo = objReciboDT.getRecibo(idRecibo)
        If dtRecibo.Rows.Count > 0 Then
            comRecibo.idEncabezado = dtRecibo.Rows(0).Item("id_EncRecibo").ToString()
            comRecibo.ruta = dtRecibo.Rows(0).Item("idRuta").ToString()
            comRecibo.serie = Trim(dtRecibo.Rows(0).Item("rserie").ToString())
            comRecibo.numero = Trim(dtRecibo.Rows(0).Item("rnumero").ToString())

            If dtRecibo.Rows(0).Item("fechaEmision").ToString() = "" Then
                comRecibo.fechaEmision = "null"
            Else
                comRecibo.fechaEmision = dtRecibo.Rows(0).Item("fechaEmision").ToString()
            End If

            comRecibo.idCliente = dtRecibo.Rows(0).Item("idCliente").ToString()

            comRecibo.importe = dtRecibo.Rows(0).Item("importe").ToString()

            comRecibo.porcentajeDesto = dtRecibo.Rows(0).Item("porcentajeDesto").ToString()
            comRecibo.importeDesto = dtRecibo.Rows(0).Item("importeDesto").ToString()
            comRecibo.importeDestoEnv = dtRecibo.Rows(0).Item("importeDestoEnv").ToString()

            comRecibo.usuarioAnula = dtRecibo.Rows(0).Item("usuarioAnula").ToString()
            If dtRecibo.Rows(0).Item("fechaAnula").ToString() = "" Then
                comRecibo.fechaAnula = "null"
            Else
                comRecibo.fechaAnula = dtRecibo.Rows(0).Item("fechaAnula").ToString()
            End If



            comRecibo.nImpresiones = dtRecibo.Rows(0).Item("nImpresiones").ToString()
            comRecibo.idusuario = dtRecibo.Rows(0).Item("idusuario").ToString()
            comRecibo.doTipo = Trim(dtRecibo.Rows(0).Item("doTipo").ToString())
            comRecibo.estado = dtRecibo.Rows(0).Item("estado").ToString()
            comRecibo.idEncFacturaRelacionada = dtRecibo.Rows(0).Item("idEncFactura").ToString()
            comRecibo.idEncCxcRelacionada = dtRecibo.Rows(0).Item("idEncCxc").ToString()
            comRecibo.noResolucion = dtRecibo.Rows(0).Item("noResolucion").ToString()
            comRecibo.fechaResolucion = dtRecibo.Rows(0).Item("fechaResolucion").ToString()
            comRecibo.inicial = dtRecibo.Rows(0).Item("inicial").ToString()
            comRecibo.final = dtRecibo.Rows(0).Item("final").ToString()
        Else
            Return Nothing
        End If
        Return comRecibo
    End Function

    Public Function getReciboDetalle(ByVal idRecibo As String) As DataTable
        Dim dtReciboDetalle As New DataTable
        dtReciboDetalle = objReciboDT.getReciboDetalle(idRecibo)
        If dtReciboDetalle.Rows.Count > 0 Then
            Return dtReciboDetalle
        Else
            Throw New Exception("No fue posible recuperar los datos del recibo")
            Return Nothing
        End If
    End Function

    Public Function getReciboByItem(ByVal rowRecibo As DataRow) As ItemCO
        Dim item As New ItemCO
        item.idProducto = rowRecibo.Item("serie").ToString()
        item.descripcion = rowRecibo.Item("numero").ToString()
        item.importe = rowRecibo.Item("importe").ToString()
        item.saldo = rowRecibo.Item("saldo").ToString()
        Return item
        Return Nothing
    End Function

    Public Function getReciboPagos(ByVal idRecibo As String) As DataTable
        Dim dtPagosDetalle As New DataTable
        dtPagosDetalle = objReciboDT.getReciboPagos(idRecibo)
        Return dtPagosDetalle
    End Function

    Public Function numeroImpresiones(ByVal idEncabezado As String, ByVal tipoDocumento As String, ByVal numero As String) As Boolean
        Select Case tipoDocumento
            Case "FACTURA"
                objFacturaDT.setNoImpresiones(idEncabezado, numero)
                Return True
            Case "NC"
                objNotaCreditoDT.setNoImpresiones(idEncabezado, numero)
            Case "RECIBO"
                objReciboDT.setNoImpresiones(idEncabezado, numero)
        End Select
        Return False
    End Function

#End Region

#Region " SOPORTE A CONSULTAS EN LINEA "
    Public Function consultarFacturas() As DataTable
        Dim dtFactura As New DataTable
        dtFactura = objFacturaDT.getConsulta()
        If dtFactura.Rows.Count > 0 Then
            Return dtFactura
        Else
            Return Nothing
        End If
    End Function

    Public Function consultarCambios() As DataTable
        Dim dtCambios As New DataTable
        dtCambios = objFacturaDT.getConsultaCambios()
        If dtCambios.Rows.Count > 0 Then
            Return dtCambios
        Else
            Return Nothing
        End If
    End Function

    Public Function consultarPedido() As DataTable
        Dim dtDespacho As New DataTable
        Dim objDespacho As New DespachoBL()
        Dim rlayer As New rLayerHandler
        dtDespacho = objDespacho.getDespachos("%", rlayer)
        If dtDespacho.Rows.Count > 0 Then
            Return dtDespacho
        Else
            Return Nothing
        End If
    End Function
    Public Function consultarRecibos() As DataTable
        Dim dtRecibo As New DataTable
        dtRecibo = objReciboDT.getConsulta()
        If dtRecibo.Rows.Count > 0 Then
            Return dtRecibo
        Else
            Return Nothing
        End If
    End Function

    Public Function consultarNotasDeCredito() As DataTable
        Dim dtnotaDecredito As New DataTable
        dtnotaDecredito = objNotaCreditoDT.getConsulta()
        If dtnotaDecredito.Rows.Count > 0 Then
            Return dtnotaDecredito
        Else
            Return Nothing
        End If
    End Function

    Public Function consultarNotaAbono() As DataTable
        Dim dtnotaAbono As New DataTable
        dtnotaAbono = objNotaCreditoDT.getConsultaNA()
        If dtnotaAbono.Rows.Count > 0 Then
            Return dtnotaAbono
        Else
            Return Nothing
        End If
    End Function

    Public Function consultarResumenMarca() As DataTable
        Dim dtresumenMarca As New DataTable
        dtresumenMarca = objNotaCreditoDT.getConsultaResumen()
        If dtresumenMarca.Rows.Count > 0 Then
            Return dtresumenMarca
        Else
            Return Nothing
        End If
    End Function


    Public Function cantidadDocumentos(ByVal tipo As String) As Integer
        Dim dtDocumentos As New DataTable
        Select Case tipo
            Case Is = "FA"
                dtDocumentos = consultarFacturas()
            Case Is = "RE"
                dtDocumentos = consultarRecibos()
            Case Is = "NC"
                dtDocumentos = consultarNotasDeCredito()
            Case Is = "DP"
                dtDocumentos = consultarPedido()
            Case Else
                Return 0
        End Select
        If dtDocumentos Is Nothing Then Return 0 Else Return dtDocumentos.Rows.Count
    End Function


    Public Function aplicaDescuentoContado(ByVal comCliente As ClienteCO, ByRef comFactura As documentoCO, ByVal askConfirm As Boolean) As Boolean
        Dim oUtil As New UtilitarioBL
        Dim pdescuento As Integer
        Dim importeLiquido As String
        Dim aplicaDescuentoC As Boolean = False
        Dim msgCollection As New messageCollection

        pdescuento = oUtil.pDescuento("EF")

        If pdescuento < 0 Or comCliente.categoria = "07" Then
            '--- Alertar sobre el descuento pago en efectivo
            importeLiquido = comFactura.importeLiquido
            comFactura.descuentoLiquido = oUtil.isDecimal(importeLiquido * pdescuento / 100)
            If askConfirm Then
                If msgCollection.raiseMensaje(200, FormatCurrency(importeLiquido, 2), FormatPercent(pdescuento / 100) & " " & FormatCurrency(comFactura.descuentoLiquido, 2)) = MsgBoxResult.Yes Then
                    comFactura.importeDesto = oUtil.isDecimal(comFactura.importeDesto) + oUtil.isDecimal(comFactura.descuentoLiquido)
                    comFactura.porcentajeDestoAdicional = pdescuento
                    Return True
                Else
                    Return False
                End If
            Else
                comFactura.importeDesto = oUtil.isDecimal(comFactura.importeDesto) + oUtil.isDecimal(comFactura.descuentoLiquido)
                comFactura.porcentajeDestoAdicional = pdescuento
            End If

        Else

            '--- No esta seteado el parametro de descuento o el cliente es categoria 07
            If msgCollection.raiseMensaje(300, FormatCurrency(comFactura.importeDesto, 2)) = MsgBoxResult.Yes Then
                Return True
            Else
                Return False
            End If
        End If
    End Function
#End Region

    Private Shared expression As Regex = New Regex("[0-9]{13}", RegexOptions.Compiled)

    Public Function HasValidFormat(ByVal cui As String) As Boolean
        If cui Is Nothing Then Throw New ArgumentNullException("cui")
        Return expression.IsMatch(cui)
    End Function

    Public Function validaPasaporte(ByVal pasaporte As String) As Boolean
        Try
            Dim t As Integer
            Dim caractEspecial As New Regex("[^a-zA-Z0-9]")
            t = pasaporte.Length
            If caractEspecial.Matches(pasaporte).Count > 0 Then
                Return True
            Else
                Return False
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function validaDPI(ByVal cui As String) As Boolean
        Try
            If cui Is Nothing Then Throw New ArgumentNullException("cui")
            If Not HasValidFormat(cui) Then Throw New FormatException("El CUI debe tener exactamente 13 dígitos y no puede contener espacios en blanco, signos de puntuación ni letras.")
            Dim cuiNumber As String = cui.Substring(0, 8)
            Dim cheker As String = cui.Substring(8, 1)
            Dim stateCodeString As String = cui.Substring(9, 2)
            Dim cityCodeString As String = cui.Substring(11, 2)
            Dim stateCode As Integer = Integer.Parse(stateCodeString)
            Dim cityCode As Integer = Integer.Parse(cityCodeString)
            Dim checkerCode As Integer = Integer.Parse(cheker)
            Dim stateCityCounts As Integer() = New Integer() {17, 8, 16, 16, 13, 14, 19, 8, 24, 21, 9, 30, 32, 21, 8, 17, 14, 5, 11, 11, 7, 17}
            If stateCode = 0 OrElse cityCode = 0 Then
                Return False
            End If
            If stateCode > stateCityCounts.Length OrElse cityCode > stateCityCounts.Max() Then
                Return False
            End If
            If cityCode > stateCityCounts(stateCode - 1) Then
                Return False
            End If
            Dim total As Integer = 0
            For i As Integer = 0 To cuiNumber.Length - 1
                total += Int(cuiNumber.Substring(i, 1)) * (i + 2)
            Next
            Dim modulus As Integer = (total Mod 11)
            Dim valid = checkerCode = modulus
            If Not valid Then
                Return False
            End If
            Return valid
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function validarNit(ByRef nit As String)

        If Trim(nit.ToUpper) = "CF" Or Trim(nit.ToUpper) = "C/F" Then
            nit = Trim(nit.ToUpper)
            Return True
        End If

        Dim posGuion As Integer
        Dim digito As String
        Dim correlativo As Integer
        Dim factor, suma, valor As Integer


        nit = Trim(nit)

        '--- Verificar si hay guion
        posGuion = nit.IndexOf("-")
        If posGuion < 0 Then

            '--- No hay guion entonces transformar al formato ( Correlativo  &  "-" & Digito )
            'If (objUtilBL.isInteger(Split(nit, "-")(0))) Then
            If (Split(nit, "-")(0).Length > 0) Then
                correlativo = nit.Substring(0, nit.Length() - 1)
            Else
                Return False
            End If

            posGuion = correlativo.ToString().Length - 1
            digito = nit.Substring(posGuion + 1)
            nit = correlativo.ToString & "-" & digito
        End If

        '--- Partir el NIT en dos partes
        If (objUtilBL.isInteger(Split(nit, "-")(0))) Then
            correlativo = Split(nit, "-")(0)
        Else
            Return False
        End If
        digito = Split(nit, "-")(1)
        factor = correlativo.ToString().Length + 1
        posGuion = nit.IndexOf("-")

        For i As Integer = 0 To posGuion - 1
            valor = nit.Substring(i, 1)
            Dim Multiplicacion = valor * factor
            suma += Multiplicacion
            factor = factor - 1
        Next

        Dim xMOd11, s As String
        xMOd11 = (11 - (suma Mod (11)) Mod (11))
        s = xMOd11
        If ((xMOd11 >= 10 And (digito.ToUpper = "K" Or digito = "0")) Or (s = digito)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function cambiarResponsable(ByVal clienteT As String, ByVal clienteSAP As String) As Boolean
        Try
            '--- Cambiar codigo de cliente
            Dim ocliente As New ClienteBL
            ocliente.cambiarResponsable(clienteT, clienteSAP)

            '--- Cambiar responsable de facturas
            Dim oFactura As New Factura
            oFactura.cambiarResponsable(clienteT, clienteSAP)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class
