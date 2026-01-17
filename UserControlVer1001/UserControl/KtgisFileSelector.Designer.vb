<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class KtgisFileSelector
    Inherits System.Windows.Forms.UserControl

    'UserControl はコンポーネント一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.gbFileSelector = New System.Windows.Forms.GroupBox()
        Me.btnRef = New System.Windows.Forms.Button()
        Me.btnOff = New System.Windows.Forms.Button()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.gbFileSelector.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbFileSelector
        '
        Me.gbFileSelector.Controls.Add(Me.btnRef)
        Me.gbFileSelector.Controls.Add(Me.btnOff)
        Me.gbFileSelector.Controls.Add(Me.txtPath)
        Me.gbFileSelector.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFileSelector.Location = New System.Drawing.Point(0, 0)
        Me.gbFileSelector.Name = "gbFileSelector"
        Me.gbFileSelector.Size = New System.Drawing.Size(296, 57)
        Me.gbFileSelector.TabIndex = 6
        Me.gbFileSelector.TabStop = False
        Me.gbFileSelector.Text = "FileSelector"
        '
        'btnRef
        '
        Me.btnRef.Location = New System.Drawing.Point(194, 14)
        Me.btnRef.Name = "btnRef"
        Me.btnRef.Size = New System.Drawing.Size(46, 21)
        Me.btnRef.TabIndex = 8
        Me.btnRef.Text = "設定"
        Me.btnRef.UseVisualStyleBackColor = True
        '
        'btnOff
        '
        Me.btnOff.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOff.Location = New System.Drawing.Point(245, 14)
        Me.btnOff.Margin = New System.Windows.Forms.Padding(2)
        Me.btnOff.Name = "btnOff"
        Me.btnOff.Size = New System.Drawing.Size(46, 21)
        Me.btnOff.TabIndex = 9
        Me.btnOff.Text = "解除"
        Me.btnOff.UseVisualStyleBackColor = True
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(3, 15)
        Me.txtPath.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(187, 19)
        Me.txtPath.TabIndex = 7
        '
        'ToolTip
        '
        Me.ToolTip.AutoPopDelay = 5000
        Me.ToolTip.InitialDelay = 10
        Me.ToolTip.ReshowDelay = 100
        Me.ToolTip.ShowAlways = True
        '
        'KtgisFileSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gbFileSelector)
        Me.Name = "KtgisFileSelector"
        Me.Size = New System.Drawing.Size(296, 57)
        Me.gbFileSelector.ResumeLayout(False)
        Me.gbFileSelector.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbFileSelector As System.Windows.Forms.GroupBox
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents btnOff As System.Windows.Forms.Button
    Friend WithEvents btnRef As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip

End Class
