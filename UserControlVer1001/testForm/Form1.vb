Public Class Form1
    Dim cbx As Long
    Dim cby As Long
    Dim cbl As Long
    Private Sub btnProgressLabel_Click(sender As Object, e As EventArgs) Handles btnProgressLabel.Click

        Timer.Interval = 200
        Timer.Start()
    End Sub




    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        Static n As Integer = 0
        If n = 0 Then
            ProgressLabel1.Start(10, "計測中", True)
        End If
        If n = 10 Then
            ProgressLabel1.Finish()
            n = 0
            Timer.Stop()
        Else
            ProgressLabel1.SetValue(n)
            n += 1
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click



        KtgisGrid2.LayerCaption = "オブジェクトグループ"
        KtgisGrid2.init("オブジェクトグループ", "オブジェクト", "データ項目", 2, 1, 5, 1, True, True, True, True, True, True, True, False, True, True)
        ProgressLabel1.Start(3, "読み込み中", False)
        For i As Integer = 0 To 2
            ProgressLabel1.SetValue(i + 1)
            Dim fname As String
            Dim sepa As String = vbTab
            Select Case i
                Case 0
                    fname = "F:\mandara10f\UserControl\testForm\sample1.txt"
                Case 1
                    fname = "F:\mandara10f\UserControl\testForm\sample2.txt"
                Case 2
                    fname = "F:\mandara10f\UserControl\testForm\１都３県公示地価2001年.csv"
                    sepa = ","
            End Select

            Dim sr As New System.IO.StreamReader(fname, System.Text.Encoding.GetEncoding("shift_jis"))
            '内容を一行ずつ読み込む
            Dim GridText As New ArrayList
            Dim Maxx As Integer = 0, Maxy As Integer = 0
            While sr.Peek() > -1
                Dim celltextdata() = sr.ReadLine().Split(sepa)
                Maxx = Math.Max(Maxx, celltextdata.GetLength(0))
                GridText.Add(celltextdata)
                Maxy += 1
            End While
            sr.Close()
            KtgisGrid2.AddLayer("レイヤ" + i.ToString, i, Maxx - 1, Maxy - 2)
            With KtgisGrid2
                .GridAlligntment(i, 0) = HorizontalAlignment.Left
            End With
            'グリッドに設定
            For j As Integer = 0 To 1
                For k As Integer = 0 To Maxx - 2
                    KtgisGrid2.FixedYSData(i, k, j + 3) = GridText.Item(j)(k + 1)
                Next
            Next

            For j As Integer = 2 To Maxy - 1
                KtgisGrid2.FixedXSData(i, 1, j - 2) = GridText.Item(j)(0)
                For k As Integer = 0 To Maxx - 2
                    KtgisGrid2.GridData(i, k, j - 2) = GridText.Item(j)(k + 1)
                Next
            Next
            datacheck(KtgisGrid2, i)
            With KtgisGrid2
                '.FixedXSWidth(i, 0) = 50
                .FixedXSWidth(i, 1) = 150
                .FixedYSHeight(i, 3) = .FixedYSHeight(i, 3) * 2
            End With

        Next
        KtgisGrid2.AddObject(0, 47, 2)
        KtgisGrid2.RemoveObject(0, 1, 5)
        KtgisGrid2.RemoveDataItem(0, 28, 41)
        KtgisGrid2.FixedXSColor(0, 0, 0) = Color.Brown
        KtgisGrid2.FixedXSColor(0, 1, 0) = Color.AliceBlue
        KtgisGrid2.FixedXSColor(0, 1, 1) = Color.Red

        KtgisGrid2.FixedYSColor(0, 0, 0) = Color.Brown
        KtgisGrid2.FixedYSColor(0, 1, 1) = Color.AliceBlue
        KtgisGrid2.FixedYSColor(0, 2, 1) = Color.Red

        KtgisGrid2.Show()
        KtgisGrid2.LayerName(1) = "123"
        ProgressLabel1.Finish()
    End Sub

    Private Sub KtgisFileSelector1_Changed() Handles KtgisFileSelector1.Changed
        MsgBox(KtgisFileSelector1.Path)
    End Sub



    Private Sub KtgisFileSelector1_Load(sender As Object, e As EventArgs) Handles KtgisFileSelector1.Load

    End Sub

    Private Sub KtgisGrid1_Change_FixedYS()

    End Sub


    Private Sub KtgisGrid1_Load(sender As Object, e As EventArgs)

    End Sub

    Private Sub KtgisGrid1_Add_Layer(InsertPoint As Integer) Handles KtgisGrid2.Add_Layer, KtgisGrid1.Add_Layer
        Dim a
        a = 1
    End Sub

    Private Sub KtgisGrid1_Change_Data() Handles KtgisGrid2.Change_Data, KtgisGrid1.Change_Data

    End Sub

    Private Sub KtgisGrid1_Change_FixedYS1() Handles KtgisGrid2.Change_FixedYS, KtgisGrid1.Change_FixedYS

        datacheck(KtgisGrid2, KtgisGrid2.Layer)

        KtgisGrid2.Refresh()
    End Sub
    Private Sub datacheck(ByRef KtgisGrid As KTGISUserControl.KTGISGrid, ByVal LayerNum As Integer)
        With KtgisGrid

            .FixedUpperLeftData(Layernum, 1, 1) = "データの種類"
            .FixedUpperLeftData(LayerNum, 1, 2) = "空白セル"
            .FixedUpperLeftData(LayerNum, 1, 3) = "タイトル"
            .FixedUpperLeftData(LayerNum, 1, 4) = "単位"
            Dim a$
            For i As Integer = 0 To .Xsize(Layernum) - 1
                If UCase(.FixedYSData(LayerNum, i, 4)) = "CAT" Then
                    a$ = "ｶﾃｺﾞﾘｰﾃﾞｰﾀ"
                ElseIf UCase(.FixedYSData(LayerNum, i, 4)) = "STR" Then
                    a$ = "文字データ"
                Else
                    a$ = "通常のデータ"
                End If
                .FixedYSData(Layernum, i, 1) = a$

                .GridAlligntment(Layernum, i) = HorizontalAlignment.Left
                If UCase(.FixedYSData(LayerNum, i, 4)) = "CAT" Then
                ElseIf UCase(.FixedYSData(LayerNum, i, 4)) = "STR" Then
                Else
                    .GridAlligntment(LayerNum, i) = HorizontalAlignment.Right
                End If

            Next
        End With

    End Sub

    Private Sub KtgisGrid1_Change_Layer(LayerNameChange As Boolean, LayerMove As Boolean, LayerDelete As Boolean) Handles KtgisGrid2.Change_Layer, KtgisGrid1.Change_Layer
        Dim a
        a = 1
    End Sub

    Private Sub KtgisGrid1_Change_LayerSelect(Layer As Integer, PreviousLayer As Integer) Handles KtgisGrid2.Change_LayerSelect, KtgisGrid1.Change_LayerSelect
        Dim a
        a = 1
    End Sub

    Private Sub KtgisGrid1_Click_FixedYS2(Layer As Integer, X As Integer, Y As Integer, Value As String, Top As Single, Left As Single, Width As Single, Height As Single) Handles KtgisGrid2.Click_FixedYS2, KtgisGrid1.Click_FixedYS2

        cbx = X
        cby = Y
        cbl = Layer
        Select Case Y
            Case 1
                ComboBox1.Left = Left
                ComboBox1.Top = Top
                ComboBox1.Width = Width
                ComboBox1.Height = Height

                ComboBox1.BringToFront()

                ComboBox1.Visible = True
                ComboBox1.Text = KtgisGrid2.FixedYSData(cbl, cbx, cby)
                ComboBox1.Focus()
            Case 2
                ComboBox2.Left = Left
                ComboBox2.Top = Top
                ComboBox2.Width = Width
                ComboBox2.Height = Height

                ComboBox2.BringToFront()
                ComboBox2.Text = KtgisGrid2.FixedYSData(cbl, cbx, cby)

                ComboBox2.Visible = True
                ComboBox2.Focus()

        End Select


    End Sub




    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        ComboBox1.Items.Add("通常のデータ")
        ComboBox1.Items.Add("ｶﾃｺﾞﾘｰﾃﾞｰﾀ")
        ComboBox1.Items.Add("文字データ")
        ComboBox1.Items.Add("URLの名称")
        ComboBox1.Items.Add("URLのアドレス")
        ComboBox1.Parent = SplitContainer1.Panel2
        ComboBox1.Visible = False
        ComboBox2.Items.Add("欠損値")
        ComboBox2.Items.Add("0または空白")
        ComboBox2.Parent = SplitContainer1.Panel2
        ComboBox2.Visible = False

        With ktObjectName
            .init("", "", "", 1, 0, 1, 0, False, False, True, True, True, True, True, True, False, True)
            Dim n As Integer = 1
            .AddLayer("", 0, 1, n)
            .GridWidth(0, 0) = .Width - 20 ' - .FixedXSWidth(0, 0)
            .GridAlligntment(0, 0) = HorizontalAlignment.Left
            For i As Integer = 0 To n - 1
                .GridData(0, 0, i) = i.ToString + "/" + n.ToString
            Next
            .Show()
        End With

    End Sub



    Private Sub ComboBox1_Enter(sender As Object, e As EventArgs) Handles ComboBox1.Enter
        ComboBox1.DroppedDown = True

    End Sub

    Private Sub ComboBox1_LostFocus(sender As Object, e As EventArgs) Handles ComboBox1.LostFocus
        ComboBox1.Visible = False
        KtgisGrid2.FixedYSData(cbl, cbx, cby) = ComboBox1.Text
        Dim Oa2$ = KtgisGrid2.FixedYSData(cbl, cbx, 3)
        Dim Oa3$ = KtgisGrid2.FixedYSData(cbl, cbx, 4)
        Dim a2$ = Oa2$, a3$ = Oa3$
        Select Case ComboBox1.Text
            Case "通常のデータ"
                If UCase(Oa2$) = "URL" Or UCase(Oa2$) = "URL_NAME" Then
                    a2$ = ""
                End If
                If UCase(Oa3$) = "CAT" Or UCase(Oa3$) = "STR" Then
                    a3$ = ""
                End If
            Case "ｶﾃｺﾞﾘｰﾃﾞｰﾀ"
                If UCase(Oa2$) = "URL" Or UCase(Oa2$) = "URL_NAME" Then
                    a2$ = ""
                End If
                a3$ = "CAT"
            Case "文字データ"
                If UCase(Oa2$) = "URL" Or UCase(Oa2$) = "URL_NAME" Then
                    a2$ = ""
                End If
                a3$ = "STR"
            Case "URLの名称"
                a2$ = "URL_NAME"
                a3$ = ""
            Case "URLのアドレス"
                a2$ = "URL"
                a3$ = ""
        End Select
        KtgisGrid2.FixedYSData(cbl, cbx, 3) = a2$
        KtgisGrid2.FixedYSData(cbl, cbx, 4) = a3$
        Call datacheck(KtgisGrid2, cbl)
        KtgisGrid2.Refresh()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ComboBox1.Visible = False
    End Sub

    Private Sub ComboBox2_LostFocus(sender As Object, e As EventArgs) Handles ComboBox2.LostFocus
        ComboBox2.Visible = False
        KtgisGrid2.FixedYSData(cbl, cbx, cby) = ComboBox2.Text
        KtgisGrid2.Refresh()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        ComboBox2.Visible = False
    End Sub
    Private Sub ComboBox2_Enter(sender As Object, e As EventArgs) Handles ComboBox2.Enter
        ComboBox2.DroppedDown = True

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        KtgisGrid2.Find("ur", KTGISUserControl.KTGISGrid.enmMatchingMode.PartialtMatching)
        KtgisGrid2.LayerData(0, "A") = "abc"
        KtgisGrid2.LayerData(0, "B") = 1
        KtgisGrid2.LayerData(0, "A") = "あいう"
        Dim v As String = CType(KtgisGrid2.LayerData(0, "A"), String)
        Dim v2 As Integer = CType(KtgisGrid2.LayerData(0, "B"), Integer)
        Dim tx As String(,) = KtgisGrid2.FixedXSData(0)
        KtgisGrid2.GridColor(0, 0, 0) = Color.Aqua
        Dim c As Color = KtgisGrid2.GridColor(0, 0, 0)
        Dim c2 As Color = KtgisGrid2.GridColor(0, 0, 1)
        KtgisGrid2.AddLayer("new", 0, 5, 15, False, False, True, False, True, False, True, False)
        KtgisGrid2.Refresh()
    End Sub

    Private Sub btnSmallGrid_Click(sender As Object, e As EventArgs) Handles btnSmallGrid.Click
        With ktGridCompatibleCharacter
            .init("データ", "行", "列", 1, 0, 0, 0, False, True, True, False, False, False, False, False, False, False)
            Dim n As Integer = 1
            .AddLayer("", 0, 1, n)
            .GridWidth(0, 0) = .Width - 20 - .FixedXSWidth(0, 0)
            .GridAlligntment(0, 0) = HorizontalAlignment.Left
            For i As Integer = 0 To n - 1
                .GridData(0, 0, i) = i.ToString + "/" + n.ToString
            Next
            .Show()
        End With
    End Sub

    Private Sub SplitContainer1_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        With ktGridCompatibleCharacter
            Dim r As Rectangle = .SelectedArea(0)
            If r.Height <> -1 Then
                .RemoveObject(0, r.Y, r.Height)
                .Show()
            End If
        End With
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        KtgisGrid2.SetGridPosition(1, 1, -1)
    End Sub

    Private Sub ktGridCompatibleCharacter_Click_DataGrid(Layer As Integer, X As Integer, Y As Integer, Value As String, Top As Single, Left As Single, Width As Single, Height As Single) Handles ktGridCompatibleCharacter.Click_DataGrid
        MsgBox(Y)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        KtgisGrid2.Layer = 2
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Dim p As Point
        If ktGridCompatibleCharacter.GetXY(0, p) = True Then
            ktGridCompatibleCharacter.RemoveObject(0, p.Y, 1)
        End If
        ktGridCompatibleCharacter.Refresh()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        KtgisGrid2.TopCell(300)
        KtgisGrid2.Refresh()
    End Sub
End Class
