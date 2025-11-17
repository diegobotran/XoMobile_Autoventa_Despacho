Imports System.Data
Imports System
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports Proyecto_xoMobile_Packs

Public Class frmBrindis
    Dim objRuta As New RutaBL
    Dim objUtilBL As New UtilitarioBL
    Dim cRecibo, cFactura, cNotaCredito As New CorrelativoCO
    Dim objUsuario As New UsuarioBl


    Private Sub frmBrindis_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim ST As New System.DateTime
        Dim DiaTexto = CStr(Format(Date.Today, "dddd"))
        Dim oce As New ceClient
        If Not dontLoad Then
            oce.shrink()
        End If
        lblDiaVenta.Text = DiaTexto
        lblFecha.Text = FormatDateTime(Date.Today, DateFormat.ShortDate)
        lblHora.Text = FormatDateTime(TimeOfDay, DateFormat.ShortTime)
        txtClave.Focus()
        panResultLogin.Visible = False
        panWarning.Visible = False
        panPie.Visible = True

        Select Case DiaTexto
            Case "lunes"
                id_glo_dia = 1
            Case "martes"
                id_glo_dia = 2
            Case "miércoles"
                id_glo_dia = 3
            Case "jueves"
                id_glo_dia = 4
            Case "viernes"
                id_glo_dia = 5
            Case "sábado"
                id_glo_dia = 6
            Case "domingo"
                id_glo_dia = 7
            Case Else
                MsgBox("El idioma del dispositivo no es el español, cambie el ajuste regional a Spanish (Guatemala) y verifique que la fecha y hora del dispositivo sea la correcta.", MsgBoxStyle.Exclamation, "Error al iniciar XO-Mobile")
                closenow = True
                Me.Close()
                Exit Sub
        End Select


        Try
            Dim dtRuta As New DataTable

            '--- Verifica si hay rutas cargadas
            dtRuta = objRuta.obtenerRutaActiva()

            '--- Verifica si la fecha de la hh es menor a la fecha de la ruta
            If DateTime.Now < dtRuta.Rows(0).Item("fechaEmision") Then
                MsgBox("La fecha de la HH debe ser mayor o igual a : " + dtRuta.Rows(0).Item("fechaEmision").ToString.Substring(0, 8) + " Cambie la fecha en Settings para continuar.", MsgBoxStyle.Critical, "xoMobile")
                closenow = True
                Me.Close()
                Exit Sub
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox("Iniciando aplicacion de sincronizacion.", MsgBoxStyle.Information, "Sincronizacion")
            oce.shrink()
            Cursor.Current = Cursors.WaitCursor
            Dim frmSincronizacion As New frmImportar_
            frmSincronizacion.ShowDialog()
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub txtClave_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtClave.KeyPress
        Cursor.Current = Cursors.WaitCursor
        Select Case e.KeyChar()
            Case ChrW(13)
                If objUsuario.login(txtClave.Text, "BRINDIS") Then
                    lnkRuta.Visible = True
                    Dim dtRuta As New DataTable

                    '--- Verifica si hay rutas cargadas
                    dtRuta = objRuta.obtenerRutaActiva()
                    If DateTime.Now < dtRuta.Rows(0).Item("fechaEmision") Then
                        MsgBox("La fecha y Hora de la HH debe ser mayor o igual a : " & dtRuta.Rows(0).Item("fechaEmision").ToString & " Cambie la fecha y hora en Settings para continuar.", MsgBoxStyle.Critical, "xoMobile")
                        closenow = True
                        Me.Close()
                        Return
                    End If
                    panLogin.Visible = False
                    objRuta.inicializarXoMobile()

                    '--- Factor de correccion de grids
                    If Me.Width <= 240 Then co_glo_FormFactor = 1 Else co_glo_FormFactor = Me.Width / 240
                    inicializarFormulario()



                    '---Alertar cuando la ruta no valida inventario
                    If Not xo_validaInventario Then
                        Dim msg As String = "INFORMACION, Esta ruta no valida inventario."
                        Dim title As String = "Confirmacion de Carga"
                        Dim style As String = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Information Or MsgBoxStyle.OkOnly
                        MsgBox(msg, style, title)
                    End If
                Else
                    panResultLogin.Visible = True
                    txtClave.Focus()
                End If
                txtClave.Text = ""
        End Select
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub cmdIniciar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdIniciar.Click
        Cursor.Current = Cursors.WaitCursor
        Dim frm_mPrincipal As New mPrincipal
        frm_mPrincipal.ShowDialog()
        co_glo_closeAndGo = False
        frmBrindis_Load(sender, e)
        panLogin.Visible = True

    End Sub

    Private Sub cmdCarga_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCarga.Click
        Cursor.Current = Cursors.WaitCursor
        If cmdCarga.Text = "Confirmar Carga" Then
            Dim frm_Carga As New frmConsultaCarga
            frm_Carga.ShowDialog()

            inicializarFormulario()
        ElseIf cmdCarga.Text = "Datos Iniciales" Then
            Dim ofrmPreguntas As New frmPreguntas
            ofrmPreguntas.ttipo = 1
            ofrmPreguntas.ShowDialog()
            inicializarFormulario()
        End If
    End Sub

    Private Sub inicializarFormulario()
        Dim dtRuta As New DataTable
        Dim objBitacora As New BitacoraBL
        Dim objRuta As New RutaBL

        '--- Progreso de correlativos consumidos
        Try
            objRuta.getIndicadoresCorrelativo(cRecibo, cFactura, cNotaCredito)
            barFactura.Value = cFactura.porcentajeConsumido
            lblFact.Text = "FACT-" + cFactura.quedan.ToString("##,##")
            barNc.Value = cNotaCredito.porcentajeConsumido
            lblNc.Text = "NC-" + cNotaCredito.quedan.ToString("##,##")
            barRecibo.Value = cRecibo.porcentajeConsumido
            lblRec.Text = "REC-" + cRecibo.quedan.ToString("##,##")
            panContinuar.Visible = False
            dtRuta = objRuta.obtenerRutaActiva()
            lblRuta.Text = "RUTA-" + dtRuta.Rows(0).Item("codRuta").ToString()
            id_glo_ruta = dtRuta.Rows(0).Item("id_ruta").ToString()
            lblVendedor.Text = co_glo_vendedor
        Catch ex As Exception
        End Try

        '--- Listado de condiciones para iniciar la ruta ---'
        If objRuta.existeCargaSinConfirmar Then
            '--- Confirmar carga
            panWarning.Visible = True
            panCarga.Visible = True
            cmdCarga.Text = "Confirmar Carga"
        ElseIf Not objBitacora.isOperacionRealizada_xo(9) Then
            '--- Toma de datos iniciales
            panWarning.Visible = True
            panCarga.Visible = True
            cmdCarga.Text = "Datos Iniciales"
        Else
            '--- Restricciones ejecutas puede continuar
            panWarning.Visible = False
            panCarga.Visible = False
            panContinuar.Location.Offset(30, 99)
            panContinuar.Visible = True
        End If
    End Sub

