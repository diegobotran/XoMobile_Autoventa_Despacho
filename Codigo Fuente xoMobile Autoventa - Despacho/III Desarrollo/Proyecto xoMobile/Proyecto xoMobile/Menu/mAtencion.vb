Imports System.Data
Imports Proyecto_xoMobile_Packs

Public Class mAtencion

#Region " DECLARACION DE VARIABLES Y OBJETOS "


    'Objetos
    Dim objDocumentoBL As New DocumentoBL
    Dim objClienteBL As New ClienteBL
    Dim objBitacora As New BitacoraBL
    Dim oRuta As New RutaBL
    Dim oWorkflow As New workflowBL
    Dim rLayer As New rLayerHandler

    'Variables de comunicacion entre capas    
    Public Cliente As New ClienteCO
    Dim workflow As New WorkFlowCo
    Dim Ruta As New RutaCO
    Dim objUtil As New UtilitarioDT
    Dim objrecibo As New Recibo

    'Variable que determina si un Workflow se realizo con exito.
    Dim wfExitoso As Boolean = False


    'Item de atencion seleccionado
    Dim itemSelected As Integer
    Dim TipoMotivo As String


#End Region

#Region " INICIALIZAR FORMULARIO "
    Private Sub mAtencion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            

            ''''*--- <DRIVER> ---------------------
            'Dim oCliente As New ClienteBL
            'id_glo_cliente = 55023602
            'id_glo_ruta = 197
            'Cliente = oCliente.getDetalleDelCliente(55023602) ' (55003789) '(55003964)
            'co_glo_despacho = False
            'xo_validaInventario = True
            'co_glo_searchProducto = "codigo"
            'co_glo_searchProducto = "descripcion"
            ''tipoRuta = "16" 'Despacho
            'tipoRuta = "17" 'Autoventa 
            '''*--- <DRIVER> ---------------------

            '--- Valores iniciales
            Ruta = oRuta.getActiva
            Me.Text = Cliente.negocio

            '--- Inicializar el menu de opciones
            crearMenuTextual()
            asignarImagenes()
            Me.lstMenu.Items(0).Selected = True

            '--- Inicializar GPS para tomar esta marca
            Try
                gps.start()
            Catch ex As Exception
                gps.stop()
            End Try

            '--- Deshabilitar opciones no disponibles (Grays)
            setGrayMenuItems()

            ''---Registrar operacion en bitacora
            Dim objBitacora As New BitacoraBL
            If objUtil.toEntero(gps.Satellites) <= 0 Then
                objBitacora.registrarOperacion(11, id_glo_cliente, 0, 0)
                'MsgBox("GPS registrado con : " & gps.Satellites & "satelites @ no sabemos donde estas.")
            Else
                objBitacora.registrarOperacion(11, id_glo_cliente, gps.Latitude, gps.Longitude)
                'MsgBox("GPS registrado con : " & gps.Satellites & "@" & gps.Latitude & " , " & gps.Longitude)
            End If

            '--- Reestablecer el cursor
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub setGrayMenuItems()
        Dim dtDocumentosPorcobrar As New DataTable
        Dim oDespacho As New DespachoBL
        Dim dtDespachos As New DataTable
        Dim rLayer As New rLayerHandler
        Dim flagCobro As Boolean = True
        Dim flagDespacho As Boolean = True

        '---Revisar si hay documentos por cobrar        
        dtDocumentosPorcobrar = objrecibo.getDocumentosPorcobrar
        If dtDocumentosPorcobrar.Rows.Count() <= 0 Then
            wfExitoso = True
            workflow.cobro = True
            lstMenu.Visible = True
            Cursor.Current = Cursors.Default
            lstMenu.Focus()
            flagCobro = False
        End If

        '--- Revisar si hay Despachos programados        
        If tipoRuta = "16" Then
            id_glo_aplicacion = ""
            dtDespachos = oDespacho.getDespachos(Cliente.codigo, rLayer)
            If Not rLayer.codigo = 0 Then
                wfExitoso = True
                workflow.despacho = True
                lstMenu.Visible = True
                Cursor.Current = Cursors.Default
                lstMenu.Focus()
                flagDespacho = False
            End If
        End If

        For I = 0 To lstMenu.Items.Count - 1
            If lstMenu.Items(I).Tag.ToString.ToLower = "cobro" And Not flagCobro Then
                lstMenu.Items(I).ImageIndex = lstMenu.Items.Count + 1
                lstMenu.Items(I).ForeColor = Color.Gray
                lstMenu.Items(I).Tag = "mAutoRealizado"

            End If

            If lstMenu.Items(I).Tag.ToString.ToLower = "despacho" And Not flagDespacho Then
                lstMenu.Items(I).ImageIndex = lstMenu.Items.Count + 1
                lstMenu.Items(I).ForeColor = Color.Gray
                lstMenu.Items(I).Tag = "mAutoRealizado"
            End If

            
        Next

    End Sub

    Private Sub crearMenuTextual()
        Dim dtWorkFlow As DataTable
        Dim dtWorkFlowCV As DataTable
        Dim dtWorkFlow2 As DataTable
        Dim dtWorkFlow3 As DataTable
        Dim nombreOpcion As String

        '--- Crear menu de opciones
        dtWorkFlow = oWorkflow.crearWorkflow(Cliente, rLayer)
        dtWorkFlow2 = oWorkflow.crearWorkflow2(Cliente, rLayer)
        dtWorkFlow3 = oWorkflow.crearWorkflow3(Cliente, rLayer)
        dtWorkFlowCV = oWorkflow.crearWorkflowCV(Cliente, rLayer)

        If Cliente.codigo = id_glo_clienteGenerico Or Cliente.tipoDi = "NUEVO" Or Cliente.tipoDi = "TEMP" Then
            For i As Integer = 0 To dtWorkFlowCV.Rows.Count() - 1
                nombreOpcion = Trim(dtWorkFlowCV.Rows(i).Item("showValue").ToString().ToLower)
                Dim lvi As New ListViewItem("     " & nombreOpcion)
                lvi.Tag = nombreOpcion
                lstMenu.Items.Add(lvi)

                '--- Identificar los item opcionales
                If dtWorkFlowCV.Rows(i).Item("tabla") = "WORKFLOW" Then
                    lstMenu.Items(i).ForeColor = Color.DarkBlue
                Else
                    lstMenu.Items(i).ForeColor = Color.Black
                End If

                '--- Identificar los item realizados
                If dtWorkFlowCV.Rows(i).Item("realizado") = 1 Then
                    lvi.Tag = "mRealizado"
                    lstMenu.Items(i).ForeColor = Color.DarkGreen
                End If
            Next
        Else

            If (tipoRuta <> "16") Then
                If (dtWorkFlow2.Rows.Count() > 0) Then
                    For i As Integer = 0 To dtWorkFlow2.Rows.Count() - 1
                        nombreOpcion = Trim(dtWorkFlow2.Rows(i).Item("nvalue").ToString().ToLower)
                        Dim lvi As New ListViewItem("     " & nombreOpcion)
                        lvi.Tag = nombreOpcion
                        lstMenu.Items.Add(lvi)

                        '--- Identificar los item opcionales
                        If dtWorkFlow2.Rows(i).Item("tabla") = "WORKFLOW" Then
                            lstMenu.Items(i).ForeColor = Color.DarkBlue
                        Else
                            lstMenu.Items(i).ForeColor = Color.Black
                        End If

                        '--- Identificar los item realizados
                        If dtWorkFlow2.Rows(i).Item("realizado") = 1 Then
                            lvi.Tag = "mRealizado"
                            lstMenu.Items(i).ForeColor = Color.DarkGreen
                        End If
                    Next

                Else
                    '--- Crear menu de opciones
                    For i As Integer = 0 To dtWorkFlow.Rows.Count() - 1
                        nombreOpcion = Trim(dtWorkFlow.Rows(i).Item("showValue").ToString().ToLower)
                        Dim lvi As New ListViewItem("     " & nombreOpcion)
                        lvi.Tag = nombreOpcion
                        lstMenu.Items.Add(lvi)

                        '--- Identificar los item opcionales
                        If dtWorkFlow.Rows(i).Item("tabla") = "WORKFLOW" Then
                            lstMenu.Items(i).ForeColor = Color.DarkBlue
                        Else
                            lstMenu.Items(i).ForeColor = Color.Black
                        End If

                        '--- Identificar los item realizados
                        If dtWorkFlow.Rows(i).Item("realizado") = 1 Then
                            lvi.Tag = "mRealizado"
                            lstMenu.Items(i).ForeColor = Color.DarkGreen
                        End If
                    Next
                End If
            Else
                If (dtWorkFlow3.Rows.Count() > 0) Then
                    '--- Crear menu de opciones
                    For i As Integer = 0 To dtWorkFlow3.Rows.Count() - 1
                        nombreOpcion = Trim(dtWorkFlow3.Rows(i).Item("nvalue").ToString().ToLower)
                        Dim lvi As New ListViewItem("     " & nombreOpcion)
                        lvi.Tag = nombreOpcion
                        lstMenu.Items.Add(lvi)

                        '--- Identificar los item opcionales
                        If dtWorkFlow3.Rows(i).Item("tabla") = "WORKFLOW" Then
                            lstMenu.Items(i).ForeColor = Color.DarkBlue
                        Else
                            lstMenu.Items(i).ForeColor = Color.Black
                        End If

                        '--- Identificar los item realizados
                        If dtWorkFlow3.Rows(i).Item("realizado") = 1 Then
                            lvi.Tag = "mRealizado"
                            lstMenu.Items(i).ForeColor = Color.DarkGreen
                        End If
                    Next
                Else
                    '--- Crear menu de opciones
                    For i As Integer = 0 To dtWorkFlow.Rows.Count() - 1
                        nombreOpcion = Trim(dtWorkFlow.Rows(i).Item("showValue").ToString().ToLower)
                        Dim lvi As New ListViewItem("     " & nombreOpcion)
                        lvi.Tag = nombreOpcion
                        lstMenu.Items.Add(lvi)

                        '--- Identificar los item opcionales
                        If dtWorkFlow.Rows(i).Item("tabla") = "WORKFLOW" Then
                            lstMenu.Items(i).ForeColor = Color.DarkBlue
                        Else
                            lstMenu.Items(i).ForeColor = Color.Black
                        End If

                        '--- Identificar los item realizados
                        If dtWorkFlow.Rows(i).Item("realizado") = 1 Then
                            lvi.Tag = "mRealizado"
                            lstMenu.Items(i).ForeColor = Color.DarkGreen
                        End If
                    Next
                End If
            End If
        End If

        'Crear menu de operaciones fijas en el menu de  Workflow
        Dim lvi2 As New ListViewItem("     Datos del cliente")
        Dim lvi3 As New ListViewItem("     Anulacion")
        Dim lvi5 As New ListViewItem("     Cambio")
        If (tipoRuta <> "16") Then
            Dim lvi4 As New ListViewItem("     Presupuesto")
            lvi4.Tag = "presupuesto"
            lstMenu.Items.Add(lvi4)
        End If
        lvi5.Tag = "cambio"
        lvi2.Tag = "cliente"
        lvi3.Tag = "anulacion"

        lstMenu.Items.Add(lvi5)
        lstMenu.Items.Add(lvi2)
        lstMenu.Items.Add(lvi3)


    End Sub

    Private Sub asignarImagenes()

        lstItemImages.Images.Clear()
        '--- Recorrer el menu para asignar imagenes el resource imagen al item correspondiente
        For i As Integer = 0 To lstMenu.Items.Count - 1
            Dim imageItem As System.Drawing.Image
            imageItem = My.Resources.ResourceManager.GetObject(lstMenu.Items(i).Tag)
            lstItemImages.Images.Add(imageItem)
        Next

        '--- Agregar imagenes de realizado y Gray realizado
        lstItemImages.Images.Add(My.Resources.mRealizado)
        lstItemImages.Images.Add(My.Resources.mAutoRealizado)

        '--- Asignar las imagenes a su correspondiente item
        For i As Integer = 0 To lstMenu.Items.Count - 1
            lstMenu.Items(i).ImageIndex = i
        Next
    End Sub

    Private Sub setAtencionRealizada(ByVal idAtencion As Integer)
        '--- Asignar el penultimo item correspondiente al icono de realizado
        lstMenu.Items(idAtencion).ImageIndex = lstMenu.Items.Count
        lstMenu.Items(idAtencion).ForeColor = Color.DarkGreen
    End Sub
