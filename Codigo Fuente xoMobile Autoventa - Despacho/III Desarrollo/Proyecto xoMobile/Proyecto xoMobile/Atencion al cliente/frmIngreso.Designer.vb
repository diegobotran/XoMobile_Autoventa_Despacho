<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmIngreso
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
    Private menuIngreso As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIngreso))
        Me.menuIngreso = New System.Windows.Forms.MainMenu
        Me.softLeft = New System.Windows.Forms.MenuItem
        Me.lSoft1 = New System.Windows.Forms.MenuItem
        Me.lSoftCancelar = New System.Windows.Forms.MenuItem
        Me.lSoftConsulta = New System.Windows.Forms.MenuItem
        Me.lIngreso = New System.Windows.Forms.MenuItem
        Me.cmdSiguiente = New System.Windows.Forms.MenuItem
        Me.lblProducto = New System.Windows.Forms.Label
        Me.panIngresoC = New System.Windows.Forms.Panel
        Me.PanR3 = New System.Windows.Forms.Panel
        Me.txtuCajaVacia = New System.Windows.Forms.TextBox
        Me.lblums3 = New System.Windows.Forms.Label
        Me.lblM3 = New System.Windows.Forms.Label
        Me.panR1 = New System.Windows.Forms.Panel
        Me.txtcLiquido = New System.Windows.Forms.TextBox
        Me.txtuLiquido = New System.Windows.Forms.TextBox
        Me.lblump1 = New System.Windows.Forms.Label
        Me.lblM1 = New System.Windows.Forms.Label
        Me.lblums1 = New System.Windows.Forms.Label
        Me.panR2 = New System.Windows.Forms.Panel
        Me.txtuEnvase = New System.Windows.Forms.TextBox
        Me.lblump2 = New System.Windows.Forms.Label
        Me.lblums2 = New System.Windows.Forms.Label
        Me.txtcEnvase = New System.Windows.Forms.TextBox
        Me.lblM2 = New System.Windows.Forms.Label
        Me.panIngreso = New System.Windows.Forms.Panel
        Me.cmdEliminar = New System.Windows.Forms.PictureBox
        Me.lstProductos = New System.Windows.Forms.ListView
        Me.imgBullets = New System.Windows.Forms.ImageList
        Me.lstAgregados = New System.Windows.Forms.ListView
        Me.lblImporte = New System.Windows.Forms.Label
        Me.lblTextImporte = New System.Windows.Forms.Label
        Me.txtBuscar = New System.Windows.Forms.TextBox
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.panImporte = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.ltsAcumulados = New System.Windows.Forms.Label
        Me.picMenu = New System.Windows.Forms.PictureBox
        Me.lblMensajeImporte = New System.Windows.Forms.Label
        Me.lblAtencion = New System.Windows.Forms.Label
        Me.panBusca = New System.Windows.Forms.Panel
        Me.progBusqueda = New System.Windows.Forms.ProgressBar
        Me.lblNombreCliente = New System.Windows.Forms.Label
        Me.picCerrar = New System.Windows.Forms.PictureBox
        Me.picBusca = New System.Windows.Forms.PictureBox
        Me.pboxAlert = New System.Windows.Forms.PictureBox
        Me.imgIcons = New System.Windows.Forms.ImageList
        Me.InputPanel1 = New Microsoft.WindowsCE.Forms.InputPanel(Me.components)
        Me.lnkMas = New System.Windows.Forms.LinkLabel
        Me.labeln = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblVariacionImporte = New System.Windows.Forms.Label
        Me.lblVariacionDesto = New System.Windows.Forms.Label
        Me.lblDescuentoPedido = New System.Windows.Forms.Label
        Me.label = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblImportePedido = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblImporteDespacho = New System.Windows.Forms.Label
        Me.lblDescuentoDespacho = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.lblLeyendaColumnas = New System.Windows.Forms.Label
        Me.panAdicionales = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.txtEnvaceA = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.panIngresoC.SuspendLayout()
        Me.PanR3.SuspendLayout()
        Me.panR1.SuspendLayout()
        Me.panR2.SuspendLayout()
        Me.panIngreso.SuspendLayout()
        Me.panImporte.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.panBusca.SuspendLayout()
        Me.panAdicionales.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuIngreso
        '
        Me.menuIngreso.MenuItems.Add(Me.softLeft)
        Me.menuIngreso.MenuItems.Add(Me.cmdSiguiente)
        '
        'softLeft
        '
        Me.softLeft.MenuItems.Add(Me.lSoft1)
        Me.softLeft.MenuItems.Add(Me.lSoftCancelar)
        Me.softLeft.MenuItems.Add(Me.lSoftConsulta)
        Me.softLeft.MenuItems.Add(Me.lIngreso)
        Me.softLeft.Text = "Menu"
        '
        'lSoft1
        '
        Me.lSoft1.Text = "Atras"
        '
        'lSoftCancelar
        '
        Me.lSoftCancelar.Text = "Cancelar"
        '
        'lSoftConsulta
        '
        Me.lSoftConsulta.Text = "Consulta"
        '
        'lIngreso
        '
        Me.lIngreso.Text = "Ingreso"
        '
        'cmdSiguiente
        '
        Me.cmdSiguiente.Text = "Siguiente"
        '
        'lblProducto
        '
        Me.lblProducto.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblProducto.ForeColor = System.Drawing.Color.White
        Me.lblProducto.Location = New System.Drawing.Point(5, 7)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(328, 63)
        Me.lblProducto.Text = "Ingreso de cantidades"
        '
        'panIngresoC
        '
        Me.panIngresoC.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panIngresoC.Controls.Add(Me.PanR3)
        Me.panIngresoC.Controls.Add(Me.panR1)
        Me.panIngresoC.Controls.Add(Me.panR2)
        Me.panIngresoC.Location = New System.Drawing.Point(5, 73)
        Me.panIngresoC.Name = "panIngresoC"
        Me.panIngresoC.Size = New System.Drawing.Size(398, 144)
        '
        'PanR3
        '
        Me.PanR3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.PanR3.Controls.Add(Me.txtuCajaVacia)
        Me.PanR3.Controls.Add(Me.lblums3)
        Me.PanR3.Controls.Add(Me.lblM3)
        Me.PanR3.Location = New System.Drawing.Point(6, 94)
        Me.PanR3.Name = "PanR3"
        Me.PanR3.Size = New System.Drawing.Size(386, 40)
        '
        'txtuCajaVacia
        '
        Me.txtuCajaVacia.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtuCajaVacia.Location = New System.Drawing.Point(218, 4)
        Me.txtuCajaVacia.Name = "txtuCajaVacia"
        Me.txtuCajaVacia.Size = New System.Drawing.Size(100, 35)
        Me.txtuCajaVacia.TabIndex = 26
        '
        'lblums3
        '
        Me.lblums3.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblums3.Location = New System.Drawing.Point(322, 8)
        Me.lblums3.Name = "lblums3"
        Me.lblums3.Size = New System.Drawing.Size(42, 26)
        Me.lblums3.Text = "un"
        '
        'lblM3
        '
        Me.lblM3.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblM3.Location = New System.Drawing.Point(6, 8)
        Me.lblM3.Name = "lblM3"
        Me.lblM3.Size = New System.Drawing.Size(48, 26)
        Me.lblM3.Text = "C"
        '
        'panR1
        '
        Me.panR1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panR1.Controls.Add(Me.txtcLiquido)
        Me.panR1.Controls.Add(Me.txtuLiquido)
        Me.panR1.Controls.Add(Me.lblump1)
        Me.panR1.Controls.Add(Me.lblM1)
        Me.panR1.Controls.Add(Me.lblums1)
        Me.panR1.Location = New System.Drawing.Point(6, 6)
        Me.panR1.Name = "panR1"
        Me.panR1.Size = New System.Drawing.Size(386, 40)
        '
        'txtcLiquido
        '
        Me.txtcLiquido.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtcLiquido.Location = New System.Drawing.Point(64, 4)
        Me.txtcLiquido.Name = "txtcLiquido"
        Me.txtcLiquido.Size = New System.Drawing.Size(100, 35)
        Me.txtcLiquido.TabIndex = 3
        '
        'txtuLiquido
        '
        Me.txtuLiquido.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtuLiquido.Location = New System.Drawing.Point(218, 4)
        Me.txtuLiquido.Name = "txtuLiquido"
        Me.txtuLiquido.Size = New System.Drawing.Size(100, 35)
        Me.txtuLiquido.TabIndex = 24
        '
        'lblump1
        '
        Me.lblump1.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblump1.Location = New System.Drawing.Point(164, 6)
        Me.lblump1.Name = "lblump1"
        Me.lblump1.Size = New System.Drawing.Size(44, 32)
        Me.lblump1.Text = "ca"
        '
        'lblM1
        '
        Me.lblM1.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblM1.Location = New System.Drawing.Point(6, 6)
        Me.lblM1.Name = "lblM1"
        Me.lblM1.Size = New System.Drawing.Size(46, 32)
        Me.lblM1.Text = "L"
        '
        'lblums1
        '
        Me.lblums1.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblums1.Location = New System.Drawing.Point(322, 8)
        Me.lblums1.Name = "lblums1"
        Me.lblums1.Size = New System.Drawing.Size(42, 30)
        Me.lblums1.Text = "un"
        '
        'panR2
        '
        Me.panR2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panR2.Controls.Add(Me.txtuEnvase)
        Me.panR2.Controls.Add(Me.lblump2)
        Me.panR2.Controls.Add(Me.lblums2)
        Me.panR2.Controls.Add(Me.txtcEnvase)
        Me.panR2.Controls.Add(Me.lblM2)
        Me.panR2.Location = New System.Drawing.Point(6, 50)
        Me.panR2.Name = "panR2"
        Me.panR2.Size = New System.Drawing.Size(386, 40)
        Me.panR2.Visible = False
        '
        'txtuEnvase
        '
        Me.txtuEnvase.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtuEnvase.Location = New System.Drawing.Point(218, 4)
        Me.txtuEnvase.Name = "txtuEnvase"
        Me.txtuEnvase.Size = New System.Drawing.Size(100, 35)
        Me.txtuEnvase.TabIndex = 25
        '
        'lblump2
        '
        Me.lblump2.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblump2.Location = New System.Drawing.Point(164, 6)
        Me.lblump2.Name = "lblump2"
        Me.lblump2.Size = New System.Drawing.Size(44, 30)
        Me.lblump2.Text = "ca"
        '
        'lblums2
        '
        Me.lblums2.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblums2.Location = New System.Drawing.Point(322, 8)
        Me.lblums2.Name = "lblums2"
        Me.lblums2.Size = New System.Drawing.Size(42, 28)
        Me.lblums2.Text = "un"
        '
        'txtcEnvase
        '
        Me.txtcEnvase.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtcEnvase.Location = New System.Drawing.Point(64, 4)
        Me.txtcEnvase.Name = "txtcEnvase"
        Me.txtcEnvase.Size = New System.Drawing.Size(100, 35)
        Me.txtcEnvase.TabIndex = 4
        '
        'lblM2
        '
        Me.lblM2.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblM2.Location = New System.Drawing.Point(6, 8)
        Me.lblM2.Name = "lblM2"
        Me.lblM2.Size = New System.Drawing.Size(52, 28)
        Me.lblM2.Text = "E"
        '
        'panIngreso
        '
        Me.panIngreso.BackColor = System.Drawing.SystemColors.MenuText
        Me.panIngreso.Controls.Add(Me.cmdEliminar)
        Me.panIngreso.Controls.Add(Me.panIngresoC)
        Me.panIngreso.Controls.Add(Me.lblProducto)
        Me.panIngreso.Location = New System.Drawing.Point(32, 158)
        Me.panIngreso.Name = "panIngreso"
        Me.panIngreso.Size = New System.Drawing.Size(410, 225)
        Me.panIngreso.Visible = False
        '
        'cmdEliminar
        '
        Me.cmdEliminar.BackColor = System.Drawing.Color.White
        Me.cmdEliminar.Image = CType(resources.GetObject("cmdEliminar.Image"), System.Drawing.Image)
        Me.cmdEliminar.Location = New System.Drawing.Point(339, 6)
        Me.cmdEliminar.Name = "cmdEliminar"
        Me.cmdEliminar.Size = New System.Drawing.Size(64, 64)
        '
        'lstProductos
        '
        Me.lstProductos.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lstProductos.FullRowSelect = True
        Me.lstProductos.Location = New System.Drawing.Point(0, 94)
        Me.lstProductos.Name = "lstProductos"
        Me.lstProductos.Size = New System.Drawing.Size(478, 372)
        Me.lstProductos.SmallImageList = Me.imgBullets
        Me.lstProductos.TabIndex = 7
        Me.lstProductos.View = System.Windows.Forms.View.Details
        Me.imgBullets.Images.Clear()
        Me.imgBullets.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.imgBullets.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Image))
        Me.imgBullets.Images.Add(CType(resources.GetObject("resource2"), System.Drawing.Image))
        '
        'lstAgregados
        '
        Me.lstAgregados.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lstAgregados.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstAgregados.Location = New System.Drawing.Point(0, 94)
        Me.lstAgregados.Name = "lstAgregados"
        Me.lstAgregados.Size = New System.Drawing.Size(480, 372)
        Me.lstAgregados.SmallImageList = Me.imgBullets
        Me.lstAgregados.TabIndex = 9
        Me.lstAgregados.View = System.Windows.Forms.View.Details
        Me.lstAgregados.Visible = False
        '
        'lblImporte
        '
        Me.lblImporte.BackColor = System.Drawing.Color.White
        Me.lblImporte.ForeColor = System.Drawing.Color.Maroon
        Me.lblImporte.Location = New System.Drawing.Point(4, 30)
        Me.lblImporte.Name = "lblImporte"
        Me.lblImporte.Size = New System.Drawing.Size(150, 32)
        Me.lblImporte.Text = "Q.0.00"
        Me.lblImporte.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTextImporte
        '
        Me.lblTextImporte.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblTextImporte.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblTextImporte.Location = New System.Drawing.Point(0, 0)
        Me.lblTextImporte.Name = "lblTextImporte"
        Me.lblTextImporte.Size = New System.Drawing.Size(150, 32)
        Me.lblTextImporte.Text = "Importe:"
        '
        'txtBuscar
        '
        Me.txtBuscar.Location = New System.Drawing.Point(3, 5)
        Me.txtBuscar.Multiline = True
        Me.txtBuscar.Name = "txtBuscar"
        Me.txtBuscar.Size = New System.Drawing.Size(342, 35)
        Me.txtBuscar.TabIndex = 16
        '
        'ComboBox1
        '
        Me.ComboBox1.Location = New System.Drawing.Point(3, 5)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(371, 41)
        Me.ComboBox1.TabIndex = 21
        '
        'panImporte
        '
        Me.panImporte.BackColor = System.Drawing.SystemColors.MenuText
        Me.panImporte.Controls.Add(Me.lblImporte)
        Me.panImporte.Controls.Add(Me.lblTextImporte)
        Me.panImporte.Location = New System.Drawing.Point(0, 0)
        Me.panImporte.Name = "panImporte"
        Me.panImporte.Size = New System.Drawing.Size(162, 70)
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.ltsAcumulados)
        Me.Panel2.Controls.Add(Me.panImporte)
        Me.Panel2.Location = New System.Drawing.Point(0, 466)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(480, 70)
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label2.Location = New System.Drawing.Point(419, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 32)
        Me.Label2.Text = "Lts."
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ltsAcumulados
        '
        Me.ltsAcumulados.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.ltsAcumulados.ForeColor = System.Drawing.Color.Maroon
        Me.ltsAcumulados.Location = New System.Drawing.Point(289, 30)
        Me.ltsAcumulados.Name = "ltsAcumulados"
        Me.ltsAcumulados.Size = New System.Drawing.Size(124, 32)
        Me.ltsAcumulados.Text = "0.000"
        Me.ltsAcumulados.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        'lblMensajeImporte
        '
        Me.lblMensajeImporte.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblMensajeImporte.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMensajeImporte.Location = New System.Drawing.Point(3, 61)
        Me.lblMensajeImporte.Name = "lblMensajeImporte"
        Me.lblMensajeImporte.Size = New System.Drawing.Size(254, 33)
        Me.lblMensajeImporte.Text = "El importe cambió"
        Me.lblMensajeImporte.Visible = False
        '
        'lblAtencion
        '
        Me.lblAtencion.BackColor = System.Drawing.Color.White
        Me.lblAtencion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblAtencion.ForeColor = System.Drawing.Color.Maroon
        Me.lblAtencion.Location = New System.Drawing.Point(0, 37)
        Me.lblAtencion.Name = "lblAtencion"
        Me.lblAtencion.Size = New System.Drawing.Size(384, 34)
        Me.lblAtencion.Text = "lblAtencion"
        '
        'panBusca
        '
        Me.panBusca.Controls.Add(Me.progBusqueda)
        Me.panBusca.Controls.Add(Me.txtBuscar)
        Me.panBusca.Controls.Add(Me.ComboBox1)
        Me.panBusca.Location = New System.Drawing.Point(0, 0)
        Me.panBusca.Name = "panBusca"
        Me.panBusca.Size = New System.Drawing.Size(480, 66)
        Me.panBusca.Visible = False
        '
        'progBusqueda
        '
        Me.progBusqueda.Location = New System.Drawing.Point(0, 55)
        Me.progBusqueda.Name = "progBusqueda"
        Me.progBusqueda.Size = New System.Drawing.Size(374, 10)
        Me.progBusqueda.Visible = False
        '
        'lblNombreCliente
        '
        Me.lblNombreCliente.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular)
        Me.lblNombreCliente.Location = New System.Drawing.Point(0, 0)
        Me.lblNombreCliente.Name = "lblNombreCliente"
        Me.lblNombreCliente.Size = New System.Drawing.Size(384, 40)
        Me.lblNombreCliente.Text = "lblNombreCliente"
        '
        'picCerrar
        '
        Me.picCerrar.Image = CType(resources.GetObject("picCerrar.Image"), System.Drawing.Image)
        Me.picCerrar.Location = New System.Drawing.Point(390, 0)
        Me.picCerrar.Name = "picCerrar"
        Me.picCerrar.Size = New System.Drawing.Size(56, 56)
        Me.picCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picCerrar.Visible = False
        '
        'picBusca
        '
        Me.picBusca.Image = CType(resources.GetObject("picBusca.Image"), System.Drawing.Image)
        Me.picBusca.Location = New System.Drawing.Point(390, 2)
        Me.picBusca.Name = "picBusca"
        Me.picBusca.Size = New System.Drawing.Size(64, 64)
        Me.picBusca.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        '
        'pboxAlert
        '
        Me.pboxAlert.Image = CType(resources.GetObject("pboxAlert.Image"), System.Drawing.Image)
        Me.pboxAlert.Location = New System.Drawing.Point(188, 54)
        Me.pboxAlert.Name = "pboxAlert"
        Me.pboxAlert.Size = New System.Drawing.Size(32, 32)
        Me.pboxAlert.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pboxAlert.Visible = False
        '
        'imgIcons
        '
        Me.imgIcons.ImageSize = New System.Drawing.Size(48, 48)
        Me.imgIcons.Images.Clear()
        Me.imgIcons.Images.Add(CType(resources.GetObject("resource3"), System.Drawing.Image))
        '
        'lnkMas
        '
        Me.lnkMas.Font = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Underline)
        Me.lnkMas.Location = New System.Drawing.Point(284, 62)
        Me.lnkMas.Name = "lnkMas"
        Me.lnkMas.Size = New System.Drawing.Size(162, 24)
        Me.lnkMas.TabIndex = 11
        Me.lnkMas.Text = "Mas columnas"
        Me.lnkMas.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lnkMas.Visible = False
        '
        'labeln
        '
        Me.labeln.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.labeln.Location = New System.Drawing.Point(6, 96)
        Me.labeln.Name = "labeln"
        Me.labeln.Size = New System.Drawing.Size(272, 42)
        Me.labeln.Text = "Descuento:"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.Label3.Location = New System.Drawing.Point(6, 188)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(272, 52)
        Me.Label3.Text = "Variacion Importe:"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.Label4.Location = New System.Drawing.Point(6, 226)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(272, 38)
        Me.Label4.Text = "Variacion Descuento:"
        '
        'lblVariacionImporte
        '
        Me.lblVariacionImporte.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblVariacionImporte.ForeColor = System.Drawing.Color.Green
        Me.lblVariacionImporte.Location = New System.Drawing.Point(268, 188)
        Me.lblVariacionImporte.Name = "lblVariacionImporte"
        Me.lblVariacionImporte.Size = New System.Drawing.Size(148, 38)
        Me.lblVariacionImporte.Text = "0.000"
        Me.lblVariacionImporte.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblVariacionDesto
        '
        Me.lblVariacionDesto.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblVariacionDesto.ForeColor = System.Drawing.Color.Green
        Me.lblVariacionDesto.Location = New System.Drawing.Point(268, 224)
        Me.lblVariacionDesto.Name = "lblVariacionDesto"
        Me.lblVariacionDesto.Size = New System.Drawing.Size(148, 38)
        Me.lblVariacionDesto.Text = "0.000"
        Me.lblVariacionDesto.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDescuentoPedido
        '
        Me.lblDescuentoPedido.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblDescuentoPedido.ForeColor = System.Drawing.Color.Maroon
        Me.lblDescuentoPedido.Location = New System.Drawing.Point(268, 100)
        Me.lblDescuentoPedido.Name = "lblDescuentoPedido"
        Me.lblDescuentoPedido.Size = New System.Drawing.Size(148, 38)
        Me.lblDescuentoPedido.Text = "0.000"
        Me.lblDescuentoPedido.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'label
        '
        Me.label.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.label.Location = New System.Drawing.Point(6, 58)
        Me.label.Name = "label"
        Me.label.Size = New System.Drawing.Size(272, 42)
        Me.label.Text = "Importe:"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(6, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(416, 42)
        Me.Label5.Text = "Datos del pedido"
        '
        'lblImportePedido
        '
        Me.lblImportePedido.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblImportePedido.ForeColor = System.Drawing.Color.Maroon
        Me.lblImportePedido.Location = New System.Drawing.Point(268, 62)
        Me.lblImportePedido.Name = "lblImportePedido"
        Me.lblImportePedido.Size = New System.Drawing.Size(148, 38)
        Me.lblImportePedido.Text = "0.000"
        Me.lblImportePedido.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(8, 136)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(416, 42)
        Me.Label6.Text = "Variacion del despacho"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(8, 276)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(416, 42)
        Me.Label1.Text = "Datos del despacho"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.Label10.Location = New System.Drawing.Point(6, 328)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(272, 52)
        Me.Label10.Text = "Importe:"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.Label9.Location = New System.Drawing.Point(6, 366)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(272, 36)
        Me.Label9.Text = "Descuento:"
        '
        'lblImporteDespacho
        '
        Me.lblImporteDespacho.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblImporteDespacho.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblImporteDespacho.Location = New System.Drawing.Point(268, 328)
        Me.lblImporteDespacho.Name = "lblImporteDespacho"
        Me.lblImporteDespacho.Size = New System.Drawing.Size(148, 38)
        Me.lblImporteDespacho.Text = "0.000"
        Me.lblImporteDespacho.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDescuentoDespacho
        '
        Me.lblDescuentoDespacho.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblDescuentoDespacho.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblDescuentoDespacho.Location = New System.Drawing.Point(268, 364)
        Me.lblDescuentoDespacho.Name = "lblDescuentoDespacho"
        Me.lblDescuentoDespacho.Size = New System.Drawing.Size(148, 38)
        Me.lblDescuentoDespacho.Text = "0.000"
        Me.lblDescuentoDespacho.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DarkRed
        Me.Panel1.Location = New System.Drawing.Point(41, 177)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(350, 1)
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.DarkRed
        Me.Panel3.Location = New System.Drawing.Point(41, 46)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(350, 1)
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.DarkRed
        Me.Panel4.Location = New System.Drawing.Point(41, 314)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(350, 1)
        '
        'lblLeyendaColumnas
        '
        Me.lblLeyendaColumnas.Font = New System.Drawing.Font("Tahoma", 5.0!, System.Drawing.FontStyle.Regular)
        Me.lblLeyendaColumnas.Location = New System.Drawing.Point(11, 407)
        Me.lblLeyendaColumnas.Name = "lblLeyendaColumnas"
        Me.lblLeyendaColumnas.Size = New System.Drawing.Size(425, 22)
        Me.lblLeyendaColumnas.Text = "|P : Columna Pedido - |D : Columna Despacho "
        Me.lblLeyendaColumnas.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'panAdicionales
        '
        Me.panAdicionales.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panAdicionales.Controls.Add(Me.lblLeyendaColumnas)
        Me.panAdicionales.Controls.Add(Me.Panel4)
        Me.panAdicionales.Controls.Add(Me.Panel3)
        Me.panAdicionales.Controls.Add(Me.Panel1)
        Me.panAdicionales.Controls.Add(Me.lblDescuentoDespacho)
        Me.panAdicionales.Controls.Add(Me.lblImporteDespacho)
        Me.panAdicionales.Controls.Add(Me.Label9)
        Me.panAdicionales.Controls.Add(Me.Label10)
        Me.panAdicionales.Controls.Add(Me.Label1)
        Me.panAdicionales.Controls.Add(Me.Label6)
        Me.panAdicionales.Controls.Add(Me.lblImportePedido)
        Me.panAdicionales.Controls.Add(Me.Label5)
        Me.panAdicionales.Controls.Add(Me.label)
        Me.panAdicionales.Controls.Add(Me.lblDescuentoPedido)
        Me.panAdicionales.Controls.Add(Me.lblVariacionDesto)
        Me.panAdicionales.Controls.Add(Me.lblVariacionImporte)
        Me.panAdicionales.Controls.Add(Me.Label4)
        Me.panAdicionales.Controls.Add(Me.Label3)
        Me.panAdicionales.Controls.Add(Me.labeln)
        Me.panAdicionales.Location = New System.Drawing.Point(32, 55)
        Me.panAdicionales.Name = "panAdicionales"
        Me.panAdicionales.Size = New System.Drawing.Size(445, 443)
        Me.panAdicionales.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.MenuText
        Me.Panel5.Controls.Add(Me.txtEnvaceA)
        Me.Panel5.Controls.Add(Me.Label8)
        Me.Panel5.Location = New System.Drawing.Point(175, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(162, 70)
        '
        'txtEnvaceA
        '
        Me.txtEnvaceA.BackColor = System.Drawing.Color.White
        Me.txtEnvaceA.ForeColor = System.Drawing.Color.Maroon
        Me.txtEnvaceA.Location = New System.Drawing.Point(4, 30)
        Me.txtEnvaceA.Name = "txtEnvaceA"
        Me.txtEnvaceA.Size = New System.Drawing.Size(150, 32)
        Me.txtEnvaceA.Text = "Q.0.00"
        Me.txtEnvaceA.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(150, 32)
        Me.Label8.Text = "Max Envase"
        '
        'frmIngreso
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(192.0!, 192.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(480, 536)
        Me.ControlBox = False
        Me.Controls.Add(Me.lnkMas)
        Me.Controls.Add(Me.picMenu)
        Me.Controls.Add(Me.lblNombreCliente)
        Me.Controls.Add(Me.pboxAlert)
        Me.Controls.Add(Me.lblMensajeImporte)
        Me.Controls.Add(Me.picBusca)
        Me.Controls.Add(Me.picCerrar)
        Me.Controls.Add(Me.lblAtencion)
        Me.Controls.Add(Me.panBusca)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.panIngreso)
        Me.Controls.Add(Me.lstAgregados)
        Me.Controls.Add(Me.lstProductos)
        Me.Controls.Add(Me.panAdicionales)
        Me.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Menu = Me.menuIngreso
        Me.MinimizeBox = False
        Me.Name = "frmIngreso"
        Me.panIngresoC.ResumeLayout(False)
        Me.PanR3.ResumeLayout(False)
        Me.panR1.ResumeLayout(False)
        Me.panR2.ResumeLayout(False)
        Me.panIngreso.ResumeLayout(False)
        Me.panImporte.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.panBusca.ResumeLayout(False)
        Me.panAdicionales.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents softLeft As System.Windows.Forms.MenuItem
    Friend WithEvents lIngreso As System.Windows.Forms.MenuItem
    Friend WithEvents lblProducto As System.Windows.Forms.Label
    Friend WithEvents panIngresoC As System.Windows.Forms.Panel
    Friend WithEvents txtcLiquido As System.Windows.Forms.TextBox
    Friend WithEvents lblums3 As System.Windows.Forms.Label
    Friend WithEvents lblM1 As System.Windows.Forms.Label
    Friend WithEvents lblums2 As System.Windows.Forms.Label
    Friend WithEvents lblM2 As System.Windows.Forms.Label
    Friend WithEvents lblums1 As System.Windows.Forms.Label
    Friend WithEvents lblM3 As System.Windows.Forms.Label
    Friend WithEvents txtuCajaVacia As System.Windows.Forms.TextBox
    Friend WithEvents txtcEnvase As System.Windows.Forms.TextBox
    Friend WithEvents txtuEnvase As System.Windows.Forms.TextBox
    Friend WithEvents lblump1 As System.Windows.Forms.Label
    Friend WithEvents txtuLiquido As System.Windows.Forms.TextBox
    Friend WithEvents lblump2 As System.Windows.Forms.Label
    Friend WithEvents panIngreso As System.Windows.Forms.Panel
    Friend WithEvents lstProductos As System.Windows.Forms.ListView
    Friend WithEvents lstAgregados As System.Windows.Forms.ListView
    Friend WithEvents PanR3 As System.Windows.Forms.Panel
    Friend WithEvents panR1 As System.Windows.Forms.Panel
    Friend WithEvents panR2 As System.Windows.Forms.Panel
    Friend WithEvents cmdSiguiente As System.Windows.Forms.MenuItem
    Friend WithEvents lblTextImporte As System.Windows.Forms.Label
    Friend WithEvents cmdEliminar As System.Windows.Forms.PictureBox
    Friend WithEvents txtBuscar As System.Windows.Forms.TextBox
    Friend WithEvents lSoft1 As System.Windows.Forms.MenuItem
    Friend WithEvents lSoftCancelar As System.Windows.Forms.MenuItem
    Friend WithEvents imgBullets As System.Windows.Forms.ImageList
    Friend WithEvents lSoftConsulta As System.Windows.Forms.MenuItem
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents lblImporte As System.Windows.Forms.Label
    Friend WithEvents panImporte As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ltsAcumulados As System.Windows.Forms.Label
    Friend WithEvents picMenu As System.Windows.Forms.PictureBox
    Friend WithEvents lblMensajeImporte As System.Windows.Forms.Label
    Public WithEvents lblAtencion As System.Windows.Forms.Label
    Friend WithEvents panBusca As System.Windows.Forms.Panel
    Friend WithEvents picCerrar As System.Windows.Forms.PictureBox
    Friend WithEvents lblNombreCliente As System.Windows.Forms.Label
    Friend WithEvents picBusca As System.Windows.Forms.PictureBox
    Friend WithEvents pboxAlert As System.Windows.Forms.PictureBox
    Friend WithEvents imgIcons As System.Windows.Forms.ImageList
    Friend WithEvents InputPanel1 As Microsoft.WindowsCE.Forms.InputPanel
    Friend WithEvents lnkMas As System.Windows.Forms.LinkLabel
    Friend WithEvents labeln As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblVariacionImporte As System.Windows.Forms.Label
    Friend WithEvents lblVariacionDesto As System.Windows.Forms.Label
    Friend WithEvents lblDescuentoPedido As System.Windows.Forms.Label
    Friend WithEvents label As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblImportePedido As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblImporteDespacho As System.Windows.Forms.Label
    Friend WithEvents lblDescuentoDespacho As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblLeyendaColumnas As System.Windows.Forms.Label
    Friend WithEvents panAdicionales As System.Windows.Forms.Panel
    Friend WithEvents progBusqueda As System.Windows.Forms.ProgressBar
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents txtEnvaceA As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
End Class
