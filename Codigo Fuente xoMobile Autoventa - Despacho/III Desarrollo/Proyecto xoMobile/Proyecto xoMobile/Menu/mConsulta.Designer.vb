<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class mConsulta
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
    Private menuConsultas As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem
        Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem
        Dim ListViewItem5 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem
        Dim ListViewItem6 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem
        Dim ListViewItem7 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem
        Dim ListViewItem8 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mConsulta))
        Me.menuConsultas = New System.Windows.Forms.MainMenu
        Me.lSoft = New System.Windows.Forms.MenuItem
        Me.rSoft = New System.Windows.Forms.MenuItem
        Me.lstConsulta = New System.Windows.Forms.ListView
        Me.imagenesConsultas = New System.Windows.Forms.ImageList
        Me.SuspendLayout()
        '
        'menuConsultas
        '
        Me.menuConsultas.MenuItems.Add(Me.lSoft)
        Me.menuConsultas.MenuItems.Add(Me.rSoft)
        '
        'lSoft
        '
        Me.lSoft.Text = "Salir"
        '
        'rSoft
        '
        Me.rSoft.Text = "Seleccionar"
        '
        'lstConsulta
        '
        ListViewItem1.ImageIndex = 0
        ListViewItem1.Tag = "0"
        ListViewItem1.Text = "     Clientes con saldo"
        ListViewItem2.ImageIndex = 1
        ListViewItem2.Tag = "1"
        ListViewItem2.Text = "     Consulta de carga"
        ListViewItem3.ImageIndex = 2
        ListViewItem3.Tag = "2"
        ListViewItem3.Text = "     Documentos emitidos"
        ListViewItem4.ImageIndex = 3
        ListViewItem4.Tag = "3"
        ListViewItem4.Text = "     Inventario de producto"
        ListViewItem5.ImageIndex = 4
        ListViewItem5.Tag = "4"
        ListViewItem5.Text = "     Envase recibido"
        ListViewItem6.ImageIndex = 5
        ListViewItem6.Tag = "5"
        ListViewItem6.Text = "     Producto vendido"
        ListViewItem7.ImageIndex = 6
        ListViewItem7.Tag = "6"
        ListViewItem7.Text = "     Cheques recibidos"
        ListViewItem8.ImageIndex = 7
        ListViewItem8.Tag = "7"
        ListViewItem8.Text = "Presupuesto"
        Me.lstConsulta.Items.Add(ListViewItem1)
        Me.lstConsulta.Items.Add(ListViewItem2)
        Me.lstConsulta.Items.Add(ListViewItem3)
        Me.lstConsulta.Items.Add(ListViewItem4)
        Me.lstConsulta.Items.Add(ListViewItem5)
        Me.lstConsulta.Items.Add(ListViewItem6)
        Me.lstConsulta.Items.Add(ListViewItem7)
        Me.lstConsulta.Items.Add(ListViewItem8)
        Me.lstConsulta.Location = New System.Drawing.Point(1, 1)
        Me.lstConsulta.Name = "lstConsulta"
        Me.lstConsulta.Size = New System.Drawing.Size(239, 264)
        Me.lstConsulta.SmallImageList = Me.imagenesConsultas
        Me.lstConsulta.TabIndex = 0
        Me.lstConsulta.View = System.Windows.Forms.View.SmallIcon
        '
        'imagenesConsultas
        '
        Me.imagenesConsultas.ImageSize = New System.Drawing.Size(64, 64)
        Me.imagenesConsultas.Images.Clear()
        Me.imagenesConsultas.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.imagenesConsultas.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Image))
        Me.imagenesConsultas.Images.Add(CType(resources.GetObject("resource2"), System.Drawing.Image))
        Me.imagenesConsultas.Images.Add(CType(resources.GetObject("resource3"), System.Drawing.Image))
        Me.imagenesConsultas.Images.Add(CType(resources.GetObject("resource4"), System.Drawing.Image))
        Me.imagenesConsultas.Images.Add(CType(resources.GetObject("resource5"), System.Drawing.Image))
        Me.imagenesConsultas.Images.Add(CType(resources.GetObject("resource6"), System.Drawing.Image))
        Me.imagenesConsultas.Images.Add(CType(resources.GetObject("resource7"), System.Drawing.Image))
        '
        'mConsulta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.ControlBox = False
        Me.Controls.Add(Me.lstConsulta)
        Me.Menu = Me.menuConsultas
        Me.Name = "mConsulta"
        Me.Text = "Consultas"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstConsulta As System.Windows.Forms.ListView
    Friend WithEvents lSoft As System.Windows.Forms.MenuItem
    Friend WithEvents rSoft As System.Windows.Forms.MenuItem
    Friend WithEvents imagenesConsultas As System.Windows.Forms.ImageList
End Class
