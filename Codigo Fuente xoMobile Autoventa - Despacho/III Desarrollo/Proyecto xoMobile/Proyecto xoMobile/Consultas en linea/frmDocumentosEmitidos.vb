Imports System.Data
Imports System.Threading
Imports Proyecto_xoMobile_Packs

Public Class frmDocumentosEmitidos

#Region " VARIABLES "
    '--- Capas
    Dim objDocumentoBL As New DocumentoBL
    Dim objUtil As New UtilitarioBL
    Dim objCliente As New ClienteBL

    '--- Tablas
    Dim dtNc, dtRecibos, dtFacturas, dtCambios, dtNA As New DataTable
    Dim dtRm As New DataTable
    '--- Datos Com
    Public v_com_cliente As New ClienteCO

    '--- Variables locales
    Dim idClienteAnula As String
    Dim idDocumento As String = ""
    Dim tipoAnulacion As Integer

    '--- Message box
    Dim msg As String
    Dim title As String
    Dim style As MsgBoxStyle
    Dim response As MsgBoxResult
#End Region

#Region " INICIALIZAR FORMULARIO "

    Private Sub frmDocumentosEmitidos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '*--- <DRIVER> ---------------------
        'Dim oCliente As New ClienteBL
        'id_glo_cliente = 55053668
        'id_glo_ruta = 157
        ''idCliente = 55053668
        'co_glo_despacho = False
        'xo_validaInventario = True
        'co_glo_aplicacion = "16"
        '*--- <DRIVER> ---------------------

        If v_com_cliente.codigo Is Nothing Then
            v_com_cliente.codigo = 0
        End If
        panClave.Visible = False
        inicializarListView()
        cargarListView()
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub cargarListView()
        crearListadoDeFacturas()
        crearListadoDeRecibos()
        crearListadoDeNotasCredito()
        crearListadoResumenMarcas()
        crearListadoCambios()
        crearListadoNotaAbono()
    End Sub

    Private Sub cargarListViewGeneral()
        crearListadoDeFacturasGeneral()
        crearListadoDeNotasCreditoGeneral()
    End Sub

    Private Sub crearListadoDeFacturas()
        Try

            lstFacturas.Items.Clear()

            'Obtener el listado de Facturas
            Dim dtal As New DataTable
            Dim oUtil As New UtilitarioBL
            dtFacturas = objDocumentoBL.consultarFacturas()
            If v_com_cliente.codigo <> 0 Then
                Dim dtFacturasCliente As New DataView
                dtFacturasCliente = dtFacturas.DefaultView
                dtFacturasCliente.RowFilter = "Codigo = " + v_com_cliente.codigo.ToString
                dtFacturas = dtFacturasCliente.ToTable()
            End If


            '--- Agregar filas a la lista

            For i As Integer = 0 To dtFacturas.Rows.Count - 1
                Dim drow As DataRow = dtFacturas.Rows(i)
                Dim lvi As New ListViewItem("")
                Dim ImporteDesto As Decimal
                lvi.SubItems.Add(drow("id_encFactura").ToString())
                lvi.SubItems.Add(drow("Codigo").ToString())
                lvi.SubItems.Add(Trim(drow("Cliente").ToString()))
                lvi.SubItems.Add(drow("Serie").ToString())
                lvi.SubItems.Add(drow("Numero").ToString())
                lvi.SubItems.Add(drow("Femi").ToString())
                lvi.SubItems.Add(drow("Hemi").ToString())
                lvi.SubItems.Add(FormatCurrency(drow("Importe").ToString(), 2))
                ImporteDesto = oUtil.isDecimal(drow("ImporteDesto").ToString)
                lvi.SubItems.Add(FormatCurrency(Math.Abs(ImporteDesto).ToString(), 2))
                lvi.SubItems.Add(drow("tipoDescuento").ToString())
                lvi.SubItems.Add(FormatCurrency(drow("Importe") - Math.Abs(ImporteDesto), 2))
                lvi.SubItems.Add(drow("seriefel").ToString())
                lvi.SubItems.Add(drow("numeroautorizacion").ToString())
                lvi.SubItems.Add(drow("preimpreso").ToString())
                lvi.SubItems.Add(drow("tTipo").ToString())
                lvi.SubItems.Add(drow("numeroacceso").ToString())
                lstFacturas.Items.Add(lvi)
                If drow("nImpresiones").ToString = "0" Then
                    lstFacturas.Items(i).ImageIndex = 2
                    lstFacturas.Items(i).BackColor = xoInfomat
                End If
                If drow("estado").ToString = "2" Then
                    lstFacturas.Items(i).ImageIndex = 1
                    lstFacturas.Items(i).BackColor = xoWarning
                End If
            Next
            lstFacturas.Items.Item(0).Selected() = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub crearListadoDeFacturasGeneral()
        Try


            'Obtener el listado de Facturas
            Dim dtal As New DataTable
            Dim oUtil As New UtilitarioBL
            dtFacturas = objDocumentoBL.consultarFacturas()
            If v_com_cliente.codigo <> 0 Then
                Dim dtNcCliente As New DataView
                dtNcCliente = dtNc.DefaultView
                dtNcCliente.RowFilter = "Codigo = " + v_com_cliente.codigo.ToString
                dtFacturas = dtNcCliente.ToTable()
            End If
            


            '--- Agregar filas a la lista

            For i As Integer = 0 To dtFacturas.Rows.Count - 1
                Dim drow As DataRow = dtFacturas.Rows(i)
                Dim lvi As New ListViewItem("")
                Dim ImporteDesto As Decimal
                lvi.SubItems.Add(drow("id_encFactura").ToString())
                lvi.SubItems.Add(drow("Codigo").ToString())
                lvi.SubItems.Add(Trim(drow("Cliente").ToString()))
                lvi.SubItems.Add(drow("Serie").ToString())
                lvi.SubItems.Add(drow("Numero").ToString())
                lvi.SubItems.Add(drow("Femi").ToString())
                lvi.SubItems.Add(drow("Hemi").ToString())
                lvi.SubItems.Add(FormatCurrency(drow("Importe").ToString(), 2))
                ImporteDesto = oUtil.isDecimal(drow("ImporteDesto").ToString)
                lvi.SubItems.Add(FormatCurrency(Math.Abs(ImporteDesto).ToString(), 2))
                lvi.SubItems.Add(drow("tipoDescuento").ToString())
                lvi.SubItems.Add(FormatCurrency(drow("Importe") - Math.Abs(ImporteDesto), 2))
                lvi.SubItems.Add(drow("seriefel").ToString())
                lvi.SubItems.Add(drow("numeroautorizacion").ToString())
                lvi.SubItems.Add(drow("preimpreso").ToString())
                lvi.SubItems.Add(drow("tTipo").ToString())
                lstFacturas.Items.Add(lvi)
                If drow("nImpresiones").ToString = "0" Then
                    lstFacturas.Items(i).ImageIndex = 2
                    lstFacturas.Items(i).BackColor = xoInfomat
                End If
                If drow("estado").ToString = "2" Then
                    lstFacturas.Items(i).ImageIndex = 1
                    lstFacturas.Items(i).BackColor = xoWarning
                End If
            Next
            lstFacturas.Items.Item(0).Selected() = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub crearListadoCambios()
        Try
            'Obtener el listado de Facturas
            Dim dtal As New DataTable
            Dim oUtil As New UtilitarioBL
            dtCambios = objDocumentoBL.consultarCambios()
            If v_com_cliente.codigo <> 0 Then
                Dim dtCambiosCliente As New DataView
                dtCambiosCliente = dtCambios.DefaultView
                dtCambiosCliente.RowFilter = "Codigo = " + v_com_cliente.codigo.ToString
                dtCambios = dtCambiosCliente.ToTable()
            End If


            '--- Agregar filas a la lista
            lstCambio.Items.Clear()
            For i As Integer = 0 To dtCambios.Rows.Count - 1
                Dim drow As DataRow = dtCambios.Rows(i)
                Dim lvi As New ListViewItem("")
                Dim ImporteDesto As Decimal
                lvi.SubItems.Add(drow("id_encFactura").ToString())
                lvi.SubItems.Add(drow("Codigo").ToString())
                lvi.SubItems.Add(Trim(drow("Cliente").ToString()))
                lvi.SubItems.Add(drow("Serie").ToString())
                lvi.SubItems.Add(drow("Numero").ToString())
                lvi.SubItems.Add(drow("Femi").ToString())
                lvi.SubItems.Add(drow("Hemi").ToString())
                lvi.SubItems.Add(FormatCurrency(drow("Importe").ToString(), 2))
                ImporteDesto = oUtil.isDecimal(drow("ImporteDesto").ToString)
                lvi.SubItems.Add(FormatCurrency(Math.Abs(ImporteDesto).ToString(), 2))
                lvi.SubItems.Add(drow("tipoDescuento").ToString())
                lvi.SubItems.Add(FormatCurrency(drow("Importe") - Math.Abs(ImporteDesto), 2))
                lstCambio.Items.Add(lvi)
                If drow("nImpresiones").ToString = "0" Then
                    lstCambio.Items(i).ImageIndex = 2
                    lstCambio.Items(i).BackColor = xoInfomat
                End If
                If drow("estado").ToString = "2" Then
                    lstCambio.Items(i).ImageIndex = 1
                    lstCambio.Items(i).BackColor = xoWarning
                End If
            Next
            lstCambio.Items.Item(0).Selected() = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub crearListadoDeRecibos()
        Try
            'Obtener el listado de REcibos

            dtRecibos = objDocumentoBL.consultarRecibos()

            If v_com_cliente.codigo <> 0 Then
                Dim dtRecibosCliente As New DataView
                dtRecibosCliente = dtRecibos.DefaultView
                dtRecibosCliente.RowFilter = "Codigo = " + v_com_cliente.codigo.ToString
                dtRecibos = dtRecibosCliente.ToTable()
            End If

            '--- Agregar filas a la lista
            lstRecibos.Items.Clear()
            For i As Integer = 0 To dtRecibos.Rows.Count - 1
                Dim drow As DataRow = dtRecibos.Rows(i)
                Dim lvi As New ListViewItem("")
                lvi.SubItems.Add(drow("id_encRecibo").ToString())
                lvi.SubItems.Add(drow("Codigo").ToString())
                lvi.SubItems.Add(Trim(drow("Cliente").ToString()))
                lvi.SubItems.Add(drow("Serie").ToString())
                lvi.SubItems.Add(drow("Numero").ToString())
                lvi.SubItems.Add(drow("Femi").ToString())
                lvi.SubItems.Add(drow("Hemi").ToString())
                lvi.SubItems.Add(FormatCurrency(drow("Importe").ToString(), 2))
                lvi.SubItems.Add(FormatCurrency(drow("saldo").ToString(), 2))
                lvi.SubItems.Add(drow("docPagado").ToString())
                If Trim(drow("doTipo").ToString()) = "ODV" Then
                    lvi.SubItems.Add("FACT")
                Else
                    lvi.SubItems.Add(drow("doTipo").ToString())
                End If
                lstRecibos.Items.Add(lvi)
                If drow("nImpresiones") = 0 Then
                    lstRecibos.Items(i).ImageIndex = 2
                    lstRecibos.Items(i).BackColor = xoInfomat
                End If
                If drow("estado") = 2 Then
                    lstRecibos.Items(i).ImageIndex = 1
                    lstRecibos.Items(i).BackColor = xoWarning
                End If
            Next
            lstRecibos.Items.Item(0).Selected() = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub crearListadoDeNotasCredito()

        Try
            dtNc = objDocumentoBL.consultarNotasDeCredito
            If v_com_cliente.codigo <> 0 Then
                Dim dtNcCliente As New DataView
                dtNcCliente = dtNc.DefaultView
                dtNcCliente.RowFilter = "Codigo = " + v_com_cliente.codigo.ToString
                dtNc = dtNcCliente.ToTable()
            End If

            '--- Agregar filas a la lista
            lstNotaCredito.Items.Clear()
            For i As Integer = 0 To dtNc.Rows.Count - 1
                Dim drow As DataRow = dtNc.Rows(i)
                Dim lvi As New ListViewItem("")
                Dim Importe As Decimal
                lvi.SubItems.Add(drow("id_encNc").ToString())
                lvi.SubItems.Add(drow("Codigo").ToString())
                lvi.SubItems.Add(Trim(drow("Cliente").ToString()))
                lvi.SubItems.Add(drow("Serie").ToString())
                lvi.SubItems.Add(drow("Numero").ToString())
                lvi.SubItems.Add(drow("Tipo").ToString())
                lvi.SubItems.Add(drow("Femi").ToString())
                lvi.SubItems.Add(drow("Hemi").ToString())
                Importe = drow("Importe")
                lvi.SubItems.Add(FormatCurrency(Math.Abs(Importe).ToString(), 2))
                lvi.SubItems.Add(drow("seriefel").ToString())
                lvi.SubItems.Add(drow("numeroautorizacion").ToString())
                lvi.SubItems.Add(drow("preimpreso").ToString())
                lvi.SubItems.Add(drow("tipoDevolucion").ToString())
                lstNotaCredito.Items.Add(lvi)
                If drow("nImpresiones") = 0 Then
                    lstNotaCredito.Items(i).ImageIndex = 2
                    lstNotaCredito.Items(i).BackColor = xoInfomat
                End If
                If drow("estado") = 2 Then
                    lstNotaCredito.Items(i).ImageIndex = 1
                    lstNotaCredito.Items(i).BackColor = xoWarning
                End If
            Next
            lstNotaCredito.Items.Item(0).Selected() = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub crearListadoNotaAbono()
        Try
            dtNA = objDocumentoBL.consultarNotaAbono
            If v_com_cliente.codigo <> 0 Then
                Dim dtNcCliente As New DataView
                dtNcCliente = dtNA.DefaultView
                dtNcCliente.RowFilter = "Codigo = " + v_com_cliente.codigo.ToString
                dtNA = dtNcCliente.ToTable()
            End If

            '--- Agregar filas a la lista
            lstNotaAbono.Items.Clear()
            For i As Integer = 0 To dtNA.Rows.Count - 1
                Dim drow As DataRow = dtNA.Rows(i)
                Dim lvi As New ListViewItem("")
                Dim Importe As Decimal
                lvi.SubItems.Add(drow("id_encNc").ToString())
                lvi.SubItems.Add(drow("Codigo").ToString())
                lvi.SubItems.Add(Trim(drow("Cliente").ToString()))
                lvi.SubItems.Add(drow("Serie").ToString())
                lvi.SubItems.Add(drow("Numero").ToString())
                lvi.SubItems.Add(drow("Tipo").ToString())
                lvi.SubItems.Add(drow("Femi").ToString())
                lvi.SubItems.Add(drow("Hemi").ToString())
                Importe = drow("Importe")
                lvi.SubItems.Add(FormatCurrency(Math.Abs(Importe).ToString(), 2))
                lvi.SubItems.Add(drow("seriefel").ToString())
                lvi.SubItems.Add(drow("numeroautorizacion").ToString())
                lvi.SubItems.Add(drow("preimpreso").ToString())
                lstNotaAbono.Items.Add(lvi)
                If drow("nImpresiones") = 0 Then
                    lstNotaAbono.Items(i).ImageIndex = 2
                    lstNotaAbono.Items(i).BackColor = xoInfomat
                End If
                If drow("estado") = 2 Then
                    lstNotaAbono.Items(i).ImageIndex = 1
                    lstNotaAbono.Items(i).BackColor = xoWarning
                End If
            Next
            lstNotaAbono.Items.Item(0).Selected() = True
        Catch ex As Exception
        End Try
    End Sub


    Private Sub crearListadoDeNotasCreditoGeneral()
     

        Try
            dtNc = objDocumentoBL.consultarNotasDeCredito
            If v_com_cliente.codigo <> 0 Then
                Dim dtNcCliente As New DataView
                dtNcCliente = dtNc.DefaultView
                dtNcCliente.RowFilter = "Codigo = " + v_com_cliente.codigo.ToString
                dtNc = dtNcCliente.ToTable()
            End If

            '--- Agregar filas a la lista
            lstNotaCredito.Items.Clear()
            For i As Integer = 0 To dtNc.Rows.Count - 1
                Dim drow As DataRow = dtNc.Rows(i)
                Dim lvi As New ListViewItem("")
                Dim Importe As Decimal
                lvi.SubItems.Add(drow("id_encNc").ToString())
                lvi.SubItems.Add(drow("Codigo").ToString())
                lvi.SubItems.Add(Trim(drow("Cliente").ToString()))
                lvi.SubItems.Add(drow("Serie").ToString())
                lvi.SubItems.Add(drow("Numero").ToString())
                lvi.SubItems.Add(drow("Tipo").ToString())
                lvi.SubItems.Add(drow("Femi").ToString())
                lvi.SubItems.Add(drow("Hemi").ToString())
                Importe = drow("Importe")
                lvi.SubItems.Add(FormatCurrency(Math.Abs(Importe).ToString(), 2))
                lvi.SubItems.Add(drow("serie").ToString())
                lvi.SubItems.Add(drow("numeroautorizacion").ToString())
                lvi.SubItems.Add(drow("preimpreso").ToString())
                lvi.SubItems.Add(drow("tipoDevolucion").ToString())
                lstNotaCredito.Items.Add(lvi)
                If drow("nImpresiones") = 0 Then
                    lstNotaCredito.Items(i).ImageIndex = 2
                    lstNotaCredito.Items(i).BackColor = xoInfomat
                End If
                If drow("estado") = 2 Then
                    lstNotaCredito.Items(i).ImageIndex = 1
                    lstNotaCredito.Items(i).BackColor = xoWarning
                End If
            Next
            lstNotaCredito.Items.Item(0).Selected() = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub crearListadoResumenMarcas()

        Try
            dtRm = objDocumentoBL.consultarResumenMarca
            If v_com_cliente.codigo <> 0 Then
                Dim dtRmCliente As New DataView
                dtRmCliente = dtNc.DefaultView
                dtRmCliente.RowFilter = "Codigo = " + v_com_cliente.codigo.ToString
                dtRm = dtRmCliente.ToTable()
            End If

            'lstResumenMarca.LargeImageList = imgMarcas   'Asociamos el listview con el imagelist para que muestre las imagenes en formato pequeño
            '--- Agregar filas a la lista
            lstResumenMarca.Items.Clear()

            For i As Integer = 0 To dtRm.Rows.Count - 1
                Dim drow As DataRow = dtRm.Rows(i)
                Dim lvi As New ListViewItem("")
                lvi.SubItems.Add(drow("idMarca").ToString())
                lvi.SubItems.Add(drow("desc_marca").ToString())
                lvi.SubItems.Add(drow("Total_litros").ToString())
                lvi.SubItems.Add(drow("importeSinIva").ToString())
                lstResumenMarca.Items.Add(lvi)


            Next
            'For i As Integer = 0 To lstResumenMarca.Items.Count - 1
            'Select Case lstResumenMarca.Items.Item(i).SubItems(1).Text
            '        Case 'CHA'
            'End Select
            'lstResumenMarca.Items(i).ImageIndex = i
            'Next

            lstResumenMarca.Items.Item(0).Selected() = True
        Catch ex As Exception
        End Try
    End Sub


