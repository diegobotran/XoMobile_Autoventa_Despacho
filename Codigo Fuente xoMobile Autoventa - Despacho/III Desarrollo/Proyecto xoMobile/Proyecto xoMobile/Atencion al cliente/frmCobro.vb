Imports System.Data
Imports Proyecto_xoMobile_Packs

Public Class frmCobro
    Dim objRecibo As New Recibo
    Dim objUtil As New UtilitarioDT
    Dim objNc As New NotaCreditoDT
    Public dtDocumentosPorcobrar As New DataTable
    Public drCliente As DataRow

    'Message box
    Dim msg As String
    Dim title As String
    Dim style As MsgBoxStyle
    Dim response As MsgBoxResult

    'Objetos de Comunicacion entre capas
    'Public comDocumento As New documentoCO
    Public v_com_cliente As New ClienteCO

    'Objetos de la capa de negocio
    Dim objDocumentoBL As New DocumentoBL
    'Dim objCobroBL As New wfCobro
    Public rlayer As New rLayerHandler


    Private Sub frmCobro_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Obtener el listado de documentos por cobrar
            dtDocumentosPorcobrar = objRecibo.getDocumentosPorcobrar
            If dtDocumentosPorcobrar.Rows.Count() <= 0 Then
                rlayer.codigo = 500
                Throw New Exception("No hay documentos para cobrar")
            Else
                crearLvDocumentos()
                Cursor.Current = Cursors.Default
            End If

            
        Catch ex As Exception
            MsgBox(ex.Message)
            Cursor.Current = Cursors.Default
            co_glo_NextForm = True
            Me.Close()
        End Try
    End Sub

    Private Sub crearLvDocumentos()

        'Limpiar
        lstDocumentos.Clear()
        Dim id = New ColumnHeader()
        Dim tipoDocumento = New ColumnHeader()
        Dim tipo = New ColumnHeader()
        Dim numero = New ColumnHeader()
        Dim importe = New ColumnHeader()
        Dim saldo = New ColumnHeader()
        Dim fechaEmision = New ColumnHeader()
        Dim fechaVencimiento = New ColumnHeader()
        Dim diasVencidos = New ColumnHeader()
        Dim descuento = New ColumnHeader()
        Dim serieFel = New ColumnHeader()
        Dim numeroautorizacion = New ColumnHeader()
        Dim diasVencidosNC = New ColumnHeader()


        tipoDocumento.Text = "Tipo"         '1
        tipo.Text = "Serie"                 '2
        numero.Text = "Numero"              '3
        importe.text = "Valor"              '4
        saldo.Text = "Saldo"                '5
        fechaEmision.Text = "F. Emision"    '6
        fechaVencimiento.Text = "F. Vence"  '7
        diasVencidos.Text = "D. Vencidos"   '8
        descuento.Text = "DPP"              '9
        serieFel.Text = "serieFEL"              '10
        numeroautorizacion.Text = "Autorizacion"              '11
        diasVencidosNC.Text = "D. NC"              '12

        lstDocumentos.Columns.Add(id)
        lstDocumentos.Columns.Add(tipoDocumento)
        lstDocumentos.Columns.Add(tipo)
        lstDocumentos.Columns.Add(numero)
        lstDocumentos.Columns.Add(importe)
        lstDocumentos.Columns.Add(saldo)
        lstDocumentos.Columns.Add(fechaEmision)
        lstDocumentos.Columns.Add(fechaVencimiento)
        lstDocumentos.Columns.Add(diasVencidos)
        lstDocumentos.Columns.Add(descuento)
        lstDocumentos.Columns.Add(serieFel)
        lstDocumentos.Columns.Add(numeroautorizacion)
        lstDocumentos.Columns.Add(diasVencidosNC)

        For i As Integer = 0 To dtDocumentosPorcobrar.Rows.Count - 1
            Dim drow As DataRow = dtDocumentosPorcobrar.Rows(i)

            'Agregar Items a la fila
            Dim lvi As New ListViewItem(i + 1.ToString())
            lvi.SubItems.Add(drow("dTipo").ToString())
            lvi.SubItems.Add(drow("serie").ToString())
            lvi.SubItems.Add(drow("numero").ToString())
            lvi.SubItems.Add(FormatCurrency(drow("importe").ToString(), 2))
            lvi.SubItems.Add(FormatCurrency(drow("saldo").ToString(), 2))
            lvi.SubItems.Add(drow("fechaEmision").ToString())
            lvi.SubItems.Add(drow("fechaVence").ToString())
            lvi.SubItems.Add(drow("diasVencidos").ToString())
            

            If objUtil.isDecimal(drow("diasVencidos")) <= 0 Then
                lvi.SubItems.Add(drow("importeDesto").ToString())
            Else
                lvi.SubItems.Add("")
            End If

            lvi.SubItems.Add(drow("serieFel").ToString())
            lvi.SubItems.Add(drow("numeroautorizacion").ToString())
            lvi.SubItems.Add(drow("DiasVencidosE").ToString())

            

            '--- Identificar CXC con compromiso de pago 
            If (drow("compromisoPago") = True) Then
                lvi.BackColor = xoInfomat
                lvi.Checked = True
            End If


            'Identificar con color rojo los documentos obligatorios de pago
            If (Trim(drow("dTipo").ToString()) = "FCHR") Or (Trim(drow("dTipo").ToString()) = "CHER") Then
                lvi.ForeColor = Color.Maroon
                lvi.Checked = True
                'drow("soloEfectivo") = 1
            End If
            lstDocumentos.Items.Add(lvi)
        Next
    End Sub

    Private Sub lstDocumentos_ItemCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles lstDocumentos.ItemCheck

        'Capturar el valor del saldo
        Dim saldo = dtDocumentosPorcobrar.Rows(e.Index).Item("saldo")

        'Aumentar el total a pagar
        Select Case e.CurrentValue
            Case CheckState.Checked
                total_envase = 0
                txtCobro.Tag = objUtil.isDecimal(txtCobro.Tag) - objUtil.isDecimal(saldo)
                txtCobro.Text = FormatCurrency(txtCobro.Tag, 2)
                txtMaxEnvase.Text = ((txtCobro.Tag) * 75) / 100
                total_venta = objUtil.isDecimal(txtCobro.Tag)
            Case CheckState.Unchecked
                total_envase = 0
                txtCobro.Tag = objUtil.isDecimal(txtCobro.Tag) + objUtil.isDecimal(saldo)
                txtCobro.Text = FormatCurrency(txtCobro.Tag, 2)
                txtMaxEnvase.Text = ((txtCobro.Tag) * 75) / 100
                total_venta = objUtil.isDecimal(txtCobro.Tag)
        End Select
    End Sub

    Private Function tieneItems() As Boolean
        If txtCobro.Text <> 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub softPagar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles softPagar.Click

        If tieneItems() Then
            ''Marcar documentos para pago.
            For i As Integer = 0 To lstDocumentos.Items().Count() - 1
                If lstDocumentos.Items(i).Checked Then
                    dtDocumentosPorcobrar.Rows(i).Item("seleccionado") = 1
                Else
                    If lstDocumentos.Items(i).ForeColor = Color.Maroon Then
                        MsgBox("Los documentos de color tienen prioridad de pago.")
                        Return
                    End If
                End If
            Next

            '--- Retornar al programa de control
            Me.Close()
        Else
            MsgBox("No hay documentos seleccionados para pagar")
        End If
    End Sub

    Private Sub softCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles softCancelar.Click
        Try
            If lstDocumentos.Items.Count > 0 Then
                msg = "Desea cancelar el cobro de cuentas pendientes?"
                title = "Cancelar"
                style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Question Or MsgBoxStyle.YesNo
                response = MsgBox(msg, style, title)
                If response = MsgBoxResult.Yes Then
                    '--- Ir al menu atencion al cliente
                    Me.Close()
                Else
                End If
            Else
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Me.Close()
        End Try
    End Sub
End Class