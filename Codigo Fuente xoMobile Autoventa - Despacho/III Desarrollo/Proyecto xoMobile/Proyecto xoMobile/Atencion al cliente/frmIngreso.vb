Imports System.Data
Imports System.Threading
Imports System.Math
Imports Proyecto_xoMobile_Packs

Public Class frmIngreso

#Region " Variables y objetos. "
    Private WithEvents itemPresupuesto As New MenuItem

    'Objetos de Comunicacion entre capas    
    Public lstAgregadosExternal As New Windows.Forms.ListView

    'Objetos de Comunicacion entre capas
    Public comDocumento As New documentoCO
    Public comProducto As New ProductoCO
    Public initLwProductos As Boolean
    'Public lstDetalle As New Windows.Forms.ListView
    Public Shared isDiferente As Boolean

    'Objetos de Comunicacion entre procesos
    Public v_com_cliente As New ClienteCO

    'Objetos de la capa de negocio
    Dim objDocumentoBL As New DocumentoBL
    Dim objProductoBL As New ProductoBL
    Dim objUtilBl As New UtilitarioBL
    Dim objClienteBL As New ClienteBL

    'Objetos
    Dim objProducto As New ProductoDT
    Dim objUtil As New UtilitarioDT
    Dim ObjUtil2 As New UtilitarioBL
    Dim objInventario As New Inventario
    Dim objFactura As New Factura
    Dim objNc As New NotaCreditoDT
    Dim objRecibo As New Recibo

    '--- Datatables de llenado externo y local
    Public Shared dtProductos As New DataTable
    Public Shared dtAgregados As New DataTable

    Dim dtProducto As New DataTable
    Dim dtProductosFull As New DataTable
    Dim isforUpdate As Boolean = False
    Dim isforDelete As Boolean = False
    Dim isIngresado As Boolean = False
    Dim firstEnabled As Boolean = True

    'Message box
    Dim msg As String
    Dim title As String
    Dim style As MsgBoxStyle
    Dim response As MsgBoxResult
    Dim itemSelected

    '--- Control del flujo de eventos
    Public rlayer As New rLayerHandler
    Public comPaso As Integer
    Public comCorriendo As Boolean

    '--- Variables publicas de despacho
    Public idPedido As String

#End Region

#Region " Inicializar Objetos. "

    Private Sub frmInventario_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            '--- Inicializar text box           
            limpiarTxtIngreso()
            Dim dtCliente As DataTable
            Dim objUtil As New UtilitarioBL
            firstEnabled = True
            dtCliente = objUtil.obtenerModGlobalPorVariable("KEY_PROD")
            ComboBox1.DataSource = dtCliente
            ComboBox1.ValueMember = dtCliente.Columns("idMod").ToString
            ComboBox1.DisplayMember = dtCliente.Columns("valor").ToString
            firstEnabled = False

            '--- Traer una instancia del cliente para actualizar cualquier dato en memoria
            v_com_cliente = objClienteBL.getDetalleDelCliente(v_com_cliente.codigo) ' (55003789) '(55003964)

            If (id_glo_sociedad = 7000) Then
                Label2.Text = "Und."
            Else
                Label2.Text = "Lts."
            End If

            '--- Establecer el icono de la aplicacion 
            Select Case id_glo_aplicacion
                Case "inventario"
                    cmdSiguiente.Text = "Aceptar"

                    '--- Listado de productos para toma de inventario
                    dtProductos = objProductoBL.ObtenerListadoProductos("Inventario", v_com_cliente)
                    dtProductosFull = dtProductos.Copy

                    '---  Inicializar listado de productos agregados           
                    inicializarEncabezadoListas()
                    crearLwProductos()
                    crearLwAgregados()

                Case "venta"

                    '--- Identificar si es un proceso nuevo o en curso
                    If glo_dtVentaProductos.Rows.Count = 0 Then
                        '--- Proceso nuevo, cargar el listado de productos
                        dtProductos = objProductoBL.ObtenerListadoProductos("venta", v_com_cliente)
                        dtProductosFull = dtProductos.Copy

                        If dtProductos.Rows.Count <= 0 Then
                            Throw New Exception("El producto para la venta se ha agotado.")
                        End If
                        crearLwAgregados()
                    Else
                        'dtProductos = glo_dtVentaProductos
                        dtProductos = objProductoBL.ObtenerListadoProductos("venta", v_com_cliente)
                        dtProductosFull = dtProductos.Copy
                        lstAgregados = glo_lvVentaAgregados
                    End If
                    inicializarEncabezadoListas()
                    crearLwProductosV()

                    calcularImporte()

                Case "nc"
                    '--- Inicilizar listas
                    inicializarEncabezadoListas()

                    If glo_dtNcProductos.Rows.Count = 0 Then
                        '--- Proceso nuevo, cargar el listado de productos
                        dtProductos = objProducto.getListadoEnvases()
                        dtProductosFull = dtProductos.Copy
                        desplegarListaAgregados()
                    Else
                        dtProductos = glo_dtNcProductos
                        dtProductosFull = dtProductos.Copy
                        lstAgregados = glo_lvNcAgregados
                    End If
                    crearLwProductos()

                Case "despacho"
                    Dim objDespacho As New DespachoBL

                    '--- Habilitar controles
                    panIngresoC.Enabled = False
                    cmdEliminar.Image = imgIcons.Images(0)
                    picBusca.Visible = False

                    '--- Logica de negocio para crear listado de productos a despachar
                    objDespacho.crearListaProductos(dtProductos, lstProductos, v_com_cliente, comDocumento)
                    desplegarListaProductos()

                    '--- Logica de negocio para crear detalle de los productos a despachar
                    objDespacho.crearListaAgregados(dtAgregados, lstAgregados, v_com_cliente, comDocumento)

                    lblImportePedido.Text = FormatCurrency(comDocumento.importePedido, 2)
                    lblDescuentoPedido.Text = FormatCurrency(comDocumento.destoPedido, 2)
                    lblVariacionDesto.Text = FormatCurrency(Math.Abs(comDocumento.destoPedido) - (comDocumento.importeDesto) * -1, 2)
                    lblDescuentoDespacho.Text = FormatCurrency(comDocumento.importeDesto, 2)
                    desplegarListaAgregados()

                Case "cambio"
                    Dim objDespacho As New DespachoBL

                    '--- Habilitar controles
                    panIngresoC.Enabled = False
                    cmdEliminar.Image = imgIcons.Images(0)
                    picBusca.Visible = False

                    '--- Logica de negocio para crear listado de productos a despachar
                    objDespacho.crearListaProductos(dtProductos, lstProductos, v_com_cliente, comDocumento)
                    desplegarListaProductos()

                    '--- Logica de negocio para crear detalle de los productos a despachar
                    objDespacho.crearListaAgregados(dtAgregados, lstAgregados, v_com_cliente, comDocumento)

                    lblImportePedido.Text = 0 'FormatCurrency(comDocumento.importePedido, 2)
                    lblDescuentoPedido.Text = 0 'FormatCurrency(comDocumento.destoPedido, 2)
                    lblVariacionDesto.Text = 0 'FormatCurrency(Math.Abs(comDocumento.destoPedido) - (comDocumento.importeDesto) * -1, 2)
                    lblDescuentoDespacho.Text = 0 'FormatCurrency(comDocumento.importeDesto, 2)
                    desplegarListaAgregados()



            End Select

            '--- Actualizar valores acumulados
            calcularImporte()

            '--- Inicializar listado de productos disponibles 
            isforUpdate = False
            lstProductos.Focus()
            lstProductos.Items(0).Selected = True

        Catch ex As Exception
            MsgBox(ex.Message)
            Cursor.Current = Cursors.Default
        End Try
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub inicializarEncabezadoListas()

        Dim codigo = New ColumnHeader()         '0
        Dim descripcion = New ColumnHeader()    '1
        Dim disponible = New ColumnHeader()     '2


        codigo.Text = "Liquido"
        descripcion.Text = "Descripcion"
        disponible.Text = "CA/UN"


        lstProductos.Columns.Add(codigo)
        lstProductos.Columns.Add(descripcion)
        lstProductos.Columns.Add(disponible)
    End Sub

    Private Sub limpiarTxtIngreso()
        txtcLiquido.Text = "0"
        txtuLiquido.Text = "0"
        txtcEnvase.Text = "0"
        txtuEnvase.Text = "0"
        txtuCajaVacia.Text = "0"

        '--- Ocultar sandwich y label mas columnas
        If tipoRuta <> "16" Then
            lnkMas.Visible = False
            picMenu.Visible = False
        End If
    End Sub

#End Region

#Region " BUSCAR ITEM. "

    Private Sub wtBuscarItem()
        Dim objUtil As New UtilitarioBL
        objUtil.buscarItem(dtProductos, txtBuscar.Text)
        crearLwProductos()
    End Sub

    Private Sub txtBuscar_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBuscar.KeyPress
        If e.KeyChar() = ChrW(13) Then
            wtBuscarItem()
            lstProductos.Focus()
        End If
    End Sub
#End Region

#Region "Seleccionar una palabra de busqueda personalizada. "
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If Not firstEnabled Then
            txtBuscar.Text = ComboBox1.Text
        End If
    End Sub
