<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class formMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.groupSimSettings = New System.Windows.Forms.GroupBox()
        Me.labelPackType = New System.Windows.Forms.Label()
        Me.comboPackType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.textSimPacks = New System.Windows.Forms.TextBox()
        Me.textSimCount = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.groupSimSettings.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(19, 112)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Run Sim"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(101, 112)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Draw GLeg"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'groupSimSettings
        '
        Me.groupSimSettings.Controls.Add(Me.Button3)
        Me.groupSimSettings.Controls.Add(Me.Button2)
        Me.groupSimSettings.Controls.Add(Me.textSimCount)
        Me.groupSimSettings.Controls.Add(Me.Button1)
        Me.groupSimSettings.Controls.Add(Me.Label2)
        Me.groupSimSettings.Controls.Add(Me.textSimPacks)
        Me.groupSimSettings.Controls.Add(Me.Label1)
        Me.groupSimSettings.Controls.Add(Me.comboPackType)
        Me.groupSimSettings.Controls.Add(Me.labelPackType)
        Me.groupSimSettings.Location = New System.Drawing.Point(12, 12)
        Me.groupSimSettings.Name = "groupSimSettings"
        Me.groupSimSettings.Size = New System.Drawing.Size(272, 147)
        Me.groupSimSettings.TabIndex = 3
        Me.groupSimSettings.TabStop = False
        Me.groupSimSettings.Text = "Simulation Settings"
        '
        'labelPackType
        '
        Me.labelPackType.AutoSize = True
        Me.labelPackType.Location = New System.Drawing.Point(16, 22)
        Me.labelPackType.Name = "labelPackType"
        Me.labelPackType.Size = New System.Drawing.Size(59, 13)
        Me.labelPackType.TabIndex = 0
        Me.labelPackType.Text = "Pack Type"
        '
        'comboPackType
        '
        Me.comboPackType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboPackType.FormattingEnabled = True
        Me.comboPackType.Items.AddRange(New Object() {"Classic", "Goblins vs. Gnomes", "The Grand Tournament"})
        Me.comboPackType.Location = New System.Drawing.Point(111, 19)
        Me.comboPackType.Name = "comboPackType"
        Me.comboPackType.Size = New System.Drawing.Size(146, 21)
        Me.comboPackType.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Open                 packs"
        '
        'textSimPacks
        '
        Me.textSimPacks.Location = New System.Drawing.Point(48, 51)
        Me.textSimPacks.Name = "textSimPacks"
        Me.textSimPacks.Size = New System.Drawing.Size(42, 20)
        Me.textSimPacks.TabIndex = 3
        Me.textSimPacks.Text = "50"
        '
        'textSimCount
        '
        Me.textSimCount.Location = New System.Drawing.Point(59, 80)
        Me.textSimCount.Name = "textSimCount"
        Me.textSimCount.Size = New System.Drawing.Size(52, 20)
        Me.textSimCount.TabIndex = 4
        Me.textSimCount.Text = "50"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(154, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Perform                    simulations"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(182, 112)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 6
        Me.Button3.Text = "Verbose Sim"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 165)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(271, 23)
        Me.ProgressBar1.TabIndex = 4
        '
        'formMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(295, 198)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.groupSimSettings)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "formMain"
        Me.Text = "Simple Pack Simulator"
        Me.groupSimSettings.ResumeLayout(False)
        Me.groupSimSettings.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents groupSimSettings As GroupBox
    Friend WithEvents Button3 As Button
    Friend WithEvents textSimCount As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents textSimPacks As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents comboPackType As ComboBox
    Friend WithEvents labelPackType As Label
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