#End Region

    '---- xoMobile 2.0
#Region " COMANDOS ANULAR DOCUMENTO "

    Private Sub cmdAnulaConReimpresion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnulaConReimpresion.Click
        Try
            tipoAnulacion = 1

            If Not xo_restringeAnulacion Then
                Select Case tabDocumentos.SelectedIndex
                    Case 0
                        seleccionoItem(lstFacturas, idDocumento)
                    Case 1
                        MsgBox("Reimpresion no aplica para recibos.")
                    Case 2
                        MsgBox("No se pueden anular Notas de Credito directamente.")
                End Select

            Else
                MsgBox("Se ha deshabilitado la anulacion de documentos.")
            End If
        Catch ex As Exception
            MsgBox("No hay un documento seleccionado.")
        End Try
    End Sub
    Private Sub cmdSoloAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSoloAnular.Click



        Cursor.Current = Cursors.WaitCursor
        Dim Fel As New Generador
        Try

            tipoAnulacion = 2
            If Not xo_restringeAnulacion Then
                Select Case tabDocumentos.SelectedIndex
                    Case 0
                        seleccionoItem(lstFacturas, idDocumento)
                    Case 1
                        seleccionoItem(lstRecibos, idDocumento)
                    Case 4
                        seleccionoItem(lstCambio, idDocumento)
                    Case 2
                        MsgBox("No se pueden anular Notas de Credito directamente.")
                End Select
            Else
                MsgBox("Se ha deshabilitado la anulacion de documentos.")
            End If
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            MsgBox("No hay un documento seleccionado.")
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    Private Function seleccionoItem(ByVal lstDocumentos As Windows.Forms.ListView, ByRef ref_idDocumento As String) As Boolean

        '--- Esta deberia de ser local
        Dim itemSelected = lstDocumentos.SelectedIndices(0)
        ref_idDocumento = lstDocumentos.Items(itemSelected).SubItems(1).Text
        idClienteAnula = lstDocumentos.Items(itemSelected).SubItems(2).Text

        '--- Valida si el documento ya fue anulado
        If lstDocumentos.Items(itemSelected).BackColor = xoWarning Then
            MsgBox("Este documento ya fue anulado")
            Return False
        End If

        '--- Si es recibo proviniente de ODV o ENV entonces no puede anular
        If (tabDocumentos.SelectedIndex = 1) And (lstDocumentos.Items(itemSelected).SubItems(11).Text <> "CXC") Then
            MsgBox("Para anular, debe hacerlo mediante el documento relacionado.")
            Return False
        End If
        '--- Confirma la anulacion del documento
        msg = "Realmente desea anular el documento: "
        msg = msg + Trim(lstDocumentos.Items(itemSelected).SubItems(4).Text) + "-" + lstDocumentos.Items(itemSelected).SubItems(5).Text
        msg = msg + ". emitido por " + lstDocumentos.Items(itemSelected).SubItems(8).Text + "."
        style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.YesNo
        response = MsgBox(msg, style, title)
        If response = MsgBoxResult.Yes Then
            

            Try

                noname()
                Return True


                'If (id_glo_fel = "X") Then
                'Else
                'noname()
                'Return True
                'End If

            Catch ex As Exception
                MsgBox(ex.Message)
                MsgBox("Debe de GENERARSE antes la Factura Electronica..")
                Return False
            End Try
        Else
            Return False
        End If
    End Function

    Private Function noname() As Boolean
        Cursor.Current = Cursors.WaitCursor


        Select Case tabDocumentos.SelectedIndex
            Case 0
                Dim itemSelected2 = Me.lstFacturas.SelectedIndices(0)
                Dim Fel As New Generador
                Dim objDocumento As New DocumentoBL
                Dim Factura As New documentoCO
                Factura = objDocumento.getFactura(dtFacturas.Rows(itemSelected2).Item("id_encFactura"))
                If (id_glo_fel = "X") Then
                    If (Len(Factura.serieFEL) > 0) Then
                        If (Fel.anulacion(Factura.serieFEL, Factura.preimpreso, Factura.nit, Factura.fechaEmision, "POR FALTA DE IMPRESION")) Then
                            anulaDocumento("Factura", idDocumento, )
                        End If
                    Else
                        anulaDocumento("Factura", idDocumento, )
                    End If
                Else
                    anulaDocumento("Factura", idDocumento, )
                End If
                

                'anulaDocumento("Factura", idDocumento, )

            Case 1
                Dim itemSelected = Me.lstRecibos.SelectedIndices(0)
                tabDocumentos.Enabled = True
                panClave.Visible = True
                txtClave.Text = ""
                txtClave.Focus()
                Cursor.Current = Cursors.Default
                Exit Function
            Case 4
                anulaDocumentoCambio("Cambio", idDocumento, )
        End Select
        cargarListView()
        Cursor.Current = Cursors.Default
    End Function

    Private Function noname2(ByVal lstDocumentos As Windows.Forms.ListView) As Boolean
        Cursor.Current = Cursors.WaitCursor
        Dim itemSelected2 = lstDocumentos.SelectedIndices(0)

        Select Case tabDocumentos.SelectedIndex
            Case 0

                Dim Fel As New Generador
                Dim objDocumento As New DocumentoBL
                Dim Factura As New documentoCO
                Factura = objDocumento.getFactura(dtFacturas.Rows(itemSelected2).Item("id_encFactura"))
                If (Len(Factura.serieFEL) > 0) Then
                    If (Fel.anulacion(Factura.serieFEL, Factura.preimpreso, Factura.nit, Factura.fechaEmision, "POR FALTA DE IMPRESION")) Then
                        anulaDocumento("Factura", idDocumento, )
                    End If
                End If

                'anulaDocumento("Factura", idDocumento, )

            Case 1
                Dim itemSelected = Me.lstRecibos.SelectedIndices(0)
                tabDocumentos.Enabled = True
                panClave.Visible = True
                txtClave.Text = ""
                txtClave.Focus()
                Cursor.Current = Cursors.Default
                Exit Function
            Case 4
                anulaDocumentoCambio("Cambio", idDocumento, )
        End Select
        cargarListView()
        Cursor.Current = Cursors.Default
    End Function