#End Region

#Region "Seleccionar un producto para contar. "

    Private Sub lstProductos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstProductos.SelectedIndexChanged
        If Me.lstProductos.SelectedIndices.Count <= 0 Then
            Return
        End If

        '--- Obtener el indice del item seleccionado
        itemSelected = Me.lstProductos.SelectedIndices(0)

        '--- Capturar el codigo y nombre del producto por medio del indice
        id_glo_producto = lstProductos.Items(itemSelected).SubItems(0).Text
        lblProducto.Text = Trim(lstProductos.Items(itemSelected).SubItems(1).Text)


    End Sub

#End Region

#Region "Mostrar panel para ingresar las cantidades. "
    Private Sub itemDetalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lIngreso.Click
        ingresarProducto()
    End Sub

    Private Sub getCantidadPorItem()

        '--- Recorrer el listado de items agregados para obtener  las cantidades
        For i As Integer = 0 To lstAgregados.Items.Count() - 1
            With lstAgregados.Items(i)
                If id_glo_producto.ToString = .SubItems(0).Text Then

                    'Liquido
                    If .SubItems(3).Text = lblM1.Text Then
                        txtcLiquido.Text = .SubItems(5).Text
                        txtuLiquido.Text = .SubItems(6).Text
                    End If

                    If id_glo_aplicacion = "nc" Then

                        '--- Envases
                        If .SubItems(3).Text = lblM2.Text Then
                            txtcEnvase.Text = .SubItems(5).Text
                            txtuEnvase.Text = .SubItems(6).Text
                        End If
                    End If

                 
                End If
            End With
        Next
    End Sub

    Private Sub ingresarProducto()
        '--- Mostrar el listado de productos si esta oculto
        lstProductos.Enabled = False
        picBusca.Enabled = False
        picCerrar.Enabled = False
        lstProductos.Enabled = False
        ComboBox1.Enabled = False
        txtBuscar.Enabled = False
        If lstAgregados.Visible = True Then
            lstAgregados.Visible = False
            Exit Sub
        Else

            '--- Verifica si hay un producto seleccionado
            If Me.lstProductos.SelectedIndices.Count <= 0 Then
                MsgBox("Seleccione un producto.")
            Else
                '--- Crear una instancia del producto seleccionado
                comProducto = objProductoBL.getDetalleDelProducto(id_glo_producto)

                '--- Mostrar panel de ingreso
                limpiarTxtIngreso()
                panIngreso.Visible = True
                lstProductos.Enabled = False

                '--- Mostrar los input de mercaderia si son necesarios
                If comProducto.idCaja = 0 Then
                    PanR3.Visible = False
                Else
                    PanR3.Visible = True
                End If

                If comProducto.idEnvase = 0 Then
                    panR2.Visible = False
                Else
                    panR2.Visible = True
                End If

                If id_glo_aplicacion = "inventario" Then
                    PanR3.Visible = False
                    panR2.Visible = False
                End If

                If id_glo_aplicacion = "venta" Then
                    panR2.Visible = False
                    Dim x = 7
                    Dim y = 50
                    PanR3.Location = New Point(x, y)

                End If


                If (lstProductos.Items(itemSelected).BackColor = xoInfomat) Then

                    'Obtener las cantidades agregadas del item
                    getCantidadPorItem()

                    'Establecer la bandera que indica que se esta haciendo una modificacion
                    isforUpdate = True
                End If

                'Establecer el foco
                If id_glo_aplicacion = "nc" Then
                    '--- La recoleccion de envase no necesita liquido
                    panR1.Visible = False
                    txtuLiquido.Text = "0"
                    txtcLiquido.Text = "0"
                    txtcEnvase.Focus()
                Else
                    txtcLiquido.Focus()
                    txtcLiquido.SelectAll()
                End If
            End If
        End If
    End Sub
#End Region

#Region " Ingreso de cantidades. "
    '--- Al obtener el foco seleccionar
    Private Sub txtcLiquido_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcLiquido.GotFocus
        txtcLiquido.SelectAll()
    End Sub

    Private Sub accionEscape()
        panIngreso.Visible = False
        lstProductos.Enabled = True
        lstProductos.Focus()
        lstProductos.Items(itemSelected).Selected = True
    End Sub
    Private Sub txtuLiquido_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuLiquido.GotFocus
        txtuLiquido.SelectAll()
    End Sub
    Private Sub txtcEnvase_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcEnvase.GotFocus
        txtcEnvase.SelectAll()
        If panR2.Visible = False Then
            'Enviar el foco a la siguiente mercaderia
            txtuCajaVacia.Focus()
        End If
    End Sub
    Private Sub txtuEnvase_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuEnvase.GotFocus
        txtuEnvase.SelectAll()
    End Sub
    Private Sub txtuCajaVacia_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuCajaVacia.GotFocus
        txtuCajaVacia.SelectAll()
        If id_glo_aplicacion = "inventario" Then

            evaluarIngreso(True)
        End If
    End Sub

    '--- Al presionar la tecla ENTER
    Private Sub txtcLiquido_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcLiquido.KeyPress
        Select Case e.KeyChar()
            Case ChrW(13)
                txtuLiquido.Focus()
            Case ChrW(Keys.Escape)
                accionEscape()
        End Select
    End Sub
    Private Sub txtuLiquido_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtuLiquido.KeyPress
        Select Case e.KeyChar()
            Case ChrW(13)
                If PanR3.Visible Then
                    txtuCajaVacia.Focus()
                Else
                    If id_glo_aplicacion = "venta" Then
                        objProductoBL.mitadMasUno(comProducto, txtuLiquido.Text, txtuCajaVacia.Text)
                    End If
                    evaluarIngreso(True)
                End If
            Case ChrW(Keys.Escape)
                accionEscape()
        End Select
    End Sub
    Private Sub txtcEnvase_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcEnvase.KeyPress
        Select Case e.KeyChar()
            Case ChrW(13)
                txtuEnvase.Focus()
            Case ChrW(Keys.Escape)
                lstProductos.Enabled = True
                panIngreso.Visible = False
                lstProductos.Focus()
                lstProductos.Items(itemSelected).Selected = True
        End Select
    End Sub
    Private Sub txtuEnvase_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtuEnvase.KeyPress
        Select Case e.KeyChar()
            Case ChrW(13)
                txtuCajaVacia.Focus()
            Case ChrW(Keys.Escape)
                accionEscape()
        End Select
    End Sub
    Private Sub txtuCajaVacia_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtuCajaVacia.KeyPress
        'Al abandonar el ultimo control grabar o actualizar
        Select Case e.KeyChar()
            Case ChrW(13)
                If id_glo_aplicacion = "venta" Then
                    objProductoBL.mitadMasUno(comProducto, txtuLiquido.Text, txtuCajaVacia.Text, inventarioComprometido(comProducto.codigo, comProducto.idCaja), txtcLiquido.Text)
                End If
                evaluarIngreso(True)
            Case ChrW(Keys.Escape)
                accionEscape()
        End Select
    End Sub

    '--- Al abandonar el control y perder el foco
    Private Sub txtcLiquido_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcLiquido.LostFocus
        'Liquido en caja completa
        If objUtil.isEntero(txtcLiquido.Text) = False Then
            txtcLiquido.Text = 0
        Else
            txtcLiquido.Text = Abs(Convert.ToInt32(txtcLiquido.Text))
        End If
    End Sub
    Private Sub txtcEnvase_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcEnvase.LostFocus
        'Envase vacio en caja completa
        If objUtil.isEntero(txtcEnvase.Text) = False Then
            txtcEnvase.Text = 0
        Else
            txtcEnvase.Text = Abs(Convert.ToInt32(txtcEnvase.Text))
        End If
    End Sub
    Private Sub txtuLiquido_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuLiquido.LostFocus
        'Liquido en unidades
        If objUtil.isEntero(txtuLiquido.Text) = False Then
            txtuLiquido.Text = 0
        Else
            txtuLiquido.Text = Abs(Convert.ToInt32(txtuLiquido.Text))
            If id_glo_aplicacion = "venta" Then
                objProductoBL.convertirUnidadesAcajas(comProducto, txtuLiquido.Text, txtcLiquido.Text, inventarioComprometido(comProducto.codigo, comProducto.idCaja), False)
            End If
        End If
    End Sub
    Private Sub txtuEnvase_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuEnvase.LostFocus
        'Envase en unidades
        If objUtil.isEntero(txtuEnvase.Text) = False Then
            txtuEnvase.Text = 0
        Else
            txtuEnvase.Text = Abs(Convert.ToInt32(txtuEnvase.Text))
            If id_glo_aplicacion = "venta" Then
                objProductoBL.convertirUnidadesAcajas(comProducto, txtuEnvase.Text, txtcEnvase.Text)
            End If
        End If
    End Sub
    Private Sub txtuCajaVacia_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuCajaVacia.LostFocus
        'Caja vacia en unidades
        If objUtil.isEntero(txtuCajaVacia.Text) = False Then
            txtuCajaVacia.Text = 0
        Else
            txtuCajaVacia.Text = Abs(Convert.ToInt32(txtuCajaVacia.Text))
        End If
    End Sub

    '--- Evaluar si los datos ingresados son para modificacion o ingreso nuevo
    Private Sub evaluarIngreso(ByVal checkCero As Boolean)
        Cursor.Current = Cursors.WaitCursor
        Dim objUtilBL As New UtilitarioBL
        If id_glo_aplicacion <> "inventario" And checkCero Then
            If (objUtil.isDecimal(txtcLiquido.Text) + objUtil.isDecimal(txtuLiquido.Text) + objUtil.isDecimal(txtcEnvase.Text) + objUtil.isDecimal(txtuEnvase.Text) + objUtil.isDecimal(txtuCajaVacia.Text) = 0) Then
                MsgBox("Para eliminar el registro utilice el icono de bote de basura.")
                Cursor.Current = Cursors.Default
                Return
            End If
        End If

        'Habilitar campos
        lstProductos.Enabled = True
        picBusca.Enabled = True
        picCerrar.Enabled = True
        ComboBox1.Enabled = True
        txtBuscar.Enabled = True
        '--- Disparar el metodo de modificacion o agrega items
        Try
            If isforUpdate Then
                'Actualizar items
                actualizarItem()
                isforUpdate = False
            Else
                'Agregar items
                agregarItem()
            End If

            'Colorear la fila si tuvo ingreso
            If isIngresado Then
                lstProductos.Items(itemSelected).ImageIndex = 0
                lstProductos.Items(itemSelected).BackColor = xoInfomat
                For i As Integer = 0 To dtProductos.Rows.Count() - 1
                    If Trim(dtProductos.Rows(i).Item("codigo")) = Trim(lstProductos.Items(itemSelected).SubItems(0).Text) Then
                        dtProductos.Rows(i).Item("agregado") = 1
                        dtProductosFull.Rows(i).Item("agregado") = 1
                    End If
                Next
                isIngresado = False
            Else
                lstProductos.Items(itemSelected).BackColor = Color.White
                lstProductos.Items(itemSelected).ImageIndex = -1
            End If
            panIngreso.Visible = False
            lstProductos.Focus()
            lstProductos.Items(itemSelected).Selected = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = Cursors.Default
    End Sub
