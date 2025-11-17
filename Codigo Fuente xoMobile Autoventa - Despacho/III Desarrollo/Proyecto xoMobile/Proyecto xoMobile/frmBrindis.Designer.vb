<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmBrindis
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBrindis))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblFecha = New System.Windows.Forms.Label
        Me.lblHora = New System.Windows.Forms.Label
        Me.lblDia = New System.Windows.Forms.Label
        Me.lblDiaVenta = New System.Windows.Forms.Label
        Me.lblRuta = New System.Windows.Forms.Label
        Me.lblDireccion = New System.Windows.Forms.Label
        Me.lblTelefonos = New System.Windows.Forms.Label
        Me.panTitulo = New System.Windows.Forms.Panel
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.panTiempo = New System.Windows.Forms.Panel
        Me.lblVendedor = New System.Windows.Forms.Label
        Me.panPie = New System.Windows.Forms.Panel
        Me.panWarning = New System.Windows.Forms.Panel
        Me.lblWarning = New System.Windows.Forms.Label
        Me.cmdCarga = New System.Windows.Forms.Button
        Me.panCarga = New System.Windows.Forms.Panel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.panContinuar = New System.Windows.Forms.Panel
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.cmdIniciar = New System.Windows.Forms.Button
        Me.panLogin = New System.Windows.Forms.Panel
        Me.panResultLogin = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtClave = New System.Windows.Forms.TextBox
        Me.lnk = New System.Windows.Forms.LinkLabel
        Me.barNc = New System.Windows.Forms.ProgressBar
        Me.barFactura = New System.Windows.Forms.ProgressBar
        Me.barRecibo = New System.Windows.Forms.ProgressBar
        Me.lblRec = New System.Windows.Forms.Label
        Me.lblNc = New System.Windows.Forms.Label
        Me.lblFact = New System.Windows.Forms.Label
        Me.MainMenu1 = New System.Windows.Forms.MainMenu
        Me.lnkRuta = New System.Windows.Forms.LinkLabel
        Me.ImageList1 = New System.Windows.Forms.ImageList
        Me.panTitulo.SuspendLayout()
        Me.panTiempo.SuspendLayout()
        Me.panPie.SuspendLayout()
        Me.panWarning.SuspendLayout()
        Me.panCarga.SuspendLayout()
        Me.panContinuar.SuspendLayout()
        Me.panLogin.SuspendLayout()
        Me.panResultLogin.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(3, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(235, 20)
        Me.Label1.Text = "DISTRIBUIDORA DE LICORES.S. A.                    "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 13.0!, System.Drawing.FontStyle.Regular)
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(3, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(235, 21)
        Me.Label2.Text = "Sistema de ventas en Ruta"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblFecha
        '
        Me.lblFecha.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.lblFecha.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblFecha.ForeColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(89, Byte), Integer), CType(CType(152, Byte), Integer))
        Me.lblFecha.Location = New System.Drawing.Point(3, 4)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(77, 18)
        Me.lblFecha.Text = "23/07/2011"
        '
        'lblHora
        '
        Me.lblHora.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.lblHora.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblHora.ForeColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(89, Byte), Integer), CType(CType(152, Byte), Integer))
        Me.lblHora.Location = New System.Drawing.Point(85, 4)
        Me.lblHora.Name = "lblHora"
        Me.lblHora.Size = New System.Drawing.Size(56, 15)
        Me.lblHora.Text = "16:00 PM"
        '
        'lblDia
        '
        Me.lblDia.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.lblDia.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblDia.ForeColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(89, Byte), Integer), CType(CType(152, Byte), Integer))
        Me.lblDia.Location = New System.Drawing.Point(147, 4)
        Me.lblDia.Name = "lblDia"
        Me.lblDia.Size = New System.Drawing.Size(35, 16)
        Me.lblDia.Text = "HOY: "
        '
        'lblDiaVenta
        '
        Me.lblDiaVenta.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDiaVenta.ForeColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(89, Byte), Integer), CType(CType(152, Byte), Integer))
        Me.lblDiaVenta.Location = New System.Drawing.Point(177, 4)
        Me.lblDiaVenta.Name = "lblDiaVenta"
        Me.lblDiaVenta.Size = New System.Drawing.Size(60, 16)
        '
        'lblRuta
        '
        Me.lblRuta.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblRuta.ForeColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(89, Byte), Integer), CType(CType(152, Byte), Integer))
        Me.lblRuta.Location = New System.Drawing.Point(3, 19)
        Me.lblRuta.Name = "lblRuta"
        Me.lblRuta.Size = New System.Drawing.Size(48, 14)
        '
        'lblDireccion
        '
        Me.lblDireccion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblDireccion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(89, Byte), Integer), CType(CType(152, Byte), Integer))
        Me.lblDireccion.Location = New System.Drawing.Point(1, 1)
        Me.lblDireccion.Name = "lblDireccion"
        Me.lblDireccion.Size = New System.Drawing.Size(230, 32)
        Me.lblDireccion.Text = "DIRECCION DE LA EMPRESA: KM. 16.5 CARRETERA ROOSEVELT 4-81 ZONA 1 MIXCO          " & _
            "                                            "
        Me.lblDireccion.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblTelefonos
        '
        Me.lblTelefonos.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblTelefonos.ForeColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(89, Byte), Integer), CType(CType(152, Byte), Integer))
        Me.lblTelefonos.Location = New System.Drawing.Point(3, 33)
        Me.lblTelefonos.Name = "lblTelefonos"
        Me.lblTelefonos.Size = New System.Drawing.Size(230, 15)
        Me.lblTelefonos.Text = "TELEFONOS EMPRESA : 24709696  "
        Me.lblTelefonos.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'panTitulo
        '
        Me.panTitulo.Controls.Add(Me.LinkLabel1)
        Me.panTitulo.Controls.Add(Me.Label2)
        Me.panTitulo.Controls.Add(Me.Label1)
        Me.panTitulo.Location = New System.Drawing.Point(0, 0)
        Me.panTitulo.Name = "panTitulo"
        Me.panTitulo.Size = New System.Drawing.Size(240, 55)
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.LinkLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.LinkLabel1.Location = New System.Drawing.Point(3, 38)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(233, 20)
        Me.LinkLabel1.TabIndex = 16
        Me.LinkLabel1.Text = "XO MOBILE VER. 1.0"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'panTiempo
        '
        Me.panTiempo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.panTiempo.Controls.Add(Me.lblVendedor)
        Me.panTiempo.Controls.Add(Me.lblRuta)
        Me.panTiempo.Controls.Add(Me.lblDiaVenta)
        Me.panTiempo.Controls.Add(Me.lblDia)
        Me.panTiempo.Controls.Add(Me.lblHora)
        Me.panTiempo.Controls.Add(Me.lblFecha)
        Me.panTiempo.Location = New System.Drawing.Point(0, 54)
        Me.panTiempo.Name = "panTiempo"
        Me.panTiempo.Size = New System.Drawing.Size(240, 39)
        '
        'lblVendedor
        '
        Me.lblVendedor.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.lblVendedor.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblVendedor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(89, Byte), Integer), CType(CType(152, Byte), Integer))
        Me.lblVendedor.Location = New System.Drawing.Point(79, 19)
        Me.lblVendedor.Name = "lblVendedor"
        Me.lblVendedor.Size = New System.Drawing.Size(158, 14)
        '
        'panPie
        '
        Me.panPie.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.panPie.Controls.Add(Me.lblDireccion)
        Me.panPie.Controls.Add(Me.lblTelefonos)
        Me.panPie.Location = New System.Drawing.Point(0, 199)
        Me.panPie.Name = "panPie"
        Me.panPie.Size = New System.Drawing.Size(240, 50)
        '
        'panWarning
        '
        Me.panWarning.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.panWarning.Controls.Add(Me.lblWarning)
        Me.panWarning.Location = New System.Drawing.Point(0, 200)
        Me.panWarning.Name = "panWarning"
        Me.panWarning.Size = New System.Drawing.Size(240, 49)
        Me.panWarning.Visible = False
        '
        'lblWarning
        '
        Me.lblWarning.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblWarning.Location = New System.Drawing.Point(11, 12)
        Me.lblWarning.Name = "lblWarning"
        Me.lblWarning.Size = New System.Drawing.Size(218, 25)
        Me.lblWarning.Text = "Realice las operaciones indicadas para continuar"
        Me.lblWarning.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmdCarga
        '
        Me.cmdCarga.Location = New System.Drawing.Point(44, 1)
        Me.cmdCarga.Name = "cmdCarga"
        Me.cmdCarga.Size = New System.Drawing.Size(122, 25)
        Me.cmdCarga.TabIndex = 12
        Me.cmdCarga.Text = "Confirmar Carga"
        '
        'panCarga
        '
        Me.panCarga.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.panCarga.Controls.Add(Me.PictureBox1)
        Me.panCarga.Controls.Add(Me.cmdCarga)
        Me.panCarga.Location = New System.Drawing.Point(32, 110)
        Me.panCarga.Name = "panCarga"
        Me.panCarga.Size = New System.Drawing.Size(178, 26)
        Me.panCarga.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(14, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 24)
        '
        'panContinuar
        '
        Me.panContinuar.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.panContinuar.Controls.Add(Me.PictureBox2)
        Me.panContinuar.Controls.Add(Me.cmdIniciar)
        Me.panContinuar.Location = New System.Drawing.Point(32, 141)
        Me.panContinuar.Name = "panContinuar"
        Me.panContinuar.Size = New System.Drawing.Size(178, 26)
        Me.panContinuar.Visible = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(14, 1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(24, 24)
        '
        'cmdIniciar
        '
        Me.cmdIniciar.Location = New System.Drawing.Point(44, 1)
        Me.cmdIniciar.Name = "cmdIniciar"
        Me.cmdIniciar.Size = New System.Drawing.Size(122, 25)
        Me.cmdIniciar.TabIndex = 12
        Me.cmdIniciar.Text = "Iniciar"
        '
        'panLogin
        '
        Me.panLogin.Controls.Add(Me.panResultLogin)
        Me.panLogin.Controls.Add(Me.Panel1)
        Me.panLogin.Location = New System.Drawing.Point(0, 91)
        Me.panLogin.Name = "panLogin"
        Me.panLogin.Size = New System.Drawing.Size(240, 110)
        '
        'panResultLogin
        '
        Me.panResultLogin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.panResultLogin.Controls.Add(Me.Label4)
        Me.panResultLogin.Location = New System.Drawing.Point(0, 82)
        Me.panResultLogin.Name = "panResultLogin"
        Me.panResultLogin.Size = New System.Drawing.Size(240, 27)
        Me.panResultLogin.Visible = False
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label4.Location = New System.Drawing.Point(12, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(218, 18)
        Me.Label4.Text = "La clave ingresada no es correcta"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtClave)
        Me.Panel1.Location = New System.Drawing.Point(7, 24)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(226, 49)
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.Label3.Location = New System.Drawing.Point(4, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 15)
        Me.Label3.Text = "Clave de acceso"
        '
        'txtClave
        '
        Me.txtClave.ForeColor = System.Drawing.Color.Maroon
        Me.txtClave.Location = New System.Drawing.Point(3, 22)
        Me.txtClave.Name = "txtClave"
        Me.txtClave.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtClave.Size = New System.Drawing.Size(218, 21)
        Me.txtClave.TabIndex = 1
        '
        'lnk
        '
        Me.lnk.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.lnk.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Underline)
        Me.lnk.Location = New System.Drawing.Point(176, 251)
        Me.lnk.Name = "lnk"
        Me.lnk.Size = New System.Drawing.Size(57, 14)
        Me.lnk.TabIndex = 2
        Me.lnk.Text = "SYNC_APP"
        '
        'barNc
        '
        Me.barNc.Location = New System.Drawing.Point(82, 186)
        Me.barNc.Name = "barNc"
        Me.barNc.Size = New System.Drawing.Size(75, 10)
        '
        'barFactura
        '
        Me.barFactura.Location = New System.Drawing.Point(163, 186)
        Me.barFactura.Name = "barFactura"
        Me.barFactura.Size = New System.Drawing.Size(75, 10)
        '
        'barRecibo
        '
        Me.barRecibo.Location = New System.Drawing.Point(1, 186)
        Me.barRecibo.Name = "barRecibo"
        Me.barRecibo.Size = New System.Drawing.Size(75, 10)
        '
        'lblRec
        '
        Me.lblRec.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblRec.Location = New System.Drawing.Point(0, 173)
        Me.lblRec.Name = "lblRec"
        Me.lblRec.Size = New System.Drawing.Size(75, 13)
        Me.lblRec.Text = "REC"
        '
        'lblNc
        '
        Me.lblNc.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblNc.Location = New System.Drawing.Point(85, 173)
        Me.lblNc.Name = "lblNc"
        Me.lblNc.Size = New System.Drawing.Size(71, 13)
        Me.lblNc.Text = "NC"
        '
        'lblFact
        '
        Me.lblFact.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblFact.Location = New System.Drawing.Point(160, 173)
        Me.lblFact.Name = "lblFact"
        Me.lblFact.Size = New System.Drawing.Size(80, 13)
        Me.lblFact.Text = "FACT"
        '
        'lnkRuta
        '
        Me.lnkRuta.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.lnkRuta.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Underline)
        Me.lnkRuta.Location = New System.Drawing.Point(0, 251)
        Me.lnkRuta.Name = "lnkRuta"
        Me.lnkRuta.Size = New System.Drawing.Size(57, 14)
        Me.lnkRuta.TabIndex = 16
        Me.lnkRuta.Text = "MI RUTA"
        Me.lnkRuta.Visible = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        '
        'frmBrindis
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.Controls.Add(Me.lnkRuta)
        Me.Controls.Add(Me.lnk)
        Me.Controls.Add(Me.panWarning)
        Me.Controls.Add(Me.panLogin)
        Me.Controls.Add(Me.panContinuar)
        Me.Controls.Add(Me.panCarga)
        Me.Controls.Add(Me.panPie)
        Me.Controls.Add(Me.panTiempo)
        Me.Controls.Add(Me.panTitulo)
        Me.Controls.Add(Me.barRecibo)
        Me.Controls.Add(Me.lblRec)
        Me.Controls.Add(Me.lblNc)
        Me.Controls.Add(Me.barNc)
        Me.Controls.Add(Me.lblFact)
        Me.Controls.Add(Me.barFactura)
        Me.Menu = Me.MainMenu1
        Me.MinimizeBox = False
        Me.Name = "frmBrindis"
        Me.Text = "Brindis"
        Me.panTitulo.ResumeLayout(False)
        Me.panTiempo.ResumeLayout(False)
        Me.panPie.ResumeLayout(False)
        Me.panWarning.ResumeLayout(False)
        Me.panCarga.ResumeLayout(False)
        Me.panContinuar.ResumeLayout(False)
        Me.panLogin.ResumeLayout(False)
        Me.panResultLogin.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblFecha As System.Windows.Forms.Label
    Friend WithEvents lblHora As System.Windows.Forms.Label
    Friend WithEvents lblDia As System.Windows.Forms.Label
    Friend WithEvents lblDiaVenta As System.Windows.Forms.Label
    Friend WithEvents lblRuta As System.Windows.Forms.Label
    Friend WithEvents lblDireccion As System.Windows.Forms.Label
    Friend WithEvents lblTelefonos As System.Windows.Forms.Label
    Friend WithEvents panTitulo As System.Windows.Forms.Panel
    Friend WithEvents panTiempo As System.Windows.Forms.Panel
    Friend WithEvents panPie As System.Windows.Forms.Panel
    Friend WithEvents cmdCarga As System.Windows.Forms.Button
    Friend WithEvents panCarga As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblVendedor As System.Windows.Forms.Label
    Friend WithEvents panContinuar As System.Windows.Forms.Panel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents cmdIniciar As System.Windows.Forms.Button
    Friend WithEvents panLogin As System.Windows.Forms.Panel
    Friend WithEvents txtClave As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents panResultLogin As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents barNc As System.Windows.Forms.ProgressBar
    Friend WithEvents barFactura As System.Windows.Forms.ProgressBar
    Friend WithEvents barRecibo As System.Windows.Forms.ProgressBar
    Friend WithEvents lblRec As System.Windows.Forms.Label
    Friend WithEvents lblNc As System.Windows.Forms.Label
    Friend WithEvents lblFact As System.Windows.Forms.Label
    Friend WithEvents panWarning As System.Windows.Forms.Panel
    Friend WithEvents lblWarning As System.Windows.Forms.Label
    Friend WithEvents lnk As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents lnkRuta As System.Windows.Forms.LinkLabel
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
End Class
