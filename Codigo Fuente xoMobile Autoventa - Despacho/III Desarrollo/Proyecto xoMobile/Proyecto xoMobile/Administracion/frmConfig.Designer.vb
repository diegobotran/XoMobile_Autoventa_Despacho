<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmConfig
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
    Private menuConfiguracion As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfig))
        Me.menuConfiguracion = New System.Windows.Forms.MainMenu
        Me.lSoft = New System.Windows.Forms.MenuItem
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.panLogin = New System.Windows.Forms.Panel
        Me.cmdCerrarLogin = New System.Windows.Forms.PictureBox
        Me.lblSolicitaClave = New System.Windows.Forms.Label
        Me.txtClave = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.cmdRestaurar = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.cmdRecuperar = New System.Windows.Forms.Button
        Me.Label11 = New System.Windows.Forms.Label
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.dgCliente = New System.Windows.Forms.ListBox
        Me.dgProducto = New System.Windows.Forms.ListBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.TrackBar3 = New System.Windows.Forms.TrackBar
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblAlertEliminar = New System.Windows.Forms.Label
        Me.cmdAll = New System.Windows.Forms.Button
        Me.cmdSingle = New System.Windows.Forms.Button
        Me.cmdCliente = New System.Windows.Forms.Button
        Me.cmdProducto = New System.Windows.Forms.Button
        Me.txtPalabra = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TrackBar2 = New System.Windows.Forms.TrackBar
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.panValidaInventario = New System.Windows.Forms.Panel
        Me.TrackBar1 = New System.Windows.Forms.TrackBar
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.tabConfig = New System.Windows.Forms.TabControl
        Me.Label12 = New System.Windows.Forms.Label
        Me.TrackBar4 = New System.Windows.Forms.TrackBar
        Me.Label13 = New System.Windows.Forms.Label
        Me.TabPage3.SuspendLayout()
        Me.panLogin.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.panValidaInventario.SuspendLayout()
        Me.tabConfig.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuConfiguracion
        '
        Me.menuConfiguracion.MenuItems.Add(Me.lSoft)
        '
        'lSoft
        '
        Me.lSoft.Text = "Salir"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.panLogin)
        Me.TabPage3.Controls.Add(Me.Label10)
        Me.TabPage3.Controls.Add(Me.cmdRestaurar)
        Me.TabPage3.Controls.Add(Me.Label6)
        Me.TabPage3.Controls.Add(Me.lblTitulo)
        Me.TabPage3.Controls.Add(Me.cmdRecuperar)
        Me.TabPage3.Controls.Add(Me.Label11)
        Me.TabPage3.Location = New System.Drawing.Point(0, 0)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(232, 242)
        Me.TabPage3.Text = "Datos"
        '
        'panLogin
        '
        Me.panLogin.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.panLogin.Controls.Add(Me.cmdCerrarLogin)
        Me.panLogin.Controls.Add(Me.lblSolicitaClave)
        Me.panLogin.Controls.Add(Me.txtClave)
        Me.panLogin.Location = New System.Drawing.Point(3, 130)
        Me.panLogin.Name = "panLogin"
        Me.panLogin.Size = New System.Drawing.Size(233, 82)
        Me.panLogin.Visible = False
        '
        'cmdCerrarLogin
        '
        Me.cmdCerrarLogin.BackColor = System.Drawing.Color.Transparent
        Me.cmdCerrarLogin.Image = CType(resources.GetObject("cmdCerrarLogin.Image"), System.Drawing.Image)
        Me.cmdCerrarLogin.Location = New System.Drawing.Point(399, 1)
        Me.cmdCerrarLogin.Name = "cmdCerrarLogin"
        Me.cmdCerrarLogin.Size = New System.Drawing.Size(32, 34)
        '
        'lblSolicitaClave
        '
        Me.lblSolicitaClave.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblSolicitaClave.Location = New System.Drawing.Point(3, 5)
        Me.lblSolicitaClave.Name = "lblSolicitaClave"
        Me.lblSolicitaClave.Size = New System.Drawing.Size(200, 35)
        Me.lblSolicitaClave.Text = "Clave de acceso:"
        '
        'txtClave
        '
        Me.txtClave.ForeColor = System.Drawing.Color.Maroon
        Me.txtClave.Location = New System.Drawing.Point(12, 38)
        Me.txtClave.Name = "txtClave"
        Me.txtClave.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtClave.Size = New System.Drawing.Size(199, 21)
        Me.txtClave.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.ForeColor = System.Drawing.Color.Maroon
        Me.Label10.Location = New System.Drawing.Point(7, 137)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(170, 27)
        Me.Label10.Text = "Reconstruir Datos."
        '
        'cmdRestaurar
        '
        Me.cmdRestaurar.Location = New System.Drawing.Point(156, 213)
        Me.cmdRestaurar.Name = "cmdRestaurar"
        Me.cmdRestaurar.Size = New System.Drawing.Size(78, 28)
        Me.cmdRestaurar.TabIndex = 13
        Me.cmdRestaurar.Text = "Reconstruir"
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.Color.Navy
        Me.Label6.Location = New System.Drawing.Point(6, 162)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(230, 64)
        Me.Label6.Text = "Elimina los datos y deja el sistema como le fue entregado al momento de iniciar l" & _
            "a ruta el dia de hoy."
        '
        'lblTitulo
        '
        Me.lblTitulo.ForeColor = System.Drawing.Color.Maroon
        Me.lblTitulo.Location = New System.Drawing.Point(4, 0)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(170, 27)
        Me.lblTitulo.Text = "Recuperar Datos."
        '
        'cmdRecuperar
        '
        Me.cmdRecuperar.Location = New System.Drawing.Point(156, 101)
        Me.cmdRecuperar.Name = "cmdRecuperar"
        Me.cmdRecuperar.Size = New System.Drawing.Size(80, 27)
        Me.cmdRecuperar.TabIndex = 9
        Me.cmdRecuperar.Text = "Recuperar"
        '
        'Label11
        '
        Me.Label11.ForeColor = System.Drawing.Color.Navy
        Me.Label11.Location = New System.Drawing.Point(3, 26)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(230, 81)
        Me.Label11.Text = "Utilice esta opcion para recuperar los datos hasta la ultima operacion realizada." & _
            " no perdera toda su informacion, si esto no funciona debe usar la opcion reconst" & _
            "ruir"
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Controls.Add(Me.panValidaInventario)
        Me.TabPage1.Location = New System.Drawing.Point(0, 0)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(240, 245)
        Me.TabPage1.Text = "Generales"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.TrackBar4)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.dgCliente)
        Me.Panel1.Controls.Add(Me.dgProducto)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.TrackBar3)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.lblAlertEliminar)
        Me.Panel1.Controls.Add(Me.cmdAll)
        Me.Panel1.Controls.Add(Me.cmdSingle)
        Me.Panel1.Controls.Add(Me.cmdCliente)
        Me.Panel1.Controls.Add(Me.cmdProducto)
        Me.Panel1.Controls.Add(Me.txtPalabra)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.TrackBar2)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(240, 245)
        Me.Panel1.Tag = "Vend"
        '
        'dgCliente
        '
        Me.dgCliente.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.dgCliente.Location = New System.Drawing.Point(122, 146)
        Me.dgCliente.Name = "dgCliente"
        Me.dgCliente.Size = New System.Drawing.Size(114, 67)
        Me.dgCliente.TabIndex = 27
        '
        'dgProducto
        '
        Me.dgProducto.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.dgProducto.Location = New System.Drawing.Point(2, 146)
        Me.dgProducto.Name = "dgProducto"
        Me.dgProducto.Size = New System.Drawing.Size(114, 67)
        Me.dgProducto.TabIndex = 26
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.Label9.Location = New System.Drawing.Point(16, 24)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(71, 11)
        Me.Label9.Text = "Producto"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.Label8.Location = New System.Drawing.Point(89, 26)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(71, 11)
        Me.Label8.Text = "Cliente"
        '
        'TrackBar3
        '
        Me.TrackBar3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TrackBar3.LargeChange = 1
        Me.TrackBar3.Location = New System.Drawing.Point(16, 35)
        Me.TrackBar3.Maximum = 1
        Me.TrackBar3.Name = "TrackBar3"
        Me.TrackBar3.Size = New System.Drawing.Size(50, 27)
        Me.TrackBar3.TabIndex = 20
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.Label7.Location = New System.Drawing.Point(16, 62)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 11)
        Me.Label7.Text = "Id        Texto"
        '
        'lblAlertEliminar
        '
        Me.lblAlertEliminar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblAlertEliminar.ForeColor = System.Drawing.Color.Maroon
        Me.lblAlertEliminar.Location = New System.Drawing.Point(3, 234)
        Me.lblAlertEliminar.Name = "lblAlertEliminar"
        Me.lblAlertEliminar.Size = New System.Drawing.Size(105, 28)
        '
        'cmdAll
        '
        Me.cmdAll.Location = New System.Drawing.Point(198, 217)
        Me.cmdAll.Name = "cmdAll"
        Me.cmdAll.Size = New System.Drawing.Size(39, 25)
        Me.cmdAll.TabIndex = 16
        Me.cmdAll.Text = "Todo"
        '
        'cmdSingle
        '
        Me.cmdSingle.Location = New System.Drawing.Point(123, 217)
        Me.cmdSingle.Name = "cmdSingle"
        Me.cmdSingle.Size = New System.Drawing.Size(69, 25)
        Me.cmdSingle.TabIndex = 15
        Me.cmdSingle.Text = "Seleccion"
        '
        'cmdCliente
        '
        Me.cmdCliente.Location = New System.Drawing.Point(127, 119)
        Me.cmdCliente.Name = "cmdCliente"
        Me.cmdCliente.Size = New System.Drawing.Size(83, 23)
        Me.cmdCliente.TabIndex = 10
        Me.cmdCliente.Text = "Cliente"
        '
        'cmdProducto
        '
        Me.cmdProducto.Location = New System.Drawing.Point(4, 119)
        Me.cmdProducto.Name = "cmdProducto"
        Me.cmdProducto.Size = New System.Drawing.Size(83, 23)
        Me.cmdProducto.TabIndex = 9
        Me.cmdProducto.Text = "Producto"
        '
        'txtPalabra
        '
        Me.txtPalabra.Location = New System.Drawing.Point(6, 92)
        Me.txtPalabra.Name = "txtPalabra"
        Me.txtPalabra.Size = New System.Drawing.Size(234, 21)
        Me.txtPalabra.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label5.ForeColor = System.Drawing.Color.Maroon
        Me.Label5.Location = New System.Drawing.Point(7, 79)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(140, 20)
        Me.Label5.Text = "Agregar palabra clave:"
        '
        'TrackBar2
        '
        Me.TrackBar2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TrackBar2.LargeChange = 1
        Me.TrackBar2.Location = New System.Drawing.Point(81, 35)
        Me.TrackBar2.Maximum = 1
        Me.TrackBar2.Name = "TrackBar2"
        Me.TrackBar2.Size = New System.Drawing.Size(57, 27)
        Me.TrackBar2.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.Label4.Location = New System.Drawing.Point(82, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 11)
        Me.Label4.Text = "Id        Texto"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label3.ForeColor = System.Drawing.Color.Maroon
        Me.Label3.Location = New System.Drawing.Point(4, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(141, 16)
        Me.Label3.Text = "Clave de busqueda:"
        '
        'panValidaInventario
        '
        Me.panValidaInventario.Controls.Add(Me.TrackBar1)
        Me.panValidaInventario.Controls.Add(Me.Label2)
        Me.panValidaInventario.Controls.Add(Me.Label1)
        Me.panValidaInventario.Location = New System.Drawing.Point(0, 0)
        Me.panValidaInventario.Name = "panValidaInventario"
        Me.panValidaInventario.Size = New System.Drawing.Size(238, 46)
        Me.panValidaInventario.Tag = "Supe"
        '
        'TrackBar1
        '
        Me.TrackBar1.LargeChange = 2
        Me.TrackBar1.Location = New System.Drawing.Point(160, 1)
        Me.TrackBar1.Maximum = 1
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(50, 27)
        Me.TrackBar1.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.Label2.Location = New System.Drawing.Point(150, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 11)
        Me.Label2.Text = "  SI         NO"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(3, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(141, 30)
        Me.Label1.Text = "Permite vender sin validar inventario?"
        '
        'tabConfig
        '
        Me.tabConfig.Controls.Add(Me.TabPage1)
        Me.tabConfig.Controls.Add(Me.TabPage3)
        Me.tabConfig.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabConfig.Location = New System.Drawing.Point(0, 0)
        Me.tabConfig.Name = "tabConfig"
        Me.tabConfig.SelectedIndex = 0
        Me.tabConfig.Size = New System.Drawing.Size(240, 268)
        Me.tabConfig.TabIndex = 2
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.Label12.Location = New System.Drawing.Point(165, 26)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(71, 11)
        Me.Label12.Text = "Conexión"
        '
        'TrackBar4
        '
        Me.TrackBar4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TrackBar4.LargeChange = 1
        Me.TrackBar4.Location = New System.Drawing.Point(156, 35)
        Me.TrackBar4.Maximum = 1
        Me.TrackBar4.Name = "TrackBar4"
        Me.TrackBar4.Size = New System.Drawing.Size(57, 27)
        Me.TrackBar4.TabIndex = 37
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.Label13.Location = New System.Drawing.Point(154, 62)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(81, 11)
        Me.Label13.Text = "Red        Internet"
        '
        'frmConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.ControlBox = False
        Me.Controls.Add(Me.tabConfig)
        Me.Menu = Me.menuConfiguracion
        Me.Name = "frmConfig"
        Me.Text = "Configuracion"
        Me.TabPage3.ResumeLayout(False)
        Me.panLogin.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.panValidaInventario.ResumeLayout(False)
        Me.tabConfig.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lSoft As System.Windows.Forms.MenuItem
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmdRestaurar As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents cmdRecuperar As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents dgCliente As System.Windows.Forms.ListBox
    Friend WithEvents dgProducto As System.Windows.Forms.ListBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TrackBar3 As System.Windows.Forms.TrackBar
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblAlertEliminar As System.Windows.Forms.Label
    Friend WithEvents cmdAll As System.Windows.Forms.Button
    Friend WithEvents cmdSingle As System.Windows.Forms.Button
    Friend WithEvents cmdCliente As System.Windows.Forms.Button
    Friend WithEvents cmdProducto As System.Windows.Forms.Button
    Friend WithEvents txtPalabra As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TrackBar2 As System.Windows.Forms.TrackBar
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents panValidaInventario As System.Windows.Forms.Panel
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tabConfig As System.Windows.Forms.TabControl
    Friend WithEvents panLogin As System.Windows.Forms.Panel
    Friend WithEvents cmdCerrarLogin As System.Windows.Forms.PictureBox
    Friend WithEvents lblSolicitaClave As System.Windows.Forms.Label
    Friend WithEvents txtClave As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TrackBar4 As System.Windows.Forms.TrackBar
    Friend WithEvents Label13 As System.Windows.Forms.Label
End Class
