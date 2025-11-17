Imports System.Data

Public Class frmClienteNuevo
    Dim documento As New DocumentoBL

#Region " FORM LOAD "
    Private Sub frmClienteNuevo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor.Current = Cursors.Default
        txtNegocio.Text = "Nombre del negocio"
        txtNombre.Text = "Nombre del cliente"
        txtNit.Text = "Nit"
        txtDPI.Text = "DPI"
        txtDepartamento.Text = "Departamento"
        txtMunicipio.Text = "Municipio"
        txtDireccion.Text = ""
        txtDummy.Focus()
        listarGiro()
        listarDepto()
    End Sub
#End Region

#Region " PLACE HOLDER "

    Private Sub txtDPI_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDPI.GotFocus
        txtDPI.ForeColor = Color.Black
        If txtDPI.Text.StartsWith("DPI") Then
            txtDPI.Text = ""
        Else
            txtDPI.SelectAll()
        End If
    End Sub

    Private Sub txtDPI_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDPI.LostFocus
        If txtDPI.Text.Length = 0 Then
            txtDPI.Text = "DPI"
            txtDPI.ForeColor = Color.Gray
        Else
            If Not (documento.validaDPI(txtDPI.Text)) Then
                MsgBox("CUI no valido")
                txtNit.Text = "CUI"
            End If
        End If
    End Sub

    Private Sub txtDepartamento_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtDepartamento.ForeColor = Color.Black
        If txtDepartamento.Text.StartsWith("Departamento") Then
            txtDepartamento.Text = ""
        Else
            txtDepartamento.SelectAll()
        End If
    End Sub

    Private Sub txtDepartamento_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtDepartamento.Text.Length = 0 Then
            txtDepartamento.Text = "Departamento"
            txtDepartamento.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub txtNombre_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNombre.GotFocus
        txtNombre.ForeColor = Color.Black
        If txtNombre.Text.StartsWith("Nombre") Then
            txtNombre.Text = ""
        Else
            txtNombre.SelectAll()
        End If
    End Sub

    Private Sub txtNombre_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNombre.LostFocus
        If txtNombre.Text.Length = 0 Then
            txtNombre.Text = "Nombre del cliente"
            txtNombre.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub txtNit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNit.GotFocus
        txtNit.ForeColor = Color.Black
        If txtNit.Text.StartsWith("Nit") Then
            txtNit.Text = ""
        Else
            txtNit.SelectAll()
        End If
    End Sub

    Private Sub txtNit_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNit.LostFocus
        If txtNit.Text.Length = 0 Then
            txtNit.Text = "Nit"
            txtNit.ForeColor = Color.Gray
        Else
            If Not (documento.validarNit(txtNit.Text)) Then
                MsgBox("NIT no valido")
                txtNit.Text = "Nit"
            End If
        End If
    End Sub



    Private Sub txtMunicipio_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtMunicipio.ForeColor = Color.Black
        If txtMunicipio.Text.StartsWith("Municipio") Then
            txtMunicipio.Text = ""
        Else
            txtMunicipio.SelectAll()
        End If
    End Sub

    Private Sub txtMunicipio_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtMunicipio.Text.Length = 0 Then
            txtMunicipio.Text = "Municipio"
            txtMunicipio.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub txtNegocio_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNegocio.GotFocus
        txtNegocio.ForeColor = Color.Black
        If txtNegocio.Text.StartsWith("Nombre") Then
            txtNegocio.Text = ""
        Else
            txtNegocio.SelectAll()
        End If
    End Sub

    Private Sub txtNegocio_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNegocio.LostFocus
        If txtNegocio.Text.Length = 0 Then
            txtNegocio.Text = "Nombre del negocio"
            txtNegocio.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub listarGiro()
        Dim dtRazones As New DataTable
        Dim outil As New UtilitarioBL

        '--- Inicializar componentes
        dtRazones = outil.getListaTipoGrupo("GIRO_NEGOCIO", "showvalue")
        lstGiro.DataSource = dtRazones
        lstGiro.ValueMember = dtRazones.Columns(1).ToString
        lstGiro.DisplayMember = dtRazones.Columns(0).ToString
    End Sub

    Private Sub listarDepto()
        Dim dtDepto As New DataTable
        Dim outil As New UtilitarioBL

        '--- Inicializar componentes
        dtDepto = outil.getListaTipoGrupoDepto()
        txtDepartamento.DataSource = dtDepto
        txtDepartamento.ValueMember = dtDepto.Columns(1).ToString
        txtDepartamento.DisplayMember = dtDepto.Columns(0).ToString
    End Sub

    Private Sub listarMuni()
        Dim dtMuni As New DataTable
        Dim outil As New UtilitarioBL
        Dim Departamento As String = ""

        Departamento = txtDepartamento.SelectedValue

        '--- Inicializar componentes
        dtMuni = outil.getListaTipoGrupoMuni(Departamento)

        txtMunicipio.DataSource = dtMuni
        txtMunicipio.ValueMember = dtMuni.Columns(1).ToString
        txtMunicipio.DisplayMember = dtMuni.Columns(0).ToString

    End Sub


