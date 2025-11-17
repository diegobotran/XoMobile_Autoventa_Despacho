<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmPreguntasDetalle
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
    Private menuPreguntas As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.menuPreguntas = New System.Windows.Forms.MainMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.rSoft = New System.Windows.Forms.MenuItem
        Me.lblQuedan = New System.Windows.Forms.Label
        Me.txtPregunta = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.panRespuesta = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.panPregunta = New System.Windows.Forms.Panel
        Me.rString = New System.Windows.Forms.TextBox
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.panTitulo = New System.Windows.Forms.Panel
        Me.rBool = New System.Windows.Forms.ComboBox
        Me.rFecha = New System.Windows.Forms.DateTimePicker
        Me.panRespuesta.SuspendLayout()
        Me.panPregunta.SuspendLayout()
        Me.panTitulo.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuPreguntas
        '
        Me.menuPreguntas.MenuItems.Add(Me.MenuItem1)
        Me.menuPreguntas.MenuItems.Add(Me.rSoft)
        '
        'MenuItem1
        '
        Me.MenuItem1.Text = " "
        '
        'rSoft
        '
        Me.rSoft.Text = "Siguiente"
        '
        'lblQuedan
        '
        Me.lblQuedan.Location = New System.Drawing.Point(82, 5)
        Me.lblQuedan.Name = "lblQuedan"
        Me.lblQuedan.Size = New System.Drawing.Size(64, 17)
        Me.lblQuedan.Text = "1 de 5"
        '
        'txtPregunta
        '
        Me.txtPregunta.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtPregunta.Location = New System.Drawing.Point(0, 70)
        Me.txtPregunta.Multiline = True
        Me.txtPregunta.Name = "txtPregunta"
        Me.txtPregunta.ReadOnly = True
        Me.txtPregunta.Size = New System.Drawing.Size(240, 84)
        Me.txtPregunta.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(12, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 17)
        Me.Label2.Text = "Pregunta:"
        '
        'panRespuesta
        '
        Me.panRespuesta.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.panRespuesta.Controls.Add(Me.Label1)
        Me.panRespuesta.Location = New System.Drawing.Point(0, 153)
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
        Me.panPregunta.Location = New System.Drawing.Point(0, 45)
        Me.panPregunta.Name = "panPregunta"
        Me.panPregunta.Size = New System.Drawing.Size(240, 26)
        '
        'rString
        '
        Me.rString.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.rString.Location = New System.Drawing.Point(0, 178)
        Me.rString.Multiline = True
        Me.rString.Name = "rString"
        Me.rString.Size = New System.Drawing.Size(240, 90)
        Me.rString.TabIndex = 20
        Me.rString.Visible = False
        '
        'lblTitulo
        '
        Me.lblTitulo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.lblTitulo.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(89, Byte), Integer), CType(CType(152, Byte), Integer))
        Me.lblTitulo.Location = New System.Drawing.Point(3, 8)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(233, 35)
        Me.lblTitulo.Text = "--- Titulo del formulario---"
        Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'panTitulo
        '
        Me.panTitulo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.panTitulo.Controls.Add(Me.lblTitulo)
        Me.panTitulo.Location = New System.Drawing.Point(0, 0)
        Me.panTitulo.Name = "panTitulo"
        Me.panTitulo.Size = New System.Drawing.Size(240, 46)
        '
        'rBool
        '
        Me.rBool.Items.Add("--- Seleccione --- ")
        Me.rBool.Items.Add("SI")
        Me.rBool.Items.Add("NO")
        Me.rBool.Location = New System.Drawing.Point(0, 178)
        Me.rBool.Name = "rBool"
        Me.rBool.Size = New System.Drawing.Size(240, 22)
        Me.rBool.TabIndex = 25
        Me.rBool.Visible = False
        '
        'rFecha
        '
        Me.rFecha.CustomFormat = "d 'de' MMM 'de' yyyy.  hh:mm:ss tt"
        Me.rFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.rFecha.Location = New System.Drawing.Point(0, 178)
        Me.rFecha.Name = "rFecha"
        Me.rFecha.Size = New System.Drawing.Size(240, 22)
        Me.rFecha.TabIndex = 26
        '
        'frmPreguntasDetalle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.ControlBox = False
        Me.Controls.Add(Me.rFecha)
        Me.Controls.Add(Me.rBool)
        Me.Controls.Add(Me.panTitulo)
        Me.Controls.Add(Me.txtPregunta)
        Me.Controls.Add(Me.panRespuesta)
        Me.Controls.Add(Me.panPregunta)
        Me.Controls.Add(Me.rString)
        Me.Menu = Me.menuPreguntas
        Me.Name = "frmPreguntasDetalle"
        Me.Text = "Registro de Ruta"
        Me.panRespuesta.ResumeLayout(False)
        Me.panPregunta.ResumeLayout(False)
        Me.panTitulo.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblQuedan As System.Windows.Forms.Label
    Friend WithEvents txtPregunta As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents panRespuesta As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents panPregunta As System.Windows.Forms.Panel
    Friend WithEvents rString As System.Windows.Forms.TextBox
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents panTitulo As System.Windows.Forms.Panel
    Friend WithEvents rBool As System.Windows.Forms.ComboBox
    Friend WithEvents rFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents rSoft As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
End Class
