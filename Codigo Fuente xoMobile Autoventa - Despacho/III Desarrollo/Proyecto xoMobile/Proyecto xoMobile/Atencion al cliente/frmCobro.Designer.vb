<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmCobro
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
    Private menuCobro As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.menuCobro = New System.Windows.Forms.MainMenu
        Me.softCancelar = New System.Windows.Forms.MenuItem
        Me.softPagar = New System.Windows.Forms.MenuItem
        Me.lstDocumentos = New System.Windows.Forms.ListView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtCobro = New System.Windows.Forms.TextBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtMaxEnvase = New System.Windows.Forms.TextBox
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuCobro
        '
        Me.menuCobro.MenuItems.Add(Me.softCancelar)
        Me.menuCobro.MenuItems.Add(Me.softPagar)
        '
        'softCancelar
        '
        Me.softCancelar.Text = "Salir"
        '
        'softPagar
        '
        Me.softPagar.Text = "Pagar"
        '
        'lstDocumentos
        '
        Me.lstDocumentos.CheckBoxes = True
        Me.lstDocumentos.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lstDocumentos.FullRowSelect = True
        Me.lstDocumentos.Location = New System.Drawing.Point(0, 0)
        Me.lstDocumentos.Name = "lstDocumentos"
        Me.lstDocumentos.Size = New System.Drawing.Size(240, 227)
        Me.lstDocumentos.TabIndex = 1
        Me.lstDocumentos.View = System.Windows.Forms.View.Details
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.MenuText
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtCobro)
        Me.Panel1.Location = New System.Drawing.Point(4, 229)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(106, 36)
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.Label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label8.Location = New System.Drawing.Point(3, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(100, 10)
        Me.Label8.Text = "Total a pagar"
        '
        'txtCobro
        '
        Me.txtCobro.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCobro.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtCobro.ForeColor = System.Drawing.Color.Maroon
        Me.txtCobro.Location = New System.Drawing.Point(3, 15)
        Me.txtCobro.Multiline = True
        Me.txtCobro.Name = "txtCobro"
        Me.txtCobro.ReadOnly = True
        Me.txtCobro.Size = New System.Drawing.Size(100, 19)
        Me.txtCobro.TabIndex = 18
        Me.txtCobro.Text = "0.00"
        Me.txtCobro.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.MenuText
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.txtMaxEnvase)
        Me.Panel2.Location = New System.Drawing.Point(131, 229)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(106, 36)
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Location = New System.Drawing.Point(3, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 10)
        Me.Label1.Text = "Max Envase"
        '
        'txtMaxEnvase
        '
        Me.txtMaxEnvase.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtMaxEnvase.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtMaxEnvase.ForeColor = System.Drawing.Color.Maroon
        Me.txtMaxEnvase.Location = New System.Drawing.Point(3, 15)
        Me.txtMaxEnvase.Multiline = True
        Me.txtMaxEnvase.Name = "txtMaxEnvase"
        Me.txtMaxEnvase.ReadOnly = True
        Me.txtMaxEnvase.Size = New System.Drawing.Size(100, 19)
        Me.txtMaxEnvase.TabIndex = 18
        Me.txtMaxEnvase.Text = "0.00"
        Me.txtMaxEnvase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'frmCobro
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lstDocumentos)
        Me.KeyPreview = True
        Me.Menu = Me.menuCobro
        Me.Name = "frmCobro"
        Me.Text = "Documentos por cobrar"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstDocumentos As System.Windows.Forms.ListView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtCobro As System.Windows.Forms.TextBox
    Friend WithEvents softCancelar As System.Windows.Forms.MenuItem
    Friend WithEvents softPagar As System.Windows.Forms.MenuItem
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtMaxEnvase As System.Windows.Forms.TextBox
End Class
