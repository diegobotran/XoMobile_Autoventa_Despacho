Imports System.Data

Public Class frmConsultaCarga

  
    Dim objRuta As New RutaBL
    Dim objUtilBL As New UtilitarioBL
    Dim msg As String
    Dim title As String
    Dim style As MsgBoxStyle
    Dim response As MsgBoxResult
    Public isRecarga As Boolean


    Private Sub frmConsultaCarga_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim dtCarga As New DataTable

            '--- Inicializar el encabezado del formulario
            initEncFormulario()

            '--- Obtener productos de las cargas no confirmadas
            'dtCarga = objRuta.obtenerCargasDelCamion()

            '--- Crear el Listado (Listview)
            crearListadoCarga()

            '--- Codigo de la Ruta
            lblRuta.Text = ""

            Cursor.Current = Cursors.Default

        Catch ex As Exception
            MsgBox(ex.Message)
            Me.Close()
        End Try

    End Sub

    Private Sub initEncFormulario()
        Dim dtCargas As New DataTable

        '--- Obtener el listado de cargas de esta ruta
        dtCargas = objRuta.obtenerListadoCargas()

        '--- Cargar combo rutas
        lstFechaCarga.DataSource = dtCargas
        lstFechaCarga.DisplayMember = dtCargas.Columns("fechaEmision").ColumnName.ToString()
        lstFechaCarga.ValueMember = dtCargas.Columns("sd").ColumnName.ToString()
    End Sub

    Private Sub crearListadoCarga()
        Dim producto = New ColumnHeader()
        producto.Width = 70 * co_glo_FormFactor
        Dim descripcion = New ColumnHeader()
        descripcion.Width = 250 * co_glo_FormFactor
        Dim cantidadInicial = New ColumnHeader()
        cantidadInicial.Width = 60 * co_glo_FormFactor
        Dim cajaUnidad = New ColumnHeader()
        cajaUnidad.Width = 60 * co_glo_FormFactor
        Dim litros = New ColumnHeader()
        litros.Width = 60 * co_glo_FormFactor

        producto.Text = "Producto"
        descripcion.Text = "Descripcion"
        cantidadInicial.Text = "Cantidad"
        cajaUnidad.Text = "CJ/UN"
        litros.Text = "Litros"

        lstCarga.Columns.Add(producto)
        lstCarga.Columns.Add(descripcion)
        lstCarga.Columns.Add(cantidadInicial)
        lstCarga.Columns.Add(cajaUnidad)
        lstCarga.Columns.Add(litros)
    End Sub

    Private Sub lstFechaCarga_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstFechaCarga.SelectedIndexChanged
        Dim sd As Integer
        sd = objUtilBL.isDecimal(lstFechaCarga.SelectedValue.ToString())

        If sd < 0 Then
            lstCarga.Items.Clear()
            lblTotalLitros.Text = "--"
            Return
        Else
            If sd > 0 Then

                '--- Cargar informacion de la ruta en el ListView
                listarCargaPorRuta(sd)

                '--- Obtener la informacion de la ruta
                lblRuta.Text = id_glo_codRuta

            End If
        End If

        '--- Habilitar checkbox de confirmacion para la ruta
        If lstCarga.Items.Count() > 0 Then
            chConfirmar.Enabled = True
        Else
            chConfirmar.Enabled = False
        End If

        If chConfirmar.CheckState = CheckState.Checked Then
            chConfirmar.Enabled = False
        Else
        End If
    End Sub

    Private Sub listarCargaPorRuta(ByVal sd As String)

        Dim dtCargaRuta As DataTable
        Dim totalLitros As Integer = 0

        '--- Obtener la carga por ruta
        dtCargaRuta = objRuta.obtenerCargaPorFechaCarga()

        '--- Agregar filas a la lista
        Try
            lstCarga.Items.Clear()
            For i As Integer = 0 To dtCargaRuta.Rows.Count - 1
                Dim drow As DataRow = dtCargaRuta.Rows(i)
                Dim lvi As New ListViewItem(drow("id_Producto").ToString())
                lvi.SubItems.Add(drow("descripcion").ToString())
                lvi.SubItems.Add(drow("cantidadInicial").ToString())
                lvi.SubItems.Add(drow("cj_un").ToString())
                lvi.SubItems.Add(drow("Litros").ToString())
                lstCarga.Items.Add(lvi)
                totalLitros = totalLitros + objUtilBL.isDecimal(drow("litros").ToString())
                lblTotalLitros.Text = totalLitros.ToString("##,##0.00")
            Next
            chConfirmar.Checked = dtCargaRuta.Rows(0).Item("confirmada")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Panel1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint
        objUtilBL.paintPannel(e, Panel1)
    End Sub

    Private Sub chConfirmar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chConfirmar.Click
        Try
            Dim objBitacora As New BitacoraBL
            Dim objInventario As New InventarioBL
            If chConfirmar.Checked = True Then
                title = "Confirmar carga"
                msg = "Esta seguro que la informacion del dispositivo movil coincide con el contenido real del camion?"
                style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.YesNo
                response = MsgBox(msg, style, title)
                If response = MsgBoxResult.Yes Then
                    ''--- Confirmar la carga
                    'If objRuta.confirmarCarga(lstFechaCarga.SelectedValue.ToString) Then
                    '    MsgBox("El contenido de la carga ha sido confirmado con exito")
                    '    chConfirmar.Enabled = False
                    '    '--- Bitacora Confirmacion
                    '    objBitacora.registrarOperacion(35, id_glo_cliente)
                    '    Me.Close()
                    'End If
                    '--- Confirmar la carga
                    If objInventario.confirmar(lstFechaCarga.SelectedValue.ToString) Then
                        MsgBox("El contenido de la carga ha sido confirmado con exito")
                        chConfirmar.Enabled = False

                        '--- Bitacora Confirmacion
                        objBitacora.registrarOperacion(35, id_glo_cliente)
                        Me.Close()
                    End If
                Else
                    chConfirmar.Checked = False
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub lSoft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lSoft.Click
        Me.Close()
    End Sub

    Private Sub frmConsultaCarga_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If objRuta.existeCargaSinConfirmar And isRecarga Then
            Dim msg = "Hay una recarga pendiente de confirmar." + vbCrLf + vbCrLf + "Seleccione OK para continuar sin confirmar la recarga de producto."
            Dim style = MsgBoxStyle.OkCancel
            Dim response = MsgBox(msg, style, "xoMobile")
            If response = MsgBoxResult.Cancel Then
                e.Cancel = True
            Else
                objRuta.eliminarRecargaSinConfirmar()
            End If
        End If
    End Sub
End Class