<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.components = New System.ComponentModel.Container()
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.btnSmallGrid = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnProgressLabel = New System.Windows.Forms.Button()
        Me.gbObjectName12 = New System.Windows.Forms.GroupBox()
        Me.btnAddObjName = New System.Windows.Forms.Button()
        Me.btnDeleteObjectName = New System.Windows.Forms.Button()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.ProgressLabel1 = New KTGISUserControl.KTGISProgressLabel()
        Me.KtgisFolderSelector1 = New KTGISUserControl.KtgisFolderSelector()
        Me.KtgisFileSelector1 = New KTGISUserControl.KtgisFileSelector()
        Me.ktObjectName = New KTGISUserControl.KTGISGrid()
        Me.ktGridCompatibleCharacter = New KTGISUserControl.KTGISGrid()
        Me.KtgisGrid2 = New KTGISUserControl.KTGISGrid()
        Me.KtgisGrid1 = New KTGISUserControl.KTGISGrid()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.gbObjectName12.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer
        '
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button7)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button5)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnSmallGrid)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ProgressLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.KtgisFolderSelector1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.KtgisFileSelector1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnProgressLabel)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.gbObjectName12)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ktGridCompatibleCharacter)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ComboBox2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ComboBox1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.KtgisGrid2)
        Me.SplitContainer1.Size = New System.Drawing.Size(858, 659)
        Me.SplitContainer1.SplitterDistance = 92
        Me.SplitContainer1.TabIndex = 7
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(528, 55)
        Me.Button7.Margin = New System.Windows.Forms.Padding(2)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(67, 27)
        Me.Button7.TabIndex = 18
        Me.Button7.Text = "TopCell"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(599, 58)
        Me.Button6.Margin = New System.Windows.Forms.Padding(2)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(49, 20)
        Me.Button6.TabIndex = 17
        Me.Button6.Text = "Button6"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(461, 58)
        Me.Button5.Margin = New System.Windows.Forms.Padding(2)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(49, 20)
        Me.Button5.TabIndex = 16
        Me.Button5.Text = "Button5"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(206, 51)
        Me.Button4.Margin = New System.Windows.Forms.Padding(2)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(67, 27)
        Me.Button4.TabIndex = 15
        Me.Button4.Text = "Button4"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(368, 50)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(76, 32)
        Me.Button3.TabIndex = 14
        Me.Button3.Text = "小さいグリッド"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'btnSmallGrid
        '
        Me.btnSmallGrid.Location = New System.Drawing.Point(286, 50)
        Me.btnSmallGrid.Name = "btnSmallGrid"
        Me.btnSmallGrid.Size = New System.Drawing.Size(76, 32)
        Me.btnSmallGrid.TabIndex = 13
        Me.btnSmallGrid.Text = "小さいグリッドテスト"
        Me.btnSmallGrid.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(78, 46)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(69, 36)
        Me.Button2.TabIndex = 12
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(265, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(41, 32)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnProgressLabel
        '
        Me.btnProgressLabel.Location = New System.Drawing.Point(166, 8)
        Me.btnProgressLabel.Margin = New System.Windows.Forms.Padding(2)
        Me.btnProgressLabel.Name = "btnProgressLabel"
        Me.btnProgressLabel.Size = New System.Drawing.Size(62, 34)
        Me.btnProgressLabel.TabIndex = 6
        Me.btnProgressLabel.Text = "ProgressLabel"
        Me.btnProgressLabel.UseVisualStyleBackColor = True
        '
        'gbObjectName12
        '
        Me.gbObjectName12.Controls.Add(Me.ktObjectName)
        Me.gbObjectName12.Controls.Add(Me.btnAddObjName)
        Me.gbObjectName12.Controls.Add(Me.btnDeleteObjectName)
        Me.gbObjectName12.Location = New System.Drawing.Point(638, 125)
        Me.gbObjectName12.Margin = New System.Windows.Forms.Padding(2)
        Me.gbObjectName12.Name = "gbObjectName12"
        Me.gbObjectName12.Padding = New System.Windows.Forms.Padding(17, 8, 2, 2)
        Me.gbObjectName12.Size = New System.Drawing.Size(203, 116)
        Me.gbObjectName12.TabIndex = 21
        Me.gbObjectName12.TabStop = False
        Me.gbObjectName12.Text = "オブジェクト名"
        '
        'btnAddObjName
        '
        Me.btnAddObjName.Location = New System.Drawing.Point(27, 87)
        Me.btnAddObjName.Name = "btnAddObjName"
        Me.btnAddObjName.Size = New System.Drawing.Size(64, 24)
        Me.btnAddObjName.TabIndex = 1
        Me.btnAddObjName.Text = "追加"
        Me.btnAddObjName.UseVisualStyleBackColor = True
        '
        'btnDeleteObjectName
        '
        Me.btnDeleteObjectName.Location = New System.Drawing.Point(102, 87)
        Me.btnDeleteObjectName.Name = "btnDeleteObjectName"
        Me.btnDeleteObjectName.Size = New System.Drawing.Size(64, 24)
        Me.btnDeleteObjectName.TabIndex = 2
        Me.btnDeleteObjectName.Text = "削除"
        Me.btnDeleteObjectName.UseVisualStyleBackColor = True
        '
        'ComboBox2
        '
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(555, 186)
        Me.ComboBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(69, 20)
        Me.ComboBox2.TabIndex = 14
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(552, 163)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(69, 20)
        Me.ComboBox1.TabIndex = 13
        '
        'ProgressLabel1
        '
        Me.ProgressLabel1.LabelColor = System.Drawing.SystemColors.Control
        Me.ProgressLabel1.Location = New System.Drawing.Point(28, 10)
        Me.ProgressLabel1.Margin = New System.Windows.Forms.Padding(4)
        Me.ProgressLabel1.Name = "ProgressLabel1"
        Me.ProgressLabel1.Size = New System.Drawing.Size(119, 31)
        Me.ProgressLabel1.TabIndex = 11
        Me.ProgressLabel1.Visible = False
        '
        'KtgisFolderSelector1
        '
        Me.KtgisFolderSelector1.AddFolder_Flag = True
        Me.KtgisFolderSelector1.Caption = "保存先フォルダ"
        Me.KtgisFolderSelector1.Folder = "c:\"
        Me.KtgisFolderSelector1.Location = New System.Drawing.Point(620, 14)
        Me.KtgisFolderSelector1.Margin = New System.Windows.Forms.Padding(2)
        Me.KtgisFolderSelector1.Name = "KtgisFolderSelector1"
        Me.KtgisFolderSelector1.Size = New System.Drawing.Size(175, 38)
        Me.KtgisFolderSelector1.TabIndex = 10
        '
        'KtgisFileSelector1
        '
        Me.KtgisFileSelector1.Caption = "開く"
        Me.KtgisFileSelector1.Extension = "*"
        Me.KtgisFileSelector1.InitFolder = "c:\"
        Me.KtgisFileSelector1.Location = New System.Drawing.Point(333, 14)
        Me.KtgisFileSelector1.Margin = New System.Windows.Forms.Padding(4)
        Me.KtgisFileSelector1.Name = "KtgisFileSelector1"
        Me.KtgisFileSelector1.Off_Button_Flag = True
        Me.KtgisFileSelector1.Path = ""
        Me.KtgisFileSelector1.Save_Flag = True
        Me.KtgisFileSelector1.Size = New System.Drawing.Size(262, 38)
        Me.KtgisFileSelector1.TabIndex = 9
        '
        'ktObjectName
        '
        Me.ktObjectName.DefaultFixedUpperLeftAlligntment = System.Windows.Forms.HorizontalAlignment.Left
        Me.ktObjectName.DefaultFixedXNumberingWidth = 50
        Me.ktObjectName.DefaultFixedXSAllignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.ktObjectName.DefaultFixedXWidth = 150
        Me.ktObjectName.DefaultFixedYSAllignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.ktObjectName.DefaultGridAlligntment = System.Windows.Forms.HorizontalAlignment.Right
        Me.ktObjectName.DefaultGridWidth = 100
        Me.ktObjectName.DefaultNumberingAlligntment = System.Windows.Forms.HorizontalAlignment.Center
        Me.ktObjectName.FixedGridColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.ktObjectName.FrameColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ktObjectName.GridColor = System.Drawing.Color.White
        Me.ktObjectName.GridFont = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ktObjectName.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ktObjectName.Layer = 0
        Me.ktObjectName.LayerCaption = Nothing
        Me.ktObjectName.Location = New System.Drawing.Point(7, 23)
        Me.ktObjectName.MsgBoxTitle = ""
        Me.ktObjectName.Name = "ktObjectName"
        Me.ktObjectName.RowCaption = Nothing
        Me.ktObjectName.Size = New System.Drawing.Size(186, 58)
        Me.ktObjectName.TabClickEnabled = False
        Me.ktObjectName.TabIndex = 0
        '
        'ktGridCompatibleCharacter
        '
        Me.ktGridCompatibleCharacter.DefaultFixedUpperLeftAlligntment = System.Windows.Forms.HorizontalAlignment.Left
        Me.ktGridCompatibleCharacter.DefaultFixedXNumberingWidth = 50
        Me.ktGridCompatibleCharacter.DefaultFixedXSAllignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.ktGridCompatibleCharacter.DefaultFixedXWidth = 150
        Me.ktGridCompatibleCharacter.DefaultFixedYSAllignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.ktGridCompatibleCharacter.DefaultGridAlligntment = System.Windows.Forms.HorizontalAlignment.Right
        Me.ktGridCompatibleCharacter.DefaultGridWidth = 100
        Me.ktGridCompatibleCharacter.DefaultNumberingAlligntment = System.Windows.Forms.HorizontalAlignment.Center
        Me.ktGridCompatibleCharacter.FixedGridColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.ktGridCompatibleCharacter.FrameColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.ktGridCompatibleCharacter.GridColor = System.Drawing.Color.White
        Me.ktGridCompatibleCharacter.GridFont = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ktGridCompatibleCharacter.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ktGridCompatibleCharacter.Layer = 0
        Me.ktGridCompatibleCharacter.LayerCaption = Nothing
        Me.ktGridCompatibleCharacter.Location = New System.Drawing.Point(528, 50)
        Me.ktGridCompatibleCharacter.Margin = New System.Windows.Forms.Padding(4)
        Me.ktGridCompatibleCharacter.MsgBoxTitle = ""
        Me.ktGridCompatibleCharacter.Name = "ktGridCompatibleCharacter"
        Me.ktGridCompatibleCharacter.RowCaption = Nothing
        Me.ktGridCompatibleCharacter.Size = New System.Drawing.Size(163, 55)
        Me.ktGridCompatibleCharacter.TabClickEnabled = False
        Me.ktGridCompatibleCharacter.TabIndex = 20
        '
        'KtgisGrid2
        '
        Me.KtgisGrid2.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar
        Me.KtgisGrid2.DefaultFixedUpperLeftAlligntment = System.Windows.Forms.HorizontalAlignment.Left
        Me.KtgisGrid2.DefaultFixedXNumberingWidth = 30
        Me.KtgisGrid2.DefaultFixedXSAllignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.KtgisGrid2.DefaultFixedXWidth = 150
        Me.KtgisGrid2.DefaultFixedYSAllignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.KtgisGrid2.DefaultGridAlligntment = System.Windows.Forms.HorizontalAlignment.Right
        Me.KtgisGrid2.DefaultGridWidth = 100
        Me.KtgisGrid2.DefaultNumberingAlligntment = System.Windows.Forms.HorizontalAlignment.Left
        Me.KtgisGrid2.Dock = System.Windows.Forms.DockStyle.Left
        Me.KtgisGrid2.FixedGridColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.KtgisGrid2.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.KtgisGrid2.FrameColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.KtgisGrid2.GridColor = System.Drawing.Color.White
        Me.KtgisGrid2.GridFont = New System.Drawing.Font("MS UI Gothic", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.KtgisGrid2.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.KtgisGrid2.Layer = 0
        Me.KtgisGrid2.LayerCaption = Nothing
        Me.KtgisGrid2.Location = New System.Drawing.Point(0, 0)
        Me.KtgisGrid2.Margin = New System.Windows.Forms.Padding(4)
        Me.KtgisGrid2.MsgBoxTitle = "テスト"
        Me.KtgisGrid2.Name = "KtgisGrid2"
        Me.KtgisGrid2.RowCaption = Nothing
        Me.KtgisGrid2.Size = New System.Drawing.Size(510, 563)
        Me.KtgisGrid2.TabClickEnabled = False
        Me.KtgisGrid2.TabIndex = 0
        '
        'KtgisGrid1
        '
        Me.KtgisGrid1.DefaultFixedUpperLeftAlligntment = System.Windows.Forms.HorizontalAlignment.Left
        Me.KtgisGrid1.DefaultFixedXNumberingWidth = 30
        Me.KtgisGrid1.DefaultFixedXSAllignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.KtgisGrid1.DefaultFixedXWidth = 150
        Me.KtgisGrid1.DefaultFixedYSAllignment = System.Windows.Forms.HorizontalAlignment.Left
        Me.KtgisGrid1.DefaultGridAlligntment = System.Windows.Forms.HorizontalAlignment.Right
        Me.KtgisGrid1.DefaultGridWidth = 100
        Me.KtgisGrid1.DefaultNumberingAlligntment = System.Windows.Forms.HorizontalAlignment.Left
        Me.KtgisGrid1.FixedGridColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.KtgisGrid1.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.KtgisGrid1.FrameColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.KtgisGrid1.GridColor = System.Drawing.Color.White
        Me.KtgisGrid1.GridFont = New System.Drawing.Font("MS UI Gothic", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.KtgisGrid1.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.KtgisGrid1.Layer = 0
        Me.KtgisGrid1.LayerCaption = Nothing
        Me.KtgisGrid1.Location = New System.Drawing.Point(36, 5)
        Me.KtgisGrid1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.KtgisGrid1.MsgBoxTitle = "テスト"
        Me.KtgisGrid1.Name = "KtgisGrid1"
        Me.KtgisGrid1.RowCaption = Nothing
        Me.KtgisGrid1.Size = New System.Drawing.Size(448, 320)
        Me.KtgisGrid1.TabClickEnabled = False
        Me.KtgisGrid1.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(858, 659)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.gbObjectName12.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnProgressLabel As System.Windows.Forms.Button
    Friend WithEvents KtgisFileSelector1 As KTGISUserControl.KtgisFileSelector
    Friend WithEvents KtgisFolderSelector1 As KTGISUserControl.KtgisFolderSelector
    Friend WithEvents KtgisGrid2 As KTGISUserControl.KTGISGrid
    Friend WithEvents ProgressLabel1 As KTGISUserControl.KTGISProgressLabel
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

    End Sub
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents btnSmallGrid As System.Windows.Forms.Button
    Friend WithEvents ktGridCompatibleCharacter As KTGISUserControl.KTGISGrid
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents KtgisGrid1 As KTGISUserControl.KTGISGrid
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents gbObjectName12 As System.Windows.Forms.GroupBox
    Friend WithEvents ktObjectName As KTGISUserControl.KTGISGrid
    Friend WithEvents btnAddObjName As System.Windows.Forms.Button
    Friend WithEvents btnDeleteObjectName As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
End Class
