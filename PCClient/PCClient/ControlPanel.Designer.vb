<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ControlPanel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ControlPanel))
        Me.AddReminder = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DateI = New System.Windows.Forms.DateTimePicker()
        Me.TimeI = New System.Windows.Forms.DateTimePicker()
        Me.ReminderI = New System.Windows.Forms.TextBox()
        Me.RemindersT = New System.Windows.Forms.TextBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.InternetI = New System.Windows.Forms.PictureBox()
        Me.InternetT = New System.Windows.Forms.Label()
        Me.ExitBTN = New System.Windows.Forms.Button()
        Me.LogoutBTN = New System.Windows.Forms.Button()
        Me.InternetN = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.InternetI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.InternetN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AddReminder
        '
        Me.AddReminder.BackColor = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.AddReminder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AddReminder.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddReminder.ForeColor = System.Drawing.Color.White
        Me.AddReminder.Location = New System.Drawing.Point(364, 323)
        Me.AddReminder.Name = "AddReminder"
        Me.AddReminder.Size = New System.Drawing.Size(105, 41)
        Me.AddReminder.TabIndex = 0
        Me.AddReminder.Text = "Add"
        Me.AddReminder.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Location = New System.Drawing.Point(46, 98)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(234, 282)
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(39, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(157, 33)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Reminders"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(297, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(201, 33)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Add Reminder"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.White
        Me.PictureBox2.Location = New System.Drawing.Point(304, 98)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(234, 282)
        Me.PictureBox2.TabIndex = 3
        Me.PictureBox2.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label3.Location = New System.Drawing.Point(330, 113)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 19)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Reminder"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label4.Location = New System.Drawing.Point(330, 179)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 19)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Time"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label5.Location = New System.Drawing.Point(330, 245)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 19)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Date"
        '
        'DateI
        '
        Me.DateI.Location = New System.Drawing.Point(334, 281)
        Me.DateI.Name = "DateI"
        Me.DateI.Size = New System.Drawing.Size(170, 21)
        Me.DateI.TabIndex = 12
        '
        'TimeI
        '
        Me.TimeI.Checked = False
        Me.TimeI.Location = New System.Drawing.Point(334, 218)
        Me.TimeI.Name = "TimeI"
        Me.TimeI.Size = New System.Drawing.Size(171, 21)
        Me.TimeI.TabIndex = 13
        '
        'ReminderI
        '
        Me.ReminderI.Font = New System.Drawing.Font("Calibri", 14.0!)
        Me.ReminderI.Location = New System.Drawing.Point(334, 141)
        Me.ReminderI.Name = "ReminderI"
        Me.ReminderI.Size = New System.Drawing.Size(170, 30)
        Me.ReminderI.TabIndex = 14
        '
        'RemindersT
        '
        Me.RemindersT.Location = New System.Drawing.Point(58, 121)
        Me.RemindersT.Multiline = True
        Me.RemindersT.Name = "RemindersT"
        Me.RemindersT.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.RemindersT.Size = New System.Drawing.Size(208, 243)
        Me.RemindersT.TabIndex = 15
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.White
        Me.PictureBox3.Location = New System.Drawing.Point(0, -1)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(761, 50)
        Me.PictureBox3.TabIndex = 16
        Me.PictureBox3.TabStop = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Font = New System.Drawing.Font("Calibri", 27.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(298, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(156, 46)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "PCClient"
        '
        'InternetI
        '
        Me.InternetI.Image = CType(resources.GetObject("InternetI.Image"), System.Drawing.Image)
        Me.InternetI.Location = New System.Drawing.Point(674, 68)
        Me.InternetI.Name = "InternetI"
        Me.InternetI.Size = New System.Drawing.Size(76, 64)
        Me.InternetI.TabIndex = 17
        Me.InternetI.TabStop = False
        '
        'InternetT
        '
        Me.InternetT.AutoSize = True
        Me.InternetT.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InternetT.ForeColor = System.Drawing.Color.White
        Me.InternetT.Location = New System.Drawing.Point(554, 89)
        Me.InternetT.Name = "InternetT"
        Me.InternetT.Size = New System.Drawing.Size(92, 23)
        Me.InternetT.TabIndex = 18
        Me.InternetT.Text = "Connected"
        '
        'ExitBTN
        '
        Me.ExitBTN.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExitBTN.Location = New System.Drawing.Point(638, 347)
        Me.ExitBTN.Name = "ExitBTN"
        Me.ExitBTN.Size = New System.Drawing.Size(112, 43)
        Me.ExitBTN.TabIndex = 19
        Me.ExitBTN.Text = "Exit"
        Me.ExitBTN.UseVisualStyleBackColor = True
        '
        'LogoutBTN
        '
        Me.LogoutBTN.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LogoutBTN.Location = New System.Drawing.Point(638, 298)
        Me.LogoutBTN.Name = "LogoutBTN"
        Me.LogoutBTN.Size = New System.Drawing.Size(112, 43)
        Me.LogoutBTN.TabIndex = 19
        Me.LogoutBTN.Text = "Logout"
        Me.LogoutBTN.UseVisualStyleBackColor = True
        '
        'InternetN
        '
        Me.InternetN.Image = CType(resources.GetObject("InternetN.Image"), System.Drawing.Image)
        Me.InternetN.Location = New System.Drawing.Point(674, 68)
        Me.InternetN.Name = "InternetN"
        Me.InternetN.Size = New System.Drawing.Size(76, 64)
        Me.InternetN.TabIndex = 20
        Me.InternetN.TabStop = False
        Me.InternetN.Visible = False
        '
        'ControlPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(760, 402)
        Me.Controls.Add(Me.InternetN)
        Me.Controls.Add(Me.LogoutBTN)
        Me.Controls.Add(Me.ExitBTN)
        Me.Controls.Add(Me.InternetT)
        Me.Controls.Add(Me.InternetI)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.RemindersT)
        Me.Controls.Add(Me.ReminderI)
        Me.Controls.Add(Me.TimeI)
        Me.Controls.Add(Me.DateI)
        Me.Controls.Add(Me.AddReminder)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ControlPanel"
        Me.Text = "Control Panel"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.InternetI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.InternetN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents AddReminder As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents DateI As DateTimePicker
    Friend WithEvents TimeI As DateTimePicker
    Friend WithEvents ReminderI As TextBox
    Friend WithEvents RemindersT As TextBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents Label6 As Label
    Friend WithEvents InternetI As PictureBox
    Friend WithEvents InternetT As Label
    Friend WithEvents ExitBTN As Button
    Friend WithEvents LogoutBTN As Button
    Friend WithEvents InternetN As PictureBox
End Class
