<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmDetalleCliente
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
    Private menuDetalleCliente As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.menuDetalleCliente = New System.Windows.Forms.MainMenu
        Me.rUpdate = New System.Windows.Forms.MenuItem
        Me.rSoft = New System.Windows.Forms.MenuItem
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.tab_contacto = New System.Windows.Forms.TabPage
        Me.panAtributoA = New System.Windows.Forms.Panel
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.panValorA = New System.Windows.Forms.Panel
        Me.lblcui = New System.Windows.Forms.Label
        Me.lblCodigo = New System.Windows.Forms.Label
        Me.lblTelefonos = New System.Windows.Forms.Label
        Me.lblNit = New System.Windows.Forms.Label
        Me.lblDireccion = New System.Windows.Forms.Label
        Me.lblPropietario = New System.Windows.Forms.Label
        Me.lblCategoria = New System.Windows.Forms.Label
        Me.lblNegocio = New System.Windows.Forms.Label
        Me.lblPasaporte = New System.Windows.Forms.Label
        Me.tab_negocio = New System.Windows.Forms.TabPage
        Me.panValor = New System.Windows.Forms.Panel
        Me.lblCondicion_ = New System.Windows.Forms.Label
        Me.lblListaPrecio_ = New System.Windows.Forms.Label
        Me.lstformasPago = New System.Windows.Forms.ListBox
        Me.lblVentaConSaldoVencido = New System.Windows.Forms.CheckBox
        Me.lblVentaConSaldo = New System.Windows.Forms.CheckBox
        Me.lblPresupuesto = New System.Windows.Forms.Label
        Me.lblDiasCredito = New System.Windows.Forms.Label
        Me.lblCreditoDisponible = New System.Windows.Forms.Label
        Me.lblCreditoAutorizado = New System.Windows.Forms.Label
        Me.lblDiaVisita = New System.Windows.Forms.Label
        Me.panAtributo = New System.Windows.Forms.Panel
        Me.lblCondicion = New System.Windows.Forms.Label
        Me.lblListaPrecio = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.tab_segmento = New System.Windows.Forms.TabPage
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.lblTipoRuta = New System.Windows.Forms.Label
        Me.lblRegion = New System.Windows.Forms.Label
        Me.lblCanal = New System.Windows.Forms.Label
        Me.lblClaseCliente = New System.Windows.Forms.Label
        Me.lvProductos = New System.Windows.Forms.ListView
        Me.Label3 = New System.Windows.Forms.Label
        Me.tab_descuentos = New System.Windows.Forms.TabPage
        Me.panDivH = New System.Windows.Forms.Panel
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.lblDescuentos = New System.Windows.Forms.Label
        Me.lblNombreCliente = New System.Windows.Forms.Label
        Me.panel = New System.Windows.Forms.Panel
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtPasaporte = New System.Windows.Forms.TextBox
        Me.cmdProcesar = New System.Windows.Forms.Button
        Me.cmdAnular = New System.Windows.Forms.Button
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtcui = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtnit = New System.Windows.Forms.TextBox
        Me.TabControl1.SuspendLayout()
        Me.tab_contacto.SuspendLayout()
        Me.panAtributoA.SuspendLayout()
        Me.panValorA.SuspendLayout()
        Me.tab_negocio.SuspendLayout()
        Me.panValor.SuspendLayout()
        Me.panAtributo.SuspendLayout()
        Me.tab_segmento.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.tab_descuentos.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuDetalleCliente
        '
        Me.menuDetalleCliente.MenuItems.Add(Me.rUpdate)
        Me.menuDetalleCliente.MenuItems.Add(Me.rSoft)
        '
        'rUpdate
        '
        Me.rUpdate.Text = " Actualizar CUI"
        '
        'rSoft
        '
        Me.rSoft.Text = "Salir"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tab_contacto)
        Me.TabControl1.Controls.Add(Me.tab_negocio)
        Me.TabControl1.Controls.Add(Me.tab_segmento)
        Me.TabControl1.Controls.Add(Me.tab_descuentos)
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(240, 268)
        Me.TabControl1.TabIndex = 0
        '
        'tab_contacto
        '
        Me.tab_contacto.Controls.Add(Me.panAtributoA)
        Me.tab_contacto.Controls.Add(Me.panValorA)
        Me.tab_contacto.Location = New System.Drawing.Point(0, 0)
        Me.tab_contacto.Name = "tab_contacto"
        Me.tab_contacto.Size = New System.Drawing.Size(240, 245)
        Me.tab_contacto.Text = "Contacto"
        '
        'panAtributoA
        '
        Me.panAtributoA.BackColor = System.Drawing.Color.LemonChiffon
        Me.panAtributoA.Controls.Add(Me.Label13)
        Me.panAtributoA.Controls.Add(Me.Label5)
        Me.panAtributoA.Controls.Add(Me.Label20)
        Me.panAtributoA.Controls.Add(Me.Label1)
        Me.panAtributoA.Controls.Add(Me.Label8)
        Me.panAtributoA.Controls.Add(Me.Label7)
        Me.panAtributoA.Controls.Add(Me.Label9)
        Me.panAtributoA.Controls.Add(Me.Label4)
        Me.panAtributoA.Controls.Add(Me.Label2)
        Me.panAtributoA.Dock = System.Windows.Forms.DockStyle.Left
        Me.panAtributoA.Location = New System.Drawing.Point(0, 0)
        Me.panAtributoA.Name = "panAtributoA"
        Me.panAtributoA.Size = New System.Drawing.Size(99, 245)
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label13.Location = New System.Drawing.Point(3, 214)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(93, 18)
        Me.Label13.Text = "Pasaporte:"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label5.Location = New System.Drawing.Point(3, 197)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 18)
        Me.Label5.Text = "CUI:"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label20.Location = New System.Drawing.Point(4, 41)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(93, 18)
        Me.Label20.Text = "Codigo:   "
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label1.Location = New System.Drawing.Point(4, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 18)
        Me.Label1.Text = "Negocio:      "
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label8.Location = New System.Drawing.Point(4, 179)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(93, 18)
        Me.Label8.Text = "Tels:"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label7.Location = New System.Drawing.Point(4, 108)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(93, 18)
        Me.Label7.Text = "Direccion:"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label9.Location = New System.Drawing.Point(4, 161)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(93, 18)
        Me.Label9.Text = "Nit:"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label4.Location = New System.Drawing.Point(4, 77)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 18)
        Me.Label4.Text = "Propietario:"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label2.Location = New System.Drawing.Point(4, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 18)
        Me.Label2.Text = "Categoria:     "
        '
        'panValorA
        '
        Me.panValorA.BackColor = System.Drawing.Color.AliceBlue
        Me.panValorA.Controls.Add(Me.lblcui)
        Me.panValorA.Controls.Add(Me.lblCodigo)
        Me.panValorA.Controls.Add(Me.lblTelefonos)
        Me.panValorA.Controls.Add(Me.lblNit)
        Me.panValorA.Controls.Add(Me.lblDireccion)
        Me.panValorA.Controls.Add(Me.lblPropietario)
        Me.panValorA.Controls.Add(Me.lblCategoria)
        Me.panValorA.Controls.Add(Me.lblNegocio)
        Me.panValorA.Controls.Add(Me.lblPasaporte)
        Me.panValorA.Dock = System.Windows.Forms.DockStyle.Right
        Me.panValorA.Location = New System.Drawing.Point(96, 0)
        Me.panValorA.Name = "panValorA"
        Me.panValorA.Size = New System.Drawing.Size(144, 245)
        '
        'lblcui
        '
        Me.lblcui.BackColor = System.Drawing.Color.AliceBlue
        Me.lblcui.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold)
        Me.lblcui.Location = New System.Drawing.Point(2, 198)
        Me.lblcui.Name = "lblcui"
        Me.lblcui.Size = New System.Drawing.Size(139, 18)
        '
        'lblCodigo
        '
        Me.lblCodigo.BackColor = System.Drawing.Color.AliceBlue
        Me.lblCodigo.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold)
        Me.lblCodigo.Location = New System.Drawing.Point(2, 41)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(139, 18)
        Me.lblCodigo.Text = "123456"
        '
        'lblTelefonos
        '
        Me.lblTelefonos.BackColor = System.Drawing.Color.AliceBlue
        Me.lblTelefonos.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold)
        Me.lblTelefonos.Location = New System.Drawing.Point(2, 179)
        Me.lblTelefonos.Name = "lblTelefonos"
        Me.lblTelefonos.Size = New System.Drawing.Size(139, 18)
        Me.lblTelefonos.Text = "54016570, 22325380"
        '
        'lblNit
        '
        Me.lblNit.BackColor = System.Drawing.Color.AliceBlue
        Me.lblNit.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold)
        Me.lblNit.Location = New System.Drawing.Point(2, 161)
        Me.lblNit.Name = "lblNit"
        Me.lblNit.Size = New System.Drawing.Size(139, 18)
        Me.lblNit.Text = "5732814-5"
        '
        'lblDireccion
        '
        Me.lblDireccion.BackColor = System.Drawing.Color.AliceBlue
        Me.lblDireccion.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold)
        Me.lblDireccion.Location = New System.Drawing.Point(2, 108)
        Me.lblDireccion.Name = "lblDireccion"
        Me.lblDireccion.Size = New System.Drawing.Size(139, 53)
        Me.lblDireccion.Text = "36 CALLE 10-45 ZONA 8 GUATEMALA GUATEMALA                                        " & _
            "                   "
        '
        'lblPropietario
        '
        Me.lblPropietario.BackColor = System.Drawing.Color.AliceBlue
        Me.lblPropietario.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold)
        Me.lblPropietario.Location = New System.Drawing.Point(2, 77)
        Me.lblPropietario.Name = "lblPropietario"
        Me.lblPropietario.Size = New System.Drawing.Size(139, 31)
        Me.lblPropietario.Text = "Gabriel Alejandro Diaz Lopez"
        '
        'lblCategoria
        '
        Me.lblCategoria.BackColor = System.Drawing.Color.AliceBlue
        Me.lblCategoria.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold)
        Me.lblCategoria.Location = New System.Drawing.Point(5, 59)
        Me.lblCategoria.Name = "lblCategoria"
        Me.lblCategoria.Size = New System.Drawing.Size(127, 18)
        Me.lblCategoria.Text = "C"
        '
        'lblNegocio
        '
        Me.lblNegocio.BackColor = System.Drawing.Color.AliceBlue
        Me.lblNegocio.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold)
        Me.lblNegocio.Location = New System.Drawing.Point(2, 7)
        Me.lblNegocio.Name = "lblNegocio"
        Me.lblNegocio.Size = New System.Drawing.Size(142, 34)
        Me.lblNegocio.Text = "VENTA DE LICORES LA BUENA ESPERANZA               "
        '
        'lblPasaporte
        '
        Me.lblPasaporte.BackColor = System.Drawing.Color.AliceBlue
        Me.lblPasaporte.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold)
        Me.lblPasaporte.Location = New System.Drawing.Point(2, 217)
        Me.lblPasaporte.Name = "lblPasaporte"
        Me.lblPasaporte.Size = New System.Drawing.Size(139, 18)
        '
        'tab_negocio
        '
        Me.tab_negocio.BackColor = System.Drawing.Color.AliceBlue
        Me.tab_negocio.Controls.Add(Me.panValor)
        Me.tab_negocio.Controls.Add(Me.panAtributo)
        Me.tab_negocio.Location = New System.Drawing.Point(0, 0)
        Me.tab_negocio.Name = "tab_negocio"
        Me.tab_negocio.Size = New System.Drawing.Size(232, 242)
        Me.tab_negocio.Text = "Negocio"
        '
        'panValor
        '
        Me.panValor.BackColor = System.Drawing.Color.AliceBlue
        Me.panValor.Controls.Add(Me.lblCondicion_)
        Me.panValor.Controls.Add(Me.lblListaPrecio_)
        Me.panValor.Controls.Add(Me.lstformasPago)
        Me.panValor.Controls.Add(Me.lblVentaConSaldoVencido)
        Me.panValor.Controls.Add(Me.lblVentaConSaldo)
        Me.panValor.Controls.Add(Me.lblPresupuesto)
        Me.panValor.Controls.Add(Me.lblDiasCredito)
        Me.panValor.Controls.Add(Me.lblCreditoDisponible)
        Me.panValor.Controls.Add(Me.lblCreditoAutorizado)
        Me.panValor.Controls.Add(Me.lblDiaVisita)
        Me.panValor.Dock = System.Windows.Forms.DockStyle.Right
        Me.panValor.Location = New System.Drawing.Point(88, 0)
        Me.panValor.Name = "panValor"
        Me.panValor.Size = New System.Drawing.Size(144, 242)
        '
        'lblCondicion_
        '
        Me.lblCondicion_.BackColor = System.Drawing.Color.AliceBlue
        Me.lblCondicion_.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold)
        Me.lblCondicion_.Location = New System.Drawing.Point(43, 134)
        Me.lblCondicion_.Name = "lblCondicion_"
        Me.lblCondicion_.Size = New System.Drawing.Size(97, 15)
        Me.lblCondicion_.Text = "1236-895653"
        Me.lblCondicion_.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblListaPrecio_
        '
        Me.lblListaPrecio_.BackColor = System.Drawing.Color.AliceBlue
        Me.lblListaPrecio_.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold)
        Me.lblListaPrecio_.Location = New System.Drawing.Point(43, 118)
        Me.lblListaPrecio_.Name = "lblListaPrecio_"
        Me.lblListaPrecio_.Size = New System.Drawing.Size(97, 15)
        Me.lblListaPrecio_.Text = "1236-895653"
        Me.lblListaPrecio_.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lstformasPago
        '
        Me.lstformasPago.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Regular)
        Me.lstformasPago.Location = New System.Drawing.Point(2, 151)
        Me.lstformasPago.Name = "lstformasPago"
        Me.lstformasPago.Size = New System.Drawing.Size(139, 74)
        Me.lstformasPago.TabIndex = 106
        '
        'lblVentaConSaldoVencido
        '
        Me.lblVentaConSaldoVencido.BackColor = System.Drawing.Color.AliceBlue
        Me.lblVentaConSaldoVencido.Enabled = False
        Me.lblVentaConSaldoVencido.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblVentaConSaldoVencido.ForeColor = System.Drawing.Color.Black
        Me.lblVentaConSaldoVencido.Location = New System.Drawing.Point(121, 101)
        Me.lblVentaConSaldoVencido.Name = "lblVentaConSaldoVencido"
        Me.lblVentaConSaldoVencido.Size = New System.Drawing.Size(20, 15)
        Me.lblVentaConSaldoVencido.TabIndex = 99
        '
        'lblVentaConSaldo
        '
        Me.lblVentaConSaldo.BackColor = System.Drawing.Color.AliceBlue
        Me.lblVentaConSaldo.Enabled = False
        Me.lblVentaConSaldo.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblVentaConSaldo.Location = New System.Drawing.Point(121, 86)
        Me.lblVentaConSaldo.Name = "lblVentaConSaldo"
        Me.lblVentaConSaldo.Size = New System.Drawing.Size(20, 15)
        Me.lblVentaConSaldo.TabIndex = 98
        '
        'lblPresupuesto
        '
        Me.lblPresupuesto.BackColor = System.Drawing.Color.AliceBlue
        Me.lblPresupuesto.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold)
        Me.lblPresupuesto.Location = New System.Drawing.Point(43, 70)
        Me.lblPresupuesto.Name = "lblPresupuesto"
        Me.lblPresupuesto.Size = New System.Drawing.Size(97, 15)
        Me.lblPresupuesto.Text = "1236-895653"
        Me.lblPresupuesto.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDiasCredito
        '
        Me.lblDiasCredito.BackColor = System.Drawing.Color.AliceBlue
        Me.lblDiasCredito.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold)
        Me.lblDiasCredito.Location = New System.Drawing.Point(43, 53)
        Me.lblDiasCredito.Name = "lblDiasCredito"
        Me.lblDiasCredito.Size = New System.Drawing.Size(97, 15)
        Me.lblDiasCredito.Text = "1236-895653"
        Me.lblDiasCredito.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCreditoDisponible
        '
        Me.lblCreditoDisponible.BackColor = System.Drawing.Color.AliceBlue
        Me.lblCreditoDisponible.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold)
        Me.lblCreditoDisponible.Location = New System.Drawing.Point(43, 37)
        Me.lblCreditoDisponible.Name = "lblCreditoDisponible"
        Me.lblCreditoDisponible.Size = New System.Drawing.Size(97, 15)
        Me.lblCreditoDisponible.Text = "1236-895653"
        Me.lblCreditoDisponible.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCreditoAutorizado
        '
        Me.lblCreditoAutorizado.BackColor = System.Drawing.Color.AliceBlue
        Me.lblCreditoAutorizado.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold)
        Me.lblCreditoAutorizado.Location = New System.Drawing.Point(43, 21)
        Me.lblCreditoAutorizado.Name = "lblCreditoAutorizado"
        Me.lblCreditoAutorizado.Size = New System.Drawing.Size(97, 15)
        Me.lblCreditoAutorizado.Text = "1236-895653"
        Me.lblCreditoAutorizado.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDiaVisita
        '
        Me.lblDiaVisita.BackColor = System.Drawing.Color.AliceBlue
        Me.lblDiaVisita.Font = New System.Drawing.Font("Tahoma", 7.5!, System.Drawing.FontStyle.Bold)
        Me.lblDiaVisita.Location = New System.Drawing.Point(43, 6)
        Me.lblDiaVisita.Name = "lblDiaVisita"
        Me.lblDiaVisita.Size = New System.Drawing.Size(97, 15)
        Me.lblDiaVisita.Text = "Lunes"
        Me.lblDiaVisita.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'panAtributo
        '
        Me.panAtributo.BackColor = System.Drawing.Color.LemonChiffon
        Me.panAtributo.Controls.Add(Me.lblCondicion)
        Me.panAtributo.Controls.Add(Me.lblListaPrecio)
        Me.panAtributo.Controls.Add(Me.Label24)
        Me.panAtributo.Controls.Add(Me.Label34)
        Me.panAtributo.Controls.Add(Me.Label32)
        Me.panAtributo.Controls.Add(Me.Label30)
        Me.panAtributo.Controls.Add(Me.Label28)
        Me.panAtributo.Controls.Add(Me.Label22)
        Me.panAtributo.Controls.Add(Me.Label18)
        Me.panAtributo.Controls.Add(Me.Label17)
        Me.panAtributo.Dock = System.Windows.Forms.DockStyle.Left
        Me.panAtributo.Location = New System.Drawing.Point(0, 0)
        Me.panAtributo.Name = "panAtributo"
        Me.panAtributo.Size = New System.Drawing.Size(99, 242)
        '
        'lblCondicion
        '
        Me.lblCondicion.BackColor = System.Drawing.Color.LemonChiffon
        Me.lblCondicion.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblCondicion.Location = New System.Drawing.Point(0, 129)
        Me.lblCondicion.Name = "lblCondicion"
        Me.lblCondicion.Size = New System.Drawing.Size(94, 17)
        Me.lblCondicion.Text = "Condicion:"
        '
        'lblListaPrecio
        '
        Me.lblListaPrecio.BackColor = System.Drawing.Color.LemonChiffon
        Me.lblListaPrecio.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblListaPrecio.Location = New System.Drawing.Point(0, 113)
        Me.lblListaPrecio.Name = "lblListaPrecio"
        Me.lblListaPrecio.Size = New System.Drawing.Size(94, 17)
        Me.lblListaPrecio.Text = "Lista Precio:"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label24.Location = New System.Drawing.Point(0, 97)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(94, 17)
        Me.Label24.Text = "Vta. Saldo V.:"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label34.Location = New System.Drawing.Point(0, 64)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(94, 17)
        Me.Label34.Text = "Presupuesto :"
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label32.Location = New System.Drawing.Point(0, 49)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(94, 17)
        Me.Label32.Text = "D. Credito:"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label30.Location = New System.Drawing.Point(0, 32)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(94, 17)
        Me.Label30.Text = "C. Disponible :"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label28.Location = New System.Drawing.Point(0, 16)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(94, 17)
        Me.Label28.Text = "C. Autorizado:"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label22.Location = New System.Drawing.Point(0, 1)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(94, 17)
        Me.Label22.Text = "Dia Visita:"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label18.Location = New System.Drawing.Point(0, 80)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(94, 17)
        Me.Label18.Text = "Vta. Saldo:"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label17.Location = New System.Drawing.Point(0, 145)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(94, 17)
        Me.Label17.Text = "Forma Pago:"
        '
        'tab_segmento
        '
        Me.tab_segmento.Controls.Add(Me.Panel3)
        Me.tab_segmento.Controls.Add(Me.Panel1)
        Me.tab_segmento.Controls.Add(Me.Panel5)
        Me.tab_segmento.Controls.Add(Me.TextBox3)
        Me.tab_segmento.Controls.Add(Me.TextBox1)
        Me.tab_segmento.Controls.Add(Me.TextBox2)
        Me.tab_segmento.Controls.Add(Me.lblTipoRuta)
        Me.tab_segmento.Controls.Add(Me.lblRegion)
        Me.tab_segmento.Controls.Add(Me.lblCanal)
        Me.tab_segmento.Controls.Add(Me.lblClaseCliente)
        Me.tab_segmento.Controls.Add(Me.lvProductos)
        Me.tab_segmento.Controls.Add(Me.Label3)
        Me.tab_segmento.Location = New System.Drawing.Point(0, 0)
        Me.tab_segmento.Name = "tab_segmento"
        Me.tab_segmento.Size = New System.Drawing.Size(232, 242)
        Me.tab_segmento.Text = "Segmento"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.MenuText
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Location = New System.Drawing.Point(0, 78)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(239, 1)
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.SystemColors.MenuText
        Me.Panel4.Location = New System.Drawing.Point(0, -2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(48, 0)
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.MenuText
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(0, 59)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(239, 1)
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.MenuText
        Me.Panel2.Location = New System.Drawing.Point(0, -2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(48, 0)
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.MenuText
        Me.Panel5.Controls.Add(Me.Panel6)
        Me.Panel5.Location = New System.Drawing.Point(0, 40)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(239, 1)
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.SystemColors.MenuText
        Me.Panel6.Location = New System.Drawing.Point(0, -2)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(48, 0)
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.LemonChiffon
        Me.TextBox3.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.TextBox3.Location = New System.Drawing.Point(0, 59)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(104, 19)
        Me.TextBox3.TabIndex = 15
        Me.TextBox3.Text = "T. RUTA"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.LemonChiffon
        Me.TextBox1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.TextBox1.Location = New System.Drawing.Point(0, 40)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(82, 19)
        Me.TextBox1.TabIndex = 13
        Me.TextBox1.Text = "CANAL"
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.LemonChiffon
        Me.TextBox2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.TextBox2.Location = New System.Drawing.Point(0, 21)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(60, 19)
        Me.TextBox2.TabIndex = 14
        Me.TextBox2.Text = "REGION"
        '
        'lblTipoRuta
        '
        Me.lblTipoRuta.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblTipoRuta.Location = New System.Drawing.Point(110, 61)
        Me.lblTipoRuta.Name = "lblTipoRuta"
        Me.lblTipoRuta.Size = New System.Drawing.Size(127, 20)
        Me.lblTipoRuta.Text = "Tipo de ruta"
        '
        'lblRegion
        '
        Me.lblRegion.ForeColor = System.Drawing.Color.Maroon
        Me.lblRegion.Location = New System.Drawing.Point(63, 23)
        Me.lblRegion.Name = "lblRegion"
        Me.lblRegion.Size = New System.Drawing.Size(174, 17)
        Me.lblRegion.Text = "Region"
        '
        'lblCanal
        '
        Me.lblCanal.ForeColor = System.Drawing.Color.Maroon
        Me.lblCanal.Location = New System.Drawing.Point(88, 42)
        Me.lblCanal.Name = "lblCanal"
        Me.lblCanal.Size = New System.Drawing.Size(149, 19)
        Me.lblCanal.Text = "Canal"
        '
        'lblClaseCliente
        '
        Me.lblClaseCliente.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblClaseCliente.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular)
        Me.lblClaseCliente.ForeColor = System.Drawing.Color.White
        Me.lblClaseCliente.Location = New System.Drawing.Point(0, 0)
        Me.lblClaseCliente.Name = "lblClaseCliente"
        Me.lblClaseCliente.Size = New System.Drawing.Size(240, 23)
        Me.lblClaseCliente.Text = "OFF/ON PREMISSE"
        Me.lblClaseCliente.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lvProductos
        '
        Me.lvProductos.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lvProductos.Location = New System.Drawing.Point(0, 106)
        Me.lvProductos.Name = "lvProductos"
        Me.lvProductos.Size = New System.Drawing.Size(241, 140)
        Me.lvProductos.TabIndex = 7
        Me.lvProductos.View = System.Windows.Forms.View.Details
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular)
        Me.Label3.ForeColor = System.Drawing.Color.Maroon
        Me.Label3.Location = New System.Drawing.Point(2, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(168, 19)
        Me.Label3.Text = "Productos Clave"
        '
        'tab_descuentos
        '
        Me.tab_descuentos.Controls.Add(Me.panDivH)
        Me.tab_descuentos.Controls.Add(Me.Panel7)
        Me.tab_descuentos.Controls.Add(Me.lblNombreCliente)
        Me.tab_descuentos.Location = New System.Drawing.Point(0, 0)
        Me.tab_descuentos.Name = "tab_descuentos"
        Me.tab_descuentos.Size = New System.Drawing.Size(232, 242)
        Me.tab_descuentos.Text = "Descuentos"
        '
        'panDivH
        '
        Me.panDivH.BackColor = System.Drawing.SystemColors.GrayText
        Me.panDivH.Location = New System.Drawing.Point(2, 23)
        Me.panDivH.Name = "panDivH"
        Me.panDivH.Size = New System.Drawing.Size(239, 1)
        '
        'Panel7
        '
        Me.Panel7.AutoScroll = True
        Me.Panel7.Controls.Add(Me.lblDescuentos)
        Me.Panel7.Location = New System.Drawing.Point(2, 27)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(236, 216)
        '
        'lblDescuentos
        '
        Me.lblDescuentos.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblDescuentos.Location = New System.Drawing.Point(0, 0)
        Me.lblDescuentos.Name = "lblDescuentos"
        Me.lblDescuentos.Size = New System.Drawing.Size(223, 357)
        Me.lblDescuentos.Text = "lblTexto descuentos"
        '
        'lblNombreCliente
        '
        Me.lblNombreCliente.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular)
        Me.lblNombreCliente.ForeColor = System.Drawing.Color.Maroon
        Me.lblNombreCliente.Location = New System.Drawing.Point(0, 0)
        Me.lblNombreCliente.Name = "lblNombreCliente"
        Me.lblNombreCliente.Size = New System.Drawing.Size(229, 26)
        Me.lblNombreCliente.Text = "<Nombre del cliente>"
        '
        'panel
        '
        Me.panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.panel.Controls.Add(Me.Label11)
        Me.panel.Controls.Add(Me.txtPasaporte)
        Me.panel.Controls.Add(Me.cmdProcesar)
        Me.panel.Controls.Add(Me.cmdAnular)
        Me.panel.Controls.Add(Me.Label10)
        Me.panel.Controls.Add(Me.txtcui)
        Me.panel.Controls.Add(Me.Label6)
        Me.panel.Controls.Add(Me.txtnit)
        Me.panel.Location = New System.Drawing.Point(7, 76)
        Me.panel.Name = "panel"
        Me.panel.Size = New System.Drawing.Size(221, 117)
        '
        'Label11
        '
        Me.Label11.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.Label11.Location = New System.Drawing.Point(4, 58)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(70, 21)
        Me.Label11.Text = "PASAPORTE"
        '
        'txtPasaporte
        '
        Me.txtPasaporte.ForeColor = System.Drawing.Color.Maroon
        Me.txtPasaporte.Location = New System.Drawing.Point(72, 58)
        Me.txtPasaporte.MaxLength = 20
        Me.txtPasaporte.Name = "txtPasaporte"
        Me.txtPasaporte.Size = New System.Drawing.Size(149, 21)
        Me.txtPasaporte.TabIndex = 10
        Me.txtPasaporte.WordWrap = False
        '
        'cmdProcesar
        '
        Me.cmdProcesar.Location = New System.Drawing.Point(82, 82)
        Me.cmdProcesar.Name = "cmdProcesar"
        Me.cmdProcesar.Size = New System.Drawing.Size(66, 29)
        Me.cmdProcesar.TabIndex = 6
        Me.cmdProcesar.Text = "Actualizar"
        '
        'cmdAnular
        '
        Me.cmdAnular.Location = New System.Drawing.Point(154, 82)
        Me.cmdAnular.Name = "cmdAnular"
        Me.cmdAnular.Size = New System.Drawing.Size(66, 29)
        Me.cmdAnular.TabIndex = 5
        Me.cmdAnular.Text = "Cancelar"
        '
        'Label10
        '
        Me.Label10.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.Label10.Location = New System.Drawing.Point(3, 33)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(29, 19)
        Me.Label10.Text = "CUI"
        '
        'txtcui
        '
        Me.txtcui.ForeColor = System.Drawing.Color.Maroon
        Me.txtcui.Location = New System.Drawing.Point(38, 31)
        Me.txtcui.MaxLength = 13
        Me.txtcui.Name = "txtcui"
        Me.txtcui.Size = New System.Drawing.Size(182, 21)
        Me.txtcui.TabIndex = 3
        Me.txtcui.WordWrap = False
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.Label6.Location = New System.Drawing.Point(4, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(28, 21)
        Me.Label6.Text = "NIT"
        '
        'txtnit
        '
        Me.txtnit.ForeColor = System.Drawing.Color.Maroon
        Me.txtnit.Location = New System.Drawing.Point(38, 4)
        Me.txtnit.Name = "txtnit"
        Me.txtnit.Size = New System.Drawing.Size(182, 21)
        Me.txtnit.TabIndex = 1
        Me.txtnit.WordWrap = False
        '
        'frmDetalleCliente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.ControlBox = False
        Me.Controls.Add(Me.panel)
        Me.Controls.Add(Me.TabControl1)
        Me.KeyPreview = True
        Me.Menu = Me.menuDetalleCliente
        Me.Name = "frmDetalleCliente"
        Me.Text = "Detalles del cliente"
        Me.TabControl1.ResumeLayout(False)
        Me.tab_contacto.ResumeLayout(False)
        Me.panAtributoA.ResumeLayout(False)
        Me.panValorA.ResumeLayout(False)
        Me.tab_negocio.ResumeLayout(False)
        Me.panValor.ResumeLayout(False)
        Me.panAtributo.ResumeLayout(False)
        Me.tab_segmento.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.tab_descuentos.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.panel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tab_contacto As System.Windows.Forms.TabPage
    Friend WithEvents tab_negocio As System.Windows.Forms.TabPage
    Friend WithEvents panValor As System.Windows.Forms.Panel
    Friend WithEvents lblVentaConSaldoVencido As System.Windows.Forms.CheckBox
    Friend WithEvents lblVentaConSaldo As System.Windows.Forms.CheckBox
    Friend WithEvents lblPresupuesto As System.Windows.Forms.Label
    Friend WithEvents lblDiasCredito As System.Windows.Forms.Label
    Friend WithEvents lblCreditoDisponible As System.Windows.Forms.Label
    Friend WithEvents lblCreditoAutorizado As System.Windows.Forms.Label
    Friend WithEvents lblDiaVisita As System.Windows.Forms.Label
    Friend WithEvents panAtributo As System.Windows.Forms.Panel
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents panValorA As System.Windows.Forms.Panel
    Friend WithEvents lblCodigo As System.Windows.Forms.Label
    Friend WithEvents lblTelefonos As System.Windows.Forms.Label
    Friend WithEvents lblNit As System.Windows.Forms.Label
    Friend WithEvents lblDireccion As System.Windows.Forms.Label
    Friend WithEvents lblPropietario As System.Windows.Forms.Label
    Friend WithEvents lblCategoria As System.Windows.Forms.Label
    Friend WithEvents lblNegocio As System.Windows.Forms.Label
    Friend WithEvents panAtributoA As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rUpdate As System.Windows.Forms.MenuItem
    Friend WithEvents rSoft As System.Windows.Forms.MenuItem
    Friend WithEvents lstformasPago As System.Windows.Forms.ListBox
    Friend WithEvents tab_segmento As System.Windows.Forms.TabPage
    Friend WithEvents lblCondicion_ As System.Windows.Forms.Label
    Friend WithEvents lblListaPrecio_ As System.Windows.Forms.Label
    Friend WithEvents lblCondicion As System.Windows.Forms.Label
    Friend WithEvents lblListaPrecio As System.Windows.Forms.Label
    Friend WithEvents lblClaseCliente As System.Windows.Forms.Label
    Friend WithEvents lblCanal As System.Windows.Forms.Label
    Friend WithEvents lblTipoRuta As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lvProductos As System.Windows.Forms.ListView
    Friend WithEvents lblRegion As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents tab_descuentos As System.Windows.Forms.TabPage
    Friend WithEvents lblDescuentos As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents panDivH As System.Windows.Forms.Panel
    Friend WithEvents lblNombreCliente As System.Windows.Forms.Label
    Friend WithEvents lblcui As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblPasaporte As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents panel As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtPasaporte As System.Windows.Forms.TextBox
    Friend WithEvents cmdProcesar As System.Windows.Forms.Button
    Friend WithEvents cmdAnular As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtcui As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtnit As System.Windows.Forms.TextBox
End Class
