<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class mPrincipal
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
        Me.components = New System.ComponentModel.Container
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem
        Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem
        Dim ListViewItem5 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem
        Dim ListViewItem6 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem
        Dim ListViewItem7 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem
        Dim ListViewItem8 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem
        Dim ListViewItem9 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem
        Dim ListViewItem10 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem
        Dim ListViewItem11 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem
        Dim ListViewItem12 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mPrincipal))
        Me.mainMenu1 = New System.Windows.Forms.MainMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.lstMenu = New System.Windows.Forms.ListView
        Me.ImageList1 = New System.Windows.Forms.ImageList
        Me.panLogin = New System.Windows.Forms.Panel
        Me.cmdCerrarLogin = New System.Windows.Forms.PictureBox
        Me.txtClave = New System.Windows.Forms.TextBox
        Me.lblSolicitaClave = New System.Windows.Forms.Label
        Me.panError = New System.Windows.Forms.Panel
        Me.lblError = New System.Windows.Forms.Label
        Me.InputPanel = New Microsoft.WindowsCE.Forms.InputPanel(Me.components)
        Me.panLogin.SuspendLayout()
        Me.panError.SuspendLayout()
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
        'lstMenu
        '
        Me.lstMenu.Activation = System.Windows.Forms.ItemActivation.TwoClick
        Me.lstMenu.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lstMenu.FullRowSelect = True
        ListViewItem1.ImageIndex = 3
        ListViewItem1.Tag = "0"
        ListViewItem1.Text = "Atencion al cliente"
        ListViewItem2.ImageIndex = 4
        ListViewItem2.Tag = "3"
        ListViewItem2.Text = "Consultas"
        ListViewItem3.ImageIndex = 8
        ListViewItem3.Tag = "4"
        ListViewItem3.Text = "Prueba Impresion"
        ListViewItem4.ImageIndex = 1
        ListViewItem4.Tag = "1"
        ListViewItem4.Text = "Liquidacion Bod. Envase"
        ListViewItem5.ImageIndex = 2
        ListViewItem5.Tag = "2"
        ListViewItem5.Text = "Liquidacion BPT"
        ListViewItem6.ImageIndex = 6
        ListViewItem6.Tag = "7"
        ListViewItem6.Text = "Datos Finales"
        ListViewItem7.ImageIndex = 7
        ListViewItem7.Tag = "8"
        ListViewItem7.Text = "Fin de dia"
        ListViewItem8.ImageIndex = 9
        ListViewItem8.Tag = "6"
        ListViewItem8.Text = "Recarga"
        ListViewItem9.ImageIndex = 5
        ListViewItem9.Tag = "5"
        ListViewItem9.Text = "Sincronizar"
        ListViewItem10.ImageIndex = 10
        ListViewItem10.Tag = "9"
        ListViewItem10.Text = "Configuracion"
        ListViewItem11.ImageIndex = 0
        ListViewItem11.Tag = "10"
        ListViewItem11.Text = "Correlativos"
        ListViewItem12.ImageIndex = 12
        ListViewItem12.Tag = "+cliente"
        ListViewItem12.Text = "+ Cliente"
        Me.lstMenu.Items.Add(ListViewItem1)
        Me.lstMenu.Items.Add(ListViewItem2)
        Me.lstMenu.Items.Add(ListViewItem3)
        Me.lstMenu.Items.Add(ListViewItem4)
        Me.lstMenu.Items.Add(ListViewItem5)
        Me.lstMenu.Items.Add(ListViewItem6)
        Me.lstMenu.Items.Add(ListViewItem7)
        Me.lstMenu.Items.Add(ListViewItem8)
        Me.lstMenu.Items.Add(ListViewItem9)
        Me.lstMenu.Items.Add(ListViewItem10)
        Me.lstMenu.Items.Add(ListViewItem11)
        Me.lstMenu.Items.Add(ListViewItem12)
        Me.lstMenu.LargeImageList = Me.ImageList1
        Me.lstMenu.Location = New System.Drawing.Point(0, 0)
        Me.lstMenu.Name = "lstMenu"
        Me.lstMenu.Size = New System.Drawing.Size(507, 565)
        Me.lstMenu.TabIndex = 10
        '
        'ImageList1
        '
        Me.ImageList1.ImageSize = New System.Drawing.Size(64, 64)
        Me.ImageList1.Images.Clear()
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Image))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource2"), System.Drawing.Image))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource3"), System.Drawing.Image))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource4"), System.Drawing.Image))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource5"), System.Drawing.Image))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource6"), System.Drawing.Image))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource7"), System.Drawing.Image))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource8"), System.Drawing.Image))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource9"), System.Drawing.Image))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource10"), System.Drawing.Image))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource11"), System.Drawing.Image))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource12"), System.Drawing.Image))
        '
        'panLogin
        '
        Me.panLogin.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.panLogin.Controls.Add(Me.cmdCerrarLogin)
        Me.panLogin.Controls.Add(Me.txtClave)
        Me.panLogin.Controls.Add(Me.lblSolicitaClave)
        Me.panLogin.Location = New System.Drawing.Point(3, 285)
        Me.panLogin.Name = "panLogin"
        Me.panLogin.Size = New System.Drawing.Size(474, 102)
        Me.panLogin.Visible = False
        '
        'cmdCerrarLogin
        '
        Me.cmdCerrarLogin.BackColor = System.Drawing.Color.Transparent
        Me.cmdCerrarLogin.Image = CType(resources.GetObject("cmdCerrarLogin.Image"), System.Drawing.Image)
        Me.cmdCerrarLogin.Location = New System.Drawing.Point(435, 7)
        Me.cmdCerrarLogin.Name = "cmdCerrarLogin"
        Me.cmdCerrarLogin.Size = New System.Drawing.Size(32, 34)
        '
        'txtClave
        '
        Me.txtClave.ForeColor = System.Drawing.Color.Maroon
        Me.txtClave.Location = New System.Drawing.Point(5, 47)
        Me.txtClave.Name = "txtClave"
        Me.txtClave.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtClave.Size = New System.Drawing.Size(463, 41)
        Me.txtClave.TabIndex = 1
        '
        'lblSolicitaClave
        '
        Me.lblSolicitaClave.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblSolicitaClave.Location = New System.Drawing.Point(5, 6)
        Me.lblSolicitaClave.Name = "lblSolicitaClave"
        Me.lblSolicitaClave.Size = New System.Drawing.Size(424, 43)
        Me.lblSolicitaClave.Text = "Clave de acceso:"
        '
        'panError
        '
        Me.panError.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.panError.Controls.Add(Me.lblError)
        Me.panError.Location = New System.Drawing.Point(0, 444)
        Me.panError.Name = "panError"
        Me.panError.Size = New System.Drawing.Size(470, 88)
        Me.panError.Visible = False
        '
        'lblError
        '
        Me.lblError.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblError.Location = New System.Drawing.Point(39, 12)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(393, 66)
        Me.lblError.Text = "La clave ingresada no es correcta"
        Me.lblError.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'mPrincipal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(192.0!, 192.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(480, 536)
        Me.ControlBox = False
        Me.Controls.Add(Me.panLogin)
        Me.Controls.Add(Me.panError)
        Me.Controls.Add(Me.lstMenu)
        Me.Location = New System.Drawing.Point(0, 52)
        Me.Menu = Me.mainMenu1
        Me.Name = "mPrincipal"
        Me.Text = "Principal"
        Me.panLogin.ResumeLayout(False)
        Me.panError.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panLogin As System.Windows.Forms.Panel
    Friend WithEvents lblSolicitaClave As System.Windows.Forms.Label
    Friend WithEvents txtClave As System.Windows.Forms.TextBox
    Friend WithEvents panError As System.Windows.Forms.Panel
    Friend WithEvents lblError As System.Windows.Forms.Label
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents cmdCerrarLogin As System.Windows.Forms.PictureBox
    Friend WithEvents InputPanel As Microsoft.WindowsCE.Forms.InputPanel
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents lstMenu As System.Windows.Forms.ListView
End Class
