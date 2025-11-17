<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmClientesSaldo
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
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.Label1 = New System.Windows.Forms.Label
        Me.lstClientesSaldo = New System.Windows.Forms.ListView
        Me.SuspendLayout()
        '
        'menuClientesSaldo
        '
        Me.menuClientesSaldo.MenuItems.Add(Me.MenuItem1)
        Me.menuClientesSaldo.MenuItems.Add(Me.MenuItem2)
        '
        'MenuItem1
        '
        Me.MenuItem1.Text = "Cancelar"
        '
        'MenuItem2
        '
        Me.MenuItem2.Text = " "
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Trebuchet MS", 13.0!, System.Drawing.FontStyle.Regular)
        Me.Label1.Location = New System.Drawing.Point(3, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(158, 46)
        Me.Label1.Text = "Consulta de clientes con saldo."
        '
        'lstClientesSaldo
        '
        Me.lstClientesSaldo.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lstClientesSaldo.FullRowSelect = True
        Me.lstClientesSaldo.Location = New System.Drawing.Point(0, 56)
        Me.lstClientesSaldo.Name = "lstClientesSaldo"
        Me.lstClientesSaldo.Size = New System.Drawing.Size(240, 212)
        Me.lstClientesSaldo.TabIndex = 2
        Me.lstClientesSaldo.View = System.Windows.Forms.View.Details
        '
        'frmClientesSaldo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.ControlBox = False
        Me.Controls.Add(Me.lstClientesSaldo)
        Me.Controls.Add(Me.Label1)
        Me.Menu = Me.menuClientesSaldo
        Me.Name = "frmClientesSaldo"
        Me.Text = "Clientes con saldo"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lstClientesSaldo As System.Windows.Forms.ListView
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
End Class
