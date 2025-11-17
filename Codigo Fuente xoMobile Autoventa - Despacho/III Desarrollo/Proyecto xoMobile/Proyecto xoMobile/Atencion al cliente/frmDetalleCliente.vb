Imports System.Data
Imports Proyecto_xoMobile_Packs

Public Class frmDetalleCliente
    Dim objUtil As New UtilitarioBL
    Dim objCliente As New ClienteBL
    Dim documento As New DocumentoBL

    Private Sub frmDetalleCliente_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '*--- DRIVER ---------------------
        'Dim cliente As New ClienteBL
        'id_glo_cliente = 55011798
        ''*--- /DRIVER ---------------------
        Dim comCliente As New ClienteCO
        Try
            comCliente = objCliente.getDetalleDelCliente(id_glo_cliente)
            mostrarDatos(comCliente)
            Cursor.Current = Cursors.Default
            panel.Visible = False
            If id_glo_cui_hh = "X" Then
                rUpdate.Enabled = True
            Else
                rUpdate.Enabled = False
            End If
        Catch ex As Exception
            MsgBox("La informacion de este cliente no esta disponible" + ex.Message)
            Cursor.Current = Cursors.Default
            Me.Close()
        End Try
    End Sub

    Private Sub mostrarDatos(ByVal cliente As ClienteCO)
        Dim dtFormasPago As DataTable
        Dim objProducto As New ProductoDT
        Dim rlayer As New rLayerHandler
        Dim dtDescuentos As DataTable
        Dim oUtil As New UtilitarioBL

        lblNegocio.Text = cliente.negocio
        lblCodigo.Text = cliente.codigo
        lblCategoria.Text = cliente.categoria
        lblPropietario.Text = cliente.propietario
        lblDireccion.Text = cliente.direccion
        lblNit.Text = cliente.nit
        lblTelefonos.Text = cliente.telefono


        If Len(cliente.numeroDi) > 0 Then
            If cliente.numeroDi.Substring(0, 1) = "P" Then
                lblPasaporte.Text = cliente.numeroDi
            Else
                lblcui.Text = cliente.numeroDi
            End If
        End If
        

        lblDiaVisita.Text = objUtil.xoDia(cliente.diaVisita)
        lblCreditoAutorizado.Text = FormatCurrency(cliente.creditoAutorizado, 2)
        lblCreditoDisponible.Text = FormatCurrency(cliente.creditoDisponible, 2)
        lblDiasCredito.Text = cliente.diasCredito
        lblPresupuesto.Text = cliente.volPresupuesto & " Lts."
        lblVentaConSaldo.Checked = cliente.ventaConSaldo
        lblVentaConSaldoVencido.Checked = cliente.ventaConSaldov
        lblCondicion_.Text = cliente.condicion
        lblListaPrecio_.Text = cliente.listaPrecio
        dtFormasPago = objCliente.getViasPagoListado(False, False)
        lstformasPago.DataSource = dtFormasPago
        lstformasPago.DisplayMember = dtFormasPago.Columns(1).ToString

        '--- Segmentacion
        lblClaseCliente.Text = cliente.n_claseCliente
        lblRegion.Text = cliente.n_region_s
        lblCanal.Text = cliente.n_canal
        lblTipoRuta.Text = cliente.n_tipoRuta
        listarProductosClave(cliente)

        '--- Descuentos
        lblNombreCliente.Text = cliente.negocio
        lblDescuentos.Text = "El cliente es categoria " & cliente.categoria & " y tiene autorizados los descuentos: " & vbCrLf & vbCrLf
        dtDescuentos = objProducto.getDescuento("DESC", cliente, "%", rlayer)
        If rlayer.evaluaTabla(dtDescuentos) Then
            For i As Integer = 0 To dtDescuentos.Rows.Count - 1
                lblDescuentos.Text &= dtDescuentos.Rows(i).Item("importe").ToString & "%" & " en productos categoria " & dtDescuentos.Rows(i).Item("categoriaP").ToString & vbCrLf
            Next
        Else
            lblDescuentos.Text = "Esta categoria de cliente no tienen descuentos programados."
        End If

        If objCliente.aceptaCredito(cliente.categoria) And cliente.categoria <> "IL01" And cliente.categoria <> "07" Then
            lblDescuentos.Text &= vbCrLf
            lblDescuentos.Text &= "El cliente tiene derecho a " & oUtil.pDescuento("EF").ToString & "%" & " de descuento adicional si paga de contado."
        End If

        lblDescuentos.Text &= vbCrLf
        dtDescuentos = objProducto.getDescuento("DESCTA", cliente, "%", rlayer)
        If rlayer.evaluaTabla(dtDescuentos) Then
            lblDescuentos.Text &= "En temporada alta si compra el presupuesto:" & vbCrLf & vbCrLf
            For i As Integer = 0 To dtDescuentos.Rows.Count - 1
                lblDescuentos.Text &= dtDescuentos.Rows(i).Item("importe").ToString & "%" & " en productos categoria " & dtDescuentos.Rows(i).Item("categoriaP").ToString & vbCrLf
            Next
        End If

        dtDescuentos = objProducto.getDescuentoManual("%", rlayer)
        If rlayer.evaluaTabla(dtDescuentos) Then
            lblDescuentos.Text &= "Descuento manual en los productos :" & vbCrLf & vbCrLf
            For i As Integer = 0 To dtDescuentos.Rows.Count - 1
                lblDescuentos.Text &= dtDescuentos.Rows(i).Item("id_producto").ToString & " hasta un  " & dtDescuentos.Rows(i).Item("importe").ToString & "%" & vbCrLf
            Next
        End If
    End Sub