#End Region
#Region " CLAVE PARA ANULACION "
    Private Sub txtClave_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtClave.KeyPress
        Select Case e.KeyChar()
            Case ChrW(13)
                Cursor.Current = Cursors.WaitCursor
                Dim objUsuario As New UsuarioBl
                If objUsuario.login(txtClave.Text, "ANULACION") Then
                    anulaDocumento("Recibo", idDocumento)
                    panClave.Visible = False
                    panError.Visible = False
                    tabDocumentos.Enabled = True
                    menuDocumentos.MenuItems(1).Enabled = True
                    menuDocumentos.MenuItems(2).Enabled = True
                    cargarListView()
                Else
                    panError.Visible = True
                End If
                Cursor.Current = Cursors.Default
            Case ChrW(Keys.Escape)
                txtClave.Text = ""
                panError.Visible = False
                panClave.Visible = False
                Cursor.Current = Cursors.WaitCursor
                tabDocumentos.Enabled = True
                menuDocumentos.MenuItems(1).Enabled = True
                menuDocumentos.MenuItems(2).Enabled = True
        End Select
    End Sub
#End Region
#Region " ANULAR DOCUMENTO "
    Private Function anulaDocumento(ByVal tipoDocumento As String, ByVal idDocumento As String, Optional ByVal MotivoAnula As String = "") As Boolean

        '--- Instanciar objetos
        Dim objDocumento As New DocumentoBL

        Select Case tipoDocumento
            Case "Factura"
                'Se agrego esta validacion para garantizar que el documento exista o corregirlo siempre y cuando cumpla las condiciones del procedimiento.
                If (objDocumentoBL.existeFacturaRecibo(idDocumento, idClienteAnula)) Then
                    '---- PORQUE IDCLIENTE
                    If objDocumento.anularFactura(idDocumento, idClienteAnula, tipoAnulacion, MotivoAnula) Then

                        MsgBox("El documento ha sido anulado.")
                    Else
                        MsgBox("Ocurrio un error al anular el documento.")
                    End If
                Else
                    If objDocumento.anularFactura2(idDocumento, idClienteAnula, "2", MotivoAnula) Then
                        MsgBox("El documento ha sido anulado.")
                    Else
                        MsgBox("Ocurrio un error al anular el documento, No existe recibo")
                    End If
                End If
            Case "Recibo"
                If response = MsgBoxResult.Yes Then
                    If objDocumentoBL.anularRecibo(idDocumento, v_com_cliente, MotivoAnula) Then
                        MsgBox("El documento ha sido anulado.")
                    Else
                        MsgBox("Ocurrio un error al anular el documento.")
                    End If
                End If
        End Select
        Return True
    End Function

    Private Function anulaDocumentoCambio(ByVal tipoDocumento As String, ByVal idDocumento As String, Optional ByVal MotivoAnula As String = "") As Boolean

        '--- Instanciar objetos
        Dim objDocumento As New DocumentoBL
        Select Case tipoDocumento
            Case "Cambio"
                '---- PORQUE IDCLIENTE
                If objDocumento.anularCambio(idDocumento, idClienteAnula, tipoAnulacion, MotivoAnula) Then
                    MsgBox("El documento ha sido anulado.")
                Else
                    MsgBox("Ocurrio un error al anular el documento.")
                End If
        End Select
        Return True
    End Function