#End Region

#Region " AGREGAR UN ITEM AL LISTADO DE PRODUCTOS "

    Private Sub agregarItem()

        Dim item As New ItemCO
        Dim porc_aceptado As Double = 0


        '--- Trasladar el foco fuera de el panel de ingreso.
        lstProductos.Focus()
        lstProductos.Enabled = True

        '--- Crear los items a cargar por medio de explosion de materiales
        If id_glo_aplicacion = "nc" Then
            txtuLiquido.Text = txtuEnvase.Text
            txtcLiquido.Text = txtcEnvase.Text
        End If
        item = objProductoBL.explosionarMaterial(v_com_cliente, comProducto, txtcLiquido.Text, txtuLiquido.Text, txtcEnvase.Text, txtuEnvase.Text, txtuCajaVacia.Text)
        If item Is Nothing Then Return

        '--- Obtener el descuento por categoria, para visualizacion previa
        objProductoBL.descuentoCategoria("DESC", v_com_cliente, comProducto, item)

        If item Is Nothing Then Return


        '--------------------------------------------------------------------------------------'
        '----------------------------   Verificar existencia   --------------------------------'
        '--------------------------------------------------------------------------------------'
        If id_glo_aplicacion = "venta" And xo_validaInventario Then

            '--- Verificar existencia de producto liquido 
            If comProducto.cantidadActual < item.un_liquido Then
                MsgBox("La cantidad ingresada excede el inventario actual de: " & comProducto.cantidadActual.ToString & " unidades.")
                calcularImporte()
                Return
            Else

                '--- Verificar existencia de cajas
                '--- Restar  las cajas que estan comprometidas en el GRID de consulta
                comProducto.cantidadActualC -= inventarioComprometido(comProducto.codigo, comProducto.idCaja)
                If comProducto.cantidadActualC < item.un_caja Then
                    If comProducto.cantidadActualC <= 0 Then
                        MsgBox("Se ha agotado el inventario de cajas. Se ha de considerar venta en unidades.", MsgBoxStyle.Exclamation)
                        txtuCajaVacia.Text = 0
                        item.un_caja = 0
                    Else
                        MsgBox("La cantidad ingresada excede el inventario actual de cajas: " & comProducto.cantidadActualC.ToString & " cajas." + vbCrLf + "Considere venta en unidades.", MsgBoxStyle.Information)
                        calcularImporte()
                        Return
                    End If
                End If
            End If
        End If

        'If (id_glo_aplicacion = "venta" Or id_glo_aplicacion = "despacho") Then
        'total_venta = total_venta + item.importeCaja + item.importeCajaIva + item.importeEnvase + item.importeEnvaseIva + item.importeLiquido + item.importeLiquidoIva
        'End If

        'If (id_glo_aplicacion = "nc") Then
        ' total_envase = total_envase + item.importeCaja + item.importeCajaIva + item.importeEnvase + item.importeEnvaseIva
        'End If

        'If (total_venta > 0) Then
        ' porc_aceptado = ((total_venta * 75) / 100)
        'End If




        'MessageBox.Show("Total Venta " & total_venta & "  Total Envase " & total_envase)

        If (id_glo_aplicacion = "nc") Then
            'If (porc_aceptado > total_envase) Then
            '--------------------------------------------------------------------------------------'
            '--------------------------------   Linea Liquido--------------------------------------'
            '--------------------------------------------------------------------------------------'
            If (objUtilBl.isDecimal(txtcLiquido.Text) > 0 Or objUtilBl.isDecimal(txtuLiquido.Text) > 0) And id_glo_aplicacion <> "nc" Then
                Dim lvi As New ListViewItem(id_glo_producto.ToString)                                        '0  - Correlativo 
                lvi.SubItems.Add(id_glo_producto.ToString)                                                   '1  - idProducto Base
                lvi.SubItems.Add(comProducto.descripcion)                                                    '2  - Descripcion producto
                lvi.SubItems.Add("L")                                                                        '3  - Tipo de producto [L,E,C]
                lvi.SubItems.Add(objUtilBl.isDecimal(item.precioVentaLiquido))                               '4  - Precio de venta
                lvi.SubItems.Add(txtcLiquido.Text)                                                           '5  - Cajas ingresadas
                lvi.SubItems.Add(txtuLiquido.Text)                                                           '6  - Unidades ingresadas
                lvi.SubItems.Add(item.importeLiquido + item.importeLiquidoIva)                               '7  - Importe
                lvi.SubItems.Add(item.un_caja)                                                               '8  - Unidades caja Total
                lvi.SubItems.Add(txtuLiquido.Text)                                                           '09 - Unidades Liquido Total
                lvi.SubItems.Add(id_glo_producto.ToString)                                                   '10 - idProducto 
                lvi.SubItems.Add(item.precioUnitarioLiquido)                                                 '11 - Precio sin iva
                lvi.SubItems.Add(item.importeLiquido)                                                        '12 - importe sin iva
                lvi.SubItems.Add(comProducto.litrosUnidad * (item.un_liquido))                               '13 - Litros
                lvi.SubItems.Add(item.porcentajeDesto)                                                       '14 - % Descuento
                lvi.SubItems.Add(item.importeDesto)                                                          '15 - Importe Descuento
                lvi.SubItems.Add((item.importeLiquido + item.importeLiquidoIva) + item.importeDesto)         '16 - Sub total
                lvi.SubItems.Add((item.valorIvaDesto))                                                       '17 - Iva del descuento
                lvi.SubItems.Add(item.importeLiquido + item.importeLiquidoIva)                               '18 - Importe para igualar Despacho
                lstAgregados.Items.Add(lvi)
                isIngresado = True

            End If



            '--------------------------------------------------------------------------------------'
            '--------------------------------     Linea Envase    ---------------------------------'    
            '--------------------------------------------------------------------------------------'
            If (objUtilBl.isDecimal(txtcLiquido.Text) > 0 Or objUtilBl.isDecimal(txtuLiquido.Text) > 0) Then
                If item.unidadesCaja > 0 And comProducto.idEnvase <> 0 Then
                    Dim lvi As New ListViewItem(id_glo_producto.ToString)                                        '0  - Correlativo
                    '--- Mostrar/ocultar nombre del item
                    If panR1.Visible Then
                        lvi.SubItems.Add("")
                        lvi.SubItems.Add("")
                    Else
                        lvi.SubItems.Add(id_glo_producto.ToString)                                               '1  - idProducto Base
                        lvi.SubItems.Add(comProducto.descripcion)                                                '2  - Descripcion producto
                    End If

                    lvi.SubItems.Add("E")                                                                        '3  - Tipo de producto [L,E,C]
                    lvi.SubItems.Add(objUtilBl.isDecimal(item.precioVentaEnvase))                                '4  - Precio de venta
                    lvi.SubItems.Add(txtcLiquido.Text)                                                           '5  - Cajas ingresadas
                    lvi.SubItems.Add(txtuLiquido.Text)                                                           '6  - Unidades ingresadas
                    lvi.SubItems.Add(item.importeEnvase + item.importeEnvaseIva)                                 '7  - Importe
                    lvi.SubItems.Add(item.un_caja)                                                               '8  - Unidades caja Total
                    lvi.SubItems.Add(txtuLiquido.Text)                                                           '9 - Unidades Liquido Total
                    lvi.SubItems.Add(comProducto.idEnvase)                                                       '10 - idProducto 
                    lvi.SubItems.Add(item.precioUnitarioEnvase)                                                  '11 - Precio sin iva
                    lvi.SubItems.Add(item.importeEnvase)                                                         '12 - importe sin iva
                    lvi.SubItems.Add(0)                                                                          '13 - Litros
                    lvi.SubItems.Add(0)                                                                          '14 - % Descuento
                    lvi.SubItems.Add(FormatCurrency(0, 2))                                                       '15 - Monto Descuento
                    lvi.SubItems.Add(0)                                                                          '16 - Sub total
                    lvi.SubItems.Add(0)                                                                          '17 - iva del descuento
                    lvi.SubItems.Add(item.importeEnvase + item.importeEnvaseIva)                                 '18 - Importe para igualar Despacho
                    lstAgregados.Items.Add(lvi)
                    isIngresado = True
                End If
            End If




            '--------------------------------------------------------------------------------------'
            '--------------------------------     Linea Caja      ---------------------------------'    
            '--------------------------------------------------------------------------------------'

            If item.un_caja > 0 And comProducto.idCaja <> 0 Then
                Dim lvi As New ListViewItem(id_glo_producto.ToString)                                    '0  - Correlativo

                '--- Mostrar/ocultar nombre del item
                If panR1.Visible Then
                    lvi.SubItems.Add("")
                    lvi.SubItems.Add("")
                Else
                    lvi.SubItems.Add(id_glo_producto.ToString)                                            '1  - idProducto Base
                    lvi.SubItems.Add(comProducto.descripcion)                                             '2  - Descripcion producto
                End If

                lvi.SubItems.Add("C")                                                                     '3  - Tipo de producto [L,E,C]
                lvi.SubItems.Add(objUtilBl.isDecimal(item.precioVentaCaja))                               '4  - Precio de venta
                lvi.SubItems.Add("")                                                                      '5  - Cajas ingresadas
                lvi.SubItems.Add(item.un_caja)                                                            '6  - Unidades ingresadas
                lvi.SubItems.Add(item.importeCaja + item.importeCajaIva)                                  '7  - Importe
                lvi.SubItems.Add("")                                                                      '8  - Unidades caja Total
                lvi.SubItems.Add(item.un_caja)                                                            '09 - Unidades Liquido Total
                lvi.SubItems.Add(comProducto.idCaja)                                                      '10 - idProducto 
                lvi.SubItems.Add(item.precioUnitarioCaja)                                                 '11 - Precio sin iva
                lvi.SubItems.Add(item.importeCaja)                                                        '12 - importe sin iva
                lvi.SubItems.Add(0)                                                                       '13 - Litros
                lvi.SubItems.Add(0)                                                                       '14 - % Descuento
                lvi.SubItems.Add(FormatCurrency(0, 2))                                                    '15 - Monto Descuento
                lvi.SubItems.Add(0)                                                                       '16 - Sub total
                lvi.SubItems.Add(0)                                                                       '17 - iva del descuento
                lvi.SubItems.Add(item.importeCaja + item.importeCajaIva)                                  '18 - Importe para igualar Despacho
                lstAgregados.Items.Add(lvi)
                isIngresado = True
            End If
            '--------------------------------------------------------------------------------------'
            '--------------------------------    Linea Total   ------------------------------------'
            '--------------------------------------------------------------------------------------'
            If isIngresado = True Then
                Dim lvi As New ListViewItem(id_glo_producto.ToString)
                lvi.SubItems.Add("S U B")
                lvi.SubItems.Add("T O T A L")
                lvi.SubItems.Add("")
                lvi.SubItems.Add("")
                lvi.SubItems.Add("")
                lvi.SubItems.Add("")
                lvi.SubItems.Add(FormatCurrency(item.importeCaja + item.importeCajaIva + item.importeEnvase + item.importeEnvaseIva + item.importeLiquido + item.importeLiquidoIva, 2))
                lvi.SubItems.Add("")
                lvi.SubItems.Add("")
                lvi.SubItems.Add("")
                lvi.SubItems.Add("")
                lvi.SubItems.Add("")
                lvi.BackColor = xoWarning
                lvi.ForeColor = Color.Black
                lstAgregados.Items.Add(lvi)
            End If
            'Actualizar el label que muestra el importe acumulado
            calcularImporte()
            'Else
            'MessageBox.Show("LA NOTA DE CREDITO EXCEDE EL 75% DEL TOTAL DEL DOCUMENTO DE REFERENCIA MONTO ACEPTADO" & porc_aceptado)
            'total_envase = total_envase - item.importeCaja - item.importeCajaIva - item.importeEnvase - item.importeEnvaseIva
            'End If
        Else
        '--------------------------------------------------------------------------------------'
        '--------------------------------   Linea Liquido--------------------------------------'
        '--------------------------------------------------------------------------------------'
        If (objUtilBl.isDecimal(txtcLiquido.Text) > 0 Or objUtilBl.isDecimal(txtuLiquido.Text) > 0) And id_glo_aplicacion <> "nc" Then
            Dim lvi As New ListViewItem(id_glo_producto.ToString)                                        '0  - Correlativo 
            lvi.SubItems.Add(id_glo_producto.ToString)                                                   '1  - idProducto Base
            lvi.SubItems.Add(comProducto.descripcion)                                                    '2  - Descripcion producto
            lvi.SubItems.Add("L")                                                                        '3  - Tipo de producto [L,E,C]
            lvi.SubItems.Add(objUtilBl.isDecimal(item.precioVentaLiquido))                               '4  - Precio de venta
            lvi.SubItems.Add(txtcLiquido.Text)                                                           '5  - Cajas ingresadas
            lvi.SubItems.Add(txtuLiquido.Text)                                                           '6  - Unidades ingresadas
            lvi.SubItems.Add(item.importeLiquido + item.importeLiquidoIva)                               '7  - Importe
            lvi.SubItems.Add(item.un_caja)                                                               '8  - Unidades caja Total
            lvi.SubItems.Add(txtuLiquido.Text)                                                           '09 - Unidades Liquido Total
            lvi.SubItems.Add(id_glo_producto.ToString)                                                   '10 - idProducto 
            lvi.SubItems.Add(item.precioUnitarioLiquido)                                                 '11 - Precio sin iva
            lvi.SubItems.Add(item.importeLiquido)                                                        '12 - importe sin iva
            lvi.SubItems.Add(comProducto.litrosUnidad * (item.un_liquido))                               '13 - Litros
            lvi.SubItems.Add(item.porcentajeDesto)                                                       '14 - % Descuento
            lvi.SubItems.Add(item.importeDesto)                                                          '15 - Importe Descuento
            lvi.SubItems.Add((item.importeLiquido + item.importeLiquidoIva) + item.importeDesto)         '16 - Sub total
            lvi.SubItems.Add((item.valorIvaDesto))                                                       '17 - Iva del descuento
            lvi.SubItems.Add(item.importeLiquido + item.importeLiquidoIva)                               '18 - Importe para igualar Despacho
            lstAgregados.Items.Add(lvi)
            isIngresado = True

        End If
        '--------------------------------------------------------------------------------------'
        '--------------------------------     Linea Envase    ---------------------------------'    
        '--------------------------------------------------------------------------------------'
        If (objUtilBl.isDecimal(txtcLiquido.Text) > 0 Or objUtilBl.isDecimal(txtuLiquido.Text) > 0) Then
            If item.unidadesCaja > 0 And comProducto.idEnvase <> 0 Then
                Dim lvi As New ListViewItem(id_glo_producto.ToString)                                        '0  - Correlativo
                '--- Mostrar/ocultar nombre del item
                If panR1.Visible Then
                    lvi.SubItems.Add("")
                    lvi.SubItems.Add("")
                Else
                    lvi.SubItems.Add(id_glo_producto.ToString)                                               '1  - idProducto Base
                    lvi.SubItems.Add(comProducto.descripcion)                                                '2  - Descripcion producto
                End If

                lvi.SubItems.Add("E")                                                                        '3  - Tipo de producto [L,E,C]
                lvi.SubItems.Add(objUtilBl.isDecimal(item.precioVentaEnvase))                                '4  - Precio de venta
                lvi.SubItems.Add(txtcLiquido.Text)                                                           '5  - Cajas ingresadas
                lvi.SubItems.Add(txtuLiquido.Text)                                                           '6  - Unidades ingresadas
                lvi.SubItems.Add(item.importeEnvase + item.importeEnvaseIva)                                 '7  - Importe
                lvi.SubItems.Add(item.un_caja)                                                               '8  - Unidades caja Total
                lvi.SubItems.Add(txtuLiquido.Text)                                                           '9 - Unidades Liquido Total
                lvi.SubItems.Add(comProducto.idEnvase)                                                       '10 - idProducto 
                lvi.SubItems.Add(item.precioUnitarioEnvase)                                                  '11 - Precio sin iva
                lvi.SubItems.Add(item.importeEnvase)                                                         '12 - importe sin iva
                lvi.SubItems.Add(0)                                                                          '13 - Litros
                lvi.SubItems.Add(0)                                                                          '14 - % Descuento
                lvi.SubItems.Add(FormatCurrency(0, 2))                                                       '15 - Monto Descuento
                lvi.SubItems.Add(0)                                                                          '16 - Sub total
                lvi.SubItems.Add(0)                                                                          '17 - iva del descuento
                lvi.SubItems.Add(item.importeEnvase + item.importeEnvaseIva)                                 '18 - Importe para igualar Despacho
                lstAgregados.Items.Add(lvi)
                isIngresado = True
            End If
        End If




        '--------------------------------------------------------------------------------------'
        '--------------------------------     Linea Caja      ---------------------------------'    
        '--------------------------------------------------------------------------------------'

        If item.un_caja > 0 And comProducto.idCaja <> 0 Then
            Dim lvi As New ListViewItem(id_glo_producto.ToString)                                    '0  - Correlativo

            '--- Mostrar/ocultar nombre del item
            If panR1.Visible Then
                lvi.SubItems.Add("")
                lvi.SubItems.Add("")
            Else
                lvi.SubItems.Add(id_glo_producto.ToString)                                            '1  - idProducto Base
                lvi.SubItems.Add(comProducto.descripcion)                                             '2  - Descripcion producto
            End If

            lvi.SubItems.Add("C")                                                                     '3  - Tipo de producto [L,E,C]
            lvi.SubItems.Add(objUtilBl.isDecimal(item.precioVentaCaja))                               '4  - Precio de venta
            lvi.SubItems.Add("")                                                                      '5  - Cajas ingresadas
            lvi.SubItems.Add(item.un_caja)                                                            '6  - Unidades ingresadas
            lvi.SubItems.Add(item.importeCaja + item.importeCajaIva)                                  '7  - Importe
            lvi.SubItems.Add("")                                                                      '8  - Unidades caja Total
            lvi.SubItems.Add(item.un_caja)                                                            '09 - Unidades Liquido Total
            lvi.SubItems.Add(comProducto.idCaja)                                                      '10 - idProducto 
            lvi.SubItems.Add(item.precioUnitarioCaja)                                                 '11 - Precio sin iva
            lvi.SubItems.Add(item.importeCaja)                                                        '12 - importe sin iva
            lvi.SubItems.Add(0)                                                                       '13 - Litros
            lvi.SubItems.Add(0)                                                                       '14 - % Descuento
            lvi.SubItems.Add(FormatCurrency(0, 2))                                                    '15 - Monto Descuento
            lvi.SubItems.Add(0)                                                                       '16 - Sub total
            lvi.SubItems.Add(0)                                                                       '17 - iva del descuento
            lvi.SubItems.Add(item.importeCaja + item.importeCajaIva)                                  '18 - Importe para igualar Despacho
            lstAgregados.Items.Add(lvi)
            isIngresado = True

        End If


        '--------------------------------------------------------------------------------------'
        '--------------------------------    Linea Total   ------------------------------------'
        '--------------------------------------------------------------------------------------'
        If isIngresado = True Then
            Dim lvi As New ListViewItem(id_glo_producto.ToString)
            lvi.SubItems.Add("S U B")
            lvi.SubItems.Add("T O T A L")
            lvi.SubItems.Add("")
            lvi.SubItems.Add("")
            lvi.SubItems.Add("")
            lvi.SubItems.Add("")
            lvi.SubItems.Add(FormatCurrency(item.importeCaja + item.importeCajaIva + item.importeEnvase + item.importeEnvaseIva + item.importeLiquido + item.importeLiquidoIva, 2))
            lvi.SubItems.Add("")
            lvi.SubItems.Add("")
            lvi.SubItems.Add("")
            lvi.SubItems.Add("")
            lvi.SubItems.Add("")
            lvi.BackColor = xoWarning
            lvi.ForeColor = Color.Black
            lstAgregados.Items.Add(lvi)
        End If

        'Actualizar el label que muestra el importe acumulado
        calcularImporte()
        End If

        

    End Sub

    Public Sub calcularImporte()
        Dim importe As Decimal
        Dim PorcentajeNC As Integer
        comDocumento.importeLiquido = 0
        comDocumento.litrosAcumulados = 0
        'Agregue esto en esta ultima versión
        importe = 0
        'MessageBox.Show("Envase permitido " & envase_permitido)


        '--- Calcular el importe total del documento
        For i As Integer = 0 To lstAgregados.Items.Count() - 1
            If lstAgregados.Items(i).SubItems(1).Text <> "S U B" Then
                If id_glo_aplicacion = "inventario" Then
                    importe += objUtilBl.isDecimal(lstAgregados.Items(i).SubItems(7).Text)
                End If
                If id_glo_aplicacion = "venta" Or id_glo_aplicacion = "nc" Then
                    importe += objUtilBl.isDecimal(lstAgregados.Items(i).SubItems(18).Text)
                End If
                If id_glo_aplicacion = "despacho" Then
                    importe += objUtilBl.isDecimal(lstAgregados.Items(i).SubItems(22).Text)
                End If
                comDocumento.litrosAcumulados += objUtilBl.isDecimal(lstAgregados.Items(i).SubItems(13).Text)
                If lstAgregados.Items(i).SubItems(3).Text = "L" Then

                    '--- Importe total del liquido
                    comDocumento.importeLiquido += objUtilBl.isDecimal(lstAgregados.Items(i).SubItems(7).Text)
                End If
            End If
        Next


        PorcentajeNC = objUtil.getPorcentaje()
        
        If id_glo_aplicacion = "despacho" Then
            total_venta = 0
            total_envase = 0
            envase_permitido = 0

            total_venta = total_venta + importe
            If (comDocumento.importeDesto < 0) Then
                envase_permitido = objUtilBl.isDecimal(((total_venta - Abs(CInt(comDocumento.importeDesto))) * PorcentajeNC) / 100)
                txtEnvaceA.Text = objUtilBl.isDecimal(((total_venta - Abs(CInt(comDocumento.importeDesto))) * PorcentajeNC) / 100)

            Else
                txtEnvaceA.Text = objUtilBl.isDecimal(((total_venta) * PorcentajeNC) / 100)
                envase_permitido = objUtilBl.isDecimal(((total_venta) * PorcentajeNC) / 100)
            End If
            
        End If

        If id_glo_aplicacion = "venta" Then
            total_venta = 0
            total_envase = 0
            envase_permitido = 0
            total_venta = total_venta + importe
            If (comDocumento.importeDesto < 0) Then
                txtEnvaceA.Text = objUtilBl.isDecimal(((total_venta - Abs(CInt(comDocumento.importeDesto))) * PorcentajeNC) / 100)

            Else
                txtEnvaceA.Text = objUtilBl.isDecimal(((total_venta) * PorcentajeNC) / 100)
                envase_permitido = objUtilBl.isDecimal(((total_venta) * PorcentajeNC) / 100)
            End If
        End If


        If (id_glo_aplicacion2 = "cobro") Then
            id_glo_aplicacion2 = ""
            If (comDocumento.importeDesto < 0) Then
                txtEnvaceA.Text = objUtilBl.isDecimal(((total_venta - Abs(CInt(comDocumento.importeDesto))) * PorcentajeNC) / 100)
                envase_permitido = objUtilBl.isDecimal(((total_venta - Abs(CInt(comDocumento.importeDesto))) * PorcentajeNC) / 100)
            Else
                txtEnvaceA.Text = objUtilBl.isDecimal(((total_venta) * PorcentajeNC) / 100)
                envase_permitido = objUtilBl.isDecimal(((total_venta) * PorcentajeNC) / 100)
            End If

        Else
            If id_glo_aplicacion = "nc" Then
                If CInt(comDocumento.importeDesto) = 0 Then
                    total_envase = importe
                    txtEnvaceA.Text = envase_permitido
                Else
                    total_envase = importe
                    envase_permitido = objUtilBl.isDecimal(((total_venta - Abs(CInt(comDocumento.importeDesto))) * PorcentajeNC) / 100)
                    txtEnvaceA.Text = envase_permitido

                End If
                
            End If
        End If

        




        lblImporte.Tag = importe
        lblImporte.Text = FormatCurrency(importe, 2)
        lblImporteDespacho.Text = FormatCurrency(importe, 2)
        ltsAcumulados.Text = comDocumento.litrosAcumulados.ToString()
        lblVariacionImporte.Text = FormatCurrency(comDocumento.importePedido - importe, 2)


    End Sub
