<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class reminderWindow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(reminderWindow))
        Me.DismissB = New System.Windows.Forms.Button()
        Me.ReminderTextL = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'DismissB
        '
        Me.DismissB.Location = New System.Drawing.Point(306, 213)
        Me.DismissB.Name = "DismissB"
        Me.DismissB.Size = New System.Drawing.Size(140, 51)
        Me.DismissB.TabIndex = 0
        Me.DismissB.Text = "Dismiss"
        Me.DismissB.UseVisualStyleBackColor = True
        '
        'ReminderTextL
        '
        Me.ReminderTextL.AutoSize = True
        Me.ReminderTextL.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReminderTextL.Location = New System.Drawing.Point(278, 98)
        Me.ReminderTextL.Name = "ReminderTextL"
        Me.ReminderTextL.Size = New System.Drawing.Size(289, 69)
        Me.ReminderTextL.TabIndex = 1
        Me.ReminderTextL.Text = "Reminder"
        Me.ReminderTextL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'reminderWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Red
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.ControlBox = False
        Me.Controls.Add(Me.ReminderTextL)
        Me.Controls.Add(Me.DismissB)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "reminderWindow"
        Me.Opacity = 0.75R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reminder"
        Me.TransparencyKey = System.Drawing.SystemColors.Control
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DismissB As Button
    Friend WithEvents ReminderTextL As Label
End Class
