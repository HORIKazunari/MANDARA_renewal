<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain_Autocorrelation
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnGetNeighboutObj = New System.Windows.Forms.Button()
        Me.btnTextCopy = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(375, 280)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(99, 24)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "終了"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(-311, 39)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(190, 151)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "縦軸データ"
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(18, 30)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(190, 151)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "対象データ"
        '
        'btnGetNeighboutObj
        '
        Me.btnGetNeighboutObj.Location = New System.Drawing.Point(46, 202)
        Me.btnGetNeighboutObj.Name = "btnGetNeighboutObj"
        Me.btnGetNeighboutObj.Size = New System.Drawing.Size(115, 44)
        Me.btnGetNeighboutObj.TabIndex = 12
        Me.btnGetNeighboutObj.Text = "隣接オブジェクトをデータ項目に取得"
        Me.btnGetNeighboutObj.UseVisualStyleBackColor = True
        '
        'btnTextCopy
        '
        Me.btnTextCopy.Location = New System.Drawing.Point(227, 280)
        Me.btnTextCopy.Name = "btnTextCopy"
        Me.btnTextCopy.Size = New System.Drawing.Size(99, 23)
        Me.btnTextCopy.TabIndex = 13
        Me.btnTextCopy.Text = "コピー"
        Me.btnTextCopy.UseVisualStyleBackColor = True
        '
        'frmMain_Autocorrelation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(496, 315)
        Me.Controls.Add(Me.btnTextCopy)
        Me.Controls.Add(Me.btnGetNeighboutObj)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpButton = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain_Autocorrelation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "空間的自己相関分析"
        AddHandler Load, AddressOf Me.frmMain_Autocorrelation_Load
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lbYData As mandara10.ListBoxEx
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ListBoxEx As mandara10.ListBoxEx
    Friend WithEvents ListViewEX As mandara10.ListViewEX
    Friend WithEvents btnGetNeighboutObj As System.Windows.Forms.Button
    Friend WithEvents btnTextCopy As System.Windows.Forms.Button
End Class
