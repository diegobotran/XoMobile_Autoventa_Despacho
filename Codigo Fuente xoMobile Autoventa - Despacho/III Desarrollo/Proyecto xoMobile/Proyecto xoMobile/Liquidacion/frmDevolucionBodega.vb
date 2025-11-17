Imports System.Data.SqlServerCe.SqlCeTransaction
Imports System
Imports System.Data
Imports DataGridCustomColumns
Imports System.Reflection
Imports Proyecto_xoMobile_Packs

Public Class frmDevolucionBodega

#Region " VARIABLES "

    Dim devolucion As New inventarioCO

    '--- Manejo de eventos de los controles personalizados
    Private WithEvents txtReal As NumericUpDown
    Private WithEvents txtRotura As NumericUpDown
    Dim fi As Reflection.FieldInfo
    Private objUtil As New UtilitarioBL
    Dim objInventario As New InventarioBL

    '--- Variables globales
    Dim unidadesCaja As Integer
    Dim gloFila, gloColumna As Integer

    '--- Indica se es necesario mostrar el reporte de liquidacion
    Public tipoLiquidacion As Integer
    Private dataSource As DataSet

    '--- Indica si la columna fisico es readonly
    Dim makeReadOnly As Boolean = False

#End Region

#Region " INICIALIZAR FORMULARIO "

    Private Sub frmDevolucionBodega_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim bodega As New BodegaBL
        Dim dtProductoDevuelto As DataTable = Nothing
        Try
            '--- Inicializar el formulario
            inicializar()
            bodega.getArticuloDevuelto(dtProductoDevuelto, devolucion)
            id_glo_aplicacion = Nothing

            '--- Verifica si la devolucion de producto ya fue realizada anteriormente
            If dtProductoDevuelto.Rows.Count > 0 Then
                'Si ya fue realizado llenar el grid con esa informacion
                cargarDevolucion(dtProductoDevuelto)
                makeReadOnly = True
                'menuDevolucionBodega.MenuItems(1).Enabled = False
                rSoft.Text = "Imprimir"
            Else
                'Si no, obtener los articulos teoricos del camion
                getTeoricoCamion()
            End If
            SetupTableStyles()
            Cursor.Current = Cursors.Default

        Catch ex As Exception
            MsgBox(ex.Message)
            Me.Close()
        End Try
        'desplegarReporte()
    End Sub

    Public Sub inicializar()

        '--- Establecer el tipo de inventario
        devolucion.tTipo = tipoLiquidacion
        lblFecha.Text = "Hoy es: " & FormatDateTime(Date.Today, DateFormat.ShortDate)
        '--- Titulo del formulario
        If tipoLiquidacion = 0 Then
            Me.Text = "Envase"
            devolucion.idBodega = id_glo_BodegaEnvase
        Else
            Me.Text = "Producto"
            devolucion.idBodega = id_glo_BodegaProducto
        End If
    End Sub


#End Region

#Region " LLENAR GRID CON TEORICO O REAL "
    Private Sub getTeoricoCamion()
        Dim dtTeorico As New DataTable
        Dim bodega As New BodegaDT
        Dim objBodega As New BodegaBL
        Me.dataSource = New DataSet
        If tipoLiquidacion = 0 Then
            'Obtener teorico de envases
            dtTeorico = bodega.getTeoricoEnvase()
        Else
            'Obtener teorico de producto        
            dtTeorico = bodega.getTeoricoProducto()
        End If
        If dtTeorico.Rows.Count <= 0 Then
            Dim msg = "No existen articulos para devolver, desea confirmar la devolucion?"
            Dim style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.YesNo
            Dim response = MsgBox(msg, style, "PRE-LIQUIDACION")
            If response = MsgBoxResult.Yes Then
                objBodega.confirmaDevolucion(tipoLiquidacion)
                Throw New Exception("El conteo en bodega se ha realizado con exito.")
            Else
                Throw New Exception("ALERTA: Aunque no existan articulos para devolver, debe confirmar la devolucion.")
            End If
        End If
        Me.dataSource.Tables.Add(dtTeorico)
    End Sub

    Private Sub cargarDevolucion(ByVal dtProductoDevuelto As DataTable)
        Me.dataSource = New DataSet
        Me.dataSource.Tables.Add(dtProductoDevuelto)
    End Sub
#End Region

