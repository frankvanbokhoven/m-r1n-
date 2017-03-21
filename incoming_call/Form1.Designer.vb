<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.rtbMessages = New System.Windows.Forms.RichTextBox()
        Me.txtUser = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPasswd = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtProxy = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtDomain = New System.Windows.Forms.TextBox()
        Me.btnRegister = New System.Windows.Forms.Button()
        Me.btnCall = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'rtbMessages
        '
        Me.rtbMessages.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbMessages.Location = New System.Drawing.Point(17, 165)
        Me.rtbMessages.Margin = New System.Windows.Forms.Padding(4)
        Me.rtbMessages.Name = "rtbMessages"
        Me.rtbMessages.Size = New System.Drawing.Size(753, 242)
        Me.rtbMessages.TabIndex = 0
        Me.rtbMessages.Text = ""
        '
        'txtUser
        '
        Me.txtUser.Location = New System.Drawing.Point(155, 82)
        Me.txtUser.Margin = New System.Windows.Forms.Padding(4)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(337, 22)
        Me.txtUser.TabIndex = 6
        Me.txtUser.Text = "1012"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 86)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 17)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "User / Extension"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 118)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 17)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Password"
        '
        'txtPasswd
        '
        Me.txtPasswd.Location = New System.Drawing.Point(155, 114)
        Me.txtPasswd.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPasswd.Name = "txtPasswd"
        Me.txtPasswd.Size = New System.Drawing.Size(337, 22)
        Me.txtPasswd.TabIndex = 8
        Me.txtPasswd.Text = "1234"
        Me.txtPasswd.UseSystemPasswordChar = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(24, 50)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(113, 17)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Proxy / Registrar"
        '
        'txtProxy
        '
        Me.txtProxy.Location = New System.Drawing.Point(155, 47)
        Me.txtProxy.Margin = New System.Windows.Forms.Padding(4)
        Me.txtProxy.Name = "txtProxy"
        Me.txtProxy.Size = New System.Drawing.Size(337, 22)
        Me.txtProxy.TabIndex = 12
        Me.txtProxy.Text = "10.0.128.128"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(24, 18)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(108, 17)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Realm / Domain"
        '
        'txtDomain
        '
        Me.txtDomain.Location = New System.Drawing.Point(155, 15)
        Me.txtDomain.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDomain.Name = "txtDomain"
        Me.txtDomain.Size = New System.Drawing.Size(337, 22)
        Me.txtDomain.TabIndex = 10
        Me.txtDomain.Text = "unet"
        '
        'btnRegister
        '
        Me.btnRegister.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRegister.Location = New System.Drawing.Point(568, 112)
        Me.btnRegister.Margin = New System.Windows.Forms.Padding(4)
        Me.btnRegister.Name = "btnRegister"
        Me.btnRegister.Size = New System.Drawing.Size(204, 28)
        Me.btnRegister.TabIndex = 14
        Me.btnRegister.Text = "Register!"
        Me.btnRegister.UseVisualStyleBackColor = True
        '
        'btnCall
        '
        Me.btnCall.Location = New System.Drawing.Point(571, 22)
        Me.btnCall.Name = "btnCall"
        Me.btnCall.Size = New System.Drawing.Size(43, 44)
        Me.btnCall.TabIndex = 15
        Me.btnCall.Text = "Call"
        Me.btnCall.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(787, 422)
        Me.Controls.Add(Me.btnCall)
        Me.Controls.Add(Me.btnRegister)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtProxy)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtDomain)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtPasswd)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.rtbMessages)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rtbMessages As System.Windows.Forms.RichTextBox
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPasswd As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtProxy As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDomain As System.Windows.Forms.TextBox
    Friend WithEvents btnRegister As System.Windows.Forms.Button
    Friend WithEvents btnCall As Button
End Class
