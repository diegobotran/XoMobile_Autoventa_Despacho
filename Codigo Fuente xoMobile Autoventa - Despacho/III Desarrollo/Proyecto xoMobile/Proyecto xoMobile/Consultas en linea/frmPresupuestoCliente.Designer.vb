<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmPresupuestoCliente
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
        Me.menuClientesSaldo = New System.Windows.Forms.MainMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.mnuImprimir = New System.Windows.Forms.MenuItem
        Me.Label1 = New System.Windows.Forms.Label
        Me.lstPresupuesto = New System.Windows.Forms.ListView
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
        Me.mnuImprimir.Text = " Imprimir"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Trebuchet MS", 13.0!, System.Drawing.FontStyle.Regular)
        Me.Label1.Location = New System.Drawing.Point(10, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(205, 23)
        Me.Label1.Text = "Presupuesto Cliente"
        '
        'lstPresupuesto
        '
        Me.lstPresupuesto.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lstPresupuesto.FullRowSelect = True
        Me.lstPresupuesto.Location = New System.Drawing.Point(0, 26)
        Me.lstPresupuesto.Name = "lstPresupuesto"
        Me.lstPresupuesto.Size = New System.Drawing.Size(240, 239)
        Me.lstPresupuesto.TabIndex = 2
        Me.lstPresupuesto.View = System.Windows.Forms.View.Details
        '
        'frmPresupuestoCliente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.ControlBox = False
        Me.Controls.Add(Me.lstPresupuesto)
        Me.Controls.Add(Me.Label1)
        Me.Menu = Me.menuClientesSaldo
        Me.Name = "frmPresupuestoCliente"
        Me.Text = "Clientes con saldo"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lstPresupuesto As System.Windows.Forms.ListView
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuImprimir As System.Windows.Forms.MenuItem
End Class
