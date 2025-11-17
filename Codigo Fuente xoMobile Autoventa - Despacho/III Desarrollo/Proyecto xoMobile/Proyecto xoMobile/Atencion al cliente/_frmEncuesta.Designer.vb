<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmEncuesta
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
    Private menuEncuesta As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEncuesta))
        Me.menuEncuesta = New System.Windows.Forms.MainMenu
        Me.lSoft = New System.Windows.Forms.MenuItem
        Me.rSoft = New System.Windows.Forms.MenuItem
        Me.lstEncuestas = New System.Windows.Forms.ListView
        Me.imgListAppl = New System.Windows.Forms.ImageList
        Me.panTiempo = New System.Windows.Forms.Panel
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.panTiempo.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuEncuesta
        '
        Me.menuEncuesta.MenuItems.Add(Me.lSoft)
        Me.menuEncuesta.MenuItems.Add(Me.rSoft)
        '
        'lSoft
        '
        Me.lSoft.Text = "Salir"
        '
        'rSoft
        '
        Me.rSoft.Text = "Aceptar"
        '
        'lstEncuestas
        '
        Me.lstEncuestas.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
        Me.lstEncuestas.FullRowSelect = True
        Me.lstEncuestas.LargeImageList = Me.imgListAppl
        Me.lstEncuestas.Location = New System.Drawing.Point(0, 55)
        Me.lstEncuestas.Name = "lstEncuestas"
        Me.lstEncuestas.Size = New System.Drawing.Size(240, 213)
        Me.lstEncuestas.SmallImageList = Me.imgListAppl
        Me.lstEncuestas.TabIndex = 4
        Me.lstEncuestas.View = System.Windows.Forms.View.Details
        '
        'imgListAppl
        '
        Me.imgListAppl.ImageSize = New System.Drawing.Size(24, 24)
        Me.imgListAppl.Images.Clear()
        Me.imgListAppl.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.imgListAppl.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Image))
        '
        'panTiempo
        '
        Me.panTiempo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.panTiempo.Controls.Add(Me.lblTitulo)
        Me.panTiempo.Location = New System.Drawing.Point(0, 0)
        Me.panTiempo.Name = "panTiempo"
        Me.panTiempo.Size = New System.Drawing.Size(240, 55)
        '
        'lblTitulo
        '
        Me.lblTitulo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.lblTitulo.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(89, Byte), Integer), CType(CType(152, Byte), Integer))
        Me.lblTitulo.Location = New System.Drawing.Point(28, 8)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(189, 35)
        Me.lblTitulo.Text = "Seleccione una Encuesta"
        Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmEncuesta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.ControlBox = False
        Me.Controls.Add(Me.lstEncuestas)
        Me.Controls.Add(Me.panTiempo)
        Me.Menu = Me.menuEncuesta
        Me.Name = "frmEncuesta"
        Me.Text = "Encuesta"
        Me.panTiempo.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstEncuestas As System.Windows.Forms.ListView
    Friend WithEvents panTiempo As System.Windows.Forms.Panel
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents lSoft As System.Windows.Forms.MenuItem
    Friend WithEvents rSoft As System.Windows.Forms.MenuItem
    Friend WithEvents imgListAppl As System.Windows.Forms.ImageList
End Class
