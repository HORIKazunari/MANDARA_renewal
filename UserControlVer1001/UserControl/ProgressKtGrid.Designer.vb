<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class KTGISGrid
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
        Me.pnlGridFrame = New System.Windows.Forms.Panel()
        Me.txtTextBox = New System.Windows.Forms.TextBox()
        Me.VscrollGrid = New System.Windows.Forms.VScrollBar()
        Me.HScrollGrid = New System.Windows.Forms.HScrollBar()
        Me.picGrid = New System.Windows.Forms.PictureBox()
        Me.ContextMenuStripTabGrid = New System.Windows.Forms.ContextMenuStrip()
        Me.mnuUndo = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPaste = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCut = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuRowNumber = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuColNumber = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuInsertRow = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuInsertRowUpword = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuInsertRowDownward = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuInsertCol = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuInsertColLeft = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuInsertColRight = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuRowHeight = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuColWidth = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuDeleteRow = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDeleteCol = New System.Windows.Forms.ToolStripMenuItem()
        Me.SSTab = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.ContextMenuStripTab = New System.Windows.Forms.ContextMenuStrip()
        Me.mnuTabRightMenuChangeTabName = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTabRightMenuMoveTab = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTabRightMenuMoveTabLeft = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTabRightMenuMoveTabRight = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTabRightMenuMoveTabAhead = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTabRightMenuMoveTabRear = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTabRightMenuInsertTab = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTabRightMenuInsertTabLeft = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTabRightMenuInsertTabRight = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTabRightMenuDeleteTab = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimerMouse = New System.Windows.Forms.Timer()
        Me.pnlGridFrame.SuspendLayout()
        CType(Me.picGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStripTabGrid.SuspendLayout()
        Me.SSTab.SuspendLayout()
        Me.ContextMenuStripTab.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlGridFrame
        '
        Me.pnlGridFrame.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.pnlGridFrame.Controls.Add(Me.txtTextBox)
        Me.pnlGridFrame.Controls.Add(Me.VscrollGrid)
        Me.pnlGridFrame.Controls.Add(Me.HScrollGrid)
        Me.pnlGridFrame.Controls.Add(Me.picGrid)
        Me.pnlGridFrame.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGridFrame.Location = New System.Drawing.Point(0, 22)
        Me.pnlGridFrame.Margin = New System.Windows.Forms.Padding(2)
        Me.pnlGridFrame.Name = "pnlGridFrame"
        Me.pnlGridFrame.Size = New System.Drawing.Size(530, 394)
        Me.pnlGridFrame.TabIndex = 4
        '
        'txtTextBox
        '
        Me.txtTextBox.Location = New System.Drawing.Point(9, 29)
        Me.txtTextBox.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTextBox.Multiline = True
        Me.txtTextBox.Name = "txtTextBox"
        Me.txtTextBox.Size = New System.Drawing.Size(74, 34)
        Me.txtTextBox.TabIndex = 4
        '
        'VscrollGrid
        '
        Me.VscrollGrid.LargeChange = 1
        Me.VscrollGrid.Location = New System.Drawing.Point(251, 38)
        Me.VscrollGrid.Name = "VscrollGrid"
        Me.VscrollGrid.Size = New System.Drawing.Size(19, 108)
        Me.VscrollGrid.TabIndex = 3
        Me.VscrollGrid.Visible = False
        '
        'HScrollGrid
        '
        Me.HScrollGrid.LargeChange = 1
        Me.HScrollGrid.Location = New System.Drawing.Point(20, 160)
        Me.HScrollGrid.Name = "HScrollGrid"
        Me.HScrollGrid.Size = New System.Drawing.Size(202, 20)
        Me.HScrollGrid.TabIndex = 2
        Me.HScrollGrid.Visible = False
        '
        'picGrid
        '
        Me.picGrid.BackColor = System.Drawing.Color.White
        Me.picGrid.ContextMenuStrip = Me.ContextMenuStripTabGrid
        Me.picGrid.Location = New System.Drawing.Point(9, 29)
        Me.picGrid.Margin = New System.Windows.Forms.Padding(2)
        Me.picGrid.Name = "picGrid"
        Me.picGrid.Size = New System.Drawing.Size(213, 118)
        Me.picGrid.TabIndex = 1
        Me.picGrid.TabStop = False
        '
        'ContextMenuStripTabGrid
        '
        Me.ContextMenuStripTabGrid.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuUndo, Me.ToolStripMenuItem1, Me.mnuCopy, Me.mnuPaste, Me.mnuCut, Me.ToolStripMenuItem2, Me.mnuRowNumber, Me.mnuColNumber, Me.ToolStripMenuItem3, Me.mnuInsertRow, Me.mnuInsertCol, Me.ToolStripMenuItem4, Me.mnuRowHeight, Me.mnuColWidth, Me.ToolStripMenuItem5, Me.mnuDeleteRow, Me.mnuDeleteCol})
        Me.ContextMenuStripTabGrid.Name = "ContextMenuStripTabGrid"
        Me.ContextMenuStripTabGrid.Size = New System.Drawing.Size(134, 298)
        '
        'mnuUndo
        '
        Me.mnuUndo.Name = "mnuUndo"
        Me.mnuUndo.Size = New System.Drawing.Size(133, 22)
        Me.mnuUndo.Text = "元に戻す(&U)"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(130, 6)
        '
        'mnuCopy
        '
        Me.mnuCopy.Name = "mnuCopy"
        Me.mnuCopy.Size = New System.Drawing.Size(133, 22)
        Me.mnuCopy.Text = "コピー(&C)"
        '
        'mnuPaste
        '
        Me.mnuPaste.Name = "mnuPaste"
        Me.mnuPaste.Size = New System.Drawing.Size(133, 22)
        Me.mnuPaste.Text = "貼り付け(&P)"
        '
        'mnuCut
        '
        Me.mnuCut.Name = "mnuCut"
        Me.mnuCut.Size = New System.Drawing.Size(133, 22)
        Me.mnuCut.Text = "切り取り(&T)"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(130, 6)
        '
        'mnuRowNumber
        '
        Me.mnuRowNumber.Name = "mnuRowNumber"
        Me.mnuRowNumber.Size = New System.Drawing.Size(133, 22)
        Me.mnuRowNumber.Text = "行数指定"
        '
        'mnuColNumber
        '
        Me.mnuColNumber.Name = "mnuColNumber"
        Me.mnuColNumber.Size = New System.Drawing.Size(133, 22)
        Me.mnuColNumber.Text = "列数指定"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(130, 6)
        '
        'mnuInsertRow
        '
        Me.mnuInsertRow.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuInsertRowUpword, Me.mnuInsertRowDownward})
        Me.mnuInsertRow.Name = "mnuInsertRow"
        Me.mnuInsertRow.Size = New System.Drawing.Size(133, 22)
        Me.mnuInsertRow.Text = "行挿入"
        '
        'mnuInsertRowUpword
        '
        Me.mnuInsertRowUpword.Name = "mnuInsertRowUpword"
        Me.mnuInsertRowUpword.Size = New System.Drawing.Size(128, 22)
        Me.mnuInsertRowUpword.Text = "前に挿入"
        '
        'mnuInsertRowDownward
        '
        Me.mnuInsertRowDownward.Name = "mnuInsertRowDownward"
        Me.mnuInsertRowDownward.Size = New System.Drawing.Size(128, 22)
        Me.mnuInsertRowDownward.Text = "後ろに挿入"
        '
        'mnuInsertCol
        '
        Me.mnuInsertCol.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuInsertColLeft, Me.mnuInsertColRight})
        Me.mnuInsertCol.Name = "mnuInsertCol"
        Me.mnuInsertCol.Size = New System.Drawing.Size(133, 22)
        Me.mnuInsertCol.Text = "列挿入"
        '
        'mnuInsertColLeft
        '
        Me.mnuInsertColLeft.Name = "mnuInsertColLeft"
        Me.mnuInsertColLeft.Size = New System.Drawing.Size(128, 22)
        Me.mnuInsertColLeft.Text = "前に挿入"
        '
        'mnuInsertColRight
        '
        Me.mnuInsertColRight.Name = "mnuInsertColRight"
        Me.mnuInsertColRight.Size = New System.Drawing.Size(128, 22)
        Me.mnuInsertColRight.Text = "後ろに挿入"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(130, 6)
        '
        'mnuRowHeight
        '
        Me.mnuRowHeight.Name = "mnuRowHeight"
        Me.mnuRowHeight.Size = New System.Drawing.Size(133, 22)
        Me.mnuRowHeight.Text = "行高変更"
        '
        'mnuColWidth
        '
        Me.mnuColWidth.Name = "mnuColWidth"
        Me.mnuColWidth.Size = New System.Drawing.Size(133, 22)
        Me.mnuColWidth.Text = "列幅変更"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(130, 6)
        '
        'mnuDeleteRow
        '
        Me.mnuDeleteRow.Name = "mnuDeleteRow"
        Me.mnuDeleteRow.Size = New System.Drawing.Size(133, 22)
        Me.mnuDeleteRow.Text = "行削除"
        '
        'mnuDeleteCol
        '
        Me.mnuDeleteCol.Name = "mnuDeleteCol"
        Me.mnuDeleteCol.Size = New System.Drawing.Size(133, 22)
        Me.mnuDeleteCol.Text = "列削除"
        '
        'SSTab
        '
        Me.SSTab.Controls.Add(Me.TabPage1)
        Me.SSTab.Controls.Add(Me.TabPage2)
        Me.SSTab.Controls.Add(Me.TabPage3)
        Me.SSTab.Controls.Add(Me.TabPage4)
        Me.SSTab.Controls.Add(Me.TabPage5)
        Me.SSTab.Controls.Add(Me.TabPage6)
        Me.SSTab.Controls.Add(Me.TabPage7)
        Me.SSTab.Dock = System.Windows.Forms.DockStyle.Top
        Me.SSTab.Location = New System.Drawing.Point(0, 0)
        Me.SSTab.Margin = New System.Windows.Forms.Padding(2)
        Me.SSTab.Name = "SSTab"
        Me.SSTab.SelectedIndex = 0
        Me.SSTab.Size = New System.Drawing.Size(530, 22)
        Me.SSTab.TabIndex = 3
        '
        'TabPage1
        '
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPage1.Size = New System.Drawing.Size(522, 0)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1ssssssssssssssssssss"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPage2.Size = New System.Drawing.Size(522, 0)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPage3.Size = New System.Drawing.Size(522, 0)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "TabPage3"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPage4.Size = New System.Drawing.Size(522, 0)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "TabPage4"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'TabPage5
        '
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPage5.Size = New System.Drawing.Size(522, 0)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "TabPage5"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'TabPage6
        '
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPage6.Size = New System.Drawing.Size(522, 0)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "TabPage6"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'TabPage7
        '
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPage7.Size = New System.Drawing.Size(522, 0)
        Me.TabPage7.TabIndex = 6
        Me.TabPage7.Text = "TabPage7"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'ContextMenuStripTab
        '
        Me.ContextMenuStripTab.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuTabRightMenuChangeTabName, Me.mnuTabRightMenuMoveTab, Me.mnuTabRightMenuInsertTab, Me.mnuTabRightMenuDeleteTab})
        Me.ContextMenuStripTab.Name = "ContextMenuStripTab"
        Me.ContextMenuStripTab.Size = New System.Drawing.Size(167, 92)
        '
        'mnuTabRightMenuChangeTabName
        '
        Me.mnuTabRightMenuChangeTabName.Name = "mnuTabRightMenuChangeTabName"
        Me.mnuTabRightMenuChangeTabName.Size = New System.Drawing.Size(166, 22)
        Me.mnuTabRightMenuChangeTabName.Text = "レイヤ名の変更"
        '
        'mnuTabRightMenuMoveTab
        '
        Me.mnuTabRightMenuMoveTab.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuTabRightMenuMoveTabLeft, Me.mnuTabRightMenuMoveTabRight, Me.mnuTabRightMenuMoveTabAhead, Me.mnuTabRightMenuMoveTabRear})
        Me.mnuTabRightMenuMoveTab.Name = "mnuTabRightMenuMoveTab"
        Me.mnuTabRightMenuMoveTab.Size = New System.Drawing.Size(166, 22)
        Me.mnuTabRightMenuMoveTab.Text = "レイヤの移動"
        '
        'mnuTabRightMenuMoveTabLeft
        '
        Me.mnuTabRightMenuMoveTabLeft.Name = "mnuTabRightMenuMoveTabLeft"
        Me.mnuTabRightMenuMoveTabLeft.Size = New System.Drawing.Size(149, 22)
        Me.mnuTabRightMenuMoveTabLeft.Text = "一つ前に移動"
        '
        'mnuTabRightMenuMoveTabRight
        '
        Me.mnuTabRightMenuMoveTabRight.Name = "mnuTabRightMenuMoveTabRight"
        Me.mnuTabRightMenuMoveTabRight.Size = New System.Drawing.Size(149, 22)
        Me.mnuTabRightMenuMoveTabRight.Text = "一つ後ろに移動"
        '
        'mnuTabRightMenuMoveTabAhead
        '
        Me.mnuTabRightMenuMoveTabAhead.Name = "mnuTabRightMenuMoveTabAhead"
        Me.mnuTabRightMenuMoveTabAhead.Size = New System.Drawing.Size(149, 22)
        Me.mnuTabRightMenuMoveTabAhead.Text = "先頭に移動"
        '
        'mnuTabRightMenuMoveTabRear
        '
        Me.mnuTabRightMenuMoveTabRear.Name = "mnuTabRightMenuMoveTabRear"
        Me.mnuTabRightMenuMoveTabRear.Size = New System.Drawing.Size(149, 22)
        Me.mnuTabRightMenuMoveTabRear.Text = "末尾に移動"
        '
        'mnuTabRightMenuInsertTab
        '
        Me.mnuTabRightMenuInsertTab.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuTabRightMenuInsertTabLeft, Me.mnuTabRightMenuInsertTabRight})
        Me.mnuTabRightMenuInsertTab.Name = "mnuTabRightMenuInsertTab"
        Me.mnuTabRightMenuInsertTab.Size = New System.Drawing.Size(166, 22)
        Me.mnuTabRightMenuInsertTab.Text = "新しいレイヤの挿入"
        '
        'mnuTabRightMenuInsertTabLeft
        '
        Me.mnuTabRightMenuInsertTabLeft.Name = "mnuTabRightMenuInsertTabLeft"
        Me.mnuTabRightMenuInsertTabLeft.Size = New System.Drawing.Size(128, 22)
        Me.mnuTabRightMenuInsertTabLeft.Text = "前に挿入"
        '
        'mnuTabRightMenuInsertTabRight
        '
        Me.mnuTabRightMenuInsertTabRight.Name = "mnuTabRightMenuInsertTabRight"
        Me.mnuTabRightMenuInsertTabRight.Size = New System.Drawing.Size(128, 22)
        Me.mnuTabRightMenuInsertTabRight.Text = "後ろに挿入"
        '
        'mnuTabRightMenuDeleteTab
        '
        Me.mnuTabRightMenuDeleteTab.Name = "mnuTabRightMenuDeleteTab"
        Me.mnuTabRightMenuDeleteTab.Size = New System.Drawing.Size(166, 22)
        Me.mnuTabRightMenuDeleteTab.Text = "レイヤの削除"
        '
        'TimerMouse
        '
        '
        'KTGISGrid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnlGridFrame)
        Me.Controls.Add(Me.SSTab)
        Me.Name = "KTGISGrid"
        Me.Size = New System.Drawing.Size(530, 416)
        Me.pnlGridFrame.ResumeLayout(False)
        Me.pnlGridFrame.PerformLayout()
        CType(Me.picGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStripTabGrid.ResumeLayout(False)
        Me.SSTab.ResumeLayout(False)
        Me.ContextMenuStripTab.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlGridFrame As System.Windows.Forms.Panel
    Friend WithEvents txtTextBox As System.Windows.Forms.TextBox
    Friend WithEvents VscrollGrid As System.Windows.Forms.VScrollBar
    Friend WithEvents HScrollGrid As System.Windows.Forms.HScrollBar
    Friend WithEvents picGrid As System.Windows.Forms.PictureBox
    Friend WithEvents SSTab As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents ContextMenuStripTab As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuTabRightMenuChangeTabName As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTabRightMenuMoveTab As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTabRightMenuMoveTabLeft As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTabRightMenuMoveTabRight As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTabRightMenuMoveTabAhead As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTabRightMenuMoveTabRear As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTabRightMenuInsertTab As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTabRightMenuInsertTabLeft As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTabRightMenuInsertTabRight As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTabRightMenuDeleteTab As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStripTabGrid As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuUndo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPaste As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuRowNumber As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuColNumber As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuInsertRow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuInsertRowUpword As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuInsertRowDownward As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuInsertCol As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuInsertColLeft As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuInsertColRight As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuRowHeight As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuColWidth As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuDeleteRow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDeleteCol As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TimerMouse As System.Windows.Forms.Timer

End Class
