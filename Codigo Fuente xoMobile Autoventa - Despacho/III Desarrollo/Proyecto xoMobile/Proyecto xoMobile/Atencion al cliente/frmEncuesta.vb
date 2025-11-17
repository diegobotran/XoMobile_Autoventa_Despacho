Imports System.Data

Public Class frmEncuesta
    Dim objEncuestas As New EncuestaBL
    Dim dtEncuestas As New DataTable
    Public FinishFlag As Boolean = False

    Private Sub inicializarListView()
        Dim img = New ColumnHeader()
        Dim titulo = New ColumnHeader()
        Dim fechaFin = New ColumnHeader()
        lstEncuestas.Clear()
        FinishFlag = True
        img.Text = ""
        img.Width = 30
        titulo.Text = "Titulo" '0
        titulo.Width = 150
        fechaFin.Text = "Fecha Limite" '1
        fechaFin.Width = 100
        lstEncuestas.Columns.Add(img)
        lstEncuestas.Columns.Add(titulo)
        lstEncuestas.Columns.Add(fechaFin)

        '--- obtener listado de encuestas
        dtEncuestas = objEncuestas.getListadoEncuestas()
        For i As Integer = 0 To dtEncuestas.Rows.Count - 1
            Dim drow As DataRow = dtEncuestas.Rows(i)
            Dim lvi As New ListViewItem("")
            lvi.SubItems.Add(drow("Titulo").ToString())
            lvi.SubItems.Add(drow("FechaFin").ToString())
            lstEncuestas.Items.Add(lvi)
            lstEncuestas.Items(i).ImageIndex = drow("estado")
            If drow("estado") = 0 Then
                FinishFlag = False
            End If
        Next
    End Sub

    Private Sub frmEncuesta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            inicializarListView()
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            MsgBox(ex.Message)
            Cursor.Current = Cursors.Default
            Me.Close()
        End Try
    End Sub



    Private Sub rSoft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rSoft.Click
        Dim idEncEncuesta As String
        If Me.lstEncuestas.SelectedIndices.Count <= 0 Then
            MsgBox("No ha seleccionado una encuesta.")
            Return
        End If
        Dim itemSelected = Me.lstEncuestas.SelectedIndices(0)
        If lstEncuestas.Items(itemSelected).ImageIndex = 1 Then
            MsgBox("Esta encuesta ya fue realizada.")
            Return
        End If
        lstEncuestas.Visible = False
        idEncEncuesta = objEncuestas.agregarEncabezadoEncuesta(dtEncuestas.Rows(itemSelected).Item("id_encuesta"))
        ejecutarEncuesta(dtEncuestas.Rows(itemSelected).Item("id_encuesta"), itemSelected, idEncEncuesta)

    End Sub

    Private Sub ejecutarEncuesta(ByVal idEncuesta As String, ByVal itemSelected As Integer, ByVal idEncEncuesta As String)
        Dim dtPreguntas As New DataTable
        Dim frmEncuestaDetalle As New frmEncuestaDetalle
        Dim oUtil As New UtilitarioBL
        Try
            dtPreguntas = objEncuestas.getListadoPreguntas(idEncuesta)
            For i As Integer = 0 To dtPreguntas.Rows.Count - 1
                Dim drow As DataRow = dtPreguntas.Rows(i)
                frmEncuestaDetalle.lblQuedan.Text = (i + 1).ToString + " de " + (dtPreguntas.Rows.Count).ToString
                frmEncuestaDetalle.lblTitulo.Text = lstEncuestas.Items(itemSelected).SubItems(1).Text
                frmEncuestaDetalle.lblTituloTema.Text = drow("tema")
                frmEncuestaDetalle.txtPregunta.Text = drow("pregunta")
                frmEncuestaDetalle.txtPregunta.Visible = True
                frmEncuestaDetalle.idEncEncuesta = idEncEncuesta
                frmEncuestaDetalle.idEncuesta = idEncuesta
                frmEncuestaDetalle.idtema = drow("idtema")
                frmEncuestaDetalle.idPregunta = drow("id_pregunta")
                Select Case oUtil.isInteger(drow("tipoRespuesta"))
                    Case 0
                        frmEncuestaDetalle.txtRespuesta.Visible = True
                        frmEncuestaDetalle.lstList.Visible = False
                        frmEncuestaDetalle.rBool.Visible = False
                        frmEncuestaDetalle.txtRespuesta.Focus()
                    Case 1
                        Dim dtAlternativas As New DataTable
                        dtAlternativas = objEncuestas.obtenerRespuestasAlternativas(drow("id_pregunta"))
                        frmEncuestaDetalle.lstList.DataSource = dtAlternativas
                        frmEncuestaDetalle.lstList.DisplayMember = dtAlternativas.Columns("alternativa").ColumnName.ToString()
                        frmEncuestaDetalle.lstList.ValueMember = dtAlternativas.Columns("id_Alternativa").ColumnName.ToString()
                        frmEncuestaDetalle.lstList.Visible = True
                        frmEncuestaDetalle.txtRespuesta.Visible = False
                        frmEncuestaDetalle.rBool.Visible = False
                        frmEncuestaDetalle.lstList.Focus()
                    Case 2
                        frmEncuestaDetalle.rBool.Visible = True
                        frmEncuestaDetalle.lstList.Visible = False
                        frmEncuestaDetalle.txtRespuesta.Visible = False
                        frmEncuestaDetalle.rBool.Focus()
                End Select
                frmEncuestaDetalle.ShowDialog()
            Next
            '--- Fin de la encuesta
            inicializarListView()
            lstEncuestas.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub panTiempo_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panTiempo.Paint
        Dim objutil As New UtilitarioBL
        objutil.paintPannel(e, panTiempo)
    End Sub

    Private Sub lSoft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lSoft.Click
        Me.Close()
    End Sub
End Class