#End Region

#Region "Modificar item del listado de productos agregados. "
    Private Sub actualizarItem()
        Dim i As Integer = 0
        Dim flag As Boolean = True

        'Eliminar los items actuales
        While flag
            Try
                If id_glo_producto.ToString = lstAgregados.Items(i).SubItems(0).Text Then
                    lstAgregados.Items.RemoveAt(i)
                    i = 0
                Else
                    i = i + 1
                    If i = lstAgregados.Items.Count() Then
                        flag = True
                    End If
                End If
            Catch ex As Exception
                flag = False
            End Try
        End While

        'Agregar las lineas actuales
        isIngresado = False

        'Agregar el item nuevo
        If Not isforDelete Then
            agregarItem()
        Else
            isforDelete = False
        End If
    End Sub
#End Region

#Region " ELIMINAR EL ITEM DEL LISTADO DE PRODUCTOS AGREGADOS "
    Private Sub cmdEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEliminar.Click
        eliminaritem()
    End Sub

    Private Sub eliminaritem()
        lstProductos.Enabled = True
        picBusca.Enabled = True
        picCerrar.Enabled = True
        ComboBox1.Enabled = True
        txtBuscar.Enabled = True
        If panIngresoC.Enabled Then

            '--- En autoventa oculta el panel y elimina el item.
            isforDelete = True
            limpiarTxtIngreso()
            evaluarIngreso(False)
            calcularImporte()
            lstProductos.Enabled = True

        Else

            '--- En despacho solo oculta el panel.
            lstProductos.Enabled = True
            panIngreso.Visible = False

        End If
    End Sub