#Region " NAVEGACION CELDAS GRID "

    Private Sub txtReal_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtReal.LostFocus

        Dim diferencia As Integer


        'Calcular la diferencia  
        If objUtil.isDecimal(txtReal.Text) > objUtil.isDecimal(dgDevolucion.Item(gloFila, 2).ToString) Then
            txtReal.Text = dgDevolucion.Item(gloFila, 2).ToString
            txtReal.Focus()
            Return
        End If
        diferencia = objUtil.isDecimal(txtReal.Text) - objUtil.isDecimal(dgDevolucion.Item(gloFila, 2).ToString)
        dgDevolucion.Item(gloFila, 5) = diferencia
    End Sub

    Private Sub dgDevolucion_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgDevolucion.CurrentCellChanged
        gloFila = dgDevolucion.CurrentCell.RowNumber
        gloColumna = dgDevolucion.CurrentCell.ColumnNumber
    End Sub

    Private Sub txtReal_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReal.KeyPress
        Dim producto As New ProductoCO
        Dim objProducto As New ProductoBL

        Select Case e.KeyChar()
            Case "*"
                '--- Entrar en modo cajas ---'
                panIngreso.Visible = True
                dgDevolucion.Enabled = False
                panIngreso.BackColor = Color.FromArgb(12, 78, 127)
                txtCaja.Text = "-"
                txtUnidades.Text = "-"
                txtCaja.SelectionStart = 1
                gloFila = dgDevolucion.CurrentCell.RowNumber
                producto = objProducto.getDetalleDelProducto(dgDevolucion.Item(gloFila, 0).ToString)
                lblUnidadesCaja.Text = producto.unidadesCaja.ToString() + ". UN/CJ"
                unidadesCaja = producto.unidadesCaja
                txtCaja.Focus()

            Case "r"
                If tipoLiquidacion = 1 Then
                    '--- Entrar en modo cajas ---'
                    panIngreso.Visible = True
                    txtCaja.Text = ""
                    txtUnidades.Text = ""
                    panIngreso.BackColor = Color.Red
                    dgDevolucion.Enabled = False
                    gloFila = dgDevolucion.CurrentCell.RowNumber
                    producto = objProducto.getDetalleDelProducto(dgDevolucion.Item(gloFila, 0).ToString)
                    lblUnidadesCaja.Text = producto.unidadesCaja.ToString() + ". UN/CJ"
                    unidadesCaja = producto.unidadesCaja
                    txtCaja.Focus()
                End If
            Case "R"
                If tipoLiquidacion = 1 Then
                    '--- Entrar en modo cajas ---'
                    panIngreso.Visible = True
                    txtCaja.Text = ""
                    txtUnidades.Text = ""
                    panIngreso.BackColor = Color.Red
                    dgDevolucion.Enabled = False
                    gloFila = dgDevolucion.CurrentCell.RowNumber
                    producto = objProducto.getDetalleDelProducto(dgDevolucion.Item(gloFila, 0).ToString)
                    lblUnidadesCaja.Text = producto.unidadesCaja.ToString() + ". UN/CJ"
                    unidadesCaja = producto.unidadesCaja
                    txtCaja.Focus()
                End If

            Case ChrW(13)


                Dim Fila As Integer
                Dim diferencia As Integer


                ''---Calcular la diferencia 
                If objUtil.isDecimal(txtReal.Text) > objUtil.isDecimal(dgDevolucion.Item(gloFila, 2).ToString) Then
                    txtReal.Text = dgDevolucion.Item(gloFila, 2).ToString
                    txtReal.Focus()
                    Return
                End If

                diferencia = objUtil.isDecimal(txtReal.Text) - objUtil.isDecimal(dgDevolucion.Item(gloFila, 2).ToString)
                dgDevolucion.Item(gloFila, 5) = diferencia
                Fila = gloFila
                Fila = Fila + 1

                '--- Cambio de celda

                If Fila = Me.dataSource.Tables(0).Rows.Count() Then
                    dgDevolucion.CurrentCell = New DataGridCell(0, gloColumna)
                    scrollto(0)
                Else
                    dgDevolucion.CurrentCell = New DataGridCell(Fila, gloColumna)
                    scrollto(Fila)
                End If



        End Select
    End Sub

    Public Function scrollto(ByVal ivalue As Integer) As Boolean
        fi = dgDevolucion.GetType().GetField("m_sbVert", BindingFlags.NonPublic Or BindingFlags.GetField Or BindingFlags.Instance)
        CType(fi.GetValue(dgDevolucion), VScrollBar).Value = ivalue
        dgDevolucion.CurrentRowIndex = ivalue
    End Function

    Private Sub txtCaja_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCaja.KeyPress
        Select Case e.KeyChar()
            Case ChrW(13)
                Dim conversion As Integer
                If panIngreso.BackColor = Color.Red Then
                    Dim columna, fila As Integer
                    dgDevolucion.Enabled = False
                    fila = dgDevolucion.CurrentCell.RowNumber
                    columna = dgDevolucion.CurrentCell.ColumnNumber
                    txtReal.Text = txtReal.Text.Replace("R", "")
                    txtReal.Text = txtReal.Text.Replace("r", "")
                    dgDevolucion.Item(fila, 6) = (objUtil.isDecimal(unidadesCaja) * objUtil.isDecimal(txtCaja.Text))
                    txtUnidades.Focus()
                Else

                    txtReal.Text = txtReal.Text.Replace("*", "")
                    txtUnidades.SelectionStart = 1
                    conversion = txtReal.Value + (objUtil.isDecimal(unidadesCaja) * objUtil.isDecimal(txtCaja.Text))
                    txtReal.Text = conversion
                    dgDevolucion.Enabled = True
                    txtUnidades.Focus()
                End If

        End Select
    End Sub

    Private Sub txtUnidades_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUnidades.KeyPress
        Select Case e.KeyChar()
            Case ChrW(13)
                Dim conversion As Integer
                If panIngreso.BackColor = Color.Red Then
                    Dim columna, fila As Integer
                    panIngreso.Visible = False
                    fila = dgDevolucion.CurrentCell.RowNumber
                    columna = dgDevolucion.CurrentCell.ColumnNumber
                    txtReal.Text = txtReal.Text.Replace("R", "")
                    txtReal.Text = txtReal.Text.Replace("r", "")
                    dgDevolucion.Item(fila, 6) = objUtil.isInteger(txtUnidades.Text) + dgDevolucion.Item(fila, 6)
                    If dgDevolucion.Item(fila, 6) > dgDevolucion.Item(fila, 4) Then
                        MsgBox("El numero de roturas a declarar es mayor al inventario real.")
                        dgDevolucion.Item(fila, 6) = 0
                    End If
                    dgDevolucion.Enabled = True
                    txtReal.Focus()
                Else
                    panIngreso.Visible = False
                    txtReal.Text = txtReal.Text.Replace("*", "")
                    conversion = txtReal.Value + objUtil.isInteger(txtUnidades.Text)
                    txtReal.Text = conversion
                    dgDevolucion.Enabled = True
                    txtReal.Focus()
                End If
        End Select

    End Sub