#End Region
#Region " METODOS UI "

    Private Sub inicializarListView()

        '---Factura
        Dim imgf = New ColumnHeader()
        Dim idF = New ColumnHeader()
        Dim Codigof = New ColumnHeader()
        Dim Clientef = New ColumnHeader()
        Dim Serief = New ColumnHeader()
        Dim Numerof = New ColumnHeader()
        Dim Femif = New ColumnHeader()
        Dim Hemif = New ColumnHeader()
        Dim Importef = New ColumnHeader()
        Dim Descuentof = New ColumnHeader()
        Dim tipoDescuentof = New ColumnHeader()
        Dim Totalf = New ColumnHeader()
        Dim serieFEL = New ColumnHeader()
        Dim numeroFEL = New ColumnHeader()
        Dim uidFEL = New ColumnHeader()
        Dim Tipof = New ColumnHeader()


        '---Recibo
        Dim imgr = New ColumnHeader()
        Dim idR = New ColumnHeader()
        Dim Codigor = New ColumnHeader()
        Dim Clienter = New ColumnHeader()
        Dim Serier = New ColumnHeader()
        Dim Numeror = New ColumnHeader()
        Dim Femir = New ColumnHeader()
        Dim Hemir = New ColumnHeader()
        Dim Importer = New ColumnHeader()
        Dim saldo = New ColumnHeader()
        Dim docPagador = New ColumnHeader()
        Dim doTipor = New ColumnHeader()

        '--- NC
        Dim imgn = New ColumnHeader()
        Dim idN = New ColumnHeader()
        Dim Codigon = New ColumnHeader()
        Dim Clienten = New ColumnHeader()
        Dim Serien = New ColumnHeader()
        Dim Numeron = New ColumnHeader()
        Dim Femin = New ColumnHeader()
        Dim Hemin = New ColumnHeader()
        Dim Importen = New ColumnHeader()
        Dim Tipon = New ColumnHeader()
        Dim serieFELNC = New ColumnHeader()
        Dim numeroFELNC = New ColumnHeader()
        Dim uidFELNC = New ColumnHeader()
        Dim tipoDevNC = New ColumnHeader()

        '--- NOTAS DE ABONO
        Dim imgna = New ColumnHeader()
        Dim idNa = New ColumnHeader()
        Dim Codigona = New ColumnHeader()
        Dim Clientena = New ColumnHeader()
        Dim Seriena = New ColumnHeader()
        Dim Numerona = New ColumnHeader()
        Dim Femina = New ColumnHeader()
        Dim Hemina = New ColumnHeader()
        Dim Importena = New ColumnHeader()
        Dim Tipona = New ColumnHeader()
        Dim serieFELNCa = New ColumnHeader()
        Dim numeroFELNCa = New ColumnHeader()
        Dim uidFELNCa = New ColumnHeader()

        '-- RESUMEN MARCA

        Dim imgRm = New ColumnHeader()
        Dim marRm = New ColumnHeader()
        Dim dmarRm = New ColumnHeader()
        Dim litrosRm = New ColumnHeader()
        Dim importeRm = New ColumnHeader()

        '-- RESUMEN CAMBIO
        '---Factura
        Dim imgc = New ColumnHeader()
        Dim idc = New ColumnHeader()
        Dim Codigoc = New ColumnHeader()
        Dim Clientec = New ColumnHeader()
        Dim Seriec = New ColumnHeader()
        Dim Numeroc = New ColumnHeader()
        Dim Femic = New ColumnHeader()
        Dim Hemic = New ColumnHeader()
        Dim Importec = New ColumnHeader()
        Dim Descuentoc = New ColumnHeader()
        Dim tipoDescuentoc = New ColumnHeader()
        Dim Totalc = New ColumnHeader()

        imgf.Text = ""
        idF.Text = "id" '0
        Codigof.Text = "Codigo" '0
        Clientef.Text = "Cliente" '1
        Serief.Text = "Serie"
        Numerof.Text = "Numero" '3
        Femif.Text = "F.Emi" '4
        Hemif.Text = "H.Emit"
        Importef.Text = "Importe"
        Descuentof.Text = "Descuento"
        tipoDescuentof.Text = "T.Descuento"
        Totalf.Text = "Total"
        serieFEL.Text = "SerieFEL"
        numeroFEL.Text = "NumeroFEL"
        uidFEL.Text = "UID"
        Tipof.Text = "TIPO"



        imgr.Text = ""
        idR.Text = "id" '0
        Codigor.Text = "Codigo" '0
        Clienter.Text = "Cliente" '1
        Serier.Text = "Serie"
        Numeror.Text = "Numero" '3
        Femir.Text = "F.Emi" '4
        Hemir.Text = "H.Emit"
        Importer.Text = "Importe"
        saldo.Text = "Saldo"
        docPagador.Text = "Do.Pagado"
        doTipor.Text = "Do.Origen"


        'TITULOS NOTAS DE CREDITO
        imgn.Text = ""
        idN.Text = "id" '0
        Codigon.Text = "Codigo" '0
        Clienten.Text = "Cliente" '1
        Serien.Text = "Serie"
        Numeron.Text = "Numero" '3
        Femin.Text = "F.Emi" '4
        Hemin.Text = "H.Emit"
        Importen.Text = "Importe"
        Tipon.Text = "Tipo"
        serieFELNC.Text = "SerieFEL"
        numeroFELNC.Text = "NumeroFEL"
        uidFELNC.Text = "UID"
        tipoDevNC.Text = "TIPO"

        'TITULOS NOTAS DE ABONO
        imgna.Text = ""
        idNa.Text = "id" '0
        Codigona.Text = "Codigo" '0
        Clientena.Text = "Cliente" '1
        Seriena.Text = "Serie"
        Numerona.Text = "Numero" '3
        Femina.Text = "F.Emi" '4
        Hemina.Text = "H.Emit"
        Importena.Text = "Importe"
        Tipona.Text = "Tipo"
        serieFELNCa.Text = "SerieFEL"
        numeroFELNCa.Text = "NumeroFEL"
        uidFELNCa.Text = "UID"

        imgRm.Text = ""
        marRm.Text = "Codigo" '0
        dmarRm.Text = "Marca" '1
        litrosRm.Text = "Litros"
        importeRm.Text = "Importe" '3

        ''--- Titulos cambio
        imgc.Text = ""
        idc.Text = "id" '0
        Codigoc.Text = "Codigo" '0
        Clientec.Text = "Cliente" '1
        Seriec.Text = "Serie"
        Numeroc.Text = "Numero" '3
        Femic.Text = "F.Emi" '4
        Hemic.Text = "H.Emit"
        Importec.Text = "Importe"
        Descuentoc.Text = "Descuento"
        tipoDescuentoc.Text = "T.Descuento"
        Totalc.Text = "Total"

        lstFacturas.Columns.Add(imgf)
        lstFacturas.Columns.Add(idF)
        lstFacturas.Columns.Add(Codigof)
        lstFacturas.Columns.Add(Clientef)
        lstFacturas.Columns.Add(Serief)
        lstFacturas.Columns.Add(Numerof)
        lstFacturas.Columns.Add(Femif)
        lstFacturas.Columns.Add(Hemif)
        lstFacturas.Columns.Add(Importef)
        lstFacturas.Columns.Add(Descuentof)
        lstFacturas.Columns.Add(tipoDescuentof)
        lstFacturas.Columns.Add(Totalf)
        lstFacturas.Columns.Add(serieFEL)
        lstFacturas.Columns.Add(numeroFEL)
        lstFacturas.Columns.Add(uidFEL)
        lstFacturas.Columns.Add(Tipof)


        lstRecibos.Columns.Add(imgr)
        lstRecibos.Columns.Add(idR)
        lstRecibos.Columns.Add(Codigor)
        lstRecibos.Columns.Add(Clienter)
        lstRecibos.Columns.Add(Serier)
        lstRecibos.Columns.Add(Numeror)
        lstRecibos.Columns.Add(Femir)
        lstRecibos.Columns.Add(Hemir)
        lstRecibos.Columns.Add(Importer)
        lstRecibos.Columns.Add(saldo)
        lstRecibos.Columns.Add(docPagador)
        lstRecibos.Columns.Add(doTipor)

        'LISTA DE DOCUMENTOS NOTAS DE CREDITO
        lstNotaCredito.Columns.Add(imgn)
        lstNotaCredito.Columns.Add(idN)
        lstNotaCredito.Columns.Add(Codigon)
        lstNotaCredito.Columns.Add(Clienten)
        lstNotaCredito.Columns.Add(Serien)
        lstNotaCredito.Columns.Add(Numeron)
        lstNotaCredito.Columns.Add(Tipon)
        lstNotaCredito.Columns.Add(Femin)
        lstNotaCredito.Columns.Add(Hemin)
        lstNotaCredito.Columns.Add(Importen)
        lstNotaCredito.Columns.Add(serieFELNC)
        lstNotaCredito.Columns.Add(numeroFELNC)
        lstNotaCredito.Columns.Add(uidFELNC)
        lstNotaCredito.Columns.Add(tipoDevNC)

        'LISTA DE DOCUMENTOS NOTA DE ABONO
        lstNotaAbono.Columns.Add(imgna)
        lstNotaAbono.Columns.Add(idNa)
        lstNotaAbono.Columns.Add(Codigona)
        lstNotaAbono.Columns.Add(Clientena)
        lstNotaAbono.Columns.Add(Seriena)
        lstNotaAbono.Columns.Add(Numerona)
        lstNotaAbono.Columns.Add(Tipona)
        lstNotaAbono.Columns.Add(Femina)
        lstNotaAbono.Columns.Add(Hemina)
        lstNotaAbono.Columns.Add(Importena)
        lstNotaAbono.Columns.Add(serieFELNCa)
        lstNotaAbono.Columns.Add(numeroFELNCa)
        lstNotaAbono.Columns.Add(uidFELNCa)

        lstResumenMarca.Columns.Add(imgRm)
        lstResumenMarca.Columns.Add(marRm)
        lstResumenMarca.Columns.Add(dmarRm)
        lstResumenMarca.Columns.Add(litrosRm)
        lstResumenMarca.Columns.Add(importeRm)

        lstCambio.Columns.Add(imgc)
        lstCambio.Columns.Add(idc)
        lstCambio.Columns.Add(Codigoc)
        lstCambio.Columns.Add(Clientec)
        lstCambio.Columns.Add(Seriec)
        lstCambio.Columns.Add(Numeroc)
        lstCambio.Columns.Add(Femic)
        lstCambio.Columns.Add(Hemic)
        lstCambio.Columns.Add(Importec)
        lstCambio.Columns.Add(Descuentoc)
        lstCambio.Columns.Add(tipoDescuentoc)
        lstCambio.Columns.Add(Totalc)

    End Sub

    Private Sub panLogin_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panError.Paint
        objUtil.paintPannel(e, panClave)
    End Sub

    Private Sub panResultLogin_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        objUtil.paintPannel(e, panError)
    End Sub

    Private Sub panLoginError_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panError.Paint
        objUtil.paintPannel(e, panError)
    End Sub

    Private Sub cmdSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalir.Click
        Me.Close()
    End Sub

