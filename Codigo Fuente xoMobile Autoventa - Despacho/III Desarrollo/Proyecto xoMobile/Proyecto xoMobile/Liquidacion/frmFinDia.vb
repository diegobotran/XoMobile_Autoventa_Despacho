Imports System.Data
Imports DataGridCustomColumns
Imports Proyecto_xoMobile_Packs

Public Class frmFinDia
    Dim objutil As New UtilitarioBL
    Dim objProducto As New ProductoBL
    Dim objRuta As New RutaBL
    Dim objInventario As New InventarioBL
    Dim dtDepositos As New DataTable
    Dim dtBancos, dtMotivosDiferencia As New DataTable
    Dim isForUpdate As Boolean = False
    Dim idDeposito As Integer
    Dim ruta As New RutaCO
    Dim Integracion As New IntegracionCO
    Dim totalDepositar As Decimal
    Dim depositoActual As Decimal
    Dim totalDiferencia As Decimal
    Dim totalDiferenciaDeclarada As Decimal

    Dim cupons As Decimal = 0
    Dim cupon2 As Decimal = 0
    Dim cupon5 As Decimal = 0

    Private dgDataSources As New DataSet



    Private Sub frmFinDia_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'txtClave.Text = "JS08051987"
        '--- Obtener la ruta activa
        ruta = objRuta.getActiva()

        '--- Inicializar controles
        inicializar()

        '--- Inicializar Grids
        inicializarMovimientos()
        inicializarIntegracion()
        inicializarDepositos()

        '--- Inicializar diferencias
        inicializarDiferencias()

        actualizarLabelsFinancieros()

        '--- Habilitar controles si se cumplen las condiciones de ejecucion
        habilitarControles()
        setAlternatingRowColor(lstIntegracion)
        setAlternatingRowColor(lstMovimientos)
        Cursor.Current = Cursors.Default
    End Sub

