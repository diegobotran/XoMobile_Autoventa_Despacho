Public Class mConsulta

    Private Sub rSoft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rSoft.Click
        Cursor.Current = Cursors.WaitCursor
        realizar()
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub realizar()
        Try
            Dim objBitacora As New BitacoraBL

            '--- Obtener el indice del item seleccionado
            If Me.lstConsulta.SelectedIndices.Count <= 0 Then
                Return
            End If
            Dim itemSelected = Me.lstConsulta.SelectedIndices(0)

            '-------------------------------------------------------------------------------------------------'
            '------------------------       CONSULTA DE CLIENTES CON SALDO      ------------------------------'
            '-------------------------------------------------------------------------------------------------'
            If lstConsulta.Items(itemSelected).Tag = "0" Then
                objBitacora.registrarOperacion(27, id_glo_cliente)
                Dim frmClientesConSaldo As New frmClientesSaldo
                frmClientesConSaldo.ShowDialog()
                Return
            End If

            '-------------------------------------------------------------------------------------------------'
            '------------------------       CONSULTA PURESUPUESTO DE RUTA       ------------------------------'
            '-------------------------------------------------------------------------------------------------'
            If lstConsulta.Items(itemSelected).Tag = "7" Then
                Dim frmPresupuesto As New frmPresupuesto
                frmPresupuesto.ShowDialog()
                Return
            End If
            

            '-------------------------------------------------------------------------------------------------'
            '---------------------------       CONSULTA DE CARGA RECARGA      --------------------------------'
            '-------------------------------------------------------------------------------------------------'
            If lstConsulta.Items(itemSelected).Tag = "1" Then
                objBitacora.registrarOperacion(28, id_glo_cliente)
                Dim frmConsultaCarga As New frmConsultaCarga
                frmConsultaCarga.ShowDialog()
                Return
            End If

            '-------------------------------------------------------------------------------------------------'
            '---------------------------       CONSULTA DE DOCUMENTOS EMITIDOS      --------------------------------'
            '-------------------------------------------------------------------------------------------------'
            If lstConsulta.Items(itemSelected).Tag = "2" Then
                objBitacora.registrarOperacion(29, id_glo_cliente)
                Dim frmDocumentosEmitidos As New frmDocumentosEmitidos
                frmDocumentosEmitidos.ShowDialog()
                Return
            End If

            '-------------------------------------------------------------------------------------------------'
            '--------------------       CONSULTA DE INVENTARIO ACTUAL DE PRODUCTO      -----------------------'
            '-------------------------------------------------------------------------------------------------'
            If lstConsulta.Items(itemSelected).Tag >= 3 Then
                Dim frmInventarioProducto As New frmInventarioProducto
                frmInventarioProducto.codigoReporte = lstConsulta.Items(itemSelected).Tag
                frmInventarioProducto.ShowDialog()
                Return
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub mConsulta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub lSoft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lSoft.Click
        Me.Close()
    End Sub

    Private Sub lstConsulta_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstConsulta.SelectedIndexChanged

    End Sub
End Class