#End Region

#Region " COMANDOS "

    '--- Salir de la pantalla
    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        Me.Close()

    End Sub

    '--- Crear cliente
    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        '---- Validar ingreso
        Cursor.Current = Cursors.WaitCursor
        If validarFormulario() Then
            Dim cliente As New ClienteBL
            cliente.crearOnline(txtNombre.Text.ToUpper, txtNegocio.Text.ToUpper, txtNit.Text.ToUpper, RTrim(txtDepartamento.SelectedValue.ToString.ToUpper), RTrim(txtMunicipio.SelectedValue.ToString.ToUpper), txtDireccion.Text.ToUpper, txtDPI.Text.ToUpper, True, lstGiro.SelectedValue)
            txtMunicipio.DataSource = Nothing
            frmClienteNuevo_Load(sender, e)
        End If
        Cursor.Current = Cursors.Default
    End Sub
#End Region

#Region " VALIDAR FORMULARIO "
    Private Function validarFormulario() As Boolean

        If Trim(txtNegocio.Text) = "Nombre del negocio" Then
            MsgBox("Escriba el nombre del negocio")
            Return False
        End If

        If Trim(txtNombre.Text) = "Nombre del cliente" Then
            MsgBox("Escriba el nombre de la persona que atiende el negocio")
            Return False
        End If

        If Trim(txtNit.Text) = "Nit" Then
            MsgBox("Escriba el numero de N.I.T.")
            Return False
        End If

        If Not (documento.validarNit(txtNit.Text)) Then
            MsgBox("NIT no valido")
            txtNit.Text = "Nit"
            Return False
        End If

        If Trim(txtDPI.Text) = "DPI" Then
            MsgBox("Escriba el numero de D.P.I")
            Return False
        End If

        If Not (documento.validaDPI(txtDPI.Text)) Then
            MsgBox("CUI no valido")
            txtDPI.Text = "CUI"
            Return False
        End If

        If Trim(txtDepartamento.Text.Length) <= 0 Then
            MsgBox("Escriba el numero del departamento")
            Return False
        End If

        If Trim(txtMunicipio.Text.Length) <= 0 Then
            MsgBox("Escriba el numero del municipio")
            Return False
        End If

        If txtDireccion.Text.Length <= 0 Then
            MsgBox("Escriba la direccion del negocio")
            Return False
        End If

        If lstGiro.Text.Length <= 0 Then
            MsgBox("Seleccione giro de negocio")
            Return False
        End If
        Return True
    End Function
#End Region

#Region " TEXT SHORTCUTS "

    Private Sub LinkLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkLabel1.Click
        txtDireccion.Text &= LinkLabel1.Text & " "
        txtDireccion.Focus()
        txtDireccion.SelectionStart = txtDireccion.Text.Length

    End Sub

    Private Sub LinkLabel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkLabel2.Click
        txtDireccion.Text &= LinkLabel2.Text & " "
        txtDireccion.Focus()
        txtDireccion.SelectionStart = txtDireccion.Text.Length
    End Sub

    Private Sub LinkLabel5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkLabel5.Click
        txtDireccion.Text &= LinkLabel5.Text & " "
        txtDireccion.Focus()
        txtDireccion.SelectionStart = txtDireccion.Text.Length
    End Sub

    Private Sub LinkLabel6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkLabel6.Click
        txtDireccion.Text &= "Colonia "
        txtDireccion.Focus()
        txtDireccion.SelectionStart = txtDireccion.Text.Length
    End Sub

    Private Sub LinkLabel9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkLabel9.Click
        txtDireccion.Text &= LinkLabel9.Text & " "
        txtDireccion.Focus()
        txtDireccion.SelectionStart = txtDireccion.Text.Length
    End Sub

    Private Sub LinkLabel10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkLabel10.Click
        txtDireccion.Text &= LinkLabel10.Text & " "
        txtDireccion.Focus()
        txtDireccion.SelectionStart = txtDireccion.Text.Length
    End Sub

    Private Sub LinkLabel7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkLabel7.Click
        txtDireccion.Text &= LinkLabel7.Text & " "
        txtDireccion.Focus()
        txtDireccion.SelectionStart = txtDireccion.Text.Length
    End Sub

    Private Sub LinkLabel8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkLabel8.Click
        txtDireccion.Text &= "Manzana"
        txtDireccion.Focus()
        txtDireccion.SelectionStart = txtDireccion.Text.Length
    End Sub

    Private Sub LinkLabel3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkLabel3.Click
        txtDireccion.Text &= "-"
        txtDireccion.Focus()
        txtDireccion.SelectionStart = txtDireccion.Text.Length
    End Sub

    Private Sub LinkLabel4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkLabel4.Click
        txtDireccion.Text &= " "
        txtDireccion.Focus()
        txtDireccion.SelectionStart = txtDireccion.Text.Length
    End Sub

    Private Sub imgHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgHelp.Click
        MsgBox("De un Click en las palabras para agregarlas a la direccion.")
    End Sub

#End Region

    
    
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        listarMuni()
    End Sub

    Private Sub txtDPI_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDPI.TextChanged

    End Sub
End Class