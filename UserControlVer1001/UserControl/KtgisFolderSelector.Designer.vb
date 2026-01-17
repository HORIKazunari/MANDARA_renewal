<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class KtgisFolderSelector
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
        Me.gbFolderSelector = New System.Windows.Forms.GroupBox()
        Me.txtFolder = New System.Windows.Forms.TextBox()
        Me.btnRef = New System.Windows.Forms.Button()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.gbFolderSelector.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbFolderSelector
        '
        Me.gbFolderSelector.Controls.Add(Me.txtFolder)
        Me.gbFolderSelector.Controls.Add(Me.btnRef)
        Me.gbFolderSelector.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFolderSelector.Location = New System.Drawing.Point(0, 0)
        Me.gbFolderSelector.Name = "gbFolderSelector"
        Me.gbFolderSelector.Size = New System.Drawing.Size(341, 43)
        Me.gbFolderSelector.TabIndex = 7
        Me.gbFolderSelector.TabStop = False
        Me.gbFolderSelector.Text = "FolderSelector"
        '
        'txtFolder
        '
        Me.txtFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFolder.Location = New System.Drawing.Point(3, 15)
        Me.txtFolder.Margin = New System.Windows.Forms.Padding(5, 3, 3, 3)
        Me.txtFolder.Name = "txtFolder"
        Me.txtFolder.Size = New System.Drawing.Size(283, 19)
        Me.txtFolder.TabIndex = 7
        '
        'btnRef
        '
        Me.btnRef.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRef.Location = New System.Drawing.Point(292, 15)
        Me.btnRef.Name = "btnRef"
        Me.btnRef.Size = New System.Drawing.Size(46, 19)
        Me.btnRef.TabIndex = 8
        Me.btnRef.Text = "設定"
        Me.btnRef.UseVisualStyleBackColor = True
        '
        'ToolTip
        '
        Me.ToolTip.AutoPopDelay = 5000
        Me.ToolTip.InitialDelay = 10
        Me.ToolTip.ReshowDelay = 100
        Me.ToolTip.ShowAlways = True
        '
        'KtgisFolderSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gbFolderSelector)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "KtgisFolderSelector"
        Me.Size = New System.Drawing.Size(341, 43)
        Me.gbFolderSelector.ResumeLayout(False)
        Me.gbFolderSelector.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbFolderSelector As System.Windows.Forms.GroupBox
    Friend WithEvents txtFolder As System.Windows.Forms.TextBox
    Friend WithEvents btnRef As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip

End Class