#End Region

#Region " EJECUTAR OPCION DE ATENCION "

    '--- Seleccionar un item de atencion
    Private Sub lSoftSeleccionar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lSoftSeleccionar.Click
        ejecutarAtencion()
    End Sub

    Private Sub lstMenu_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lstMenu.KeyPress
        If e.KeyChar = ChrW(13) Then
            ejecutarAtencion()
        End If
    End Sub

    Private Sub ejecutarAtencion()

        '--- Obtener el indice del item seleccionado
        If Me.lstMenu.SelectedIndices.Count <= 0 Then
            Exit Sub
        End If

        '--- Validar si el item puede ser seleccionado en base al WorkFlow
        itemSelected = Me.lstMenu.SelectedIndices(0)
        If Not validarWorkFlow(itemSelected) Then
            MsgBox("Debe seguir el orden de atencion")
            Exit Sub
        End If

        '--- Inicializar Controles de ejecucion
        Cursor.Current = Cursors.WaitCursor
        lstMenu.Visible = False

        '--- Ejecutar el item seleccionado
        If Not lstMenu.Items(itemSelected).Tag = "mAutoRealizado" Then
            Select Case Trim(lstMenu.Items(itemSelected).Text.ToLower)
                Case "encuesta"
                    doEncuesta()
                Case "cobro"
                    doCobro()
                Case "inventario"
                    doInventario()
                Case "venta"
                    doVenta()
                Case "despacho"
                    doDespacho()
                Case "anulacion"
                    doAnulacion()
                Case "datos del cliente"
                    doDetalleCliente()
                Case "presupuesto"
                    doPresupuesto()
                Case "cambio"
                    doCambio()
            End Select
        End If
        Cursor.Current = Cursors.Default
        lstMenu.Visible = True
        lstMenu.Focus()
    End Sub

    Private Function doInventario()

        '-------------------------------------------------------------------------------------------------'
        '--------------------------------       Toma de inventario      ----------------------------------'
        '-------------------------------------------------------------------------------------------------'        
        Cursor.Current = Cursors.WaitCursor
        lstMenu.Visible = False

        Try
            Dim objBitacora As New BitacoraBL
            objBitacora.registrarOperacion(12, id_glo_cliente)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        '--- Realizar el workflow
        Dim wfInventario As New workFlowInventario
        wfInventario.comCliente = Cliente
        If wfInventario.ejecutarInventario Then
            wfExitoso = True
        Else
            wfExitoso = False
        End If

        '--- Bitacora: Fin Inventario físico
        Try
            wfBitacora(wfExitoso, 1, 2)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        '--- Actualizar tarea realizadas en el menu
        If wfExitoso Then setAtencionRealizada(itemSelected)
        lstMenu.Visible = True
        Cursor.Current = Cursors.Default
        lstMenu.Focus()
        Return wfExitoso

    End Function

    Private Function doEncuesta()

        '--- Crear una instancia del workflow Encuesta
        Dim frmEncuesta As New frmEncuesta

        '--- Bitacora: Inicio Encuesta       
        objBitacora.registrarOperacion(15, id_glo_cliente)

        '--- Paso de variables globales
        frmEncuesta.ShowDialog()
        If frmEncuesta.FinishFlag Then
            wfExitoso = True
        End If

        '--- Registrar en Bitacora: fin del inventario físico
        wfBitacora(wfExitoso, 7, 8)

        '--- Al finalizar el flujo, actualizar las tareas realizadas en el menu
        If wfExitoso Then setAtencionRealizada(itemSelected)
        Return wfExitoso
    End Function

    Private Function doCobro()

        If xo_restringeCobro Then
            MsgBox("Se ha deshabilitado la recuperacion de cartera.")
            Cursor.Current = Cursors.Default
            Return False
        End If

        '--- Antes de iniciar el wf eliminar registros pendientes
        objDocumentoBL.eliminarDocumento("FACT")
        objDocumentoBL.eliminarDocumento("REC")
        objDocumentoBL.eliminarDocumento("NC")

        '--- Registrar en Bitacora: inicio Cobro                             
        objBitacora.registrarOperacion(13, id_glo_cliente)

        '--- Crear una instancia del workflow Despacho
        Dim workflowCobra As New workFlowCobro
        workflowCobra.comCliente = Cliente
        wfExitoso = workflowCobra.ejecutarCobro
        lstMenu.Focus()

        '--- Registrar en Bitacora: Fin Cobro       
        wfBitacora(wfExitoso, 3, 4)

        '--- Al finalizar el flujo, actualizar las tareas realizadas en el menu
        setAtencionRealizada(itemSelected)


        Return wfExitoso
    End Function

    Private Function doVenta()

        If xo_restringeVenta Then
            MsgBox("Se ha deshabilitado la venta.")
            Cursor.Current = Cursors.Default
            Return False
        End If

        '--- Antes de iniciar el wf eliminar registros pendientes
        If objDocumentoBL.eliminarDocumento("FACT") Then
            MsgBox("La ultima operacion de facturacion no fue realizada con exito. Consulte los documentos emitidos y verifique que las operaciones esten al dia.")
        End If
        If objDocumentoBL.eliminarDocumento("REC") Then
            MsgBox("La ultima operacion de recibo no fue realizada con exito. Consulte los documentos emitidos y verifique que las operaciones esten al dia.")
        End If
        If objDocumentoBL.eliminarDocumento("NC") Then
            MsgBox("La ultima operacion de nota de credito no fue realizada con exito. Consulte los documentos emitidos y verifique que las operaciones esten al dia.")
        End If


        If objClienteBL.permiteAtencionConSaldo(Cliente, "VENTA") Then
            total_venta = 0
            total_envase = 0
            Dim ObjIngresoPedido As New frmIngreso()

            '--- Crear una instancia del workflow venta
            'ObjIngresoPedido.lblAtencion.Text = "Ingreso de orden de venta"
            ObjIngresoPedido.Text = Me.Text
            id_glo_aplicacion = "venta"

            '--- Paso de variables globales
            ObjIngresoPedido.v_com_cliente = Cliente

            '--- Registrar en Bitacora: inicio venta                         
            objBitacora.registrarOperacion(14, id_glo_cliente)


            '--- Crear una instancia del workflow venta
            Dim workflowDespacho As New workFlowVenta
            workflowDespacho.comCliente = Cliente
            wfExitoso = workflowDespacho.ejecutarVenta()
            lstMenu.Focus()

            '--- Registrar en Bitacora: fin venta             
            wfBitacora(wfExitoso, 5, 6)
            If wfExitoso Then setAtencionRealizada(itemSelected)
        End If
        Return wfExitoso
    End Function

    Private Function doDespacho()

        '-------------------------------------------------------------------------------------------------'
        '-----------------------------------       DESPACHO          -------------------------------------'
        '-------------------------------------------------------------------------------------------------'

        If xo_restringeVenta Then
            MsgBox("Se ha deshabilitado el despacho.")
            Cursor.Current = Cursors.Default
            Return False
        End If

        If objClienteBL.permiteAtencionConSaldo(Cliente, "DESPACHO") Then
            If co_glo_confirma_despacho Then
                MsgBox("Se ha deshabilitado el despacho.")
                Cursor.Current = Cursors.Default
                Return False
            End If


            '--- Registrar en Bitacora: inicio Despacho                              
            objBitacora.registrarOperacion(45, id_glo_cliente)

            '--- Crear una instancia del workflow Despacho
            Dim workflowDespacho As New workFlowVenta
            workflowDespacho.comCliente = Cliente
            wfExitoso = workflowDespacho.ejecutarDespacho()
            lstMenu.Focus()

            '--- Registrar en Bitacora: Fin Despacho       
            wfBitacora(wfExitoso, 46, 47)

            '--- Al finalizar el flujo, actualizar las tareas realizadas en el menu
            setAtencionRealizada(itemSelected)
        End If
        Return wfExitoso
    End Function

    Private Function doCambio()

        If xo_restringeVenta Then
            MsgBox("Se ha deshabilitado los cambios.")
            Cursor.Current = Cursors.Default
            Return False
        End If

        '-------------------------------------------------------------------------------------------------'
        '-----------------------------------       CAMBIOS          -------------------------------------'
        '-------------------------------------------------------------------------------------------------'


        If Cliente.cambio <> "X" Then
            MsgBox("Este cliente no tiene acceso a Cambios de producto..")
            Cursor.Current = Cursors.Default
            Return False
        End If

        '--- Registrar en Bitacora: inicio Despacho                              
        objBitacora.registrarOperacion(51, id_glo_cliente)

        '--- Crear una instancia del workflow Despacho
        Dim workflowDespacho As New workFlowVenta
        workflowDespacho.comCliente = Cliente
        wfExitoso = workflowDespacho.ejecutarCambio()
        lstMenu.Focus()

        '--- Registrar en Bitacora: Fin Despacho       
        wfBitacora(wfExitoso, 51, 47)

        '--- Al finalizar el flujo, actualizar las tareas realizadas en el menu
        setAtencionRealizada(itemSelected)

        Return wfExitoso
    End Function

    Private Sub doAnulacion()
        '-------------------------------------------------------------------------------------------------'
        '--------------------------       ANULACION DE DOCUMENTOS         --------------------------------'
        '-------------------------------------------------------------------------------------------------'
        If xo_restringeAnulacion Then
            MsgBox("Se ha deshabilitado la anulacion de documentos.")
            Cursor.Current = Cursors.Default
            Exit Sub
        End If

        '--- Antes de iniciar el wf eliminar registros pendientes
        objDocumentoBL.eliminarDocumento("FACT")
        objDocumentoBL.eliminarDocumento("REC")
        objDocumentoBL.eliminarDocumento("NC")

        '--- Crear una instancia del workflow Anulacion
        Dim frmDocumentosEmitidos As New frmDocumentosEmitidos()

        '--- Paso de variables globales
        frmDocumentosEmitidos.v_com_cliente = Cliente
        frmDocumentosEmitidos.ShowDialog()
    End Sub

    Private Sub doDetalleCliente()
        '-------------------------------------------------------------------------------------------------'
        '--------------------------       DETALLE DEL CLIENTE         --------------------------------'
        '-------------------------------------------------------------------------------------------------'
        If lstMenu.Items(itemSelected).Tag = "cliente" Then
            Dim frmDetalleCliente As New frmDetalleCliente()
            frmDetalleCliente.ShowDialog()
            lstMenu.Focus()
        End If
    End Sub

    Private Sub doPresupuesto()
        '-------------------------------------------------------------------------------------------------'
        '--------------------------      PRESUPUESTO CLIENTE         --------------------------------'
        '-------------------------------------------------------------------------------------------------'
        If lstMenu.Items(itemSelected).Tag = "presupuesto" Then
            Dim frmPresupuestoCliente As New frmPresupuestoCliente()
            frmPresupuestoCliente.ShowDialog()
            lstMenu.Focus()
        End If
    End Sub

    Private Function validarWorkFlow(ByVal itemSelected) As Boolean

        'Revisar si desde el inicio del listado hasta el item seleccionado hay un texto azul
        If itemSelected = 0 Then
            Return True
        Else
            If lstMenu.Items(itemSelected).ForeColor = Color.DarkBlue Then
                For i As Integer = 0 To itemSelected - 1
                    If lstMenu.Items(i).ForeColor = Color.DarkBlue Then
                        Return False
                    End If
                Next
            End If
            Return True

            Return True
        End If
    End Function
