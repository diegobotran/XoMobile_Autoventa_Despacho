<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmDespacho
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDespacho))
        Me.mainMenu1 = New System.Windows.Forms.MainMenu
        Me.cmdConsulta = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.cmdAceptar = New System.Windows.Forms.MenuItem
        Me.lstDespachos = New System.Windows.Forms.ListView
        Me.ImageList = New System.Windows.Forms.ImageList
        Me.panAtencion = New System.Windows.Forms.Panel
        Me.lstRazones = New System.Windows.Forms.ComboBox
        Me.lblRazon = New System.Windows.Forms.Label
        Me.lstConsulta = New System.Windows.Forms.ListView
        Me.lblTotal = New System.Windows.Forms.Label
        Me.lblDesto = New System.Windows.Forms.Label
        Me.lblImportec = New System.Windows.Forms.Label
        Me.lblDestoc = New System.Windows.Forms.Label
        Me.lblNombreCliente = New System.Windows.Forms.Label
        Me.lblAtencion = New System.Windows.Forms.Label
        Me.picMenu = New System.Windows.Forms.PictureBox
        Me.panIconografia = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblLeyenda = New System.Windows.Forms.Label
        Me.lblAlerta = New System.Windows.Forms.Label
        Me.picAlerta = New System.Windows.Forms.PictureBox
        Me.lblDespachado = New System.Windows.Forms.Label
        Me.picDespachado = New System.Windows.Forms.PictureBox
        Me.lblPedido = New System.Windows.Forms.Label
        Me.picPedido = New System.Windows.Forms.PictureBox
        Me.panAtencion.SuspendLayout()
        Me.panIconografia.SuspendLayout()
        Me.SuspendLayout()
        '
        'mainMenu1
        '
        Me.mainMenu1.MenuItems.Add(Me.cmdConsulta)
        Me.mainMenu1.MenuItems.Add(Me.cmdAceptar)
        '
        'cmdConsulta
        '
        Me.cmdConsulta.MenuItems.Add(Me.MenuItem1)
        Me.cmdConsulta.MenuItems.Add(Me.MenuItem2)
        Me.cmdConsulta.Text = "Menu"
        '
        'MenuItem1
        '
        Me.MenuItem1.Text = "Salir"
        '
        'MenuItem2
        '
        Me.MenuItem2.Text = "Ver pedido"
        '
        'cmdAceptar
        '
        Me.cmdAceptar.Text = "Despachar"
        '
        'lstDespachos
        '
        Me.lstDespachos.CheckBoxes = True
        Me.lstDespachos.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lstDespachos.FullRowSelect = True
        Me.lstDespachos.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstDespachos.Location = New System.Drawing.Point(0, 89)
        Me.lstDespachos.Name = "lstDespachos"
        Me.lstDespachos.Size = New System.Drawing.Size(480, 447)
        Me.lstDespachos.SmallImageList = Me.ImageList
        Me.lstDespachos.TabIndex = 10
        Me.lstDespachos.View = System.Windows.Forms.View.Details
        '
        'ImageList
        '
        Me.ImageList.ImageSize = New System.Drawing.Size(32, 32)
        Me.ImageList.Images.Clear()
        Me.ImageList.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.ImageList.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Image))
        Me.ImageList.Images.Add(CType(resources.GetObject("resource2"), System.Drawing.Image))
        '
        'panAtencion
        '
        Me.panAtencion.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.panAtencion.Controls.Add(Me.lstRazones)
        Me.panAtencion.Controls.Add(Me.lblRazon)
        Me.panAtencion.Location = New System.Drawing.Point(12, 200)
        Me.panAtencion.Name = "panAtencion"
        Me.panAtencion.Size = New System.Drawing.Size(450, 162)
        Me.panAtencion.Visible = False
        '
        'lstRazones
        '
        Me.lstRazones.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.lstRazones.Location = New System.Drawing.Point(6, 101)
        Me.lstRazones.Name = "lstRazones"
        Me.lstRazones.Size = New System.Drawing.Size(438, 45)
        Me.lstRazones.TabIndex = 1
        '
        'lblRazon
        '
        Me.lblRazon.ForeColor = System.Drawing.Color.White
        Me.lblRazon.Location = New System.Drawing.Point(6, 8)
        Me.lblRazon.Name = "lblRazon"
        Me.lblRazon.Size = New System.Drawing.Size(438, 82)
        Me.lblRazon.Text = "Seleccione la Razon de no despacho y presione <ENTER>"
        Me.lblRazon.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lstConsulta
        '
        Me.lstConsulta.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lstConsulta.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstConsulta.Location = New System.Drawing.Point(0, 89)
        Me.lstConsulta.Name = "lstConsulta"
        Me.lstConsulta.Size = New System.Drawing.Size(480, 373)
        Me.lstConsulta.TabIndex = 11
        Me.lstConsulta.View = System.Windows.Forms.View.Details
        '
        'lblTotal
        '
        Me.lblTotal.Location = New System.Drawing.Point(6, 470)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(150, 30)
        Me.lblTotal.Text = "Importe:"
        '
        'lblDesto
        '
        Me.lblDesto.Location = New System.Drawing.Point(6, 502)
        Me.lblDesto.Name = "lblDesto"
        Me.lblDesto.Size = New System.Drawing.Size(150, 30)
        Me.lblDesto.Text = "Descuento:"
        '
        'lblImportec
        '
        Me.lblImportec.ForeColor = System.Drawing.Color.Maroon
        Me.lblImportec.Location = New System.Drawing.Point(168, 472)
        Me.lblImportec.Name = "lblImportec"
        Me.lblImportec.Size = New System.Drawing.Size(150, 30)
        Me.lblImportec.Text = "Q.00.00"
        '
        'lblDestoc
        '
        Me.lblDestoc.ForeColor = System.Drawing.Color.Maroon
        Me.lblDestoc.Location = New System.Drawing.Point(168, 502)
        Me.lblDestoc.Name = "lblDestoc"
        Me.lblDestoc.Size = New System.Drawing.Size(150, 30)
        Me.lblDestoc.Text = "Q.00.00"
        '
        'lblNombreCliente
        '
        Me.lblNombreCliente.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular)
        Me.lblNombreCliente.Location = New System.Drawing.Point(6, 0)
        Me.lblNombreCliente.Name = "lblNombreCliente"
        Me.lblNombreCliente.Size = New System.Drawing.Size(404, 40)
        Me.lblNombreCliente.Text = "lblNombreCliente"
        '
        'lblAtencion
        '
        Me.lblAtencion.BackColor = System.Drawing.Color.White
        Me.lblAtencion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblAtencion.ForeColor = System.Drawing.Color.Maroon
        Me.lblAtencion.Location = New System.Drawing.Point(12, 40)
        Me.lblAtencion.Name = "lblAtencion"
        Me.lblAtencion.Size = New System.Drawing.Size(408, 34)
        Me.lblAtencion.Text = "lblAtencion"
        '
        'picMenu
        '
        Me.picMenu.BackColor = System.Drawing.Color.White
        Me.picMenu.Image = CType(resources.GetObject("picMenu.Image"), System.Drawing.Image)
        Me.picMenu.Location = New System.Drawing.Point(448, 0)
        Me.picMenu.Name = "picMenu"
        Me.picMenu.Size = New System.Drawing.Size(32, 58)
        Me.picMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'panIconografia
        '
        Me.panIconografia.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panIconografia.Controls.Add(Me.Panel2)
        Me.panIconografia.Controls.Add(Me.lblLeyenda)
        Me.panIconografia.Controls.Add(Me.lblAlerta)
        Me.panIconografia.Controls.Add(Me.picAlerta)
        Me.panIconografia.Controls.Add(Me.lblDespachado)
        Me.panIconografia.Controls.Add(Me.picDespachado)
        Me.panIconografia.Controls.Add(Me.lblPedido)
        Me.panIconografia.Controls.Add(Me.picPedido)
        Me.panIconografia.Location = New System.Drawing.Point(51, 57)
        Me.panIconografia.Name = "panIconografia"
        Me.panIconografia.Size = New System.Drawing.Size(421, 187)
        Me.panIconografia.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.DarkRed
        Me.Panel2.Location = New System.Drawing.Point(27, 49)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(350, 1)
        '
        'lblLeyenda
        '
        Me.lblLeyenda.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblLeyenda.ForeColor = System.Drawing.Color.Black
        Me.lblLeyenda.Location = New System.Drawing.Point(13, 3)
        Me.lblLeyenda.Name = "lblLeyenda"
        Me.lblLeyenda.Size = New System.Drawing.Size(269, 40)
        Me.lblLeyenda.Text = "Significado de iconos"
        '
        'lblAlerta
        '
        Me.lblAlerta.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblAlerta.Location = New System.Drawing.Point(44, 146)
        Me.lblAlerta.Name = "lblAlerta"
        Me.lblAlerta.Size = New System.Drawing.Size(361, 40)
        Me.lblAlerta.Text = "El pedido es distinto al despacho"
        '
        'picAlerta
        '
        Me.picAlerta.Image = CType(resources.GetObject("picAlerta.Image"), System.Drawing.Image)
        Me.picAlerta.Location = New System.Drawing.Point(14, 148)
        Me.picAlerta.Name = "picAlerta"
        Me.picAlerta.Size = New System.Drawing.Size(24, 24)
        '
        'lblDespachado
        '
        Me.lblDespachado.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblDespachado.Location = New System.Drawing.Point(44, 106)
        Me.lblDespachado.Name = "lblDespachado"
        Me.lblDespachado.Size = New System.Drawing.Size(370, 40)
        Me.lblDespachado.Text = "Pedido despachado"
        '
        'picDespachado
        '
        Me.picDespachado.Image = CType(resources.GetObject("picDespachado.Image"), System.Drawing.Image)
        Me.picDespachado.Location = New System.Drawing.Point(14, 108)
        Me.picDespachado.Name = "picDespachado"
        Me.picDespachado.Size = New System.Drawing.Size(24, 24)
        '
        'lblPedido
        '
        Me.lblPedido.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblPedido.Location = New System.Drawing.Point(44, 66)
        Me.lblPedido.Name = "lblPedido"
        Me.lblPedido.Size = New System.Drawing.Size(361, 40)
        Me.lblPedido.Text = "Pedido pendiente de despacho"
        '
        'picPedido
        '
        Me.picPedido.Image = CType(resources.GetObject("picPedido.Image"), System.Drawing.Image)
        Me.picPedido.Location = New System.Drawing.Point(14, 68)
        Me.picPedido.Name = "picPedido"
        Me.picPedido.Size = New System.Drawing.Size(24, 24)
        '
        'frmDespacho
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(192.0!, 192.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(480, 536)
        Me.ControlBox = False
        Me.Controls.Add(Me.panIconografia)
        Me.Controls.Add(Me.picMenu)
        Me.Controls.Add(Me.lblAtencion)
        Me.Controls.Add(Me.lblNombreCliente)
        Me.Controls.Add(Me.panAtencion)
        Me.Controls.Add(Me.lstDespachos)
        Me.Controls.Add(Me.lblImportec)
        Me.Controls.Add(Me.lblDestoc)
        Me.Controls.Add(Me.lblDesto)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.lstConsulta)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(0, 52)
        Me.Menu = Me.mainMenu1
        Me.Name = "frmDespacho"
        Me.Text = "Pedidos"
        Me.panAtencion.ResumeLayout(False)
        Me.panIconografia.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstDespachos As System.Windows.Forms.ListView
    Friend WithEvents cmdConsulta As System.Windows.Forms.MenuItem
    Friend WithEvents cmdAceptar As System.Windows.Forms.MenuItem
    Friend WithEvents panAtencion As System.Windows.Forms.Panel
    Friend WithEvents lstRazones As System.Windows.Forms.ComboBox
    Friend WithEvents lblRazon As System.Windows.Forms.Label
    Friend WithEvents lstConsulta As System.Windows.Forms.ListView
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents lblDesto As System.Windows.Forms.Label
    Friend WithEvents lblImportec As System.Windows.Forms.Label
    Friend WithEvents lblDestoc As System.Windows.Forms.Label
    Friend WithEvents lblNombreCliente As System.Windows.Forms.Label
    Public WithEvents lblAtencion As System.Windows.Forms.Label
    Friend WithEvents ImageList As System.Windows.Forms.ImageList
    Friend WithEvents picMenu As System.Windows.Forms.PictureBox
    Friend WithEvents panIconografia As System.Windows.Forms.Panel
    Friend WithEvents lblAlerta As System.Windows.Forms.Label
    Friend WithEvents picAlerta As System.Windows.Forms.PictureBox
    Friend WithEvents lblDespachado As System.Windows.Forms.Label
    Friend WithEvents picDespachado As System.Windows.Forms.PictureBox
    Friend WithEvents lblPedido As System.Windows.Forms.Label
    Friend WithEvents picPedido As System.Windows.Forms.PictureBox
    Friend WithEvents lblLeyenda As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
End Class
