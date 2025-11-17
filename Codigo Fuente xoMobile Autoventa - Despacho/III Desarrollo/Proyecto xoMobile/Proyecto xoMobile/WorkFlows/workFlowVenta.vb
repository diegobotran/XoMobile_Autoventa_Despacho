Imports System.Data
Imports System.Threading
Imports Proyecto_xoMobile_Packs


Public Class workFlowVenta

#Region " DECLARACION DE VARIABLESY OBJETOS "

    'Objetos de Comunicacion entre procesos
    Public comCliente As ClienteCO
    Public comNotaCreditoEnv, comNotaCreditoDpp, comNotaCreditoBoni, comRecibo, comBonificacion, comPedido As New documentoCO
    Public comItemBonificacion As New ItemCO
    Dim dtAgregados, dtProductos As New DataTable
    Dim objUtilBl As New UtilitarioBL
    'Variables locales
    Public dtDocumentosPorcobrar As New DataTable
    Public rLayer As New rLayerHandler
    Dim Documentos() As DataRow
    Dim paso As Integer = 0
    Dim corriendo As Boolean = False
    'Dim isContado = True
    Dim activarPrevision As Boolean = False
    Dim lstFacturaDetalle As New Windows.Forms.ListView
    Dim lstNotaCreditoDetalle As New Windows.Forms.ListView

    'Objetos de la capa de negocio
    Dim oUtil As New UtilitarioBL
    Dim msgCollection As New messageCollection
    Dim objDocumento As New DocumentoBL
    Dim objRuta As New RutaBL
    Dim oDespacho As New DespachoBL

#End Region


    Public Function ejecutarDespacho() As Boolean

        '--- Pasos para ejecutar un despacho
        id_glo_aplicacion = "despacho"
        Select Case paso
            Case Is = 0
                stepListarPedidos()
        End Select

        '--- Funcion Ciclica
        For Each row In Documentos
            corriendo = True
            glo_dtNcProductos.Rows.Clear()
            glo_dtVentaProductos.Rows.Clear()
            While corriendo
                Select Case paso
                    Case Is = 0
                        corriendo = False
                    Case Is = 1
                        stepRecuperaPedido_(row)

                    Case Is = 2
                        stepMuestraPedido()

                    Case Is = 3
                        stepRecoleccionEnvase()

                    Case Is = 4
                        stepPago()

                    Case Is = 5
                        confirmarVenta()
                    Case Is = 6
                        Exit For
                End Select
            End While
        Next
        Return True
    End Function

    Public Function ejecutarCambio() As Boolean

        '--- Pasos para ejecutar un despacho
        id_glo_aplicacion = "cambio"
        Select Case paso
            Case Is = 0
                stepListarPedidos()
        End Select

        '--- Funcion Ciclica
        For Each row In Documentos
            corriendo = True
            glo_dtNcProductos.Rows.Clear()
            glo_dtVentaProductos.Rows.Clear()
            While corriendo
                Select Case paso
                    Case Is = 0
                        corriendo = False
                    Case Is = 1
                        stepRecuperaPedido_(row)

                    Case Is = 2
                        stepMuestraPedidoCambio()

                    Case Is = 3
                        'stepRecoleccionEnvase()
                        paso = 4
                    Case Is = 4
                        ''stepPago()
                        confirmarCambio()
                        'Case Is = 5
                        'confirmarVenta()
                    Case Is = 6
                        Exit For
                End Select
            End While
        Next
        Return True
    End Function


    Public Function ejecutarVenta() As Boolean

        corriendo = True
        paso = 1
        glo_dtNcProductos.Rows.Clear()
        glo_dtVentaProductos.Rows.Clear()
        'glo_dtVentaProductos = Nothing

        '--- Pasos para ejecutar una venta             
        While corriendo
            Select Case paso
                Case Is = 0
                    corriendo = False

                Case Is = 1
                    stepRealizarVenta()
                    descuentosEspeciales(comCliente, lstFacturaDetalle, comPedido)
                Case Is = 2
                    stepRecoleccionEnvase()
                Case Is = 3
                    pagoDeContado()
                Case Is = 4
                    stepPago()
                Case Is = 5
                    confirmarVenta()
                Case Is = 6
            End Select
        End While
        glo_productos_venta_dt.Rows.Clear()
        If rLayer.codigo = 500 Then
            Return False
        Else
            Return True
        End If
    End Function

