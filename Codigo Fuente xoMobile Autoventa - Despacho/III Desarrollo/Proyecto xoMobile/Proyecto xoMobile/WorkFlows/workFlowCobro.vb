Imports System.Data
Imports System.Threading
Imports Proyecto_xoMobile_Packs


Public Class workFlowCobro

#Region " DECLARACION DE VARIABLESY OBJETOS "

    'Objetos de Comunicacion entre procesos
    Public comCliente As ClienteCO
    Public comNotaCreditoEnv, comNotaCreditoDpp, comNotaCreditoBoni, comRecibo, comCxc, comBonificacion As New documentoCO
    Public comItemBonificacion As New ItemCO
  

    'Variables locales
    Public dtDocumentosPorcobrar As New DataTable
    Public rLayer As New rLayerHandler
    Dim Documentos() As DataRow
    Dim paso As Integer
    Dim corriendo As Boolean = False
    Dim lstNotaCreditoDetalle As New Windows.Forms.ListView

    'Objetos de la capa de negocio
    Dim oUtil As New UtilitarioBL
    Dim msgCollection As New messageCollection



#End Region

    Public Function ejecutarCobro() As Boolean
        corriendo = True
        paso = 1

        '--- Pasos para ejecutar el cobro
        Select Case paso
            Case Is = 1
                If Not stepCobroCxc() Then Return False
        End Select

        For Each row In Documentos
            corriendo = True
            glo_dtNcProductos.Rows.Clear()
            While corriendo
                Select Case paso
                    Case Is = 2
                        stepRecoleccionEnvaseCobro(row)
                    Case Is = 3
                        If pagoDeContado() Then
                            stepNotaCreditoDpp()
                        Else
                            stepClearNotaCreditoDpp()
                        End If
                    Case Is = 4
                        stepPago()
                    Case Is = 5
                        imprimirDocumentos()
                        paso = 2
                        corriendo = False
                    Case Is = 6
                        Exit For
                End Select
            End While
        Next
        Return True
    End Function

    Public Function stepCobroCxc() As Boolean

        '--- CU20: Realizar Cobro
        Dim frmCxc As New frmCobro
        frmCxc.v_com_cliente = comCliente
        frmCxc.ShowDialog()


        '--- Evaluar la ejecucion del proceso CU20
        rLayer = frmCxc.rlayer
        Select Case rLayer.codigo
            Case 500
                '--- Codigo de aborto de la operacion
                corriendo = False
                paso = 6
                Return False

            Case Is = 1
                '--- Hay un mensaje de error disponible en el texto
                MsgBox("Error en el proceso de cobro:" + vbCrLf + rLayer.texto)
                Return False
        End Select

        '--- Recuperar objetos del proceso CU20
        dtDocumentosPorcobrar = frmCxc.dtDocumentosPorcobrar

        '--- Seleccionar los documentos a pagar
        Documentos = dtDocumentosPorcobrar.Select("seleccionado = 1")
        paso = 2
        Return True
    End Function

