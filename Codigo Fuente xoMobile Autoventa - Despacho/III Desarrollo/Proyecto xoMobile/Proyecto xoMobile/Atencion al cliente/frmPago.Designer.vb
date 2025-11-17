<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmPago
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
    Private menuPago As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.menuPago = New System.Windows.Forms.MainMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.lsoftAtras = New System.Windows.Forms.MenuItem
        Me.lsoftCancelar = New System.Windows.Forms.MenuItem
        Me.rSoft = New System.Windows.Forms.MenuItem
        Me.dgFormasPago = New System.Windows.Forms.DataGrid
        Me.dgFormasPagoStyle = New System.Windows.Forms.DataGridTableStyle
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblTotalPagar = New System.Windows.Forms.Label
        Me.panSaldo = New System.Windows.Forms.Panel
        Me.txtSaldo = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.panCobros = New System.Windows.Forms.Panel
        Me.txtDescuento = New System.Windows.Forms.Label
        Me.txtEnvase = New System.Windows.Forms.Label
        Me.txtTotal = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblNumero = New System.Windows.Forms.Label
        Me.lblSerie = New System.Windows.Forms.Label
        Me.lblNegocio = New System.Windows.Forms.Label
        Me.lblAtencion = New System.Windows.Forms.Label
        Me.panSaldo.SuspendLayout()
        Me.panCobros.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuPago
        '
        Me.menuPago.MenuItems.Add(Me.MenuItem1)
        Me.menuPago.MenuItems.Add(Me.rSoft)
        '
        'MenuItem1
        '
        Me.MenuItem1.MenuItems.Add(Me.lsoftAtras)
        Me.MenuItem1.MenuItems.Add(Me.lsoftCancelar)
        Me.MenuItem1.Text = "Opciones"
        '
        'lsoftAtras
        '
        Me.lsoftAtras.Text = "Atras"
        '
        'lsoftCancelar
        '
        Me.lsoftCancelar.Text = "Cancelar"
        '
        'rSoft
        '
        Me.rSoft.Text = "Pagar"
        '
        'dgFormasPago
        '
        Me.dgFormasPago.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dgFormasPago.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Regular)
        Me.dgFormasPago.Location = New System.Drawing.Point(0, 265)
        Me.dgFormasPago.Name = "dgFormasPago"
        Me.dgFormasPago.PreferredRowHeight = 18
        Me.dgFormasPago.RowHeadersVisible = False
        Me.dgFormasPago.Size = New System.Drawing.Size(480, 221)
        Me.dgFormasPago.TabIndex = 0
        Me.dgFormasPago.TableStyles.Add(Me.dgFormasPagoStyle)
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 212)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(223, 40)
        Me.Label1.Text = "Total a pagar:"
        '
        'lblTotalPagar
        '
        Me.lblTotalPagar.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalPagar.Location = New System.Drawing.Point(316, 216)
        Me.lblTotalPagar.Name = "lblTotalPagar"
        Me.lblTotalPagar.Size = New System.Drawing.Size(164, 36)
        Me.lblTotalPagar.Tag = "1500.25"
        Me.lblTotalPagar.Text = "#,###.##"
        Me.lblTotalPagar.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'panSaldo
        '
        Me.panSaldo.BackColor = System.Drawing.SystemColors.MenuText
        Me.panSaldo.Controls.Add(Me.txtSaldo)
        Me.panSaldo.Controls.Add(Me.Label8)
        Me.panSaldo.Location = New System.Drawing.Point(0, 486)
        Me.panSaldo.Name = "panSaldo"
        Me.panSaldo.Size = New System.Drawing.Size(286, 50)
        '
        'txtSaldo
        '
        Me.txtSaldo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtSaldo.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtSaldo.ForeColor = System.Drawing.Color.Maroon
        Me.txtSaldo.Location = New System.Drawing.Point(162, 8)
        Me.txtSaldo.Multiline = True
        Me.txtSaldo.Name = "txtSaldo"
        Me.txtSaldo.ReadOnly = True
        Me.txtSaldo.Size = New System.Drawing.Size(116, 34)
        Me.txtSaldo.TabIndex = 18
        Me.txtSaldo.Text = "0.00"
        Me.txtSaldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.Label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label8.Location = New System.Drawing.Point(6, 11)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(150, 32)
        Me.Label8.Text = "Saldo Pendiente:"
        '
        'panCobros
        '
        Me.panCobros.Controls.Add(Me.txtDescuento)
        Me.panCobros.Controls.Add(Me.txtEnvase)
        Me.panCobros.Controls.Add(Me.txtTotal)
        Me.panCobros.Controls.Add(Me.Label3)
        Me.panCobros.Controls.Add(Me.Label4)
        Me.panCobros.Controls.Add(Me.Label5)
        Me.panCobros.Location = New System.Drawing.Point(0, 78)
        Me.panCobros.Name = "panCobros"
        Me.panCobros.Size = New System.Drawing.Size(477, 123)
        '
        'txtDescuento
        '
        Me.txtDescuento.BackColor = System.Drawing.Color.AliceBlue
        Me.txtDescuento.ForeColor = System.Drawing.SystemColors.MenuText
        Me.txtDescuento.Location = New System.Drawing.Point(206, 42)
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.Size = New System.Drawing.Size(268, 42)
        Me.txtDescuento.Text = "0"
        Me.txtDescuento.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtEnvase
        '
        Me.txtEnvase.BackColor = System.Drawing.SystemColors.Info
        Me.txtEnvase.ForeColor = System.Drawing.SystemColors.MenuText
        Me.txtEnvase.Location = New System.Drawing.Point(206, 84)
        Me.txtEnvase.Name = "txtEnvase"
        Me.txtEnvase.Size = New System.Drawing.Size(268, 38)
        Me.txtEnvase.Text = "0"
        Me.txtEnvase.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtTotal
        '
        Me.txtTotal.BackColor = System.Drawing.SystemColors.Info
        Me.txtTotal.ForeColor = System.Drawing.SystemColors.MenuText
        Me.txtTotal.Location = New System.Drawing.Point(206, 3)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(268, 38)
        Me.txtTotal.Text = "0"
        Me.txtTotal.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.AliceBlue
        Me.Label3.Location = New System.Drawing.Point(0, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(208, 42)
        Me.Label3.Text = "Imp. Descuento:"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Info
        Me.Label4.Location = New System.Drawing.Point(0, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(208, 42)
        Me.Label4.Text = "Envase:"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Info
        Me.Label5.Location = New System.Drawing.Point(0, 2)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(208, 42)
        Me.Label5.Text = "Total:"
        '
        'lblNumero
        '
        Me.lblNumero.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblNumero.ForeColor = System.Drawing.Color.Maroon
        Me.lblNumero.Location = New System.Drawing.Point(140, 43)
        Me.lblNumero.Name = "lblNumero"
        Me.lblNumero.Size = New System.Drawing.Size(138, 28)
        Me.lblNumero.Text = "#####"
        '
        'lblSerie
        '
        Me.lblSerie.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblSerie.ForeColor = System.Drawing.Color.Maroon
        Me.lblSerie.Location = New System.Drawing.Point(63, 43)
        Me.lblSerie.Name = "lblSerie"
        Me.lblSerie.Size = New System.Drawing.Size(74, 28)
        Me.lblSerie.Text = "MF08"
        '
        'lblNegocio
        '
        Me.lblNegocio.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular)
        Me.lblNegocio.Location = New System.Drawing.Point(3, 0)
        Me.lblNegocio.Name = "lblNegocio"
        Me.lblNegocio.Size = New System.Drawing.Size(477, 40)
        Me.lblNegocio.Text = "lblNombreCliente"
        '
        'lblAtencion
        '
        Me.lblAtencion.BackColor = System.Drawing.Color.White
        Me.lblAtencion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblAtencion.ForeColor = System.Drawing.Color.Maroon
        Me.lblAtencion.Location = New System.Drawing.Point(3, 40)
        Me.lblAtencion.Name = "lblAtencion"
        Me.lblAtencion.Size = New System.Drawing.Size(385, 31)
        Me.lblAtencion.Text = "Pago"
        '
        'frmPago
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(192.0!, 192.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(480, 536)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblSerie)
        Me.Controls.Add(Me.lblNumero)
        Me.Controls.Add(Me.lblAtencion)
        Me.Controls.Add(Me.lblNegocio)
        Me.Controls.Add(Me.panCobros)
        Me.Controls.Add(Me.lblTotalPagar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgFormasPago)
        Me.Controls.Add(Me.panSaldo)
        Me.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Location = New System.Drawing.Point(0, 52)
        Me.Menu = Me.menuPago
        Me.Name = "frmPago"
        Me.panSaldo.ResumeLayout(False)
        Me.panCobros.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgFormasPago As System.Windows.Forms.DataGrid
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblTotalPagar As System.Windows.Forms.Label
    Friend WithEvents dgFormasPagoStyle As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents panSaldo As System.Windows.Forms.Panel
    Friend WithEvents txtSaldo As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents rSoft As System.Windows.Forms.MenuItem
    Friend WithEvents panCobros As System.Windows.Forms.Panel
    Friend WithEvents lblNumero As System.Windows.Forms.Label
    Friend WithEvents lblSerie As System.Windows.Forms.Label
    Friend WithEvents txtDescuento As System.Windows.Forms.Label
    Friend WithEvents txtEnvase As System.Windows.Forms.Label
    Friend WithEvents txtTotal As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents lsoftCancelar As System.Windows.Forms.MenuItem
    Friend WithEvents lsoftAtras As System.Windows.Forms.MenuItem
    Friend WithEvents lblNegocio As System.Windows.Forms.Label
    Public WithEvents lblAtencion As System.Windows.Forms.Label
End Class
