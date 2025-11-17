<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmConsultaCarga
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
    Private menuCarga As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConsultaCarga))
        Me.menuCarga = New System.Windows.Forms.MainMenu
        Me.lSoft = New System.Windows.Forms.MenuItem
        Me.lstFechaCarga = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.chConfirmar = New System.Windows.Forms.CheckBox
        Me.lstCarga = New System.Windows.Forms.ListView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblRuta = New System.Windows.Forms.Label
        Me.lblTotalLitros = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.imgList = New System.Windows.Forms.ImageList
        Me.Label5 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuCarga
        '
        Me.menuCarga.MenuItems.Add(Me.lSoft)
        '
        'lSoft
        '
        Me.lSoft.Text = "Regresar"
        '
        'lstFechaCarga
        '
        Me.lstFechaCarga.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstFechaCarga.Items.Add("--- Seleccione ---")
        Me.lstFechaCarga.Location = New System.Drawing.Point(36, 7)
        Me.lstFechaCarga.Name = "lstFechaCarga"
        Me.lstFechaCarga.Size = New System.Drawing.Size(125, 24)
        Me.lstFechaCarga.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label2.Location = New System.Drawing.Point(1, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 15)
        Me.Label2.Text = "Fecha:"
        '
        'chConfirmar
        '
        Me.chConfirmar.Enabled = False
        Me.chConfirmar.Font = New System.Drawing.Font("Tahoma", 6.0!, System.Drawing.FontStyle.Regular)
        Me.chConfirmar.Location = New System.Drawing.Point(218, 9)
        Me.chConfirmar.Name = "chConfirmar"
        Me.chConfirmar.Size = New System.Drawing.Size(19, 20)
        Me.chConfirmar.TabIndex = 5
        '
        'lstCarga
        '
        Me.lstCarga.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lstCarga.FullRowSelect = True
        Me.lstCarga.Location = New System.Drawing.Point(0, 87)
        Me.lstCarga.Name = "lstCarga"
        Me.lstCarga.Size = New System.Drawing.Size(240, 166)
        Me.lstCarga.TabIndex = 6
        Me.lstCarga.View = System.Windows.Forms.View.Details
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Info
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.chConfirmar)
        Me.Panel1.Controls.Add(Me.lstFechaCarga)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(0, 55)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(240, 33)
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label4.Location = New System.Drawing.Point(165, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 19)
        Me.Label4.Text = "Confirmar"
        '
        'lblRuta
        '
        Me.lblRuta.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Regular)
        Me.lblRuta.ForeColor = System.Drawing.Color.Maroon
        Me.lblRuta.Location = New System.Drawing.Point(197, 7)
        Me.lblRuta.Name = "lblRuta"
        Me.lblRuta.Size = New System.Drawing.Size(37, 27)
        '
        'lblTotalLitros
        '
        Me.lblTotalLitros.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalLitros.BackColor = System.Drawing.Color.AliceBlue
        Me.lblTotalLitros.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalLitros.ForeColor = System.Drawing.Color.Maroon
        Me.lblTotalLitros.Location = New System.Drawing.Point(155, 253)
        Me.lblTotalLitros.Name = "lblTotalLitros"
        Me.lblTotalLitros.Size = New System.Drawing.Size(85, 15)
        Me.lblTotalLitros.Text = "0"
        Me.lblTotalLitros.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label3.BackColor = System.Drawing.Color.AliceBlue
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(0, 253)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(156, 15)
        Me.Label3.Text = "(Σ)   Litros:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.imgList.Images.Clear()
        Me.imgList.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.imgList.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Image))
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular)
        Me.Label5.Location = New System.Drawing.Point(-3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(194, 52)
        Me.Label5.Text = "Consulta de cargas y recargas"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.DarkRed
        Me.Panel2.Location = New System.Drawing.Point(0, 51)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(240, 3)
        '
        'frmConsultaCarga
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblRuta)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblTotalLitros)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lstCarga)
        Me.Menu = Me.menuCarga
        Me.Name = "frmConsultaCarga"
        Me.Text = "Consulta de carga"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstFechaCarga As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chConfirmar As System.Windows.Forms.CheckBox
    Friend WithEvents lstCarga As System.Windows.Forms.ListView
    Friend WithEvents lSoft As System.Windows.Forms.MenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblRuta As System.Windows.Forms.Label
    Friend WithEvents lblTotalLitros As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents imgList As System.Windows.Forms.ImageList
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
End Class
