Imports System.Data
Imports DataGridCustomColumns
Imports Proyecto_xoMobile_Packs

Public Class frmPago
    '--- Message box
    Dim msg As String
    Dim title As String
    Dim style As MsgBoxStyle
    Dim response As MsgBoxResult
    Dim objUtil As New UtilitarioDT

    'Variables
    'Public isContado As Boolean = False
    Dim flag As Integer
    Dim importeDestoOriginal As String = 0, importeDestoPPOriginal As String = 0


    'Manejo de eventos de los controles personalizados
    Private WithEvents lstBancos As ComboBox
    Private WithEvents txtValor As TextBox
    Private WithEvents txtDocumento As TextBox
    Private WithEvents txtDummy As TextBox

    'Objetos de Comunicacion entre capas
    Public comDocumentoPago As New documentoCO
    Public v_com_documento As New documentoCO
    Public documento As New documentoCO
    Public comNotaCreditoEnv As New documentoCO

    Public v_com_cliente As New ClienteCO

    'Objetos de la capa de negocio
    Dim objClienteBL As New ClienteBL
    Dim objUtilBL As New UtilitarioBL
    Dim objDocumentoBL As New DocumentoBL

    '--- Fuentes de datos para el Grid de vias de pago
    Private dgDataSources As New DataSet
    Public rlayer As New rLayerHandler
    'Public comPaso As Integer
    Public comCorriendo As Boolean


    Private Sub frmPago_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dtViasPago As New DataTable
        Dim importeDestoOriginal As String = 0
        Dim importeDestoPPOriginal As String = 0
        importeDestoOriginal = v_com_documento.importeDesto
        importeDestoPPOriginal = v_com_documento.importeDestoPP

        '--- Traer una instancia del cliente para actualizar cualquier dato en memoria
        v_com_cliente = objClienteBL.getDetalleDelCliente(v_com_cliente.codigo) ' (55003789) '(55003964)
        txtDescuento.Text = 0
        txtDescuento.Tag = 0
        '--- Popular el formulario segun el metodo de beneficios
        With objUtilBL
            lblSerie.Text = v_com_documento.serie
            lblNumero.Text = v_com_documento.numero
            lblNegocio.Text = v_com_cliente.negocio
            txtTotal.Text = FormatCurrency(v_com_documento.importe, 2)
            txtTotal.Tag = v_com_documento.importe
            txtEnvase.Text = FormatCurrency(v_com_documento.importeDestoEnv, 2)
            txtEnvase.Tag = v_com_documento.importeDestoEnv
            If v_com_documento.isContado Then
                txtDescuento.Tag = Math.Abs(objUtilBL.isDecimal(v_com_documento.importeDesto))
                txtDescuento.Text = FormatCurrency(txtDescuento.Tag, 2)
            Else
                txtDescuento.Tag = 0
                txtDescuento.Text = 0
            End If
            lblTotalPagar.Tag = .isDecimal(txtTotal.Tag) - .isDecimal(txtDescuento.Tag) - .isDecimal(txtEnvase.Tag)
            lblTotalPagar.Text = FormatCurrency(lblTotalPagar.Tag, 2)
            txtSaldo.Text = FormatCurrency(lblTotalPagar.Text, 2)
            txtSaldo.Tag = lblTotalPagar.Tag
        End With
        'MsgBox(v_com_documento.isContado)
        '--- Agregar las fuentes de pago
        dtViasPago = objClienteBL.getViasPago(v_com_documento.isContado, v_com_documento.soloEfectivo)
        dgDataSources.Tables.Add(dtViasPago)
        dgDataSources.Tables.Add(objUtilBL.getBancos)

        '--- Establecer estilo del grid y Mostrar las vias de pago
        SetupTableStyles()
        Cursor.Current = Cursors.Default

        '--- Ocultar el boton atras
        If v_com_documento.ttipo <> "ODV" Then
        End If


        '--- Detectar si es pago 0 
        If efectuarPagoCero() Then
            txtSaldo.ForeColor = Color.Red
        End If
    End Sub

    Private Sub panCobros_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panCobros.Paint
        objUtilBL.paintPannel(e, panCobros)
    End Sub

