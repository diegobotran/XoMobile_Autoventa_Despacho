<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmEncuestaDetalle
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Private menuRespuestas As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.menuRespuestas = New System.Windows.Forms.MainMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.rSoft = New System.Windows.Forms.MenuItem
        Me.txtRespuesta = New System.Windows.Forms.TextBox
        Me.txtPregunta = New System.Windows.Forms.TextBox
        Me.lstList = New System.Windows.Forms.ComboBox
        Me.lblTituloTema = New System.Windows.Forms.Label
        Me.panTitulo = New System.Windows.Forms.Panel
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.panRespuesta = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.panPregunta = New System.Windows.Forms.Panel
        Me.lblQuedan = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.rBool = New System.Windows.Forms.ComboBox
        Me.panTitulo.SuspendLayout()
        Me.panRespuesta.SuspendLayout()
        Me.panPregunta.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuRespuestas
        '
        Me.menuRespuestas.MenuItems.Add(Me.MenuItem1)
        Me.menuRespuestas.MenuItems.Add(Me.rSoft)
        '
        'MenuItem1
        '
        Me.MenuItem1.Text = " "
        '
        'rSoft
        '
        Me.rSoft.Text = "Siguiente"
        '
        'txtRespuesta
        '
        Me.txtRespuesta.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.txtRespuesta.Location = New System.Drawing.Point(0, 194)
        Me.txtRespuesta.Multiline = True
        Me.txtRespuesta.Name = "txtRespuesta"
        Me.txtRespuesta.Size = New System.Drawing.Size(240, 74)
        Me.txtRespuesta.TabIndex = 17
        Me.txtRespuesta.Visible = False
        '
        'txtPregunta
        '
        Me.txtPregunta.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtPregunta.Location = New System.Drawing.Point(0, 105)
        Me.txtPregunta.Multiline = True
        Me.txtPregunta.Name = "txtPregunta"
        Me.txtPregunta.ReadOnly = True
        Me.txtPregunta.Size = New System.Drawing.Size(240, 65)
        Me.txtPregunta.TabIndex = 16
        '
        'lstList
        '
        Me.lstList.Location = New System.Drawing.Point(0, 194)
        Me.lstList.Name = "lstList"
        Me.lstList.Size = New System.Drawing.Size(240, 22)
        Me.lstList.TabIndex = 15
        Me.lstList.Visible = False
        '
        'lblTituloTema
        '
        Me.lblTituloTema.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.lblTituloTema.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.lblTituloTema.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.lblTituloTema.Location = New System.Drawing.Point(0, 55)
        Me.lblTituloTema.Name = "lblTituloTema"
        Me.lblTituloTema.Size = New System.Drawing.Size(240, 24)
        Me.lblTituloTema.Text = "---Titulo del tema ---"
        Me.lblTituloTema.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'panTitulo
        '
        Me.panTitulo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.panTitulo.Controls.Add(Me.lblTitulo)
        Me.panTitulo.Location = New System.Drawing.Point(0, 0)
        Me.panTitulo.Name = "panTitulo"
        Me.panTitulo.Size = New System.Drawing.Size(240, 55)
        '
        'lblTitulo
        '
        Me.lblTitulo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.lblTitulo.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(89, Byte), Integer), CType(CType(152, Byte), Integer))
        Me.lblTitulo.Location = New System.Drawing.Point(30, 9)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(189, 35)
        Me.lblTitulo.Text = "Seleccione una Encuesta"
        Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'panRespuesta
        '
        Me.panRespuesta.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.panRespuesta.Controls.Add(Me.Label1)
        Me.panRespuesta.Location = New System.Drawing.Point(0, 169)
        Me.panRespuesta.Name = "panRespuesta"
        Me.panRespuesta.Size = New System.Drawing.Size(240, 26)
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 13)
        Me.Label1.Text = "Respuesta:"
        '
        'panPregunta
        '
        Me.panPregunta.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.panPregunta.Controls.Add(Me.lblQuedan)
        Me.panPregunta.Controls.Add(Me.Label2)
        Me.panPregunta.Location = New System.Drawing.Point(0, 80)
        Me.panPregunta.Name = "panPregunta"
        Me.panPregunta.Size = New System.Drawing.Size(240, 26)
        '
        'lblQuedan
        '
        Me.lblQuedan.Location = New System.Drawing.Point(82, 5)
        Me.lblQuedan.Name = "lblQuedan"
        Me.lblQuedan.Size = New System.Drawing.Size(64, 17)
        Me.lblQuedan.Text = "1 de 5"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(12, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 17)
        Me.Label2.Text = "Pregunta:"
        '
        'rBool
        '
        Me.rBool.Items.Add("--- Seleccione --- ")
        Me.rBool.Items.Add("SI")
        Me.rBool.Items.Add("NO")
        Me.rBool.Location = New System.Drawing.Point(0, 195)
        Me.rBool.Name = "rBool"
        Me.rBool.Size = New System.Drawing.Size(240, 22)
        Me.rBool.TabIndex = 26
        Me.rBool.Visible = False
        '
        'frmEncuestaDetalle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.ControlBox = False
        Me.Controls.Add(Me.rBool)
        Me.Controls.Add(Me.panTitulo)
        Me.Controls.Add(Me.panPregunta)
        Me.Controls.Add(Me.panRespuesta)
        Me.Controls.Add(Me.txtRespuesta)
        Me.Controls.Add(Me.txtPregunta)
        Me.Controls.Add(Me.lstList)
        Me.Controls.Add(Me.lblTituloTema)
        Me.Menu = Me.menuRespuestas
        Me.Name = "frmEncuestaDetalle"
        Me.Text = "Encuesta"
        Me.panTitulo.ResumeLayout(False)
        Me.panRespuesta.ResumeLayout(False)
        Me.panPregunta.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtRespuesta As System.Windows.Forms.TextBox
    Friend WithEvents txtPregunta As System.Windows.Forms.TextBox
    Friend WithEvents lstList As System.Windows.Forms.ComboBox
    Friend WithEvents lblTituloTema As System.Windows.Forms.Label
    Friend WithEvents panTitulo As System.Windows.Forms.Panel
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents rSoft As System.Windows.Forms.MenuItem
    Friend WithEvents panRespuesta As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents panPregunta As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblQuedan As System.Windows.Forms.Label
    Friend WithEvents rBool As System.Windows.Forms.ComboBox
End Class