#Region " INICIALIZAR FORMULARIO "
    Private Function habilitarControles() As Boolean

        '--- Habilitar controles de confirmacion
        Dim oBitacora As New BitacoraBL
        If oBitacora.isOperacionRealizada_xo(10) Then
            If oBitacora.isOperacionRealizada_xo(37) Then
                If oBitacora.isOperacionRealizada_xo(36) Then
                    panResultLogin.Visible = False
                Else
                    lblError.Text = "No ha liquidado en bodega de envase."
                    panResultLogin.Visible = True
                    deshabilitarControles(False)
                End If
            Else
                lblError.Text = "No ha liquidado en bodega de producto."
                panResultLogin.Visible = True
                deshabilitarControles(False)
            End If
        Else
            lblError.Text = "No ha respondido las preguntas finales."
            panResultLogin.Visible = True
            deshabilitarControles(False)
        End If

        '--- Si la operacion fue confirmada entonces habilitar controles para impresion
        If ruta.exportar Then
            deshabilitarControles(True)
            cmdConfirma.Text = "Imprimir"
            lblError.Text = "Ruta-" + Trim(ruta.codRuta) + " Liquidacion confirmada."
            panResultLogin.Visible = True
        End If
    End Function
    Private Sub inicializar()

        '--- Inicializar controles de mensaje
        lblDeclaro.Text = lblDeclaro.Text + ruta.codRuta


        '--- Cargar Combos
        dtBancos = objutil.listarSeleccion("BANCOS")
        dtMotivosDiferencia = objutil.getListaTipo("FALTANTES','SOBRANTES", "showValue")
        lstBancosOut.DataSource = dtBancos
        Try
            lstBancosOut.SelectedIndex = 11
        Catch ex As Exception
            '--- Selecciona el default
        End Try

        lstMotivoDiferencia.DataSource = dtMotivosDiferencia
        lstBancosOut.ValueMember = dtBancos.Columns(1).ToString
        lstBancosOut.DisplayMember = dtBancos.Columns(0).ToString
        lstMotivoDiferencia.ValueMember = dtMotivosDiferencia.Columns(1).ToString
        lstMotivoDiferencia.DisplayMember = dtMotivosDiferencia.Columns(0).ToString

        '---Inicializar mensaje de controles


    End Sub
    Private Sub actualizarLabelsFinancieros()
        '--- Inicializar labels financieros

        lblDeposito1.Text = FormatCurrency(totalDepositar, 2)
        lblDeposito2.Text = FormatCurrency(totalDepositar, 2)
        lblDeposito3.Text = FormatCurrency(totalDepositar, 2)
        lblDeposito4.Text = FormatCurrency(totalDepositar, 2)

        lblDepositado2.Text = FormatCurrency(depositoActual, 2)
        lblDepositado3.Text = FormatCurrency(depositoActual, 2)
        lblDepositado4.Text = FormatCurrency(depositoActual, 2)

        lblDiferencia1.Text = FormatCurrency(totalDiferencia + totalDiferenciaDeclarada, 2)
        lblDiferencia2.Text = FormatCurrency(totalDiferencia + totalDiferenciaDeclarada, 2)
        lblDiferencia3.Text = FormatCurrency(totalDiferencia + totalDiferenciaDeclarada, 2)
        lblDiferencia4.Text = FormatCurrency(totalDiferencia + totalDiferenciaDeclarada, 2)

        lblDiferenciaReportada.Text = FormatCurrency(totalDiferenciaDeclarada, 2)
        lblTotalDiferencia.Text = FormatCurrency(totalDiferenciaDeclarada, 2)

        If (totalDiferencia + totalDiferenciaDeclarada) = 0 Then
            cmdConfirma.Enabled = True
            panResultLogin.Visible = False
        Else
            cmdConfirma.Enabled = False
            lblError.Text = " Aun tiene diferencias de efectivo "
        End If
    End Sub
    Private Sub escapeEdicion()
        lstBancosOut.BackColor = Color.White
        txtDocumentoOut.BackColor = Color.White
        txtValorOut.BackColor = Color.White
        lstBancosOut.SelectedIndex = 0
        txtDocumentoOut.Text = ""
        txtValorOut.Text = ""
        isForUpdate = False
        lstBancosOut.Focus()
    End Sub
    Private Sub deshabilitarControles(ByVal f As Boolean)
        cmdConfirma.Enabled = f
        panDeposito.Enabled = f
        lstDepositos.Enabled = f
        txtValorDiferencia.Enabled = f

        '--- Bloquear deposito
        lstDepositos.Enabled = False
        txtValorOut.Enabled = False

        '--- Bloquear Diferencias
        txtValorDiferencia.Enabled = False
        lstDiferencias.Enabled = False

    End Sub
    Private Sub inicializarDiferencias()
        Dim objRuta As New RutaBL
        Dim dtDiferencia As New DataTable
        Try
            dtDiferencia = objRuta.consultarDiferencias()
            For i As Integer = 0 To dtDiferencia.Rows.Count - 1
                Dim drow As DataRow = dtDiferencia.Rows(i)
                Dim lvi As New ListViewItem(Trim(drow("motivo").ToString()))
                lvi.SubItems.Add(FormatCurrency(drow("valor").ToString(), 2))
                lvi.SubItems.Add(drow("idMotivo").ToString)
                lvi.SubItems.Add(drow("tipoMotivo").ToString)
                lstDiferencias.Items.Add(lvi)
            Next
        Catch ex As Exception
            'MsgBox(ex.Message)
            'No quiero reportar el error... aunque es arriesgado
        End Try
        'End If
    End Sub
#End Region