#End Region

    '---- xoMobile 1.0
#Region " Impresion de documentos emitidos "

    Private Sub cmdImprimeDocto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImprimeDocto.Click
        Cursor.Current = Cursors.WaitCursor
        Dim objImpresion As New ImpresionBL
        Select Case tabDocumentos.SelectedIndex
            Case 0
                If Me.lstFacturas.SelectedIndices.Count <= 0 Then
                    MsgBox("No hay un documento seleccionado.")
                    Cursor.Current = Cursors.Default
                    Return
                End If
                Dim itemSelected = Me.lstFacturas.SelectedIndices(0)
                If lstFacturas.Items(itemSelected).BackColor = xoInfomat Then
                    'Regla 1 - La factura debe tener el registro del recibo
                    'Regla 2 - El recibo debe tener el idFactura en el campo idEncFactura
                    If (id_glo_fel = "X") Then
                        If (objDocumentoBL.existeFacturaRecibo(dtFacturas.Rows(itemSelected).Item("id_encFactura"), dtFacturas.Rows(itemSelected).Item("codigo"))) Then

                            If (id_glo_sociedad = 4000) Then
                                If objImpresion.imprimeFactura(dtFacturas.Rows(itemSelected).Item("id_encFactura"), dtFacturas.Rows(itemSelected).Item("idEncRecibo").ToString(), True) Then
                                    objDocumentoBL.numeroImpresiones(dtFacturas.Rows(itemSelected).Item("id_encFactura"), "FACTURA", "1")
                                End If
                            Else
                                If objImpresion.imprimeFacturaLevuni(dtFacturas.Rows(itemSelected).Item("id_encFactura"), dtFacturas.Rows(itemSelected).Item("idEncRecibo").ToString(), True) Then
                                    objDocumentoBL.numeroImpresiones(dtFacturas.Rows(itemSelected).Item("id_encFactura"), "FACTURA", "1")
                                End If
                            End If

                        Else
                            MessageBox.Show("Verificar el documento no se pudo imprimir..")
                        End If
                    Else
                        If (objDocumentoBL.existeFacturaRecibo(dtFacturas.Rows(itemSelected).Item("id_encFactura"), dtFacturas.Rows(itemSelected).Item("codigo"))) Then
                            If objImpresion.imprimeFacturaSINFEL(dtFacturas.Rows(itemSelected).Item("id_encFactura"), dtFacturas.Rows(itemSelected).Item("idEncRecibo").ToString(), True) Then
                                objDocumentoBL.numeroImpresiones(dtFacturas.Rows(itemSelected).Item("id_encFactura"), "FACTURA", "1")
                            End If
                        Else
                            MessageBox.Show("Verificar el documento no se pudo imprimir..")
                        End If
                    End If
                    
                    'De lo contrario 
                    'Verificar si existe el recibo y el idEncFactura es igual a 0 y el monto corresponda a la factura y el recibo este en estado (1)
                    'Actualize la referencia del encabezado de la factura en el IDFactura.

                    'Si no creo el recibo o no existe debe anular solo la factura y la nota de credito si existe.
                Else
                    MsgBox("No se puede volver a imprimir.")
                    Cursor.Current = Cursors.Default
                    Return
                End If
            Case 1
                If Me.lstRecibos.SelectedIndices.Count <= 0 Then
                    MsgBox("No hay un documento seleccionado.")
                    Cursor.Current = Cursors.Default
                    Return
                End If
                Dim itemSelected = Me.lstRecibos.SelectedIndices(0)
                'If lstRecibos.Items(itemSelected).BackColor = xoInfomat Then
                If objImpresion.imprimeRecibo(dtRecibos.Rows(itemSelected).Item("id_encRecibo"), True) Then
                    objDocumentoBL.numeroImpresiones(dtRecibos.Rows(itemSelected).Item("id_encRecibo"), "RECIBO", "1")
                End If
                'Else
                'MsgBox("No se puede volver a imprimir.")
                Cursor.Current = Cursors.Default
                Return
                'End If
            Case 2
                If Me.lstNotaCredito.SelectedIndices.Count <= 0 Then
                    MsgBox("No hay un documento seleccionado.")
                    Cursor.Current = Cursors.Default
                    Return
                End If
                Dim itemSelected = Me.lstNotaCredito.SelectedIndices(0)
                If lstNotaCredito.Items(itemSelected).BackColor = xoInfomat Then
                    Dim docAsociado As String
                    Dim Recibo As New documentoCO
                    Dim Factura As New documentoCO
                    Dim Cliente As New ClienteCO
                    Dim objDocumento As New DocumentoBL
                    Recibo = objDocumento.getRecibo(dtRecibos.Rows(itemSelected).Item("id_encRecibo"))
                    Cliente = objCliente.getDetalleDelCliente(Recibo.idCliente)

                    Try
                        If dtNc.Rows(itemSelected).Item("idEncFactura").ToString() <> "" Then
                            Factura = objDocumento.getFactura(dtNc.Rows(itemSelected).Item("idEncFactura"))
                            docAsociado = Factura.serie + "-" + Factura.numero
                        Else
                            Recibo = objDocumento.getRecibo(dtNc.Rows(itemSelected).Item("idEncRecibo"))
                            docAsociado = Factura.serie + "-" + Factura.numero
                        End If
                    Catch ex As Exception

                    End Try
                    

                    If (id_glo_fel = "X") Then
                        If objImpresion.ImprimeNotaCredito(dtNc.Rows(itemSelected).Item("id_encNc"), True, 0) Then
                            objDocumentoBL.numeroImpresiones(dtNc.Rows(itemSelected).Item("id_encNc"), "NC", "1")
                        End If
                    Else
                        If objImpresion.ImprimeNotaCreditoSINFEL(dtNc.Rows(itemSelected).Item("id_encNc"), True, 0) Then
                            objDocumentoBL.numeroImpresiones(dtNc.Rows(itemSelected).Item("id_encNc"), "NC", "1")
                        End If
                    End If
                Else
                    MsgBox("No se puede volver a imprimir.")
                    Cursor.Current = Cursors.Default
                    Return
                End If
            Case 5
                If Me.lstNotaAbono.SelectedIndices.Count <= 0 Then
                    MsgBox("No hay un documento seleccionado.")
                    Cursor.Current = Cursors.Default
                    Return
                End If
                Dim itemSelected = Me.lstNotaAbono.SelectedIndices(0)
                If lstNotaAbono.Items(itemSelected).BackColor = xoInfomat Then
                    Dim docAsociado As String
                    Dim Recibo As New documentoCO
                    Dim Factura As New documentoCO
                    Dim Cliente As New ClienteCO
                    Dim objDocumento As New DocumentoBL
                    Recibo = objDocumento.getRecibo(dtRecibos.Rows(itemSelected).Item("id_encRecibo"))
                    Cliente = objCliente.getDetalleDelCliente(Recibo.idCliente)

                    Try
                        If dtNA.Rows(itemSelected).Item("idEncFactura").ToString() <> "" Then
                            Factura = objDocumento.getFactura(dtNA.Rows(itemSelected).Item("idEncFactura"))
                            docAsociado = Factura.serie + "-" + Factura.numero
                        Else
                            Recibo = objDocumento.getRecibo(dtNA.Rows(itemSelected).Item("idEncRecibo"))
                            docAsociado = Factura.serie + "-" + Factura.numero
                        End If
                    Catch ex As Exception

                    End Try


                    If (id_glo_fel = "X") Then
                        If objImpresion.ImprimeNotaCredito(dtNA.Rows(itemSelected).Item("id_encNc"), True, 0) Then
                            objDocumentoBL.numeroImpresiones(dtNA.Rows(itemSelected).Item("id_encNc"), "NC", "1")
                        End If
                    Else
                        If objImpresion.ImprimeNotaCreditoSINFEL(dtNA.Rows(itemSelected).Item("id_encNc"), True, 0) Then
                            objDocumentoBL.numeroImpresiones(dtNc.Rows(itemSelected).Item("id_encNc"), "NC", "1")
                        End If
                    End If
                Else
                    MsgBox("No se puede volver a imprimir.")
                    Cursor.Current = Cursors.Default
                    Return
                End If
        End Select
        Cursor.Current = Cursors.Default
        cargarListView()
    End Sub

    Private Sub cmdImprimeReporte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImprimeReporte.Click
        Try
            Dim starter As New ThreadStart(AddressOf Me.imprimir)
            Dim t As New Thread(starter)
            t.IsBackground = True
            t.Start()
        Catch ex As Exception
            MsgBox(ex.Message())
        End Try
    End Sub

    Private Sub imprimir()
        Dim objReport As New printReporte
        Dim itemCount As Integer
        itemCount = lstFacturas.Items.Count + lstNotaCredito.Items.Count + lstNotaCredito.Items.Count + lstCambio.Items.Count
        If itemCount > 0 Then
            '--- Reporte de anulados
            Dim objImpresion As New Impresion
            If objImpresion.ConfirmaImpresion("Documentos emitidos.") Then
                objReport.documentosEmitidos(1, v_com_cliente.codigo, True)
                objReport.documentosEmitidos(0, v_com_cliente.codigo, True)
            End If
        Else
            MsgBox("No hay informacion para imprimir.")
        End If

    End Sub