#Region " Establecer estilo del grid y Mostrar las vias de pago.   "
    Private Sub SetupTableStyles()
        Dim alternatingColor As Color = Color.LightYellow
        Dim dtViaPago As DataTable = Me.dgDataSources.Tables(0)


        '----------------------------------------------------------------------
        '------------------------- Crear columnas -----------------------------
        '----------------------------------------------------------------------

        '--- [1] Vias de Pago
        Dim dataGridCustomColumn2 As New DataGridCustomNumericTextBoxColumn
        With dataGridCustomColumn2
            .Owner = Me.dgFormasPago
            .HeaderText = "F. Pago"
            .MappingName = dtViaPago.Columns(1).ColumnName
            .Width = dgFormasPago.Width * 25 / 100         ' 30% of the grid size
            .AlternatingBackColor = alternatingColor
            .ReadOnly = True
        End With
        dgFormasPagoStyle.GridColumnStyles.Add(dataGridCustomColumn2)

        '--- [2] Valor a pagar
        Dim dataGridCustomColumn3 As New DataGridCustomNumericTextBoxColumn
        With dataGridCustomColumn3
            .Owner = Me.dgFormasPago
            .HeaderText = "Valor"
            .MappingName = dtViaPago.Columns(2).ColumnName
            .Width = dgFormasPago.Width * 25 / 100         ' 15% of the grid size
            .AlternatingBackColor = alternatingColor
            .ReadOnly = False
        End With
        dgFormasPagoStyle.GridColumnStyles.Add(dataGridCustomColumn3)

        '--- [3] Numero de documento
        Dim dataGridCustomColumn4 As New DataGridCustomTextBoxColumn
        With dataGridCustomColumn4
            .Owner = Me.dgFormasPago
            .HeaderText = "Documento"
            .MappingName = dtViaPago.Columns(3).ColumnName
            .Width = dgFormasPago.Width * 20 / 100         ' 15% of the grid size
            .AlternatingBackColor = alternatingColor
            .ReadOnly = False
        End With
        dgFormasPagoStyle.GridColumnStyles.Add(dataGridCustomColumn4)

        '--- [4] institucion al que pertenece el documento
        Dim dataGridCustomColumn5 As New DataGridCustomComboBoxColumn()
        With dataGridCustomColumn5
            .Owner = Me.dgFormasPago
            .HeaderText = "Institucion"
            .MappingName = dtViaPago.Columns(4).ColumnName
            .Width = dgFormasPago.Width * 50 / 100         ' 37% of the grid size
            .AlternatingBackColor = alternatingColor
            .ReadOnly = False
        End With
        dgFormasPagoStyle.GridColumnStyles.Add(dataGridCustomColumn5)


        Me.dgFormasPagoStyle.MappingName = dtViaPago.TableName                ' Setup table mapping name
        Me.dgFormasPago.DataSource = dtViaPago                                ' Setup grid's data source        
        Me.dgFormasPago.Focus()
        Me.dgFormasPago.CurrentRowIndex = 0

        'Capturar los controles en variables manejadoras
        lstBancos = CType(dataGridCustomColumn5.HostedControl, ComboBox)
        txtValor = CType(dataGridCustomColumn3.HostedControl, TextBox)
        txtDocumento = CType(dataGridCustomColumn4.HostedControl, TextBox)

        Dim dtBancos As DataTable = Me.dgDataSources.Tables(1)
        lstBancos.DataSource = dtBancos
        lstBancos.DisplayMember = dtBancos.Columns(0).ColumnName
        lstBancos.ValueMember = dtBancos.Columns(1).ColumnName
        dgFormasPago.CurrentCell = New DataGridCell(0, 1)
        lstBancos.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub
#End Region

