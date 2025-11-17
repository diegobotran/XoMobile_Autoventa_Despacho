<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmSystat
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
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.lblGenerales = New System.Windows.Forms.Label
        Me.lstSS = New System.Windows.Forms.ListView
        Me.lblFecha = New System.Windows.Forms.Label
        Me.lnkClientes = New System.Windows.Forms.LinkLabel
        Me.lnkCarga = New System.Windows.Forms.LinkLabel
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.SuspendLayout()
        '
        'mainMenu1
        '
        Me.mainMenu1.MenuItems.Add(Me.MenuItem1)
        '
        'MenuItem1
        '
        Me.MenuItem1.Text = "Salir"
        '
        'lblGenerales
        '
        Me.lblGenerales.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.lblGenerales.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblGenerales.ForeColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(89, Byte), Integer), CType(CType(152, Byte), Integer))
        Me.lblGenerales.Location = New System.Drawing.Point(0, 0)
        Me.lblGenerales.Name = "lblGenerales"
        Me.lblGenerales.Size = New System.Drawing.Size(112, 18)
        Me.lblGenerales.Text = "Hoy es:"
        '
        'lstSS
        '
        Me.lstSS.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstSS.Location = New System.Drawing.Point(0, 20)
        Me.lstSS.Name = "lstSS"
        Me.lstSS.Size = New System.Drawing.Size(240, 246)
        Me.lstSS.TabIndex = 2
        Me.lstSS.View = System.Windows.Forms.View.Details
        '
        'lblFecha
        '
        Me.lblFecha.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFecha.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.lblFecha.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblFecha.Location = New System.Drawing.Point(109, 0)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(128, 20)
        Me.lblFecha.Text = "lblFecha"
        Me.lblFecha.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lnkClientes
        '
        Me.lnkClientes.Location = New System.Drawing.Point(0, 269)
        Me.lnkClientes.Name = "lnkClientes"
        Me.lnkClientes.Size = New System.Drawing.Size(131, 20)
        Me.lnkClientes.TabIndex = 6
        Me.lnkClientes.Text = "Clientes con saldo"
        '
        'lnkCarga
        '
        Me.lnkCarga.Location = New System.Drawing.Point(109, 269)
        Me.lnkCarga.Name = "lnkCarga"
        Me.lnkCarga.Size = New System.Drawing.Size(82, 20)
        Me.lnkCarga.TabIndex = 7
        Me.lnkCarga.Text = "Carga Basica"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Location = New System.Drawing.Point(188, 269)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(52, 20)
        Me.LinkLabel1.TabIndex = 10
        Me.LinkLabel1.Text = "Cerrar"
        '
        'frmSystat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.lnkCarga)
        Me.Controls.Add(Me.lnkClientes)
        Me.Controls.Add(Me.lstSS)
        Me.Controls.Add(Me.lblGenerales)
        Me.Controls.Add(Me.lblFecha)
        Me.KeyPreview = True
        Me.Name = "frmSystat"
        Me.Text = "xoMobile"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblGenerales As System.Windows.Forms.Label
    Friend WithEvents lstSS As System.Windows.Forms.ListView
    Friend WithEvents lblFecha As System.Windows.Forms.Label
    Friend WithEvents lnkClientes As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkCarga As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
End Class