#Region " FUNCION CICLICA, CANCELACION DE DOCUMENTOS"

    Public Function stepRecoleccionEnvase(ByVal rowDocumento As DataRow) As Boolean

        Dim frmRecoleccionEnvase As New frmIngreso

        '--- Documento de CXC
        With rowDocumento
            comCxc.idEncabezado = .Item("id_cxc").ToString
            comCxc.importeDesto = oUtil.isDecimal(.Item("importeDesto").ToString)
            comCxc.serie = Trim(.Item("serie").ToString())
            comCxc.numero = Trim(.Item("numero").ToString())
            comCxc.importe = .Item("saldo").ToString
            comCxc.ttipo = "CXC"
            comCxc.diasVencidos = oUtil.isInteger(.Item("diasVencidos").ToString(), 1)
            comCxc.diasVencidosE = oUtil.isInteger(.Item("diasVencidosE").ToString(), 1)
            comCxc.importeDestoEnv = 0
            comCxc.soloEfectivo = .Item("soloEfectivo").ToString()
            comCxc.aceptaAbono = True
            comCxc.montoTotal = .Item("importe").ToString()
            comCxc.serieFEL = .Item("serieFel").ToString()
            comCxc.preimpreso = .Item("numeroautorizacion").ToString()
            If (comCxc.diasVencidosE) >= 60 Then
                paso = 3
                Return True
            End If
            
        End With


        '--- CU21:Recoleccion de envase
        id_glo_aplicacion = "nc"

        frmRecoleccionEnvase.v_com_cliente = comCliente
        frmIngreso.lblNombreCliente.Text = comCliente.negocio
        frmRecoleccionEnvase.lblAtencion.Text = "Envase"
        frmRecoleccionEnvase.picMenu.Enabled = False
        frmRecoleccionEnvase.lblNombreCliente.Text = comCliente.negocio
        frmRecoleccionEnvase.Text = "Pago " + comCxc.serie + " " + comCxc.numero

        co_glo_NextForm = True
        frmRecoleccionEnvase.ShowDialog()
        Cursor.Current = Cursors.Default

        '--- Evaluar la ejecucion del proceso CU21
        rLayer = frmRecoleccionEnvase.rlayer
        Select Case rLayer.codigo
            'Case 500
            '    '--- Codigo de aborto de la operacion
            '    paso = frmRecoleccionEnvase.comPaso
            '    corriendo = frmRecoleccionEnvase.comCorriendo
            '    Return False

            Case Is = 1
                '--- Hay un mensaje de error disponible en el texto
                MsgBox("Error en el proceso de cobro:" + vbCrLf + rLayer.texto)
                Return False

            Case Is = 2
                paso -= 1
                corriendo = False
                Return True

            Case Is = 3
                corriendo = False
                Return False

          
        End Select

        '--- Recuperar objetos del proceso CU21
        comNotaCreditoEnv = frmRecoleccionEnvase.comDocumento
        lstNotaCreditoDetalle = frmRecoleccionEnvase.lstAgregados
        comNotaCreditoEnv.serieFEL = comCxc.serieFEL
        comNotaCreditoEnv.preimpreso = comCxc.preimpreso
        comNotaCreditoEnv.diasVencidosE = comCxc.diasVencidosE
        
        '--- Completar datos de nota de credito6
        comNotaCreditoEnv.idEncCxcRelacionada = comRecibo.idEncCxcRelacionada
        comNotaCreditoEnv.condicion = id_glo_condicion


        '--- Recuperar objetos del proceso CU21
        'comNotaCreditoEnv = frmRecoleccionEnvase.comDocumento

        '--- Agregar al recibo el descuento por devolucion de envase
        comCxc.importeDestoEnv = comNotaCreditoEnv.importe

        paso = 3
        Return True
    End Function


    Public Function stepRecoleccionEnvaseCobro(ByVal rowDocumento As DataRow) As Boolean

        Dim frmRecoleccionEnvase As New frmIngreso


        '--- Documento de CXC
        With rowDocumento
            comCxc.idEncabezado = .Item("id_cxc").ToString
            comCxc.importeDesto = oUtil.isDecimal(.Item("importeDesto").ToString)
            comCxc.serie = Trim(.Item("serie").ToString())
            comCxc.numero = Trim(.Item("numero").ToString())
            comCxc.importe = .Item("saldo").ToString
            comCxc.ttipo = "CXC"
            comCxc.diasVencidos = oUtil.isInteger(.Item("diasVencidos").ToString(), 1)
            comCxc.diasVencidosE = oUtil.isInteger(.Item("diasVencidosE").ToString(), 1)
            comCxc.importeDestoEnv = 0
            comCxc.soloEfectivo = .Item("soloEfectivo").ToString()
            comCxc.aceptaAbono = True
            comCxc.montoTotal = .Item("importe").ToString()
            comCxc.serieFEL = .Item("serieFel").ToString()
            comCxc.preimpreso = .Item("numeroautorizacion").ToString()
            If (comCxc.diasVencidosE) >= 60 Then
                paso = 3
                Return True
            End If
            
        End With


        '--- CU21:Recoleccion de envase
        id_glo_aplicacion = "nc"

        frmRecoleccionEnvase.v_com_cliente = comCliente
        frmIngreso.lblNombreCliente.Text = comCliente.negocio
        frmRecoleccionEnvase.lblAtencion.Text = "Envase"
        frmRecoleccionEnvase.picMenu.Enabled = False
        frmRecoleccionEnvase.lblNombreCliente.Text = comCliente.negocio
        frmRecoleccionEnvase.Text = "Pago " + comCxc.serie + " " + comCxc.numero

        'Cuando se genera un cobro debe hacer este proceso usando un id_global2 solo para identificar que la NC viene de un cobro
        id_glo_aplicacion2 = "cobro"
        frmRecoleccionEnvase.txtEnvaceA.Text = (comCxc.importe * 75) / 100


        co_glo_NextForm = True
        frmRecoleccionEnvase.ShowDialog()
        Cursor.Current = Cursors.Default

        '--- Evaluar la ejecucion del proceso CU21
        rLayer = frmRecoleccionEnvase.rlayer
        Select Case rLayer.codigo
            'Case 500
            '    '--- Codigo de aborto de la operacion
            '    paso = frmRecoleccionEnvase.comPaso
            '    corriendo = frmRecoleccionEnvase.comCorriendo
            '    Return False

            Case Is = 1
                '--- Hay un mensaje de error disponible en el texto
                MsgBox("Error en el proceso de cobro:" + vbCrLf + rLayer.texto)
                Return False

            Case Is = 2
                paso -= 1
                corriendo = False
                Return True

            Case Is = 3
                corriendo = False
                Return False


        End Select

        '--- Recuperar objetos del proceso CU21
        comNotaCreditoEnv = frmRecoleccionEnvase.comDocumento
        lstNotaCreditoDetalle = frmRecoleccionEnvase.lstAgregados
        comNotaCreditoEnv.serieFEL = comCxc.serieFEL
        comNotaCreditoEnv.preimpreso = comCxc.preimpreso
        comNotaCreditoEnv.diasVencidosE = comCxc.diasVencidosE

        '--- Completar datos de nota de credito6
        comNotaCreditoEnv.idEncCxcRelacionada = comRecibo.idEncCxcRelacionada
        comNotaCreditoEnv.condicion = id_glo_condicion


        '--- Recuperar objetos del proceso CU21
        'comNotaCreditoEnv = frmRecoleccionEnvase.comDocumento

        '--- Agregar al recibo el descuento por devolucion de envase
        comCxc.importeDestoEnv = comNotaCreditoEnv.importe

        paso = 3
        Return True
    End Function

    Public Function stepPago() As Boolean
        '--- CU31:Pago del documento
        Dim frmPago As New frmPago
        frmPago.v_com_cliente = comCliente
        frmPago.v_com_documento = comCxc
        frmPago.v_com_documento.ttipo = "CXC"
        frmPago.Text = "Pago " + comCxc.serie + " " + comCxc.numero
        'frmPago.isContado = True
        frmPago.ShowDialog()

        '--- Evaluar la ejecucion del proceso CU31
        rLayer = frmPago.rlayer
        Select Case rLayer.codigo
            'Case 500
            '    '--- Codigo de aborto de la operacion
            '    corriendo = frmPago.comCorriendo
            '    paso = frmPago.comPaso
            '    Return False

            Case Is = 1
                '--- Hay un mensaje de error disponible en el texto
                MsgBox("Error en el proceso de cobro:" + vbCrLf + rLayer.texto)
                Return False

            Case Is = 2
                paso -= 2
                Return True

            Case Is = 3
                corriendo = False
                Return False


        End Select

        '--- Recuperar objetos del proceso CU31
        comRecibo = frmPago.comDocumentoPago

        '--- Vincular la nota de credito de envase con el recibo
        Dim oNotaCredito As New NotaCreditoDT
        oNotaCredito.vincularRecibo(comRecibo.idEncabezado, comNotaCreditoEnv.idEncabezado)
        paso = 5

        Return True
    End Function

    Private Function pagoDeContado() As Boolean
        If comCxc.diasVencidos <= 0 And Math.Abs(oUtil.isDecimal(comCxc.importeDesto)) > 0 Then
            If msgCollection.raiseMensaje(100, FormatCurrency(comCxc.importeDesto, 2)) = MsgBoxResult.Yes Then
                comCxc.aceptaAbono = False
                comCxc.soloAbono = False
                paso = 4
                Return True
            Else
                comCxc.aceptaAbono = True   'Agregado validar caso DPP
                comCxc.soloAbono = True
                comCxc.importeDesto = 0
                paso = 4
                Return False
            End If
        Else
            comCxc.soloAbono = False
            comCxc.importeDesto = 0
            paso = 4
            Return False
        End If
    End Function

    Private Function stepNotaCreditoDpp() As Boolean
        comNotaCreditoDpp.importe = comCxc.importeDesto
        comNotaCreditoDpp.estado = 0
        comNotaCreditoDpp.ttipo = 5
        comNotaCreditoDpp.tipoDevolucion = 6
        comNotaCreditoDpp.condicion = id_glo_condicion
        Return True
    End Function

    Private Function stepClearNotaCreditoDpp() As Boolean
        '--- Importe 0 crea la condicion para no grabar
        comNotaCreditoDpp.importe = 0
        Return True
    End Function

    Private Function imprimirDocumentos() As Boolean
        '--- Impresion de documentos
        Try
            'Dim starter As New ThreadStart(AddressOf Me.confirmarOperacion)
            'Dim t As New Thread(starter)
            't.IsBackground = True
            't.Start()
            confirmarOperacion()
        Catch ex As Exception
        End Try
    End Function
