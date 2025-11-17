<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class mAtencion
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
    Private menuAttCliente As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mAtencion))
        Me.menuAttCliente = New System.Windows.Forms.MainMenu
        Me.RSoftSalir = New System.Windows.Forms.MenuItem
        Me.lSoftSeleccionar = New System.Windows.Forms.MenuItem
        Me.lstMenu = New System.Windows.Forms.ListView
        Me.lstItemImages = New System.Windows.Forms.ImageList
        Me.panAtencion = New System.Windows.Forms.Panel
        Me.lstRazonAtencion = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lstItemsImagesR = New System.Windows.Forms.ImageList
        Me.panAtencion.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuAttCliente
        '
        Me.menuAttCliente.MenuItems.Add(Me.RSoftSalir)
        Me.menuAttCliente.MenuItems.Add(Me.lSoftSeleccionar)
        '
        'RSoftSalir
        '
        Me.RSoftSalir.Text = "Regresar"
        '
        'lSoftSeleccionar
        '
        Me.lSoftSeleccionar.Text = "Seleccionar"
        '
        'lstMenu
        '
        Me.lstMenu.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lstMenu.FullRowSelect = True
        Me.lstMenu.Location = New System.Drawing.Point(0, 0)
        Me.lstMenu.Name = "lstMenu"
        Me.lstMenu.Size = New System.Drawing.Size(477, 533)
        Me.lstMenu.SmallImageList = Me.lstItemImages
        Me.lstMenu.TabIndex = 17
        Me.lstMenu.View = System.Windows.Forms.View.List
        '
        'lstItemImages
        '
        Me.lstItemImages.ImageSize = New System.Drawing.Size(64, 64)
        '
        'panAtencion
        '
        Me.panAtencion.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.panAtencion.Controls.Add(Me.lstRazonAtencion)
        Me.panAtencion.Controls.Add(Me.Label3)
        Me.panAtencion.Location = New System.Drawing.Point(0, 161)
        Me.panAtencion.Name = "panAtencion"
        Me.panAtencion.Size = New System.Drawing.Size(477, 141)
        Me.panAtencion.Visible = False
        '
        'lstRazonAtencion
        '
        Me.lstRazonAtencion.Location = New System.Drawing.Point(1, 87)
        Me.lstRazonAtencion.Name = "lstRazonAtencion"
        Me.lstRazonAtencion.Size = New System.Drawing.Size(473, 41)
        Me.lstRazonAtencion.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(2, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(472, 82)
        Me.Label3.Text = "Seleccione la Razon de no atencion y presione <ENTER>"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lstItemsImagesR
        '
        Me.lstItemsImagesR.ImageSize = New System.Drawing.Size(64, 64)
        Me.lstItemsImagesR.Images.Clear()
        Me.lstItemsImagesR.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.lstItemsImagesR.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Image))
        '
        'mAtencion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(192.0!, 192.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(480, 536)
        Me.ControlBox = False
        Me.Controls.Add(Me.panAtencion)
        Me.Controls.Add(Me.lstMenu)
        Me.Location = New System.Drawing.Point(0, 52)
        Me.Menu = Me.menuAttCliente
        Me.Name = "mAtencion"
        Me.Text = "Atencion al cliente"
        Me.panAtencion.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RSoftSalir As System.Windows.Forms.MenuItem
    Friend WithEvents lSoftSeleccionar As System.Windows.Forms.MenuItem
    Friend WithEvents lstMenu As System.Windows.Forms.ListView
    Friend WithEvents panAtencion As System.Windows.Forms.Panel
    Friend WithEvents lstRazonAtencion As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lstItemsImagesR As System.Windows.Forms.ImageList
    Friend WithEvents lstItemImages As System.Windows.Forms.ImageList
End Class
