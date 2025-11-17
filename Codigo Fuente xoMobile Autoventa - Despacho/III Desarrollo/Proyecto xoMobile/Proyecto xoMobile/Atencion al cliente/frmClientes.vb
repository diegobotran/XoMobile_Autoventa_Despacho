Imports System.Data
Imports Proyecto_xoMobile_Packs
Public Class frmClientes

#Region "Variables y objetos"

    'Objetos
    Dim objCliente As New ClienteDT
    Dim firstEnabled As Boolean = True

    'Variables
    Dim dtClientes As New DataTable
    Dim dtBuscarCliente As New DataTable

    'Objetos de Comunicacion entre capas
    Public comCliente As New ClienteCO

    'Objetos de la capa de negocios
    Dim objClienteBL As New ClienteBL
    Dim oUtil As New UtilitarioBL


    
#End Region
    
#Region "Inicializar Objetos. "

    Private Sub frmClientes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objUtil As New UtilitarioBL
        Dim dtCliente As DataTable
        Try
            '--- Crear el listado de clientes
            crearLwClientes()

            '--- Titulo de la forma
            Me.Text = "Dia " + objUtil.xoDia(id_glo_dia)

            '--- Limite de correlativos
            If xo_LimiteCorrelativoAlcanzado Then
                panError.Visible = True
                lblError.Text = "Queda menos del 25% de correlativos."
            Else
                Dim size As System.Drawing.Size
                size.Width = lstClientes.Size.Width
                size.Height = lstClientes.Size.Height + 27
                lstClientes.Size = size
            End If


            '--- Combo busqueda
            dtCliente = objUtil.obtenerModGlobalPorVariable("KEY_CLI")
            lstCase.DataSource = dtCliente
            lstCase.ValueMember = dtCliente.Columns("idMod").ToString
            lstCase.DisplayMember = dtCliente.Columns("valor").ToString
            Cursor.Current = Cursors.Default
            firstEnabled = False
            lstClientes.Focus()
            'lstClientes.Items(0).Selected = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub crearLwClientes(Optional ByVal isVisitar As Boolean = True)
        Dim vClientes As New DataView
        Dim rlayer As New rLayerHandler
        Dim codigo = New ColumnHeader()
        Dim negocio = New ColumnHeader()
        Dim direccion = New ColumnHeader()
        Dim telefono = New ColumnHeader()


        lstClientes.Clear()
        codigo.Text = "Codigo" '0
        negocio.Text = "Negocio" '1
        direccion.Text = "Direccion" '3
        telefono.Text = "telefono" '4
        codigo.Width = 120
        direccion.Width = 350
        telefono.Width = 120
        lstClientes.Columns.Add(codigo)
        lstClientes.Columns.Add(negocio)
        lstClientes.Columns.Add(direccion)
        lstClientes.Columns.Add(telefono)

        '--- Obtener el listado de clientes
        dtClientes = objCliente.getListado(id_glo_dia)

        '--- Filtro ver todos
        If isVisitar Then
            vClientes = dtClientes.DefaultView
            vClientes.RowFilter = "visitar = 'True'"
            dtClientes = vClientes.ToTable
        End If

        '--- Agregar filas a la lista
        For i As Integer = 0 To dtClientes.Rows.Count - 1
            Dim drow As DataRow = dtClientes.Rows(i)
            Dim lvi As New ListViewItem(drow("codigo").ToString())
            lvi.SubItems.Add(Trim(drow("descripcion").ToString()))
            lvi.SubItems.Add(Trim(drow("direccion").ToString()))
            lvi.SubItems.Add(drow("telefono").ToString())
            lstClientes.Items.Add(lvi)

            '--- Clientes visitados
            Select Case drow("visitado")
                Case 1 ' completo
                    lstClientes.Items(i).ImageIndex = 0
                    lstClientes.Items(i).BackColor = xoInfomat
                Case 2 ' parcial
                    lstClientes.Items(i).ImageIndex = 1
                Case 3 ' no atencion
                    lstClientes.Items(i).ImageIndex = 2
                    lstClientes.Items(i).BackColor = xoWarning
            End Select

            If drow("Visitar") = False Then
                lstClientes.Items(i).ForeColor() = xoAttentionLabel
                lstClientes.Items(i).BackColor = xoAttention
            End If

            If drow("saldo") <> 0 Then
                lstClientes.Items(i).ForeColor = Color.DarkRed
            End If
        Next
        lstClientes.Enabled = True
        lstClientes.Focus()

        If dtClientes.Rows.Count <= 0 And tipoRuta = "16" Then
            Dim lvi As New ListViewItem("1")
            lvi.SubItems.Add("No hay clientes para el día " & oUtil.xoDia(id_glo_dia))
            lvi.SubItems.Add("")
            lvi.SubItems.Add("")
            lstClientes.Items.Add(lvi)
            lstClientes.Enabled = False
        End If
        lstClientes.Items(0).Selected = True

        '--- Titulo de la forma
        Me.Text = "Día " & oUtil.xoDia(id_glo_dia)
    End Sub

    Private Sub limpiarTxt()
        txtBuscar.Text = ""
    End Sub

#End Region

