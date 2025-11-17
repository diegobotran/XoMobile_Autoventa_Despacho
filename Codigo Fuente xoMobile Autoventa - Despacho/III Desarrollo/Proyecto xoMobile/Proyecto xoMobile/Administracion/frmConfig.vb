Imports System.Data

Public Class frmConfig
    Dim selectedGrid As Integer = 0
    Public closeAndGo As Boolean = False

    Private Sub frmConfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim control As Control
        Dim objUtil As New UtilitarioBL

        For Each control In Me.Controls
            If Not control.Tag = id_glo_rol Then

            End If
        Next
        If id_glo_rol = "SUPE" Then
            Panel1.Visible = False
        End If
        Cursor.Current = Cursors.Default
        If (xo_validaInventario) Then TrackBar1.Value = 1 Else TrackBar1.Value = 0
        If (co_glo_searchCliente = "descripcion") Then TrackBar2.Value = 1 Else TrackBar2.Value = 0
        If (co_glo_searchProducto = "descripcion") Then TrackBar3.Value = 1 Else TrackBar3.Value = 0
        Try
            actualizarCliente()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Try

            actualizarProducto()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TrackBar1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.ValueChanged
        Cursor.Current = Cursors.WaitCursor
        Dim objUtil As New UtilitarioBL
        Try
            If TrackBar1.Value = 1 Then
                xo_validaInventario = True
                objUtil.actualizarModGlobal("xo_validaInventario", "True")
            Else
                xo_validaInventario = False
                objUtil.actualizarModGlobal("xo_validaInventario", "False")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub TrackBar2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar2.ValueChanged
        Cursor.Current = Cursors.WaitCursor
        Dim objUtil As New UtilitarioBL
        Try
            If TrackBar2.Value = 0 Then
                objUtil.actualizarModGlobal("co_glo_searchCliente", "codigo")
                co_glo_searchCliente = "codigo"
            Else
                objUtil.actualizarModGlobal("co_glo_searchCliente", "descripcion")
                co_glo_searchCliente = "descripcion"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub TrackBar3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar3.ValueChanged

        Cursor.Current = Cursors.WaitCursor
        Dim objUtil As New UtilitarioBL
        Try
            If TrackBar3.Value = 0 Then
                objUtil.actualizarModGlobal("co_glo_searchProducto", "codigo")
                co_glo_searchProducto = "Codigo"
            Else
                objUtil.actualizarModGlobal("co_glo_searchProducto", "descripcion")
                co_glo_searchProducto = "descripcion"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub cmdProducto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProducto.Click
        Dim objUtil As New UtilitarioBL
        Try
            objUtil.crearModGlobal("KEY_PROD", txtPalabra.Text)
            actualizarProducto()
            txtPalabra.Text = ""
            txtPalabra.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmdCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCliente.Click
        Dim objUtil As New UtilitarioBL
        Try
            objUtil.crearModGlobal("KEY_CLI", txtPalabra.Text)
            actualizarCliente()
            txtPalabra.Text = ""
            txtPalabra.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmdSingle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSingle.Click
        Try
            Dim objUtil As New UtilitarioBL
            If selectedGrid = 1 Then
                objUtil.eliminarModGlobal("KEY_PROD", dgProducto.SelectedValue, False)
                actualizarProducto()
            Else
                objUtil.eliminarModGlobal("KEY_CLI", dgCliente.SelectedValue, False)
                actualizarCliente()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmdAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAll.Click
        Dim objUtil As New UtilitarioBL
        Try
            If selectedGrid = 1 Then
                objUtil.eliminarModGlobal("KEY_PROD", "", True)
                actualizarProducto()
            Else
                objUtil.eliminarModGlobal("KEY_CLI", "", True)
                actualizarCliente()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub lSoft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lSoft.Click
        Me.Close()
    End Sub
   
    Private Sub dgProducto_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgProducto.GotFocus
        selectedGrid = 1
        lblAlertEliminar.Text = "Eliminar palabra en producto "
    End Sub

    Private Sub dgCliente_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgCliente.GotFocus
        selectedGrid = 2
        lblAlertEliminar.Text = "Eliminar palabra en cliente "
    End Sub

    Private Sub actualizarCliente()
        Dim objUtil As New UtilitarioBL
        Dim dtCliente As DataTable
        dtCliente = objUtil.obtenerModGlobalPorVariable("KEY_CLI")
        dgCliente.DataSource = dtCliente
        dgCliente.ValueMember = dtCliente.Columns("idMod").ToString
        dgCliente.DisplayMember = dtCliente.Columns("valor").ToString
    End Sub

    Private Sub actualizarProducto()
        Dim objUtil As New UtilitarioBL
        Dim dtProducto As DataTable
        dtProducto = objUtil.obtenerModGlobalPorVariable("KEY_PROD")
        dgProducto.DataSource = dtProducto
        dgProducto.ValueMember = dtProducto.Columns("idMod").ToString
        dgProducto.DisplayMember = dtProducto.Columns("valor").ToString
    End Sub

    Private Sub cmdRecuperar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRecuperar.Click
        panLogin.Visible = True

    End Sub

    Private Sub cmdRestaurar_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRestaurar.Click
        Try
            Dim title = "Reconstruir"
            Dim msg = "Esta seguro de reonstruir la base de datos?"
            Dim style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.YesNo
            Dim response = MsgBox(msg, style, title)
            If response = MsgBoxResult.Yes Then
                Cursor.Current = Cursors.WaitCursor
                Dim ce As New ceClient
                ce.sdf_restore()
                Cursor.Current = Cursors.Default
                closeAndGo = True
                MsgBox("Base de datos reconstruida.")
                panLogin.Visible = False
                'Bitacora
                Dim objBitacora As New BitacoraBL
                objBitacora.registrarOperacion(42, id_glo_cliente)
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox("No se puede reonstruir el ultimo estado de la base de datos, intente nuevamente despues de reiniciar el equipo. Si el problema persiste comuniquese al departamento de informatica." + ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub panLogin_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtClave_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtClave.KeyPress
        Cursor.Current = Cursors.WaitCursor
        Select Case e.KeyChar()
            Case ChrW(13)
                Try
                    Dim objUsuario As New UsuarioBl
                    If (txtClave.Text = "xo2020.") Then
                        Dim title = "Recuperar"
                        Dim msg = "Esta seguro de recuperar la base de datos?"
                        Dim style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.YesNo
                        Dim response = MsgBox(msg, style, title)
                        If response = MsgBoxResult.Yes Then
                            Cursor.Current = Cursors.WaitCursor
                            Dim ce As New ceClient
                            ce.sdf_restoreFromBackup()
                            Cursor.Current = Cursors.Default
                            closeAndGo = True
                            MsgBox("Base de datos recuperada.")
                            'Bitacora
                            Dim objBitacora As New BitacoraBL
                            objBitacora.registrarOperacion(41, id_glo_cliente)
                            Me.Close()
                        End If
                    End If

                Catch ex As Exception
                    MsgBox("No se puede recuperar el ultimo estado de la base de datos, intente nuevamente despues de reiniciar el equipo. Si el problema persiste utilice la opcion RECONSTRUIR." + ex.Message, MsgBoxStyle.Critical)
                End Try
        End Select
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub TrackBar4_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar4.ValueChanged
        Cursor.Current = Cursors.WaitCursor
        Dim objUtil As New UtilitarioBL
        Try
            If TrackBar4.Value = 0 Then
                'objUtil.actualizarModGlobal("co_glo_searchCliente", "codigo")
                MessageBox.Show("Activado RED" & id_glo_internet)
                id_glo_internet = False
            Else
                'objUtil.actualizarModGlobal("co_glo_searchCliente", "descripcion")
                MessageBox.Show("Activado INTERNET" & id_glo_internet)
                id_glo_internet = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = Cursors.Default
    End Sub
End Class