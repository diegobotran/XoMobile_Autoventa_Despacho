Imports System.Data
Imports System.Threading
Imports Proyecto_xoMobile_Packs

Public Class frmPresupuestoCliente

    Dim objRutaBL As New RutaBL
    Dim objUtilBL As New UtilitarioBL

    '--- Message box
    Dim msg As String
    Dim title As String
    Dim style As MsgBoxStyle


    Private Sub frmClientesSaldo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            crearListadoDePresupuesto(id_glo_cliente)
            setAlternatingRowColor()
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            MsgBox("Error al crear listado de clientes con saldo: " + ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub setAlternatingRowColor()
        For Each item As ListViewItem In lstPresupuesto.Items()
            If ((item.Index Mod 2) = 0) Then
                item.BackColor = Color.AliceBlue
                lstPresupuesto.Items(item.Index).BackColor = Color.AliceBlue
            Else
                lstPresupuesto.Items(item.Index).BackColor = Color.LightYellow
            End If
        Next
    End Sub

    Private Sub crearListadoDePresupuesto(ByVal idcliente As String)

        'Obtener el listado de clientes
        Dim dtPresupuestoRuta As New DataTable

        'dtPresupuestoRuta = objRutaBL.ObtenerPresupuesto()

        dtPresupuestoRuta = objRutaBL.ObtenerPresupuestoCliente(idcliente)

        Dim codigo = New ColumnHeader()
        Dim marca = New ColumnHeader()
        Dim lit_presupuesto = New ColumnHeader()
        Dim lit_real = New ColumnHeader()
        Dim venta_real = New ColumnHeader()
        Dim nec_lit_dia = New ColumnHeader()
        Dim nec_vta_dia = New ColumnHeader()



        codigo.Text = "" '0
        marca.Text = "Marca" '1
        lit_presupuesto.Text = "PPTO_Lit" '1
        lit_real.Text = "Lit_Real"
        venta_real.Text = "Vta_Real"
        nec_lit_dia.Text = "NCD. Ltr."
        nec_vta_dia.Text = "NCD Qtz."

        lstPresupuesto.Columns.Add(codigo)
        lstPresupuesto.Columns.Add(marca)
        lstPresupuesto.Columns.Add(lit_presupuesto)
        lstPresupuesto.Columns.Add(lit_real)
        lstPresupuesto.Columns.Add(venta_real)
        lstPresupuesto.Columns.Add(nec_lit_dia)
        lstPresupuesto.Columns.Add(nec_vta_dia)


        '--- Agregar filas a la lista
        lstPresupuesto.Items.Clear()
        For i As Integer = 0 To dtPresupuestoRuta.Rows.Count - 1
            Dim drow As DataRow = dtPresupuestoRuta.Rows(i)
            Dim lvi As New ListViewItem(drow("codigo").ToString())
            lvi.SubItems.Add(drow("descripcion").ToString())
            lvi.SubItems.Add(objUtilBL.isDecimal(drow("lit_presupuesto").ToString()))
            lvi.SubItems.Add(objUtilBL.isDecimal(drow("lit_real").ToString()))
            lvi.SubItems.Add(FormatCurrency(drow("venta_real"), 2).ToString())
            lvi.SubItems.Add(objUtilBL.isDecimal(drow("nec_lit_dia").ToString()))
            lvi.SubItems.Add(FormatCurrency(drow("nec_vta_dia"), 2).ToString())

            lstPresupuesto.Items.Add(lvi)
        Next

    End Sub

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        liberar()
        Me.Close()
    End Sub

    Private Sub liberar()
        lstPresupuesto.Clear()
    End Sub

    Private Sub mnuImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImprimir.Click
        Try
            Dim starter As New ThreadStart(AddressOf Me.imprimirResumen)
            Dim t As New Thread(starter)
            t.IsBackground = True
            t.Start()
        Catch ex As Exception
            MsgBox(ex.Message())
        End Try
    End Sub

    Private Sub imprimirResumen()

        Dim objReport As New printReporte
        Dim itemCount As Integer

        itemCount = lstPresupuesto.Items.Count
        If itemCount > 0 Then

            '--- Reporte de anulados
            Dim objImpresion As New Impresion
            If objImpresion.ConfirmaImpresion("Documentos emitidos.") Then
                objReport.resumenPresupuesto()
            End If
        Else
            MsgBox("No hay informacion para imprimir.")
        End If

    End Sub


End Class