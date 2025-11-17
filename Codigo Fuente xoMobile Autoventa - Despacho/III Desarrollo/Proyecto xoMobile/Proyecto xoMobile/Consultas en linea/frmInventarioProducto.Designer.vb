<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmInventarioProducto
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
    Private menuInventarioProducto As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInventarioProducto))
        Me.menuInventarioProducto = New System.Windows.Forms.MainMenu
        Me.lSoft = New System.Windows.Forms.MenuItem
        Me.rSoft = New System.Windows.Forms.MenuItem
        Me.lblConsulta = New System.Windows.Forms.Label
        Me.lstCarga = New System.Windows.Forms.ListView
        Me.imagenesConsultas = New System.Windows.Forms.ImageList
        Me.SuspendLayout()
        '
        'menuInventarioProducto
        '
        Me.menuInventarioProducto.MenuItems.Add(Me.lSoft)
        Me.menuInventarioProducto.MenuItems.Add(Me.rSoft)
        '
        'lSoft
        '
        Me.lSoft.Text = "Regresar"
        '
        'rSoft
        '
        Me.rSoft.Text = "Imprimir"
        '
        'lblConsulta
        '
        Me.lblConsulta.Font = New System.Drawing.Font("Trebuchet MS", 13.0!, System.Drawing.FontStyle.Regular)
        Me.lblConsulta.Location = New System.Drawing.Point(12, 7)
        Me.lblConsulta.Name = "lblConsulta"
        Me.lblConsulta.Size = New System.Drawing.Size(164, 47)
        Me.lblConsulta.Text = "Titulo de la consulta"
        '
        'lstCarga
        '
        Me.lstCarga.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lstCarga.FullRowSelect = True
        Me.lstCarga.Location = New System.Drawing.Point(0, 57)
        Me.lstCarga.Name = "lstCarga"
        Me.lstCarga.Size = New System.Drawing.Size(240, 208)
        Me.lstCarga.TabIndex = 7
        Me.lstCarga.View = System.Windows.Forms.View.Details
        '
        'imagenesConsultas
        '
        Me.imagenesConsultas.ImageSize = New System.Drawing.Size(24, 24)
        Me.imagenesConsultas.Images.Clear()
        Me.imagenesConsultas.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.imagenesConsultas.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Image))
        Me.imagenesConsultas.Images.Add(CType(resources.GetObject("resource2"), System.Drawing.Image))
        '
        'frmInventarioProducto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.ControlBox = False
        Me.Controls.Add(Me.lstCarga)
        Me.Controls.Add(Me.lblConsulta)
        Me.Menu = Me.menuInventarioProducto
        Me.Name = "frmInventarioProducto"
        Me.Text = "Inventario"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lSoft As System.Windows.Forms.MenuItem
    Friend WithEvents rSoft As System.Windows.Forms.MenuItem
    Friend WithEvents lblConsulta As System.Windows.Forms.Label
    Friend WithEvents lstCarga As System.Windows.Forms.ListView
    Friend WithEvents imagenesConsultas As System.Windows.Forms.ImageList
End Class
