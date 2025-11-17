<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmClienteNuevo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmClienteNuevo))
        Me.txtNombre = New System.Windows.Forms.TextBox
        Me.txtNit = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtDireccion = New System.Windows.Forms.TextBox
        Me.MainMenu1 = New System.Windows.Forms.MainMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.lblDescripcion = New System.Windows.Forms.Label
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtDummy = New System.Windows.Forms.TextBox
        Me.txtDPI = New System.Windows.Forms.TextBox
        Me.txtNegocio = New System.Windows.Forms.TextBox
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel3 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel4 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel5 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel6 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel7 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel8 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel9 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel10 = New System.Windows.Forms.LinkLabel
        Me.imgHelp = New System.Windows.Forms.PictureBox
        Me.lstGiro = New System.Windows.Forms.ComboBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.txtMunicipio = New System.Windows.Forms.ComboBox
        Me.txtDepartamento = New System.Windows.Forms.ComboBox
        Me.txtPasaporte = New System.Windows.Forms.TextBox
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtNombre
        '
        Me.txtNombre.ForeColor = System.Drawing.Color.Gray
        Me.txtNombre.Location = New System.Drawing.Point(3, 161)
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(450, 41)
        Me.txtNombre.TabIndex = 3
        Me.txtNombre.Text = "Nombre del cliente"
        '
        'txtNit
        '
        Me.txtNit.ForeColor = System.Drawing.Color.Gray
        Me.txtNit.Location = New System.Drawing.Point(3, 208)
        Me.txtNit.Name = "txtNit"
        Me.txtNit.Size = New System.Drawing.Size(450, 41)
        Me.txtNit.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(0, 500)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(405, 41)
        Me.Label4.Text = "Direccion"
        '
        'txtDireccion
        '
        Me.txtDireccion.Location = New System.Drawing.Point(3, 578)
        Me.txtDireccion.Multiline = True
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.Size = New System.Drawing.Size(450, 243)
        Me.txtDireccion.TabIndex = 3
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.Add(Me.MenuItem1)
        Me.MainMenu1.MenuItems.Add(Me.MenuItem2)
        '
        'MenuItem1
        '
        Me.MenuItem1.Text = "Cancelar"
        '
        'MenuItem2
        '
        Me.MenuItem2.Text = "Crear"
        '
        'lblDescripcion
        '
        Me.lblDescripcion.BackColor = System.Drawing.Color.SteelBlue
        Me.lblDescripcion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblDescripcion.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.lblDescripcion.Location = New System.Drawing.Point(3, 34)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(466, 58)
        Me.lblDescripcion.Text = "Despues de crear un cliente podra realizar las operaciones de atencion y emitir u" & _
            "n pedido."
        '
        'lblTitulo
        '
        Me.lblTitulo.BackColor = System.Drawing.Color.SteelBlue
        Me.lblTitulo.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular)
        Me.lblTitulo.ForeColor = System.Drawing.Color.White
        Me.lblTitulo.Location = New System.Drawing.Point(0, 0)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(453, 64)
        Me.lblTitulo.Text = "Creacion de cliente nuevo"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.SteelBlue
        Me.Panel1.Controls.Add(Me.lblDescripcion)
        Me.Panel1.Controls.Add(Me.lblTitulo)
        Me.Panel1.Controls.Add(Me.txtDummy)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(454, 100)
        '
        'txtDummy
        '
        Me.txtDummy.ForeColor = System.Drawing.Color.Gray
        Me.txtDummy.Location = New System.Drawing.Point(3, 48)
        Me.txtDummy.Name = "txtDummy"
        Me.txtDummy.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDummy.Size = New System.Drawing.Size(450, 41)
        Me.txtDummy.TabIndex = 11
        '
        'txtDPI
        '
        Me.txtDPI.ForeColor = System.Drawing.Color.Gray
        Me.txtDPI.Location = New System.Drawing.Point(3, 257)
        Me.txtDPI.MaxLength = 13
        Me.txtDPI.Name = "txtDPI"
        Me.txtDPI.Size = New System.Drawing.Size(450, 41)
        Me.txtDPI.TabIndex = 5
        Me.txtDPI.Text = "DPI"
        '
        'txtNegocio
        '
        Me.txtNegocio.ForeColor = System.Drawing.Color.Gray
        Me.txtNegocio.Location = New System.Drawing.Point(3, 114)
        Me.txtNegocio.Name = "txtNegocio"
        Me.txtNegocio.Size = New System.Drawing.Size(450, 41)
        Me.txtNegocio.TabIndex = 8
        Me.txtNegocio.Text = "Nombre del negocio"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Underline)
        Me.LinkLabel1.Location = New System.Drawing.Point(0, 541)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(64, 30)
        Me.LinkLabel1.TabIndex = 11
        Me.LinkLabel1.Text = " Calle"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Underline)
        Me.LinkLabel2.Location = New System.Drawing.Point(60, 541)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(50, 30)
        Me.LinkLabel2.TabIndex = 11
        Me.LinkLabel2.Text = " AV"
        '
        'LinkLabel3
        '
        Me.LinkLabel3.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Underline)
        Me.LinkLabel3.ForeColor = System.Drawing.Color.Coral
        Me.LinkLabel3.Location = New System.Drawing.Point(278, 500)
        Me.LinkLabel3.Name = "LinkLabel3"
        Me.LinkLabel3.Size = New System.Drawing.Size(71, 30)
        Me.LinkLabel3.TabIndex = 11
        Me.LinkLabel3.Text = "Guion"
        '
        'LinkLabel4
        '
        Me.LinkLabel4.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Underline)
        Me.LinkLabel4.ForeColor = System.Drawing.Color.Coral
        Me.LinkLabel4.Location = New System.Drawing.Point(361, 500)
        Me.LinkLabel4.Name = "LinkLabel4"
        Me.LinkLabel4.Size = New System.Drawing.Size(90, 30)
        Me.LinkLabel4.TabIndex = 11
        Me.LinkLabel4.Text = "Espacio"
        '
        'LinkLabel5
        '
        Me.LinkLabel5.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Underline)
        Me.LinkLabel5.Location = New System.Drawing.Point(100, 541)
        Me.LinkLabel5.Name = "LinkLabel5"
        Me.LinkLabel5.Size = New System.Drawing.Size(62, 30)
        Me.LinkLabel5.TabIndex = 11
        Me.LinkLabel5.Text = " Zona"
        '
        'LinkLabel6
        '
        Me.LinkLabel6.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Underline)
        Me.LinkLabel6.Location = New System.Drawing.Point(162, 541)
        Me.LinkLabel6.Name = "LinkLabel6"
        Me.LinkLabel6.Size = New System.Drawing.Size(49, 30)
        Me.LinkLabel6.TabIndex = 11
        Me.LinkLabel6.Text = " Col."
        '
        'LinkLabel7
        '
        Me.LinkLabel7.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Underline)
        Me.LinkLabel7.Location = New System.Drawing.Point(332, 541)
        Me.LinkLabel7.Name = "LinkLabel7"
        Me.LinkLabel7.Size = New System.Drawing.Size(59, 30)
        Me.LinkLabel7.TabIndex = 11
        Me.LinkLabel7.Text = " Lote"
        '
        'LinkLabel8
        '
        Me.LinkLabel8.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Underline)
        Me.LinkLabel8.Location = New System.Drawing.Point(384, 541)
        Me.LinkLabel8.Name = "LinkLabel8"
        Me.LinkLabel8.Size = New System.Drawing.Size(67, 30)
        Me.LinkLabel8.TabIndex = 11
        Me.LinkLabel8.Text = " Manz."
        '
        'LinkLabel9
        '
        Me.LinkLabel9.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Underline)
        Me.LinkLabel9.Location = New System.Drawing.Point(222, 541)
        Me.LinkLabel9.Name = "LinkLabel9"
        Me.LinkLabel9.Size = New System.Drawing.Size(50, 30)
        Me.LinkLabel9.TabIndex = 11
        Me.LinkLabel9.Text = " Blv."
        '
        'LinkLabel10
        '
        Me.LinkLabel10.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Underline)
        Me.LinkLabel10.Location = New System.Drawing.Point(278, 541)
        Me.LinkLabel10.Name = "LinkLabel10"
        Me.LinkLabel10.Size = New System.Drawing.Size(56, 30)
        Me.LinkLabel10.TabIndex = 11
        Me.LinkLabel10.Text = " KM."
        '
        'imgHelp
        '
        Me.imgHelp.Image = CType(resources.GetObject("imgHelp.Image"), System.Drawing.Image)
        Me.imgHelp.Location = New System.Drawing.Point(116, 500)
        Me.imgHelp.Name = "imgHelp"
        Me.imgHelp.Size = New System.Drawing.Size(32, 32)
        Me.imgHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        '
        'lstGiro
        '
        Me.lstGiro.Location = New System.Drawing.Point(4, 441)
        Me.lstGiro.Name = "lstGiro"
        Me.lstGiro.Size = New System.Drawing.Size(448, 41)
        Me.lstGiro.TabIndex = 14
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(409, 347)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(42, 41)
        Me.Button1.TabIndex = 27
        Me.Button1.Text = "+"
        '
        'txtMunicipio
        '
        Me.txtMunicipio.Location = New System.Drawing.Point(5, 395)
        Me.txtMunicipio.Name = "txtMunicipio"
        Me.txtMunicipio.Size = New System.Drawing.Size(386, 41)
        Me.txtMunicipio.TabIndex = 26
        '
        'txtDepartamento
        '
        Me.txtDepartamento.Location = New System.Drawing.Point(4, 348)
        Me.txtDepartamento.Name = "txtDepartamento"
        Me.txtDepartamento.Size = New System.Drawing.Size(387, 41)
        Me.txtDepartamento.TabIndex = 25
        '
        'txtPasaporte
        '
        Me.txtPasaporte.ForeColor = System.Drawing.Color.Gray
        Me.txtPasaporte.Location = New System.Drawing.Point(3, 304)
        Me.txtPasaporte.Name = "txtPasaporte"
        Me.txtPasaporte.Size = New System.Drawing.Size(450, 41)
        Me.txtPasaporte.TabIndex = 31
        Me.txtPasaporte.Text = "PASAPORTE"
        '
        'frmClienteNuevo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(192.0!, 192.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(480, 536)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtPasaporte)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtMunicipio)
        Me.Controls.Add(Me.txtDepartamento)
        Me.Controls.Add(Me.lstGiro)
        Me.Controls.Add(Me.imgHelp)
        Me.Controls.Add(Me.LinkLabel6)
        Me.Controls.Add(Me.LinkLabel5)
        Me.Controls.Add(Me.LinkLabel10)
        Me.Controls.Add(Me.LinkLabel9)
        Me.Controls.Add(Me.LinkLabel8)
        Me.Controls.Add(Me.LinkLabel7)
        Me.Controls.Add(Me.LinkLabel4)
        Me.Controls.Add(Me.LinkLabel3)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.txtNegocio)
        Me.Controls.Add(Me.txtDPI)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.txtDireccion)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtNit)
        Me.Controls.Add(Me.txtNombre)
        Me.Location = New System.Drawing.Point(0, 52)
        Me.Menu = Me.MainMenu1
        Me.MinimizeBox = False
        Me.Name = "frmClienteNuevo"
        Me.Text = "Agregar Cliente"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtNombre As System.Windows.Forms.TextBox
    Friend WithEvents txtNit As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDireccion As System.Windows.Forms.TextBox
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents lblDescripcion As System.Windows.Forms.Label
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtDPI As System.Windows.Forms.TextBox
    Friend WithEvents txtNegocio As System.Windows.Forms.TextBox
    Friend WithEvents txtDummy As System.Windows.Forms.TextBox
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel3 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel4 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel5 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel6 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel7 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel8 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel9 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel10 As System.Windows.Forms.LinkLabel
    Friend WithEvents imgHelp As System.Windows.Forms.PictureBox
    Friend WithEvents lstGiro As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtMunicipio As System.Windows.Forms.ComboBox
    Friend WithEvents txtDepartamento As System.Windows.Forms.ComboBox
    Friend WithEvents txtPasaporte As System.Windows.Forms.TextBox
End Class
