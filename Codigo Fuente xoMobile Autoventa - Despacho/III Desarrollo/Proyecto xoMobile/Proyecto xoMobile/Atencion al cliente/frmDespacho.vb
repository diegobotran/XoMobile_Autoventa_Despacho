Imports System.Data
Imports Proyecto_xoMobile_Packs




Public Class frmDespacho

    

#Region " DECLARACION DE VARIABLES Y OBJETOS "
    '---Objetos de Comunicacion entre procesos
    Public Cliente As ClienteCO
    Public rLayer As New rLayerHandler

    '--- Objetos capa de negocio
    Dim oDespacho As New DespachoBL
    Dim msgCollection As New messageCollection
    Dim objUtil As New UtilitarioBL
    Public dtDespachos As New DataTable

    ' Esta linea se agrego para la prueba (VER. 2.0.13
    'Dim comDocumento As documentoCO
    '**************************************************

    '--- Variables
    Dim selectedIndex As Integer

#End Region

#Region " INICIALIZAR FORMULARIO "

    Private Sub Despacho_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'LINEA DE PRUEBA QUE SE AGREGO VER 2.0.13
            'comDocumento.isContado = True
            '**************************************

            'Obtener el listado de documentos a despachar
            dtDespachos = oDespacho.getDespachos(Cliente.codigo, rLayer)
            If rLayer.codigo = 0 Then
                crearListaDespachos(dtDespachos)
            Else
                Me.Close()
            End If
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            MsgBox(ex.Message)
            Cursor.Current = Cursors.Default
            Me.Close()
        End Try
    End Sub

    Private Sub crearListaDespachos(ByVal dtDespachos As DataTable)
        lstDespachos.Clear()

        Dim checkbox = New ColumnHeader()
        Dim id = New ColumnHeader()
        Dim Documento = New ColumnHeader()
        Dim Monto = New ColumnHeader()
        Dim Litros = New ColumnHeader()
        Dim razon = New ColumnHeader()
        Dim idPedido = New ColumnHeader()
        Dim isdiferente = New ColumnHeader()
        Dim tipoPago = New ColumnHeader()
        Dim descuentoPP = New ColumnHeader()
        Dim condicion = New ColumnHeader()

        id.Text = "No."                '1
        Documento.Text = "Documento"   '2
        Monto.Text = "Monto"           '3
        Litros.Text = "Litros"         '4
        razon.Text = "Razon"           '5
        idPedido.Text = "idPedido"     '6
        tipoPago.Text = "Tipo Pago"    '7
        descuentoPP.Text = "Descuento" '8
        descuentoPP.Text = "Condicion" '9

        lstDespachos.Columns.Add(checkbox)
        lstDespachos.Columns.Add(id)
        lstDespachos.Columns.Add(Documento)
        lstDespachos.Columns.Add(Monto)
        lstDespachos.Columns.Add(Litros)
        lstDespachos.Columns.Add(razon)
        lstDespachos.Columns.Add(idPedido)
        lstDespachos.Columns.Add(isdiferente)
        lstDespachos.Columns.Add(tipoPago)
        lstDespachos.Columns.Add(descuentoPP)
        lstDespachos.Columns.Add(condicion)

        '--- Ocultar campos de referencia
        idPedido.Width = 0
        isdiferente.Width = 0

        For i As Integer = 0 To dtDespachos.Rows.Count - 1
            Dim drow As DataRow = dtDespachos.Rows(i)
            Dim lvi As New ListViewItem()
            lvi.SubItems.Add(i + 1.ToString())
            lvi.SubItems.Add(drow("serie").ToString())
            lvi.SubItems.Add(FormatCurrency(drow("valor").ToString(), 2))
            lvi.SubItems.Add(drow("litros").ToString & " Lts.")
            lvi.SubItems.Add(drow("showvalue").ToString)
            lvi.SubItems.Add(drow("id_despacho").ToString)
            lvi.SubItems.Add(drow("isDiferente").ToString)
            lvi.SubItems.Add(drow("tipoPago").ToString)
            If objUtil.isDecimal(drow("importeDestoPP").ToString) = 0 Then
                lvi.SubItems.Add(drow("importeDesto").ToString)
            Else
                lvi.SubItems.Add(drow("importeDestoPP").ToString)
            End If
            lvi.SubItems.Add(drow("id_pedido").ToString)
            lvi.SubItems.Add(drow("condicion").ToString)
            lstDespachos.Items.Add(lvi)



            '--- Marcar el item checked si ya fue despachado
            If drow("despachado").ToString() = "True" Then
                lvi.BackColor = Color.GreenYellow
                lstDespachos.Items(lstDespachos.Items.Count - 1).ImageIndex = 1
            Else
                If drow("despachado").ToString() = "False" Or drow("despachado").ToString() = "" Then
                    '--- Evaluar si el despacho tiene cantidades diferentes.
                    If drow("isDiferente").ToString <> "0" Then
                        lstDespachos.Items(lstDespachos.Items.Count - 1).ImageIndex = 2
                        lstDespachos.Items(lstDespachos.Items.Count - 1).ForeColor() = xoAttentionLabel
                    Else
                        lvi.BackColor = xoInfomat
                        lstDespachos.Items(lstDespachos.Items.Count - 1).ImageIndex = 0
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub crearConsultaDespachos(ByVal dtDespachos As DataTable)
        lstConsulta.Clear()
        Dim one = New ColumnHeader()
        Dim idProducto = New ColumnHeader()
        Dim Descripcion = New ColumnHeader()
        Dim idRubro = New ColumnHeader()
        Dim trqt = New ColumnHeader()
        Dim precio = New ColumnHeader()
        Dim importe = New ColumnHeader()
        Dim importeDesto = New ColumnHeader()
        Dim total As Decimal = 0
        Dim desto As Decimal = 0

        idProducto.Text = "Codigo"    '1
        Descripcion.Text = "Producto"           '2
        idRubro.Text = "M"              '3
        trqt.Text = "CA/UN"             '4
        precio.Text = "Precio"          '5
        importe.Text = "Importe"        '6
        importeDesto.Text = "Descuento" '7

        lstConsulta.Columns.Add(one)
        lstConsulta.Columns.Add(idProducto)
        lstConsulta.Columns.Add(Descripcion)
        lstConsulta.Columns.Add(idRubro)
        lstConsulta.Columns.Add(trqt)
        lstConsulta.Columns.Add(precio)
        lstConsulta.Columns.Add(importe)
        lstConsulta.Columns.Add(importeDesto)
        one.Width = 0


        For i As Integer = 0 To dtDespachos.Rows.Count - 1
            Dim drow As DataRow = dtDespachos.Rows(i)
            Dim lvi As New ListViewItem()
            If drow("idRubro").ToString() = "L" Then
                lvi.SubItems.Add(drow("idProducto").ToString())
                lvi.SubItems.Add(Trim(drow("descripcion").ToString()))
                lvi.BackColor = xoInfomat
            Else
                lvi.SubItems.Add("")
                lvi.SubItems.Add("")
            End If
            lvi.SubItems.Add(drow("idRubro").ToString())
            lvi.SubItems.Add(drow("trqt").ToString())
            lvi.SubItems.Add(FormatCurrency(objUtil.isDecimal(drow("precio").ToString()), 2))
            lvi.SubItems.Add(FormatCurrency(objUtil.isDecimal(drow("importe").ToString()), 2))
            lvi.SubItems.Add(FormatCurrency(objUtil.isDecimal(drow("importeDesto").ToString()), 2))
            lstConsulta.Items.Add(lvi)
            total = total + objUtil.isDecimal(drow("importe").ToString())
            desto = desto + objUtil.isDecimal(drow("importeDesto").ToString())
        Next
        lblDestoc.Text = FormatCurrency(Math.Abs(desto), 2)
        lblImportec.Text = FormatCurrency(total, 2)
    End Sub