#Region " INICIALIZAR GRIDS "
    Private Sub inicializarMovimientos()

        Dim producto As New ProductoCO

        Dim dtMovimiento As New DataTable
        Dim idProducto = New ColumnHeader()
        Dim descripcion = New ColumnHeader()
        Dim unidadesCaja = New ColumnHeader()
        Dim cantidadInicial = New ColumnHeader()
        Dim fisicoDevuelto = New ColumnHeader()
        Dim fisicoVendido = New ColumnHeader()
        Dim importeLiquido = New ColumnHeader()

        Dim sumImporteEnvase, sumImporteLiquido As Decimal
        Dim sumICJ, sumIUN, sumFCJ, sumFUN, sumVCJ, sumVUN As Integer

        lstDepositos.Clear()
        Try
            dtMovimiento = objInventario.consultarMovimientoLiquidacion()
            idProducto.Text = "Codigo"
            descripcion.Text = "Producto"
            cantidadInicial.Text = "Inicial."
            fisicoDevuelto.Text = "Venta"
            fisicoVendido.Text = "Devolucion"
            importeLiquido.Text = "Actual"
            unidadesCaja.Text = "un x Caja"

            lstMovimientos.Columns.Add(idProducto)
            lstMovimientos.Columns.Add(descripcion)
            lstMovimientos.Columns.Add(unidadesCaja)
            lstMovimientos.Columns.Add(cantidadInicial)
            lstMovimientos.Columns.Add(fisicoDevuelto)
            lstMovimientos.Columns.Add(fisicoVendido)
            lstMovimientos.Columns.Add(importeLiquido)

            lstDepositos.Items.Clear()
            For i As Integer = 0 To dtMovimiento.Rows.Count - 1
                Dim unidades, cajas As String
                Dim drow As DataRow = dtMovimiento.Rows(i)
                Dim lvi As New ListViewItem(drow("id_Producto").ToString())
                lvi.SubItems.Add(Trim(drow("descripcion").ToString()))
                lvi.SubItems.Add(Trim(drow("unidadesCaja").ToString()))

                '--- Convertir unidades iniciales a cajas
                producto.unidadesCaja = objutil.isDecimal(drow("unidadesCaja").ToString())
                unidades = drow("cantidadInicial").ToString
                cajas = "0"
                objProducto.convertirUnidadesAcajas(producto, unidades, cajas)
                lvi.SubItems.Add(cajas & "/" & unidades)

                '--- Convertir unidades vendidas a cajas
                unidades = drow("cantidadVendida").ToString
                cajas = "0"
                objProducto.convertirUnidadesAcajas(producto, unidades, cajas)
                lvi.SubItems.Add(cajas & "/" & unidades)

                '--- Convertir unidades devueltas a cajas
                unidades = drow("cantidadDevolucion").ToString
                cajas = "0"
                objProducto.convertirUnidadesAcajas(producto, unidades, cajas)
                lvi.SubItems.Add(cajas & "/" & unidades)

                '--- Convertir unidades actual a cajas
                unidades = drow("cantidadActual").ToString
                cajas = "0"
                objProducto.convertirUnidadesAcajas(producto, unidades, cajas)
                lvi.SubItems.Add(cajas & "/" & unidades)
                lstMovimientos.Items.Add(lvi)
            Next
            totalDepositar = sumImporteEnvase + sumImporteLiquido
            lblTotal.Text = FormatCurrency(totalDepositar, 2)
            lblTotalEnvase.Text = FormatCurrency(sumImporteEnvase, 2)
            lblTotalLiquido.Text = FormatCurrency(sumImporteLiquido, 2)
            lblTotalCarga.Text = sumICJ.ToString + "/" + sumIUN.ToString
            lblTotalDevolucion.Text = sumFCJ.ToString + "/" + sumFUN.ToString
            lblTotalVenta.Text = sumVCJ.ToString + "/" + sumVUN.ToString
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub inicializarDepositos()
        Dim objRuta As New RutaBL
        Dim banco = New ColumnHeader()
        Dim documento = New ColumnHeader()
        Dim valor = New ColumnHeader()
        Dim totalDeposito As Decimal

        If lstDepositos.Columns.Count = 0 Then
            banco.Text = "Banco"
            documento.Text = "Documento"
            valor.Text = "Valor"
            lstDepositos.Columns.Add(banco)
            lstDepositos.Columns.Add(documento)
            lstDepositos.Columns.Add(valor)
            lstDepositos.Items.Clear()
        End If

        Try
            lstDepositos.Items.Clear()
            dtDepositos = objRuta.obtenerDepositos()
            For i As Integer = 0 To dtDepositos.Rows.Count - 1
                Dim drow As DataRow = dtDepositos.Rows(i)
                Dim lvi As New ListViewItem(Trim(drow("SHOWVALUE").ToString()))
                lvi.SubItems.Add(drow("documento").ToString())
                lvi.SubItems.Add(FormatCurrency(drow("importe").ToString(), 2))
                totalDeposito = totalDeposito + drow("importe")
                lstDepositos.Items.Add(lvi)
            Next
            depositoActual = totalDeposito
            totalDiferencia = totalDepositar - depositoActual
            actualizarLabelsFinancieros()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub inicializarIntegracion()
        Dim objRuta As New RutaBL
        Dim dtIntegracion As New DataTable
        Dim dtCupones As New DataTable
        Dim partida = New ColumnHeader()
        Dim importe = New ColumnHeader()
        Try
            dtCupones = objRuta.obtenerCupones()
            dtIntegracion = objRuta.obtenerIntegracion()
            partida.Text = "Partida"
            importe.Text = "Importe"
            lstIntegracion.Columns.Add(partida)
            lstIntegracion.Columns.Add(importe)
            lstIntegracion.Items.Clear()
            Try
                If dtCupones.Rows.Count > 0 Then
                    For a As Integer = 0 To dtCupones.Rows.Count - 1
                        Dim drow As DataRow = dtCupones.Rows(a)
                        Select Case Trim(drow("idViaPago").ToString())
                            Case "W"
                                cupons = objutil.isDecimal(drow("importe").ToString())
                            Case "Y"
                                cupon2 = objutil.isDecimal(drow("importe").ToString())
                            Case "Z"
                                cupon5 = objutil.isDecimal(drow("importe").ToString())
                        End Select

                        LblCupons.Text = FormatCurrency(cupons, 2)
                        LblCupon2.Text = FormatCurrency(cupon2, 2)
                        LblCupon5.Text = FormatCurrency(cupon5, 2)

                    Next
                End If
            Catch ex As Exception
                MsgBox("No hay cupones Operados en HH")
            End Try
            

            For i As Integer = 0 To dtIntegracion.Rows.Count - 1
                Dim drow As DataRow = dtIntegracion.Rows(i)
                Dim lvi As New ListViewItem(drow("partida").ToString())
                lvi.SubItems.Add(FormatCurrency(objutil.isDecimal(drow("importe").ToString()), 2))
                Select Case Trim(drow("partida").ToString())
                    Case "(+) VENTAS"
                        Integracion.VENTA = objutil.isDecimal(drow("importe").ToString())
                        lstIntegracion.Items.Add(lvi)
                    Case "Contado"
                        lstIntegracion.Items.Add(lvi)
                    Case "Credito"
                        lstIntegracion.Items.Add(lvi)
                    Case "Envase"
                        lstIntegracion.Items.Add(lvi)
                    Case "DE LOS CUALES"
                        lstIntegracion.Items.Add(lvi)
                    Case "Liquido"
                        lstIntegracion.Items.Add(lvi)
                    Case "Envase"
                        lstIntegracion.Items.Add(lvi)
                    Case "(-) ENVASE RECIBIDO"
                        Integracion.ENVASE_RECIBIDO = objutil.isDecimal(drow("importe").ToString())
                        lstIntegracion.Items.Add(lvi)
                    Case "Envase recibido en ventas"
                        lstIntegracion.Items.Add(lvi)
                    Case "Envase recibido en cobros"
                        lstIntegracion.Items.Add(lvi)
                    Case "(+) CREDITOS CONCEDIDOS"
                        Integracion.CRED_CONCEDIDOS = objutil.isDecimal(drow("importe").ToString())
                        lstIntegracion.Items.Add(lvi)
                    Case "(-) DESCUENTOS APLICADOS"
                        Integracion.DESC_CONCEDIDOS = objutil.isDecimal(drow("importe").ToString())
                        lstIntegracion.Items.Add(lvi)
                    Case "(+) CREDITOS COBRADOS"
                        Integracion.CRED_COBRADOS = objutil.isDecimal(drow("importe").ToString())
                        lstIntegracion.Items.Add(lvi)
                        'Agregando vías de pago para cupones o promociones
                    Case "(-) OTRAS VIAS"
                        Integracion.OTRAS_VIAS = objutil.isDecimal(drow("importe").ToString())
                        lstIntegracion.Items.Add(lvi)
                        'Agregando el detalle de las otras vias de pago
                        Try
                            For ab As Integer = 0 To dtIntegracion.Rows.Count - 1
                                Dim drows As DataRow = dtIntegracion.Rows(ab)
                                Dim lviv As New ListViewItem(drows("partida").ToString())
                                lviv.SubItems.Add(FormatCurrency(objutil.isDecimal(drows("importe").ToString()), 2))
                                Select Case Trim(drows("idRubro").ToString())
                                    Case "X"
                                        lstIntegracion.Items.Add(lviv)
                                End Select
                            Next
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try
                    Case "DEVOLUCION PT"
                        Integracion.DEV_PRODUCTO = objutil.isDecimal(drow("importe").ToString())
                    Case "TOTAL_DEPOSITO"
                        totalDepositar = objutil.isDecimal(drow("importe").ToString()) + Integracion.OTRAS_VIAS
                End Select
            Next
            lblDeposito1.Text = FormatCurrency(totalDepositar, 2)
            lblDeposito2.Text = FormatCurrency(totalDepositar, 2)
            LblTotalVias.Text = FormatCurrency(Integracion.OTRAS_VIAS, 2)
            lbltotalOperaciones.Text = FormatCurrency(totalDepositar + (Integracion.OTRAS_VIAS * (-1)), 2)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