#Region "Buscar item en el listado. "

    Private Sub txtBuscar_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBuscar.KeyPress

        If e.KeyChar = ChrW(13) Then
            '--- Ayuda a ignorar el enter y evitar cambio de linea en el textbox

            Dim objUtil As New UtilitarioDT
            objUtil.buscarCliente(lstClientes, dtClientes, txtBuscar.Text)
        End If
    End Sub

    Private Sub txtBuscar_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuscar.TextChanged
        'Dim objUtil As New UtilitarioDT
        'objUtil.buscarCliente(lstClientes, dtClientes, txtBuscar.Text)
    End Sub
#End Region

#Region "Seleccionar un cliente para atender. "

    'Iniciar la atencion al presionar la tecla enter
    Private Sub lstClientes_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lstClientes.KeyPress
        If e.KeyChar = ChrW(13) Then
            Try
                If Me.lstClientes.SelectedIndices.Count <= 0 Then
                    MsgBox("Seleccione un cliente.")
                    Return
                End If
                atenderCliente()
                crearLwClientes()
                Cursor.Current = Cursors.Default
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
        If e.KeyChar = "B" Or e.KeyChar = "b" Then
            txtBuscar.Focus()
        End If
    End Sub

    'Iniciar la atencion al presionar el boton atender
    Private Sub cmdAtender_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.lstClientes.SelectedIndices.Count <= 0 Then
                MsgBox("Seleccione un cliente.")
                Return
            End If
            atenderCliente()
            crearLwClientes()
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    'Iniciar el proceso de atencion
    Private Sub atenderCliente()

        obtenerDetalleDelCliente()

        'Mostrar el menu de atencion 
        Cursor.Current = Cursors.WaitCursor
        Dim objMenAtencion As New mAtencion

        'Pasar una instancia del cliente
        objMenAtencion.Cliente = comCliente
        objMenAtencion.ShowDialog()


        '--- Limite de correlativos
        If xo_LimiteCorrelativoAlcanzado Then
            panError.Visible = True
            lblError.Text = "Queda menos del 25% de correlativos."
        End If
    End Sub

#End Region

#Region "Seleccionar un cliente para ver sus detalles. "
    Private Sub lInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lInfo.Click
        Cursor.Current = Cursors.WaitCursor
        If Me.lstClientes.SelectedIndices.Count <= 0 Then
            Return
        End If
        Dim frmDetalleCliente As New frmDetalleCliente()
        frmDetalleCliente.ShowDialog()
    End Sub
#End Region

#Region "Obtener una instancia del cliente seleccionado. "
    Private Sub obtenerDetalleDelCliente()
        'Capturar el codigo del cliente
        Dim itemSelected = Me.lstClientes.SelectedIndices(0)
        id_glo_cliente = lstClientes.Items(itemSelected).SubItems(0).Text
        comCliente.codigo = id_glo_cliente
        comCliente = objClienteBL.getDetalleDelCliente(id_glo_cliente)
        id_glo_condicion = comCliente.condicion
    End Sub
#End Region

#Region "Seleccionar una palabra de busqueda personalizada. "
    Private Sub lstCase_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCase.SelectedIndexChanged
        If Not firstEnabled Then
            txtBuscar.Text = lstCase.Text
        End If
    End Sub
#End Region

    Private Sub lstClientes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstClientes.SelectedIndexChanged
        If Me.lstClientes.SelectedIndices.Count <= 0 Then
            Return
        End If
        Dim itemSelected = Me.lstClientes.SelectedIndices(0)
        id_glo_cliente = lstClientes.Items(itemSelected).SubItems(0).Text
    End Sub


#Region " COMANDOS DEL MENU "
    Private Sub lAtender_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lAtender.Click
        Try
            If Me.lstClientes.SelectedIndices.Count <= 0 Then
                MsgBox("Seleccione un cliente.")
                Return
            End If
            atenderCliente()
            crearLwClientes()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmdLunes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLunes.Click
        id_glo_dia = 1
        crearLwClientes()
    End Sub

    Private Sub cmdMartes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMartes.Click
        id_glo_dia = 2
        crearLwClientes()
    End Sub

    Private Sub cmdMiercoles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMiercoles.Click
        id_glo_dia = 3
        crearLwClientes()
    End Sub

    Private Sub cmdJueves_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdJueves.Click
        id_glo_dia = 4
        crearLwClientes()
    End Sub

    Private Sub cmdViernes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdViernes.Click
        id_glo_dia = 5
        crearLwClientes()
    End Sub

    Private Sub cmdSabado_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSabado.Click
        id_glo_dia = 6
        crearLwClientes()
    End Sub

    Private Sub cmdDomingo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDomingo.Click
        id_glo_dia = 7
        crearLwClientes()
    End Sub

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        Me.Close()
    End Sub

    Private Sub cmVerTodos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmVerTodos.Click
        '--- False hace que se vean todos
        crearLwClientes(False)
    End Sub

#End Region

    Private Sub panError_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panError.Paint
        Dim objUtil As New UtilitarioBL
        objUtil.paintPannel(e, panError)

    End Sub

   
    Private Sub frmClientes_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = System.Windows.Forms.Keys.Up) Then
            'Up
        End If
        If (e.KeyCode = System.Windows.Forms.Keys.Down) Then
            'Down
        End If
        If (e.KeyCode = System.Windows.Forms.Keys.Left) Then
            'Left
        End If
        If (e.KeyCode = System.Windows.Forms.Keys.Right) Then
            'Right
        End If
        If (e.KeyCode = System.Windows.Forms.Keys.Enter) Then
            'Enter
        End If

    End Sub
End Class