#End Region

    Private Function confirmarOperacion() As Boolean
        Dim objRecibo As New Recibo

        Dim objImpresion As New ImpresionBL
        Dim objDocumento As New DocumentoBL
        Dim objRuta As New RutaBL
        Dim dtCxcDetalle As New DataTable

        Dim Fel As New Generador

        '--- Datos complementarios de nota de credito
        comNotaCreditoEnv.idReciboRelacionado = comRecibo.idEncabezado
        comNotaCreditoDpp.idReciboRelacionado = comRecibo.idEncabezado

        '--- Crear Nota de Credito en la base de datos 
        If comNotaCreditoEnv.importe <> 0 Then

            'GENERACION DEL DOCUMENTO EN HH - ENVIO DE DATOS PARA DOCUMENTO ELECTRONICO - SI EL DOCUMENTO TIENE MAS DE 60 DIAS ES NOTA DE ABONO
            If (id_glo_fel = "X") Then
                If (comNotaCreditoEnv.diasVencidosE > 60) Then
                    objDocumento.crearNotaDeCreditoNA(lstNotaCreditoDetalle, comNotaCreditoEnv)
                    If (id_glo_fel = "X") Then
                        MessageBox.Show("ESTE DOCUMENTO TIENE MAS DE 60 DIAS, GENERA NOTA DE ABONO ")
                        Fel.generadorNABONO(comNotaCreditoEnv, comCliente)
                    End If

                Else
                    objDocumento.crearNotaDeCredito(lstNotaCreditoDetalle, comNotaCreditoEnv)
                    If (id_glo_fel = "X") Then
                        Fel.generadorNCCXC(comNotaCreditoEnv, comCliente)
                    End If
                End If
            Else
                objDocumento.crearNotaDeCredito(lstNotaCreditoDetalle, comNotaCreditoEnv)
            End If
        End If
        
        '--- Crear Nota de Credito DPP 
        If comNotaCreditoDpp.importe <> 0 Then
            comNotaCreditoDpp.serieFEL = comNotaCreditoEnv.serieFEL
            comNotaCreditoDpp.preimpreso = comNotaCreditoEnv.preimpreso
            dtCxcDetalle = objRecibo.getDocumentosPorcobrarDetalle(comCxc.idEncabezado)

            'GENERACION DEL DOCUMENTO EN HH - ENVIO DE DATOS PARA DOCUMENTO ELECTRONICO - SI EL DOCUMENTO TIENE MAS DE 60 DIAS ES NOTA DE ABONO
            If (comNotaCreditoEnv.diasVencidosE > 60) Then
                objDocumento.crearNotaDeCreditoDPPNA(dtCxcDetalle, comNotaCreditoDpp)
                If (id_glo_fel = "X") Then
                    MessageBox.Show("ESTE DOCUMENTO TIENE MAS DE 60 DIAS, GENERA NOTA DE ABONO")
                    Fel.generadorNABONO(comNotaCreditoDpp, comCliente)
                End If
                
            Else
                objDocumento.crearNotaDeCreditoDPP(dtCxcDetalle, comNotaCreditoDpp)
                If (id_glo_fel = "X") Then
                    Fel.generadorNCCXCDPP(comNotaCreditoDpp, comCliente)
                End If


            End If

        End If


        '--- Confirmar la operacion comercial
        If Not objRuta.confirmarOperacionComercial() Then
            Throw New Exception("Las operaciones comerciales no se pudieron confirmar")
        End If

        '--- Actualizar la cuenta por cobrar
        actualizarCuentaPorCobrar(comRecibo, comCxc.idEncabezado)



        '--- Imprimir Documentos
        Try
            If Not objImpresion.imprimeRecibo(comRecibo.idEncabezado, False) Then
                MsgBox("La impresora no esta disponible.")
            End If


            If Not comNotaCreditoEnv.idEncabezado = 0 Then
                'objImpresion.ImprimeNotaCredito(comNotaCreditoEnv.idEncabezado, False, comNotaCreditoEnv.serie + comNotaCreditoEnv.numero)
                If (id_glo_fel = "X") Then
                    objImpresion.ImprimeNotaCredito(comNotaCreditoEnv.idEncabezado, False, comCxc.serie + comCxc.numero)
                Else
                    objImpresion.ImprimeNotaCreditoSINFEL(comNotaCreditoEnv.idEncabezado, False, comCxc.serie + comCxc.numero)
                End If
                'objImpresion.ImprimeNotaCredito(comNotaCreditoEnv.idEncabezado, False, comCxc.serie + comCxc.numero)

            End If
            If Not comNotaCreditoDpp.idEncabezado = 0 Then
                objImpresion.ImprimeNotaCredito(comNotaCreditoDpp.idEncabezado, False, comCxc.serie + comCxc.numero)

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        '--- Actualizar correlativos
        Dim cRecibo, cFactura, cNotaCredito, cCambio As New CorrelativoCO
        objRuta.getIndicadoresCorrelativo(cRecibo, cFactura, cNotaCredito, cCambio)
        Cursor.Current = Cursors.Default

        '--- Ejecutar Backup de la base de datos
        Try
            Dim ce As New ceClient
            ce.sdf_backup()
        Catch ex As Exception
        End Try
    End Function

    Public Sub actualizarCuentaPorCobrar(ByVal Recibo As documentoCO, ByVal idCxc As String)
        Dim oRecibo As New Recibo
        Dim objCliente As New ClienteBL
        Dim pcobro, pago, montoTotal As Decimal
        Dim rLayer As New rLayerHandler
        Dim objDocumento As New DocumentoBL

        Try
            '--- Alcanza el valor minimo para considerarse cobro efectivo.
            pcobro = oUtil.getDataValue("PCOBRO", "%MIN_COBRO_COMISION")
            pago = oUtil.isDecimal(Recibo.importePago) + oUtil.isDecimal(Recibo.importeDestoEnv)
            montoTotal = oUtil.isDecimal(comCxc.importe)

            '--- Revisar si este documento ya tuvo pagos previos
            rLayer = objDocumento.pagosRealizadosCXC(idCxc, montoTotal, pago)


            '--- Calcula si el pago es efectivo
            If (pago * 100) / montoTotal >= pcobro Or Recibo.saldo = 0 Then

                Recibo.pagoEfectivo = "'True'"
            Else
                Recibo.pagoEfectivo = "'False'"
            End If

            '--- Si aun hay saldo pendiente de cancelar
            oRecibo.actualizaCxc(Recibo.saldo, idCxc, Recibo.pagoEfectivo)


            Dim valorImporte As Decimal
            valorImporte = oUtil.isDecimal(Recibo.importePago) + oUtil.isDecimal(Recibo.importeDesto) + oUtil.isDecimal(Recibo.importeDestoEnv)

            '--- Actualizar el credito disponible en el objeto y en la BD
            objCliente.actualizarCreditoDisponible(comCliente, valorImporte)

        Catch ex As Exception
            MsgBox("No se actualizo la CXC." + ex.Message)
        End Try
        

    End Sub

End Class
