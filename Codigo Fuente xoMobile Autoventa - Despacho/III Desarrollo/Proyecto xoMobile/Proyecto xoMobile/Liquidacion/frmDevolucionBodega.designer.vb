<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmDevolucionBodega
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

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lSoft = New System.Windows.Forms.MenuItem
        Me.menuDevolucionBodega = New System.Windows.Forms.MainMenu
        Me.rSoft = New System.Windows.Forms.MenuItem
        Me.dgDevolucion = New System.Windows.Forms.DataGrid
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle
        Me.panIngreso = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtUnidades = New System.Windows.Forms.TextBox
        Me.lblUnidadesCaja = New System.Windows.Forms.Label
        Me.lblM3 = New System.Windows.Forms.Label
        Me.txtCaja = New System.Windows.Forms.TextBox
        Me.lblFecha = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.DataGridTableStyle2 = New System.Windows.Forms.DataGridTableStyle
        Me.dgFrozen = New System.Windows.Forms.DataGrid
        Me.panIngreso.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lSoft
        '
        Me.lSoft.Text = "Cancelar"
        '
        'menuDevolucionBodega
        '
        Me.menuDevolucionBodega.MenuItems.Add(Me.lSoft)
        Me.menuDevolucionBodega.MenuItems.Add(Me.rSoft)
        '
        'rSoft
        '
        Me.rSoft.Text = "Confirmar"
        '
        'dgDevolucion
        '
        Me.dgDevolucion.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgDevolucion.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dgDevolucion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.dgDevolucion.Location = New System.Drawing.Point(0, 26)
        Me.dgDevolucion.Name = "dgDevolucion"
        Me.dgDevolucion.PreferredRowHeight = 70
        Me.dgDevolucion.RowHeadersVisible = False
        Me.dgDevolucion.Size = New System.Drawing.Size(480, 510)
        Me.dgDevolucion.TabIndex = 3
        Me.dgDevolucion.TableStyles.Add(Me.DataGridTableStyle1)
        '
        'panIngreso
        '
        Me.panIngreso.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.panIngreso.Controls.Add(Me.Panel1)
        Me.panIngreso.Location = New System.Drawing.Point(72, 156)
        Me.panIngreso.Name = "panIngreso"
        Me.panIngreso.Size = New System.Drawing.Size(360, 124)
        Me.panIngreso.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtUnidades)
        Me.Panel1.Controls.Add(Me.lblUnidadesCaja)
        Me.Panel1.Controls.Add(Me.lblM3)
        Me.Panel1.Controls.Add(Me.txtCaja)
        Me.Panel1.Location = New System.Drawing.Point(10, 10)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(336, 102)
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.Label1.Location = New System.Drawing.Point(174, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 40)
        Me.Label1.Text = "Un:"
        '
        'txtUnidades
        '
        Me.txtUnidades.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.txtUnidades.Location = New System.Drawing.Point(240, 12)
        Me.txtUnidades.Name = "txtUnidades"
        Me.txtUnidades.Size = New System.Drawing.Size(80, 45)
        Me.txtUnidades.TabIndex = 27
        '
        'lblUnidadesCaja
        '
        Me.lblUnidadesCaja.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblUnidadesCaja.Location = New System.Drawing.Point(72, 64)
        Me.lblUnidadesCaja.Name = "lblUnidadesCaja"
        Me.lblUnidadesCaja.Size = New System.Drawing.Size(236, 32)
        Me.lblUnidadesCaja.Text = "---"
        '
        'lblM3
        '
        Me.lblM3.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.lblM3.Location = New System.Drawing.Point(6, 18)
        Me.lblM3.Name = "lblM3"
        Me.lblM3.Size = New System.Drawing.Size(54, 40)
        Me.lblM3.Text = "Cj:"
        '
        'txtCaja
        '
        Me.txtCaja.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.txtCaja.Location = New System.Drawing.Point(72, 12)
        Me.txtCaja.Name = "txtCaja"
        Me.txtCaja.Size = New System.Drawing.Size(80, 45)
        Me.txtCaja.TabIndex = 26
        '
        'lblFecha
        '
        Me.lblFecha.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFecha.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.lblFecha.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblFecha.Location = New System.Drawing.Point(0, 0)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(720, 40)
        Me.lblFecha.Text = "lblFecha"
        Me.lblFecha.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(40, 446)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(144, 40)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Button1"
        '
        'dgFrozen
        '
        Me.dgFrozen.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgFrozen.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dgFrozen.Enabled = False
        Me.dgFrozen.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.dgFrozen.Location = New System.Drawing.Point(190, 492)
        Me.dgFrozen.Name = "dgFrozen"
        Me.dgFrozen.PreferredRowHeight = 25
        Me.dgFrozen.Size = New System.Drawing.Size(290, 312)
        Me.dgFrozen.TabIndex = 4
        Me.dgFrozen.TableStyles.Add(Me.DataGridTableStyle2)
        Me.dgFrozen.Visible = False
        '
        'frmDevolucionBodega
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(192.0!, 192.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(480, 536)
        Me.ControlBox = False
        Me.Controls.Add(Me.panIngreso)
        Me.Controls.Add(Me.dgDevolucion)
        Me.Controls.Add(Me.dgFrozen)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.lblFecha)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(0, 52)
        Me.Menu = Me.menuDevolucionBodega
        Me.Name = "frmDevolucionBodega"
        Me.panIngreso.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lSoft As System.Windows.Forms.MenuItem
    Private WithEvents menuDevolucionBodega As System.Windows.Forms.MainMenu
    Friend WithEvents rSoft As System.Windows.Forms.MenuItem
    Friend WithEvents dgDevolucion As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents panIngreso As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtCaja As System.Windows.Forms.TextBox
    Friend WithEvents lblM3 As System.Windows.Forms.Label
    Friend WithEvents lblUnidadesCaja As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtUnidades As System.Windows.Forms.TextBox
    Friend WithEvents lblFecha As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DataGridTableStyle2 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents dgFrozen As System.Windows.Forms.DataGrid

End Class