#End Region

#Region " COMANDOS "
    Private Sub rSoft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rSoft.Click
        Dim bodega As New BodegaBL

        Cursor.Current = Cursors.WaitCursor

        '--- Si ya se grabo la devolucion entonces solo se imprime el ticket
        If rSoft.Text = "Imprimir" Then
            bodega.desplegarReporte(tipoLiquidacion)
            Return
        End If


        '--- Grabar devolucion en bodega
        Try
            panIngreso.Focus()
            bodega.devolverProducto(devolucion, dataSource.Tables(0), dgDevolucion, tipoLiquidacion)
        Catch ex As Exception
            MsgBox("Pre liquidacion: " & ex.Message)
            Me.Close()
        End Try
        Me.Close()
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub lSoft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lSoft.Click
        Me.Close()
    End Sub
#End Region

#Region " GRID DE PRODUCTOS "
    Private Sub SetupTableStyles()

        'Dim alternatingColor As Color = Color.LightSteelBlue
        Dim alternatingColor As Color = Color.LightYellow
        Dim vehicle As DataTable = Me.dataSource.Tables(0)
        Dim FrozenTab As DataTable = Me.dataSource.Tables(0)

        '----------------------------------------------------------------------
        '------------------------- Crear columnas -----------------------------
        '----------------------------------------------------------------------

        '--- Columna Item Sobrepuesta
        Dim supDataGridCustomColumn0 As New DataGridCustomTextBoxColumn
        With supDataGridCustomColumn0
            .Owner = Me.dgDevolucion
            .Format = "##"
            .FormatInfo = Nothing
            .HeaderText = "Cod" 'vehicle.Columns(0).ColumnName
            .MappingName = vehicle.Columns(0).ColumnName
            .Width = 90
            .AlternatingBackColor = alternatingColor
            .ReadOnly = True
        End With
        DataGridTableStyle1.GridColumnStyles.Add(supDataGridCustomColumn0)
        'DataGridTableStyle2.GridColumnStyles.Add(supDataGridCustomColumn0)


        '--- Columna Envases Sobrepuesta
        Dim supDataGridCustomColumn1 As New DataGridCustomTextBoxColumn
        With supDataGridCustomColumn1
            .Owner = Me.dgDevolucion
            .HeaderText = "Descripcion" 'vehicle.Columns(0).ColumnName
            .MappingName = vehicle.Columns(1).ColumnName
            .Width = 170
            .AlternatingBackColor = alternatingColor
            .ReadOnly = True
        End With
        DataGridTableStyle1.GridColumnStyles.Add(supDataGridCustomColumn1)
        'DataGridTableStyle2.GridColumnStyles.Add(supDataGridCustomColumn1)

        '---0 Columna Fast View
        Dim dataGridCustomColumn2 As New DataGridCustomTextBoxColumn
        With dataGridCustomColumn2
            .Owner = Me.dgDevolucion
            .HeaderText = "Teo." 'vehicle.Columns(0).ColumnName
            .MappingName = vehicle.Columns(2).ColumnName
            Width = 100
            .AlternatingBackColor = alternatingColor
            .ReadOnly = True
        End With
        DataGridTableStyle1.GridColumnStyles.Add(dataGridCustomColumn2)
        
        '=======================================================================

        '---1 Columna Cajas
        Dim dataGridCustomColumn3 As New DataGridCustomTextBoxColumn
        With dataGridCustomColumn3
            .Owner = Me.dgDevolucion
            .HeaderText = "CJ/UN" 'vehicle.Columns(0).ColumnName
            .MappingName = vehicle.Columns(3).ColumnName
            .Width = 80
            .AlternatingBackColor = alternatingColor
            .ReadOnly = True
        End With
        DataGridTableStyle1.GridColumnStyles.Add(dataGridCustomColumn3)

        '---2 Real
        Dim dataGridCustomColumn4 As New DataGridCustomUpDownColumn()
        With dataGridCustomColumn4
            .Owner = Me.dgDevolucion
            .HeaderText = "Real"
            .MappingName = vehicle.Columns(4).ColumnName
            '.NullText = "-Unknown-"
            .Width = 100
            .Alignment = HorizontalAlignment.Left
            .AlternatingBackColor = alternatingColor
            .ReadOnly = makeReadOnly
        End With
        Me.DataGridTableStyle1.GridColumnStyles.Add(dataGridCustomColumn4)



        '---3 Diferencia
        Dim dataGridCustomColumn5 As New DataGridCustomTextBoxColumn
        With dataGridCustomColumn5
            .Owner = Me.dgDevolucion
            .HeaderText = "Dif"
            .MappingName = vehicle.Columns(5).ColumnName
            .Width = 100
            .AlternatingBackColor = alternatingColor
            .ReadOnly = True
        End With
        DataGridTableStyle1.GridColumnStyles.Add(dataGridCustomColumn5)

        Dim dataGridCustomColumn6 As New DataGridCustomUpDownColumn()
        If tipoLiquidacion = 1 Then
            '---4 Roto
            With dataGridCustomColumn6
                .Owner = Me.dgDevolucion
                .HeaderText = "Rotura"
                .MappingName = vehicle.Columns(6).ColumnName
                .Width = 100
                .Alignment = HorizontalAlignment.Left
                .AlternatingBackColor = alternatingColor
                .ReadOnly = makeReadOnly
            End With
            Me.DataGridTableStyle1.GridColumnStyles.Add(dataGridCustomColumn6)
        End If

        Me.DataGridTableStyle2.MappingName = FrozenTab.TableName
        Me.dgFrozen.DataSource = FrozenTab                                      ' Setup grid's data source      
        Me.DataGridTableStyle1.MappingName = vehicle.TableName                  ' Setup table mapping name
        Me.dgDevolucion.DataSource = vehicle                                    ' Setup grid's data source        
        Me.dgDevolucion.Focus()


        '---------- Mapeo de tablas
        Me.DataGridTableStyle2.MappingName = FrozenTab.TableName
        Me.dgFrozen.DataSource = FrozenTab                                      ' Setup grid's data source      
        Me.DataGridTableStyle1.MappingName = vehicle.TableName                  ' Setup table mapping name
        Me.dgDevolucion.DataSource = vehicle                                    ' Setup grid's data source        
        Me.dgDevolucion.Focus()
        txtReal = CType(dataGridCustomColumn4.HostedControl, NumericUpDown)
        If tipoLiquidacion = 1 Then
            txtRotura = CType(dataGridCustomColumn6.HostedControl, NumericUpDown)
        End If
        Try
            dgDevolucion.CurrentCell = New DataGridCell(0, 4)
        Catch ex As Exception
        End Try
    End Sub
#End Region

End Class

