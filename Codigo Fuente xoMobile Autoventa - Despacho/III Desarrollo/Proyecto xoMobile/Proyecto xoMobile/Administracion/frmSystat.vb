Imports System.Data
Imports Proyecto_xoMobile_Packs

Public Class frmSystat

    Dim lvi As New ListViewItem
    Dim dtGenerales As New DataTable

    Private Sub frmSysStat_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ruta As New RutaBL
        lblFecha.Text = FormatDateTime(Date.Today, DateFormat.ShortDate)
        ruta.inicializarXoMobile()
        inicializarLstSS()
        opcGenerales()
        opcMovimientos()
        opcRuta()
        opcSincronizacion()
        Cursor.Current = Cursors.Default
    End Sub


#Region " Operaciones de control"

    Private Sub opcGenerales()
        '---Agregar Categoria
        lvi = agregarCategoriaSS("GENERALES")

        '--- Agregar Etiquetas
        Dim oRuta As New RutaBL
        Dim oCliente As New ClienteBL

        Dim cliente As ClienteCO
        Dim ruta As New RutaCO



        ruta = oRuta.getActiva()
        cliente = oCliente.getDetalleDelCliente(ruta.clienteGenerico)

        With ruta
            agregarEtiqueta("Ruta:", .codRuta)
            agregarEtiqueta("Vendedor: ", cliente.negocio, .clienteGenerico)
            agregarEtiqueta("Importo datos el:", .fechaEmision.Date & "-" & .fechaEmision.Hour & ":" & .fechaEmision.Minute & ":" & .fechaEmision.Second)
        End With
        agregarCategoriaSS("")
    End Sub

    Private Sub opcMovimientos()


        '---Agregar Categoria
        lvi = agregarCategoriaSS("OPERACIONES")

        '--- Agregar Etiquetas
        Dim oDocumentos As New DocumentoBL
        agregarEtiqueta("No. Facturas", oDocumentos.cantidadDocumentos("FA"))
        agregarEtiqueta("No. NC", oDocumentos.cantidadDocumentos("NC"))
        agregarEtiqueta("No. Recibos", oDocumentos.cantidadDocumentos("RE"))

        If tipoRuta = "16" Then
            agregarEtiqueta("Despachos Programados", oDocumentos.cantidadDocumentos("DP"))
        End If

        agregarCategoriaSS("")
    End Sub

    Private Sub opcRuta()

        Dim oRuta As New RutaBL
        Dim oBitacora As New BitacoraBL
        Dim ruta As New RutaCO
        Dim valor As String = ""

        '---Agregar Categoria
        lvi = agregarCategoriaSS("LIQUIDACION")

        '--- Agregar Etiquetas
        Dim oDocumentos As New DocumentoBL
        If oBitacora.isOperacionRealizada_xo(37) Then valor = "Si" Else valor = "No"
        agregarEtiqueta("B. Producto Terminado", valor)
        If oBitacora.isOperacionRealizada_xo(36) Then valor = "Si" Else valor = "No"
        agregarEtiqueta("B. Envase", valor)
        If oBitacora.isOperacionRealizada_xo(10) Then valor = "Si" Else valor = "No"
        agregarEtiqueta("Preg. Finales", valor)
        If ruta.exportar Then valor = "si" Else valor = "No"
        agregarEtiqueta("Deposito", valor)
        If oBitacora.isOperacionRealizada_xo(38) Then valor = "Si" Else valor = "No"
        agregarEtiqueta("L.Creditos", valor)
        agregarCategoriaSS("")
    End Sub

    Private Sub opcSincronizacion()

        Dim oRuta As New RutaBL
        Dim oBitacora As New BitacoraBL
        Dim ruta As New RutaCO
        Dim valor As String = ""

        Dim oDocumentos As New DocumentoBL
        ruta = oRuta.getActiva()

        '---Agregar Categoria
        lvi = agregarCategoriaSS("SINCRONIZACION")

        '--- Agregar Etiquetas
        If ruta.exportar Then valor = "Lista" Else valor = "Pendiente"
        agregarEtiqueta("Exportar", valor)
        If ruta.importar Then valor = "Importar" Else valor = "Pendiente"
        agregarEtiqueta("Importar", valor)
        agregarCategoriaSS("")
    End Sub

#End Region

#Region "Controles UI"

    Private Sub inicializarLstSS()
        Dim dtRetorno As New DataTable
        Dim ssCategoria = New ColumnHeader()
        Dim ssEtiqueta = New ColumnHeader()
        Dim ssValor = New ColumnHeader()
        Dim ssValorAdicional = New ColumnHeader()
        ssCategoria.Text = ""
        'ssCategoria.Width = 75
        ssEtiqueta.Text = ""
        'ssEtiqueta.Width = 125
        ssValor.Text = ""
        'ssValor.Width = 75
        ssValorAdicional.Text = ""
        'ssValorAdicional.Width = 75
        lstSS.Columns.Add(ssCategoria)
        lstSS.Columns.Add(ssEtiqueta)
        lstSS.Columns.Add(ssValor)
        lstSS.Columns.Add(ssValorAdicional)





    End Sub

    Private Function agregarCategoriaSS(ByVal categoria As String) As ListViewItem
        '--- Agregar un item de encabezado al listado 
        Dim lvi As New ListViewItem(categoria)
        lstSS.Items.Add(lvi)
        Return lvi
    End Function

    Private Sub agregarEtiqueta(ByVal etiqueta As String, ByVal valor As String, Optional ByVal valorAdicional As String = "")
        '--- Agregar un item de encabezado al listado 
        Dim lvi As New ListViewItem(etiqueta)
        lvi.SubItems.Add(valor)
        lvi.SubItems.Add(valorAdicional)
        lstSS.Items.Add(lvi)
    End Sub

    Private Sub lnkClientes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkClientes.Click
        Dim frmClientesConSaldo As New frmClientesSaldo
        frmClientesConSaldo.ShowDialog()
    End Sub

    Private Sub lnkCarga_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCarga.Click
        Dim frm_Carga As New frmConsultaCarga
        frm_Carga.chConfirmar.Visible = False
        frm_Carga.ShowDialog()
    End Sub

#End Region




    Private Sub LinkLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkLabel1.Click
        Me.Close()
    End Sub

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        Me.Close()
    End Sub

   
End Class