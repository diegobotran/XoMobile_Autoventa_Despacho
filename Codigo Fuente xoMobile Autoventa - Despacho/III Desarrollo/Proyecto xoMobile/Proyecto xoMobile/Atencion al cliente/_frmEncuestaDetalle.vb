Public Class frmEncuestaDetalle
    Dim objUtil As New UtilitarioBL
    Public idEncuesta, idtema, idPregunta, idEncEncuesta As String

    Private Sub rSoft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rSoft.Click
        Dim respuesta As String
        respuesta = validarRespuesta()
        If respuesta <> "" Then
            '---Grabar respuesta
            Dim objEncuesta As New EncuestaBL
            If objEncuesta.agregarRespuesta(idEncEncuesta, idEncuesta, idtema, idPregunta, 0, respuesta) Then
                Me.Close()
            Else
                MsgBox("No se pudo agregar la respuesta.")
            End If
        End If
        txtRespuesta.Text = ""
    End Sub

    Private Function validarRespuesta() As String
        If txtRespuesta.Visible Then
            If txtRespuesta.Text = "" Then
                MsgBox("No puede dejar en blanco la respuesta.")
                Return ""
            Else
                Return txtRespuesta.Text
            End If
        End If
        If lstList.Visible Then
            If lstList.SelectedIndex = 0 Then
                MsgBox("Seleccione una respuesta del listado.")
                Return ""
            Else
                Return lstList.Text
            End If
        End If
        Return True
    End Function

#Region " Dibujar paneles"

    Private Sub panTitulo_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panTitulo.Paint
        objUtil.paintPannel(e, panTitulo)
    End Sub

    Private Sub panPregunta_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panPregunta.Paint
        objUtil.paintPannel(e, panPregunta)
    End Sub

    Private Sub panRespuesta_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panRespuesta.Paint
        objUtil.paintPannel(e, panRespuesta)
    End Sub
#End Region

    
End Class