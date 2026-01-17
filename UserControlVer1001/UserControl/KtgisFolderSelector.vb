Imports System.Windows.Forms

Public Class KtgisFolderSelector
    Dim AddFolderFlag As Boolean
    Public Event Changed()


    <System.ComponentModel.Description("キャプション文字の設定と取得")> _
    Public Property Caption() As String
        Get
            Return gbFolderSelector.Text
        End Get
        Set(value As String)
            gbFolderSelector.Text = value
        End Set
    End Property

    <System.ComponentModel.Description("フォルダの設定と取得")> _
    Public Property Folder() As String
        Get
            Return txtFolder.Text
        End Get
        Set(value As String)
            txtFolder.Text = value
        End Set
    End Property

    <System.ComponentModel.Description("「フォルダの新規追加ボタン」の設定と状態取得")> _
    Public Property AddFolder_Flag() As Boolean
        Get
            Return AddFolderFlag
        End Get
        Set(value As Boolean)
            AddFolderFlag = value
        End Set
    End Property

    Private Sub btnRef_Click(sender As Object, e As EventArgs) Handles btnRef.Click
        Dim fbd As New FolderBrowserDialog

        fbd.Description = gbFolderSelector.Text
        Dim fol As String = txtFolder.Text
        If fol = "" Then
            fol = Folder
        End If
        fbd.RootFolder = Environment.SpecialFolder.Desktop
        fbd.SelectedPath = fol
        fbd.ShowNewFolderButton = AddFolderFlag

        'ダイアログを表示する
        If fbd.ShowDialog(Me) = DialogResult.OK Then
            '選択されたフォルダを表示する
            txtFolder.Text = fbd.SelectedPath()
        End If
    End Sub

    Private Sub txtFolder_Click(sender As Object, e As EventArgs) Handles txtFolder.Click
        txtFolder.SelectAll()
    End Sub


    Private Sub txtFolder_TextChanged(sender As Object, e As EventArgs) Handles txtFolder.TextChanged
        ToolTip.SetToolTip(txtFolder, txtFolder.Text)
        RaiseEvent Changed()
    End Sub
End Class