#End Region

#Region " BOTON DESPACHAR "

    Private Sub rSoft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAceptar.Click
        Cursor.Current = Cursors.WaitCursor
        If Not isMarcado() Then
            Cursor.Current = Cursors.Default
            If rLayer.codigo = 0 Then Exit Sub
            If rLayer.codigo <> 2 Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub listarMotivosDespacho()
        Dim dtRazones As New DataTable
        Dim outil As New UtilitarioBL

        '--- Inicializar componentes
        panAtencion.Visible = True
        lstDespachos.Enabled = False
        mainMenu1.MenuItems(0).Enabled = False
        mainMenu1.MenuItems(1).Enabled = False
        dtRazones = outil.getListaTipo("RAZONES_NO_DESPACHO")

        lstRazones.DataSource = dtRazones
        lstRazones.ValueMember = dtRazones.Columns(1).ToString
        lstRazones.DisplayMember = dtRazones.Columns(0).ToString
    End Sub

    Private Sub lstRazones_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lstRazones.KeyPress
        Select Case e.KeyChar()
            Case ChrW(13)
                oDespacho.actualizarMotivoDespacho(Cliente, lstRazones.SelectedValue)
                Me.Close()
            Case ChrW(Keys.Escape)
                lstRazones.SelectedIndex = 0
                panAtencion.Visible = False
                lstDespachos.Enabled = True
                mainMenu1.MenuItems(0).Enabled = True
                mainMenu1.MenuItems(1).Enabled = True
        End Select
    End Sub

    Private Function isMarcado() As Boolean
        Dim despachadosCount As Integer = 0
        Dim flagDespacho As Boolean = False

        For i As Integer = 0 To lstDespachos.Items().Count() - 1
            If lstDespachos.Items(i).BackColor = Color.GreenYellow Then
                despachadosCount += 1
                If lstDespachos.Items(i).Checked Then
                    MsgBox("Este despacho ya se realizo")
                    Return False
                End If
            Else
                If lstDespachos.Items(i).Checked Then
                    dtDespachos.Rows(i).Item("estado") = "1"
                    flagDespacho = True
                Else
                    dtDespachos.Rows(i).Item("estado") = "0"
                End If
            End If
        Next
        If despachadosCount = lstDespachos.Items().Count() Then
            rLayer.texto = "Ya se han realizado todos los despachos para este cliente."
            rLayer.codigo = 1
            Return False
        End If

        If Not flagDespacho Then
            MsgBox("Tiene que marcar los documentos despachados o declarar un motivo de no despacho.", MsgBoxStyle.Information)
            If msgCollection.raiseMensaje(500) = MsgBoxResult.Yes Then
                '--- Mostrar motivos de no despacho
                Dim oWorkflow As New UtilitarioBL
                listarMotivosDespacho()
                rLayer.codigo = 2
                lstRazones.Focus()
                Return False
            End If
        End If
        '--- Si llego hasta aqui entonces
        Me.Close()
    End Function
