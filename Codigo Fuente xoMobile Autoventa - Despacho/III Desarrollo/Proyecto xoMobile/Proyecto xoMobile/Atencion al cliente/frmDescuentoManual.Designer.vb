<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmDescuentoManual
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
    Private mainMenu1 As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.mainMenu1 = New System.Windows.Forms.MainMenu
        Me.panIngreso = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblMaximo = New System.Windows.Forms.Label
        Me.txtDescuento = New System.Windows.Forms.TextBox
        Me.lblProducto = New System.Windows.Forms.Label
        Me.lblArticulo = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.panIngreso.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'panIngreso
        '
        Me.panIngreso.BackColor = System.Drawing.SystemColors.MenuText
        Me.panIngreso.Controls.Add(Me.Panel1)
        Me.panIngreso.Controls.Add(Me.lblProducto)
        Me.panIngreso.Location = New System.Drawing.Point(18, 82)
        Me.panIngreso.Name = "panIngreso"
        Me.panIngreso.Size = New System.Drawing.Size(205, 94)
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel1.Controls.Add(Me.lblMaximo)
        Me.Panel1.Controls.Add(Me.txtDescuento)
        Me.Panel1.Location = New System.Drawing.Point(3, 28)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(199, 60)
        '
        'lblMaximo
        '
        Me.lblMaximo.Location = New System.Drawing.Point(3, 39)
        Me.lblMaximo.Name = "lblMaximo"
        Me.lblMaximo.Size = New System.Drawing.Size(193, 19)
        Me.lblMaximo.Text = "Hasta un 10%"
        Me.lblMaximo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtDescuento
        '
        Me.txtDescuento.Location = New System.Drawing.Point(3, 5)
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.Size = New System.Drawing.Size(193, 21)
        Me.txtDescuento.TabIndex = 3
        '
        'lblProducto
        '
        Me.lblProducto.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.lblProducto.ForeColor = System.Drawing.Color.White
        Me.lblProducto.Location = New System.Drawing.Point(0, 8)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(199, 22)
        Me.lblProducto.Text = "Porcentaje maximo manual"
        '
        'lblArticulo
        '
        Me.lblArticulo.BackColor = System.Drawing.Color.Transparent
        Me.lblArticulo.ForeColor = System.Drawing.SystemColors.MenuText
        Me.lblArticulo.Location = New System.Drawing.Point(2, 2)
        Me.lblArticulo.Name = "lblArticulo"
        Me.lblArticulo.Size = New System.Drawing.Size(238, 74)
        Me.lblArticulo.Text = "Nombre del item"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lblArticulo)
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(240, 76)
        '
        'frmDescuentoManual
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.panIngreso)
        Me.Menu = Me.mainMenu1
        Me.Name = "frmDescuentoManual"
        Me.Text = "Desc. Manual"
        Me.panIngreso.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panIngreso As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblMaximo As System.Windows.Forms.Label
    Friend WithEvents txtDescuento As System.Windows.Forms.TextBox
    Friend WithEvents lblProducto As System.Windows.Forms.Label
    Public WithEvents lblArticulo As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
End Class
