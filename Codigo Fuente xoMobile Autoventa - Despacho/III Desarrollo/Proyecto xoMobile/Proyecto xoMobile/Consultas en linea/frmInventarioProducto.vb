Imports System.Threading
Imports System.Data
Imports Proyecto_xoMobile_Packs
Public Class frmInventarioProducto
    Public codigoReporte As Integer
    Shared dtCarga As New DataTable
   
#Region " INICIAR FORMULARIO "
    Private Sub frmInventarioProducto_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim objBitacora As New BitacoraBL
            Select Case codigoReporte
                Case 3
                    objBitacora.registrarOperacion(31, id_glo_cliente)
                    lblConsulta.Text = "Inventario actual de producto."
                    'imgConsulta.Image = imagenesConsultas.Images(0)
                Case 4
                    objBitacora.registrarOperacion(32, id_glo_cliente)
                    lblConsulta.Text = "Inventario actual de envase recibido."
                    'imgConsulta.Image = imagenesConsultas.Images(1)
                Case 5
                    objBitacora.registrarOperacion(33, id_glo_cliente)
                    lblConsulta.Text = "Inventario de ventas actual."
                    'imgConsulta.Image = imagenesConsultas.Images(2)
                Case 6
                    Me.Text = "Cheques"
                    objBitacora.registrarOperacion(34, id_glo_cliente)
                    lblConsulta.Text = "Consulta de cheques recibidos."
                    'imgConsulta.Image = imagenesConsultas.Images(3)
            End Select
            llenarGrid(codigoReporte)
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            MsgBox(ex.Message)
            Me.Close()
        End Try
    End Sub
    Private Sub llenarGrid(ByVal codigoReporte As String)
        Dim img = New ColumnHeader()
        Dim Codigo = New ColumnHeader()
        Dim Descripcion = New ColumnHeader()
        Dim inicial = New ColumnHeader()
        Dim actual = New ColumnHeader()

        'Se agrego para que muestre en el grid
        Dim ttipo = New ColumnHeader()
        Dim lvendido = New ColumnHeader()
        '************************************

        Dim objRuta As New RutaBL

        Dim dwCarga As New DataView
        Dim objInventario As New InventarioBL
        Dim objProducto As New ProductoBL
        Dim producto As New ProductoCO
        Dim objUtil As New UtilitarioBL

        img.Text = ""
        Codigo.Text = "Codigo"
        Descripcion.Text = "Descripcion"
        inicial.Text = "Inicial"
        actual.Text = "Actual"
        'Se agrego para que muestre en el grid
        ttipo.Text = "Tipo"
        lvendido.Text = "LVend"
        '********************************
        lstCarga.Columns.Add(img)
        lstCarga.Columns.Add(Codigo)
        lstCarga.Columns.Add(Descripcion)
        lstCarga.Columns.Add(inicial)
        'Se agrego para que muestre en el grid
        lstCarga.Columns.Add(ttipo)
        lstCarga.Columns.Add(lvendido)
        '*************************************

        Select Case codigoReporte
            Case 3
                dtCarga = objInventario.consultarMovimientoLiquidacion
            Case 4
                dtCarga = objRuta.obtenerEnvaseRecibido
                dwCarga = dtCarga.DefaultView
                dwCarga.RowFilter = " ttipo = 4 and idRuta = " + id_glo_ruta.ToString()
                dtCarga = dwCarga.ToTable
            Case 5
                dtCarga = objInventario.consultarMovimientoLiquidacion
            Case 6
                Codigo.Text = "Documento"
                Descripcion.Text = "Banco"
                actual.Text = "Importe"
                dtCarga = objRuta.obtenerChequesRecibidos
                dwCarga = dtCarga.DefaultView
                dwCarga.RowFilter = " idRuta = " + id_glo_ruta.ToString()
                dtCarga = dwCarga.ToTable
        End Select
        lstCarga.Columns.Add(actual)

        For i As Integer = 0 To dtCarga.Rows.Count() - 1
            Dim drow As DataRow = dtCarga.Rows(i)
            Dim lvi As New ListViewItem("")
            Dim unidades, cajas As String

            If codigoReporte = 6 Then
                lvi.SubItems.Add(drow("documento").ToString())
                lvi.SubItems.Add(drow("showvalue").ToString())
                lvi.SubItems.Add(FormatCurrency(drow("importe"), 2).ToString())

            Else
                lvi.SubItems.Add(drow("id_producto").ToString())
                lvi.SubItems.Add(drow("Descripcion").ToString())

                '--- Convertir unidades iniciales a cajas
                producto.unidadesCaja = objUtil.isdecimal(drow("unidadesCaja").ToString())
                unidades = drow("cantidadInicial").ToString
                cajas = "0"
                objProducto.convertirUnidadesAcajas(producto, unidades, cajas)
                lvi.SubItems.Add(cajas & "/" & unidades)
                  Select codigoReporte
                    Case 3, 4
                        unidades = drow("cantidadActual").ToString
                    Case 5
                        unidades = drow("cantidadVendida").ToString
                End Select
                cajas = "0"
                objProducto.convertirUnidadesAcajas(producto, unidades, cajas)
                lvi.SubItems.Add(cajas & "/" & unidades)
                lvi.SubItems.Add(drow("ttipo").ToString())
                lvi.SubItems.Add(drow("litrosVendido").ToString())
            End If
            lstCarga.Items.Add(lvi)
        Next
    End Sub
#End Region

#Region " IMPRESION DE REPORTE "
    Private Sub rsoft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rSoft.Click
        Try
            Dim starter As New ThreadStart(AddressOf Me.imprimir)
            Dim t As New Thread(starter)
            t.IsBackground = True
            t.Start()
        Catch ex As Exception
            MsgBox(ex.Message())
            Me.Close()
        End Try
    End Sub
    Private Sub imprimir()
        Dim objReport As New printReporte
        Dim reportString As String
        Select Case codigoReporte
            Case 3
                reportString = objReport.inventario_actualProducto(lstCarga)
            Case 4
                reportString = objReport.inventario_actualEnvase(lstCarga)
            Case 5
                reportString = objReport.inventario_venta(lstCarga)
            Case 6
                reportString = objReport.chequesRecibidos()
        End Select
    End Sub
#End Region

#Region " COMANDOS "
    Private Sub lSoft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lSoft.Click
        Me.Close()
    End Sub
#End Region
End Class