#End Region

#Region "Consultar articulos agregados. "
    Private Sub lSoftConsulta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lSoftConsulta.Click
        lstAgregados.Visible = True
    End Sub
#End Region

#Region "Grabar items en la base de datos. "

    Private Sub cmdSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSiguiente.Click
        Cursor.Current = Cursors.WaitCursor

        'If (id_glo_aplicacion = "nc") Then
        'If (total_envase > txtEnvaceA.Text) Then
        'MessageBox.Show("LA NOTA DE CREDITO EXCEDE EL TOTAL DEL DOCUMENTO DE REFERENCIA MONTO ACEPTADO ")
        'Cursor.Current = Cursors.Default
        'Return
        'End If
        'End If

        co_glo_NextForm = True

        '--- Verificar si el listado de agregados tiene items


        If tieneItems() Then

            'Mostrar el mensaje de confirmacion con items
            mensajeConfirmacion(True, response)
            If response = MsgBoxResult.Yes Then


                '--- Calcular los descuentos de temporada alta 
                'objDocumentoBL.calcularDescuentoFactura(v_com_cliente, lstAgregados, comDocumento)

                '--- Grabar la informacion y continuar a la siguiente pantalla
                grabarItems()
                proximaForma(True)
            Else
                '--- Permanecer en esta pantalla
                Cursor.Current = Cursors.Default
                Return
            End If
        Else
            'El listado de articulos agregados no tiene items 
            'Mostrar el mensaje de confirmacion sin items
            mensajeConfirmacion(False, response)
            If response = MsgBoxResult.Yes Then
                'co_glo_NextForm = False
                comPaso = 1
                comCorriendo = False
                proximaForma(False)

                Select Case id_glo_aplicacion
                    Case "venta"
                        rlayer.codigo = 2
                    Case "inventario"
                        rlayer.codigo = 3
                    Case Else
                        Cursor.Current = Cursors.Default
                End Select
            Else
                Cursor.Current = Cursors.Default
                Return
            End If
        End If


        'Determinar si salir a la pantalla de atencion
        If co_glo_salir Then
            Me.Close()
        End If
    End Sub

    Private Function tieneItems() As Boolean
        '---Contar los items agregados en el listView
        If lstAgregados.Items.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub mensajeConfirmacion(ByVal conItems As Boolean, ByRef response As MsgBoxResult)

        '--- Mensajes a mostrar cuando hay items seleccionados
        If conItems Then
            Select Case id_glo_aplicacion
                Case "inventario"
                    msg = "Grabar el inventario?"
                    title = "Modulo de inventario"
                Case "venta", "despacho"
                    msg = "Grabar la orden y continuar con el pago?"
                    title = "Modulo de venta"
                Case "nc"
                    msg = "Grabar la devolucion y continuar?"
                    title = "Recoleccion de envase"
                Case "cambio"
                    msg = "Dese grabar el documento de cambio?"
                    title = "Modulo de cambio"

            End Select
            style = MsgBoxStyle.YesNo
        Else
            '--- Mensajes a mostrar cuando no se seleccionaron items
            Select Case id_glo_aplicacion
                Case "inventario"
                    msg = "No se agregaron items, desea cancelar?"
                    title = "Modulo de inventario"
                Case "venta"
                    msg = "No se agregaron items, desea cancelar?"
                    title = "Modulo de venta"
                Case "nc"
                    msg = "No se agregaron items, desea continuar?"
                    title = "Recoleccion de envase"
                Case "cambio"
                    msg = "No grabo el documento de cambio?"
                    title = "Modulo de cambio"
            End Select

            style = MsgBoxStyle.YesNo
        End If
        response = MsgBox(msg, style, title)
    End Sub

    Private Sub proximaForma(ByVal conItems As Boolean)

        '--- Formulario a mostrar cuando hay items seleccionados
        If conItems Then

            Select Case id_glo_aplicacion

                Case "inventario"
                    '--- Ir a menu de atencion
                    Me.Close()

                Case "venta"
                    co_glo_salir = True
                    glo_lvVentaAgregados = lstAgregados
                    glo_dtVentaProductos = dtProductos

                Case "nc"
                    co_glo_salir = True
                    glo_lvNcAgregados = lstAgregados
                    glo_dtNcProductos = dtProductos

            End Select
        Else
            '--- Formulario a mostrar cuando no se seleccionaron items
            Select Case id_glo_aplicacion

                Case "inventario"
                    'Ir a menu atencion
                    Me.Close()

                Case "venta"
                    'Ir a menu atencion
                    Me.Close()

                Case "nc"
                    glo_lvNcAgregados = lstAgregados
                    glo_dtNcProductos = dtProductos
            End Select
        End If
    End Sub

    Private Sub grabarItems()
        Try
            Select Case id_glo_aplicacion
                Case "nc"
                    '--- Capturar datos de la Nota de Credito

                    comDocumento.importe = lblImporte.Tag
                    comDocumento.estado = 0
                    comDocumento.ttipo = 4              'Nota de credito por devolucion de envase
                    comDocumento.tipoDevolucion = 2     'Nota de credito por devolucion de envase              

                Case "venta"
                    '--- Capturar datos de la orden de venta
                    comDocumento.importe = lblImporte.Tag
                    comDocumento.estado = 0
                    comDocumento.ttipo = "ODV"
            End Select
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