#Region " CONFIRMA LIQUIDACION "
    Private Sub txtClave_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtClave.KeyPress
        Try
            Dim usuario As New UsuarioCo
            Dim objUsuario As New UsuarioBl
            Dim oUtil As New UtilitarioBL
            Cursor.Current = Cursors.WaitCursor
            Select Case e.KeyChar()
                Case ChrW(13)
                    If (objRuta.validarDoctosFELenviado) Then
                        If objUsuario.login(txtClave.Text, "LIQUIDACION") Then
                            panLogin.Visible = False
                            If Not confirmarLiquidacion() Then
                                Cursor.Current = Cursors.Default
                            End If
                            Return
                            panResultLogin.Visible = False
                        Else
                            panResultLogin.Visible = True
                            txtClave.Focus()
                            lblError.Text = "La clave ingresada no es correcta"
                        End If
                        txtClave.Text = ""
                    Else
                        MessageBox.Show("HAY DOCUMENTOS PENDIENTES DE GENERAR FEL")
                    End If
                    
                Case ChrW(Keys.Escape)
                    txtClave.Text = ""
                    panResultLogin.Visible = False
                    panLogin.Visible = False
            End Select
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub cmdConfirma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConfirma.Click
        Dim objRuta As New RutaBL
        If (id_glo_fel = "X") Then
            If (objRuta.validarDoctosFELenviado) Then
                If cmdConfirma.Text = "Imprimir" Then
                    Dim RS As New printReporte
                    RS.finDia(lstMovimientos)
                    Return
                End If
            Else
                MessageBox.Show("DEBE GENERAR FEL PARA TODOS LOS DOCUMENTOS, REVISAR FACT. POR FALTANTES ")
            End If
        Else
            If cmdConfirma.Text = "Imprimir" Then
                Dim RS As New printReporte
                RS.finDia(lstMovimientos)
                Return
            End If
        End If

        panLogin.Visible = True
        InputPanel1.Enabled = True
        txtClave.Focus()
    End Sub
    Private Function confirmarLiquidacion() As Boolean
        Try

            cmdConfirma.Text = "Imprimir"
            deshabilitarControles(False)
            Integracion.COD_DIFERENCIA = 0
            Integracion.IMPORTE_DIFERENCIA = totalDiferencia
            Integracion.MONEDA = Trim(co_glo_moneda)
            Integracion.RUTA = id_glo_codRuta
            Integracion.TOTAL_LIQUIDAR = totalDepositar
            Integracion.DIF_CORTE = totalDiferencia
            Integracion.VALOR_BOLETA_DEPOSITO = depositoActual
            For i As Integer = 0 To lstDepositos.Items.Count() - 1
                Integracion.NO_BOLETA_DEPOSITO = Integracion.NO_BOLETA_DEPOSITO + ", " + lstDepositos.Items(i).SubItems(1).Text
            Next

            objRuta.liquidar(Integracion, lstDiferencias)
            MsgBox("Se ha confirmado la informacion para liquidar.")
            cmdConfirma.Text = "Imprimir"
            Dim RS As New printReporte
            RS.finDia(lstMovimientos)
            ruta.exportar = True
            objRuta.actualizar(ruta)
            Me.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