#End Region

#Region " SALIR DEL MENU "
    Private Sub RSoftSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RSoftSalir.Click
        Cursor.Current = Cursors.WaitCursor
        Try
            '--- Obtener las actividades realizadas
            Dim objBitacora As New BitacoraBL
            oWorkflow.statusWorkflow2(workflow, Cliente, rLayer, lstMenu)

            '--- Todas las actividades realizadas entonces salir
            If workflow.TodasRealizadas Then

                '--- Bitacora fin atencion
                objBitacora.registrarOperacion(20, id_glo_cliente)

                '--- Cambiar el status de atencion del cliente atendido totalmente
                objClienteBL.actualizarEstatusVisita(Cliente, 1)
                Me.Close()
                Exit Sub
            End If

            '--- Si ninguna esta realizada: listar motivos de no atencion
            If workflow.NingunaRealizada Then

                '--- Bitacora fin atencion
                objBitacora.registrarOperacion(39, id_glo_cliente)

                '--- Cambiar el status de atencion del cliente  no atendido
                objClienteBL.actualizarEstatusVisita(Cliente, 3)

                '--- Mostrar motivos de no atencion
                listarMotivos("NO_ATENCION")
                Exit Sub
            End If

            '--- Cambiar el status de atencion del cliente atendido parcialmente
            objClienteBL.actualizarEstatusVisita(Cliente, 2)
            objBitacora.registrarOperacion(40, id_glo_cliente)

            '--- Verificar operaciones comodin realizadas
            If (tipoRuta = "16" And Not workflow.despacho And Not co_glo_despacho) Then
                listarMotivos("NO_DESPACHO")
                Exit Sub
            ElseIf (tipoRuta <> "16" And Not workflow.venta And Not xo_restringeVenta) Then
                listarMotivos("NO_VENTA")
                Exit Sub
            End If
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            Cursor.Current = Cursors.Default
            Me.Close()
        End Try
    End Sub

    Private Sub listarMotivos(ByRef Motivo As String)
        Dim dtRazones As New DataTable
        Dim outil As New UtilitarioBL
        TipoMotivo = Motivo
        '--- Inicializar componentes
        panAtencion.Visible = True
        lstMenu.Enabled = False
        menuAttCliente.MenuItems(0).Enabled = False
        menuAttCliente.MenuItems(1).Enabled = False
        Select Case TipoMotivo
            Case "NO_VENTA"
                Label3.Text = "Seleccione la Razon de no venta y presione <ENTER>"
                dtRazones = outil.getListaTipo("RAZONES_NO_VENTA")
            Case "NO_ATENCION"
                dtRazones = objUtil.getListaTipo("RAZONES_NO_ATENCION")
            Case "NO_DESPACHO"
                Label3.Text = "Seleccione la Razon de no despacho y presione <ENTER>"
                dtRazones = outil.getListaTipo("RAZONES_NO_DESPACHO")
        End Select
        lstRazonAtencion.DataSource = dtRazones
        lstRazonAtencion.ValueMember = dtRazones.Columns(1).ToString
        lstRazonAtencion.DisplayMember = dtRazones.Columns(0).ToString
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub lstRazonAtencion_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lstRazonAtencion.KeyPress
        Select Case e.KeyChar()
            Case ChrW(13)
                Select Case TipoMotivo
                    Case "NO_VENTA"
                        objClienteBL.actualizarRazonNovisita(Cliente, lstRazonAtencion.SelectedValue, 2)
                    Case "NO_ATENCION"
                        objClienteBL.actualizarRazonNovisita(Cliente, lstRazonAtencion.SelectedValue, 1)
                    Case "NO_DESPACHO"
                        Dim oDespacho As New DespachoBL
                        oDespacho.actualizarMotivoDespacho(Cliente, lstRazonAtencion.SelectedValue)
                End Select
                Me.Close()
            Case ChrW(Keys.Escape)
                lstRazonAtencion.SelectedIndex = 0
                panAtencion.Visible = False
                lstMenu.Enabled = True
                menuAttCliente.MenuItems(0).Enabled = True
                menuAttCliente.MenuItems(1).Enabled = True
        End Select
    End Sub
#End Region

#Region " REGISTRO DE UNA OPERACION EN BITACORA "
    Private Sub wfBitacora(ByVal wfexito As Boolean, ByVal operacionExito As Integer, ByVal operacionFracaso As Integer)
        Dim error_msg = ""
        Try
            Select Case wfexito
                Case True
                    objBitacora.registrarOperacion(operacionExito, id_glo_cliente.ToString())
                Case False
                    objBitacora.registrarOperacion(operacionFracaso, id_glo_cliente.ToString())
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

#Region " METODOS UI "
    Private Sub panAtencion_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panAtencion.Paint
        objUtil.paintPannel(e, panAtencion)
    End Sub
#End Region

End Class