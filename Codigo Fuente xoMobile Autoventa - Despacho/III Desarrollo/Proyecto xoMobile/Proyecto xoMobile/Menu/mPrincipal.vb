Imports System.Data
Imports Proyecto_xoMobile_Packs

Public Class mPrincipal
    Dim objUtil As New UtilitarioBL
    Dim objBitacora As New BitacoraBL
    Dim itemSelected As Integer
    Dim sistema As Integer

#Region " Inicializar Menu Principal"

    Private Sub mPrincipal_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''--DRIVER
        'tipoRuta = 16
        'id_glo_ruta = 196
        'id_glo_codRuta = 196

        '--- Eliminar el menu de despachos
        Dim objruta As New RutaBL
        objruta.inicializarXoMobile()
        If tipoRuta <> "16" Then
            lstMenu.Items(11).ImageIndex = 12
            lstMenu.Items(11).Tag = "+cliente"
        Else
            lstMenu.Items(11).ImageIndex = 11
            lstMenu.Items(11).Tag = "11"
            lstMenu.Items(11).Text = "Despachos"
        End If
        inicializarMenu()
    End Sub

    Private Sub inicializarMenu()

        lstMenu.Focus()
        Cursor.Current = Cursors.Default
        If xo_LimiteCorrelativoAlcanzado Then
            panError.Visible = True
            lblError.Text = "Queda menos del 25% de correlativos."
        End If
    End Sub