#End Region

#Region " PAINTERS "
    Private Sub panAtencion_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panAtencion.Paint
        objUtil.paintPannel(e, panAtencion)
    End Sub
#End Region

#Region " CHEQUEAR UN DESPACHO PARA ATENDER "
    Private Sub lstDespachos_ItemCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles lstDespachos.ItemCheck
        For i = 0 To lstDespachos.Items.Count() - 1
            If Not lstDespachos.Items(i).Index = e.Index Then
                lstDespachos.Items(i).Checked = False
            End If
        Next
    End Sub
#End Region

#Region " HELPERS / PANELS "
    Private Sub panIconografia_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panIconografia.Paint
        objUtil.paintPannel(e, panIconografia)
    End Sub

    Private Sub picMenu_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picMenu.Click
        If panIconografia.Visible Then
            panIconografia.Visible = False
        Else
            panIconografia.Visible = True
        End If
    End Sub
#End Region


   

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        Me.Close()

    End Sub
#Region " BOTON CONSULTA "
    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        If cmdConsulta.Text = "Cerrar" Then
            lstDespachos.Visible = True
            cmdConsulta.Text = "Consulta"
            'cmdAceptar.Enabled = False
        Else
            Dim itemSelected As Integer
            Dim pItems As New DataTable
            Dim idPedido As String

            Try
                itemSelected = Me.lstDespachos.SelectedIndices(0)
                idPedido = lstDespachos.Items.Item(itemSelected).SubItems(10).Text
            Catch ex As Exception
                Me.lstDespachos.Items(0).Selected = True
                itemSelected = Me.lstDespachos.SelectedIndices(0)
                idPedido = lstDespachos.Items.Item(itemSelected).SubItems(10).Text
            End Try

            pItems = oDespacho.ObtenerPedidoDetalleById(idPedido)
            lstDespachos.Visible = False
            crearConsultaDespachos(pItems)
            cmdConsulta.Text = "Cerrar"
        End If
    End Sub
#End Region

End Class