#End Region

    
    Private Sub cmdImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImprimir.Click

    End Sub

    Private Sub frmDocumentosEmitidos_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
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
        itemCount = lstFacturas.Items.Count + lstNotaCredito.Items.Count + lstNotaCredito.Items.Count
        If itemCount > 0 Then

            '--- Reporte de anulados
            Dim objImpresion As New Impresion
            If objImpresion.ConfirmaImpresion("Documentos emitidos.") Then
                objReport.resumenMarcas(1, v_com_cliente.codigo, True)
            End If
        Else
            MsgBox("No hay informacion para imprimir.")
        End If

    End Sub

    Private Sub imprimirSINFEL()
        Dim objReport As New printReporte
        Dim itemCount As Integer
        itemCount = lstFacturas.Items.Count + lstNotaCredito.Items.Count + lstNotaCredito.Items.Count
        If itemCount > 0 Then

            '--- Reporte de anulados
            Dim objImpresion As New Impresion
            If objImpresion.ConfirmaImpresion("Documentos emitidos.") Then
                objReport.resumenSINFEL(1, v_com_cliente.codigo, True)
            End If
        Else
            MsgBox("No hay informacion para imprimir.")
        End If

    End Sub


    Private Sub lstResumenMarca_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub lstRecibos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstRecibos.SelectedIndexChanged

    End Sub

  
    Private Sub MenuFEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuFEL.Click
        Cursor.Current = Cursors.WaitCursor
        Try
            tipoAnulacion = 2
            Select Case tabDocumentos.SelectedIndex
                Case 0
                    Dim Fel As New Generador
                    Dim itemSelected = Me.lstFacturas.SelectedIndices(0)
                    v_com_cliente = objCliente.getDetalleDelCliente(Me.lstFacturas.Items(itemSelected).SubItems(2).Text)
                    If (dtFacturas.Rows(itemSelected).Item("tTipo") = "ZTAP") Then
                        Try
                            Cursor.Current = Cursors.WaitCursor
                            ' ********** Generación Factura Electronica Guatefacturas  **********
                            If (id_glo_fel = "X") Then

                                If (Fel.generador3(dtFacturas.Rows(itemSelected).Item("id_encFactura"), v_com_cliente)) Then
                                    MessageBox.Show("Documento Generado Correctamente ")
                                Else
                                    MessageBox.Show("Error al generar documento ")
                                End If
                            End If

                            ' ******** FIN DE LA FUNCION *************
                            cargarListViewGeneral()
                            Cursor.Current = Cursors.Default
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try
                    End If

                    If (dtFacturas.Rows(itemSelected).Item("tTipo") = "ZTAE") Then
                        Try
                            Cursor.Current = Cursors.WaitCursor
                            ' ********** Generación Factura Electronica Guatefacturas  **********
                            If (id_glo_fel = "X") Then

                                If (Fel.generadorFaltante(dtFacturas.Rows(itemSelected).Item("id_encFactura"), v_com_cliente)) Then
                                    MessageBox.Show("Documento Generado Correctamente ")
                                Else
                                    MessageBox.Show("Error al generar documento ")
                                End If
                            End If

                            ' ******** FIN DE LA FUNCION *************
                            cargarListViewGeneral()
                            Cursor.Current = Cursors.Default
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try
                    End If




            End Select



            Select Case tabDocumentos.SelectedIndex
                '-- EN CASO QUE SEA UNA FACTURA PARA OPERAR DOCUMENTO --
                Case 0
                    Dim Fel As New Generador
                    Dim itemSelected = Me.lstFacturas.SelectedIndices(0)
                    v_com_cliente = objCliente.getDetalleDelCliente(Me.lstFacturas.Items(itemSelected).SubItems(2).Text)
                    Try
                        Cursor.Current = Cursors.WaitCursor
                        ' ********** Generación Factura Electronica Guatefacturas  **********
                        If (id_glo_fel = "X") Then
                            If objDocumentoBL.validaDocumentoFEL(dtFacturas.Rows(itemSelected).Item("id_encFactura")) Then
                                MessageBox.Show("ESTE DOCUMENTO YA FUE OPERADO EN FEL")
                            Else
                                If (Fel.generador3(dtFacturas.Rows(itemSelected).Item("id_encFactura"), v_com_cliente)) Then
                                    MessageBox.Show("Documento Generado Correctamente ")
                                Else
                                    MessageBox.Show("Error al generar documento ")
                                End If
                            End If

                        End If
                        ' ******** FIN DE LA FUNCION *************
                        cargarListViewGeneral()
                        Cursor.Current = Cursors.Default
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                    '-- EN CASO QUE SEA UNA NOTA DE CREDITO PARA OPERAR DOCUMENTO --
                Case 2
                    Dim Fel As New Generador
                    Dim itemSelected = Me.lstNotaCredito.SelectedIndices(0)

                    v_com_cliente = objCliente.getDetalleDelCliente(Me.lstNotaCredito.Items(itemSelected).SubItems(2).Text)

                    Try
                        Cursor.Current = Cursors.WaitCursor
                        ' ********** Generación Factura Electronica Guatefacturas  **********

                        If objDocumentoBL.validaDocumentoFELNC(dtNc.Rows(itemSelected).Item("id_encNc")) Then
                            MessageBox.Show("ESTE DOCUMENTO YA FUE OPERADO EN FEL")
                        Else

                            If (dtNc.Rows(itemSelected).Item("tipoDevolucion") = 6) Then
                                If (Fel.generadorNCCXCDPPREF(dtNc.Rows(itemSelected).Item("id_encNc"), v_com_cliente)) Then
                                    MessageBox.Show("Documento Generado Correctamente DPP")
                                Else
                                    MessageBox.Show("Error al generar el DPP")
                                End If
                            Else
                                If (Fel.generadorNCRef(dtNc.Rows(itemSelected).Item("id_encNc"), v_com_cliente)) Then
                                    MessageBox.Show("Documento Generado Correctamente DPP")
                                Else
                                    MessageBox.Show("Error al generar el DPP")
                                End If
                            End If
                        End If


                        ' ******** FIN DE LA FUNCION *************
                        cargarListViewGeneral()
                        Cursor.Current = Cursors.Default
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                    '-- EN CASO QUE SEA UNA NOTA DE ABONO PARA OPERAR DOCUMENTO --
                Case 5
                    Dim Fel As New Generador
                    Dim itemSelected = Me.lstNotaAbono.SelectedIndices(0)
                    v_com_cliente = objCliente.getDetalleDelCliente(Me.lstNotaAbono.Items(itemSelected).SubItems(2).Text)
                    Try
                        Cursor.Current = Cursors.WaitCursor
                        ' ********** Generación Factura Electronica Guatefacturas  **********
                        If (id_glo_fel = "X") Then
                            If (Fel.generadorNABONOREF(dtNA.Rows(itemSelected).Item("id_encNc"), v_com_cliente)) Then
                                MessageBox.Show("Documento Generado Correctamente ")
                            Else
                                MessageBox.Show("Error al generar documento ")
                            End If
                        End If
                        ' ******** FIN DE LA FUNCION *************
                        cargarListViewGeneral()
                        Cursor.Current = Cursors.Default
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
            End Select
            
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            MsgBox("No hay un documento seleccionado.")
            Cursor.Current = Cursors.Default
        End Try

        
    End Sub

    Public Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
        ' by making Generator static, we preserve the same instance '
        ' (i.e., do not create new instances with the same seed over and over) '
        ' between calls '
        Static Generator As System.Random = New System.Random()
        Return Generator.Next(Min, Max)
    End Function

    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        Cursor.Current = Cursors.WaitCursor
        Dim objImpresion As New ImpresionBL
        Dim NumeroContingencia As Integer
        Dim NumeroContingenciaNC As Integer
        Dim contingencia As String = ""
        Dim nRuta As String = id_glo_codRuta
        Dim rutaContingencia As String = ""
        Dim tipo_receptor As String = ""
        Dim idreceptor As String = ""
        Dim ejecutar As Boolean = False
        rutaContingencia = nRuta.PadLeft(3, "0")
        Select Case tabDocumentos.SelectedIndex
            Case 0
                NumeroContingencia = 0
                If Me.lstFacturas.SelectedIndices.Count <= 0 Then
                    MsgBox("No hay un documento seleccionado.")
                    Cursor.Current = Cursors.Default
                    Return
                End If
                Dim itemSelected = Me.lstFacturas.SelectedIndices(0)

                'Regla 1 - La factura debe tener el registro del recibo
                'Regla 2 - El recibo debe tener el idFactura en el campo idEncFactura
                If (objDocumentoBL.existeFacturaRecibo(dtFacturas.Rows(itemSelected).Item("id_encFactura"), dtFacturas.Rows(itemSelected).Item("codigo"))) Then
                    If objDocumentoBL.validaDocumentoFEL(dtFacturas.Rows(itemSelected).Item("id_encFactura")) Then
                        MessageBox.Show("ESTE DOCUMENTO YA FUE OPERADO EN FEL")
                    Else
                        If objDocumentoBL.validaDocumentoFEL(dtFacturas.Rows(itemSelected).Item("id_encFactura")) Then
                            If (objDocumentoBL.validaContingenciaFACT(dtFacturas.Rows(itemSelected).Item("id_encFactura"))) Then
                                MessageBox.Show("NO SE PUEDE GENERAR DOCUMENTO DE CONTINGENCIA YA QUE ESTE TIENE FEL")
                            Else
                                'Consultar número de contingencia disponible
                                contingencia = objDocumentoBL.getContigencia(dtFacturas.Rows(itemSelected).Item("id_encFactura"))
                                
                                v_com_cliente = objCliente.getDetalleDelCliente(dtFacturas.Rows(itemSelected).Item("Codigo"))


                                Try
                                    Dim texto As String = ""
                                    texto = Replace(v_com_cliente.nit, "-", "")
                                    v_com_cliente.nit = Replace(texto, "/", "")
                                Catch ex As Exception
                                    v_com_cliente.nit = "CF"
                                End Try


                                If dtFacturas.Rows(itemSelected).Item("Importe") > id_glo_total_cf Then
                                    If v_com_cliente.nit <> "CF" Then
                                        tipo_receptor = 4
                                        idreceptor = v_com_cliente.nit
                                    Else
                                        If Len(v_com_cliente.numeroDi) > 0 Then
                                            If v_com_cliente.numeroDi.Substring(0, 1) <> "P" Then
                                                tipo_receptor = 2
                                                idreceptor = v_com_cliente.numeroDi
                                            Else
                                                Try
                                                    Dim quitarP As String = ""
                                                    quitarP = Replace(v_com_cliente.numeroDi, "P", "")
                                                    tipo_receptor = 3
                                                    idreceptor = quitarP
                                                Catch ex As Exception
                                                    tipo_receptor = 3
                                                    idreceptor = v_com_cliente.numeroDi
                                                End Try
                                            End If

                                        Else
                                            tipo_receptor = 4
                                            idreceptor = v_com_cliente.nit
                                        End If
                                    End If
                                Else
                                    tipo_receptor = 4
                                    idreceptor = v_com_cliente.nit
                                End If


                                If ejecutar = True Then
                                    objDocumentoBL.actualizarConingencia(dtFacturas.Rows(itemSelected).Item("id_encFactura"), contingencia, tipo_receptor, idreceptor)
                                    'objDocumentoBL.actualizarConingencia2(contingencia)
                                    objDocumentoBL.actualizarNumeroAcceso(contingencia, dtFacturas.Rows(itemSelected).Item("serie"), dtFacturas.Rows(itemSelected).Item("numero"), v_com_cliente.codigo)
                                    'Actualizar serie-correlativo administrativo
                                    If objImpresion.imprimeFacturaContingencia(dtFacturas.Rows(itemSelected).Item("id_encFactura"), dtFacturas.Rows(itemSelected).Item("idEncRecibo").ToString(), True) Then
                                        objDocumentoBL.numeroImpresiones(dtFacturas.Rows(itemSelected).Item("id_encFactura"), "FACTURA", "1")
                                    End If
                                Else
                                    MessageBox.Show("NO SE PUEDE GENERAR CONTINGENCIA, VALIDAR LOS DATOS DEL CLIENTE (NIT, CUI, DPI) considerar ")

                                End If

                            End If
                        Else
                            v_com_cliente = objCliente.getDetalleDelCliente(dtFacturas.Rows(itemSelected).Item("Codigo"))

                            Try
                                Dim texto As String = ""
                                texto = Replace(v_com_cliente.nit, "-", "")
                                v_com_cliente.nit = Replace(texto, "/", "")
                            Catch ex As Exception
                                v_com_cliente.nit = "CF"
                            End Try


                            If dtFacturas.Rows(itemSelected).Item("Importe") > id_glo_total_cf Then
                                If v_com_cliente.nit <> "CF" Then
                                    tipo_receptor = 4
                                    idreceptor = v_com_cliente.nit
                                Else
                                    If Len(v_com_cliente.numeroDi) > 0 Then
                                        If v_com_cliente.numeroDi.Substring(0, 1) <> "P" Then
                                            tipo_receptor = 2
                                            idreceptor = v_com_cliente.numeroDi
                                        Else
                                            Try
                                                Dim quitarP As String = ""
                                                quitarP = Replace(v_com_cliente.numeroDi, "P", "")
                                                tipo_receptor = 3
                                                idreceptor = quitarP
                                            Catch ex As Exception
                                                tipo_receptor = 3
                                                idreceptor = v_com_cliente.numeroDi
                                            End Try
                                        End If

                                    Else
                                        tipo_receptor = 4
                                        idreceptor = v_com_cliente.nit
                                    End If
                                End If
                            Else
                                tipo_receptor = 4
                                idreceptor = v_com_cliente.nit
                            End If


                            contingencia = objDocumentoBL.getContigencia(dtFacturas.Rows(itemSelected).Item("id_encFactura"))
                            'contingencia = GetRandom(100000000, 999999999)
                            objDocumentoBL.actualizarConingencia(dtFacturas.Rows(itemSelected).Item("id_encFactura"), contingencia, tipo_receptor, idreceptor)
                            'objDocumentoBL.actualizarConingencia2(contingencia)
                            objDocumentoBL.actualizarNumeroAcceso(contingencia, dtFacturas.Rows(itemSelected).Item("serie"), dtFacturas.Rows(itemSelected).Item("numero"), v_com_cliente.codigo)
                            'Actualizar serie-correlativo administrativo
                            If objImpresion.imprimeFacturaContingencia(dtFacturas.Rows(itemSelected).Item("id_encFactura"), dtFacturas.Rows(itemSelected).Item("idEncRecibo").ToString(), True) Then
                                objDocumentoBL.numeroImpresiones(dtFacturas.Rows(itemSelected).Item("id_encFactura"), "FACTURA", "1")
                            End If
                        End If
                    End If
                Else
                    MessageBox.Show("Verificar el documento no se pudo operar")
                    Cursor.Current = Cursors.Default
                    Return
                End If
                'De lo contrario 
                'Verificar si existe el recibo y el idEncFactura es igual a 0 y el monto corresponda a la factura y el recibo este en estado (1)
                'Actualize la referencia del encabezado de la factura en el IDFactura.

                'Si no creo el recibo o no existe debe anular solo la factura y la nota de credito si existe.
            Case 1

                If Me.lstRecibos.SelectedIndices.Count <= 0 Then
                    MsgBox("No hay un documento seleccionado.")
                    Cursor.Current = Cursors.Default
                    Return
                End If

                Dim itemSelected = Me.lstRecibos.SelectedIndices(0)
                'If lstRecibos.Items(itemSelected).BackColor = xoInfomat Then

                If objImpresion.imprimeRecibo(dtRecibos.Rows(itemSelected).Item("id_encRecibo"), True) Then
                    objDocumentoBL.numeroImpresiones(dtRecibos.Rows(itemSelected).Item("id_encRecibo"), "RECIBO", "1")
                End If
                'Else
                'MsgBox("No se puede volver a imprimir.")
                Cursor.Current = Cursors.Default
                Return
                'End If
            Case 2
                NumeroContingenciaNC = 0
                If Me.lstNotaCredito.SelectedIndices.Count <= 0 Then
                    MsgBox("No hay un documento seleccionado.")
                    Cursor.Current = Cursors.Default
                    Return
                End If

                Dim itemSelected = Me.lstNotaCredito.SelectedIndices(0)
                If objDocumentoBL.validaDocumentoFELNC(dtNc.Rows(itemSelected).Item("id_encNc")) Then
                    MessageBox.Show("ESTE DOCUMENTO YA FUE OPERADO EN FEL")
                Else
                    If lstNotaCredito.Items(itemSelected).BackColor = xoInfomat Then
                        Dim docAsociado As String
                        Dim Recibo As New documentoCO
                        Dim Factura As New documentoCO
                        Dim Cliente As New ClienteCO
                        Dim objDocumento As New DocumentoBL
                        Recibo = objDocumento.getRecibo(dtRecibos.Rows(itemSelected).Item("id_encRecibo"))
                        Cliente = objCliente.getDetalleDelCliente(Recibo.idCliente)


                        'If dtNc.Rows(itemSelected).Item("id_EncNC").ToString() <> "" Then
                        'Try
                        'Factura = objDocumento.getFactura(dtNc.Rows(itemSelected).Item("idEncFactura"))
                        'docAsociado = Factura.serie + "-" + Factura.numero
                        'Catch ex As Exception

                        'End Try

                        'Else
                        '   Recibo = objDocumento.getRecibo(dtNc.Rows(itemSelected).Item("idEncRecibo"))
                        '  docAsociado = Factura.serie + "-" + Factura.numero
                        'End If


                        v_com_cliente = objCliente.getDetalleDelCliente(dtNc.Rows(itemSelected).Item("Codigo"))

                        Try
                            Dim texto As String = ""
                            texto = Replace(v_com_cliente.nit, "-", "")
                            v_com_cliente.nit = Replace(texto, "/", "")
                        Catch ex As Exception
                            v_com_cliente.nit = "CF"
                        End Try


                        If dtFacturas.Rows(itemSelected).Item("Importe") > id_glo_total_cf Then
                            If v_com_cliente.nit <> "CF" Then
                                tipo_receptor = 4
                                idreceptor = v_com_cliente.nit
                            Else
                                If Len(v_com_cliente.numeroDi) > 0 Then
                                    If v_com_cliente.numeroDi.Substring(0, 1) <> "P" Then
                                        tipo_receptor = 2
                                        idreceptor = v_com_cliente.numeroDi
                                    Else
                                        Try
                                            Dim quitarP As String = ""
                                            quitarP = Replace(v_com_cliente.numeroDi, "P", "")
                                            tipo_receptor = 3
                                            idreceptor = quitarP
                                        Catch ex As Exception
                                            tipo_receptor = 3
                                            idreceptor = v_com_cliente.numeroDi
                                        End Try
                                    End If
                                    tipo_receptor = 4
                                    idreceptor = v_com_cliente.nit
                                Else

                                End If
                            End If
                        Else
                            tipo_receptor = 4
                            idreceptor = v_com_cliente.nit
                        End If



                        If objDocumentoBL.validaDocumentoFEL(dtNc.Rows(itemSelected).Item("idEncFactura")) Then
                            If (objDocumentoBL.validaContingenciaNC(dtNc.Rows(itemSelected).Item("id_encNc"))) Then
                                MessageBox.Show("NO SE PUEDE GENERAR DOCUMENTO DE CONTINGENCIA YA QUE ESTE TIENE FEL")
                            Else
                                'contingencia = GetRandom(100000000, 999999999)
                                contingencia = objDocumentoBL.getContigencia(dtNc.Rows(itemSelected).Item("id_encNc"))
                                'objDocumentoBL.actualizarConingencia2(contingencia)  
                                objDocumentoBL.actualizarConingenciaNC(dtNc.Rows(itemSelected).Item("id_encNc"), contingencia, tipo_receptor, idreceptor)
                                objDocumentoBL.actualizarNumeroAcceso(contingencia, dtNc.Rows(itemSelected).Item("serie"), dtNc.Rows(itemSelected).Item("numero"), v_com_cliente.codigo)
                                'Actualizar serie-correlativo administrativo
                                If objImpresion.ImprimeNotaCreditoContingencia(dtNc.Rows(itemSelected).Item("id_encNc"), True, 0) Then
                                    objDocumentoBL.numeroImpresiones(dtNc.Rows(itemSelected).Item("id_encNc"), "NC", "1")
                                End If
                            End If
                        Else
                            'contingencia = GetRandom(100000000, 999999999)
                            contingencia = objDocumentoBL.getContigencia(dtNc.Rows(itemSelected).Item("id_encNc"))
                            objDocumentoBL.actualizarConingenciaNC(dtNc.Rows(itemSelected).Item("id_encNc"), contingencia, tipo_receptor, idreceptor)
                            objDocumentoBL.actualizarNumeroAcceso(contingencia, dtNc.Rows(itemSelected).Item("serie"), dtNc.Rows(itemSelected).Item("numero"), v_com_cliente.codigo)
                            'Actualizar serie-correlativo administrativo
                            If objImpresion.ImprimeNotaCreditoContingencia(dtNc.Rows(itemSelected).Item("id_encNc"), True, 0) Then
                                objDocumentoBL.numeroImpresiones(dtNc.Rows(itemSelected).Item("id_encNc"), "NC", "1")
                            End If
                        End If
                    Else
                        MsgBox("No se puede volver a imprimir.")
                        Cursor.Current = Cursors.Default
                        Return
                End If
                End If
        End Select
        Cursor.Current = Cursors.Default
        cargarListView()
    End Sub

    Private Sub MenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem3.Click
        Try
            Dim starter As New ThreadStart(AddressOf Me.imprimirSINFEL)
            Dim t As New Thread(starter)
            t.IsBackground = True
            t.Start()
        Catch ex As Exception
            MsgBox(ex.Message())
        End Try
    End Sub
End Class