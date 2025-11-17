<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmPresupuesto
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
    Private menuClientesSaldo As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPresupuesto))
        Me.menuClientesSaldo = New System.Windows.Forms.MainMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.mnuImprimir = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.imgListAppl = New System.Windows.Forms.ImageList
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.tabDocumentos = New System.Windows.Forms.TabControl
        Me.txtClave = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.tRec = New System.Windows.Forms.TabPage
        Me.tFact = New System.Windows.Forms.TabPage
        Me.lstPresupuesto = New System.Windows.Forms.ListView
        Me.lstPresupuestoV = New System.Windows.Forms.ListView
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.MenuItem5 = New System.Windows.Forms.MenuItem
        Me.tabDocumentos.SuspendLayout()
        Me.tRec.SuspendLayout()
        Me.tFact.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuClientesSaldo
        '
        Me.menuClientesSaldo.MenuItems.Add(Me.MenuItem1)
        Me.menuClientesSaldo.MenuItems.Add(Me.mnuImprimir)
        '
        'MenuItem1
        '
        Me.MenuItem1.Text = "Cancelar"
        '
        'mnuImprimir
        '
        Me.mnuImprimir.MenuItems.Add(Me.MenuItem2)
        Me.mnuImprimir.MenuItems.Add(Me.MenuItem3)
        Me.mnuImprimir.MenuItems.Add(Me.MenuItem4)
        Me.mnuImprimir.MenuItems.Add(Me.MenuItem5)
        Me.mnuImprimir.Text = " Imprimir"
        '
        'MenuItem2
        '
        Me.MenuItem2.Text = "Ppto. Litros"
        '
        'MenuItem3
        '
        Me.MenuItem3.Text = "Ppto. Valorizado"
        Me.imgListAppl.Images.Clear()
        Me.imgListAppl.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.imgListAppl.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Image))
        Me.imgListAppl.Images.Add(CType(resources.GetObject("resource2"), System.Drawing.Image))
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(1, 1)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(236, 22)
        Me.DateTimePicker1.TabIndex = 4
        '
        'tabDocumentos
        '
        Me.tabDocumentos.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabDocumentos.Controls.Add(Me.tFact)
        Me.tabDocumentos.Controls.Add(Me.tRec)
        Me.tabDocumentos.Dock = System.Windows.Forms.DockStyle.None
        Me.tabDocumentos.Location = New System.Drawing.Point(0, 29)
        Me.tabDocumentos.Name = "tabDocumentos"
        Me.tabDocumentos.SelectedIndex = 0
        Me.tabDocumentos.Size = New System.Drawing.Size(240, 239)
        Me.tabDocumentos.TabIndex = 5
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
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.Label3.Location = New System.Drawing.Point(4, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 15)
        '
        'tRec
        '
        Me.tRec.AutoScroll = True
        Me.tRec.Controls.Add(Me.lstPresupuestoV)
        Me.tRec.Location = New System.Drawing.Point(0, 0)
        Me.tRec.Name = "tRec"
        Me.tRec.Size = New System.Drawing.Size(240, 216)
        Me.tRec.Text = "VALORIZADO"
        '
        'tFact
        '
        Me.tFact.Controls.Add(Me.lstPresupuesto)
        Me.tFact.Location = New System.Drawing.Point(0, 0)
        Me.tFact.Name = "tFact"
        Me.tFact.Size = New System.Drawing.Size(240, 216)
        Me.tFact.Text = "LITROS"
        '
        'lstPresupuesto
        '
        Me.lstPresupuesto.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lstPresupuesto.FullRowSelect = True
        Me.lstPresupuesto.LargeImageList = Me.imgListAppl
        Me.lstPresupuesto.Location = New System.Drawing.Point(5, 2)
        Me.lstPresupuesto.Name = "lstPresupuesto"
        Me.lstPresupuesto.Size = New System.Drawing.Size(230, 213)
        Me.lstPresupuesto.SmallImageList = Me.imgListAppl
        Me.lstPresupuesto.TabIndex = 4
        Me.lstPresupuesto.View = System.Windows.Forms.View.Details
        '
        'lstPresupuestoV
        '
        Me.lstPresupuestoV.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lstPresupuestoV.FullRowSelect = True
        Me.lstPresupuestoV.LargeImageList = Me.imgListAppl
        Me.lstPresupuestoV.Location = New System.Drawing.Point(5, 2)
        Me.lstPresupuestoV.Name = "lstPresupuestoV"
        Me.lstPresupuestoV.Size = New System.Drawing.Size(230, 213)
        Me.lstPresupuestoV.SmallImageList = Me.imgListAppl
        Me.lstPresupuestoV.TabIndex = 5
        Me.lstPresupuestoV.View = System.Windows.Forms.View.Details
        '
        'MenuItem4
        '
        Me.MenuItem4.Text = "Ppto. Litros Dia"
        '
        'MenuItem5
        '
        Me.MenuItem5.Text = "Ppto. Valores Dia"
        '
        'frmPresupuesto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.ControlBox = False
        Me.Controls.Add(Me.tabDocumentos)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Menu = Me.menuClientesSaldo
        Me.Name = "frmPresupuesto"
        Me.Text = "Ppto. Ruta"
        Me.tabDocumentos.ResumeLayout(False)
        Me.tRec.ResumeLayout(False)
        Me.tFact.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuImprimir As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents imgListAppl As System.Windows.Forms.ImageList
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents tabDocumentos As System.Windows.Forms.TabControl
    Friend WithEvents tFact As System.Windows.Forms.TabPage
    Friend WithEvents tRec As System.Windows.Forms.TabPage
    Friend WithEvents txtClave As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lstPresupuesto As System.Windows.Forms.ListView
    Friend WithEvents lstPresupuestoV As System.Windows.Forms.ListView
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
End Class