#Region " Navegar en el Grid de Vias de pago.  "

    Private Sub txtValor_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtValor.KeyPress
        Select Case e.KeyChar()
            Case ChrW(13)
                Dim fila As Integer
                Dim columna As Integer
                fila = dgFormasPago.CurrentCell.RowNumber
                columna = dgFormasPago.CurrentCell.ColumnNumber
                txtValor.Text = objUtilBL.isDecimal(txtValor.Text)
                dgDataSources.Tables(0).Rows(fila).Item("valor") = txtValor.Text
                If dgDataSources.Tables(0).Rows(fila).Item(0) = "E" Or dgDataSources.Tables(0).Rows(fila).Item(0) = "CR" Then
                    txtDocumento.Text = "N/A"
                    lstBancos.Text = "N/A"
                    fila = fila + 1
                    txtValor.SelectAll()
                Else
                    columna = columna + 1
                End If
                reducirSaldoPendiente()
                If fila = dgDataSources.Tables(0).Rows.Count() Then
                    dgFormasPago.CurrentCell = New DataGridCell(0, 1)
                    txtValor.SelectAll()
                Else
                    dgFormasPago.CurrentCell = New DataGridCell(fila, columna)
                    txtDocumento.SelectAll()
                End If
        End Select
    End Sub
    Private Sub txtValor_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtValor.LostFocus
        Dim fila As Integer
        fila = dgFormasPago.CurrentCell.RowNumber
        txtValor.Text = objUtilBL.isDecimal(txtValor.Text)
        dgDataSources.Tables(0).Rows(fila).Item("valor") = txtValor.Text
        If dgDataSources.Tables(0).Rows(fila).Item(0) = "E" Or dgDataSources.Tables(0).Rows(fila).Item(0) = "CR" Then
            txtDocumento.Text = "N/A"
            lstBancos.Text = "N/A"
            txtValor.SelectAll()
        End If
        reducirSaldoPendiente()
    End Sub
    Private Sub txtDocumento_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDocumento.GotFocus
        txtDocumento.ReadOnly = False
    End Sub

    Private Sub txtDocumento_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDocumento.KeyPress
        Select Case e.KeyChar()
            Case ChrW(13)
                flag = 1
                Dim fila As Integer
                Dim columna As Integer
                txtDocumento.Text = txtDocumento.Text.ToUpper
                dgDataSources.Tables(0).Rows(dgFormasPago.CurrentCell.RowNumber).Item("documento") = txtDocumento.Text
                fila = dgFormasPago.CurrentCell.RowNumber
                columna = dgFormasPago.CurrentCell.ColumnNumber + 1
                dgFormasPago.CurrentCell = New DataGridCell(fila, columna)
                lstBancos.Focus()
                txtDocumento.ReadOnly = True
                reducirSaldoPendiente()
        End Select
    End Sub
    Private Sub lstBancos_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstBancos.LostFocus
        If Not flag = 1 Then
            Dim fila As Integer
            Dim columna As Integer
            fila = dgFormasPago.CurrentCell.RowNumber
            columna = dgFormasPago.CurrentCell.ColumnNumber
            fila = fila + 1
            columna = 1
            If fila = dgDataSources.Tables(0).Rows.Count() Then
                dgFormasPago.CurrentCell = New DataGridCell(0, 1)
                txtValor.SelectAll()
            Else
                dgFormasPago.CurrentCell = New DataGridCell(fila, columna)
                txtValor.SelectAll()
            End If
            flag = 0
            reducirSaldoPendiente()
        End If
    End Sub
    Private Sub lstBancos_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lstBancos.KeyPress
        Select Case e.KeyChar()
            Case ChrW(13)
                Dim fila As Integer
                Dim columna As Integer
                fila = dgFormasPago.CurrentCell.RowNumber
                columna = dgFormasPago.CurrentCell.ColumnNumber
                fila = fila + 1
                columna = 1
                If fila = dgDataSources.Tables(0).Rows.Count() Then
                    dgFormasPago.CurrentCell = New DataGridCell(0, 1)
                    txtValor.SelectAll()
                Else
                    dgFormasPago.CurrentCell = New DataGridCell(fila, columna)
                    txtValor.SelectAll()
                End If
                reducirSaldoPendiente()
        End Select

    End Sub

    Public Sub reducirSaldoPendiente()
        Dim valor As Decimal
        For i As Integer = 0 To dgDataSources.Tables(0).Rows.Count() - 1
            valor = valor + objUtilBL.isDecimal(dgDataSources.Tables(0).Rows(i).Item("valor"))
        Next
        txtSaldo.Text = FormatCurrency(lblTotalPagar.Tag - valor, 2)
        txtSaldo.Tag = Decimal.Round(txtSaldo.Text, 2)
    End Sub
    Private Sub panSaldo_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panSaldo.Paint
        objUtilBL.paintPannel(e, panSaldo)
    End Sub