#End Region
#Region " BOLETAS DE DEPOSITO "
    Private Sub lstBancosOut_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lstBancosOut.KeyPress
        Select Case e.KeyChar()
            Case ChrW(13)
                txtDocumentoOut.Focus()
                txtDocumentoOut.SelectAll()
            Case ChrW(Keys.Escape)
                escapeEdicion()

        End Select
    End Sub
    Private Sub txtDocumentoOut_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDocumentoOut.KeyPress
        Select Case e.KeyChar()
            Case ChrW(13)
                txtValorOut.Focus()
                txtValorOut.SelectAll()
                txtValorOut.Text.Replace(ChrW(13), "")
            Case ChrW(Keys.Escape)
                escapeEdicion()
        End Select
    End Sub
    Private Sub txtValorOut_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtValorOut.KeyPress
        Select Case e.KeyChar()
            Case ChrW(13)

                If validarIngreso() Then
                    Dim dtDepositos As New DataTable
                    Dim objRuta As New RutaBL

                    If isForUpdate Then
                        objRuta.actualizarDeposito(idDeposito, lstBancosOut.SelectedValue, txtDocumentoOut.Text, txtValorOut.Text)
                        lstBancosOut.BackColor = Color.White
                        txtDocumentoOut.BackColor = Color.White
                        txtValorOut.BackColor = Color.White
                        lstDepositos.Enabled = True
                        lstBancosOut.SelectedIndex = 0
                        txtDocumentoOut.Text = ""
                        txtValorOut.Text = ""
                        isForUpdate = False
                        inicializarDepositos()
                    Else
                        objRuta.agregarDeposito(lstBancosOut.SelectedValue, txtDocumentoOut.Text, txtValorOut.Text)
                        lstBancosOut.SelectedIndex = 0
                        txtDocumentoOut.Text = ""
                        txtValorOut.Text = ""
                        dtDepositos = objRuta.obtenerDepositos()
                        inicializarDepositos()
                    End If
                End If
                lstBancosOut.Focus()
            Case ChrW(Keys.Escape)
                escapeEdicion()
        End Select
    End Sub
    Private Sub lstDepositos_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lstDepositos.KeyPress

        Select Case e.KeyChar()
            Case ChrW(68)
                eliminarDeposito()
            Case ChrW(100)
                eliminarDeposito()
            Case ChrW(Keys.Escape)
                escapeEdicion()
        End Select
    End Sub
    Private Sub eliminarDeposito()
        'Message box
        Dim msg As String
        Dim title As String
        Dim style As MsgBoxStyle
        Dim response As MsgBoxResult
        title = "Eliminar"
        msg = "Esta seguro de eliminar el deposito seleccionado?"
        style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.YesNo
        response = MsgBox(msg, style, title)
        If response = MsgBoxResult.Yes Then
            Dim objRuta As New RutaBL
            objRuta.eliminarDeposito(idDeposito)
            inicializarDepositos()
            actualizarLabelsFinancieros()
            escapeEdicion()
            MsgBox("Deposito eliminado.")
        Else
            Return
        End If
    End Sub
    Private Sub lstDepositos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstDepositos.SelectedIndexChanged
        Try
            If Me.lstDepositos.SelectedIndices.Count <= 0 Then
                Return
            End If
            Dim itemSelected = Me.lstDepositos.SelectedIndices(0)
            isForUpdate = True
            lstBancosOut.SelectedValue = Convert.ToInt32(dtDepositos.Rows(itemSelected).Item("DATAVALUE"))
            txtDocumentoOut.Text = dtDepositos.Rows(itemSelected).Item("documento")
            txtValorOut.Text = dtDepositos.Rows(itemSelected).Item("importe")
            idDeposito = dtDepositos.Rows(itemSelected).Item("id_deposito")
            lstBancosOut.BackColor = xoWarning
            txtDocumentoOut.BackColor = xoWarning
            txtValorOut.BackColor = xoWarning
            lstBancosOut.Focus()
        Catch ex As Exception

        End Try
    End Sub
    Private Function validarIngreso() As Boolean
        If lstBancosOut.SelectedIndex = 0 Then
            MsgBox("Debe seleccionar un banco.")
            Return False
        End If
        If txtDocumentoOut.Text = "" Then
            MsgBox("Debe escribir un numero de documento valido.")
            Return False
        End If
        If objutil.isDecimal(txtValorOut.Text) <= 0 Then
            MsgBox("El valor a depositar no es valido")
            Return False
        End If
        Return True
    End Function
