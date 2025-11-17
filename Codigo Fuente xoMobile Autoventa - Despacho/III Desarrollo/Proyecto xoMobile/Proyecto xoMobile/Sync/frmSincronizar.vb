Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Threading
'Imports System.Runtime.InteropServices
Imports System.Data.SqlServerCe
Imports Proyecto_xoMobile_Packs


Public Class frmImportar_

    '<DllImport("coredll")> _
    Private Shared Function SetSystemPowerState( _
        ByVal psState As String, _
        ByVal StateFlags As Integer, _
        ByVal Options As Integer) As Integer
    End Function

    Const POWER_STATE_ON As Integer = &H10000
    Const POWER_STATE_OFF As Integer = &H20000
    Const POWER_STATE_SUSPEND As Integer = &H200000
    Const POWER_STATE_RESET As Integer = &H800000

    Public stopThreadsNow As Boolean = False
    Public goToConfirmar As Boolean = False
    Dim objService As New xoService.xotest

    Dim objCore As New ceClient

    Dim i As Long
    Dim Tiempo As String
    Dim dtImportLog As New DataTable
    Dim export As New ceExportLogic
    Dim contadorThreads, contadorThreadsCtrl As Integer
    Dim ruta As New RutaCO
    Dim oRuta As New RutaBL
    Dim isError As Boolean
    Dim eventoTexto As String
    Public returnToMain As Boolean = False
    Public conexion_exitosa As Boolean = False

    Private Sub frmImportar_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            DateTimePicker1.Value = Date.Today
            DateTimePicker2.Value = Date.Today
            inicializarLstImport()
            inicializarLstExport()
            txtRuta.Text = id_glo_codRuta
            txtServidor.Text = id_glo_server
            'MessageBox.Show(id_glo_sociedad)
            ComboBox1.Text = id_glo_sociedad
            ruta = oRuta.getActiva()

            objService.Timeout = -1

            '<Driver>
            '--- Aceptar los canales de seguridad SSL
            'System.Net.ServicePointManager.CertificatePolicy = New TrustAllCertificatePolicy()
            'Dim objRuta As New RutaBL
            'objRuta.inicializarXoMobile()
            'tipoRuta = "17" 'Autoventa
            '''*--- <DRIVER> ---------------------
            '</Driver>



            If Not ruta.exportar Then
                '--- No se han sufragado los requisitos para exportar
                cmdExportar.Enabled = False
                panExportar.Visible = True
                lbl_msExportar.Text = "Esta ruta esta pendiente de liquidacion."
            End If

            '--- Habilitar controles de confirmacion
            Dim oBitacora As New BitacoraBL
            If (oBitacora.isOperacionRealizada_xo(48) Or tipoRuta <> "16") Then
                If oBitacora.isOperacionRealizada_xo(10) Then
                    If oBitacora.isOperacionRealizada_xo(37) Then
                        If oBitacora.isOperacionRealizada_xo(36) Then
                        Else
                            lbl_msExportar.Text = "No ha liquidado en bodega de envase"
                            cmdExportar.Enabled = False
                            panExportar.Visible = True
                        End If
                    Else
                        lbl_msExportar.Text = "No ha liquidado en bodega de producto"
                        cmdExportar.Enabled = False
                        panExportar.Visible = True
                    End If
                Else
                    lbl_msExportar.Text = "No ha respondido las preguntas finales"
                    cmdExportar.Enabled = False
                    panExportar.Visible = True
                End If
            Else
                lbl_msExportar.Text = "Falta la declaración de despachos"
                cmdExportar.Enabled = False
                panExportar.Visible = True
            End If




        Catch ex As Exception
            '--- Si no hay ruta activa entonces hay que importar
            ruta.importar = True
            cmdExportar.Enabled = False
            panExportar.Visible = True
        End Try
        Cursor.Current = Cursors.Default
    End Sub

#Region "Exportar"
    Private Sub cmdExportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Dim dsxo As New DataSet
        Cursor.Current = Cursors.WaitCursor
        If (id_glo_sociedad = 4000 Or id_glo_sociedad = 7000) Then
            '--- Inicializar objetos
            lstExport.Items.Clear()
            dtImportLog.Rows.Clear()
            cmdExportar.Enabled = False

            '--- Inicializar URL del objeto de conexion      
            If Not objCore.construyeURL(chkEstoyFuera.Checked, objService, txtServidor.Text, txtRuta.Text) Then Return

            '--- Bitacora: Inicio Exportacion de datos
            Dim objBitacora As New BitacoraBL
            objBitacora.registrarOperacion(43, id_glo_cliente)

            '--- Agregar las tablas al Data Set que se va a enviar
            i = 0
            Timer2.Enabled = True
            lblEvento.Text = ""
            Dim tImportar As New ThreadStart(AddressOf Me.txoAgregarTablas)
            Dim mt As New Thread(tImportar)
            mt.Start()
        Else
            MessageBox.Show("SELECCIONAR LA SOCIEDAD QUE DESEA EXPORTAR, 4000 DLSA  |  7000 LEVUNI")
        End If
        
    End Sub

    Private Sub txoAgregarTablas()
        Dim dsxo As New DataSet
        Dim xoExportLog As New DataSet
        Dim xoRespuesta As String = ""


        Cursor.Current = Cursors.WaitCursor

        '--- Enviar la version del sistema.
        Try
            eventoTexto = "Agregando version del sistema"
            Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
            dsxo.Tables.Add("VERSION")
            dsxo.Tables("VERSION").Columns.Add("xo", String.Empty.GetType())
            dsxo.Tables("VERSION").Rows.Add(New String() {id_glo_sistema + id_glo_version})
        Catch ex As Exception

        End Try

        '--- Exporta clientes nuevos
        Try
            Dim dtClientes As New DataTable
            Dim oCliente As New ClienteBL
            Dim cliente As New ClienteCO
            Dim sapID As String = ""

            If Not oCliente.getNuevos(dtClientes) Is Nothing Then
                eventoTexto = "Creando clientes nuevos"
                Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
                For i As Integer = 0 To dtClientes.Rows.Count - 1
                    cliente = oCliente.getDetalleDelCliente(dtClientes.Rows(i).Item("id_cliente"))
                    If oCliente.crearOnline(cliente.propietario, cliente.negocio, cliente.nit, cliente.departamento, cliente.deptoMuni, cliente.direccion, cliente.numeroDi, False, cliente.giro, sapID, cliente.latitud, cliente.longitud, False) Then

                        '--- Actualizar codigo de cliente en los documentos emitidos
                        Dim oDocumento As New DocumentoBL
                        oDocumento.cambiarResponsable(cliente.codigo, sapID)
                    End If
                Next
            End If
        Catch ex As Exception

        End Try



        eventoTexto = "Agregando documentos emitidos"
        Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
        If Not export.xo_dataSet_AgregarMaestro(dsxo, "MAESTRO", dtImportLog) Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If


        eventoTexto = "Agregando detalle de Despacho"
        Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
        If Not export.xo_dataSet_AgregarDetalleDespacho(dsxo, "CDESPACHO", dtImportLog) Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        eventoTexto = "Agregando Efectividad de Despacho"
        Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
        If Not export.xo_dataSet_AgregarEfectividadDespacho(dsxo, "CEFECTIVIDAD_DESPACHO", dtImportLog) Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        eventoTexto = "Agregando Efectividad de Cobro"
        Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
        If Not export.xo_dataSet_AgregarEfectividadCobro(dsxo, "CEFECTIVIDAD_COBRO", dtImportLog) Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        eventoTexto = "Agregando Encuestas"
        Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
        If Not export.xo_dataSet_AgregarEncuestaRespuestas(dsxo, "CENCUESTA_DETALLE", dtImportLog) Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        eventoTexto = "Actualizando clientes"
        Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
        If Not export.xo_dataSet_actualizacliente(dsxo, "RCLIENTE", dtImportLog) Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        'eventoTexto = "Actualizando NombresFEL"
        'Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
        'If Not export.xo_dataSet_actualizaNombre(dsxo, "RNOMBRES", dtImportLog) Then
        'stopThreadsNow = True
        'isError = True
        'Cursor.Current = Cursors.Default
        'Return
        'End If

        eventoTexto = "Actualizando Libro de Ventas"
        Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
        If Not export.xo_dataSet_libroventas(dsxo, "RLVENTAS", dtImportLog) Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If


        eventoTexto = "Agregando Bitacora."
        Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
        If Not export.xo_dataSet_AgregarBitacora(dsxo, "BITACORA", dtImportLog) Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        eventoTexto = "Agregando Efectividad."
        Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
        If Not export.xo_dataSet_AgregarEfectividad(dsxo, "EFECTIVIDAD", dtImportLog) Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        eventoTexto = "Agregando inventario de clientes."
        Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
        If Not export.xo_dataSet_AgregarInventarioFisico(dsxo, "INVENTARIO_FISICO", dtImportLog) Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        eventoTexto = "Agregando correlativos."
        Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
        If Not export.xo_dataSet_AgregarCorrelativo(dsxo, "RCCORRELATIVO", dtImportLog) Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        eventoTexto = "Agregando correlativos contingencia"
        Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
        If Not export.xo_dataSet_AgregarCorrelativoContingencia(dsxo, "RCONTINGENCIA", dtImportLog) Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        eventoTexto = "Agregando preguntas iniciales y finales."
        Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
        If Not export.xo_dataSet_AgregarPreguntasIF(dsxo, "PREGNTAS_IF", dtImportLog) Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        eventoTexto = "Agregando liquidacion."
        Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
        If Not export.xo_dataSet_AgregarIntegracion(dsxo, "L_INTEGRACION", dtImportLog) Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        eventoTexto = "Agregando movimiento de liquidacion."
        Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
        If Not export.xo_dataSet_AgregarMovimiento(dsxo, "L_MOVIMIENTO", dtImportLog) Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        eventoTexto = "Agregando movimiento diferencia en corte."
        Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
        If Not export.xo_dataSet_AgregarDiferencia(dsxo, "L_DIFERENCIA", dtImportLog) Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If


        '--- Recupera cuenta por cobrar para desmarcar compromisos de Pago
        eventoTexto = "Agregando compromisos de pago."
        Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
        If Not export.xo_dataSet_AgregarCompromiso(dsxo, "COMPROMISOP", dtImportLog) Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        '--- Enviar archivos texto
        eventoTexto = "Exportando archivos de texto."
        Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
        'enviar el dtImportLog

        export.enviarArchivoTexto(dtImportLog)
        '--- Enviar informacion
        Try
            eventoTexto = "Exportando Informacion a SAP."
            Me.lblEvento.Invoke(New EventHandler(AddressOf logEventos))
            xoExportLog = objService.xoExportar(dsxo, xoRespuesta, id_glo_codRuta, id_glo_sociedad)
            conexion_exitosa = True
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", xoRespuesta, "", "Exportar", ex.Message, "xoMobile"})
        End Try

        If xoExportLog.Tables.Contains("TERROR") Then
            Dim tab As New DataTable
            For i As Integer = 0 To xoExportLog.Tables("TERROR").Rows.Count() - 1
                With xoExportLog.Tables("TERROR").Rows(i)
                    dtImportLog.Rows.Add(New String() {.Item("xoLogType"), .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                End With
            Next
        End If

        '--- Bitacora: Finaliza Exportacion de datos
        Dim objBitacora As New BitacoraBL
        objBitacora.registrarOperacion(44, id_glo_cliente)
        Cursor.Current = Cursors.Default
        stopThreadsNow = True
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        i = i + 1
        Tiempo = Format(Int(i / 3600) Mod 24, "00") & ":" & _
                    Format(Int(i / 60) Mod 60, "00") & ":" & _
                    Format(i Mod 60, "00")
        lblRelojExp.Text = Tiempo
        If stopThreadsNow Then
            WorkerUpdateExport(sender, e)
            cmdExportar.Enabled = True
            Timer2.Enabled = False
            Cursor.Current = Cursors.Default
            stopThreadsNow = False
            If isError Then
                MsgBox("Ocurrio un error al tratar de exportar los datos.")
            Else
                MsgBox("Proceso de Exportacion finalizado.")
                ruta.importar = True

                'Procedimiento para reiniciar el equipo despues de realizar el export.
                Try
                    SetSystemPowerState(Nothing, POWER_STATE_RESET, 0)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try

            End If
        End If
    End Sub

    Public Sub logEventos()
        lblEvento.Text = eventoTexto
    End Sub

    Public Sub WorkerUpdateExport(ByVal sender As Object, ByVal e As EventArgs)
        If dtImportLog.Rows.Count > 0 Then

            '--- Agregar filas a la lista
            Me.lstExport.Items.Clear()
            For i As Integer = 0 To dtImportLog.Rows.Count - 1
                Dim drow As DataRow = dtImportLog.Rows(i)

                '--- Agregar Items a la fila
                Dim lvi As New ListViewItem(drow("xoLogType").ToString())
                lvi.SubItems.Add(drow("xoFriendlyFunction").ToString())
                lvi.SubItems.Add(drow("xoErrorStatament").ToString())
                lvi.SubItems.Add(drow("xoSapDestination").ToString())
                lvi.SubItems.Add(drow("xoSapFunction").ToString())
                lvi.SubItems.Add(drow("xoWebFunction").ToString())
                lstExport.Items.Add(lvi)

                '--- Agregar imagen 
                Select Case drow("xoLogType").ToString()
                    Case "Exito"
                        Me.lstExport.Items(i).ImageIndex = 0
                    Case "Advertencia"
                        Me.lstExport.Items(i).ImageIndex = 1
                    Case "Error"
                        Me.lstExport.Items(i).ImageIndex = 2
                End Select
            Next
            Me.lstExport.Items.Item(0).Selected() = True
        End If
    End Sub
#End Region

#Region "Importar"
    Private Sub cmdImportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImportar.Click

        '--- Inicializar URL del objeto de conexion      
        If Not objCore.construyeURL(chkEstoyFuera.Checked, objService, txtServidor.Text, txtRuta.Text) Then Return

        If ruta.importar = False Then
            Dim msg = "Hay informacion pendiente de Exportar. Desea volver a Importar datos?"
            Dim style = MsgBoxStyle.YesNo
            Dim response = MsgBox(msg, style, "Importar")
            If response = MsgBoxResult.No Then
                Return
            End If
        End If

        '--- Habilitar controles
        panFechaCarga.Visible = True
        DateTimePicker1.Focus()
        lstImport.Items.Clear()
        dtImportLog.Rows.Clear()
    End Sub

    Private Sub DateTimePicker1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If (id_glo_sociedad = 4000 Or id_glo_sociedad = 7000) Then
            Select Case e.KeyChar()
                Case ChrW(13)
                    If txtRuta.Text <> "" Then
                        id_glo_codRuta = txtRuta.Text
                        If txtServidor.Text <> "" Then
                            Try
                                pan_msImportar.Visible = False
                                id_glo_server = txtServidor.Text
                                id_glo_sociedad = ComboBox1.Text
                                '--- Obtener el valor de la fecha de carga
                                co_glo_fechaCarga = DateTimePicker1.Value
                                panFechaCarga.Visible = False
                                lstImport.Focus()

                                '--- Bloquear importacion datos 
                                i = 0
                                ruta.exportar = False
                                ruta.importar = False
                                cmdExportar.Enabled = False
                                cmdImportar.Enabled = False
                                xoImportar(sender, e)
                                panExportar.Visible = True
                                isError = False

                                '--- Bitacora: inicio Importacion de datos
                                Dim objBitacora As New BitacoraBL
                                objBitacora.registrarOperacion(41, id_glo_cliente)

                            Catch ex As Exception
                                panFechaCarga.Visible = False
                                MsgBox(ex.Message)
                                cmdImportar.Enabled = True
                            End Try
                        Else
                            panFechaCarga.Visible = False
                            pan_msImportar.Visible = True
                            lbl_msImportar.Text = "Escriba la direccion del servidor en la pestaña PARAMETROS"
                            cmdImportar.Enabled = True
                            pan_msImportar.Focus()

                        End If
                    Else
                        panFechaCarga.Visible = False
                        pan_msImportar.Visible = True
                        lbl_msImportar.Text = "Escriba el codigo de ruta en la pestaña PARAMETROS"
                        cmdImportar.Enabled = True
                        pan_msImportar.Focus()
                    End If
                Case ChrW(Keys.Escape)
                    panFechaCarga.Visible = False
            End Select
        Else
            MessageBox.Show("SELECCIONAR LA SOCIEDAD QUE DESEA EXPORTAR, 4000 DLSA  |  7000 LEVUNI")
        End If
        
    End Sub

    Private Sub xoImportar(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Cursor.Current = Cursors.WaitCursor
        Dim objruta As New RutaBL
        Try
            cmdImportar.Enabled = False

            '--- Importar la informacion de la ruta default
            Timer1.Enabled = True
            ProgressBar.Value = 0
            lstImport.Items.Clear()
            dtImportLog.Rows.Clear()
            objruta.inicializaImportacion()
            If Not Me.getRuta() Then
                Cursor.Current = Cursors.Default
                WorkerUpdateImport(sender, e)
                cmdImportar.Enabled = True
                TabControl1.Enabled = True
                ruta = oRuta.getActiva()
                Return
            End If
            id_glo_ruta = objruta.getActiva().id_ruta
            WorkerUpdateImport(sender, e)
            contadorThreads = 18
            ProgressBar.Maximum = contadorThreads
            contadorThreadsCtrl = contadorThreads

            'Iniciar thread de importacion
            Dim tImportar As New ThreadStart(AddressOf Me.txoImportar)
            Dim mt As New Thread(tImportar)
            mt.Start()

        Catch ex As Exception
            MsgBox(ex.Message)
            cmdImportar.Enabled = True
            Cursor.Current = Cursors.Default
        End Try
        Cursor.Current = Cursors.Default
    End Sub

    Public Sub txoImportar()

        Cursor.Current = Cursors.WaitCursor
        If Not Me.getCorrelativoSAP() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        If Not Me.getWorkflowSAP() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        'WorkFlow Giro de Negocio
        If Not Me.getWorkflowSAPGiro() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        'Presupuesto del cliente por marca
        If Not Me.getPresupuestoCliente() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If
        If tipoRuta <> "16" Then
            'Datos maestros Deptos y Municipios
            If Not Me.getDeptos() Then
                stopThreadsNow = True
                isError = True
                Cursor.Current = Cursors.Default
                Return
            End If
        End If

        If Not Me.getRazonesNV() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        If Not Me.getPreguntasInicialesFinales() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        If Not Me.getCxc() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        If Not Me.setGlobals() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If


        If Not Me.getBoom() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        If Not Me.getProducto() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If


        If Not Me.getClientes() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        If Not Me.getLockMaterial() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If


        If Not Me.getViasPago() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        If Not Me.getAsignaViasPago() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If


        If Not Me.getContingencia() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If



        If Not Me.getUsuarios() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        If Not Me.getCredenciales(dtImportLog) Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        If Not Me.getCargaCamion() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        If Not Me.getCluster() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        If Not Me.getTipos() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        If Not Me.getEncuesta() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If
        If Not Me.getPreciosDescuentos() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        If Not Me.getRango() Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If
        If tipoRuta = "16" Then
            If Not Me.getDespachos() Then
                stopThreadsNow = True
                isError = True
                Cursor.Current = Cursors.Default
                Return
            End If
        End If

        Dim oceImport As New ceImport
        If Not oceImport.importacionComplemento(dtImportLog) Then
            stopThreadsNow = True
            isError = True
            Cursor.Current = Cursors.Default
            Return
        End If

        stopThreadsNow = True
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        i = i + 1
        Tiempo = Format(Int(i / 3600) Mod 24, "00") & ":" & _
                    Format(Int(i / 60) Mod 60, "00") & ":" & _
                    Format(i Mod 60, "00")
        lblReloj.Text = Tiempo


        Try
            If Not contadorThreads = contadorThreadsCtrl Then
                contadorThreadsCtrl = contadorThreads
                Me.lstImport.Invoke(New EventHandler(AddressOf WorkerUpdateImport))
                ProgressBar.Value += 1
            End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message())
        End Try
        
        If stopThreadsNow Then
            ProgressBar.Value = ProgressBar.Maximum
            Timer1.Enabled = False
            Me.lstImport.Invoke(New EventHandler(AddressOf WorkerUpdateImport))
            Cursor.Current = Cursors.Default
            stopThreadsNow = False
            If isError Then
                MsgBox("ERROR - Revisar el listado de operaciones")
            Else
                MsgBox("Proceso de importacion finalizado.")
                '--- Bitacora: Finaliza Importacion de datos
                Dim objBitacora As New BitacoraBL
                objBitacora.registrarOperacion(42, id_glo_cliente)
            End If
            cmdImportar.Enabled = True
            TabControl1.Enabled = True
            '--- Bloquear importacion datos 
            '---(Activa el mensaje de advertencia)
            ruta.importar = False
            'co_glo_closeAndGo = True
            returnToMain = True
            Cursor.Current = Cursors.Default
            'isDataChanged = True
            Try
                ' oRuta.actualizar(ruta)
            Catch ex As Exception
                MsgBox("Imposible actualizar datos de ruta.")
            End Try
        End If
    End Sub

    Public Sub WorkerUpdateImport(ByVal sender As Object, ByVal e As EventArgs)
        If dtImportLog.Rows.Count > 0 Then

            '--- Agregar filas a la lista
            Me.lstImport.Items.Clear()
            For i As Integer = 0 To dtImportLog.Rows.Count - 1
                Dim drow As DataRow = dtImportLog.Rows(i)

                '--- Agregar Items a la fila
                Dim lvi As New ListViewItem(drow("xoLogType").ToString())
                lvi.SubItems.Add(drow("xoFriendlyFunction").ToString())
                lvi.SubItems.Add(drow("xoErrorStatament").ToString())
                lvi.SubItems.Add(drow("xoSapDestination").ToString())
                lvi.SubItems.Add(drow("xoSapFunction").ToString())
                lvi.SubItems.Add(drow("xoWebFunction").ToString())
                lstImport.Items.Add(lvi)

                '--- Agregar imagen 
                Select Case drow("xoLogType").ToString()
                    Case "Exito"
                        Me.lstImport.Items(i).ImageIndex = 0
                    Case "Advertencia"
                        Me.lstImport.Items(i).ImageIndex = 1
                    Case "Error"
                        Me.lstImport.Items(i).ImageIndex = 2
                End Select
            Next
            Me.lstImport.Items.Item(0).Selected() = True
        End If

    End Sub

    Private Function getCluster() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport
            'dsxo = objService.xoGetClusterSegmentacion(id_glo_sociedad, id_glo_codRuta)
            dsxo = objService.xoGetClusterSegmentacion(id_glo_sociedad, id_glo_codRuta, co_glo_fechaCarga)
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                End With
                Return False
            Else
                contadorThreads -= 1
                Return oceImport.importIntoRCLUSTER_SEG(dsxo.Tables("RCLUSTER_SEG"), dtImportLog)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Cluster", "", "getCluster", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Public Function getCredenciales(ByRef dtImportLog As DataTable) As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport
            dsxo = objService.xoGetFel2(id_glo_sociedad)
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                End With
                Return False
            Else
                contadorThreads -= 1
                Return oceImport.importIntoRCREDENCIALES(dsxo.Tables("RCREDENCIALES"), dtImportLog)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Importar", "", "Comando Importar", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getRuta() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport

            '--- Obtener Ruta
            dsxo = objService.xoGetRuta(id_glo_codRuta, id_glo_sociedad)
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                    Return False
                End With
            Else
                Return oceImport.importIntoRRUTA(dsxo.Tables("RRUTA"), dtImportLog)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Ruta", "", "getRuta", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getCorrelativoSAP() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport
            dsxo = objService.xoGetResolucionesSapHH(id_glo_codRuta, id_glo_sociedad)
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                    Return False
                End With
            Else
                Return oceImport.importIntoCorrelativoSAP(dsxo.Tables("RESOLUCION_SAT"), dtImportLog)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Correlativos SAT", "", "getCorrelativoSAP", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getWorkflowSAP() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport

            dsxo = objService.xoGetWorkflowSAPHH(id_glo_codRuta, id_glo_sociedad)
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                    Return False
                End With
            Else
                Return oceImport.importIntoWorkflowSAP(dsxo.Tables("TTIPO"), dtImportLog)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Workflow", "", "getWorkflowSAP", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getWorkflowSAPGiro() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport

            dsxo = objService.xoGetWorkflowGiro(id_glo_sociedad)
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                    Return False
                End With
            Else
                Return oceImport.importIntoWorkflowSAPGiro(dsxo.Tables("RWORKFLOW_GIRO"), dtImportLog)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Workflow", "", "getWorkflowSAPGiro", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getPresupuestoCliente() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport

            dsxo = objService.xoGetPresupuestoCliente(id_glo_sociedad, id_glo_codRuta, co_glo_fechaCarga)

            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                    Return False
                End With
            Else
                Return oceImport.importIntoPresupuestoCliente(dsxo.Tables("RPRESUPUESTO_CLIENTE"), dtImportLog)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Workflow", "", "getPresupuestoCliente", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getDeptos() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport

            dsxo = objService.xoGetDeptos(7000)
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                    Return False
                End With
            Else
                Return oceImport.importIntoDeptos(dsxo.Tables("RDEPTO_MUNI"), dtImportLog)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Deptos", "", "getDeptos", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function setGlobals() As Boolean
        Dim objUtil As New UtilitarioBL
        Dim dtGlobales, dtModGlobal As New DataTable
        Dim delcmd As SqlCeCommand
        Dim oceImport As New ceImport
        Dim oceClient As New ceClient
        Dim dsModGlobal As New DataSet
        Dim renovar As Boolean = False

        Try


            '--- Obtener las variables globales actuales
            dtGlobales = objUtil.obtenerModGlobalPorVariable("id_glo_codRuta")
            If dtGlobales.Rows.Count = 0 Then
                renovar = True
            End If

            If dtGlobales.Rows.Count > 0 Then
                If dtGlobales.Rows(0).Item("valor") <> id_glo_codRuta Then
                    renovar = True
                End If
            End If

            If renovar Then
                Dim dsGlobales As New DataSet
                oceClient.dbConnect()
                oceClient.Conexion.Open()
                Dim ceDataAdapter = New SqlCeDataAdapter("select * from MODGLOBAL", oceClient.Conexion)
                Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
                ceDataAdapter.Fill(dsModGlobal, "MODGLOBAL")

                '--- Eliminar las variables globales
                delcmd = New SqlCeCommand("DELETE FROM MODGLOBAL", oceClient.Conexion)
                ceDataAdapter.DeleteCommand = delcmd
                ceDataAdapter.DeleteCommand.ExecuteNonQuery()

                '--- Renovar valores de variables globales
                dtModGlobal.TableName = "MODGLOBAL"
                dtModGlobal.Columns.Add("variable", String.Empty.GetType())
                dtModGlobal.Columns.Add("valor", String.Empty.GetType())
                dtModGlobal.Rows.Add(New String() {"xo_server", id_glo_server})
                dtModGlobal.Rows.Add(New String() {"xo_sociedad", id_glo_sociedad})
                dtModGlobal.Rows.Add(New String() {"id_glo_codRuta", id_glo_codRuta})
                dtModGlobal.Rows.Add(New String() {"co_glo_searchCliente", "descripcion"})
                dtModGlobal.Rows.Add(New String() {"co_glo_searchProducto", "descripcion"})
                dtModGlobal.Rows.Add(New String() {"xo_validaInventario", "true"})
                id_glo_centro = objService.xoGetCentro(id_glo_codRuta, id_glo_sociedad, co_glo_fechaCarga)
                If id_glo_centro.StartsWith("Centro") Then
                    dtImportLog.Rows.Add(New String() {"Error", "Globales", "", "setGlobals()", "Centro de distribucion no encontrado, revisar datos de carga de producto", "SAP_PRODUCTIVO"})
                    Return False
                End If
                dtModGlobal.Rows.Add(New String() {"xo_centro", id_glo_centro})
                dtImportLog.Rows.Add(New String() {"Exito", "Globales", "", "getGlobals", dtModGlobal.Rows.Count().ToString() + " Registros importados con exito", "xoMobile"})
                For i As Integer = 0 To dtModGlobal.Rows.Count - 1
                    Dim ceRow As DataRow = dsModGlobal.Tables("MODGLOBAL").NewRow()
                    ceRow("variable") = dtModGlobal.Rows(i).Item("variable")
                    ceRow("valor") = dtModGlobal.Rows(i).Item("valor")
                    dsModGlobal.Tables("MODGLOBAL").Rows.Add(ceRow)
                Next
                ceDataAdapter.Update(dsModGlobal, "MODGLOBAL")
                oceClient.Conexion.Close()
                contadorThreads -= 1
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Globales", "", "setGlobals()", ex.Message, "xoMobile"})
            Return False
        End Try
        Return True
    End Function

    Private Function getProducto() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport
            dsxo = objService.xoGetMateriales(id_glo_sociedad)
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                End With
                Return False
            Else
                contadorThreads -= 1
                Return oceImport.importIntoRPRODUCTO(dsxo.Tables("RPRODUCTO"), dtImportLog)
            End If

        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Importar", "", "Comando Importar", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getContingencia() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport
            dsxo = objService.xoGetContingenciaImp(id_glo_codRuta, id_glo_sociedad)
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                End With
                Return False
            Else
                contadorThreads -= 1
                Return oceImport.importIntoRContingencia(dsxo.Tables("RCONTINGENCIA"), dtImportLog)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Importar", "", "Comando Importar", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getUsuarios() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport
            dsxo = objService.xoGetUsuarios(id_glo_sociedad)
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                End With
                Return False
            Else
                contadorThreads -= 1
                Return oceImport.importIntoRUSUARIO(dsxo.Tables("RUSUARIO"), dtImportLog)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Importar", "", "Comando Importar", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getClientes() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport

            dsxo = objService.xoGetClientes(id_glo_codRuta, id_glo_sociedad, co_glo_fechaCarga)
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                End With
                Return False
            Else
                contadorThreads -= 1
                Return oceImport.importIntoRCLIENTE(dsxo.Tables("RCLIENTE"), dtImportLog)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Clientes", "", "getClientes", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getLockMaterial() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport

            dsxo = objService.xoGetLockMateriales(id_glo_sociedad)
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                End With
                Return False
            Else
                contadorThreads -= 1
                Return oceImport.importIntoRLockMaterial(dsxo.Tables("RLOCK_MATERIAL"), dtImportLog)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "getLockMaterial", "", "getLockMaterial", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getPreciosDescuentos() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport
            'dsxo = objService.xoGetPreciosDescuentos(id_glo_codRuta, id_glo_sociedad)
            dsxo = objService.xoGetPrecios(id_glo_codRuta, id_glo_sociedad, "x", co_glo_fechaCarga)
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                End With
                Return False
            Else
                contadorThreads -= 1
                Return oceImport.importIntoZCAMPOS2(dsxo.Tables("ZCAMPOS2"), dtImportLog)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Precios y descuentos", "", "getPreciosDescuentos", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getEncuesta() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport
            dsxo = objService.xoGetEncuesta(id_glo_sociedad)
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                    Return False
                End With
            Else
                If oceImport.importIntoRENCUESTA(dsxo.Tables("RENCUESTA"), dtImportLog) Then
                    If oceImport.importIntorEncuesta_Temas(dsxo.Tables("RENCUESTA_TEMAS"), dtImportLog) Then
                        If oceImport.importIntorEncuesta_preguntas(dsxo.Tables("RENCUESTA_PREGUNTAS"), dtImportLog) Then
                            If oceImport.importIntoRENCUESTA_ALTERNATIVAS(dsxo.Tables("RENCUESTA_ALTERNATIVAS"), dtImportLog) Then
                                contadorThreads -= 1
                                Return True
                            End If
                        End If
                    End If
                End If
                Return False
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Encuestas", "", "getEncuesta", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getCxc() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport
            'dsxo = objService.xoGetCuentaPorCobrar(id_glo_codRuta, id_glo_sociedad)
            dsxo = objService.xoGetCuentaPorCobrar2(id_glo_codRuta, id_glo_sociedad, co_glo_fechaCarga)
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                End With
                Return False
            Else
                If oceImport.importIntoRCXC(dsxo.Tables("RCXC"), dtImportLog) Then
                    If oceImport.importIntoRCXC_DETALLE(dsxo.Tables("RCXC_DETALLE"), dtImportLog) Then
                        contadorThreads -= 1
                        Return True
                    End If
                End If
                Return False
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Cuenta por cobrar", "", "getCxc", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getCargaCamion() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport
            dsxo = objService.xoGetCarga2(id_glo_codRuta, id_glo_sociedad, co_glo_fechaCarga)
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                End With
                Return False
            Else
                contadorThreads -= 1
                Return oceImport.importIntoRCARGA(dsxo.Tables("RCARGA"), dtImportLog)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Carga de camion", "", "getCargaCamion", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getViasPago() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport
            dsxo = objService.xoGetFormasPago()
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                End With
                Return False
            Else
                contadorThreads -= 1
                Return oceImport.importIntoRVIAS_PAGO(dsxo.Tables("RVIAS_PAGO"), dtImportLog)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Catalogo de vias de pago", "", "getViasPago", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getAsignaViasPago() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport
            'dsxo = objService.xoGetAsignaViasPago(id_glo_codRuta, id_glo_sociedad)
            dsxo = objService.xoGetAsignaViasPago_2(id_glo_codRuta, id_glo_sociedad, co_glo_fechaCarga)
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                End With
                Return False
            Else
                contadorThreads -= 1
                Return oceImport.importIntoRASIGNA_VIAS_PAGO(dsxo.Tables("RASIGNA_VIAS_PAGO"), dtImportLog, False)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Asignacion de vias de pago", "", "getAsignaViasPago", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getTipos() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport
            dsxo = objService.xoGetTipos(id_glo_sociedad)
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                End With
                Return False
            Else
                contadorThreads -= 1
                Return oceImport.importIntoTTIPO(dsxo.Tables("TTIPO"), dtImportLog)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Importar tabla de tipos", "", "getTipos", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getBoom() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport
            dsxo = objService.xoGetBoom()
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                End With
                Return False
            Else
                contadorThreads -= 1
                Return oceImport.importIntoBOM(dsxo.Tables("RBOOM"), dtImportLog)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Importar bom de materiales", "", "getBoom", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getPreguntasInicialesFinales() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport
            dsxo = objService.xoGetPreguntasInicialesFinales(id_glo_ruta, id_glo_sociedad)
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                End With
                Return False
            Else
                contadorThreads -= 1
                Return oceImport.importIntoPreguntasInicialesFinales(dsxo.Tables("RSALIDA_INGRESO"), dtImportLog)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Preguntas Iniciales y Finales", "", "getPreguntasInicialesFinales", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getRazonesNV() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport
            dsxo = objService.xoGetRazonesNV()
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                End With
                Return False
            Else
                contadorThreads -= 1
                Return oceImport.importIntoRazonesNV(dsxo.Tables("TTIPO"), dtImportLog)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Razones no visita", "", "getRazonesNV", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getRango() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport
            dsxo = objService.xoGetRango
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                End With
                Return False
            Else
                contadorThreads -= 1
                Return oceImport.importIntoRANGO(dsxo.Tables("RRANGO"), dtImportLog)
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Precios y descuentos", "", "getPreciosDescuentos", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function getDespachos() As Boolean
        Dim dsxo As New DataSet
        Try
            Dim oceImport As New ceImport
            dsxo = objService.xoGetDespachos_2(id_glo_codRuta, id_glo_sociedad, co_glo_fechaCarga)
            If dsxo.Tables.Contains("TERROR") Then
                With dsxo.Tables("TERROR").Rows(0)
                    dtImportLog.Rows.Add(New String() {"Error", .Item("xoFriendlyFunction"), .Item("xoSapFunction"), .Item("xoWebFunction"), .Item("xoErrorStatament"), .Item("xoSapDestination")})
                End With
                Return False
            Else
                If oceImport.importIntoDESPACHO(dsxo.Tables("RDESPACHO"), dtImportLog) Then
                    If oceImport.importIntoDESPACHO_DETALLE(dsxo.Tables("RDESPACHO_DETALLE"), dtImportLog) Then
                        contadorThreads -= 1

                        '--- Proceso complementario
                        oceImport.completarDespachos()
                        Return True
                    End If
                Else
                    contadorThreads -= 1
                    Return False
                End If
                Return False
            End If
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Despachos programados", "", "getDespacho", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function


#End Region

#Region "UI Metodos"
    Private Sub lblRuta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblRuta.Click
        txtRuta.Enabled = True
        txtRuta.Focus()
        txtRuta.BackColor = Color.White
    End Sub
    Private Sub lblServidor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblServidor.Click
        txtServidor.Enabled = True
        txtServidor.Focus()
        txtServidor.BackColor = Color.White
    End Sub
    Private Sub panExportar_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panExportar.Paint
        Dim oUtil As New UtilitarioBL
        oUtil.paintPannel(e, panExportar)
    End Sub
    Private Sub inicializarLstImport()
        dtImportLog.TableName = "IMPORT_LOG"
        dtImportLog.Columns.Add("xoLogType", String.Empty.GetType())
        dtImportLog.Columns.Add("xoFriendlyFunction", String.Empty.GetType())
        dtImportLog.Columns.Add("xoSapFunction", String.Empty.GetType())
        dtImportLog.Columns.Add("xoWebFunction", String.Empty.GetType())
        dtImportLog.Columns.Add("xoErrorStatament", String.Empty.GetType())
        dtImportLog.Columns.Add("xoSapDestination", String.Empty.GetType())

        Dim xoLogType = New ColumnHeader()
        Dim xoFriendlyFunction = New ColumnHeader()
        Dim xoSapFunction = New ColumnHeader()
        Dim xoWebFunction = New ColumnHeader()
        Dim xoErrorStatament = New ColumnHeader()
        Dim xoSapDestination = New ColumnHeader()

        xoLogType.Text = ""
        xoLogType.Width = 75
        xoFriendlyFunction.Text = "Origen"
        xoFriendlyFunction.Width = 125
        xoSapFunction.Text = "SAP"
        xoSapFunction.Width = 75
        xoWebFunction.Text = "Web"
        xoWebFunction.Width = 100
        xoErrorStatament.Text = "Detalle"
        xoErrorStatament.Width = 300
        xoSapDestination.Text = "En"
        xoSapDestination.Width = 75

        lstImport.Columns.Add(xoLogType)
        lstImport.Columns.Add(xoFriendlyFunction)
        lstImport.Columns.Add(xoErrorStatament)
        lstImport.Columns.Add(xoSapDestination)
        lstImport.Columns.Add(xoSapFunction)
        lstImport.Columns.Add(xoWebFunction)
    End Sub
    Private Sub inicializarLstExport()
        Dim xoLogType = New ColumnHeader()
        Dim xoFriendlyFunction = New ColumnHeader()
        Dim xoSapFunction = New ColumnHeader()
        Dim xoWebFunction = New ColumnHeader()
        Dim xoErrorStatament = New ColumnHeader()
        Dim xoSapDestination = New ColumnHeader()

        xoLogType.Text = ""
        xoLogType.Width = 50
        xoFriendlyFunction.Text = "Origen"
        xoFriendlyFunction.Width = 75
        xoSapFunction.Text = "SAP"
        xoSapFunction.Width = 75
        xoWebFunction.Text = "Web"
        xoWebFunction.Width = 100
        xoErrorStatament.Text = "Detalle"
        xoErrorStatament.Width = 300
        xoSapDestination.Text = "En"
        xoSapDestination.Width = 75

        lstExport.Columns.Add(xoLogType)
        lstExport.Columns.Add(xoFriendlyFunction)
        lstExport.Columns.Add(xoErrorStatament)
        lstExport.Columns.Add(xoSapDestination)
        lstExport.Columns.Add(xoSapFunction)
        lstExport.Columns.Add(xoWebFunction)
    End Sub


#Region "Comandos"
    Private Sub cmdTestc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTestc.Click
        Cursor.Current = Cursors.WaitCursor
        Dim textoDevuelto As String = ""

        id_glo_server = txtServidor.Text
        id_glo_codRuta = txtRuta.Text
        id_glo_estoy_fuera = chkEstoyFuera.Checked
        id_glo_sociedad = ComboBox1.Text

        '--- Construye URL
        If Not objCore.construyeURL(chkEstoyFuera.Checked, objService, txtServidor.Text, txtRuta.Text) Then Return

        '--- Comprobar la coneccion
        If Not export.comprobarConexion(objService) Then
            conexion_exitosa = False
        Else
            conexion_exitosa = True

            '--- Establece parametros generales
            

            'id_glo_estoy_fuera = chkEstoyFuera.Checked
            'If chkEstoyFuera.Checked Then
            'id_glo_estoy_fuera = True
            'Else
            'id_glo_estoy_fuera = False
            'End If
        End If

        txtServidor.Focus()
        Cursor.Current = Cursors.Default


    End Sub

    Private Sub cmdRecarga_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRecarga.Click
        Dim dsxo As New DataSet
        Dim oProducto As New ProductoDT
        Cursor.Current = Cursors.WaitCursor
        Try
            '--- Inicializar URL del objeto de conexion      
            If Not objCore.construyeURL(chkEstoyFuera.Checked, objService, txtServidor.Text, txtRuta.Text) Then Return
            co_glo_fechaCarga = DateTimePicker1.Value
            getCargaCamion()
            xoErrorStatament.Text = "Se importo la informacion de consignacion de forma exitosa." + dtImportLog.Rows(dtImportLog.Rows.Count - 1).Item("xoErrorStatament")
            goToConfirmar = True
            cmdRecarga.Enabled = False
        Catch ex As Exception
            xoErrorStatament.Text = "Error." + ex.Message
            Cursor.Current = Cursors.Default
        End Try
        Cursor.Current = Cursors.Default
    End Sub
#End Region






    Private Sub lSoft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lSoft.Click
        Me.Close()
    End Sub

    Private Sub panFechaCarga_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panFechaCarga.Paint
        Dim objUtil As New UtilitarioBL
        objUtil.paintPannel(e, panFechaCarga)
    End Sub

    Private Sub pan_msImportar_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pan_msImportar.Paint
        Dim outil As New UtilitarioBL
        outil.paintPannel(e, pan_msImportar)
    End Sub
#End Region


    Private Sub lnkOrigen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        DateTimePicker2.Enabled = True
    End Sub


    Private Sub lnkRuta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkRuta.Click
        Dim ofrmSystat As New frmSystat
        Cursor.Current = Cursors.WaitCursor
        ofrmSystat.ShowDialog()
    End Sub



    Private Sub LinkLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkLabel1.Click
        ComboBox1.Enabled = True
        ComboBox1.Focus()
        ComboBox1.BackColor = Color.White
    End Sub
End Class

