Imports System.Data
Public Class frmPreguntasDetalle

    Dim objUtil As New UtilitarioBL
    Public tipoControl As String
    Public idPregunta As String


    Private Sub rSoft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rSoft.Click
        Dim respuesta As String
        respuesta = validarRespuesta()
        If respuesta <> "" Then
            '---Grabar respuesta
            Dim objRuta As New RutaBL
            If objRuta.responderPregunta(idPregunta, respuesta) Then
                Me.Close()
            Else
                MsgBox("No se pudo agregar la respuesta.")
            End If
        End If
    End Sub

    Private Function validarRespuesta() As String
        Select Case tipoControl
            Case "NUMBER"
                Dim objUtil As New UtilitarioBL
                Dim valorNumerico As Decimal
                valorNumerico = objUtil.isDecimalUKID(rString.Text)
                If valorNumerico = -78737 Then
                    MsgBox("Debe escribir un valor numerico")
                    rString.Text = ""
                    Return ""
                Else
                    Return valorNumerico.ToString
                End If

            Case "STRING"
                If rString.Text = "" Then
                    rString.Text = ""
                    Return ""
                Else
                    Return rString.Text
                End If
            Case "BOOL"
                If rBool.SelectedIndex = 0 Then
                    MsgBox("Seleccione una respuesta del listado.")
                    rBool.SelectedIndex = 0
                    Return ""
                Else
                    Return rBool.Text
                End If
            Case "DATETIME"
                Return rFecha.Value.ToString
            Case Else
                Return ""
        End Select
    End Function

    Private Sub panPregunta_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panPregunta.Paint
        objUtil.paintPannel(e, panPregunta)
    End Sub

    Private Sub panRespuesta_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panRespuesta.Paint
        objUtil.paintPannel(e, panRespuesta)
    End Sub

    Private Sub panTitulo_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panTitulo.Paint
        objUtil.paintPannel(e, panTitulo)
    End Sub

   

   
End Class