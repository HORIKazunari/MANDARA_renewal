Imports System.Windows.Forms

Public Class KtgisFileSelector
    Dim Save_f As Boolean
    Dim cmdOff_f As Boolean
    Dim Ext As String
    Dim IniFolder As String

    Public Event Changed()


    <System.ComponentModel.Description("保存の場合true")> _
    Public Property Save_Flag() As Boolean
        Get
            Return Save_f
        End Get
        Set(value As Boolean)
            Save_f = value
            If value = True Then
                btnRef.Text = "設定"
            Else
                btnRef.Text = "参照"
            End If
        End Set
    End Property
    <System.ComponentModel.Description("「解除」ボタンの設定と取得")> _
    Public Property Off_Button_Flag() As Boolean
        Get
            Return btnOff.Visible
        End Get
        Set(value As Boolean)
            btnOff.Visible = value
            cmdOff_f = value
            setLayout()
        End Set
    End Property

    <System.ComponentModel.Description("キャプション文字の設定と取得")> _
        Public Property Caption() As String
        Get
            Return gbFileSelector.Text
        End Get
        Set(value As String)
            gbFileSelector.Text = value
        End Set
    End Property

   <System.ComponentModel.Description("パスの設定と取得")> _
    Public Property Path() As String
        Get
            Return txtPath.Text
        End Get
        Set(value As String)
            txtPath.Text = value
        End Set
    End Property
    <System.ComponentModel.Description("デフォルトフォルダの設定と取得")> _
    Public Property InitFolder() As String
        Get
            Return IniFolder
        End Get
        Set(value As String)
            IniFolder = value
        End Set
    End Property
    <System.ComponentModel.Description("拡張子指定。複数の場合は|で区切る")> _
    Public Property Extension() As String
        Get
            Return Ext
        End Get
        Set(value As String)
            Ext = value

        End Set
    End Property

    Private Sub setLayout()
        Select Case cmdOff_f
            Case False
                btnRef.Left = btnOff.Left
            Case True
                btnRef.Left = btnOff.Left - btnRef.Width - 5
        End Select
        txtPath.Width = btnRef.Left - txtPath.Left - 5

    End Sub

    Private Sub btnOff_Click(sender As Object, e As EventArgs) Handles btnOff.Click
        If Path <> "" Then
            Path = ""
        End If
    End Sub

    Private Sub btnRef_Click(sender As Object, e As EventArgs) Handles btnRef.Click
        Dim PT As String
        Dim f As Boolean

        If Me.Path = "" Then
            PT = Me.InitFolder
        Else
            PT = System.IO.Path.GetDirectoryName(Me.Path)
        End If

        f = RefferenceFile(Ext, PT, gbFileSelector.Text, Save_f)

    End Sub

    Private Function RefferenceFile(ByVal ext As String, ByVal InitDir As String, ByRef prompt As String, ByVal saveDlg_F As Boolean) As Boolean
        Dim Fname As String

        Dim filter As String = ""
        Dim extsp() As String = ext.Split("|")
        Dim exnum As Integer = extsp.GetLength(0)
        If exnum > 1 Then
            filter = "ファイル|"
            For i As Integer = 0 To extsp.GetLength(0) - 1
                filter += "*." + extsp(i) + ";"
            Next
            filter += "|"
        End If
        For i As Integer = 0 To extsp.GetLength(0) - 1
            If extsp(i) <> "*" Then
                filter += extsp(i)
            End If
            filter += "ファイル(*." & extsp(i) & ")|*." & extsp(i)
            If i <> extsp.GetLength(0) - 1 Then
                filter += "|"
            End If
        Next
        Fname = txtPath.Text
        Dim f As Boolean = False
        If saveDlg_F = True Then

            Dim sfd As New SaveFileDialog()
            sfd.InitialDirectory = InitDir
            sfd.Filter = filter
            If sfd.ShowDialog() = DialogResult.OK Then
                Fname = sfd.FileName
                f = True
            End If
        Else
            Dim ofd As New OpenFileDialog()
            ofd.FileName = Fname
            ofd.InitialDirectory = InitDir
            ofd.Filter = filter
            ofd.Title = prompt
            'ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = True

            'ダイアログを表示する
            If ofd.ShowDialog() = DialogResult.OK Then
                Fname = ofd.FileName
                f = True
            End If
        End If
        If f = True Then
            Dim n As Integer = InStrRev(Fname, ".")
            If n = 0 And extsp(0) <> "*" And extsp(0) <> "" Then
                Fname += "." & extsp(0)
            End If
            txtPath.Text = Fname

            Return True
        Else
            Return False
        End If

    End Function

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Me.Extension = ""
        Me.InitFolder = ""
        Me.Off_Button_Flag = False
        Me.Save_Flag = False
        Me.Path = ""

    End Sub

    Private Sub txtPath_MouseClick(sender As Object, e As MouseEventArgs) Handles txtPath.MouseClick
        txtPath.SelectAll()
    End Sub

    Private Sub txtPath_TextChanged(sender As Object, e As EventArgs) Handles txtPath.TextChanged
        ToolTip.SetToolTip(txtPath, System.IO.Path.GetFileName(txtPath.Text))
        RaiseEvent Changed()
    End Sub

    Private Sub KtgisFileSelector_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        setLayout()
    End Sub
End Class