#End Region

#Region " LSOFT Regresar una pantalla "
    Private Sub lSoft1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lSoft1.Click
        Cursor.Current = Cursors.WaitCursor
        '--- Pantalla anterior
        Select Case id_glo_aplicacion
            Case "venta", "despacho", "nc"
                '--- Pantalla anterior               
                rlayer.codigo = 2
                Me.Close()

            Case "inventario"
                msg = "Desea cancelar la toma de inventario?"
                title = "Cancelar"
                style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.YesNo
                response = MsgBox(msg, style, title)
                If response = MsgBoxResult.Yes Then
                    rlayer.codigo = 3
                    Me.Close()
                Else
                    Cursor.Current = Cursors.Default
                End If
            Case Else
                Cursor.Current = Cursors.Default
        End Select
    End Sub
#End Region

#Region " LSOFT Cancelar Operacion "
    Private Sub lSoftCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lSoftCancelar.Click
        '---Codigo de aborto de la operacion
        rlayer.codigo = 3
        Me.Close()
    End Sub
#End Region

#Region " CREAR LISTADO DE PRODUCTOS Y LISTADO DE AGREGADOS "
#Region " DESPACHO "
    Private Sub desplegarListaAgregados()

        '--- Crear encabezados y columnas del Listview
        Dim correlativo = New ColumnHeader()
        Dim codigo = New ColumnHeader()
        Dim descripcion = New ColumnHeader()
        Dim mercaderia = New ColumnHeader()
        Dim precio = New ColumnHeader()
        Dim cajas = New ColumnHeader()
        Dim unidades = New ColumnHeader()
        Dim importe = New ColumnHeader()
        Dim cajaDm = New ColumnHeader()
        Dim unidadDm = New ColumnHeader()
        Dim codigoDm = New ColumnHeader()
        Dim precioSinIva = New ColumnHeader()
        Dim importeSinIva = New ColumnHeader()
        Dim litros = New ColumnHeader()
        Dim descuento = New ColumnHeader()
        Dim porcentajeDescuento = New ColumnHeader()
        Dim sub_total = New ColumnHeader()
        Dim unidades_despacho = New ColumnHeader()
        Dim importeDespacho = New ColumnHeader()
        Dim descuento_despacho = New ColumnHeader()
        Dim subtotal_despacho = New ColumnHeader()
        Dim precioDespacho = New ColumnHeader()
        Dim importeSinIvaDespacho = New ColumnHeader()
        Dim trqtDespacho = New ColumnHeader()
        Dim porcentajeDesto = New ColumnHeader()

        '--- Texto de los encabezados
        correlativo.Text = ""                           '0
        correlativo.Width = 0
        codigo.Text = "Codigo"                          '1
        descripcion.Text = "Descripcion"                '2
        mercaderia.Text = "Mat."                        '3
        precio.Text = "Precio|P"                        '4
        cajas.Text = "CJ"                               '5
        unidades.Text = "UN"                            '6
        importe.Text = "Importe|P"                      '7
        cajaDm.Text = "CJDM"                            '8
        cajaDm.Width = 0
        unidadDm.Text = "UNDM"                          '9
        unidadDm.Width = 0
        codigoDm.Text = "CODIGODM"                      '10
        codigoDm.Width = 0
        precioSinIva.Text = "PRECIO_SIN_IVA"            '11
        precioSinIva.Width = 0
        importeSinIva.Text = "IMPORTE_SIN_IVA"          '12
        importeSinIva.Width = 0
        litros.Text = "Lts."                            '13
        porcentajeDescuento.Text = "%.Desto|P"          '14
        porcentajeDescuento.Width = 0
        descuento.Text = "Desto"                        '15
        sub_total.Text = "Sub. Total"                   '16
        unidades_despacho.Text = "UN|D"                 '17
        importeDespacho.Text = "Importe|D"              '18
        descuento_despacho.Text = "Desto|D"             '19
        subtotal_despacho.Text = "Sub Total|D"          '20
        precioDespacho.Text = "Precio|D"                '21
        importeSinIvaDespacho.Text = "Importe -IVA"     '22
        trqtDespacho.Text = "CJ/UN |D"                  '23
        trqtDespacho.Text = "% |D"                  '24

        '--- Agregar columnas base
        lstAgregados.Columns.Add(correlativo)           '0
        lstAgregados.Columns.Add(codigo)                '1
        lstAgregados.Columns.Add(descripcion)           '2
        lstAgregados.Columns.Add(mercaderia)            '3 
        lstAgregados.Columns.Add(precio)                '4
        lstAgregados.Columns.Add(cajas)                 '5
        lstAgregados.Columns.Add(unidades)              '6
        lstAgregados.Columns.Add(importe)               '7
        lstAgregados.Columns.Add(cajaDm)                '8
        lstAgregados.Columns.Add(unidadDm)              '9
        lstAgregados.Columns.Add(codigoDm)              '10 
        lstAgregados.Columns.Add(precioSinIva)          '11
        lstAgregados.Columns.Add(importeSinIva)         '12
        lstAgregados.Columns.Add(litros)                '13
        lstAgregados.Columns.Add(porcentajeDescuento)   '14
        lstAgregados.Columns.Add(descuento)             '15
        lstAgregados.Columns.Add(sub_total)             '16
        lstAgregados.Columns.Add(unidades_despacho)     '17 
        lstAgregados.Columns.Add(subtotal_despacho)     '18 
        lstAgregados.Columns.Add(importeSinIvaDespacho) '19 
        lstAgregados.Columns.Add(trqtDespacho)          '20 
        lstAgregados.Columns.Add(precioDespacho)        '21 
        lstAgregados.Columns.Add(importeDespacho)       '22 
        lstAgregados.Columns.Add(descuento_despacho)    '23

        '--- Ocultas por defecto
        'importeSinIvaDespacho.Width = 0
        'unidades_despacho.Width = 0
        'sub_total.Width = 0
        'subtotal_despacho.Width = 0
        'litros.Width = 0


        ''--- Cambiar titulo por contexto
        'If Not comDocumento.isContado Then
        '    descuentoP.Text = "DescPP|P"
        '    descuentoD.Text = "DescPP|D"
        'End If

    End Sub
    Private Sub desplegarListaProductos()


        Dim codigo = New ColumnHeader()         '0
        Dim descripcion = New ColumnHeader()    '1
        Dim pedido = New ColumnHeader()         '2
        Dim despacho = New ColumnHeader()       '3
        Dim importeP = New ColumnHeader()       '4
        Dim importeD = New ColumnHeader()       '5
        Dim descuentoP = New ColumnHeader()     '6
        Dim descuentoD = New ColumnHeader()     '7

        codigo.Text = "Codigo"
        descripcion.Text = "Descripcion"
        pedido.Text = "Pedido"
        despacho.Text = "Despacho"
        importeP.Text = "Importe|P"
        importeD.Text = "Importe|D"
        descuentoP.Text = "Desc.|P"
        descuentoD.Text = "Desc.|D"

        lstProductos.Columns.Add(codigo)
        lstProductos.Columns.Add(descripcion)
        lstProductos.Columns.Add(pedido)
        lstProductos.Columns.Add(despacho)
        lstProductos.Columns.Add(importeP)
        lstProductos.Columns.Add(importeD)
        lstProductos.Columns.Add(descuentoP)
        lstProductos.Columns.Add(descuentoD)


        '--- Ocultar columnas
        If comDocumento.isDiferente Then
            descuentoP.Width = 0
            importeP.Width = 0
        Else
            descuentoD.Width = 0
            importeD.Width = 0
        End If

        '--- Cambiar titulo por contexto
        If Not comDocumento.isContado Then
            descuentoP.Text = "DescPP|P"
            descuentoD.Text = "DescPP|D"
        End If
    End Sub