#Region " PASOS ATENCION DESPACHO"

    Public Function stepListarPedidos() As Boolean

        '--- CU30: Realizar Venta
        Dim objDocumento As New DocumentoBL
        Dim objDespacho As New DespachoBL
        Dim fDespacho As New frmDespacho


        '--- CU24: Despacho
        fDespacho.lblNombreCliente.Text = comCliente.negocio
        fDespacho.lblAtencion.Text = "Pedidos"
        fDespacho.Cliente = comCliente
        fDespacho.ShowDialog()

        '--- Evaluar la ejecucion del proceso CU30
        rLayer = fDespacho.rLayer
        Select Case rLayer.codigo

            Case Is = 1
                '--- Hay un mensaje de error disponible en el texto
                corriendo = False
                MsgBox(rLayer.texto)
                Return False

            Case Is = 2
                '--- Se selecciono una razon de no despacho
                corriendo = False
                Return False
            Case Is = 3
                corriendo = False
                Return False
        End Select
        'If (id_glo_aplicacion <> "cambio") Then
        'Documentos = fDespacho.dtDespachos.Select("estado = 1 AND tPedido <> 'CAM' ")
        'Else
        Documentos = fDespacho.dtDespachos.Select("estado = 1")
        'End If
        '--- Seleccionar los documentos a pagar

        If Documentos.Length = 0 Then
            corriendo = False
        ElseIf Documentos.Length > 0 Then
            paso = 1
        End If
        Return True
    End Function

    Public Function stepRecuperaPedido_(ByVal rowDocumento As DataRow) As Boolean

        Dim objDespacho As New DespachoBL
        Dim vProductosLiquido As New DataView
        Dim objProducto As New ProductoBL

        Cursor.Current = Cursors.WaitCursor

        '--- Recuperar datos del pedido seleccionado
        comPedido.idPedido = rowDocumento.Item("id_Pedido")
        comPedido.tipoPago = rowDocumento.Item("tipoPago")
        comPedido.importeDestoPP = rowDocumento.Item("importeDestoPP").ToString
        comPedido.condicion = rowDocumento.Item("condicion").ToString
        comPedido.motivo = rowDocumento.Item("tmotivo")
        id_glo_condicion = comPedido.condicion

        If comPedido.tipoPago = "CREDITO" Then
            comPedido.isContado = False
            'aqui se agrego esta linea de codigo para la version 2.0.13
        Else
            comPedido.isContado = True
            '******************************************************
        End If

        comPedido.isPedido = True
        If rowDocumento.Item("isDiferente").ToString <> "0" Then comPedido.isDiferente = True

        '--- Crear listado de productos agregados
        dtAgregados = objProducto.ObtenerListadoProductos("despacho", comCliente, comPedido.idPedido)

        '--- Crear Listado de productos liquidos
        vProductosLiquido = dtAgregados.DefaultView
        vProductosLiquido.RowFilter = "idRubro = 'L'"
        dtProductos = vProductosLiquido.ToTable

        Cursor.Current = Cursors.Default
        '--- Evaluar la ejecucion del proceso 
        If dtAgregados Is Nothing Then
            MsgBox("No se encontraron productos en este pedido.")
            paso -= 1
            Return False
        End If

        paso = 2
    End Function

    Public Function stepMuestraPedido() As Boolean

        '--- Mostrar pedido en el formulario
        Dim objDocumento As New DocumentoBL
        Dim objDespacho As New DespachoBL
        Dim ccF As New CorrelativoCO
        Dim frmIngreso As New frmIngreso


        '--- Formulario de Despacho
        id_glo_aplicacion = "despacho"
        frmIngreso.lblNombreCliente.Text = comCliente.negocio
        frmIngreso.lblAtencion.Text = "Pedido de " + comPedido.tipoPago
        frmIngreso.v_com_cliente = comCliente
        frmIngreso.idPedido = comPedido.idPedido
        frmIngreso.dtAgregados = dtAgregados.Copy
        frmIngreso.dtProductos = dtProductos.Copy
        frmIngreso.isDiferente = comPedido.isDiferente
        frmIngreso.comDocumento = comPedido

        If comPedido.isDiferente Then
            frmIngreso.pboxAlert.Visible = True
            frmIngreso.lblMensajeImporte.Visible = True
            frmIngreso.picMenu.Visible = True
            '--- Cambiar el tamaño del panel de datos del pedido
            frmIngreso.panAdicionales.Height = 440
        End If


        frmIngreso.ShowDialog()

        '--- Evaluar la ejecucion del proceso CU30 
        rLayer = frmIngreso.rlayer
        Select Case rLayer.codigo

            Case Is = 1
                '--- Hay un mensaje de error disponible en el texto 
                MsgBox("Error en el proceso de despacho:" + vbCrLf + rLayer.texto)
                Return False

            Case Is = 2
                paso -= 2
                ejecutarDespacho()

                Return True
            Case Is = 3
                corriendo = False
                Return False
        End Select
        'Reiniciar los valores del pedido

        'total_venta = 0
        '--- Recuperar objetos del proceso    
        comPedido = frmIngreso.comDocumento
        comPedido.importe = frmIngreso.lblImporte.Tag
        lstFacturaDetalle = frmIngreso.lstAgregados

        '--- Recupera la siguiente serie y numero para fines de impresion.   
        objRuta.obtenerCorrelativos(Nothing, ccF)
        comPedido.serie = ccF.serie
        comPedido.numero = ccF.actual + 1
        paso = 3
        Return True
    End Function

    Public Function stepMuestraPedidoCambio() As Boolean

        '--- Mostrar pedido en el formulario
        Dim objDocumento As New DocumentoBL
        Dim objDespacho As New DespachoBL
        Dim ccc As New CorrelativoCO
        Dim frmIngreso As New frmIngreso


        '--- Formulario de Despacho
        id_glo_aplicacion = "cambio"
        frmIngreso.lblNombreCliente.Text = comCliente.negocio
        frmIngreso.lblAtencion.Text = "Pedido de " + comPedido.tipoPago
        frmIngreso.v_com_cliente = comCliente
        frmIngreso.idPedido = comPedido.idPedido
        frmIngreso.dtAgregados = dtAgregados.Copy
        frmIngreso.dtProductos = dtProductos.Copy
        frmIngreso.isDiferente = comPedido.isDiferente
        frmIngreso.comDocumento = comPedido

        If comPedido.isDiferente Then
            frmIngreso.pboxAlert.Visible = True
            frmIngreso.lblMensajeImporte.Visible = True
            frmIngreso.picMenu.Visible = True
            '--- Cambiar el tamaño del panel de datos del pedido
            frmIngreso.panAdicionales.Height = 440
        End If


        frmIngreso.ShowDialog()

        '--- Evaluar la ejecucion del proceso CU30
        rLayer = frmIngreso.rlayer
        Select Case rLayer.codigo

            Case Is = 1
                '--- Hay un mensaje de error disponible en el texto
                MsgBox("Error en el proceso de despacho:" + vbCrLf + rLayer.texto)
                Return False

            Case Is = 2
                paso -= 2
                ejecutarCambio()
                Return True
            Case Is = 3
                corriendo = False
                Return False
        End Select

        '--- Recuperar objetos del proceso 
        comPedido = frmIngreso.comDocumento
        comPedido.importe = frmIngreso.lblImporte.Tag
        lstFacturaDetalle = frmIngreso.lstAgregados

        '--- Recupera la siguiente serie y numero para fines de impresion.
        objRuta.obtenerCorrelativos(Nothing, Nothing, Nothing, ccc)
        comPedido.serie = ccc.serie
        comPedido.numero = ccc.actual + 1
        paso = 3
        Return True
    End Function

    Public Function stepRealizarVenta() As Boolean

        '--- CU30: Realizar Venta
        Dim objDocumento As New DocumentoBL
        Dim objProductoDT As New ProductoDT
        Dim frmVenta As New frmIngreso
        Dim ccF As New CorrelativoCO

        frmVenta.v_com_cliente = comCliente
        frmVenta.lblAtencion.Text = "Orden venta"
        frmVenta.lblNombreCliente.Text = comCliente.negocio
        id_glo_aplicacion = "venta"
        co_glo_NextForm = True


        '--- Cargar tabla candados de materiales
        glo_productos_locks_dt = objProductoDT.getLocks(rLayer)


        frmVenta.ShowDialog()

        '--- Evaluar la ejecucion del proceso CU30
        rLayer = frmVenta.rlayer
        Select Case rLayer.codigo

            Case Is = 1
                '--- Hay un mensaje de error disponible en el texto
                MsgBox("Error en el proceso de Venta:" + vbCrLf + rLayer.texto)
                Return False

            Case Is = 2
                paso -= 1
                Return True

            Case Is = 3
                corriendo = False
                Return False

            Case 500
                '--- Codigo de aborto de la operacion
                paso = frmIngreso.comPaso
                corriendo = frmIngreso.comCorriendo
                Return False
        End Select

        '--- Recuperar objetos del proceso CU30
        comPedido = frmVenta.comDocumento
        lstFacturaDetalle = frmVenta.lstAgregados
        MessageBox.Show(frmVenta.lblImporte.Text)
        

        '--- Recupera la siguiente serie y numero para fines de impresion.
        objRuta.obtenerCorrelativos(Nothing, ccF)
        comPedido.serie = ccF.serie
        comPedido.numero = ccF.actual + 1
        paso = 2
        Return True
    End Function

    Public Function stepRecoleccionEnvase() As Boolean
        If (id_glo_sociedad <> 7000) Then


            Dim frmRecoleccionEnvase As New frmIngreso

            '--- CU21:Recoleccion de envase
            id_glo_aplicacion = "nc"
            frmRecoleccionEnvase.v_com_cliente = comCliente
            frmRecoleccionEnvase.lblAtencion.Text = "Envase"
            frmRecoleccionEnvase.lblNombreCliente.Text = comCliente.negocio
            frmRecoleccionEnvase.picMenu.Enabled = False
            frmRecoleccionEnvase.ShowDialog()
            Cursor.Current = Cursors.Default

            '--- Evaluar la ejecucion del proceso CU21
            rLayer = frmRecoleccionEnvase.rlayer
            Select Case rLayer.codigo
                Case Is = 1
                    '--- Hay un mensaje de error disponible en el texto
                    MsgBox("Error en el proceso de cobro:" + vbCrLf + rLayer.texto)
                    Return False

                Case Is = 2
                    paso -= 1
                    Return True

                Case Is = 3
                    corriendo = False
                    Return False
            End Select

            '--- Recuperar objetos del proceso CU21
            comNotaCreditoEnv = frmRecoleccionEnvase.comDocumento
            lstNotaCreditoDetalle = frmRecoleccionEnvase.lstAgregados

            '--- Agregar a la factura el descuento por devolucion de envase
            comPedido.importeDestoEnv = comNotaCreditoEnv.importe
            comNotaCreditoEnv.condicion = id_glo_condicion
            If tipoRuta <> 16 Then
                paso = 3
            Else
                paso = 4
            End If
        Else
            paso = 4
        End If

        Return True
    End Function

    Public Function stepPago() As Boolean
        Dim valorEnvase As Decimal = 0
        '--- CU31:Pago del documento
        Dim frmPago As New frmPago
        frmPago.v_com_cliente = comCliente
        frmPago.v_com_documento = comPedido 'comPedido
        frmPago.v_com_documento.ttipo = "ODV"
        frmPago.Text = "Pago " + comPedido.serie + " " + comPedido.numero
        frmPago.lblNumero.Text = comPedido.numero
        frmPago.lblSerie.Text = comPedido.serie

        'Esto se agrego para validar el importe de envase del documento
        For i As Integer = 0 To lstNotaCreditoDetalle.Items.Count() - 1
            If lstNotaCreditoDetalle.Items(i).SubItems(1).Text <> "S U B" Then

                If id_glo_aplicacion = "venta" Or id_glo_aplicacion = "nc" Then
                    valorEnvase += objUtilBl.isDecimal(lstNotaCreditoDetalle.Items(i).SubItems(18).Text)
                End If

            End If
        Next
        'valorEnvase = 10
        frmPago.v_com_documento.importeDestoEnv = valorEnvase
        'fin de la validacion

        frmPago.ShowDialog()

        '--- Evaluar la ejecucion del proceso CU31
        rLayer = frmPago.rlayer
        Select Case rLayer.codigo

            Case Is = 1
                '--- Hay un mensaje de error disponible en el texto
                MsgBox("Error en el proceso de cobro:" + vbCrLf + rLayer.texto)
                Return False

            Case Is = 2
                If tipoRuta <> 16 Then
                    paso -= 2
                Else
                    paso -= 1
                End If
                Return True

            Case Is = 3
                corriendo = False
                Return False
        End Select

        '--- Recuperar objetos del proceso CU31
        comRecibo = frmPago.comDocumentoPago
        paso = 5
        Return True
    End Function

    Private Function pagoDeContado() As Boolean

        Dim importeLiquido As String
        Dim aplicaDescuentoC As Boolean = False
        Dim item As New ItemCO

        comPedido.porcentajeDestoAdicional = 0
        ' Estas son las lineas que se agregaron por el bug del descuento
        comPedido.importeDesto = 0
        comPedido.descuentoProductos = 0
        Try
            If comCliente.aceptaCredito And Trim(comCliente.categoria) <> "07" And Trim(comCliente.categoria) <> "08" Then

                Dim pdescuento As Integer
                pdescuento = oUtil.pDescuento("EF")

                '--- Alertar sobre el descuento pago de contado 
                If oUtil.isInteger(pdescuento) < 0 Then
                    importeLiquido = comPedido.importeLiquido
                    comPedido.descuentoPagoContado = oUtil.isDecimal(importeLiquido * oUtil.pDescuento("EF") / 100)

                    If msgCollection.raiseMensaje(200, FormatCurrency(importeLiquido, 2), FormatCurrency(comPedido.descuentoPagoContado, 2)) = MsgBoxResult.Yes Then
                        comPedido.isContado = True
                        aplicaDescuentoC = True
                        comPedido.porcentajeDestoAdicional = pdescuento
                    Else
                        comPedido.isContado = False
                        comPedido.descuentoPagoContado = 0
                    End If
                Else
                    '--- No hay descuento por pago en efectivo, pregunta si va a pagar credito o efectivo
                    If msgCollection.raiseMensaje(300, FormatCurrency(comPedido.importeDesto, 2)) = MsgBoxResult.Yes Then
                        comPedido.isContado = True
                        aplicaDescuentoC = True
                    Else
                        '--- Acepta pago credito
                        comPedido.isContado = False

                    End If
                End If
            Else

                '--- CASO ESPECIAL Puede pagar con efectivo y credito solo si es 7, el 8 ya no tiene credito
                '--- Preguntar si desea pagar al credito o al contado
                If Trim(comCliente.categoria) = "07" And comCliente.aceptaCredito Then
                    If msgCollection.raiseMensaje(300, FormatCurrency(comPedido.importeDesto, 2)) = MsgBoxResult.Yes Then
                        '--- Acepta el pago de contado
                        comPedido.isContado = True
                        aplicaDescuentoC = True

                    Else
                        '--- Acepta pago credito

                        comPedido.isContado = False

                    End If

                End If
            End If
            paso = 4

            '--- Descuento total
            For i As Integer = 0 To lstFacturaDetalle.Items.Count() - 1
                If lstFacturaDetalle.Items(i).SubItems(3).Text = "L" Then
                    item.porcentajeDesto = oUtil.isDecimal(lstFacturaDetalle.Items(i).SubItems(14).Text)
                    item.importeDesto = oUtil.isDecimal((item.porcentajeDesto / 100) * oUtil.isDecimal(lstFacturaDetalle.Items(i).SubItems(12).Text))
                    item.valorIvaDesto = -1 * (item.importeDesto - (oUtil.isDecimal((item.porcentajeDesto / 100) * oUtil.isDecimal(lstFacturaDetalle.Items(i).SubItems(7).Text))))
                    comPedido.descuentoProductos += item.importeDesto + item.valorIvaDesto
                End If
            Next
            comPedido.importeDesto = oUtil.isDecimal(comPedido.descuentoPagoContado) + oUtil.isDecimal(comPedido.descuentoProductos)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = Cursors.Default
        paso = 4
    End Function

    Public Function descuentosEspeciales(ByVal cliente As ClienteCO, ByRef lstAgregados As Windows.Forms.ListView, ByRef ordenVenta As documentoCO) As Boolean

        '--- Si aplica calcula el descuento de temporada alta y los descuentos manuales
        '    afectando el porcentaje de descuento en el objeto lstAgregados (detalle de la factura)

        Dim dtDescuentos As New DataTable
        Dim item As New ItemCO
        Dim producto As New ProductoCO
        Dim objProductoBL As New ProductoBL
        Dim objDocumentoBL As New DocumentoBL
        Dim maximo As Decimal
        Dim _alcanzaPresupuesto As Boolean
        Try
            '---Salir si es ruta distinta de despacho o si el cliente es generico
            If (tipoRuta = 16) Or (id_glo_cliente = id_glo_clienteGenerico) Then Return True

            '--- Determinar si alcanzo el presupuesto
            _alcanzaPresupuesto = objDocumentoBL.alcanzaPresupuesto(cliente, lstAgregados, dtDescuentos, ordenVenta)

            '--- Calcular los descuentos para cada ITEM en base a la tabla DESCTA
            For i As Integer = 0 To lstAgregados.Items.Count() - 1

                If lstAgregados.Items(i).SubItems(3).Text = "L" Then

                    producto = objProductoBL.getDetalleDelProducto(lstAgregados.Items.Item(i).SubItems(10).Text)
                    item = objProductoBL.explosionarMaterial(cliente, producto, lstAgregados.Items(i).SubItems(5).Text, lstAgregados.Items(i).SubItems(6).Text, 0, 0, 0)

                    '--- Revisar si el producto aplica Descuento manual
                    If objProductoBL.getDescuentoManual(maximo, producto.codigo) Then
                        Dim frmDm As New frmDescuentoManual

                        frmDm.maximo = maximo
                        frmDm.lblArticulo.Text = producto.descripcion
                        frmDm.lblMaximo.Text = "Hasta un " + frmDm.maximo.ToString + " %."
                        frmDm.ShowDialog()

                        '--- % descuento 
                        lstAgregados.Items(i).SubItems(14).Text = frmDm.txtDescuento.Text * -1
                        Continue For
                    End If

                    If _alcanzaPresupuesto Then

                        '--- Obtener el descuento por categoria, para visualizacion previa
                        objProductoBL.descuentoCategoria("DESCTA", cliente, producto, item)

                        '--- % descuento Temporada alta
                        lstAgregados.Items(i).SubItems(14).Text = oUtil.isDecimal(item.porcentajeDesto)
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            MsgBox("Error en calculo de descuentos: " & ex.Message)
            Return False
        End Try
    End Function
