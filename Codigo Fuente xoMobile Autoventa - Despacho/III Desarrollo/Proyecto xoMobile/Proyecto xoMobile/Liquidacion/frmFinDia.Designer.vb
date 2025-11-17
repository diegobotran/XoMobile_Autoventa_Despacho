Imports DataGridCustomColumns

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmFinDia
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
    Private menuFinDia As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFinDia))
        Me.menuFinDia = New System.Windows.Forms.MainMenu
        Me.lSoft = New System.Windows.Forms.MenuItem
        Me.lstIntegracion = New System.Windows.Forms.ListView
        Me.lstMovimientos = New System.Windows.Forms.ListView
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.Movimientos = New System.Windows.Forms.TabPage
        Me.panTotalMovimiento = New System.Windows.Forms.Panel
        Me.panEncTotal = New System.Windows.Forms.Panel
        Me.Label17 = New System.Windows.Forms.Label
        Me.lblTotalVenta = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblTotal = New System.Windows.Forms.Label
        Me.lblTotalLiquido = New System.Windows.Forms.Label
        Me.lblTotalEnvase = New System.Windows.Forms.Label
        Me.lblTotalCarga = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.lblTotalDevolucion = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lbltotalOperaciones = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.LblTotalVias = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.lblDeposito1 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblDiferencia1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.LblCupon5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.LblCupons = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.LblCupon2 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.lstDepositos = New System.Windows.Forms.ListView
        Me.panDeposito = New System.Windows.Forms.Panel
        Me.txtValorOut = New DataGridCustomColumns.NumericTextBox
        Me.txtDocumentoOut = New System.Windows.Forms.TextBox
        Me.lstBancosOut = New System.Windows.Forms.ComboBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblDiferencia2 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblDeposito2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblDepositado2 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Diferencia = New System.Windows.Forms.TabPage
        Me.panDivision = New System.Windows.Forms.Panel
        Me.txtValorDiferencia = New DataGridCustomColumns.NumericTextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.lblDiferencia4 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblDeposito4 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.lblDepositado4 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.lstDiferencias = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.Label18 = New System.Windows.Forms.Label
        Me.lstMotivoDiferencia = New System.Windows.Forms.ComboBox
        Me.lblTotalDiferencia = New System.Windows.Forms.Label
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.panResultLogin = New System.Windows.Forms.Panel
        Me.lblError = New System.Windows.Forms.Label
        Me.panLogin = New System.Windows.Forms.Panel
        Me.cmdCerrarLogin = New System.Windows.Forms.PictureBox
        Me.lblSolicitaClave = New System.Windows.Forms.Label
        Me.txtClave = New System.Windows.Forms.TextBox
        Me.cmdConfirma = New System.Windows.Forms.Button
        Me.lblDeclaro = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.lblDiferenciaReportada = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.lblDiferencia3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblDeposito3 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblDepositado3 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.InputPanel1 = New Microsoft.WindowsCE.Forms.InputPanel(Me.components)
        Me.TabControl1.SuspendLayout()
        Me.Movimientos.SuspendLayout()
        Me.panTotalMovimiento.SuspendLayout()
        Me.panEncTotal.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.panDeposito.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Diferencia.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.panResultLogin.SuspendLayout()
        Me.panLogin.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuFinDia
        '
        Me.menuFinDia.MenuItems.Add(Me.lSoft)
        '
        'lSoft
        '
        Me.lSoft.Text = "Regresar"
        '
        'lstIntegracion
        '
        Me.lstIntegracion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lstIntegracion.FullRowSelect = True
        Me.lstIntegracion.Location = New System.Drawing.Point(0, 0)
        Me.lstIntegracion.Name = "lstIntegracion"
        Me.lstIntegracion.Size = New System.Drawing.Size(480, 347)
        Me.lstIntegracion.TabIndex = 3
        Me.lstIntegracion.View = System.Windows.Forms.View.Details
        '
        'lstMovimientos
        '
        Me.lstMovimientos.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lstMovimientos.FullRowSelect = True
        Me.lstMovimientos.Location = New System.Drawing.Point(0, 0)
        Me.lstMovimientos.Name = "lstMovimientos"
        Me.lstMovimientos.Size = New System.Drawing.Size(480, 486)
        Me.lstMovimientos.TabIndex = 4
        Me.lstMovimientos.View = System.Windows.Forms.View.Details
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.Movimientos)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.Diferencia)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Regular)
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(480, 536)
        Me.TabControl1.TabIndex = 5
        '
        'Movimientos
        '
        Me.Movimientos.Controls.Add(Me.lstMovimientos)
        Me.Movimientos.Controls.Add(Me.panTotalMovimiento)
        Me.Movimientos.Location = New System.Drawing.Point(0, 0)
        Me.Movimientos.Name = "Movimientos"
        Me.Movimientos.Size = New System.Drawing.Size(480, 492)
        Me.Movimientos.Text = "Movimientos"
        '
        'panTotalMovimiento
        '
        Me.panTotalMovimiento.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.panTotalMovimiento.Controls.Add(Me.panEncTotal)
        Me.panTotalMovimiento.Controls.Add(Me.lblTotalVenta)
        Me.panTotalMovimiento.Controls.Add(Me.Label7)
        Me.panTotalMovimiento.Controls.Add(Me.lblTotal)
        Me.panTotalMovimiento.Controls.Add(Me.lblTotalLiquido)
        Me.panTotalMovimiento.Controls.Add(Me.lblTotalEnvase)
        Me.panTotalMovimiento.Controls.Add(Me.lblTotalCarga)
        Me.panTotalMovimiento.Controls.Add(Me.Label12)
        Me.panTotalMovimiento.Controls.Add(Me.lblTotalDevolucion)
        Me.panTotalMovimiento.Controls.Add(Me.Label14)
        Me.panTotalMovimiento.Location = New System.Drawing.Point(0, 396)
        Me.panTotalMovimiento.Name = "panTotalMovimiento"
        Me.panTotalMovimiento.Size = New System.Drawing.Size(480, 100)
        '
        'panEncTotal
        '
        Me.panEncTotal.Controls.Add(Me.Label17)
        Me.panEncTotal.Location = New System.Drawing.Point(2, 30)
        Me.panEncTotal.Name = "panEncTotal"
        Me.panEncTotal.Size = New System.Drawing.Size(474, 30)
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.Orange
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label17.Location = New System.Drawing.Point(2, 2)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(474, 26)
        Me.Label17.Text = "Venta:    Env.                    Liq.                 Total"
        '
        'lblTotalVenta
        '
        Me.lblTotalVenta.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.lblTotalVenta.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalVenta.ForeColor = System.Drawing.Color.Maroon
        Me.lblTotalVenta.Location = New System.Drawing.Point(376, 2)
        Me.lblTotalVenta.Name = "lblTotalVenta"
        Me.lblTotalVenta.Size = New System.Drawing.Size(106, 40)
        Me.lblTotalVenta.Text = "    --"
        Me.lblTotalVenta.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label7.Location = New System.Drawing.Point(330, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 40)
        Me.Label7.Text = "Vta:"
        '
        'lblTotal
        '
        Me.lblTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.lblTotal.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotal.ForeColor = System.Drawing.Color.DarkRed
        Me.lblTotal.Location = New System.Drawing.Point(336, 66)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(140, 30)
        Me.lblTotal.Text = "0"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotalLiquido
        '
        Me.lblTotalLiquido.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.lblTotalLiquido.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalLiquido.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblTotalLiquido.Location = New System.Drawing.Point(172, 66)
        Me.lblTotalLiquido.Name = "lblTotalLiquido"
        Me.lblTotalLiquido.Size = New System.Drawing.Size(140, 30)
        Me.lblTotalLiquido.Text = "0"
        Me.lblTotalLiquido.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotalEnvase
        '
        Me.lblTotalEnvase.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.lblTotalEnvase.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalEnvase.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblTotalEnvase.Location = New System.Drawing.Point(4, 66)
        Me.lblTotalEnvase.Name = "lblTotalEnvase"
        Me.lblTotalEnvase.Size = New System.Drawing.Size(140, 30)
        Me.lblTotalEnvase.Text = "0"
        Me.lblTotalEnvase.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotalCarga
        '
        Me.lblTotalCarga.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.lblTotalCarga.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalCarga.ForeColor = System.Drawing.Color.Maroon
        Me.lblTotalCarga.Location = New System.Drawing.Point(68, 2)
        Me.lblTotalCarga.Name = "lblTotalCarga"
        Me.lblTotalCarga.Size = New System.Drawing.Size(106, 40)
        Me.lblTotalCarga.Text = "    --"
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label12.Location = New System.Drawing.Point(0, 2)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(74, 40)
        Me.Label12.Text = "Carga:"
        '
        'lblTotalDevolucion
        '
        Me.lblTotalDevolucion.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.lblTotalDevolucion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalDevolucion.ForeColor = System.Drawing.Color.Maroon
        Me.lblTotalDevolucion.Location = New System.Drawing.Point(224, 2)
        Me.lblTotalDevolucion.Name = "lblTotalDevolucion"
        Me.lblTotalDevolucion.Size = New System.Drawing.Size(106, 40)
        Me.lblTotalDevolucion.Text = "    --"
        Me.lblTotalDevolucion.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label14.Location = New System.Drawing.Point(170, 2)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(66, 40)
        Me.Label14.Text = "Dev:"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Panel1)
        Me.TabPage2.Controls.Add(Me.lstIntegracion)
        Me.TabPage2.Location = New System.Drawing.Point(0, 0)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(472, 498)
        Me.TabPage2.Text = "Integracion"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel1.Controls.Add(Me.lbltotalOperaciones)
        Me.Panel1.Controls.Add(Me.Label25)
        Me.Panel1.Controls.Add(Me.LblTotalVias)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.lblDeposito1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.lblDiferencia1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(0, 353)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(480, 135)
        '
        'lbltotalOperaciones
        '
        Me.lbltotalOperaciones.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lbltotalOperaciones.ForeColor = System.Drawing.Color.OrangeRed
        Me.lbltotalOperaciones.Location = New System.Drawing.Point(276, 66)
        Me.lbltotalOperaciones.Name = "lbltotalOperaciones"
        Me.lbltotalOperaciones.Size = New System.Drawing.Size(184, 29)
        Me.lbltotalOperaciones.Text = "0"
        Me.lbltotalOperaciones.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label25.Location = New System.Drawing.Point(7, 68)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(196, 27)
        Me.Label25.Text = "Total Operaciones :"
        '
        'LblTotalVias
        '
        Me.LblTotalVias.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LblTotalVias.ForeColor = System.Drawing.Color.ForestGreen
        Me.LblTotalVias.Location = New System.Drawing.Point(276, 31)
        Me.LblTotalVias.Name = "LblTotalVias"
        Me.LblTotalVias.Size = New System.Drawing.Size(184, 40)
        Me.LblTotalVias.Text = "0"
        Me.LblTotalVias.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label20.Location = New System.Drawing.Point(6, 31)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(196, 40)
        Me.Label20.Text = "Total Otras Vias :"
        '
        'lblDeposito1
        '
        Me.lblDeposito1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblDeposito1.ForeColor = System.Drawing.Color.Maroon
        Me.lblDeposito1.Location = New System.Drawing.Point(276, 0)
        Me.lblDeposito1.Name = "lblDeposito1"
        Me.lblDeposito1.Size = New System.Drawing.Size(184, 40)
        Me.lblDeposito1.Text = "0"
        Me.lblDeposito1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label1.Location = New System.Drawing.Point(6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(264, 40)
        Me.Label1.Text = "Total a Depositar:"
        '
        'lblDiferencia1
        '
        Me.lblDiferencia1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblDiferencia1.ForeColor = System.Drawing.Color.Red
        Me.lblDiferencia1.Location = New System.Drawing.Point(276, 106)
        Me.lblDiferencia1.Name = "lblDiferencia1"
        Me.lblDiferencia1.Size = New System.Drawing.Size(184, 29)
        Me.lblDiferencia1.Text = "0"
        Me.lblDiferencia1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label2.Location = New System.Drawing.Point(6, 108)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(264, 27)
        Me.Label2.Text = "Diferencias de efectivo:"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Panel3)
        Me.TabPage3.Controls.Add(Me.lstDepositos)
        Me.TabPage3.Controls.Add(Me.panDeposito)
        Me.TabPage3.Controls.Add(Me.Panel2)
        Me.TabPage3.Location = New System.Drawing.Point(0, 0)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(480, 492)
        Me.TabPage3.Text = "Deposito"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.PaleTurquoise
        Me.Panel3.Controls.Add(Me.LblCupon5)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.LblCupons)
        Me.Panel3.Controls.Add(Me.Label22)
        Me.Panel3.Controls.Add(Me.LblCupon2)
        Me.Panel3.Controls.Add(Me.Label24)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 289)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(480, 85)
        '
        'LblCupon5
        '
        Me.LblCupon5.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.LblCupon5.ForeColor = System.Drawing.Color.Maroon
        Me.LblCupon5.Location = New System.Drawing.Point(282, 51)
        Me.LblCupon5.Name = "LblCupon5"
        Me.LblCupon5.Size = New System.Drawing.Size(184, 24)
        Me.LblCupon5.Text = "0"
        Me.LblCupon5.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.LblCupon5.Visible = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label3.Location = New System.Drawing.Point(7, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(163, 24)
        Me.Label3.Text = "Cupón #5"
        Me.Label3.Visible = False
        '
        'LblCupons
        '
        Me.LblCupons.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.LblCupons.ForeColor = System.Drawing.Color.Maroon
        Me.LblCupons.Location = New System.Drawing.Point(347, 0)
        Me.LblCupons.Name = "LblCupons"
        Me.LblCupons.Size = New System.Drawing.Size(119, 27)
        Me.LblCupons.Text = "0"
        Me.LblCupons.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.LblCupons.Visible = False
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label22.Location = New System.Drawing.Point(6, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(164, 27)
        Me.Label22.Text = "Cupón Sorpresa"
        Me.Label22.Visible = False
        '
        'LblCupon2
        '
        Me.LblCupon2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.LblCupon2.ForeColor = System.Drawing.Color.Maroon
        Me.LblCupon2.Location = New System.Drawing.Point(282, 27)
        Me.LblCupon2.Name = "LblCupon2"
        Me.LblCupon2.Size = New System.Drawing.Size(184, 24)
        Me.LblCupon2.Text = "0"
        Me.LblCupon2.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.LblCupon2.Visible = False
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label24.Location = New System.Drawing.Point(7, 27)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(163, 24)
        Me.Label24.Text = "Cupón #2"
        Me.Label24.Visible = False
        '
        'lstDepositos
        '
        Me.lstDepositos.Dock = System.Windows.Forms.DockStyle.Top
        Me.lstDepositos.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lstDepositos.FullRowSelect = True
        Me.lstDepositos.Location = New System.Drawing.Point(0, 0)
        Me.lstDepositos.Name = "lstDepositos"
        Me.lstDepositos.Size = New System.Drawing.Size(480, 240)
        Me.lstDepositos.TabIndex = 3
        Me.lstDepositos.View = System.Windows.Forms.View.Details
        '
        'panDeposito
        '
        Me.panDeposito.Controls.Add(Me.txtValorOut)
        Me.panDeposito.Controls.Add(Me.txtDocumentoOut)
        Me.panDeposito.Controls.Add(Me.lstBancosOut)
        Me.panDeposito.Location = New System.Drawing.Point(0, 241)
        Me.panDeposito.Name = "panDeposito"
        Me.panDeposito.Size = New System.Drawing.Size(480, 42)
        '
        'txtValorOut
        '
        Me.txtValorOut.AllowSpace = False
        Me.txtValorOut.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtValorOut.Location = New System.Drawing.Point(369, 0)
        Me.txtValorOut.Name = "txtValorOut"
        Me.txtValorOut.Size = New System.Drawing.Size(111, 35)
        Me.txtValorOut.TabIndex = 3
        Me.txtValorOut.Text = "Valor QTZ"
        '
        'txtDocumentoOut
        '
        Me.txtDocumentoOut.AcceptsReturn = True
        Me.txtDocumentoOut.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtDocumentoOut.Location = New System.Drawing.Point(252, 0)
        Me.txtDocumentoOut.Name = "txtDocumentoOut"
        Me.txtDocumentoOut.Size = New System.Drawing.Size(118, 35)
        Me.txtDocumentoOut.TabIndex = 2
        Me.txtDocumentoOut.Text = "No. Boleta"
        '
        'lstBancosOut
        '
        Me.lstBancosOut.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lstBancosOut.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lstBancosOut.Location = New System.Drawing.Point(0, 0)
        Me.lstBancosOut.Name = "lstBancosOut"
        Me.lstBancosOut.Size = New System.Drawing.Size(252, 35)
        Me.lstBancosOut.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel2.Controls.Add(Me.lblDiferencia2)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.lblDeposito2)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.lblDepositado2)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 374)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(480, 118)
        '
        'lblDiferencia2
        '
        Me.lblDiferencia2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDiferencia2.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblDiferencia2.Location = New System.Drawing.Point(282, 86)
        Me.lblDiferencia2.Name = "lblDiferencia2"
        Me.lblDiferencia2.Size = New System.Drawing.Size(184, 40)
        Me.lblDiferencia2.Text = "0"
        Me.lblDiferencia2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label8.Location = New System.Drawing.Point(6, 86)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(264, 32)
        Me.Label8.Text = "Diferencias de efectivo:"
        '
        'lblDeposito2
        '
        Me.lblDeposito2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDeposito2.ForeColor = System.Drawing.Color.Maroon
        Me.lblDeposito2.Location = New System.Drawing.Point(282, 6)
        Me.lblDeposito2.Name = "lblDeposito2"
        Me.lblDeposito2.Size = New System.Drawing.Size(184, 40)
        Me.lblDeposito2.Text = "0"
        Me.lblDeposito2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label4.Location = New System.Drawing.Point(6, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(264, 40)
        Me.Label4.Text = "Total a Depositar:"
        '
        'lblDepositado2
        '
        Me.lblDepositado2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDepositado2.ForeColor = System.Drawing.Color.Maroon
        Me.lblDepositado2.Location = New System.Drawing.Point(282, 46)
        Me.lblDepositado2.Name = "lblDepositado2"
        Me.lblDepositado2.Size = New System.Drawing.Size(184, 40)
        Me.lblDepositado2.Text = "0"
        Me.lblDepositado2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label6.Location = New System.Drawing.Point(6, 46)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(264, 40)
        Me.Label6.Text = "Depositado actualmente:"
        '
        'Diferencia
        '
        Me.Diferencia.Controls.Add(Me.panDivision)
        Me.Diferencia.Controls.Add(Me.txtValorDiferencia)
        Me.Diferencia.Controls.Add(Me.Label16)
        Me.Diferencia.Controls.Add(Me.Panel5)
        Me.Diferencia.Controls.Add(Me.lstDiferencias)
        Me.Diferencia.Controls.Add(Me.Label18)
        Me.Diferencia.Controls.Add(Me.lstMotivoDiferencia)
        Me.Diferencia.Controls.Add(Me.lblTotalDiferencia)
        Me.Diferencia.Location = New System.Drawing.Point(0, 0)
        Me.Diferencia.Name = "Diferencia"
        Me.Diferencia.Size = New System.Drawing.Size(480, 492)
        Me.Diferencia.Text = "Diferencia"
        '
        'panDivision
        '
        Me.panDivision.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.panDivision.Location = New System.Drawing.Point(0, 175)
        Me.panDivision.Name = "panDivision"
        Me.panDivision.Size = New System.Drawing.Size(474, 2)
        '
        'txtValorDiferencia
        '
        Me.txtValorDiferencia.AllowSpace = False
        Me.txtValorDiferencia.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtValorDiferencia.Location = New System.Drawing.Point(308, 186)
        Me.txtValorDiferencia.Name = "txtValorDiferencia"
        Me.txtValorDiferencia.Size = New System.Drawing.Size(166, 35)
        Me.txtValorDiferencia.TabIndex = 12
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label16.Location = New System.Drawing.Point(6, 444)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(326, 40)
        Me.Label16.Text = "Diferencia Reportada:"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel5.Controls.Add(Me.lblDiferencia4)
        Me.Panel5.Controls.Add(Me.Label10)
        Me.Panel5.Controls.Add(Me.lblDeposito4)
        Me.Panel5.Controls.Add(Me.Label13)
        Me.Panel5.Controls.Add(Me.lblDepositado4)
        Me.Panel5.Controls.Add(Me.Label15)
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(480, 130)
        '
        'lblDiferencia4
        '
        Me.lblDiferencia4.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDiferencia4.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblDiferencia4.Location = New System.Drawing.Point(282, 86)
        Me.lblDiferencia4.Name = "lblDiferencia4"
        Me.lblDiferencia4.Size = New System.Drawing.Size(184, 40)
        Me.lblDiferencia4.Text = "0"
        Me.lblDiferencia4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label10.Location = New System.Drawing.Point(6, 86)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(298, 32)
        Me.Label10.Text = "Diferencias de efectivo:"
        '
        'lblDeposito4
        '
        Me.lblDeposito4.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDeposito4.ForeColor = System.Drawing.Color.Maroon
        Me.lblDeposito4.Location = New System.Drawing.Point(282, 6)
        Me.lblDeposito4.Name = "lblDeposito4"
        Me.lblDeposito4.Size = New System.Drawing.Size(184, 40)
        Me.lblDeposito4.Text = "0"
        Me.lblDeposito4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label13.Location = New System.Drawing.Point(6, 6)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(326, 40)
        Me.Label13.Text = "Total a Depositar:"
        '
        'lblDepositado4
        '
        Me.lblDepositado4.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDepositado4.ForeColor = System.Drawing.Color.Maroon
        Me.lblDepositado4.Location = New System.Drawing.Point(282, 46)
        Me.lblDepositado4.Name = "lblDepositado4"
        Me.lblDepositado4.Size = New System.Drawing.Size(184, 40)
        Me.lblDepositado4.Text = "0"
        Me.lblDepositado4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label15.Location = New System.Drawing.Point(6, 46)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(326, 40)
        Me.Label15.Text = "Depositado actualmente:"
        '
        'lstDiferencias
        '
        Me.lstDiferencias.Columns.Add(Me.ColumnHeader1)
        Me.lstDiferencias.Columns.Add(Me.ColumnHeader2)
        Me.lstDiferencias.Columns.Add(Me.ColumnHeader3)
        Me.lstDiferencias.Columns.Add(Me.ColumnHeader4)
        Me.lstDiferencias.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lstDiferencias.FullRowSelect = True
        Me.lstDiferencias.Location = New System.Drawing.Point(0, 234)
        Me.lstDiferencias.Name = "lstDiferencias"
        Me.lstDiferencias.Size = New System.Drawing.Size(474, 206)
        Me.lstDiferencias.TabIndex = 13
        Me.lstDiferencias.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Motivo"
        Me.ColumnHeader1.Width = 169
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Valor"
        Me.ColumnHeader2.Width = 125
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "idMotivo"
        Me.ColumnHeader3.Width = 60
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "ColumnHeader"
        Me.ColumnHeader4.Width = 60
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label18.Location = New System.Drawing.Point(182, 194)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(138, 40)
        Me.Label18.Text = "Valor:   Q."
        '
        'lstMotivoDiferencia
        '
        Me.lstMotivoDiferencia.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lstMotivoDiferencia.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lstMotivoDiferencia.Location = New System.Drawing.Point(0, 130)
        Me.lstMotivoDiferencia.Name = "lstMotivoDiferencia"
        Me.lstMotivoDiferencia.Size = New System.Drawing.Size(480, 35)
        Me.lstMotivoDiferencia.TabIndex = 11
        '
        'lblTotalDiferencia
        '
        Me.lblTotalDiferencia.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalDiferencia.ForeColor = System.Drawing.Color.Maroon
        Me.lblTotalDiferencia.Location = New System.Drawing.Point(282, 446)
        Me.lblTotalDiferencia.Name = "lblTotalDiferencia"
        Me.lblTotalDiferencia.Size = New System.Drawing.Size(184, 40)
        Me.lblTotalDiferencia.Text = "0"
        Me.lblTotalDiferencia.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.panResultLogin)
        Me.TabPage4.Controls.Add(Me.panLogin)
        Me.TabPage4.Controls.Add(Me.cmdConfirma)
        Me.TabPage4.Controls.Add(Me.lblDeclaro)
        Me.TabPage4.Controls.Add(Me.Panel4)
        Me.TabPage4.Location = New System.Drawing.Point(0, 0)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(480, 492)
        Me.TabPage4.Text = "Liquidar"
        '
        'panResultLogin
        '
        Me.panResultLogin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.panResultLogin.Controls.Add(Me.lblError)
        Me.panResultLogin.Location = New System.Drawing.Point(0, 420)
        Me.panResultLogin.Name = "panResultLogin"
        Me.panResultLogin.Size = New System.Drawing.Size(480, 72)
        Me.panResultLogin.Visible = False
        '
        'lblError
        '
        Me.lblError.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblError.Location = New System.Drawing.Point(46, 8)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(420, 32)
        Me.lblError.Text = "La clave ingresada no es correcta"
        Me.lblError.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'panLogin
        '
        Me.panLogin.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.panLogin.Controls.Add(Me.cmdCerrarLogin)
        Me.panLogin.Controls.Add(Me.lblSolicitaClave)
        Me.panLogin.Controls.Add(Me.txtClave)
        Me.panLogin.Location = New System.Drawing.Point(3, 323)
        Me.panLogin.Name = "panLogin"
        Me.panLogin.Size = New System.Drawing.Size(474, 91)
        Me.panLogin.Visible = False
        '
        'cmdCerrarLogin
        '
        Me.cmdCerrarLogin.BackColor = System.Drawing.Color.Transparent
        Me.cmdCerrarLogin.Image = CType(resources.GetObject("cmdCerrarLogin.Image"), System.Drawing.Image)
        Me.cmdCerrarLogin.Location = New System.Drawing.Point(399, 1)
        Me.cmdCerrarLogin.Name = "cmdCerrarLogin"
        Me.cmdCerrarLogin.Size = New System.Drawing.Size(32, 34)
        '
        'lblSolicitaClave
        '
        Me.lblSolicitaClave.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblSolicitaClave.Location = New System.Drawing.Point(3, 0)
        Me.lblSolicitaClave.Name = "lblSolicitaClave"
        Me.lblSolicitaClave.Size = New System.Drawing.Size(200, 35)
        Me.lblSolicitaClave.Text = "Clave de acceso:"
        '
        'txtClave
        '
        Me.txtClave.ForeColor = System.Drawing.Color.Maroon
        Me.txtClave.Location = New System.Drawing.Point(4, 38)
        Me.txtClave.Name = "txtClave"
        Me.txtClave.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtClave.Size = New System.Drawing.Size(465, 41)
        Me.txtClave.TabIndex = 1
        '
        'cmdConfirma
        '
        Me.cmdConfirma.Font = New System.Drawing.Font("Tahoma", 8.5!, System.Drawing.FontStyle.Bold)
        Me.cmdConfirma.Location = New System.Drawing.Point(137, 337)
        Me.cmdConfirma.Name = "cmdConfirma"
        Me.cmdConfirma.Size = New System.Drawing.Size(202, 88)
        Me.cmdConfirma.TabIndex = 3
        Me.cmdConfirma.Text = "Confirmar"
        '
        'lblDeclaro
        '
        Me.lblDeclaro.Location = New System.Drawing.Point(6, 205)
        Me.lblDeclaro.Name = "lblDeclaro"
        Me.lblDeclaro.Size = New System.Drawing.Size(460, 140)
        Me.lblDeclaro.Text = "Al presionar el boton ""Confirmar"" se esta autorizando y confirmando la revision y" & _
            " validacion de la informacion generada por la Ruta No. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.lblDeclaro.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel4.Controls.Add(Me.lblDiferenciaReportada)
        Me.Panel4.Controls.Add(Me.Label19)
        Me.Panel4.Controls.Add(Me.lblDiferencia3)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.lblDeposito3)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.lblDepositado3)
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(480, 170)
        '
        'lblDiferenciaReportada
        '
        Me.lblDiferenciaReportada.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDiferenciaReportada.ForeColor = System.Drawing.Color.DarkViolet
        Me.lblDiferenciaReportada.Location = New System.Drawing.Point(282, 79)
        Me.lblDiferenciaReportada.Name = "lblDiferenciaReportada"
        Me.lblDiferenciaReportada.Size = New System.Drawing.Size(184, 26)
        Me.lblDiferenciaReportada.Text = "0"
        Me.lblDiferenciaReportada.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label19.Location = New System.Drawing.Point(6, 79)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(264, 26)
        Me.Label19.Text = "Diferencia reportada:"
        '
        'lblDiferencia3
        '
        Me.lblDiferencia3.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDiferencia3.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblDiferencia3.Location = New System.Drawing.Point(282, 53)
        Me.lblDiferencia3.Name = "lblDiferencia3"
        Me.lblDiferencia3.Size = New System.Drawing.Size(184, 26)
        Me.lblDiferencia3.Text = "0"
        Me.lblDiferencia3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label5.Location = New System.Drawing.Point(6, 53)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(264, 26)
        Me.Label5.Text = "Diferencias de efectivo:"
        '
        'lblDeposito3
        '
        Me.lblDeposito3.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDeposito3.ForeColor = System.Drawing.Color.Maroon
        Me.lblDeposito3.Location = New System.Drawing.Point(282, 6)
        Me.lblDeposito3.Name = "lblDeposito3"
        Me.lblDeposito3.Size = New System.Drawing.Size(184, 25)
        Me.lblDeposito3.Text = "0"
        Me.lblDeposito3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label9.Location = New System.Drawing.Point(6, 6)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(264, 25)
        Me.Label9.Text = "Total a Depositar:"
        '
        'lblDepositado3
        '
        Me.lblDepositado3.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblDepositado3.ForeColor = System.Drawing.Color.Maroon
        Me.lblDepositado3.Location = New System.Drawing.Point(282, 28)
        Me.lblDepositado3.Name = "lblDepositado3"
        Me.lblDepositado3.Size = New System.Drawing.Size(184, 25)
        Me.lblDepositado3.Text = "0"
        Me.lblDepositado3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label11.Location = New System.Drawing.Point(6, 28)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(264, 25)
        Me.Label11.Text = "Depositado actualmente:"
        '
        'frmFinDia
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(192.0!, 192.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(480, 536)
        Me.ControlBox = False
        Me.Controls.Add(Me.TabControl1)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(0, 52)
        Me.Menu = Me.menuFinDia
        Me.Name = "frmFinDia"
        Me.Text = "Fin de dia"
        Me.TabControl1.ResumeLayout(False)
        Me.Movimientos.ResumeLayout(False)
        Me.panTotalMovimiento.ResumeLayout(False)
        Me.panEncTotal.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.panDeposito.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Diferencia.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.panResultLogin.ResumeLayout(False)
        Me.panLogin.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstIntegracion As System.Windows.Forms.ListView
    Friend WithEvents lstMovimientos As System.Windows.Forms.ListView
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents Movimientos As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents lblDiferencia1 As System.Windows.Forms.Label
    Friend WithEvents lblDeposito1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblDiferencia2 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblDeposito2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblDepositado2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents panDeposito As System.Windows.Forms.Panel
    Friend WithEvents txtValorOut As NumericTextBox
    Friend WithEvents txtDocumentoOut As System.Windows.Forms.TextBox
    Friend WithEvents lstBancosOut As System.Windows.Forms.ComboBox
    Friend WithEvents lstDepositos As System.Windows.Forms.ListView
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblDiferencia3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblDeposito3 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblDepositado3 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblDeclaro As System.Windows.Forms.Label
    Friend WithEvents cmdConfirma As System.Windows.Forms.Button
    Friend WithEvents lSoft As System.Windows.Forms.MenuItem
    Friend WithEvents panTotalMovimiento As System.Windows.Forms.Panel
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents lblTotalLiquido As System.Windows.Forms.Label
    Friend WithEvents lblTotalEnvase As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lblTotalVenta As System.Windows.Forms.Label
    Friend WithEvents panEncTotal As System.Windows.Forms.Panel
    Friend WithEvents panLogin As System.Windows.Forms.Panel
    Friend WithEvents txtClave As System.Windows.Forms.TextBox
    Friend WithEvents panResultLogin As System.Windows.Forms.Panel
    Friend WithEvents lblError As System.Windows.Forms.Label
    Friend WithEvents Diferencia As System.Windows.Forms.TabPage
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents lblDiferencia4 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblDeposito4 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblDepositado4 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lstDiferencias As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lstMotivoDiferencia As System.Windows.Forms.ComboBox
    Friend WithEvents txtValorDiferencia As DataGridCustomColumns.NumericTextBox
    Friend WithEvents lblTotalDiferencia As System.Windows.Forms.Label
    Friend WithEvents lblDiferenciaReportada As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents panDivision As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblTotalCarga As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblTotalDevolucion As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cmdCerrarLogin As System.Windows.Forms.PictureBox
    Friend WithEvents lblSolicitaClave As System.Windows.Forms.Label
    Friend WithEvents InputPanel1 As Microsoft.WindowsCE.Forms.InputPanel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents LblCupon5 As System.Windows.Forms.Label
    Friend WithEvents LblCupons As System.Windows.Forms.Label
    Friend WithEvents LblCupon2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lbltotalOperaciones As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents LblTotalVias As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
End Class