#End Region
#Region " AUTOVENTA "
    Private Sub crearLwProductos()
        Dim vProductos As New DataView
        Dim dtDisplayProductos As New DataTable
        Dim pDisponible As Decimal
        Dim producto As New ProductoCO
        Dim objproducto As New ProductoBL

        '--- Limpiar la lista
        lstProductos.Items.Clear()
        progBusqueda.Visible = True
        lstProductos.Columns(0).Width = 120
        
        '--- Filtrar productos
        vProductos = dtProductos.DefaultView
        vProductos.RowFilter = "display = 1  AND locked = 0"
        dtDisplayProductos = vProductos.ToTable
        progBusqueda.Maximum = dtDisplayProductos.Rows.Count

        '--- Agregar filas a la lista
        For i As Integer = 0 To dtDisplayProductos.Rows.Count - 1
            Dim drow As DataRow = dtDisplayProductos.Rows(i)
            Dim unidades, cajas As String

            '--- Agregar Items a la fila
            Dim lvi As New ListViewItem(drow("codigo").ToString())
            lvi.SubItems.Add(Trim(drow("descripcion").ToString()))

            '--- Convertir unidades iniciales a cajas
            producto.unidadesCaja = objUtil.isDecimal(drow("unidadesCaja").ToString())
            unidades = objUtil.isDecimal(drow("cantidadActual").ToString)
            cajas = "0"
            objproducto.convertirUnidadesAcajas(producto, unidades, cajas)
            lvi.SubItems.Add(cajas & "/" & unidades)
            lstProductos.Items.Add(lvi)

            '--- Colorear items agregados
            If (drow("agregado").ToString = "1") Then
                lstProductos.Items(lstProductos.Items.Count - 1).ImageIndex = 0
                lstProductos.Items(lstProductos.Items.Count - 1).BackColor = xoInfomat
            End If

            '--- Colorea la disponibilidad 
            If id_glo_aplicacion = "venta" Then
                pDisponible = (objUtilBl.isDecimal(drow("cantidadActual")) * 100) / objUtilBl.isDecimal(drow("cantidadInicial"))
                If (pDisponible > 15 And pDisponible <= 30) Then
                    lvi.BackColor = xoInfomat
                End If

                If pDisponible <= 15 And pDisponible > 0 Then
                    lvi.BackColor = xoWarning
                End If
            End If
        Next
        dtProductos = dtProductosFull.Copy
        progBusqueda.Visible = False
        If lstProductos.Items.Count > 0 Then lstProductos.Items.Item(0).Selected() = True
    End Sub

    Private Sub crearLwProductosV()
        Dim vProductos As New DataView
        Dim dtDisplayProductos As New DataTable
        Dim pDisponible As Decimal
        Dim producto As New ProductoCO
        Dim objproducto As New ProductoBL

        '--- Limpiar la lista
        lstProductos.Items.Clear()
        progBusqueda.Visible = True
        lstProductos.Columns(0).Width = 120

        '--- Filtrar productos
        vProductos = dtProductos.DefaultView
        vProductos.RowFilter = "display = 1 and locked = 0"
        dtDisplayProductos = vProductos.ToTable
        progBusqueda.Maximum = dtDisplayProductos.Rows.Count

        '--- Agregar filas a la lista
        For i As Integer = 0 To dtDisplayProductos.Rows.Count - 1
            Dim drow As DataRow = dtDisplayProductos.Rows(i)
            Dim unidades, cajas As String

            '--- Agregar Items a la fila
            Dim lvi As New ListViewItem(drow("codigo").ToString())
            lvi.SubItems.Add(Trim(drow("descripcion").ToString()))

            '--- Convertir unidades iniciales a cajas
            producto.unidadesCaja = objUtil.isDecimal(drow("unidadesCaja").ToString())
            unidades = objUtil.isDecimal(drow("cantidadActual").ToString)
            cajas = "0"
            objproducto.convertirUnidadesAcajas(producto, unidades, cajas)
            lvi.SubItems.Add(cajas & "/" & unidades)
            lstProductos.Items.Add(lvi)

            '--- Colorear items agregados
            If (drow("agregado").ToString = "1") Then
                lstProductos.Items(lstProductos.Items.Count - 1).ImageIndex = 0
                lstProductos.Items(lstProductos.Items.Count - 1).BackColor = xoInfomat
            End If

            '--- Colorea la disponibilidad 
            If id_glo_aplicacion = "venta" Then
                pDisponible = (objUtilBl.isDecimal(drow("cantidadActual")) * 100) / objUtilBl.isDecimal(drow("cantidadInicial"))
                If (pDisponible > 15 And pDisponible <= 30) Then
                    lvi.BackColor = xoInfomat
                End If

                If pDisponible <= 15 And pDisponible > 0 Then
                    lvi.BackColor = xoWarning
                End If
            End If
        Next
        dtProductos = dtProductosFull.Copy
        progBusqueda.Visible = False
        If lstProductos.Items.Count > 0 Then lstProductos.Items.Item(0).Selected() = True
    End Sub

    Private Sub crearLwAgregados()

        lstAgregados.Clear()

        '--- Crear encabezados y columnas del Listview
        Dim correlativo = New ColumnHeader()
        Dim codigo = New ColumnHeader()
        Dim descripcion = New ColumnHeader()
        Dim mercaderia = New ColumnHeader()
        Dim precio = New ColumnHeader()
        Dim cajas = New ColumnHeader()
        Dim unidades = New ColumnHeader()
        Dim importe = New ColumnHeader()
        Dim cajaDm = New ColumnHeader()
        Dim unidadDm = New ColumnHeader()
        Dim codigoDm = New ColumnHeader()
        Dim precioSinIva = New ColumnHeader()
        Dim importeSinIva = New ColumnHeader()
        Dim litros = New ColumnHeader()
        Dim descuento = New ColumnHeader()
        Dim porcentajeDescuento = New ColumnHeader()

        '--- Texto de los encabezados
        correlativo.Text = "0"                  '0
        codigo.Text = "Cod."                    '1
        descripcion.Text = "Descripcion"        '2
        mercaderia.Text = "M"                   '3
        precio.Text = "Precio"                  '4
        cajas.Text = "CJ"                       '5
        unidades.Text = "UN"                    '6
        importe.Text = "Q."                     '7
        cajaDm.Text = "CJDM"                    '8
        unidadDm.Text = "UNDM"                  '9
        codigoDm.Text = "CODIGODM"              '10
        precioSinIva.Text = "PRECIO_SIN_IVA"    '11
        importeSinIva.Text = "IMPORTE_SIN_IVA"  '12
        litros.Text = "LITROS"                  '13
        porcentajeDescuento.Text = "%.Desto"    '14
        descuento.Text = "QTZ"                  '15


        '--- Ancho de los encabezados
        correlativo.Width = 0
        codigo.Width = 120
        descripcion.Width = 350
        mercaderia.Width = 50
        precio.Width = 100
        cajas.Width = 50
        unidades.Width = 50
        importe.Width = 100
        cajaDm.Width = 0
        unidadDm.Width = 0
        codigoDm.Width = 0
        precioSinIva.Width = 0
        importeSinIva.Width = 0
        litros.Width = 100
        porcentajeDescuento.Width = 100
        descuento.Width = 100

        '--- Agregar columnas base
        lstAgregados.Columns.Add(correlativo)           '0
        lstAgregados.Columns.Add(codigo)                '1
        lstAgregados.Columns.Add(descripcion)           '2
        lstAgregados.Columns.Add(mercaderia)            '3
        lstAgregados.Columns.Add(precio)                '4
        lstAgregados.Columns.Add(cajas)                 '5
        lstAgregados.Columns.Add(unidades)              '6
        lstAgregados.Columns.Add(importe)               '7
        lstAgregados.Columns.Add(cajaDm)                '8
        lstAgregados.Columns.Add(unidadDm)              '9
        lstAgregados.Columns.Add(codigoDm)              '10
        lstAgregados.Columns.Add(precioSinIva)          '11
        lstAgregados.Columns.Add(importeSinIva)         '12
        lstAgregados.Columns.Add(litros)                '13
        lstAgregados.Columns.Add(porcentajeDescuento)   '14
        lstAgregados.Columns.Add(descuento)             '15

        If lstAgregadosExternal.Items.Count() > 0 Then
            For i As Integer = 0 To lstAgregadosExternal.Items.Count() - 1

                '--- Agregar Items a la fila
                Dim lvi As New ListViewItem(lstAgregadosExternal.Items.Item(i).SubItems(0).Text)
                lvi.SubItems.Add(lstAgregadosExternal.Items.Item(i).SubItems(1).Text)
                lvi.SubItems.Add(Trim(lstAgregadosExternal.Items.Item(i).SubItems(2).Text))
                lvi.SubItems.Add(lstAgregadosExternal.Items.Item(i).SubItems(3).Text)
                lvi.SubItems.Add(lstAgregadosExternal.Items.Item(i).SubItems(4).Text)
                lvi.SubItems.Add(lstAgregadosExternal.Items.Item(i).SubItems(5).Text)
                lvi.SubItems.Add(lstAgregadosExternal.Items.Item(i).SubItems(6).Text)
                lvi.SubItems.Add(lstAgregadosExternal.Items.Item(i).SubItems(7).Text)
                lvi.SubItems.Add(lstAgregadosExternal.Items.Item(i).SubItems(8).Text)
                lvi.SubItems.Add(lstAgregadosExternal.Items.Item(i).SubItems(9).Text)
                lvi.SubItems.Add(lstAgregadosExternal.Items.Item(i).SubItems(10).Text)
                lvi.SubItems.Add(lstAgregadosExternal.Items.Item(i).SubItems(11).Text)
                lvi.SubItems.Add(lstAgregadosExternal.Items.Item(i).SubItems(12).Text)
                lvi.SubItems.Add(lstAgregadosExternal.Items.Item(i).SubItems(13).Text)
                lvi.SubItems.Add(lstAgregadosExternal.Items.Item(i).SubItems(14).Text)
                lvi.SubItems.Add(lstAgregadosExternal.Items.Item(i).SubItems(15).Text)
                lvi.SubItems.Add(lstAgregadosExternal.Items.Item(i).SubItems(16).Text)
                lstAgregados.Items.Add(lvi)
            Next
        End If
    End Sub