#Region " PAINTERS "
    Private Sub panValor_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panValor.Paint
        objUtil.paintPannel(e, panValor)
    End Sub

    Private Sub panAtributo_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panAtributo.Paint
        objUtil.paintPannel(e, panAtributo)
    End Sub

    Private Sub panValorA_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panValorA.Paint
        objUtil.paintPannel(e, panValorA)
    End Sub

    Private Sub panAtributoA_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panAtributoA.Paint
        objUtil.paintPannel(e, panValorA)
    End Sub
#End Region

#Region " BUTTONS "
    Private Sub rSoft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rSoft.Click
        Me.Close()
    End Sub
#End Region


    Private Sub listarProductosClave(ByVal cliente As ClienteCO)
        Dim oUtil As New UtilitarioDT
        Dim objProducto As New ProductoBL
        Dim dtProductosClave As New DataTable
        Dim dvProductosClave As New DataView

        dtProductosClave = objProducto.ObtenerListadoProductos("productos_clave", cliente)
        If dtProductosClave Is Nothing Then Return

        Dim codigo = New ColumnHeader()
        Dim negocio = New ColumnHeader()
        Dim direccion = New ColumnHeader()
        Dim telefono = New ColumnHeader()

        lvProductos.Clear()
        codigo.Text = "Codigo"
        negocio.Text = "Descripción"
        lvProductos.Columns.Add(codigo)
        lvProductos.Columns.Add(negocio)

        '--- Agregar filas a la lista
        For i As Integer = 0 To dtProductosClave.Rows.Count - 1
            Dim drow As DataRow = dtProductosClave.Rows(i)
            Dim lvi As New ListViewItem(drow("idmarca").ToString())
            lvi.SubItems.Add(Trim(drow("descripcion").ToString()))
            lvProductos.Items.Add(lvi)
        Next
        Try
            'oUtil.setAlternatingRowColor(lvProductos)
            lvProductos.Focus()
            lvProductos.Items(0).Selected = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub rUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rUpdate.Click
        panel.Visible = True
        txtnit.Text = lblNit.Text
        txtcui.Text = lblcui.Text
        txtPasaporte.Text = lblPasaporte.Text
    End Sub
    

    Private Sub txtnit_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtnit.Text.Length = 0 Then
            txtnit.Text = "CF"
            txtnit.ForeColor = Color.Gray
        Else
            If Not (documento.validarNit(txtnit.Text)) Then
                MsgBox("NIT no valido")
                txtnit.Text = "CF"
            End If
        End If
    End Sub

    Private Sub txtcui_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtcui.Text.Length = 0 Then
            txtcui.Text = ""
            txtcui.ForeColor = Color.Gray
        Else
            If Not (documento.validaDPI(txtcui.Text)) Then
                MsgBox("CUI no valido")
                txtcui.Text = ""
            End If
        End If
    End Sub

    Private Sub txtPasaporte_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtPasaporte.Text.Length = 0 Then
            txtPasaporte.Text = ""
            txtPasaporte.ForeColor = Color.Gray
        Else
            If Not (documento.validaPasaporte(txtPasaporte.Text)) Then
                MsgBox("PASAPORTE no valido")
                txtPasaporte.Text = ""
            End If
        End If
    End Sub

    Private Sub cmdProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProcesar.Click
        Dim comCliente As New ClienteCO
        Try
            Dim texto As String = ""
            txtnit.Text = txtnit.Text.ToUpper
            texto = Replace(txtnit.Text, "-", "")
            txtnit.Text = Replace(texto, "/", "")
        Catch ex As Exception
            txtnit.Text = "CF"
        End Try

        If (txtnit.Text = "CF") Then
            lblNit.Text = "C/F"
            If Len(txtcui.Text) > 0 Then
                If Not (documento.validaDPI(txtcui.Text)) Then
                    MsgBox("CUI no valido")
                    txtcui.Text = ""
                Else
                    lblcui.Text = txtcui.Text
                    panel.Visible = False
                    MessageBox.Show("ACTUALIZA DATOS NIT " & lblNit.Text & "  CUI " & txtcui.Text)
                    comCliente = objCliente.getDetalleDelCliente(id_glo_cliente)
                    comCliente.nit = lblNit.Text
                    comCliente.numeroDi = lblcui.Text
                    objCliente.actualizaNIT_CUI(comCliente)
                    Cursor.Current = Cursors.Default
                End If
            Else
                If Len(txtPasaporte.Text) > 0 Then
                    lblPasaporte.Text = txtPasaporte.Text
                    Dim quitarP As String = ""
                    quitarP = Replace(lblPasaporte.Text, "P", "")
                    MessageBox.Show("ACTUALIZA DATOS NIT " & lblNit.Text & "  Pasaporte P" & quitarP)
                    panel.Visible = False
                    comCliente = objCliente.getDetalleDelCliente(id_glo_cliente)
                    comCliente.nit = lblNit.Text
                    comCliente.numeroDi = "P" & quitarP
                    objCliente.actualizaNIT_CUI(comCliente)
                    Cursor.Current = Cursors.Default
                Else
                    lblPasaporte.Text = ""
                    MessageBox.Show("**** DEBE LLENAR EL CUI O PASAPORTE ****")
                End If
            End If
        Else
            Try
                If Not (documento.validarNit(txtnit.Text)) Then
                    MsgBox("NIT no valido")
                    txtnit.Text = ""
                Else
                    lblNit.Text = txtnit.Text
                    MessageBox.Show("ACTUALIZA DATOS NIT " & lblNit.Text)
                    panel.Visible = False
                    comCliente = objCliente.getDetalleDelCliente(id_glo_cliente)
                    comCliente.nit = lblNit.Text
                    objCliente.actualizaNIT_CUI(comCliente)
                    Cursor.Current = Cursors.Default
                    If Len(txtcui.Text) > 0 Then
                        If Not (documento.validaDPI(txtcui.Text)) Then
                            MsgBox("CUI no valido")
                            txtcui.Text = ""
                        Else
                            lblcui.Text = txtcui.Text
                            MessageBox.Show("ACTUALIZA DATOS NIT " & lblNit.Text & "  CUI " & txtcui.Text)
                            panel.Visible = False
                            comCliente = objCliente.getDetalleDelCliente(id_glo_cliente)
                            comCliente.nit = lblNit.Text
                            comCliente.numeroDi = lblcui.Text
                            objCliente.actualizaNIT_CUI(comCliente)
                            Cursor.Current = Cursors.Default
                        End If
                    Else
                        If Len(txtPasaporte.Text) > 0 Then
                            lblPasaporte.Text = txtPasaporte.Text
                            Dim quitarP As String = ""
                            quitarP = Replace(lblPasaporte.Text, "P", "")
                            MessageBox.Show("ACTUALIZA DATOS NIT " & lblNit.Text & "  Pasaporte P" & quitarP)
                            panel.Visible = False
                            comCliente = objCliente.getDetalleDelCliente(id_glo_cliente)
                            comCliente.nit = lblNit.Text
                            comCliente.numeroDi = "P" & quitarP
                            objCliente.actualizaNIT_CUI(comCliente)
                            Cursor.Current = Cursors.Default
                        Else
                            lblPasaporte.Text = ""
                        End If
                    End If
                End If
            Catch ex As Exception
                panel.Visible = False
                MsgBox("La informacion de este cliente no esta disponible" + ex.Message)
                Cursor.Current = Cursors.Default
            End Try

        End If
    End Sub

    Private Sub cmdAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnular.Click
        panel.Visible = False
        txtnit.Text = ""
        txtcui.Text = ""
        txtPasaporte.Text = ""
    End Sub
End Class