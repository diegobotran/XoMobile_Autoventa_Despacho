Imports System.Data
Imports System.Threading
Imports Proyecto_xoMobile_Packs

Public Class frmPresupuesto

    Dim objRutaBL As New RutaBL
    Dim objUtilBL As New UtilitarioBL

    '--- Message box
    Dim msg As String
    Dim title As String
    Dim style As MsgBoxStyle

    Private Sub frmClientesSaldo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            crearListadoDePresupuesto()
            setAlternatingRowColor()
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            MsgBox("Error al crear listado de clientes con saldo: " + ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub setAlternatingRowColor()
        Dim presupuesto As Decimal = 0
        Dim presupuestov As Decimal = 0
        Dim litros As Decimal = 0
        Dim Valor As Decimal = 0
        For Each item As ListViewItem In lstPresupuesto.Items

            For Each subitem In item.SubItems
                presupuesto = item.SubItems(3).Text
                litros = item.SubItems(4).Text

            Next

            If (presupuesto > litros) Then
                item.ImageIndex = 0
            Else
                item.ImageIndex = 1
            End If
            'ordenador de mayor a menor basados en los que estan en color rojo
            'Rojo -> los que estan abajo del presupuesto
            'Verde -> Los que estan iguales o mayores al presupuesto
        Next

        For Each item As ListViewItem In lstPresupuestoV.Items

            For Each subitem In item.SubItems
                presupuestov = item.SubItems(3).Text
                Valor = item.SubItems(4).Text
            Next
            If (presupuestov > Valor) Then
                'MsgBox("Presupuesto " + presupuesto.ToString)
                'MsgBox("litros " + presupuesto.ToString)
                item.ImageIndex = 0
            Else
                item.ImageIndex = 1
            End If
            'ordenador de mayor a menor basados en los que estan en color rojo
            'Rojo -> los que estan abajo del presupuesto
            'Verde -> Los que estan iguales o mayores al presupuesto
        Next
    End Sub

    Private Sub crearListadoDePresupuesto()

        'Obtener el listado de clientes
        Dim dtPresupuestoRuta As New DataTable
        'If (idcliente <> 0) Then
        'dtPresupuestoRuta = objRutaBL.ObtenerPresupuestoCliente(idcliente)
        'Else
        Dim hoy As Date = DateTime.Now.ToString("dd/MM/yyyy")
        Dim dias As Integer
        dias = hoy.DayOfWeek
        dtPresupuestoRuta = objRutaBL.ObtenerPresupuestoDia(dias)
        'End If
        Dim imgRm = New ColumnHeader()
        Dim imgRmV = New ColumnHeader()
        Dim codigo = New ColumnHeader()
        Dim codigoV = New ColumnHeader()
        Dim marca = New ColumnHeader()
        Dim marcaV = New ColumnHeader()
        Dim lit_presupuesto = New ColumnHeader()
        Dim mt_presupuesto = New ColumnHeader()
        Dim lit_real = New ColumnHeader()
        Dim venta_real = New ColumnHeader()
        Dim nec_lit_dia = New ColumnHeader()
        Dim nec_vta_dia = New ColumnHeader()
        Dim nec_lit_mes = New ColumnHeader()
        Dim nec_vta_mes = New ColumnHeader()


        imgRm.Text = ""
        codigo.Text = "" '0
        marca.Text = "Marca" '1
        lit_presupuesto.Text = "PPTO_Lit" '1
        mt_presupuesto.Text = "MT_PPTO" '1
        lit_real.Text = "Lit_Real"
        venta_real.Text = "Vta_Real"
        nec_lit_dia.Text = "NCD. Ltr."
        nec_vta_dia.Text = "NCD Qtz."
        nec_lit_mes.Text = "NCM. Ltr."
        nec_vta_mes.Text = "NCM Qtz."


        'Lista del presupuesto en Litros
        lstPresupuesto.Columns.Add(imgRm)
        lstPresupuesto.Columns.Add(codigo)
        lstPresupuesto.Columns.Add(marca)
        lstPresupuesto.Columns.Add(lit_presupuesto)
        lstPresupuesto.Columns.Add(lit_real)
        lstPresupuesto.Columns.Add(nec_lit_dia)
        lstPresupuesto.Columns.Add(nec_lit_mes)

        'Lista del presupesto Valorizado
        lstPresupuestoV.Columns.Add(imgRmV)
        lstPresupuestoV.Columns.Add(codigoV)
        lstPresupuestoV.Columns.Add(marcaV)
        lstPresupuestoV.Columns.Add(mt_presupuesto)
        lstPresupuestoV.Columns.Add(venta_real)
        lstPresupuestoV.Columns.Add(nec_vta_dia)
        lstPresupuestoV.Columns.Add(nec_vta_mes)


        lstPresupuesto.LargeImageList = imgListAppl
        lstPresupuestoV.LargeImageList = imgListAppl
        '--- Limpiar la lista de Valores
        lstPresupuesto.Items.Clear()
        lstPresupuestoV.Items.Clear()
        For i As Integer = 0 To dtPresupuestoRuta.Rows.Count - 1
            Dim drow As DataRow = dtPresupuestoRuta.Rows(i)
            Dim lvi As New ListViewItem("")
            lvi.SubItems.Add(drow("codigo").ToString())
            lvi.SubItems.Add(drow("descripcion").ToString())
            lvi.SubItems.Add(objUtilBL.isDecimal(drow("lit_presupuesto").ToString()))
            lvi.SubItems.Add(objUtilBL.isDecimal(drow("lit_real").ToString()))
            lvi.SubItems.Add(objUtilBL.isDecimal(drow("nec_lit_dia").ToString()))
            lvi.SubItems.Add(objUtilBL.isDecimal(drow("nec_lit_mes").ToString()))
            lstPresupuesto.Items.Add(lvi)
        Next

        For i As Integer = 0 To dtPresupuestoRuta.Rows.Count - 1
            Dim drow As DataRow = dtPresupuestoRuta.Rows(i)
            Dim lvi As New ListViewItem("")
            lvi.SubItems.Add(drow("codigo").ToString())
            lvi.SubItems.Add(drow("descripcion").ToString())
            lvi.SubItems.Add(objUtilBL.isDecimal(drow("mt_presupuesto").ToString()))
            lvi.SubItems.Add(FormatCurrency(drow("venta_real"), 2).ToString())
            lvi.SubItems.Add(FormatCurrency(drow("nec_vta_dia"), 2).ToString())
            lvi.SubItems.Add(FormatCurrency(drow("nec_vta_mes"), 2).ToString())
            lstPresupuestoV.Items.Add(lvi)
        Next

        lstPresupuesto.Items.Item(0).Selected() = True

    End Sub


    Private Sub crearListadoDePresupuesto2(ByVal fecha As Date)

        'Obtener el listado de clientes
        Dim dtPresupuestoRuta As New DataTable
        'If (idcliente <> 0) Then
        'dtPresupuestoRuta = objRutaBL.ObtenerPresupuestoCliente(idcliente)
        'Else
        Dim dia As Integer
        Try
            dia = fecha.DayOfWeek
            dtPresupuestoRuta = objRutaBL.ObtenerPresupuestoDia(dia)
        Catch ex As Exception
            MsgBox("No se obtuvo el dia seleccionado")
        End Try
        

        

        lstPresupuesto.LargeImageList = imgListAppl
        lstPresupuestoV.LargeImageList = imgListAppl

        '--- Limpiar la lista de Valores
        lstPresupuesto.Items.Clear()
        lstPresupuestoV.Items.Clear()
        For i As Integer = 0 To dtPresupuestoRuta.Rows.Count - 1
            Dim drow As DataRow = dtPresupuestoRuta.Rows(i)
            Dim lvi As New ListViewItem("")
            lvi.SubItems.Add(drow("codigo").ToString())
            lvi.SubItems.Add(drow("descripcion").ToString())
            lvi.SubItems.Add(objUtilBL.isDecimal(drow("lit_presupuesto").ToString()))
            lvi.SubItems.Add(objUtilBL.isDecimal(drow("lit_real").ToString()))
            lvi.SubItems.Add(objUtilBL.isDecimal(drow("nec_lit_dia").ToString()))
            lvi.SubItems.Add(objUtilBL.isDecimal(drow("nec_lit_mes").ToString()))
            lstPresupuesto.Items.Add(lvi)
        Next

        For i As Integer = 0 To dtPresupuestoRuta.Rows.Count - 1
            Dim drow As DataRow = dtPresupuestoRuta.Rows(i)
            Dim lvi As New ListViewItem("")
            lvi.SubItems.Add(drow("codigo").ToString())
            lvi.SubItems.Add(drow("descripcion").ToString())
            lvi.SubItems.Add(objUtilBL.isDecimal(drow("mt_presupuesto").ToString()))
            lvi.SubItems.Add(FormatCurrency(drow("venta_real"), 2).ToString())
            lvi.SubItems.Add(FormatCurrency(drow("nec_vta_dia"), 2).ToString())
            lvi.SubItems.Add(FormatCurrency(drow("nec_vta_mes"), 2).ToString())
            lstPresupuestoV.Items.Add(lvi)
        Next

        'For i As Integer = 0 To lstPresupuesto.Items.Count - 1
        ' lstPresupuesto.Items(i).ImageIndex = i
        ' Next


        lstPresupuesto.Items.Item(0).Selected() = True

    End Sub

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        liberar()
        Me.Close()
    End Sub

    Private Sub liberar()
        lstPresupuesto.Clear()
        lstPresupuestoV.Clear()
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


    Private Sub imprimirResumen3()
        Dim fechaDia As Date = DateTime.Now.ToString("dd/MM/yyyy")
        Dim diaD As Integer

        diaD = fechaDia.DayOfWeek

        Dim objReport As New printReporte
        Dim itemCount As Integer

        itemCount = lstPresupuesto.Items.Count
        If itemCount > 0 Then

            '--- Reporte de anulados
            Dim objImpresion As New Impresion
            If objImpresion.ConfirmaImpresion("Documentos emitidos.") Then
                objReport.resumenPresupuestoDia(diaD)
            End If
        Else
            MsgBox("No hay informacion para imprimir.")
        End If

    End Sub

    Private Sub imprimirResumen4()
        Dim fechaDia As Date = DateTime.Now.ToString("dd/MM/yyyy")
        Dim diaD As Integer

        diaD = fechaDia.DayOfWeek

        Dim objReport As New printReporte
        Dim itemCount As Integer

        itemCount = lstPresupuesto.Items.Count
        If itemCount > 0 Then

            '--- Reporte de anulados
            Dim objImpresion As New Impresion
            If objImpresion.ConfirmaImpresion("Documentos emitidos.") Then
                objReport.resumenPresupuestoDiaV(diaD)
            End If
        Else
            MsgBox("No hay informacion para imprimir.")
        End If

    End Sub

    Private Sub imprimirResumen2()

        Dim objReport As New printReporte
        Dim itemCount As Integer

        itemCount = lstPresupuesto.Items.Count
        If itemCount > 0 Then

            '--- Reporte de anulados
            Dim objImpresion As New Impresion
            If objImpresion.ConfirmaImpresion("Documentos emitidos.") Then
                objReport.resumenPresupuestoV()
            End If
        Else
            MsgBox("No hay informacion para imprimir.")
        End If

    End Sub

    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        Try
            Dim starter As New ThreadStart(AddressOf Me.imprimirResumen)
            Dim t As New Thread(starter)
            t.IsBackground = True
            t.Start()
        Catch ex As Exception
            MsgBox(ex.Message())
        End Try
    End Sub

    Private Sub MenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem3.Click
        Try
            Dim starter As New ThreadStart(AddressOf Me.imprimirResumen2)
            Dim t As New Thread(starter)
            t.IsBackground = True
            t.Start()
        Catch ex As Exception
            MsgBox(ex.Message())
        End Try
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        Try
            crearListadoDePresupuesto2(DateTimePicker1.Text)
            setAlternatingRowColor()
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            MsgBox("Error al crear listado de clientes con saldo: " + ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub txtClave_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    End Sub

    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem4.Click
        Try
            Dim starter As New ThreadStart(AddressOf Me.imprimirResumen3)
            Dim t As New Thread(starter)
            t.IsBackground = True
            t.Start()
        Catch ex As Exception
            MsgBox(ex.Message())
        End Try
    End Sub

    Private Sub MenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem5.Click
        Try
            Dim starter As New ThreadStart(AddressOf Me.imprimirResumen4)
            Dim t As New Thread(starter)
            t.IsBackground = True
            t.Start()
        Catch ex As Exception
            MsgBox(ex.Message())
        End Try
    End Sub

End Class