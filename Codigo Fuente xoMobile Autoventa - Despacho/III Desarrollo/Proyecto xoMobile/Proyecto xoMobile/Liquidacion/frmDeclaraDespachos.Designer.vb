<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmDeclaraDespachos
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
        Me.mainMenu1 = New System.Windows.Forms.MainMenu
        Me.cmdSalir = New System.Windows.Forms.MenuItem
        Me.cmdConfirmar = New System.Windows.Forms.MenuItem
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.lstDespachos = New System.Windows.Forms.ListView
        Me.lblDespachados = New System.Windows.Forms.Label
        Me.lblEntregados = New System.Windows.Forms.Label
        Me.lblEfectividad = New System.Windows.Forms.Label
        Me.lblNprogramados = New System.Windows.Forms.Label
        Me.lblNentregados = New System.Windows.Forms.Label
        Me.lblNefectividad = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'mainMenu1
        '
        Me.mainMenu1.MenuItems.Add(Me.cmdSalir)
        Me.mainMenu1.MenuItems.Add(Me.cmdConfirmar)
        '
        'cmdSalir
        '
        Me.cmdSalir.Text = "Salir"
        '
        'cmdConfirmar
        '
        Me.cmdConfirmar.Text = "Confirmar"
        '
        'lblTitulo
        '
        Me.lblTitulo.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Regular)
        Me.lblTitulo.Location = New System.Drawing.Point(76, 0)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(158, 41)
        Me.lblTitulo.Text = "Despachos Entregados"
        Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lstDespachos
        '
        Me.lstDespachos.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lstDespachos.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstDespachos.Location = New System.Drawing.Point(0, 47)
        Me.lstDespachos.Name = "lstDespachos"
        Me.lstDespachos.Size = New System.Drawing.Size(240, 183)
        Me.lstDespachos.TabIndex = 11
        Me.lstDespachos.View = System.Windows.Forms.View.Details
        '
        'lblDespachados
        '
        Me.lblDespachados.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblDespachados.Location = New System.Drawing.Point(0, 251)
        Me.lblDespachados.Name = "lblDespachados"
        Me.lblDespachados.Size = New System.Drawing.Size(100, 15)
        Me.lblDespachados.Text = "Programados:"
        '
        'lblEntregados
        '
        Me.lblEntregados.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblEntregados.Location = New System.Drawing.Point(1, 233)
        Me.lblEntregados.Name = "lblEntregados"
        Me.lblEntregados.Size = New System.Drawing.Size(100, 15)
        Me.lblEntregados.Text = "Entregados:"
        '
        'lblEfectividad
        '
        Me.lblEfectividad.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblEfectividad.Location = New System.Drawing.Point(114, 252)
        Me.lblEfectividad.Name = "lblEfectividad"
        Me.lblEfectividad.Size = New System.Drawing.Size(100, 15)
        Me.lblEfectividad.Text = "Efectividad:"
        '
        'lblNprogramados
        '
        Me.lblNprogramados.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblNprogramados.ForeColor = System.Drawing.Color.Maroon
        Me.lblNprogramados.Location = New System.Drawing.Point(77, 251)
        Me.lblNprogramados.Name = "lblNprogramados"
        Me.lblNprogramados.Size = New System.Drawing.Size(30, 15)
        Me.lblNprogramados.Text = "100."
        '
        'lblNentregados
        '
        Me.lblNentregados.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblNentregados.ForeColor = System.Drawing.Color.Maroon
        Me.lblNentregados.Location = New System.Drawing.Point(77, 233)
        Me.lblNentregados.Name = "lblNentregados"
        Me.lblNentregados.Size = New System.Drawing.Size(40, 15)
        Me.lblNentregados.Text = "100."
        '
        'lblNefectividad
        '
        Me.lblNefectividad.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lblNefectividad.ForeColor = System.Drawing.Color.DarkRed
        Me.lblNefectividad.Location = New System.Drawing.Point(187, 253)
        Me.lblNefectividad.Name = "lblNefectividad"
        Me.lblNefectividad.Size = New System.Drawing.Size(53, 15)
        Me.lblNefectividad.Text = "66.66%"
        '
        'frmDeclaraDespachos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblNefectividad)
        Me.Controls.Add(Me.lblNentregados)
        Me.Controls.Add(Me.lblNprogramados)
        Me.Controls.Add(Me.lblEfectividad)
        Me.Controls.Add(Me.lblEntregados)
        Me.Controls.Add(Me.lblDespachados)
        Me.Controls.Add(Me.lstDespachos)
        Me.Controls.Add(Me.lblTitulo)
        Me.Menu = Me.mainMenu1
        Me.Name = "frmDeclaraDespachos"
        Me.Text = "Despachos"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents lstDespachos As System.Windows.Forms.ListView
    Friend WithEvents lblDespachados As System.Windows.Forms.Label
    Friend WithEvents lblEntregados As System.Windows.Forms.Label
    Friend WithEvents lblEfectividad As System.Windows.Forms.Label
    Friend WithEvents lblNprogramados As System.Windows.Forms.Label
    Friend WithEvents lblNentregados As System.Windows.Forms.Label
    Friend WithEvents lblNefectividad As System.Windows.Forms.Label
    Friend WithEvents cmdSalir As System.Windows.Forms.MenuItem
    Friend WithEvents cmdConfirmar As System.Windows.Forms.MenuItem
End Class
