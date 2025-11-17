Imports System.Data
Public Class frmClientesSaldo
    Dim objClienteBL As New ClienteBL


    Private Sub frmClientesSaldo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            crearListadoDeClientesConSaldo()
            setAlternatingRowColor()
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            MsgBox("Error al crear listado de clientes con saldo: " + ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub setAlternatingRowColor()
        For Each item As ListViewItem In lstClientesSaldo.Items
            If ((item.Index Mod 2) = 0) Then
                item.BackColor = Color.AliceBlue
                lstClientesSaldo.Items(item.Index).BackColor = Color.AliceBlue
            Else
                lstClientesSaldo.Items(item.Index).BackColor = Color.LightYellow
            End If
        Next
    End Sub

    Private Sub crearListadoDeClientesConSaldo()

        'Obtener el listado de clientes
        Dim dtclientesSaldo As New DataTable
        dtclientesSaldo = objClienteBL.getClientesConSaldo()
        Dim codigo = New ColumnHeader()
        Dim negocio = New ColumnHeader()
        Dim propietario = New ColumnHeader()
        Dim direccion = New ColumnHeader()
        Dim telefono = New ColumnHeader()
        Dim totalImporte = New ColumnHeader()
        Dim totalSaldo = New ColumnHeader()
        Dim diaVisita = New ColumnHeader()

        codigo.Text = "Codigo" '0
        negocio.Text = "Negocio" '1
        direccion.Text = "Direccion" '3
        telefono.Text = "telefono" '4
        totalImporte.Text = "Importe Tot."
        totalSaldo.Text = "Saldo Tot."
        diaVisita.Text = "Dia Visita"
        codigo.Width = 75
        direccion.Width = 200

        lstClientesSaldo.Columns.Add(codigo)
        lstClientesSaldo.Columns.Add(negocio)
        lstClientesSaldo.Columns.Add(totalImporte)
        lstClientesSaldo.Columns.Add(totalSaldo)
        lstClientesSaldo.Columns.Add(diaVisita)
        lstClientesSaldo.Columns.Add(direccion)
        lstClientesSaldo.Columns.Add(telefono)

        '--- Agregar filas a la lista
        lstClientesSaldo.Items.Clear()
        For i As Integer = 0 To dtclientesSaldo.Rows.Count - 1
            Dim drow As DataRow = dtclientesSaldo.Rows(i)
            Dim lvi As New ListViewItem(drow("codigo").ToString())
            lvi.SubItems.Add(drow("negocio").ToString())
            lvi.SubItems.Add(FormatCurrency(drow("totalImporte"), 2).ToString())
            lvi.SubItems.Add(FormatCurrency(drow("totalSaldo"), 2).ToString())
            lvi.SubItems.Add(drow("diaVisita").ToString())
            lvi.SubItems.Add(drow("direccion").ToString())
            lvi.SubItems.Add(drow("telefono").ToString())
            lstClientesSaldo.Items.Add(lvi)
        Next
        
    End Sub

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        Me.Close()
    End Sub
End Class