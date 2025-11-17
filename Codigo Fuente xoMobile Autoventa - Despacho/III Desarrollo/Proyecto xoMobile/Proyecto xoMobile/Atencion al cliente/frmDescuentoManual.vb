Public Class frmDescuentoManual
    Public maximo As Decimal
    Dim oUtil As New UtilitarioBL

    Private Sub frmDescuentoManual_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtDescuento.Focus()
        Cursor.Current = Cursors.Default
    End Sub
   
    Private Sub txtDescuento_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDescuento.KeyPress

        If e.KeyChar() = ChrW(13) Then
            Dim objUtil As New UtilitarioBL
            Dim valorNumerico As Decimal
            valorNumerico = objUtil.isDecimalUKID(txtDescuento.Text)
            If valorNumerico = -78737 Then
                MsgBox("Debe escribir un valor numerico")
                txtDescuento.SelectAll()
            Else
                If valorNumerico > maximo Or valorNumerico < 0 Then
                    MsgBox("Escriba un valor entre 0 y " + maximo.ToString)
                    txtDescuento.SelectAll()
                Else
                    Cursor.Current = Cursors.WaitCursor
                    Me.Close()
                End If
            End If
        End If
    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint
        oUtil.paintPannel(e, Panel2)
    End Sub
End Class