#End Region
#Region " Diferencia en liquidacion "

    Private Sub lstMotivoDiferencia_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lstMotivoDiferencia.KeyPress
        Select Case e.KeyChar()
            Case ChrW(13)
                txtValorDiferencia.Focus()
                txtValorDiferencia.SelectAll()
            Case ChrW(Keys.Escape)
                escapeEdicion()
        End Select
    End Sub

    Private Sub txtValorDiferencia_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtValorDiferencia.KeyPress

        '--- Agregar el valor y motivo al grid
        Dim lvi As New ListViewItem(lstMotivoDiferencia.Text)
        Select Case e.KeyChar()
            Case ChrW(13)
                Dim tipoMotivo As String
                tipoMotivo = dtMotivosDiferencia.Rows(lstMotivoDiferencia.SelectedIndex).Item("TABLA").ToString()
                If tipoMotivo = "FALTANTES" Then
                    txtValorDiferencia.Text = objutil.isDecimal(txtValorDiferencia.Text) * -1
                End If
                lvi.SubItems.Add(FormatCurrency(txtValorDiferencia.Text, 2))
                lvi.SubItems.Add(lstMotivoDiferencia.SelectedValue.ToString)
                lvi.SubItems.Add(dtMotivosDiferencia.Rows(lstMotivoDiferencia.SelectedIndex).Item("TABLA").ToString())
                lstDiferencias.Items.Add(lvi)
                calcularDiferenciaAcumulada(objutil.isDecimal(txtValorDiferencia.Text), tipoMotivo)
                txtValorDiferencia.Text = ""
        End Select
    End Sub

    Private Sub lstdiferencias_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lstDiferencias.KeyPress
        Select Case e.KeyChar()
            Case ChrW(68)
                eliminarDiferencia()
            Case ChrW(100)
                eliminarDiferencia()
            Case ChrW(Keys.Escape)
                escapeEdicion()
        End Select
    End Sub

    Private Function eliminarDiferencia() As Boolean
        Try
            Dim tipoMotivo As String
            Dim valor As Decimal
            Dim itemSelected = Me.lstDiferencias.SelectedIndices(0)
            Dim oUtil As New UtilitarioBL
            tipoMotivo = lstDiferencias.Items.Item(itemSelected).SubItems(3).Text
            valor = oUtil.isDecimal(FormatNumber(lstDiferencias.Items.Item(itemSelected).SubItems(1).Text))
            lstDiferencias.Items.RemoveAt(itemSelected)
            calcularDiferenciaAcumulada(valor * -1, tipoMotivo)
        Catch ex As Exception
            '--- No retornar
        End Try
    End Function

    Private Sub calcularDiferenciaAcumulada(ByVal valorDiferencia As Decimal, ByVal tipoMotivo As String)
        totalDiferenciaDeclarada = totalDiferenciaDeclarada + valorDiferencia
        actualizarLabelsFinancieros()
    End Sub