#End Region
#End Region

#Region " PAINTERS / HELPERS / UI "
    Private Sub txtBuscar_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub picMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picMenu.Click

        If panAdicionales.Visible Then
            panAdicionales.Visible = False
        Else
            panAdicionales.Visible = True
            panAdicionales.Focus()
        End If
    End Sub
    Private Sub lstProductos_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lstProductos.KeyPress
        If e.KeyChar = "I" Or e.KeyChar = "i" Or e.KeyChar = "g" Or e.KeyChar = "G" Then
            ingresarProducto()
        End If
        If e.KeyChar = "B" Or e.KeyChar = "b" Or e.KeyChar = "a" Or e.KeyChar = "A" Then
            txtBuscar.Focus()
        End If

        If e.KeyChar = "C" Or e.KeyChar = "c" Or e.KeyChar = "d" Or e.KeyChar = "D" Then
            lstAgregados.Visible = True
        End If
    End Sub
    Private Sub lstAgregados_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lstAgregados.KeyPress
        If e.KeyChar = "I" Or e.KeyChar = "i" Then
            ingresarProducto()
        End If
    End Sub
    Private Sub panAdicionales_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panAdicionales.Paint
        objUtilBl.paintPannel(e, panAdicionales)
    End Sub
    Private Sub picBusca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picBusca.Click
        panBusca.Visible = True
        lblNombreCliente.Visible = False
        lblAtencion.Visible = False
        picBusca.Visible = False
        picCerrar.Visible = True
        'txtBuscar.Text = "Escriba su busqueda"
        txtBuscar.Focus()
        txtBuscar.SelectAll()
        InputPanel1.Enabled = True
    End Sub
    Private Sub picCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picCerrar.Click
        panBusca.Visible = False
        lblNombreCliente.Visible = True
        lblAtencion.Visible = True
        txtBuscar.SelectAll()
        txtBuscar.Text = If(co_glo_searchProducto = "descripcion", "", "0")
        picBusca.Visible = True
        picCerrar.Visible = False
        InputPanel1.Enabled = False
        wtBuscarItem()
    End Sub
    Private Sub txtBuscar_LostFocus_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuscar.LostFocus
        InputPanel1.Enabled = False
    End Sub
    Private Sub lnkMas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkMas.Click
        '--- Extender el ancho de las columnas ocultas igual a su vecino de la derecha
        lstProductos.Columns.Item(4).Width = lstProductos.Columns.Item(0).Width
        lstProductos.Columns.Item(6).Width = lstProductos.Columns.Item(0).Width
        lstProductos.Columns.Item(5).Width = lstProductos.Columns.Item(0).Width
        lstProductos.Columns.Item(7).Width = lstProductos.Columns.Item(0).Width
    End Sub
    Private Sub panAdicionales_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles panAdicionales.LostFocus
        panAdicionales.Visible = False
    End Sub
#End Region

#Region " INVENTARIO COMPROMETIDO "
    Private Function inventarioComprometido(ByVal codigoLiquido As String, ByVal codigoCaja As String) As Integer
        Dim cantidad As Integer = 0
        For i As Integer = 0 To lstAgregados.Items.Count() - 1

            If (lstAgregados.Items(i).SubItems(10).Text = codigoCaja) And codigoLiquido <> lstAgregados.Items(i).SubItems(0).Text Then
                '--- Obtener el numero de unidades
                cantidad += objUtilBl.isInteger(lstAgregados.Items(i).SubItems(6).Text)
            End If
        Next
        Return cantidad
    End Function
#End Region

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        lstAgregados.Visible = True
    End Sub
End Class

