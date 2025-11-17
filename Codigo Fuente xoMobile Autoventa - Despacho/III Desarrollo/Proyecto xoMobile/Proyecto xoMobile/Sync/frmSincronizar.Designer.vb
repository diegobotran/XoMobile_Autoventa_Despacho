<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmImportar_
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
    Private menuImportar As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImportar_))
        Me.menuImportar = New System.Windows.Forms.MainMenu
        Me.lSoft = New System.Windows.Forms.MenuItem
        Me.cmdImportar = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.pan_msImportar = New System.Windows.Forms.Panel
        Me.lbl_msImportar = New System.Windows.Forms.Label
        Me.panFechaCarga = New System.Windows.Forms.Panel
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.ProgressBar = New System.Windows.Forms.ProgressBar
        Me.lblReloj = New System.Windows.Forms.Label
        Me.lstImport = New System.Windows.Forms.ListView
        Me.imgList = New System.Windows.Forms.ImageList
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.lblEvento = New System.Windows.Forms.Label
        Me.lblRelojExp = New System.Windows.Forms.Label
        Me.panExportar = New System.Windows.Forms.Panel
        Me.lbl_msExportar = New System.Windows.Forms.Label
        Me.cmdExportar = New System.Windows.Forms.Button
        Me.lstExport = New System.Windows.Forms.ListView
        Me.txtExp = New System.Windows.Forms.TextBox
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker
        Me.xoErrorStatament = New System.Windows.Forms.Label
        Me.cmdRecarga = New System.Windows.Forms.Button
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.chkEstoyFuera = New System.Windows.Forms.CheckBox
        Me.lnkRuta = New System.Windows.Forms.LinkLabel
        Me.cmdTestc = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblServidor = New System.Windows.Forms.LinkLabel
        Me.txtServidor = New System.Windows.Forms.TextBox
        Me.lblRuta = New System.Windows.Forms.LinkLabel
        Me.txtRuta = New System.Windows.Forms.TextBox
        Me.Timer1 = New System.Windows.Forms.Timer
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.lblError = New System.Windows.Forms.Label
        Me.Timer2 = New System.Windows.Forms.Timer
        Me.ComboBox1 = New System.Windows.Forms.TextBox
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.pan_msImportar.SuspendLayout()
        Me.panFechaCarga.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.panExportar.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuImportar
        '
        Me.menuImportar.MenuItems.Add(Me.lSoft)
        '
        'lSoft
        '
        Me.lSoft.Text = "Salir"
        '
        'cmdImportar
        '
        Me.cmdImportar.Location = New System.Drawing.Point(0, 0)
        Me.cmdImportar.Name = "cmdImportar"
        Me.cmdImportar.Size = New System.Drawing.Size(240, 35)
        Me.cmdImportar.TabIndex = 0
        Me.cmdImportar.Text = "Importar"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(240, 268)
        Me.TabControl1.TabIndex = 4
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.pan_msImportar)
        Me.TabPage1.Controls.Add(Me.panFechaCarga)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.ProgressBar)
        Me.TabPage1.Controls.Add(Me.lblReloj)
        Me.TabPage1.Controls.Add(Me.lstImport)
        Me.TabPage1.Controls.Add(Me.cmdImportar)
        Me.TabPage1.Location = New System.Drawing.Point(0, 0)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(240, 245)
        Me.TabPage1.Text = "Importar"
        '
        'pan_msImportar
        '
        Me.pan_msImportar.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pan_msImportar.Controls.Add(Me.lbl_msImportar)
        Me.pan_msImportar.Location = New System.Drawing.Point(1, 208)
        Me.pan_msImportar.Name = "pan_msImportar"
        Me.pan_msImportar.Size = New System.Drawing.Size(239, 37)
        Me.pan_msImportar.Visible = False
        '
        'lbl_msImportar
        '
        Me.lbl_msImportar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lbl_msImportar.Location = New System.Drawing.Point(12, 5)
        Me.lbl_msImportar.Name = "lbl_msImportar"
        Me.lbl_msImportar.Size = New System.Drawing.Size(218, 30)
        Me.lbl_msImportar.Text = "Escriba la direccion del servidor en la pestaña PARAMETROS"
        Me.lbl_msImportar.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'panFechaCarga
        '
        Me.panFechaCarga.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.panFechaCarga.Controls.Add(Me.DateTimePicker1)
        Me.panFechaCarga.Controls.Add(Me.Label5)
        Me.panFechaCarga.Location = New System.Drawing.Point(18, 103)
        Me.panFechaCarga.Name = "panFechaCarga"
        Me.panFechaCarga.Size = New System.Drawing.Size(206, 66)
        Me.panFechaCarga.Visible = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = ""
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(12, 38)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(181, 22)
        Me.DateTimePicker1.TabIndex = 1
        Me.DateTimePicker1.Value = New Date(2011, 12, 13, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label5.Location = New System.Drawing.Point(13, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(180, 41)
        Me.Label5.Text = "Seleccione la fecha de carga y presione <ENTER>"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.Label2.Location = New System.Drawing.Point(148, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 10)
        Me.Label2.Text = "100%"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.Label1.Location = New System.Drawing.Point(4, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 10)
        Me.Label1.Text = "0%"
        '
        'ProgressBar
        '
        Me.ProgressBar.Location = New System.Drawing.Point(1, 42)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(173, 10)
        '
        'lblReloj
        '
        Me.lblReloj.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblReloj.Location = New System.Drawing.Point(173, 42)
        Me.lblReloj.Name = "lblReloj"
        Me.lblReloj.Size = New System.Drawing.Size(65, 10)
        Me.lblReloj.Text = "0"
        Me.lblReloj.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lstImport
        '
        Me.lstImport.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstImport.FullRowSelect = True
        Me.lstImport.LargeImageList = Me.imgList
        Me.lstImport.Location = New System.Drawing.Point(0, 70)
        Me.lstImport.Name = "lstImport"
        Me.lstImport.Size = New System.Drawing.Size(240, 175)
        Me.lstImport.SmallImageList = Me.imgList
        Me.lstImport.TabIndex = 4
        Me.lstImport.View = System.Windows.Forms.View.Details
        Me.imgList.Images.Clear()
        Me.imgList.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.imgList.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Image))
        Me.imgList.Images.Add(CType(resources.GetObject("resource2"), System.Drawing.Image))
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.lblEvento)
        Me.TabPage4.Controls.Add(Me.lblRelojExp)
        Me.TabPage4.Controls.Add(Me.panExportar)
        Me.TabPage4.Controls.Add(Me.cmdExportar)
        Me.TabPage4.Controls.Add(Me.lstExport)
        Me.TabPage4.Controls.Add(Me.txtExp)
        Me.TabPage4.Location = New System.Drawing.Point(0, 0)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(240, 245)
        Me.TabPage4.Text = "Exportar"
        '
        'lblEvento
        '
        Me.lblEvento.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblEvento.ForeColor = System.Drawing.Color.DarkRed
        Me.lblEvento.Location = New System.Drawing.Point(1, 35)
        Me.lblEvento.Name = "lblEvento"
        Me.lblEvento.Size = New System.Drawing.Size(195, 13)
        '
        'lblRelojExp
        '
        Me.lblRelojExp.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblRelojExp.Location = New System.Drawing.Point(172, 38)
        Me.lblRelojExp.Name = "lblRelojExp"
        Me.lblRelojExp.Size = New System.Drawing.Size(65, 10)
        Me.lblRelojExp.Text = "0"
        Me.lblRelojExp.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'panExportar
        '
        Me.panExportar.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.panExportar.Controls.Add(Me.lbl_msExportar)
        Me.panExportar.Location = New System.Drawing.Point(1, 203)
        Me.panExportar.Name = "panExportar"
        Me.panExportar.Size = New System.Drawing.Size(239, 42)
        Me.panExportar.Visible = False
        '
        'lbl_msExportar
        '
        Me.lbl_msExportar.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lbl_msExportar.Location = New System.Drawing.Point(12, 5)
        Me.lbl_msExportar.Name = "lbl_msExportar"
        Me.lbl_msExportar.Size = New System.Drawing.Size(218, 36)
        Me.lbl_msExportar.Text = "Escriba la direccion del servidor en la pestaña PARAMETROS"
        Me.lbl_msExportar.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmdExportar
        '
        Me.cmdExportar.Location = New System.Drawing.Point(0, 0)
        Me.cmdExportar.Name = "cmdExportar"
        Me.cmdExportar.Size = New System.Drawing.Size(240, 35)
        Me.cmdExportar.TabIndex = 1
        Me.cmdExportar.Text = "Exportar Datos"
        '
        'lstExport
        '
        Me.lstExport.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstExport.FullRowSelect = True
        Me.lstExport.LargeImageList = Me.imgList
        Me.lstExport.Location = New System.Drawing.Point(0, 51)
        Me.lstExport.Name = "lstExport"
        Me.lstExport.Size = New System.Drawing.Size(240, 193)
        Me.lstExport.SmallImageList = Me.imgList
        Me.lstExport.TabIndex = 5
        Me.lstExport.View = System.Windows.Forms.View.Details
        '
        'txtExp
        '
        Me.txtExp.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtExp.Location = New System.Drawing.Point(1, 173)
        Me.txtExp.Multiline = True
        Me.txtExp.Name = "txtExp"
        Me.txtExp.Size = New System.Drawing.Size(239, 44)
        Me.txtExp.TabIndex = 6
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.DateTimePicker2)
        Me.TabPage3.Controls.Add(Me.xoErrorStatament)
        Me.TabPage3.Controls.Add(Me.cmdRecarga)
        Me.TabPage3.Location = New System.Drawing.Point(0, 0)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(240, 245)
        Me.TabPage3.Text = "Recarga"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = ""
        Me.DateTimePicker2.Enabled = False
        Me.DateTimePicker2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker2.Location = New System.Drawing.Point(0, 34)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(240, 20)
        Me.DateTimePicker2.TabIndex = 7
        Me.DateTimePicker2.Value = New Date(2011, 12, 13, 0, 0, 0, 0)
        '
        'xoErrorStatament
        '
        Me.xoErrorStatament.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.xoErrorStatament.ForeColor = System.Drawing.Color.Maroon
        Me.xoErrorStatament.Location = New System.Drawing.Point(7, 75)
        Me.xoErrorStatament.Name = "xoErrorStatament"
        Me.xoErrorStatament.Size = New System.Drawing.Size(226, 166)
        Me.xoErrorStatament.Text = "--"
        '
        'cmdRecarga
        '
        Me.cmdRecarga.Location = New System.Drawing.Point(0, 0)
        Me.cmdRecarga.Name = "cmdRecarga"
        Me.cmdRecarga.Size = New System.Drawing.Size(240, 35)
        Me.cmdRecarga.TabIndex = 0
        Me.cmdRecarga.Text = "Recargar Producto"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.ComboBox1)
        Me.TabPage2.Controls.Add(Me.LinkLabel1)
        Me.TabPage2.Controls.Add(Me.chkEstoyFuera)
        Me.TabPage2.Controls.Add(Me.lnkRuta)
        Me.TabPage2.Controls.Add(Me.cmdTestc)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.lblServidor)
        Me.TabPage2.Controls.Add(Me.txtServidor)
        Me.TabPage2.Controls.Add(Me.lblRuta)
        Me.TabPage2.Controls.Add(Me.txtRuta)
        Me.TabPage2.Location = New System.Drawing.Point(0, 0)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(240, 245)
        Me.TabPage2.Text = "Parametros"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Location = New System.Drawing.Point(9, 11)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(71, 21)
        Me.LinkLabel1.TabIndex = 21
        Me.LinkLabel1.Text = "Sociedad"
        '
        'chkEstoyFuera
        '
        Me.chkEstoyFuera.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkEstoyFuera.ForeColor = System.Drawing.Color.Maroon
        Me.chkEstoyFuera.Location = New System.Drawing.Point(21, 198)
        Me.chkEstoyFuera.Name = "chkEstoyFuera"
        Me.chkEstoyFuera.Size = New System.Drawing.Size(210, 20)
        Me.chkEstoyFuera.TabIndex = 19
        Me.chkEstoyFuera.Text = "Estoy fuera de licorera"
        '
        'lnkRuta
        '
        Me.lnkRuta.BackColor = System.Drawing.Color.White
        Me.lnkRuta.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Underline)
        Me.lnkRuta.Location = New System.Drawing.Point(3, 230)
        Me.lnkRuta.Name = "lnkRuta"
        Me.lnkRuta.Size = New System.Drawing.Size(57, 14)
        Me.lnkRuta.TabIndex = 17
        Me.lnkRuta.Text = "MI RUTA"
        '
        'cmdTestc
        '
        Me.cmdTestc.Location = New System.Drawing.Point(93, 91)
        Me.cmdTestc.Name = "cmdTestc"
        Me.cmdTestc.Size = New System.Drawing.Size(140, 50)
        Me.cmdTestc.TabIndex = 5
        Me.cmdTestc.Text = "Probar conexion"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label3.ForeColor = System.Drawing.Color.Maroon
        Me.Label3.Location = New System.Drawing.Point(3, 144)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(230, 61)
        Me.Label3.Text = "Los campos para establecer el codigo de ruta y la IP del servidor  estan protegid" & _
            "os, de clic en el nombre para desbloquearlos."
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblServidor
        '
        Me.lblServidor.Location = New System.Drawing.Point(8, 64)
        Me.lblServidor.Name = "lblServidor"
        Me.lblServidor.Size = New System.Drawing.Size(79, 20)
        Me.lblServidor.TabIndex = 4
        Me.lblServidor.Text = "Servidor"
        '
        'txtServidor
        '
        Me.txtServidor.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.txtServidor.Enabled = False
        Me.txtServidor.Location = New System.Drawing.Point(93, 64)
        Me.txtServidor.Name = "txtServidor"
        Me.txtServidor.Size = New System.Drawing.Size(140, 21)
        Me.txtServidor.TabIndex = 3
        Me.txtServidor.Text = "192.9.2.86"
        '
        'lblRuta
        '
        Me.lblRuta.Location = New System.Drawing.Point(8, 37)
        Me.lblRuta.Name = "lblRuta"
        Me.lblRuta.Size = New System.Drawing.Size(79, 20)
        Me.lblRuta.TabIndex = 2
        Me.lblRuta.Text = "Ruta"
        '
        'txtRuta
        '
        Me.txtRuta.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.txtRuta.Enabled = False
        Me.txtRuta.Location = New System.Drawing.Point(93, 36)
        Me.txtRuta.Name = "txtRuta"
        Me.txtRuta.Size = New System.Drawing.Size(140, 21)
        Me.txtRuta.TabIndex = 1
        Me.txtRuta.Text = "184"
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(6, 4)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(16, 16)
        '
        'lblError
        '
        Me.lblError.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblError.Location = New System.Drawing.Point(12, 5)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(218, 18)
        Me.lblError.Text = "La clave ingresada no es correcta"
        Me.lblError.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Timer2
        '
        Me.Timer2.Interval = 1000
        '
        'ComboBox1
        '
        Me.ComboBox1.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.ComboBox1.Enabled = False
        Me.ComboBox1.Location = New System.Drawing.Point(91, 11)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(140, 21)
        Me.ComboBox1.TabIndex = 24
        Me.ComboBox1.Text = "4000"
        '
        'frmImportar_
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.ControlBox = False
        Me.Controls.Add(Me.TabControl1)
        Me.Menu = Me.menuImportar
        Me.MinimizeBox = False
        Me.Name = "frmImportar_"
        Me.Text = "Sincronizacion"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.pan_msImportar.ResumeLayout(False)
        Me.panFechaCarga.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.panExportar.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdImportar As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents cmdExportar As System.Windows.Forms.Button
    Friend WithEvents lstImport As System.Windows.Forms.ListView
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lblReloj As System.Windows.Forms.Label
    Friend WithEvents ProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents lblRuta As System.Windows.Forms.LinkLabel
    Friend WithEvents txtRuta As System.Windows.Forms.TextBox
    Friend WithEvents lblServidor As System.Windows.Forms.LinkLabel
    Friend WithEvents txtServidor As System.Windows.Forms.TextBox
    Friend WithEvents lstExport As System.Windows.Forms.ListView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdTestc As System.Windows.Forms.Button
    Friend WithEvents panExportar As System.Windows.Forms.Panel
    Friend WithEvents lbl_msExportar As System.Windows.Forms.Label
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents lblError As System.Windows.Forms.Label
    Friend WithEvents txtExp As System.Windows.Forms.TextBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents cmdRecarga As System.Windows.Forms.Button
    Friend WithEvents xoErrorStatament As System.Windows.Forms.Label
    Friend WithEvents lSoft As System.Windows.Forms.MenuItem
    Friend WithEvents panFechaCarga As System.Windows.Forms.Panel
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pan_msImportar As System.Windows.Forms.Panel
    Friend WithEvents lbl_msImportar As System.Windows.Forms.Label
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents lblRelojExp As System.Windows.Forms.Label
    Friend WithEvents lblEvento As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents lnkRuta As System.Windows.Forms.LinkLabel
    Friend WithEvents imgList As System.Windows.Forms.ImageList
    Friend WithEvents chkEstoyFuera As System.Windows.Forms.CheckBox
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents ComboBox1 As System.Windows.Forms.TextBox
End Class