#End Region

#Region " PERSISTENCIA DE DATOS "

    Private Function confirmarVenta() As Boolean
        Try
            'Dim starter As New ThreadStart(AddressOf Me.persistenciaVenta)
            'Dim t As New Thread(starter)
            't.IsBackground = True
            't.Start()
            'paso = 6
            'corriendo = False

            persistenciaVenta()
            paso = 5
            corriendo = False

        Catch ex As Exception
            MsgBox("No se pudo crear la factura.")
            paso = 5
            corriendo = False
        End Try
    End Function

    Private Function confirmarCambio() As Boolean
        Try
            'Dim starter As New ThreadStart(AddressOf Me.persistenciaVenta)
            'Dim t As New Thread(starter)
            't.IsBackground = True
            't.Start()
            'paso = 6
            'corriendo = False

            persistenciaCambio()
            paso = 5
            corriendo = False

        Catch ex As Exception
            MsgBox("No se pudo crear la factura.")
            paso = 5
            corriendo = False
        End Try
    End Function

    Private Function persistenciaVenta() As Boolean
        Dim Fel As New Generador
        Dim numero As Integer = 1
        Dim objImpresion As New ImpresionBL
        Dim objDocumento As New DocumentoBL
        Dim objRutaBL As New RutaBL
        Dim objRuta As New RutaBL
        Dim objFacturaDT As New Factura
        Dim comPedidoComplemento As New documentoCO
        Dim objInventario As New InventarioBL
        Dim objDocumentoBL As New DocumentoBL
        Dim dtElectronico As DataTable

        '--- Preparar la factura para persistencia
        comPedido.fechaEmision = "GETDATE()"
        comPedido.fechaAnula = "null"
        comPedido.porcentajeIva = co_glo_porcentajeIVA
        comPedido.estado = 1
        comPedido.ttipo = "ODV"
        comPedido.idusuario = id_glo_usuario


        If comPedido.isContado Then
            comPedido.condicion = "IL01"
            id_glo_condicion = comPedido.condicion
            comPedido.importeDestoPP = 0
            comPedido.porcentajeDestoPP = 0
            comPedido.fechaVence = "null"
            comPedido.importeDesto = oUtil.isDecimal(comPedido.importeDesto)
        Else
            'comPedido.condicion = id_glo_condicion
            comPedido.fechaVence = "GETDATE() + " + oUtil.getDataValue("CPAGO", comPedido.condicion).ToString
            If tipoRuta <> "16" Then
                comPedido.importeDestoPP = comPedido.descuentoProductos
            End If
            comPedido.importeDesto = 0
            comPedido.porcentajeDesto = 0
        End If

        '--- Agregar a la factura el descuento por devolucion de envase
        comPedido.importeDestoEnv = comNotaCreditoEnv.importe

        'Validar si existe el recibo para asociarlo a la factura
        If (objDocumento.existeRecibo(comRecibo.idEncabezado)) Then

            '--- Vincular la factura con el recibo
            comPedido.idReciboRelacionado = comRecibo.idEncabezado

            '--- Crear factura en la base de datos     
            If comPedido.isPedido Then
                objDocumento.crearOrdenVentaDespacho(lstFacturaDetalle, comPedido, comCliente)
            Else
                objDocumento.crearOrdenVenta(lstFacturaDetalle, comPedido, comCliente)
            End If
            'Validar si existe el recibo.

            ' ********** Generación Factura Electronica Guatefacturas  **********
            Try
                If (id_glo_fel = "X") Then

                    If id_glo_sociedad = 7000 Then
                        If (Fel.generador3(comPedido.idEncabezado, comCliente)) Then
                            'MessageBox.Show("Documento FACTURA Generado Correctamente ")
                            dtElectronico = objDocumentoBL.datosElectronicos(comPedido.idEncabezado)
                            comNotaCreditoEnv.serieFEL = dtElectronico.Rows(0).Item("NUMEROAUTORIZACION")
                            comNotaCreditoEnv.preimpreso = dtElectronico.Rows(0).Item("PREIMPRESO")
                        Else
                            MessageBox.Show("Error al generar documento ")
                            comNotaCreditoEnv.serieFEL = ""
                            comNotaCreditoEnv.preimpreso = ""
                        End If
                    Else
                        If (Fel.generador(comPedido, comCliente)) Then
                            'MessageBox.Show("Documento FACTURA Generado Correctamente ")
                            dtElectronico = objDocumentoBL.datosElectronicos(comPedido.idEncabezado)
                            comNotaCreditoEnv.serieFEL = dtElectronico.Rows(0).Item("NUMEROAUTORIZACION")
                            comNotaCreditoEnv.preimpreso = dtElectronico.Rows(0).Item("PREIMPRESO")
                        Else
                            MessageBox.Show("Error al generar documento ")
                            comNotaCreditoEnv.serieFEL = ""
                            comNotaCreditoEnv.preimpreso = ""
                        End If
                    End If


                    
                End If
            Catch ex As Exception

            End Try
            
            ' ******** FIN DE LA FUNCION *************

            '--- Actualizar los datos del encabezado y detalle de la orden de venta
            objDocumento.actualizarFactura(comPedido, comPedido.isContado)

            '--- Crear Nota de Credito en la base de datos
            comNotaCreditoEnv.idEncFacturaRelacionada = comPedido.idEncabezado
            comNotaCreditoEnv.idReciboRelacionado = comRecibo.idEncabezado

            If comNotaCreditoEnv.importe <> 0 Then
                ' Creación de Nota de Crédito Electronica.
                If (id_glo_fel = "X") Then
                    If (comNotaCreditoEnv.diasVencidosE > 60) Then
                        If (objDocumento.crearNotaDeCredito(lstNotaCreditoDetalle, comNotaCreditoEnv)) Then
                            If (id_glo_fel = "X") Then
                                MessageBox.Show("ESTE DOCUMENTO TIENE MAS DE 60 DIAS, GENERA NOTA DE ABONO")
                                Fel.generadorNABONO(comNotaCreditoEnv, comCliente)
                            End If

                            'MessageBox.Show("Documento NOTA CREDITO Generado Correctamente ")
                        End If
                    Else
                        If (objDocumento.crearNotaDeCredito(lstNotaCreditoDetalle, comNotaCreditoEnv)) Then
                            If (id_glo_fel = "X") Then
                                Fel.generadorNC(comNotaCreditoEnv, comCliente)
                                'MessageBox.Show("Documento NOTA CREDITO Generado Correctamente ")
                            End If
                        End If
                    End If
                Else
                    If (objDocumento.crearNotaDeCredito(lstNotaCreditoDetalle, comNotaCreditoEnv)) Then

                    End If
                End If


                
            End If

            '--- Agregar movimientos de inventario
            objInventario.actualizarInventario(comPedido.idEncabezado, -1, comPedido.ttipo)


            '--- Vincular el recibo con la factura   [Este lo vamos a hacer por objetos]
            comRecibo.fechaAnula = "null"
            comRecibo.idEncFacturaRelacionada = comPedido.idEncabezado
            objDocumento.actualizarRecibo(comRecibo)


            '--- Crear cuenta por cobrar
            crearCuentaPorCobrar(comCliente, comPedido, comRecibo)

            '--- Confirmar el despacho realizado
            oDespacho.marcarPedidoDespacho(comPedido.idPedido, True, comCliente)

            '--- Confirmar la operacion comercial
            If Not objRuta.confirmarOperacionComercial() Then
                Throw New Exception("Las operaciones comerciales no se pudieron confirmar")
            End If



            '--- Imprimir Documentos
            Try

                'If Not objImpresion.imprimeRecibo(comRecibo.idEncabezado, False) Then
                ' MsgBox("La impresora no esta disponible.")
                ' numero = 0
                ' End If
                objDocumento.validaRecibos(comRecibo)

                If (objDocumento.validaFactura(comPedido.idEncabezado, comRecibo.idEncabezado)) Then
                    objImpresion.imprimeRecibo(comRecibo.idEncabezado, False)
                    If (id_glo_fel = "X") Then
                        
                        If objDocumento.validaDocumentoFEL(comPedido.idEncabezado) Then
                            objImpresion.imprimeFactura(comPedido.idEncabezado, comRecibo.idEncabezado, False)
                        Else
                            MessageBox.Show("NO HAY CONEXIÓN, VUELVA A INTENTAR")
                        End If

                    Else
                        objImpresion.imprimeFacturaSINFEL(comPedido.idEncabezado, comRecibo.idEncabezado, False)
                    End If

                    'objDocumento.numeroImpresiones(comRecibo.idEncabezado, "RECIBO", numero)
                    'objDocumento.numeroImpresiones(comPedido.idEncabezado, "FACTURA", numero)
                    If Not comNotaCreditoEnv.idEncabezado = 0 Then
                        If (id_glo_fel = "X") Then
                            If (Len(comNotaCreditoEnv.preimpreso) > 0) Then
                                objImpresion.ImprimeNotaCredito(comNotaCreditoEnv.idEncabezado, False, comPedido.serie + comPedido.numero)
                            End If
                        Else
                            objImpresion.ImprimeNotaCreditoSINFEL(comNotaCreditoEnv.idEncabezado, False, comPedido.serie + comPedido.numero)
                        End If

                        'objDocumento.numeroImpresiones(comNotaCreditoEnv.idEncabezado, "NC", numero)
                    End If
                Else
                    MessageBox.Show("SE GENERO DOCUMENTOS INCONSISTENTES, SE ANULARÁ EL MOVIMIENTO ACTUAL..")
                    'Proceso de anulación de documentos.

                    'VALIDAR SI TIENE FEL PARA ANULAR AUTOMATICAMENTE.
                    'ATENCION ATENCION
                    objDocumento.anularFactura(comPedido.idEncabezado, comCliente.codigo, "2", "ERROR_FACT")

                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            '--- Actualizar correlativos
            Dim cRecibo, cFactura, cNotaCredito, cCambio As New CorrelativoCO
            objRutaBL.getIndicadoresCorrelativo(cRecibo, cFactura, cNotaCredito, cCambio)
            Cursor.Current = Cursors.Default

            '--- Ejecutar Backup de la base de datos
            Try
                Dim ce As New ceClient
                ce.sdf_backup()
            Catch ex As Exception
            End Try
        Else
            MessageBox.Show("FAVOR DE VERIFICAR LOS DOCUMENTOS, NO SE CREO EL RECIBO ")
        End If
    End Function


    Private Function persistenciaCambio() As Boolean

        Dim numero As Integer = 1
        Dim objImpresion As New ImpresionBL
        Dim objDocumento As New DocumentoBL
        Dim objRutaBL As New RutaBL
        Dim objRuta As New RutaBL
        Dim objFacturaDT As New Factura
        Dim comPedidoComplemento As New documentoCO
        Dim objInventario As New InventarioBL

        '--- Preparar la factura para persistencia
        comPedido.fechaEmision = "GETDATE()"
        comPedido.fechaAnula = "null"
        comPedido.porcentajeIva = co_glo_porcentajeIVA
        comPedido.estado = 1
        comPedido.ttipo = "CD"
        comPedido.idusuario = id_glo_usuario

        If comPedido.isContado Then
            comPedido.condicion = "IL01"
            id_glo_condicion = comPedido.condicion
            comPedido.importeDestoPP = 0
            comPedido.porcentajeDestoPP = 0
            comPedido.fechaVence = "null"
            comPedido.importeDesto = 0
        Else
            'comPedido.condicion = id_glo_condicion
            comPedido.fechaVence = "GETDATE() + " + oUtil.getDataValue("CPAGO", comPedido.condicion).ToString
            If tipoRuta <> "16" Then
                comPedido.importeDestoPP = 0
            End If
            comPedido.importeDesto = 0
            comPedido.porcentajeDesto = 0
        End If

        '--- Agregar a la factura el descuento por devolucion de envase
        comPedido.importeDestoEnv = 0

        '--- Vincular la factura con el recibo
        comPedido.idReciboRelacionado = 0
        'MsgBox(comPedido.motivo)
        '--- Crear factura en la base de datos     
        If comPedido.isPedido Then
            objDocumento.crearOrdenVentaCambio(lstFacturaDetalle, comPedido, comCliente)
        End If

        '--- Actualizar los datos del encabezado y detalle de la orden de venta
        objDocumento.actualizarFactura(comPedido, comPedido.isContado)



        '--- Crear Nota de Credito en la base de datos
        comNotaCreditoEnv.idEncFacturaRelacionada = comPedido.idEncabezado
        'comNotaCreditoEnv.idReciboRelacionado = comRecibo.idEncabezado
        'If comNotaCreditoEnv.importe <> 0 Then objDocumento.crearNotaDeCredito(lstNotaCreditoDetalle, comNotaCreditoEnv)


        '--- Agregar movimientos de inventario
        objInventario.actualizarInventario(comPedido.idEncabezado, -1, comPedido.ttipo)


        '--- Vincular el recibo con la factura   [Este lo vamos a hacer por objetos]
        comRecibo.fechaAnula = "null"
        comRecibo.idEncFacturaRelacionada = comPedido.idEncabezado
        'objDocumento.actualizarRecibo(comRecibo)


        '--- Crear cuenta por cobrar
        'crearCuentaPorCobrar(comCliente, comPedido, comRecibo)

        '--- Confirmar el despacho realizado
        oDespacho.marcarPedidoDespacho(comPedido.idPedido, True, comCliente)

        '--- Confirmar la operacion comercial
        If Not objRuta.confirmarOperacionComercial() Then
            Throw New Exception("Las operaciones comerciales no se pudieron confirmar")
        End If


        '--- Imprimir Documentos
        Try

            'If Not objImpresion.imprimeRecibo(comRecibo.idEncabezado, False) Then
            ' MsgBox("La impresora no esta disponible.")
            ' numero = 0
            ' End If

            'objImpresion.imprimeRecibo(comRecibo.idEncabezado, False)
            objImpresion.imprimeCambio(comPedido.idEncabezado, comRecibo.idEncabezado, False)
            objImpresion.imprimeCambio(comPedido.idEncabezado, comRecibo.idEncabezado, False)
            'objDocumento.numeroImpresiones(comRecibo.idEncabezado, "RECIBO", numero)
            'objDocumento.numeroImpresiones(comPedido.idEncabezado, "FACTURA", numero)
            'If Not comNotaCreditoEnv.idEncabezado = 0 Then
            'objImpresion.ImprimeNotaCredito(comNotaCreditoEnv.idEncabezado, False, comPedido.serie + comPedido.numero)
            'objDocumento.numeroImpresiones(comNotaCreditoEnv.idEncabezado, "NC", numero)
            'End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        '--- Actualizar correlativos
        Dim cRecibo, cFactura, cNotaCredito, cCambio As New CorrelativoCO
        objRutaBL.getIndicadoresCorrelativo(cRecibo, cFactura, cNotaCredito, cCambio)
        Cursor.Current = Cursors.Default

        '--- Ejecutar Backup de la base de datos
        Try
            Dim ce As New ceClient
            ce.sdf_backup()
        Catch ex As Exception
        End Try
    End Function



    Public Function crearCuentaPorCobrar(ByVal Cliente As ClienteCO, ByVal Factura As documentoCO, ByVal Recibo As documentoCO) As Boolean
        Dim dtFactura As New DataTable
        Dim item As ItemCO
        Dim idcxc As String
        Dim oRecibo As New Recibo
        Dim items() As DataRow
        If Recibo.saldo > 0 Then

            '--- Encabezado
            idcxc = oRecibo.crearCxc(Factura, Cliente, Recibo)

            '--- Detalle
            dtFactura = objDocumento.getFacturaDetalle(Factura.idEncabezado)
            items = dtFactura.Select("idRubro = 'L' ")
            For Each row In items
                item = objDocumento.getFacturaByItem(row.Item("item").ToString, row)
                oRecibo.crearDetalleCxc(idcxc, item)
            Next
        Else
            Return True
        End If
    End Function

    Public Function cancelarOperacion(ByVal objDocumento As DocumentoBL, ByVal Formulario As Windows.Forms.Form, ByRef rlayer As rLayerHandler, ByRef paso As Integer, ByRef corriendo As Boolean) As Boolean

        '--- Cancelar Operacion
        Cursor.Current = Cursors.WaitCursor
        Dim msg = "Desea cancelar la operacion?"
        Dim title = "Cancelar"
        Dim style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.YesNo
        Dim response = MsgBox(msg, style, title)
        If response = MsgBoxResult.Yes Then
            objDocumento.eliminarDocumento("FACT")
            objDocumento.eliminarDocumento("REC")
            objDocumento.eliminarDocumento("NC")
            objDocumento.eliminarDocumento("BONI")
            glo_dtNcProductos.Rows.Clear()
            glo_lvNcAgregados.Clear()
            rlayer.codigo = 500
            corriendo = False
            Formulario.Close()
        End If
        Return True
    End Function

    Public Function unPasoAtras(ByVal objDocumento As DocumentoBL, ByVal Formulario As Windows.Forms.Form, ByVal rlayer As rLayerHandler, ByRef paso As Integer, ByRef corriendo As Boolean) As Boolean
        Cursor.Current = Cursors.WaitCursor
        corriendo = True
        paso += -1
        rlayer.codigo = 2
        Formulario.Close()
        Cursor.Current = Cursors.Default
        Return True
    End Function

#End Region

End Class