#End Region



    Private Sub lstMenu_ItemActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMenu.ItemActivate
        realizar()
    End Sub

    Private Sub realizar()

        '--- Obtener el indice del item seleccionado
        If Me.lstMenu.SelectedIndices.Count <= 0 Then
            Return
        End If
        itemSelected = Me.lstMenu.SelectedIndices(0)

        '-------------------------------------------------------------------------------------------------'
        '--------------------------------       ATENCION AL CLIENTE      ---------------------------------'
        '-------------------------------------------------------------------------------------------------'
        If lstMenu.Items(itemSelected).Tag = "0" Then
            Cursor.Current = Cursors.WaitCursor
            Dim objFrmClientes As New frmClientes
            objFrmClientes.ShowDialog()
            inicializarMenu()
            Return
        End If

        '-------------------------------------------------------------------------------------------------'
        '------------------------------     DEVOLUCION DE ENVASE          --------------------------------'
        '-------------------------------------------------------------------------------------------------'
        If lstMenu.Items(itemSelected).Tag = "1" Then
            'txtClave.Text = "FG16081984"
            If objBitacora.isOperacionRealizada_xo(36) Then
                Dim objFrmDevproducto As New frmDevolucionBodega
                objFrmDevproducto.tipoLiquidacion = 0
                objFrmDevproducto.ShowDialog()
                inicializarMenu()
            Else
                panLogin.Visible = True
                txtClave.Focus()
                inicializarMenu()
                InputPanel.Enabled = True
            End If

            Return
        End If

        '-------------------------------------------------------------------------------------------------'
        '--------------------------------     DEVOLUCION DE PRODUCTO     ---------------------------------'
        '-------------------------------------------------------------------------------------------------'
        If lstMenu.Items(itemSelected).Tag = "2" Then
            'txtClave.Text = "FG16081984"
            If objBitacora.isOperacionRealizada_xo(37) Then
                Dim objFrmDevproducto As New frmDevolucionBodega
                objFrmDevproducto.tipoLiquidacion = 1
                objFrmDevproducto.ShowDialog()
                panLogin.Visible = False
                inicializarMenu()
            Else
                panLogin.Visible = True
                txtClave.Focus()
                inicializarMenu()
                InputPanel.Enabled = True
            End If
            Return
        End If

        '-------------------------------------------------------------------------------------------------'
        '--------------------------------       CONSULTAS EN LINEA       ---------------------------------'
        '-------------------------------------------------------------------------------------------------'
        If lstMenu.Items(itemSelected).Tag = "3" Then
            Cursor.Current = Cursors.WaitCursor
            Dim oFrmConsulta As New mConsulta
            oFrmConsulta.ShowDialog()
            inicializarMenu()
            Return
        End If

        '-------------------------------------------------------------------------------------------------'
        '-----------------------------       FINALIZAR/INICIAR RUTA       --------------------------------'
        '-------------------------------------------------------------------------------------------------'

        If lstMenu.Items(itemSelected).Tag = "7" Then
            If objBitacora.isOperacionRealizada_xo(10) Then
                MsgBox("Los datos finales ya fueron capturados.")
            Else
                Dim msg = "Desea ingresar los datos finales?"
                Dim style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.YesNo
                Dim response = MsgBox(msg, style, "Finalizar Ruta")
                If response = MsgBoxResult.Yes Then
                    Cursor.Current = Cursors.WaitCursor
                    Dim oFrmRuta As New frmPreguntas
                    oFrmRuta.ttipo = 2
                    oFrmRuta.ShowDialog()
                    inicializarMenu()
                End If
                Return
            End If
        End If

        '-------------------------------------------------------------------------------------------------'
        '-----------------------------------       SINCRONIZAR       -------------------------------------'
        '-------------------------------------------------------------------------------------------------'
        If lstMenu.Items(itemSelected).Tag = "5" Then
            panLogin.Visible = True
            inicializarMenu()
            txtClave.Focus()
            InputPanel.Enabled = True
            Return
        End If

        '-------------------------------------------------------------------------------------------------'
        '-----------------------------------       DESPACHOS       -------------------------------------'
        '-------------------------------------------------------------------------------------------------'
        If lstMenu.Items(itemSelected).Tag = "11" Then
            If objBitacora.isOperacionRealizada_xo(48) Then
                Dim ofrmDepacho As New frmDeclaraDespachos
                ofrmDepacho.realizado = True
                ofrmDepacho.ShowDialog()
            Else
                panLogin.Visible = True
                txtClave.Focus()
            End If
            Return
        End If

        '--------------------------------------------------------------------------------------------------------'
        '-----------------------------------       PRUEBA DE IMPRESION      -------------------------------------'
        '--------------------------------------------------------------------------------------------------------'
        If lstMenu.Items(itemSelected).Tag = "4" Then
            Dim objImpresion As New ImpresionBL
            objImpresion.pruebaImpresion()
            Return
        End If

        '--------------------------------------------------------------------------------------------------------'
        '-----------------------------------       RECARGA      -------------------------------------------------'
        '--------------------------------------------------------------------------------------------------------'
        If lstMenu.Items(itemSelected).Tag = "6" Then
            Cursor.Current = Cursors.WaitCursor
            Dim objImportar As New frmImportar_
            Dim objConfirmarCarga As New frmConsultaCarga
            objImportar.TabControl1.TabPages.RemoveAt(0)
            objImportar.TabControl1.TabPages.RemoveAt(0)
            objImportar.TabControl1.SelectedIndex = 0
            objImportar.ShowDialog()
            If objImportar.goToConfirmar Then
                objConfirmarCarga.isRecarga = True
                objConfirmarCarga.ShowDialog()
            End If
            Cursor.Current = Cursors.Default
            Return
        End If

        '--------------------------------------------------------------------------------------------------------'
        '-----------------------------------       FIN DE DIA      ----------------------------------------------'
        '--------------------------------------------------------------------------------------------------------'
        If lstMenu.Items(itemSelected).Tag = "8" Then
            Cursor.Current = Cursors.WaitCursor
            Dim objConsulta As New frmFinDia
            objConsulta.ShowDialog()
            Return
        End If

        '--------------------------------------------------------------------------------------------------------'
        '-----------------------------------       CONFIGURACION      -------------------------------------------'
        '--------------------------------------------------------------------------------------------------------'
        If lstMenu.Items(itemSelected).Tag = "9" Then
            Cursor.Current = Cursors.WaitCursor
            Dim objConfig As New frmConfig
            objConfig.ShowDialog()
            If objConfig.closeAndGo Then
                Me.Close()
            End If
            Return
        End If

        '--------------------------------------------------------------------------------------------------------'
        '-----------------------------------       CORRELATIVOS      --------------------------------------------'
        '--------------------------------------------------------------------------------------------------------'
        If lstMenu.Items(itemSelected).Tag = "10" Then
            Dim objRuta As New Ruta
            Dim dtCorrelativos As New DataTable
            Dim xString As String = Nothing
            dtCorrelativos = objRuta.getCorrelativo
            
            For i As Integer = 0 To dtCorrelativos.Rows.Count - 1
                Dim drow As DataRow = dtCorrelativos.Rows(i)
                xString = xString & "Tipo: " & drow("dTipo").ToString() & vbCrLf
                xString = xString & "Res.: " & drow("noResolucion").ToString() & vbCrLf
                xString = xString & "Ser.: " & drow("serie").ToString() & vbCrLf
                xString = xString & "Act.: " & drow("actual").ToString() - 1 & vbCrLf
                xString = xString & "------------------------------"
                xString = xString & vbCrLf
            Next
            MsgBox(xString)
        End If

        '--------------------------------------------------------------------------------------------------------'
        '----------------------------------       CLIENTE NUEVO      --------------------------------------------'
        '--------------------------------------------------------------------------------------------------------'
        If lstMenu.Items(itemSelected).Tag = "+cliente" Then
            Cursor.Current = Cursors.WaitCursor
            Dim objClienteNuevo As New frmClienteNuevo
            objClienteNuevo.ShowDialog()
            Return
        End If
    End Sub

    '--- Login a opciones restringidas
    Private Sub txtClave_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtClave.KeyPress
        Cursor.Current = Cursors.WaitCursor
        Select Case e.KeyChar()
            Case ChrW(13)
                Dim usuario As New UsuarioCo
                Dim objUsuario As New UsuarioBl

                '--- Sincronizacion
                If lstMenu.Items(itemSelected).Tag = "6" Or lstMenu.Items(itemSelected).Tag = "5" Then
                    panLogin.Visible = False
                    If objUsuario.login(txtClave.Text, "SINCRONIZACION") Then
                        Cursor.Current = Cursors.WaitCursor
                        Dim ofrmSincronizar As New frmImportar_
                        ofrmSincronizar.TabControl1.TabPages.RemoveAt(2)
                        ofrmSincronizar.TabControl1.SelectedIndex = 0
                        InputPanel.Enabled = False
                        ofrmSincronizar.ShowDialog()
                        If ofrmSincronizar.returnToMain Then Me.Close()
                    Else
                        'panError.Visible = True
                        'lblError.Text = "La clave ingresada no es correcta"
                        'txtClave.Focus()
                    End If
                End If

                '--- Bodegas
                If lstMenu.Items(itemSelected).Tag = "1" Or lstMenu.Items(itemSelected).Tag = "2" Then
                    If objUsuario.login(txtClave.Text, "BODEGA") Then
                        panLogin.Visible = False
                        Dim objFrmDevproducto As New frmDevolucionBodega

                        If lstMenu.Items(itemSelected).Tag = "1" Then
                            objFrmDevproducto.tipoLiquidacion = 0
                        End If

                        If lstMenu.Items(itemSelected).Tag = "2" Then
                            objFrmDevproducto.tipoLiquidacion = 1
                        End If

                        panError.Visible = False
                        InputPanel.Enabled = False
                        objFrmDevproducto.ShowDialog()


                    Else
                        panError.Visible = True
                        lblError.Text = "La clave ingresada no es correcta"
                        txtClave.Focus()
                    End If
                End If

                If lstMenu.Items(itemSelected).Tag = "11" Then
                    If objUsuario.login(txtClave.Text, "BODEGA") Then
                        Dim frmDespacho As New frmDeclaraDespachos
                        frmDespacho.ShowDialog()
                    Else
                        panError.Visible = True
                        lblError.Text = "La clave ingresada no es correcta"
                        txtClave.Focus()
                    End If
                End If
                txtClave.Text = ""
                panError.Visible = False
                panLogin.Visible = False
        End Select
        Cursor.Current = Cursors.Default
    End Sub

#Region " Dibujar paneles "
    Private Sub panLogin_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panLogin.Paint
        objUtil.paintPannel(e, panLogin)
    End Sub

    Private Sub panResultLogin_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        objUtil.paintPannel(e, panError)
    End Sub

    Private Sub panLoginError_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panError.Paint
        objUtil.paintPannel(e, panError)
    End Sub

    Private Sub cmdCerrarLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCerrarLogin.Click
        panLogin.Visible = False
        InputPanel.Enabled = False
    End Sub
#End Region

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        dontLoad = True
        Me.Close()
    End Sub


  
End Class