#Region " Dibujar paneles "
    Private Sub panResultLogin_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panResultLogin.Paint
        objUtilBL.paintPannel(e, panResultLogin)
    End Sub

    Private Sub panLogin_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panLogin.Paint
        objUtilBL.paintPannel(e, panLogin)
    End Sub

    Private Sub panTitulo_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panTitulo.Paint
        objUtilBL.paintPannel(e, panTitulo)
    End Sub

    Private Sub panTiempo_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panTiempo.Paint
        objUtilBL.paintPannel(e, panTiempo)
    End Sub

    Private Sub panPie_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panPie.Paint
        objUtilBL.paintPannel(e, panPie)
    End Sub

    Private Sub panWarning_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panWarning.Paint
        objUtilBL.paintPannel(e, panWarning)
    End Sub
#End Region

#Region " SYNC-APP "
    Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnk.Click
        Dim ofrmSincronizar As New frmImportar_
        Dim oBitacora As New BitacoraBL
        Cursor.Current = Cursors.WaitCursor
        panLogin.Visible = False
        ofrmSincronizar.TabControl1.TabPages.RemoveAt(2)
        ofrmSincronizar.TabControl1.TabPages.RemoveAt(1)
        ofrmSincronizar.TabControl1.SelectedIndex = 0
        ofrmSincronizar.ShowDialog()
        co_glo_closeAndGo = False
        frmBrindis_Load(sender, e)
        panLogin.Visible = True
        Cursor.Current = Cursors.Default
    End Sub
#End Region

    Private Sub frmBrindis_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Not closenow Then
            Dim msg = "Esta seguro de cerrar la aplicacion"
            Dim style = MsgBoxStyle.YesNo
            Dim response = MsgBox(msg, style, "xoMobile")
            If response = MsgBoxResult.No Then
                e.Cancel = True
            End If
        End If
    End Sub



    'Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint
    '    objUtilBL.paintPannel(e, Panel1)
    'End Sub

    Private Sub LinkLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkLabel1.Click
        '--- Datos del Release
        Dim msg = id_glo_sistema + id_glo_version + vbCrLf + "Release 2 - 10/04/2015" + vbCrLf + "Industrias Licoreras de Guatemala"
        Dim style = MsgBoxStyle.OkOnly
        Dim response = MsgBox(msg, style, "xoMobile")
    End Sub

    Private Sub lnkRuta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkRuta.Click
        Dim ofrmSystat As New frmSystat
        Cursor.Current = Cursors.WaitCursor
        ofrmSystat.ShowDialog()
    End Sub
End Class