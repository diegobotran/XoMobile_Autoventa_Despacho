<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmDocumentosEmitidos
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
    Private menuDocumentos As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDocumentosEmitidos))
        Me.menuDocumentos = New System.Windows.Forms.MainMenu
        Me.cmdSalir = New System.Windows.Forms.MenuItem
        Me.cmdAnular = New System.Windows.Forms.MenuItem
        Me.cmdAnulaConReimpresion = New System.Windows.Forms.MenuItem
        Me.cmdSoloAnular = New System.Windows.Forms.MenuItem
        Me.cmdImprimir = New System.Windows.Forms.MenuItem
        Me.cmdImprimeDocto = New System.Windows.Forms.MenuItem
        Me.cmdImprimeReporte = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MenuFEL = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.tabDocumentos = New System.Windows.Forms.TabControl
        Me.tFact = New System.Windows.Forms.TabPage
        Me.lstFacturas = New System.Windows.Forms.ListView
        Me.imgListAppl = New System.Windows.Forms.ImageList
        Me.tRec = New System.Windows.Forms.TabPage
        Me.panClave = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtClave = New System.Windows.Forms.TextBox
        Me.lstRecibos = New System.Windows.Forms.ListView
        Me.tNc = New System.Windows.Forms.TabPage
        Me.lstNotaCredito = New System.Windows.Forms.ListView
        Me.tRm = New System.Windows.Forms.TabPage
        Me.lstResumenMarca = New System.Windows.Forms.ListView
        Me.tCm = New System.Windows.Forms.TabPage
        Me.lstCambio = New System.Windows.Forms.ListView
        Me.tNA = New System.Windows.Forms.TabPage
        Me.lstNotaAbono = New System.Windows.Forms.ListView
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.lblError = New System.Windows.Forms.Label
        Me.panError = New System.Windows.Forms.Panel
        Me.lblEmensaje = New System.Windows.Forms.Label
        Me.tabDocumentos.SuspendLayout()
        Me.tFact.SuspendLayout()
        Me.tRec.SuspendLayout()
        Me.panClave.SuspendLayout()
        Me.tNc.SuspendLayout()
        Me.tRm.SuspendLayout()
        Me.tCm.SuspendLayout()
        Me.tNA.SuspendLayout()
        Me.panError.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuDocumentos
        '
        Me.menuDocumentos.MenuItems.Add(Me.cmdSalir)
        Me.menuDocumentos.MenuItems.Add(Me.cmdAnular)
        Me.menuDocumentos.MenuItems.Add(Me.cmdImprimir)
        '
        'cmdSalir
        '
        Me.cmdSalir.Text = "Salir"
        '
        'cmdAnular
        '
        Me.cmdAnular.MenuItems.Add(Me.cmdAnulaConReimpresion)
        Me.cmdAnular.MenuItems.Add(Me.cmdSoloAnular)
        Me.cmdAnular.Text = "      Anulacion"
        '
        'cmdAnulaConReimpresion
        '
        Me.cmdAnulaConReimpresion.Enabled = False
        Me.cmdAnulaConReimpresion.Text = "Con reimpresion"
        '
        'cmdSoloAnular
        '
        Me.cmdSoloAnular.Text = "Solo anular"
        '
        'cmdImprimir
        '
        Me.cmdImprimir.MenuItems.Add(Me.cmdImprimeDocto)
        Me.cmdImprimir.MenuItems.Add(Me.cmdImprimeReporte)
        Me.cmdImprimir.MenuItems.Add(Me.MenuItem1)
        Me.cmdImprimir.MenuItems.Add(Me.MenuFEL)
        Me.cmdImprimir.MenuItems.Add(Me.MenuItem2)
        Me.cmdImprimir.MenuItems.Add(Me.MenuItem3)
        Me.cmdImprimir.Text = "      Imprimir"
        '
        'cmdImprimeDocto
        '
        Me.cmdImprimeDocto.Checked = True
        Me.cmdImprimeDocto.Text = "Documento seleccionado"
        '
        'cmdImprimeReporte
        '
        Me.cmdImprimeReporte.Text = "Reporte de Docs emitidos"
        '
        'MenuItem1
        '
        Me.MenuItem1.Text = "Resumen por Marca"
        '
        'MenuFEL
        '
        Me.MenuFEL.Text = "Generar FEL"
        '
        'MenuItem2
        '
        Me.MenuItem2.Text = "Generar Contingencia"
        '
        'MenuItem3
        '
        Me.MenuItem3.Text = "Reporte Docto SIN FEL"
        '
        'tabDocumentos
        '
        Me.tabDocumentos.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabDocumentos.Controls.Add(Me.tFact)
        Me.tabDocumentos.Controls.Add(Me.tRec)
        Me.tabDocumentos.Controls.Add(Me.tNc)
        Me.tabDocumentos.Controls.Add(Me.tRm)
        Me.tabDocumentos.Controls.Add(Me.tCm)
        Me.tabDocumentos.Controls.Add(Me.tNA)
        Me.tabDocumentos.Dock = System.Windows.Forms.DockStyle.None
        Me.tabDocumentos.Location = New System.Drawing.Point(0, 45)
        Me.tabDocumentos.Name = "tabDocumentos"
        Me.tabDocumentos.SelectedIndex = 0
        Me.tabDocumentos.Size = New System.Drawing.Size(240, 223)
        Me.tabDocumentos.TabIndex = 0
        '
        'tFact
        '
        Me.tFact.Controls.Add(Me.lstFacturas)
        Me.tFact.Location = New System.Drawing.Point(0, 0)
        Me.tFact.Name = "tFact"
        Me.tFact.Size = New System.Drawing.Size(240, 200)
        Me.tFact.Text = "FACT"
        '
        'lstFacturas
        '
        Me.lstFacturas.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstFacturas.FullRowSelect = True
        Me.lstFacturas.Location = New System.Drawing.Point(0, 0)
        Me.lstFacturas.Name = "lstFacturas"
        Me.lstFacturas.Size = New System.Drawing.Size(240, 200)
        Me.lstFacturas.SmallImageList = Me.imgListAppl
        Me.lstFacturas.TabIndex = 3
        Me.lstFacturas.View = System.Windows.Forms.View.Details
        Me.imgListAppl.Images.Clear()
        Me.imgListAppl.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.imgListAppl.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Image))
        Me.imgListAppl.Images.Add(CType(resources.GetObject("resource2"), System.Drawing.Image))
        '
        'tRec
        '
        Me.tRec.AutoScroll = True
        Me.tRec.Controls.Add(Me.panClave)
        Me.tRec.Controls.Add(Me.lstRecibos)
        Me.tRec.Location = New System.Drawing.Point(0, 0)
        Me.tRec.Name = "tRec"
        Me.tRec.Size = New System.Drawing.Size(232, 197)
        Me.tRec.Text = "REC"
        '
        'panClave
        '
        Me.panClave.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.panClave.Controls.Add(Me.Label3)
        Me.panClave.Controls.Add(Me.txtClave)
        Me.panClave.Location = New System.Drawing.Point(7, 87)
        Me.panClave.Name = "panClave"
        Me.panClave.Size = New System.Drawing.Size(226, 49)
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
        'lstRecibos
        '
        Me.lstRecibos.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstRecibos.FullRowSelect = True
        Me.lstRecibos.Location = New System.Drawing.Point(0, 0)
        Me.lstRecibos.Name = "lstRecibos"
        Me.lstRecibos.Size = New System.Drawing.Size(240, 200)
        Me.lstRecibos.SmallImageList = Me.imgListAppl
        Me.lstRecibos.TabIndex = 4
        Me.lstRecibos.View = System.Windows.Forms.View.Details
        '
        'tNc
        '
        Me.tNc.Controls.Add(Me.lstNotaCredito)
        Me.tNc.Location = New System.Drawing.Point(0, 0)
        Me.tNc.Name = "tNc"
        Me.tNc.Size = New System.Drawing.Size(232, 197)
        Me.tNc.Text = "NC"
        '
        'lstNotaCredito
        '
        Me.lstNotaCredito.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstNotaCredito.FullRowSelect = True
        Me.lstNotaCredito.Location = New System.Drawing.Point(0, 0)
        Me.lstNotaCredito.Name = "lstNotaCredito"
        Me.lstNotaCredito.Size = New System.Drawing.Size(240, 200)
        Me.lstNotaCredito.SmallImageList = Me.imgListAppl
        Me.lstNotaCredito.TabIndex = 5
        Me.lstNotaCredito.View = System.Windows.Forms.View.Details
        '
        'tRm
        '
        Me.tRm.Controls.Add(Me.lstResumenMarca)
        Me.tRm.Location = New System.Drawing.Point(0, 0)
        Me.tRm.Name = "tRm"
        Me.tRm.Size = New System.Drawing.Size(232, 197)
        Me.tRm.Text = "RM"
        '
        'lstResumenMarca
        '
        Me.lstResumenMarca.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstResumenMarca.FullRowSelect = True
        Me.lstResumenMarca.Location = New System.Drawing.Point(-2, -4)
        Me.lstResumenMarca.Name = "lstResumenMarca"
        Me.lstResumenMarca.Size = New System.Drawing.Size(244, 208)
        Me.lstResumenMarca.TabIndex = 7
        Me.lstResumenMarca.View = System.Windows.Forms.View.Details
        '
        'tCm
        '
        Me.tCm.Controls.Add(Me.lstCambio)
        Me.tCm.Location = New System.Drawing.Point(0, 0)
        Me.tCm.Name = "tCm"
        Me.tCm.Size = New System.Drawing.Size(232, 197)
        Me.tCm.Text = "CAM"
        '
        'lstCambio
        '
        Me.lstCambio.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstCambio.FullRowSelect = True
        Me.lstCambio.Location = New System.Drawing.Point(0, 0)
        Me.lstCambio.Name = "lstCambio"
        Me.lstCambio.Size = New System.Drawing.Size(240, 200)
        Me.lstCambio.SmallImageList = Me.imgListAppl
        Me.lstCambio.TabIndex = 6
        Me.lstCambio.View = System.Windows.Forms.View.Details
        '
        'tNA
        '
        Me.tNA.Controls.Add(Me.lstNotaAbono)
        Me.tNA.Location = New System.Drawing.Point(0, 0)
        Me.tNA.Name = "tNA"
        Me.tNA.Size = New System.Drawing.Size(232, 197)
        Me.tNA.Text = "NA"
        '
        'lstNotaAbono
        '
        Me.lstNotaAbono.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstNotaAbono.FullRowSelect = True
        Me.lstNotaAbono.Location = New System.Drawing.Point(3, 1)
        Me.lstNotaAbono.Name = "lstNotaAbono"
        Me.lstNotaAbono.Size = New System.Drawing.Size(240, 200)
        Me.lstNotaAbono.SmallImageList = Me.imgListAppl
        Me.lstNotaAbono.TabIndex = 6
        Me.lstNotaAbono.View = System.Windows.Forms.View.Details
        '
        'lblTitulo
        '
        Me.lblTitulo.Font = New System.Drawing.Font("Trebuchet MS", 13.0!, System.Drawing.FontStyle.Regular)
        Me.lblTitulo.Location = New System.Drawing.Point(3, 0)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(106, 42)
        Me.lblTitulo.Text = "Documentos emitidos."
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
        Me.lblError.Location = New System.Drawing.Point(13, 7)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(218, 20)
        Me.lblError.Text = "La clave ingresada no es correcta"
        Me.lblError.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'panError
        '
        Me.panError.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.panError.Controls.Add(Me.lblEmensaje)
        Me.panError.Location = New System.Drawing.Point(7, 183)
        Me.panError.Name = "panError"
        Me.panError.Size = New System.Drawing.Size(226, 29)
        Me.panError.Visible = False
        '
        'lblEmensaje
        '
        Me.lblEmensaje.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblEmensaje.Location = New System.Drawing.Point(34, 1)
        Me.lblEmensaje.Name = "lblEmensaje"
        Me.lblEmensaje.Size = New System.Drawing.Size(162, 27)
        Me.lblEmensaje.Text = "La anulacion precisa una clave de liquidacion"
        Me.lblEmensaje.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmDocumentosEmitidos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.ControlBox = False
        Me.Controls.Add(Me.panError)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.tabDocumentos)
        Me.KeyPreview = True
        Me.Menu = Me.menuDocumentos
        Me.Name = "frmDocumentosEmitidos"
        Me.Text = "Documentos"
        Me.tabDocumentos.ResumeLayout(False)
        Me.tFact.ResumeLayout(False)
        Me.tRec.ResumeLayout(False)
        Me.panClave.ResumeLayout(False)
        Me.tNc.ResumeLayout(False)
        Me.tRm.ResumeLayout(False)
        Me.tCm.ResumeLayout(False)
        Me.tNA.ResumeLayout(False)
        Me.panError.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tabDocumentos As System.Windows.Forms.TabControl
    Friend WithEvents tFact As System.Windows.Forms.TabPage
    Friend WithEvents tRec As System.Windows.Forms.TabPage
    Friend WithEvents tNc As System.Windows.Forms.TabPage
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents lstFacturas As System.Windows.Forms.ListView
    Friend WithEvents lstRecibos As System.Windows.Forms.ListView
    Friend WithEvents lstNotaCredito As System.Windows.Forms.ListView
    Friend WithEvents imgListAppl As System.Windows.Forms.ImageList
    Friend WithEvents cmdAnular As System.Windows.Forms.MenuItem
    Friend WithEvents cmdImprimir As System.Windows.Forms.MenuItem
    Friend WithEvents cmdImprimeDocto As System.Windows.Forms.MenuItem
    Friend WithEvents cmdImprimeReporte As System.Windows.Forms.MenuItem
    Friend WithEvents cmdSalir As System.Windows.Forms.MenuItem
    Friend WithEvents panClave As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtClave As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents lblError As System.Windows.Forms.Label
    Friend WithEvents panError As System.Windows.Forms.Panel
    Friend WithEvents lblEmensaje As System.Windows.Forms.Label
    Friend WithEvents cmdAnulaConReimpresion As System.Windows.Forms.MenuItem
    Friend WithEvents cmdSoloAnular As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents tRm As System.Windows.Forms.TabPage
    Friend WithEvents lstResumenMarca As System.Windows.Forms.ListView
    Friend WithEvents tCm As System.Windows.Forms.TabPage
    Friend WithEvents lstCambio As System.Windows.Forms.ListView
    Friend WithEvents MenuFEL As System.Windows.Forms.MenuItem
    Friend WithEvents tNA As System.Windows.Forms.TabPage
    Friend WithEvents lstNotaAbono As System.Windows.Forms.ListView
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
End Class