#End Region
#Region " Utilitarios - Painters - Focus - exit app "
    Private Sub panResultLogin_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panResultLogin.Paint
        Dim oUtil As New UtilitarioBL
        oUtil.paintPannel(e, panResultLogin)
    End Sub
    Private Sub setAlternatingRowColor(ByVal lst As ListView)
        For Each item As ListViewItem In lst.Items
            If ((item.Index Mod 2) = 0) Then
                item.BackColor = Color.AliceBlue
                lst.Items(item.Index).BackColor = Color.AliceBlue
            Else
                lst.Items(item.Index).BackColor = Color.LightYellow
            End If
        Next
    End Sub
    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint
        objutil.paintPannel(e, Panel1)
    End Sub
    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint
        objutil.paintPannel(e, Panel2)
    End Sub
    Private Sub Panel4_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel4.Paint
        objutil.paintPannel(e, Panel4)
    End Sub
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 2 Then
            lstBancosOut.Focus()
        End If
        If TabControl1.SelectedIndex = 3 Then
            lstMotivoDiferencia.Focus()
        End If
    End Sub
    Private Sub lSoft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lSoft.Click
        Me.Close()
    End Sub
#End Region

    Private Sub lstMotivoDiferencia_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstMotivoDiferencia.SelectedValueChanged
        txtValorDiferencia.Focus()
        txtValorDiferencia.SelectAll()
    End Sub

    Private Sub txtDocumentoOut_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDocumentoOut.GotFocus
        txtDocumentoOut.SelectAll()
    End Sub

    Private Sub txtDocumentoOut_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDocumentoOut.LostFocus
        If txtDocumentoOut.Text = "" Then
            txtDocumentoOut.Text = "No.Documento"
        End If
    End Sub

    Private Sub cmdCerrarLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmdCerrarLogin_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCerrarLogin.Click
        panLogin.Visible = False
        InputPanel1.Enabled = False
    End Sub

    Private Sub lstBancosOut_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstBancosOut.SelectedIndexChanged
        txtDocumentoOut.Focus()
    End Sub

    Private Sub panLogin_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles panLogin.GotFocus

    End Sub
End Class

