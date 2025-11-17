Imports System.Data

Public Class frmPreguntas
    Public ttipo As Integer = 1
    Dim FlagOut As Boolean
    Dim FlagOutError As Boolean = False
    Public PreguntasDetalle As New frmPreguntasDetalle
    Dim itemSelected As Integer

    Private Sub ocultarRespuestas()
        PreguntasDetalle.rString.Visible = False
        PreguntasDetalle.rString.Text = ""
        PreguntasDetalle.rFecha.Visible = False
        PreguntasDetalle.rFecha.Value = Date.Today
        PreguntasDetalle.rBool.Visible = False
        PreguntasDetalle.rBool.SelectedIndex = 0
    End Sub

    Private Sub iniciarPreguntas()
        Dim objRuta As New RutaBL
        Dim dtPreguntas As New DataTable
        Try
            dtPreguntas = objRuta.obtenerPreguntas(ttipo)
            FlagOut = False
            For i As Integer = 0 To dtPreguntas.Rows.Count - 1
                Dim drow As DataRow = dtPreguntas.Rows(i)
                PreguntasDetalle.lblQuedan.Text = (i + 1).ToString + " de " + (dtPreguntas.Rows.Count).ToString
                If ttipo = 1 Then
                    PreguntasDetalle.lblTitulo.Text = "Datos Iniciales"
                Else
                    PreguntasDetalle.lblTitulo.Text = "Datos Finales"
                End If
                PreguntasDetalle.txtPregunta.Text = drow("pregunta")
                Select Case drow("tipoDato")
                    Case "NUMBER"
                        ocultarRespuestas()
                        PreguntasDetalle.rString.Visible = True
                        PreguntasDetalle.rString.Focus()
                    Case "STRING"
                        ocultarRespuestas()
                        PreguntasDetalle.rString.Visible = True
                        PreguntasDetalle.rString.Focus()
                    Case "BOOL"
                        ocultarRespuestas()
                        PreguntasDetalle.rBool.Visible = True
                        PreguntasDetalle.rBool.Focus()
                    Case "DATETIME"
                        ocultarRespuestas()
                        PreguntasDetalle.rFecha.Visible = True
                        PreguntasDetalle.rFecha.Focus()
                End Select
                PreguntasDetalle.tipoControl = drow("tipoDato")
                PreguntasDetalle.idPregunta = drow("id_SalidaIngreso")
                PreguntasDetalle.ShowDialog()
            Next
            FlagOut = True
        Catch ex As Exception
            MsgBox(ex.Message)
            Dim objBitacora As New BitacoraBL
            If ttipo = 1 Then
                objBitacora.registrarOperacion(9, 0)
            Else
                objBitacora.registrarOperacion(10, 0)
            End If
            FlagOutError = True
        End Try
    End Sub

    Private Sub frmPreguntas_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Try
            If FlagOutError Then
                Me.Close()
            Else
                If FlagOut Then
                    MsgBox("Datos capturados con exito.")
                    Dim objBitacora As New BitacoraBL
                    If ttipo = 1 Then
                        objBitacora.registrarOperacion(9, 0)
                    Else
                        objBitacora.registrarOperacion(10, 0)
                    End If
                    Me.Close()
                Else
                    Cursor.Current = Cursors.Default
                    iniciarPreguntas()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmPreguntas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If ttipo = 1 Then
            Me.Text = "Datos Iniciales."
        Else
            Me.Text = "Datos Finales."
        End If
    End Sub
End Class