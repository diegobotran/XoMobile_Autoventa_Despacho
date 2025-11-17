<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmClientes
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
    Private menuClientes As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmClientes))
        Me.menuClientes = New System.Windows.Forms.MainMenu
        Me.lSoft = New System.Windows.Forms.MenuItem
        Me.lInfo = New System.Windows.Forms.MenuItem
        Me.lAtender = New System.Windows.Forms.MenuItem
        Me.cmVerTodos = New System.Windows.Forms.MenuItem
        Me.rSoft = New System.Windows.Forms.MenuItem
        Me.cmdLunes = New System.Windows.Forms.MenuItem
        Me.cmdMartes = New System.Windows.Forms.MenuItem
        Me.cmdMiercoles = New System.Windows.Forms.MenuItem
        Me.cmdJueves = New System.Windows.Forms.MenuItem
        Me.cmdViernes = New System.Windows.Forms.MenuItem
        Me.cmdSabado = New System.Windows.Forms.MenuItem
        Me.cmdDomingo = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtBuscar = New System.Windows.Forms.TextBox
        Me.lstClientes = New System.Windows.Forms.ListView
        Me.imgList = New System.Windows.Forms.ImageList
        Me.lstCase = New System.Windows.Forms.ComboBox
        Me.panError = New System.Windows.Forms.Panel
        Me.lblError = New System.Windows.Forms.Label
        Me.panError.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuClientes
        '
        Me.menuClientes.MenuItems.Add(Me.lSoft)
        Me.menuClientes.MenuItems.Add(Me.rSoft)
        Me.menuClientes.MenuItems.Add(Me.MenuItem1)
        '
        'lSoft
        '
        Me.lSoft.MenuItems.Add(Me.lInfo)
        Me.lSoft.MenuItems.Add(Me.lAtender)
        Me.lSoft.MenuItems.Add(Me.cmVerTodos)
        Me.lSoft.Text = "Menu"
        '
        'lInfo
        '
        Me.lInfo.Text = "Info"
        '
        'lAtender
        '
        Me.lAtender.Text = "Atender"
        '
        'cmVerTodos
        '
        Me.cmVerTodos.Text = "Ver todo el día"
        '
        'rSoft
        '
        Me.rSoft.MenuItems.Add(Me.cmdLunes)
        Me.rSoft.MenuItems.Add(Me.cmdMartes)
        Me.rSoft.MenuItems.Add(Me.cmdMiercoles)
        Me.rSoft.MenuItems.Add(Me.cmdJueves)
        Me.rSoft.MenuItems.Add(Me.cmdViernes)
        Me.rSoft.MenuItems.Add(Me.cmdSabado)
        Me.rSoft.MenuItems.Add(Me.cmdDomingo)
        Me.rSoft.Text = "Cambiar Dia"
        '
        'cmdLunes
        '
        Me.cmdLunes.Text = "Lunes"
        '
        'cmdMartes
        '
        Me.cmdMartes.Text = "Martes"
        '
        'cmdMiercoles
        '
        Me.cmdMiercoles.Text = "Miercoles"
        '
        'cmdJueves
        '
        Me.cmdJueves.Text = "Jueves"
        '
        'cmdViernes
        '
        Me.cmdViernes.Text = "Viernes"
        '
        'cmdSabado
        '
        Me.cmdSabado.Text = "Sabado"
        '
        'cmdDomingo
        '
        Me.cmdDomingo.Text = "Domingo"
        '
        'MenuItem1
        '
        Me.MenuItem1.Text = "Salir"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Calibri", 8.5!, System.Drawing.FontStyle.Regular)
        Me.Label1.Location = New System.Drawing.Point(8, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 36)
        Me.Label1.Text = "Buscar"
        '
        'txtBuscar
        '
        Me.txtBuscar.Location = New System.Drawing.Point(110, 6)
        Me.txtBuscar.Multiline = True
        Me.txtBuscar.Name = "txtBuscar"
        Me.txtBuscar.Size = New System.Drawing.Size(330, 35)
        Me.txtBuscar.TabIndex = 3
        '
        'lstClientes
        '
        Me.lstClientes.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lstClientes.FullRowSelect = True
        Me.lstClientes.LargeImageList = Me.imgList
        Me.lstClientes.Location = New System.Drawing.Point(0, 54)
        Me.lstClientes.Name = "lstClientes"
        Me.lstClientes.Size = New System.Drawing.Size(480, 420)
        Me.lstClientes.SmallImageList = Me.imgList
        Me.lstClientes.TabIndex = 10
        Me.lstClientes.View = System.Windows.Forms.View.Details
        '
        'imgList
        '
        Me.imgList.ImageSize = New System.Drawing.Size(30, 30)
        Me.imgList.Images.Clear()
        Me.imgList.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.imgList.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Image))
        Me.imgList.Images.Add(CType(resources.GetObject("resource2"), System.Drawing.Image))
        '
        'lstCase
        '
        Me.lstCase.Items.Add("ABARROTERIA")
        Me.lstCase.Items.Add("TIENDA")
        Me.lstCase.Items.Add("CANTINA")
        Me.lstCase.Items.Add("CHI")
        Me.lstCase.Items.Add("LA")
        Me.lstCase.Location = New System.Drawing.Point(110, 6)
        Me.lstCase.Name = "lstCase"
        Me.lstCase.Size = New System.Drawing.Size(364, 41)
        Me.lstCase.TabIndex = 15
        '
        'panError
        '
        Me.panError.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.panError.Controls.Add(Me.lblError)
        Me.panError.Location = New System.Drawing.Point(0, 474)
        Me.panError.Name = "panError"
        Me.panError.Size = New System.Drawing.Size(480, 62)
        Me.panError.Visible = False
        '
        'lblError
        '
        Me.lblError.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblError.Location = New System.Drawing.Point(2, 2)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(472, 54)
        Me.lblError.Text = "La clave ingresada no es correcta"
        Me.lblError.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmClientes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(192.0!, 192.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(480, 536)
        Me.ControlBox = False
        Me.Controls.Add(Me.panError)
        Me.Controls.Add(Me.lstClientes)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtBuscar)
        Me.Controls.Add(Me.lstCase)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(0, 52)
        Me.Menu = Me.menuClientes
        Me.Name = "frmClientes"
        Me.Text = "Clientes Ruta 23"
        Me.panError.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBuscar As System.Windows.Forms.TextBox
    Friend WithEvents lstClientes As System.Windows.Forms.ListView
    Friend WithEvents imgList As System.Windows.Forms.ImageList
    Friend WithEvents lstCase As System.Windows.Forms.ComboBox
    Friend WithEvents lSoft As System.Windows.Forms.MenuItem
    Friend WithEvents rSoft As System.Windows.Forms.MenuItem
    Friend WithEvents panError As System.Windows.Forms.Panel
    Friend WithEvents lblError As System.Windows.Forms.Label
    Friend WithEvents lAtender As System.Windows.Forms.MenuItem
    Friend WithEvents lInfo As System.Windows.Forms.MenuItem
    Friend WithEvents cmdLunes As System.Windows.Forms.MenuItem
    Friend WithEvents cmdMartes As System.Windows.Forms.MenuItem
    Friend WithEvents cmdMiercoles As System.Windows.Forms.MenuItem
    Friend WithEvents cmdJueves As System.Windows.Forms.MenuItem
    Friend WithEvents cmdViernes As System.Windows.Forms.MenuItem
    Friend WithEvents cmdSabado As System.Windows.Forms.MenuItem
    Friend WithEvents cmdDomingo As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents cmVerTodos As System.Windows.Forms.MenuItem
End Class