#End Region

    Public Function operacion() As Boolean

    End Function

    Private Sub rSoft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rSoft.Click
        Dim PorcentajeNC As Integer
        Dim totalNC As String
        Dim testString As String
        Dim ejecutar As Boolean = False



        

        Try
            Dim texto As String = ""
            texto = Replace(v_com_cliente.nit, "-", "")
            v_com_cliente.nit = Replace(texto, "/", "")
        Catch ex As Exception
            v_com_cliente.nit = "CF"
        End Try

        If v_com_cliente.nit <> "CF" Then
            ejecutar = True

        Else
            'el cliente tiene CF en su dato maestro
            If txtTotal.Text <= id_glo_total_cf Then
                ejecutar = True
            Else
                If Len(v_com_cliente.numeroDi) > 0 Then
                    If v_com_cliente.numeroDi.Substring(0, 1) <> "P" Then
                        ejecutar = True
                    Else
                        Try
                            Dim quitarP As String = ""
                            quitarP = Replace(v_com_cliente.numeroDi, "P", "")
                            ejecutar = True
                        Catch ex As Exception
                            ejecutar = False
                        End Try
                    End If
                End If
            End If
        End If

        If v_com_documento.ttipo = "CXC" Then
            ejecutar = True
        End If

        If ejecutar = True Then
            Cursor.Current = Cursors.WaitCursor
            reducirSaldoPendiente()
            flag = 0
            lstBancos_LostFocus(sender, e)
            txtValor_LostFocus(sender, e)
            PorcentajeNC = objUtil.getPorcentaje()
            If txtSaldo.ForeColor = Color.Red Then
                aceptarPago()
            Else
                If objDocumentoBL.validarFormasPago(dgDataSources.Tables(0), dgDataSources.Tables(1), v_com_cliente, txtEnvase.Tag, v_com_documento.isContado) Then
                    totalNC = (txtTotal.Text - txtDescuento.Text) * (PorcentajeNC / 100)
                    If (((txtEnvase.Text * 100) / (txtTotal.Text - txtDescuento.Text)) > PorcentajeNC) Then
                        testString = FormatCurrency(totalNC, , , TriState.True, TriState.True)
                        MsgBox("La nota de crédito excede el " & PorcentajeNC & "% al total del documento, debe tomar el descuento que aplica limite a recibir " & FormatCurrency(totalNC, 2))
                        Cursor.Current = Cursors.Default
                        Return
                    End If
                    If v_com_documento.aceptaAbono And v_com_documento.aceptaDescuento Then
                        If txtSaldo.Tag = 0 Then
                            MsgBox("No se permite el pago completo, solo abono.")
                            Cursor.Current = Cursors.Default
                            Return
                        End If
                        If txtSaldo.Tag < 0 Then
                            MsgBox("Los montos ingresados exeden el total a pagar.")
                        Else
                            aceptarPago()
                            Cursor.Current = Cursors.Default
                            Return
                        End If
                    End If
                    If v_com_documento.soloAbono Then
                        If txtSaldo.Tag = 0 Then
                            MsgBox("No se permite el pago completo, solo abono.")
                            Cursor.Current = Cursors.Default
                            Return
                        End If
                    End If
                    If v_com_documento.aceptaAbono Then
                        If txtSaldo.Tag < 0 Then
                            MsgBox("Los montos ingresados exeden el total a pagar.")
                        Else
                            aceptarPago()
                            Cursor.Current = Cursors.Default
                            Return
                        End If
                    Else
                        If txtSaldo.Text < 0 Then
                            MsgBox("Los montos ingresados exeden el total a pagar.")
                            Cursor.Current = Cursors.Default
                            Return
                        ElseIf txtSaldo.Tag > 0 Then
                            MsgBox("Aun queda saldo pendiente de cancelar.")

                            Cursor.Current = Cursors.Default
                            Return
                        Else
                            aceptarPago()
                            Cursor.Current = Cursors.Default
                            Return
                        End If
                    End If
                Else
                    Cursor.Current = Cursors.Default
                End If
            End If
        Else
            MessageBox.Show("DEBE ACTUALIZAR LOS DATOS CON EL NIT / DPI O PASAPORTE")
        End If



    End Sub


    Private Function efectuarPagoCero() As Boolean
        If txtSaldo.Text = 0 And (txtDescuento.Tag - txtTotal.Tag) = 0 Then
            MsgBox("Se ha detectado un pago cero porque el Descuento es igual al saldo total.")
            dgFormasPago.Enabled = False
            Return True
        Else
            dgFormasPago.Enabled = True
            Return False
        End If
    End Function

    Private Sub aceptarPago()
        'Obtener informacion del recibo
        comDocumentoPago.idEncCxcRelacionada = v_com_documento.idEncabezado
        comDocumentoPago.doTipo = v_com_documento.ttipo
        comDocumentoPago.estado = 0
        comDocumentoPago.importe = txtTotal.Tag
        comDocumentoPago.importePago = lblTotalPagar.Tag
        comDocumentoPago.importeDestoEnv = txtEnvase.Tag
        comDocumentoPago.importeDesto = txtDescuento.Tag
        comDocumentoPago.porcentajeDesto = v_com_documento.porcentajeDesto
        comDocumentoPago.numero = lblNumero.Text
        comDocumentoPago.serie = lblSerie.Text
        comDocumentoPago.dgPagos = dgDataSources.Tables(0)
        comDocumentoPago.moneda = co_glo_moneda
        comDocumentoPago.saldo = txtSaldo.Tag
        objDocumentoBL.crearRecibo(comDocumentoPago, v_com_documento, v_com_cliente)
        'objDocumentoBL.validaRecibo(comDocumentoPago, v_com_documento, v_com_cliente)

        Me.Close()
    End Sub

    Private Sub validarRecibo()

    End Sub

#Region " Cancelar la operacion "

    Private Sub lsoftCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lsoftCancelar.Click
        '--- Cancelar Operacion
        'Dim wf As New workFlowVenta
        'comPaso = 0
        'wf.cancelarOperacion(objDocumentoBL, Me, rlayer, comPaso, comCorriendo)
        rlayer.codigo = 3
        Me.Close()
    End Sub

#End Region

#Region " Regresar una pantalla "
    Private Sub lsoftAtras_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lsoftAtras.Click
        rlayer.codigo = 2
        Me.Close()
    End Sub
#End Region


    
  
End Class