Imports System.Drawing
Imports System.Windows.Forms

Public Class KTGISGrid
    Public Enum enmMatchingMode
        ''' <summary>
        ''' 完全一致
        ''' </summary>
        ''' <remarks></remarks>
        PerfectMatching = 0
        ''' <summary>
        ''' 部分一致
        ''' </summary>
        ''' <remarks></remarks>
        PartialtMatching = 1
    End Enum
    Private Structure Undo_InputCopyPasteClearInfo
        Public Layer As Integer
        Public caption As String
        Public Rect As Rectangle
        Public GridData As String
    End Structure
    Private Structure Undo_InsertRows
        Public Layer As Integer
        Public caption As String
        Public Top As Integer
        Public Bottom As Integer
    End Structure
    Private Structure Undo_InsertColumns
        Public Layer As Integer
        Public caption As String
        Public Left As Integer
        Public Right As Integer
    End Structure
    Private Structure Undo_DeleteRows
        Public Layer As Integer
        Public caption As String
        Public Top As Integer
        Public Bottom As Integer
        Public GridData As String
    End Structure
    Private Structure Undo_DeleteColumns
        Public Layer As Integer
        Public caption As String
        Public Left As Integer
        Public Right As Integer
        Public GridData As String
    End Structure
    Private Structure Undo_ChangeRowHeight
        Public Layer As Integer
        Public caption As String
        Public Top As Integer
        Public Bottom As Integer
        Public Height() As Integer
    End Structure
    Private Structure Undo_ChangeColumnWidth
        Public Layer As Integer
        Public caption As String
        Public Left As Integer
        Public Right As Integer
        Public Width() As Integer
    End Structure
    Private Structure Undo_ChangeLayerName
        Public Layer As Integer
        Public caption As String
        Public Name As String
    End Structure
    Private Structure Undo_SwapLayer
        Public Layer1 As Integer
        Public Layer2 As Integer
        Public caption As String
    End Structure
    Private Structure Undo_MoveLayer
        Public OriginLay As Integer
        Public DestLay As Integer
        Public caption As String
    End Structure
    Private Structure Undo_deleteLayer
        Public OriginLay As Integer
        Public GridData As Grid_Info
        Public caption As String
    End Structure
    Private Structure Undo_InsertLayer
        Public Layer As Integer
        Public caption As String
    End Structure


    Dim UndoArray As New ArrayList

    Private Structure FixedObjectNameData_Info
        Public Width As Integer
        Public Allignment As Windows.Forms.HorizontalAlignment
    End Structure


    Private Structure FixedDataItemData_Info
        Public Height As Integer
        Public Allignment As Windows.Forms.HorizontalAlignment
    End Structure

    Private Structure FixedUpperLeft_Info
        Public Text As String
        Public Allignment As Windows.Forms.HorizontalAlignment
    End Structure

    Private Structure CellData_Info
        Public Width As Integer
        Public Allignment As Windows.Forms.HorizontalAlignment
    End Structure

    Private Structure GridTextColor_Info
        Public Text As String
        Public colorSetF As Boolean
        Public Color As Color
    End Structure
    Private Structure Operation_enable_info
        Public RightClickEnabled As Boolean 'グリッド上の右クリックでコピー以外は使えなくする
        Public RightClickAllEnabled As Boolean '右クリックメニューの使用可否
        Public InputEnabled As Boolean '入力の可否
        Public GridRowEnabled As Boolean '行削除・挿入を行わない
        Public GridColEnabled As Boolean '列削除・挿入を行わない
        Public FixedXSEnabled As Boolean '左固定領域の変更を行わない
        Public FixedYSEnabled As Boolean '上固定領域の変更を行わない
        Public FixedUpperLeftEnabeld As Boolean '左上固定領域の変更を行わない
    End Structure
    Private Structure Grid_Info
        Public OriginalLayerNumber As Integer
        Public Grid_Text(,) As GridTextColor_Info
        Public FixedObjectName(,) As GridTextColor_Info
        Public FixedDataItem(,) As GridTextColor_Info
        Public FixedUpperLeft(,) As FixedUpperLeft_Info
        Public Ope As Operation_enable_info

        Public LayerName As String
        Public LayerData As Dictionary(Of String, Object)
        Public YMax As Integer
        Public Xmax As Integer
        Public DataItemData() As CellData_Info
        Public CellHeight() As Integer

        Public FixedDataItemData() As FixedDataItemData_Info

        Public ReadOnly Property FixedDataItemHeight() As Integer
            Get
                Dim H As Integer = 0
                For i As Integer = 0 To Me.FixedDataItemData.GetUpperBound(0)
                    H += Me.FixedDataItemData(i).Height
                Next
                Return H
            End Get
        End Property
        Public FixedObjectNameData() As FixedObjectNameData_Info
        Public ReadOnly Property FixedObjectNameDataWidth() As Integer
            Get
                Dim W As Integer = 0
                For i As Integer = 0 To Me.FixedObjectNameData.GetUpperBound(0)
                    W += Me.FixedObjectNameData(i).Width
                Next
                Return W
            End Get
        End Property
        Public FixedUpperLeftAllignment As Integer
        Public GridLineCol As Color
        Public TopCell As Integer
        Public LeftCell As Integer
        Public BottomCell As Integer
        Public RightCell As Integer
        Public SelectedF As Boolean
        Public MouseDownX As Integer
        Public MouseDownY As Integer
        Public MouseDown_Mode As Integer '0/通常のセル 1/最上段　2/左端
        Public MouseUpX As Integer
        Public MouseUpY As Integer
        Public ReadOnly Property MouseUpDownRect As Rectangle
            Get
                Return Rectangle.FromLTRB(Math.Min(Me.MouseDownX, Me.MouseUpX), Math.Min(Me.MouseDownY, Me.MouseUpY),
                                          Math.Max(Me.MouseDownX, Me.MouseUpX), Math.Max(Me.MouseDownY, Me.MouseUpY))
            End Get
        End Property
        Public Function Clone() As Grid_Info
            Dim CloneGrid As Grid_Info
            With CloneGrid

                .Grid_Text = Me.Grid_Text.Clone
                .FixedObjectName = Me.FixedObjectName.Clone
                .FixedDataItem = Me.FixedDataItem.Clone
                .FixedUpperLeft = Me.FixedUpperLeft.Clone
                .Ope = Me.Ope
                .LayerName = Me.LayerName
                .LayerData = New Dictionary(Of String, Object)
                For Each ld In Me.LayerData
                    .LayerData.Add(ld.Key, ld.Value)
                Next
                .YMax = Me.YMax
                .Xmax = Me.Xmax
                .DataItemData = Me.DataItemData.Clone
                .CellHeight = Me.CellHeight.Clone
                .FixedDataItemData = Me.FixedDataItemData.Clone
                .FixedObjectNameData = Me.FixedObjectNameData.Clone
                .FixedUpperLeftAllignment = Me.FixedUpperLeftAllignment
                .GridLineCol = Me.GridLineCol
                .TopCell = Me.TopCell
                .LeftCell = Me.LeftCell
                .BottomCell = Me.BottomCell
                .RightCell = Me.RightCell
                .SelectedF = Me.SelectedF
                .MouseDownX = Me.MouseDownX
                .MouseDownY = Me.MouseDownY
                .MouseDown_Mode = Me.MouseDown_Mode
                .MouseUpX = Me.MouseUpX
                .MouseUpY = Me.MouseUpY

            End With
            Return CloneGrid
        End Function
    End Structure
    Dim Grid_Property() As Grid_Info
    Private Structure Grid_Color
        Public Frame As System.Drawing.Color
        Public SelectedFrame As System.Drawing.Color
        Public Grid As System.Drawing.Color
        Public GridLine As System.Drawing.Color
        Public TextBox As System.Drawing.Color
        Public SelectedGrid As System.Drawing.Color
        Public FixedGrid As System.Drawing.Color
        Public SelectedFixedGrid As System.Drawing.Color

    End Structure

    Private Structure Grid_Total_Info
        Public initOK As Boolean
        Public LayerNum As Integer
        Public Layer As Integer
        Public FixedDataItem_n As Integer
        Public FixedDataItem_n2 As Integer
        Public FixedObjectName_n As Integer
        Public FixedObjectName_n2 As Integer
        Public tOpe As Operation_enable_info

        Public TabVisible As Boolean 'レイヤタブを表示しない
        Public TabClickEnabled As Boolean 'タブで右クリックメニュー利用
        Public RightClickEnabled As Boolean
        Public RightClickAllEnabled As Boolean '右クリックメニューの使用可否
        Public RowCaption As String
        Public ColumnCaption As String
        Public LayerCaption As String
        Public DefaultFixedXWidth As Integer
        Public DefaultFixedXNumberingWidth As Integer
        Public DefaultGridWidth As Integer
        Public DefaultFixedYSAllignment As Windows.Forms.HorizontalAlignment
        Public DefaultFixedXSAllignment As Windows.Forms.HorizontalAlignment
        Public DefaultFixedUpperLeftAlligntment As Windows.Forms.HorizontalAlignment
        Public DefaultGridAlligntment As Windows.Forms.HorizontalAlignment
        Public DefaultNumberingAlligntment As Windows.Forms.HorizontalAlignment
        Public Color As Grid_Color
        Public GridFont As Font
        Public MsgBoxTitle As String
    End Structure
    Dim Grid_Total As Grid_Total_Info


    Dim ToolTip1 As ToolTip
    Dim TimerVX As Integer, TimerVY As Integer
    Dim GridMouseDown As Boolean

    Private Structure Grid_Resize_Info
        Public Enable As Integer
        Public GridX As Integer
        Public GridY As Integer
        Public LeftX As Integer
        Public TopY As Integer
        Public firstF As Boolean
    End Structure

    Dim GridResize As Grid_Resize_Info

    Private Declare Function SetCapture Lib "user32" (ByVal hWnd As Integer) As Integer
    Private Declare Function ReleaseCapture Lib "user32" () As Integer

    Dim GX As Integer, GY As Integer



    '■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    'イベント
    Public Event Change_Data() 'データがどこか変更された場合に発生
    ''' <summary>
    ''' レイヤの追加メニューを選択した場合に発生
    ''' </summary>
    ''' <param name="InsertPoint">挿入箇所</param>
    ''' <remarks></remarks>
    Public Event Add_Layer(ByVal InsertPoint As Integer) '

    ''' <summary>
    ''' 表示レイヤを変更した場合に発生
    ''' </summary>
    ''' <param name="Layer"></param>
    ''' <param name="PreviousLayer"></param>
    ''' <remarks></remarks>
    Public Event Change_LayerSelect(ByVal Layer As Integer, ByVal PreviousLayer As Integer)

    ''' <summary>
    ''' 上部の固定部分の枠２行目をクリックした場合に発生
    ''' </summary>
    ''' <param name="Layer"></param>
    ''' <param name="X"></param>
    ''' <param name="Y"></param>
    ''' <param name="Value"></param>
    ''' <param name="Top"></param>
    ''' <param name="Left"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <remarks></remarks>
    Public Event Click_FixedYS2(ByVal Layer As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal Value As String, ByVal Top As Single, ByVal Left As Single, ByVal Width As Single, ByVal Height As Single)

    ''' <summary>
    ''' データ部分をクリックした場合に発生
    ''' </summary>
    ''' <param name="Layer"></param>
    ''' <param name="X"></param>
    ''' <param name="Y"></param>
    ''' <param name="Value"></param>
    ''' <param name="Top"></param>
    ''' <param name="Left"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <remarks></remarks>
    Public Event Click_DataGrid(ByVal Layer As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal Value As String, ByVal Top As Single, ByVal Left As Single, ByVal Width As Single, ByVal Height As Single)

    ''' <summary>
    ''' 上部の固定部分かつ枠でない部分がコントロール内で変更された場合に発生
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Change_FixedYS()

    ''' <summary>
    ''' 左部の固定部分かつ枠でない部分がコントロール内で変更された場合に発生
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Change_FixedXS()

    ''' <summary>
    ''' 左上部の固定部分かつ枠でない部分がコントロール内で変更された場合に発生
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Change_FixedUpperLeft()
    '

    ''' <summary>
    ''' レイヤ名の変更、レイヤの移動などで発生
    ''' </summary>
    ''' <param name="LayerNameChange">レイヤ名の変更</param>
    ''' <param name="LayerMove">レイヤの移動</param>
    ''' <param name="LayerDelete">レイヤの削除</param>
    ''' <remarks></remarks>
    Public Event Change_Layer(ByVal LayerNameChange As Boolean, ByVal LayerMove As Boolean, ByVal LayerDelete As Boolean)
    '■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    'プロパティ－－－－－－－－－－－－－－－－－－－－－－－－－－


    ''' <summary>
    ''' 現在のレイヤの上端セルの位置（設定の際はrefresh必要）
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <remarks></remarks>
    Public Sub TopCell(ByVal Value As Integer)
        Grid_Property(Grid_Total.Layer).TopCell = Value
    End Sub
    ''' <summary>
    ''' 現在のレイヤの上端セルの取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TopCell() As Integer
        Return Grid_Property(Grid_Total.Layer).TopCell
    End Function

    ''' <summary>
    ''' 現在のレイヤの左端セルの位置（設定の際はrefresh必要）
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <remarks></remarks>
    Public Sub LeftCell(ByVal Value As Integer)
        Grid_Property(Grid_Total.Layer).LeftCell = Value
    End Sub
    ''' <summary>
    ''' 現在のレイヤの左端セルの取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LeftCell() As Integer
        Return Grid_Property(Grid_Total.Layer).LeftCell
    End Function


    ''' <summary>
    ''' InputBoxMsgBoxのタイトル欄の文字
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MsgBoxTitle() As String
        Get
            Return Grid_Total.MsgBoxTitle
        End Get
        Set(value As String)
            Grid_Total.MsgBoxTitle = value
        End Set
    End Property
    ''' <summary>
    ''' グリッドのフォント
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GridFont() As Font
        Get
            Return Grid_Total.GridFont
        End Get
        Set(value As Font)
            Grid_Total.GridFont = value
        End Set
    End Property


    ''' <summary>
    ''' 左端番号列の幅
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DefaultFixedXNumberingWidth() As Integer
        Get
            Return Grid_Total.DefaultFixedXNumberingWidth
        End Get
        Set(value As Integer)
            Grid_Total.DefaultFixedXNumberingWidth = value
        End Set
    End Property
    Public Property DefaultFixedXWidth() As Integer
        Get
            Return Grid_Total.DefaultFixedXWidth
        End Get
        Set(value As Integer)
            Grid_Total.DefaultFixedXWidth = value
        End Set
    End Property
    Public Property DefaultGridWidth() As Integer
        Get
            Return Grid_Total.DefaultGridWidth
        End Get
        Set(value As Integer)
            Grid_Total.DefaultGridWidth = value
        End Set
    End Property
    Public Property DefaultFixedUpperLeftAlligntment() As Windows.Forms.HorizontalAlignment
        Get
            Return Grid_Total.DefaultFixedUpperLeftAlligntment
        End Get
        Set(value As Windows.Forms.HorizontalAlignment)
            Grid_Total.DefaultFixedUpperLeftAlligntment = value
        End Set
    End Property
    Public Property DefaultFixedYSAllignment() As Windows.Forms.HorizontalAlignment
        Get
            Return Grid_Total.DefaultFixedYSAllignment
        End Get
        Set(value As Windows.Forms.HorizontalAlignment)
            Grid_Total.DefaultFixedYSAllignment = value
        End Set
    End Property
    Public Property DefaultFixedXSAllignment() As Windows.Forms.HorizontalAlignment
        Get
            Return Grid_Total.DefaultFixedXSAllignment
        End Get
        Set(value As Windows.Forms.HorizontalAlignment)
            Grid_Total.DefaultFixedXSAllignment = value
        End Set
    End Property
    Public Property DefaultGridAlligntment() As Windows.Forms.HorizontalAlignment
        Get
            Return Grid_Total.DefaultGridAlligntment
        End Get
        Set(value As Windows.Forms.HorizontalAlignment)
            Grid_Total.DefaultGridAlligntment = value
        End Set
    End Property
    Public Property DefaultNumberingAlligntment() As Windows.Forms.HorizontalAlignment
        Get
            Return Grid_Total.DefaultNumberingAlligntment
        End Get
        Set(value As Windows.Forms.HorizontalAlignment)
            Grid_Total.DefaultNumberingAlligntment = value
        End Set
    End Property

    ''' <summary>
    ''' グリッドの選択範囲を取得
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <value></value>
    ''' <returns>Rectangle構造体、選択されていない場合は幅、高さが-1</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SelectedArea(ByVal LayerNum As Integer) As Rectangle
        Get
            Dim R As Rectangle = Grid_Property(LayerNum).MouseUpDownRect
            If Grid_Property(LayerNum).SelectedF = False Then
                R.Height = -1
                R.Width = -1
                R.X = 0
                R.Y = 0
            Else
                R.Height += 1
                R.Width += 1
            End If
            Return R
        End Get
    End Property
    ''' <summary>
    ''' グリッドの横セル数取得/設定
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Xsize(ByVal LayerNum As Integer) As Integer
        Get
            Return Grid_Property(LayerNum).Xmax
        End Get
        Set(value As Integer)
            Dim xmax As Integer = Grid_Property(LayerNum).Xmax
            If value = xmax Then
                Return
            ElseIf value > xmax Then
                InsertColumns(LayerNum, xmax, value - xmax)
            Else
                DeleteColumns(LayerNum, value, xmax - value)
            End If
        End Set
    End Property

    ''' <summary>
    ''' グリッドの縦セル数取得/設定
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Ysize(ByVal LayerNum As Integer) As Integer
        Get
            Return Grid_Property(LayerNum).YMax
        End Get
        Set(value As Integer)
            Dim ymax As Integer = Grid_Property(LayerNum).YMax
            If value = ymax Then
                Return
            ElseIf value > ymax Then
                InsertRows(LayerNum, ymax, value - ymax)
            Else
                DeleteRows(LayerNum, value, ymax - value)
            End If
        End Set
    End Property

    ''' <summary>
    ''' 上固定部分の行数
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FixedYsNum() As Integer
        Get
            Return Grid_Total.FixedDataItem_n
        End Get
    End Property

    ''' <summary>
    ''' '上固定部分二段目の行数
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FixedYsNum2() As Integer
        Get
            Return Grid_Total.FixedDataItem_n2
        End Get
    End Property

    ''' <summary>
    ''' '左固定部分の行数
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FixedXsNum() As Integer
        Get
            Return Grid_Total.FixedObjectName_n
        End Get
    End Property

    ''' <summary>
    '''  '左固定部分二段の行数
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FixedXsNum2() As Integer
        Get
            Return Grid_Total.FixedObjectName_n2
        End Get
    End Property

    ''' <summary>
    ''' グリッドの位置を指定して表示
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <param name="LeftCell"></param>
    ''' <param name="TopCell"></param>
    ''' <remarks></remarks>
    Public Sub SetGridPosition(ByVal LayerNum As Integer, ByVal LeftCell As Integer, ByVal TopCell As Integer)
        With Grid_Property(LayerNum)
            .TopCell = Math.Max(TopCell, 0)
            .LeftCell = Math.Max(LeftCell, 0)
            .SelectedF = True
            .MouseDownX = LeftCell
            .MouseDownY = TopCell
            .MouseUpX = LeftCell
            .MouseUpY = TopCell
        End With
        If Grid_Total.Layer <> LayerNum Then
            SSTab.SelectedIndex = LayerNum
        Else
            Call Print_Grid_Data()
        End If
    End Sub

    ''' <summary>
    ''' 'グリッドのデータを配列取得／取得のみ
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads ReadOnly Property GridData(ByVal LayerNum As Integer) As String(,)
        Get

            With Grid_Property(LayerNum)
                Dim xs As Integer = .Xmax
                Dim ys As Integer = .YMax
                Dim D(.Xmax - 1, .YMax - 1) As String
                For i As Integer = 0 To xs - 1
                    For j As Integer = 0 To ys - 1
                        D(i, j) = .Grid_Text(i, j).Text
                    Next
                Next
                Return D
            End With
        End Get
    End Property


    ''' <summary>
    ''' 'グリッドの上部固定部分を配列取得
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads ReadOnly Property FixedYSData(ByVal LayerNum As Integer) As String(,)
        Get

            With Grid_Property(LayerNum)
                Dim xs As Integer = .FixedDataItem.GetUpperBound(0)
                Dim ys As Integer = .FixedDataItem.GetUpperBound(1)
                Dim dt(xs, ys) As String
                For i As Integer = 0 To xs
                    For j As Integer = 0 To ys
                        dt(i, j) = .FixedDataItem(i, j).Text
                    Next
                Next
                Return dt
            End With
        End Get
    End Property

    ''' <summary>
    ''' 'グリッドの左端固定部分を配列取得
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads ReadOnly Property FixedXSData(ByVal LayerNum As Integer) As String(,)
        Get
            With Grid_Property(LayerNum)
                Dim xs As Integer = .FixedObjectName.GetUpperBound(0)
                Dim ys As Integer = .FixedObjectName.GetUpperBound(1)
                Dim dt(xs, ys) As String
                For i As Integer = 0 To xs
                    For j As Integer = 0 To ys
                        dt(i, j) = .FixedObjectName(i, j).Text
                    Next
                Next
                Return dt
            End With
        End Get

    End Property


    ''' <summary>
    ''' 'グリッドの左上固定部分を配列取得
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads ReadOnly Property FixedUpperLeftData(ByVal LayerNum As Integer) As String(,)
        Get
            With Grid_Property(LayerNum)
                Dim xs As Integer = .FixedUpperLeft.GetUpperBound(0)
                Dim ys As Integer = .FixedUpperLeft.GetUpperBound(1)
                Dim dt(ys, ys) As String
                For i As Integer = 0 To xs
                    For j As Integer = 0 To ys
                        dt(i, j) = .FixedUpperLeft(i, j).Text
                    Next
                Next
                Return dt
            End With
        End Get
    End Property


    ''' <summary>
    ''' 'グリッドの文字設定取得（セル単位）：実行時のみ
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <param name="X"></param>
    ''' <param name="Y"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Property GridData(ByVal LayerNum As Integer, ByVal X As Integer, ByVal Y As Integer) As String
        Get
            With Grid_Property(LayerNum)
                Return .Grid_Text(X, Y).Text
            End With
        End Get
        Set(ByVal value As String)
            With Grid_Property(LayerNum)
                .Grid_Text(X, Y).Text = value
            End With
        End Set
    End Property
    ''' <summary>
    ''' 'グリッドの左端固定部分の文字設定取得
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <param name="X"></param>
    ''' <param name="Y"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Property FixedXSData(ByVal LayerNum As Integer, ByVal X As Integer, ByVal Y As Integer) As String
        Get

            With Grid_Property(LayerNum)
                Return .FixedObjectName(X, Y).Text
            End With
        End Get
        Set(ByVal value As String)
            With Grid_Property(LayerNum)
                .FixedObjectName(X, Y).Text = value
            End With
        End Set
    End Property
    ''' <summary>
    ''' グリッドの左端固定部分の個別色設定
    ''' </summary>
    ''' <param name="Layernum"></param>
    ''' <param name="X"></param>
    ''' <param name="Y"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Property FixedXSColor(ByVal Layernum As Integer, ByVal X As Integer, Y As Integer) As System.Drawing.Color
        Get
            If Grid_Property(Layernum).FixedObjectName(X, Y).colorSetF = True Then
                Return Grid_Property(Layernum).FixedObjectName(X, Y).Color
            Else
                If X < Grid_Total.FixedObjectName_n2 Then
                    Return Grid_Total.Color.Frame
                Else
                    Return Grid_Total.Color.FixedGrid
                End If
            End If
        End Get
        Set(value As System.Drawing.Color)
            Grid_Property(Layernum).FixedObjectName(X, Y).colorSetF = True
            Grid_Property(Layernum).FixedObjectName(X, Y).Color = value
        End Set
    End Property
    ''' <summary>
    ''' レイヤのグリッドの左端固定部分の色設定をすべてクリア
    ''' </summary>
    ''' <param name="Layernum"></param>
    ''' <remarks></remarks>
    Public Overloads Sub FixedXSColorReset(ByVal Layernum As Integer)
        With Grid_Property(Layernum)
            For i As Integer = 0 To Grid_Total.FixedObjectName_n - 1
                For j As Integer = 0 To .YMax - 1
                    .FixedObjectName(i, j).colorSetF = False
                Next
            Next
        End With
        Print_Grid_Data()

    End Sub
    ''' <summary>
    ''' グリッドの左端固定部分の色設定クリア
    ''' </summary>
    ''' <param name="Layernum"></param>
    ''' <param name="X"></param>
    ''' <param name="Y"></param>
    ''' <remarks></remarks>
    Public Overloads Sub FixedXSColorReset(ByVal Layernum As Integer, ByVal X As Integer, Y As Integer)
        Grid_Property(Layernum).FixedObjectName(X, Y).colorSetF = False
    End Sub

    ''' <summary>
    ''' 'グリッドの上端固定部分の文字設定取得
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <param name="X"></param>
    ''' <param name="Y"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Property FixedYSData(ByVal LayerNum As Integer, ByVal X As Integer, ByVal Y As Integer) As String
        Get
            With Grid_Property(LayerNum)
                Return .FixedDataItem(X, Y).Text
            End With
        End Get
        Set(ByVal value As String)
            With Grid_Property(LayerNum)
                .FixedDataItem(X, Y).Text = value
            End With
        End Set
    End Property
    ''' <summary>
    ''' グリッドの上端固定部分の個別色設定
    ''' </summary>
    ''' <param name="Layernum"></param>
    ''' <param name="X"></param>
    ''' <param name="Y"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Property FixedYSColor(ByVal Layernum As Integer, ByVal X As Integer, Y As Integer) As System.Drawing.Color
        Get
            If Grid_Property(Layernum).FixedDataItem(X, Y).colorSetF = True Then
                Return Grid_Property(Layernum).Grid_Text(X, Y).Color
            Else
                If Y < Grid_Total.FixedDataItem_n2 Then
                    Return Grid_Total.Color.Frame
                Else
                    Return Grid_Total.Color.FixedGrid
                End If
            End If
        End Get
        Set(value As System.Drawing.Color)
            Grid_Property(Layernum).FixedDataItem(X, Y).colorSetF = True
            Grid_Property(Layernum).FixedDataItem(X, Y).Color = value
        End Set
    End Property
    ''' <summary>
    ''' レイヤのグリッドの上端固定部分の色設定をすべてクリア
    ''' </summary>
    ''' <param name="Layernum"></param>
    ''' <remarks></remarks>
    Public Overloads Sub FixedYSColorReset(ByVal Layernum As Integer)
        With Grid_Property(Layernum)
            For i As Integer = 0 To Grid_Total.FixedDataItem_n - 1
                For j As Integer = 0 To .YMax - 1
                    .FixedDataItem(i, j).colorSetF = False
                Next
            Next
            Print_Grid_Data()
        End With
    End Sub
    ''' <summary>
    ''' グリッドの上端固定部分の色設定クリア
    ''' </summary>
    ''' <param name="Layernum"></param>
    ''' <param name="X"></param>
    ''' <param name="Y"></param>
    ''' <remarks></remarks>
    Public Overloads Sub FixedYSColorReset(ByVal Layernum As Integer, ByVal X As Integer, Y As Integer)
        Grid_Property(Layernum).FixedDataItem(X, Y).colorSetF = False
    End Sub
    ''' <summary>
    ''' 'グリッド上端固定部分の文字設定
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <param name="X"></param>
    ''' <param name="Y"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Property FixedUpperLeftData(ByVal LayerNum As Integer, ByVal X As Integer, ByVal Y As Integer) As String
        Get

            With Grid_Property(LayerNum)
                Return .FixedUpperLeft(X, Y).Text
            End With
        End Get
        Set(ByVal value As String)
            With Grid_Property(LayerNum)
                .FixedUpperLeft(X, Y).Text = value
            End With
        End Set
    End Property
    ''' <summary>
    ''' グリッドの高さ設定
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <param name="Y"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GridHeight(ByVal LayerNum As Integer, ByVal Y As Integer) As Integer
        Get
            Return Grid_Property(LayerNum).CellHeight(Y)
        End Get
        Set(ByVal value As Integer)
            Grid_Property(LayerNum).CellHeight(Y) = value
        End Set
    End Property

    ''' <summary>
    ''' グリッドの幅設定
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <param name="X"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GridWidth(ByVal LayerNum As Integer, ByVal X As Integer) As Integer
        Get
            Return Grid_Property(LayerNum).DataItemData(X).Width
        End Get
        Set(ByVal value As Integer)
            Grid_Property(LayerNum).DataItemData(X).Width = value
        End Set
    End Property
    ''' <summary>
    ''' グリッドの配置設定
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <param name="X"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GridAlligntment(ByVal LayerNum As Integer, ByVal X As Integer) As Windows.Forms.HorizontalAlignment
        Get
            Return Grid_Property(LayerNum).DataItemData(X).Allignment
        End Get
        Set(ByVal value As Windows.Forms.HorizontalAlignment)
            Grid_Property(LayerNum).DataItemData(X).Allignment = value
        End Set
    End Property

    ''' <summary>
    ''' グリッドの左上固定部分の配置設定
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FixedUpperLeftAlligntment(ByVal LayerNum As Integer) As Windows.Forms.HorizontalAlignment
        Get
            Return Grid_Property(LayerNum).FixedUpperLeftAllignment
        End Get
        Set(ByVal value As Windows.Forms.HorizontalAlignment)
            Grid_Property(LayerNum).FixedUpperLeftAllignment = value
        End Set
    End Property

    ''' <summary>
    ''' グリッドの左端固定部分の配置設定
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <param name="n"></param>
    '''  <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FixedXSAllignment(ByVal LayerNum As Integer, ByVal n As Integer) As Windows.Forms.HorizontalAlignment
        Get
            Return Grid_Property(LayerNum).FixedObjectNameData(n).Allignment
        End Get
        Set(ByVal value As Windows.Forms.HorizontalAlignment)
            Grid_Property(LayerNum).FixedObjectNameData(n).Allignment = value
        End Set
    End Property
    ''' <summary>
    ''' グリッドの左端固定部分の幅設定
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <param name="n"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FixedXSWidth(ByVal LayerNum As Integer, ByVal n As Integer) As Integer
        Get
            Return Grid_Property(LayerNum).FixedObjectNameData(n).Width
        End Get
        Set(ByVal value As Integer)
            Grid_Property(LayerNum).FixedObjectNameData(n).Width = value
        End Set
    End Property
    ''' <summary>
    ''' 上部固定部分の行ごとの配置設定
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <param name="n"></param>
    '''  <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FixedYSAllignment(ByVal LayerNum As Integer, ByVal n As Integer) As Windows.Forms.HorizontalAlignment
        Get
            Return Grid_Property(LayerNum).FixedDataItemData(n).Allignment
        End Get
        Set(ByVal value As Windows.Forms.HorizontalAlignment)
            Grid_Property(LayerNum).FixedDataItemData(n).Allignment = value
        End Set
    End Property
    ''' <summary>
    ''' 上部固定部分の行ごとの高さ設定
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <param name="n"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FixedYSHeight(ByVal LayerNum As Integer, ByVal n As Integer) As Integer
        Get
            Return Grid_Property(LayerNum).FixedDataItemData(n).Height
        End Get
        Set(ByVal value As Integer)
            Grid_Property(LayerNum).FixedDataItemData(n).Height = value
        End Set
    End Property
    ''' <summary>
    ''' レイヤの最大数を取得：実行時・取得のみ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LayerMax() As Integer
        Get
            Return Grid_Total.LayerNum
        End Get
    End Property
    ''' <summary>
    ''' レイヤタグを取得：：実行時・取得のみ　設定はAddLayerメソッド
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <param name="key"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LayerData(ByVal LayerNum As Integer, ByVal key As String) As Object
        Get
            Return Grid_Property(LayerNum).LayerData(key)
        End Get
        Set(ByVal value As Object)
            Grid_Property(LayerNum).LayerData(key) = value
        End Set
    End Property
    ''' <summary>
    ''' '現在のレイヤを取得：実行時
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Layer() As Integer
        Get
            Return Grid_Total.Layer
        End Get

        Set(ByVal value As Integer)
            SSTab.SelectedIndex = value
        End Set
    End Property
    ''' <summary>
    ''' 'レイヤ名を取得・設定
    ''' </summary>
    ''' <param name="LayerNum"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LayerName(ByVal LayerNum As Integer) As String
        Get
            Return Grid_Property(LayerNum).LayerName
        End Get
        Set(value As String)
            Grid_Property(LayerNum).LayerName = value
            Dim tab As TabPage = SSTab.TabPages(LayerNum)
            tab.Text = value
        End Set
    End Property
    ''' <summary>
    ''' タブをクリックしてレイヤメニューが出るかどうか
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TabClickEnabled() As Boolean
        Get
            Return Grid_Total.TabClickEnabled
        End Get
        Set(ByVal value As Boolean)
            Grid_Total.TabClickEnabled = value
        End Set
    End Property



    ''' <summary>
    ''' 'セルの既定色設定
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Property GridColor() As System.Drawing.Color
        Get
            Return Grid_Total.Color.Grid
        End Get
        Set(ByVal value As System.Drawing.Color)
            Grid_Total.Color.Grid = value
        End Set
    End Property
    ''' <summary>
    ''' セルの個別色設定
    ''' </summary>
    ''' <param name="Layernum"></param>
    ''' <param name="X"></param>
    ''' <param name="Y"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Property GridColor(ByVal Layernum As Integer, ByVal X As Integer, Y As Integer) As System.Drawing.Color
        Get
            If Grid_Property(Layernum).Grid_Text(X, Y).colorSetF = False Then
                Return Grid_Total.Color.Grid
            Else
                Return Grid_Property(Layernum).Grid_Text(X, Y).Color
            End If

        End Get
        Set(value As System.Drawing.Color)
            Grid_Property(Layernum).Grid_Text(X, Y).colorSetF = True
            Grid_Property(Layernum).Grid_Text(X, Y).Color = value
        End Set
    End Property
    ''' <summary>
    ''' レイヤのグリッドの色設定をすべてクリア
    ''' </summary>
    ''' <param name="Layernum"></param>
    ''' <remarks></remarks>
    Public Overloads Sub GridColorReset(ByVal Layernum As Integer)
        With Grid_Property(Layernum)
            For i As Integer = 0 To .Xmax - 1
                For j As Integer = 0 To .YMax - 1
                    .Grid_Text(i, j).colorSetF = False
                Next
            Next
        End With
        Print_Grid_Data()

    End Sub
    ''' <summary>
    ''' グリッドの色設定クリア
    ''' </summary>
    ''' <param name="Layernum"></param>
    ''' <param name="X"></param>
    ''' <param name="Y"></param>
    ''' <remarks></remarks>
    Public Overloads Sub GridColorReset(ByVal Layernum As Integer, ByVal X As Integer, Y As Integer)
        Grid_Property(Layernum).Grid_Text(X, Y).colorSetF = False
    End Sub

    ''' <summary>
    ''' '固定部分の色設定
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FixedGridColor() As System.Drawing.Color
        Get
            Return Grid_Total.Color.FixedGrid
        End Get
        Set(ByVal value As System.Drawing.Color)
            Grid_Total.Color.FixedGrid = value
        End Set
    End Property



    ''' <summary>
    ''' 'セル境界線色設定
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GridLineColor() As System.Drawing.Color
        Get
            Return Grid_Total.Color.GridLine
        End Get
        Set(ByVal value As System.Drawing.Color)
            Grid_Total.Color.GridLine = value
        End Set
    End Property

    ''' <summary>
    ''' '枠部分色設定
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FrameColor() As System.Drawing.Color
        Get
            Return Grid_Total.Color.Frame
        End Get
        Set(ByVal value As System.Drawing.Color)
            Grid_Total.Color.Frame = value
        End Set
    End Property



    ''' <summary>
    ''' '行のキャプション
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RowCaption() As String
        Get
            Return Grid_Total.RowCaption
        End Get
        Set(ByVal value As String)
            Grid_Total.RowCaption = value
            'mnuRowColNumber(0).Caption = Cap & "数の指定"
            'mnuInsertRow.Caption = Cap & "挿入"
            'mnuDeleteRow.Caption = Cap & "削除"
        End Set
    End Property

    ''' <summary>
    ''' '列のキャプション
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ColumnCaption() As String
        Get
            Return Grid_Total.ColumnCaption
        End Get
    End Property

    ''' <summary>
    ''' 'レイヤ行キャプション
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LayerCaption() As String
        Get
            Return Grid_Total.LayerCaption
        End Get
        Set(ByVal value As String)
            Grid_Total.LayerCaption = value

        End Set
    End Property


    ''' <summary>
    ''' レイヤ追加
    ''' </summary>
    ''' <param name="LayName">レイヤ名</param>
    ''' <param name="LayerNum">レイヤ追加位置</param>
    ''' <param name="Xsize">データ横列数</param>
    ''' <param name="Ysize">データ縦行数</param>
    ''' <remarks></remarks>
    Public Overloads Sub AddLayer(ByVal LayName As String, ByVal LayerNum As Integer, ByVal Xsize As Integer, Ysize As Integer)
        Call Insert_Layer(LayName, LayerNum, LayerNum, Xsize, Ysize, Grid_Total.tOpe)

    End Sub
    ''' <summary>
    ''' レイヤ追加
    ''' </summary>
    ''' <param name="LayName">レイヤ名</param>
    ''' <param name="LayerNum">レイヤ追加位置</param>
    ''' <param name="Xsize">データ横列数</param>
    ''' <param name="Ysize">データ縦行数</param>
    ''' <param name="RightClickEnabled">右クリックメニュー(コピー以外)可能フラグ</param>
    ''' <param name="RightClickAllEnabled">右クリックメニュー(全体)可能フラグ</param>
    ''' <param name="InputEnabled">入力可能フラグ</param>
    ''' <param name="GridRowEnabled">行削除・挿入メニュー可能フラグ</param>
    ''' <param name="GridColEnabled">列削除・挿入メニュー可能フラグ</param>
    ''' <param name="FixedXSEnabled">列固定領域の変更可能フラグ</param>
    ''' <param name="FixedYSEnabled">行固定領域の変更可能フラグ</param>
    ''' <param name="FixedUpperLeftEnabeld">左上固定領域の変更能フラグ</param>
    ''' <remarks></remarks>
    Public Overloads Sub AddLayer(ByVal LayName As String, ByVal LayerNum As Integer, ByVal Xsize As Integer, Ysize As Integer,
                            ByVal RightClickEnabled As Boolean, ByVal RightClickAllEnabled As Boolean, ByVal InputEnabled As Boolean, _
                            ByVal GridRowEnabled As Boolean, ByVal GridColEnabled As Boolean, _
                            ByVal FixedXSEnabled As Boolean, ByVal FixedYSEnabled As Boolean, _
                            ByVal FixedUpperLeftEnabeld As Boolean)
        Dim Ope As Operation_enable_info
        With Ope
            .FixedUpperLeftEnabeld = FixedUpperLeftEnabeld
            .FixedXSEnabled = FixedXSEnabled
            .FixedYSEnabled = FixedYSEnabled
            .GridColEnabled = GridColEnabled
            .GridRowEnabled = GridRowEnabled
            .InputEnabled = InputEnabled
            .RightClickEnabled = RightClickEnabled
            .RightClickAllEnabled = RightClickAllEnabled
        End With
        Insert_Layer(LayName, LayerNum, LayerNum, Xsize, Ysize, Ope)
    End Sub
    ''' <summary>
    ''' 'データ項目追加
    ''' </summary>
    ''' <param name="Layer">レイヤ番号</param>
    ''' <param name="AddPoint">追加横列位置</param>
    ''' <param name="AddNum">追加列数</param>
    ''' <remarks></remarks>
    Public Sub AddDataItem(ByVal Layer As Integer, ByVal AddPoint As Integer, ByVal AddNum As Integer)
        If Layer < 0 Or Grid_Total.LayerNum < Layer Then
            MsgBox("Layerが誤っています。", MsgBoxStyle.Exclamation, Grid_Total.MsgBoxTitle)
            Return
        End If
        If AddPoint < 0 Then
            MsgBox("AddPointが誤っています。", MsgBoxStyle.Exclamation, Grid_Total.MsgBoxTitle)
            Return
        End If
        If AddNum < 0 Then
            MsgBox("AddNumが誤っています。", MsgBoxStyle.Exclamation, Grid_Total.MsgBoxTitle)
            Return
        End If
        Dim xMaxS As Integer = Grid_Property(Layer).Xmax
        If xMaxS <= AddPoint Then
            AddPoint = xMaxS
        End If
        Call InsertColumns(Layer, AddPoint, AddNum)
    End Sub
    ''' <summary>
    ''' オブジェクト追加
    ''' </summary>
    ''' <param name="Layer">レイヤ番号</param>
    ''' <param name="AddPoint">追加縦行位置</param>
    ''' <param name="AddNum">追加行数</param>
    ''' <remarks></remarks>
    Public Sub AddObject(ByVal Layer As Integer, ByVal AddPoint As Integer, ByVal AddNum As Integer)
        If Layer < 0 Or Grid_Total.LayerNum < Layer Then
            MsgBox("Layerが誤っています。", MsgBoxStyle.Exclamation, Grid_Total.MsgBoxTitle)
            Return
        End If
        If AddPoint < 0 Then
            MsgBox("AddPointが誤っています。", MsgBoxStyle.Exclamation, Grid_Total.MsgBoxTitle)
            Return
        End If
        If AddNum < 0 Then
            MsgBox("AddNumが誤っています。", MsgBoxStyle.Exclamation, Grid_Total.MsgBoxTitle)
            Return
        End If
        Dim YMaxS As Integer = Grid_Property(Layer).YMax
        If YMaxS <= AddPoint Then
            AddPoint = YMaxS
        End If
        Call InsertRows(Layer, AddPoint, AddNum)
    End Sub
    ''' <summary>
    ''' レイヤ削除
    ''' </summary>
    ''' <param name="Layer"></param>
    ''' <remarks></remarks>
    Public Sub RemoveLayer(ByVal Layer)
        Dim mxt As Integer = SSTab.TabCount
        Delete_Layer(Layer)
        Set_SSTAB_Name()
        Dim nnt As Integer
        If Layer = mxt - 1 Then
            nnt = Layer - 1
        Else
            nnt = Layer
        End If
        SSTab.SelectedIndex = nnt
        Grid_Total.Layer = nnt
        Call Print_Grid_ViewSize()
        Call Print_Grid_Data()
    End Sub
    ''' <summary>
    ''' オブジェクト削除
    ''' </summary>
    ''' <param name="Layer">レイヤ</param>
    ''' <param name="RemovePoint">削除する位置</param>
    ''' <param name="RemoveNum">削除する数</param>
    ''' <remarks></remarks>
    Public Sub RemoveObject(ByVal Layer As Integer, ByVal RemovePoint As Integer, ByVal RemoveNum As Integer)
        If Layer < 0 Or Grid_Total.LayerNum < Layer Then
            MsgBox("Layerが誤っています。", MsgBoxStyle.Exclamation)
            Return
        End If
        If RemovePoint < 0 Then
            MsgBox("RemovePointが誤っています。", MsgBoxStyle.Exclamation)
            Return
        End If
        If RemoveNum < 0 Then
            MsgBox("RemoveNumが誤っています。", MsgBoxStyle.Exclamation)
            Return
        End If
        Dim YMaxS As Integer = Grid_Property(Layer).YMax
        If YMaxS <= RemovePoint Then
            MsgBox("RemovePointが誤っています。", MsgBoxStyle.Exclamation)
            Return
        ElseIf YMaxS < RemovePoint + RemoveNum Then
            RemoveNum = YMaxS - RemovePoint
        End If
        DeleteRows(Layer, RemovePoint, RemoveNum)
    End Sub

    ''' <summary>
    ''' データ設定後に表示
    ''' </summary>
    ''' <remarks></remarks>
    Public Overloads Sub Show()
        SSTab.Visible = Grid_Total.TabVisible
        pnlGridFrame.Refresh()
        picGrid.Visible = True
        txtTextBox.Visible = False
        Grid_Total.Layer = 0

        Call Print_Grid_ViewSize()
        Call Print_Grid_Data()
        Call Menu_Enable()
    End Sub
    ''' <summary>
    ''' データ項目削除
    ''' </summary>
    ''' <param name="Layer"></param>
    ''' <param name="RemovePoint"></param>
    ''' <param name="RemoveNum"></param>
    ''' <remarks></remarks>
    Public Sub RemoveDataItem(ByVal Layer As Integer, ByVal RemovePoint As Integer, ByVal RemoveNum As Integer)
        If Layer < 0 Or Grid_Total.LayerNum < Layer Then
            MsgBox("Layerが誤っています。", MsgBoxStyle.Exclamation)
            Return
        End If
        If RemovePoint < 0 Then
            MsgBox("RemovePointが誤っています。", MsgBoxStyle.Exclamation)
            Return
        End If
        If RemoveNum < 0 Then
            MsgBox("RemoveNumが誤っています。", MsgBoxStyle.Exclamation)
            Return
        End If
        Dim xMaxS As Integer = Grid_Property(Layer).Xmax
        If xMaxS <= RemovePoint Then
            MsgBox("RemovePointが誤っています。", MsgBoxStyle.Exclamation)
            Return
        ElseIf xMaxS < RemovePoint + RemoveNum Then
            RemoveNum = xMaxS - RemovePoint
        End If
        DeleteColumns(Layer, RemovePoint, RemoveNum)
    End Sub
    ''' <summary>
    ''' 検索
    ''' </summary>
    ''' <param name="FindStr">検索文字</param>
    ''' <param name="MatchingMode">マッチング</param>
    ''' <remarks></remarks>
    Public Sub Find(ByVal FindStr As String, ByVal MatchingMode As enmMatchingMode)

        Dim SPL As Integer = Grid_Total.Layer
        Dim SX As Integer, SY As Integer
        With Grid_Property(SPL)
            If .SelectedF = True Then
                SX = .MouseDownX
                SY = .MouseDownY
                If SX = -Grid_Total.FixedObjectName_n Then
                    SX += 1
                End If
                If SY = -Grid_Total.FixedDataItem_n Then
                    SY += 1
                End If
            Else
                SX = -Grid_Total.FixedObjectName_n + 1
                SY = -Grid_Total.FixedDataItem_n + 1
            End If

        End With
        Dim X As Integer = SX
        Dim Y As Integer = SY
        Dim L As Integer = SPL
        Dim Index As Integer = -1
        Do
            With Grid_Property(L)
                Y += 1
                If Y = .YMax Then
                    Y = -Grid_Total.FixedDataItem_n + 1
                    X += 1
                End If
                If X = .Xmax Then
                    L += 1
                    If L = Grid_Total.LayerNum Then
                        L = 0
                    End If
                    X = -Grid_Total.FixedObjectName_n + 1
                End If
            End With
            Dim gstr As String = Get_XYData(L, X, Y)
            If gstr Is Nothing = False Then
                Select Case MatchingMode
                    Case enmMatchingMode.PerfectMatching
                        If gstr = FindStr Then
                            Index = 0
                        End If
                    Case enmMatchingMode.PartialtMatching
                        Index = gstr.IndexOf(FindStr, StringComparison.OrdinalIgnoreCase)
                End Select
            End If
        Loop Until Index <> -1 Or (SX = X And SY = Y And SPL = L)
        If (SX = X And SY = Y And SPL = L And Index = -1) Then
            MsgBox("見つかりませんでした:" + FindStr, vbExclamation, Grid_Total.MsgBoxTitle)
        Else
            With Grid_Property(L)
                .TopCell = Math.Max(Y, 0)
                .LeftCell = Math.Max(X, 0)
                .SelectedF = True
                .MouseDownX = X
                .MouseDownY = Y
                .MouseUpX = X
                .MouseUpY = Y
            End With
            If Grid_Total.Layer <> L Then
                SSTab.SelectedIndex = L
            Else
                'HScrollGrid.Tag = "OFF"
                'VscrollGrid.Tag = "OFF"
                'If X < 0 Then
                '    HScrollGrid.Value = 0
                'Else
                '    HScrollGrid.Value = X
                'End If
                'If Y < 0 Then
                '    VscrollGrid.Value = 0
                'Else
                '    VscrollGrid.Value = Y
                'End If
                'HScrollGrid.Tag = ""
                'VscrollGrid.Tag = ""
                Call Print_Grid_Data()
            End If
        End If
    End Sub
    ''' <summary>
    ''' 逆方向検索
    ''' </summary>
    ''' <param name="FindStr">検索文字</param>
    ''' <param name="MatchingMode">マッチング</param>
    ''' <remarks></remarks>
    Public Sub FindRev(ByVal FindStr As String, ByVal MatchingMode As enmMatchingMode)


        Dim SPL As Integer = Grid_Total.Layer
        Dim SX As Integer, SY As Integer
        With Grid_Property(SPL)
            If .SelectedF = True Then
                SX = .MouseDownX
                SY = .MouseDownY
                If SX = -Grid_Total.FixedObjectName_n Then
                    SX += 1
                End If
                If SY = -Grid_Total.FixedDataItem_n Then
                    SY += 1
                End If
            Else
                SX = .Xmax - 1
                SY = .YMax - 1
            End If

        End With
        Dim X As Integer = SX
        Dim Y As Integer = SY
        Dim L As Integer = SPL
        Dim Index As Integer = -1
        Do
            With Grid_Property(L)
                Y -= 1
                If Y = -Grid_Total.FixedDataItem_n Then
                    Y = .YMax - 1
                    X -= 1
                End If
                If X = -Grid_Total.FixedObjectName_n Then
                    L -= 1
                    If L = -1 Then
                        L = Grid_Total.LayerNum - 1
                    End If
                    X = Grid_Property(L).Xmax - 1
                    Y = Grid_Property(L).YMax - 1
                End If
            End With
            Dim gstr As String = Get_XYData(L, X, Y)
            If gstr Is Nothing = False Then
                Select Case MatchingMode
                    Case enmMatchingMode.PerfectMatching
                        If gstr = FindStr Then
                            Index = 0
                        End If
                    Case enmMatchingMode.PartialtMatching
                        Index = gstr.IndexOf(FindStr, StringComparison.OrdinalIgnoreCase)
                End Select
            End If
        Loop Until Index <> -1 Or (SX = X And SY = Y And SPL = L)
        If (SX = X And SY = Y And SPL = L And Index = -1) Then
            MsgBox("見つかりませんでした:" + FindStr, vbExclamation, Grid_Total.MsgBoxTitle)
        Else
            With Grid_Property(L)
                .TopCell = Math.Max(Y, 0)
                .LeftCell = Math.Max(X, 0)
                .SelectedF = True
                .MouseDownX = X
                .MouseDownY = Y
                .MouseUpX = X
                .MouseUpY = Y
            End With
            If Grid_Total.Layer <> L Then
                SSTab.SelectedIndex = L
            Else
                Call Print_Grid_Data()
            End If
        End If
    End Sub


    ''' <summary>
    ''' 設定の変更を反映するため
    ''' </summary>
    ''' <remarks></remarks>
    Public Overloads Sub Refresh()
        Call Print_Grid_Data()
    End Sub

    '■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    '以下はコントロール内部－－－－－－－－－－－－－－－－－－－－

    Private Sub Set_Data_from_txtBox_To_Grid()
        'テキストボックスの文字をグリッドにセットする



        Dim tx As String = Get_Data_from_Grid(Grid_Total.Layer, GX, GY)

        Dim newTx As String = txtTextBox.Text
        If tx <> newTx$ Then
            Call SetUndo_Input(GX, GY, newTx$ & "の入力")
            Call Set_Data_To_Grid(Grid_Total.Layer, GX, GY, newTx$, True)
            Call Check_ChangeEvent(GX, GY)
        End If
    End Sub

    Private Sub Check_ChangeEvent(ByVal X As Integer, ByVal Y As Integer)
        Dim f As Boolean
        f = False
        With Grid_Property(Grid_Total.Layer).Ope
            If X < 0 And Y >= 0 And .FixedXSEnabled = True Then
                RaiseEvent Change_FixedXS()
                f = True
            End If
            If Y < 0 And X >= 0 And .FixedYSEnabled = True Then
                RaiseEvent Change_FixedYS()
                f = True
            End If
            If Y < 0 And X < 0 And .FixedUpperLeftEnabeld = True Then
                RaiseEvent Change_FixedUpperLeft()
                f = True
            End If
            If f = False And .InputEnabled = True Then
                RaiseEvent Change_Data()
            End If
        End With
    End Sub

    Private Sub Check_ChangeEventRange(ByVal X As Integer, ByVal Y As Integer, ByVal Xn As Integer, ByVal Yn As Integer)
        With Grid_Property(Grid_Total.Layer).Ope
            If Y < 0 And X < 0 And .FixedUpperLeftEnabeld = True Then
                RaiseEvent Change_FixedUpperLeft()
            End If
            If X < 0 And 0 <= Y + Yn - 1 And .FixedXSEnabled = True Then
                RaiseEvent Change_FixedXS()
            End If
            If Y < 0 And 0 <= X + Xn - 1 And .FixedYSEnabled = True Then
                RaiseEvent Change_FixedYS()
            End If
            If 0 <= X + Xn - 1 And 0 <= Y + Yn - 1 Then
                If .InputEnabled = True Then
                    RaiseEvent Change_Data()
                End If
            End If
        End With

    End Sub
    Private Sub mnuCopy_Click(sender As Object, e As EventArgs) Handles mnuCopy.Click
        Grid_Copy()
    End Sub

    Private Sub mnuPaste_Click(sender As Object, e As EventArgs) Handles mnuPaste.Click
        Dim clip As String = System.Windows.Forms.Clipboard.GetText
        If clip = "" Then Exit Sub
        Call Grid_Paste(clip, False)
    End Sub

    Private Sub mnuCut_Click(sender As Object, e As EventArgs) Handles mnuCut.Click
        Grid_Copy()
        Grid_Clear("切り取り")
    End Sub

    Private Sub mnuColWidth_Click(sender As Object, e As EventArgs) Handles mnuColWidth.Click

        With Grid_Property(Grid_Total.Layer)
            Dim X As Integer = .MouseDownX
            Dim w As Integer
            If X >= 0 Then
                w = .DataItemData(X).Width
            Else
                w = .FixedObjectNameData(X + Grid_Total.FixedObjectName_n).Width
            End If
            Dim SF As String = InputBox("列幅変更（ピクセル）", Grid_Total.MsgBoxTitle, w)
            If SF <> "" Then
                Dim left As Integer = Math.Min(.MouseDownX, .MouseUpX)
                Dim right As Integer = Math.Max(.MouseDownX, .MouseUpX)
                Dim width As Integer = Val(SF)
                width = Math.Max(1, width)
                SetUndo_ChangeColumnWidth(Grid_Total.Layer, left, right)
                ChangeColumnWidth(Grid_Total.Layer, left, right, width)
            End If
            Call Print_Grid_ViewSize()
            Call Print_Grid_Data()
        End With
    End Sub
    Private Sub SetUndo_ChangeColumnWidth(ByVal Layer As Integer, ByVal left As Integer, ByVal right As Integer)
        Dim UndoData As Undo_ChangeColumnWidth
        Dim oldW(right - left) As Integer
        With Grid_Property(Layer)
            For i As Integer = left To right
                If i < 0 Then
                    oldW(i - left) = .FixedObjectNameData(i + Grid_Total.FixedObjectName_n).Width
                Else
                    oldW(i - left) = .DataItemData(i).Width
                End If
            Next
        End With
        With UndoData
            .caption = "幅変更"
            .Layer = Layer
            .Width = oldW.Clone
            .Left = left
            .Right = right
        End With
        UndoArray.Add(UndoData)
        SetUndoMenu()
    End Sub
    Private Overloads Sub ChangeColumnWidth(ByVal Layer As Integer, ByVal left As Integer, ByVal right As Integer, ByVal Width() As Integer)
        With Grid_Property(Layer)
            For i As Integer = left To right
                If i < 0 Then
                    .FixedObjectNameData(i + Grid_Total.FixedObjectName_n).Width = Width(i - left)
                Else
                    .DataItemData(i).Width = Width(i - left)
                End If
            Next
        End With
    End Sub
    Private Overloads Sub ChangeColumnWidth(ByVal Layer As Integer, ByVal left As Integer, ByVal right As Integer, ByVal Width As Integer)
        With Grid_Property(Layer)
            For i As Integer = left To right
                If i < 0 Then
                    .FixedObjectNameData(i + Grid_Total.FixedObjectName_n).Width = Width
                Else
                    .DataItemData(i).Width = Width
                End If
            Next
        End With
    End Sub
    Private Sub mnuDeleteCol_Click(sender As Object, e As EventArgs) Handles mnuDeleteCol.Click

        With Grid_Property(Grid_Total.Layer)
            Dim rect As Rectangle = .MouseUpDownRect
            Dim r1 As Integer = rect.Left
            Dim r2 As Integer = rect.Right
            Dim r As Integer = r2 - r1 + 1
            If r1 < 0 Or r = .Xmax Then Exit Sub
            SetUndo_DeleteColumns(Grid_Total.ColumnCaption & "削除", r1, r)
            Call DeleteColumns(Grid_Total.Layer, r1, r)
            If (.Xmax <= .MouseUpX) Then
                .MouseUpX = .Xmax - 1
            End If
            If (.Xmax <= .MouseDownX) Then
                .MouseDownX = .Xmax - 1
            End If
            Call Print_Grid_Data()
            RaiseEvent Change_FixedYS()
            RaiseEvent Change_Data()
        End With
    End Sub

    Private Sub mnuDeleteRow_Click(sender As Object, e As EventArgs) Handles mnuDeleteRow.Click

        With Grid_Property(Grid_Total.Layer)
            Dim rect As Rectangle = .MouseUpDownRect
            Dim r1 As Integer = rect.Top
            Dim r2 As Integer = rect.Bottom
            Dim r As Integer = r2 - r1 + 1
            If r1 < 0 Or r = .YMax Then Exit Sub
            SetUndo_DeleteRows(Grid_Total.RowCaption & "削除", r1, r)
            Call DeleteRows(Grid_Total.Layer, r1, r)
            If (.YMax <= .MouseUpY) Then
                .MouseUpY = .YMax - 1
            End If
            If (.Xmax <= .MouseDownY) Then
                .MouseDownY = .YMax - 1
            End If
            Call Print_Grid_Data()
            RaiseEvent Change_FixedXS()
            RaiseEvent Change_Data()
        End With
    End Sub
    Private Sub mnuInsertRowDownward_Click(sender As Object, e As EventArgs) Handles mnuInsertRowDownward.Click, mnuInsertRowUpword.Click

        With Grid_Property(Grid_Total.Layer)
            Dim ip As Integer
            Dim rect As Rectangle = .MouseUpDownRect
            If rect.Top < 0 Then Exit Sub
            Dim r As Integer = rect.Bottom - rect.Top + 1
            If sender.name = "mnuInsertRowDownward" Then
                ip = rect.Top + 1
            Else
                ip = rect.Top
                .MouseDownY = .MouseDownY + r
                .MouseUpY = .MouseUpY + r
            End If

            SetUndo_InsertRows(Grid_Total.RowCaption & "挿入", ip, r)
            Call InsertRows(Grid_Total.Layer, ip, r)
            RaiseEvent Change_FixedXS()
            RaiseEvent Change_Data()
        End With
        Call Print_Grid_Data()

    End Sub




    Private Sub mnuRowNumber_Click(sender As Object, e As EventArgs) Handles mnuRowNumber.Click
        Dim PV As Integer = Grid_Property(Grid_Total.Layer).YMax
        Dim SF As String = InputBox(Grid_Total.RowCaption & "数指定", Grid_Total.MsgBoxTitle, PV)
        If SF <> "" Then
            Dim V As Integer = Val(SF)
            Dim n As Integer = V - PV
            If V > 0 And n <> 0 Then
                If n < 0 Then
                    SetUndo_DeleteRows(Grid_Total.RowCaption & "数変更", PV + n, -n)
                    Call DeleteRows(Grid_Total.Layer, PV + n, -n)
                    Call Print_Grid_Data()
                    RaiseEvent Change_FixedXS()
                    RaiseEvent Change_Data()
                Else
                    Call InsertRows(Grid_Total.Layer, PV, n)
                    Call Print_Grid_Data()
                    RaiseEvent Change_FixedXS()
                    RaiseEvent Change_Data()
                End If
            End If
        End If
    End Sub

    Private Sub mnuColNumber_Click(sender As Object, e As EventArgs) Handles mnuColNumber.Click
        Dim PV As Integer = Grid_Property(Grid_Total.Layer).Xmax
        Dim SF As String = InputBox(Grid_Total.ColumnCaption & "数指定", Grid_Total.MsgBoxTitle, PV)
        If SF <> "" Then
            Dim V As Integer = Val(SF)
            Dim n As Integer = V - PV
            If V > 0 And n <> 0 Then
                If n < 0 Then
                    SetUndo_DeleteColumns(Grid_Total.ColumnCaption & "数指定", PV + n, -n)
                    Call DeleteColumns(Grid_Total.Layer, PV + n, -n)
                    Call Print_Grid_Data()
                    RaiseEvent Change_FixedYS()
                    RaiseEvent Change_Data()
                Else
                    SetUndo_InsertColumns(Grid_Total.ColumnCaption & "数指定", PV, n)
                    Call InsertColumns(Grid_Total.Layer, PV, n)
                    Call Print_Grid_Data()
                    RaiseEvent Change_FixedYS()
                    RaiseEvent Change_Data()
                End If
            End If
        End If
    End Sub

    Private Sub mnuRowHeight_Click(sender As Object, e As EventArgs) Handles mnuRowHeight.Click


        With Grid_Property(Grid_Total.Layer)
            Dim Y As Integer = .MouseDownY
            Dim H As Integer
            If Y >= 0 Then
                H = .CellHeight(Y)
            Else
                H = .FixedDataItemData(Y + Grid_Total.FixedDataItem_n).Height
            End If
            Dim SF As String = InputBox("行高変更（ピクセル）", Grid_Total.MsgBoxTitle, H)

            If SF <> "" Then
                Dim top As Integer = Math.Min(.MouseDownY, .MouseUpY)
                Dim bottom As Integer = Math.Max(.MouseDownY, .MouseUpY)
                Dim Height As Integer = Val(SF)
                Height = Math.Max(1, Height)
                SetUndo_ChangeRowHeight(Grid_Total.Layer, top, bottom)
                ChangeRowHeight(Grid_Total.Layer, top, bottom, Height)
            End If
            Call Print_Grid_ViewSize()
            Call Print_Grid_Data()
        End With
    End Sub
    Private Overloads Sub ChangeRowHeight(ByVal Layer As Integer, ByVal top As Integer, ByVal bottom As Integer, ByVal Height() As Integer)
        With Grid_Property(Layer)
            For i As Integer = top To bottom
                If i < 0 Then
                    .FixedDataItemData(i + Grid_Total.FixedDataItem_n).Height = Height(i - top)
                Else
                    .CellHeight(i) = Height(i - top)
                End If
            Next
        End With
    End Sub
    Private Overloads Sub ChangeRowHeight(ByVal Layer As Integer, ByVal top As Integer, ByVal bottom As Integer, ByVal Height As Integer)
        With Grid_Property(Layer)
            For i As Integer = top To bottom
                If i < 0 Then
                    .FixedDataItemData(i + Grid_Total.FixedDataItem_n).Height = Height
                Else
                    .CellHeight(i) = Height
                End If
            Next
        End With
    End Sub

    Private Sub picGrid_DoubleClick(sender As Object, e As EventArgs) Handles picGrid.DoubleClick
        GridMouseDown = True
    End Sub


    Private Sub picGrid_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles picGrid.PreviewKeyDown
        Dim CTRL_Key As Boolean = My.Computer.Keyboard.CtrlKeyDown
        Dim Shift_Key As Boolean = My.Computer.Keyboard.ShiftKeyDown
        If (CTRL_Key = True And e.KeyValue = Keys.ControlKey) Or
                (Shift_Key = True And e.KeyValue = Keys.ShiftKey) Then
            Exit Sub
        End If

        If CTRL_Key = True Then
            Select Case e.KeyValue
                Case Keys.Z 'Ctrl+Z
                    Call Undo()
                    Exit Sub
                Case Keys.X 'Ctrl+X
                    If mnuCut.Enabled = True And Grid_Property(Grid_Total.Layer).Ope.InputEnabled = True Then
                        Grid_Copy()
                        Grid_Clear("切り取り")
                        Exit Sub
                    End If
                Case Keys.C 'Ctrl+C
                    If mnuCopy.Enabled = True Then
                        Call Grid_Copy()
                        Exit Sub
                    End If
                Case Keys.V 'Ctrl+V
                    If mnuPaste.Enabled = True And Grid_Property(Grid_Total.Layer).Ope.InputEnabled = True Then
                        Dim clip As String = System.Windows.Forms.Clipboard.GetText()
                        If clip <> "" Then
                            Call Grid_Paste(clip, False)
                            Exit Sub
                        End If
                    End If
            End Select
        End If

        If Grid_Property(Grid_Total.Layer).SelectedF = False Then
            Exit Sub
        End If
        If e.KeyValue = Keys.Delete And Grid_Property(Grid_Total.Layer).Ope.InputEnabled = True Then
            Grid_Clear("削除")
        Else
            If CTRL_Key = True Then
                'CTRL+カーソル／上下左右端にとぶ
                With Grid_Property(Grid_Total.Layer)
                    Dim x As Integer, y As Integer
                    If Shift_Key = True Then
                        x = .MouseUpX
                        y = .MouseUpY
                    Else
                        x = .MouseDownX
                        y = .MouseDownY
                    End If
                    If e.KeyValue = Keys.Left Then
                        x = 0
                        GX = x
                        .LeftCell = x
                        HScrollGrid_ValueSet()
                    End If
                    If e.KeyValue = Keys.Right Then
                        x = .Xmax - 1
                        GX = x
                        .LeftCell = x
                        HScrollGrid_ValueSet()
                    End If
                    If e.KeyValue = Keys.Up Then
                        y = 0
                        GY = y
                        .TopCell = y
                        Call VScrollGrid_ValueSet()
                    End If
                    If e.KeyValue = Keys.Down Then
                        y = .YMax - 1
                        GY = y
                        .TopCell = y
                        Call VScrollGrid_ValueSet()
                    End If
                    If Shift_Key = False Then
                        .MouseDownX = x
                        .MouseDownY = y
                    End If
                    .MouseUpX = x
                    .MouseUpY = y
                End With
                Call Print_Grid_Data()
            End If
            If CTRL_Key = False And e.KeyValue <> Keys.Tab And Shift_Key = True Then
                'SHIFT+カーソル／選択範囲移動
                Call Key_Move(e.KeyValue, True)
                Call Print_Grid_Data()
            End If
            If (CTRL_Key = False And Shift_Key = False) Or (e.KeyValue = Keys.Tab) Then
                '通常の移動
                Call Key_Move(e.KeyValue, Shift_Key)
                Call Print_Grid_Data()
            End If

        End If
    End Sub
    Private Sub Key_Move(ByVal KeyCode As Integer, ByVal Shit_f As Boolean)

        Dim X As Integer, Y As Integer

        With Grid_Property(Grid_Total.Layer)
            If Shit_f = True Then
                X = .MouseUpX
                Y = .MouseUpY
            Else
                X = .MouseDownX
                Y = .MouseDownY
            End If
            Select Case KeyCode
                Case Keys.Left, Keys.Right, Keys.Down, Keys.Up, Keys.Tab, Keys.Return, Keys.PageDown, Keys.PageUp, Keys.Home
                    If KeyCode = Keys.Left Or (KeyCode = Keys.Tab And Shit_f = True) Then
                        X -= 1
                    End If
                    If KeyCode = Keys.Right Or (KeyCode = Keys.Tab And Shit_f = False) Then
                        X += 1
                    End If
                    If KeyCode = Keys.Up Then
                        Y -= 1
                    End If
                    If KeyCode = Keys.Down Or KeyCode = Keys.Return Then
                        Y += 1
                    End If
                    If KeyCode = Keys.PageDown Then
                        Y += 20
                    End If
                    If KeyCode = Keys.PageUp Then
                        Y -= 20
                    End If
                    If KeyCode = Keys.Home Then
                        X = 0
                        Y = 0
                    End If

                    If X >= .Xmax Then
                        X = .Xmax - 1
                    End If
                    If X < -Grid_Total.FixedObjectName_n + Grid_Total.FixedObjectName_n2 Then
                        X = -Grid_Total.FixedObjectName_n + Grid_Total.FixedObjectName_n2
                    End If
                    If Y >= .YMax Then
                        Y = .YMax - 1
                    End If
                    If Y < -Grid_Total.FixedDataItem_n + Grid_Total.FixedDataItem_n2 Then
                        Y = -Grid_Total.FixedDataItem_n + Grid_Total.FixedDataItem_n2
                    End If

                    Scroll_Set(X, Y)

                    .SelectedF = False

                    If Shit_f = False Or KeyCode = Keys.Tab Then
                        Call Print_Grid_Data()
                    End If
                    GX = X
                    GY = Y
                    .SelectedF = True
                    If Shit_f = False Or KeyCode = Keys.Tab Then
                        .MouseDownX = GX
                        .MouseDownY = GY
                    End If
                    .MouseUpX = GX
                    .MouseUpY = GY
            End Select
        End With
    End Sub

    Private Sub Scroll_Set(ByVal X As Integer, ByVal Y As Integer)

        Dim hv As Integer = HScrollGrid.Value
        Dim vv As Integer = VscrollGrid.Value
        With Grid_Property(Grid_Total.Layer)
            If .BottomCell < Y And Y <= .YMax - 1 And vv < .YMax - 1 Then
                .TopCell += 1
                Call VScrollGrid_ValueSet()

            ElseIf 0 <= Y And Y < .TopCell Then
                .TopCell = Y
                Call VScrollGrid_ValueSet()
            End If
            If .RightCell < X And X <= .Xmax - 1 And hv < .Xmax - 1 Then
                .LeftCell += 1
                HScrollGrid_ValueSet()
            ElseIf X < .LeftCell And X >= 0 Then
                .LeftCell = X
                HScrollGrid_ValueSet()
            End If
        End With
    End Sub

    Private Sub SSTab_Click(sender As Object, e As EventArgs) Handles SSTab.Click
        If (Control.MouseButtons And MouseButtons.Right) = MouseButtons.Right Then
            popupTabMenu()
        End If
    End Sub

    Private Sub SSTab_DoubleClick(sender As Object, e As EventArgs) Handles SSTab.DoubleClick
        If Grid_Total.TabClickEnabled = True Then
            mnuTabRightMenuChangeTabName.PerformClick()
        End If
    End Sub
    Private Sub SSTab_MouseDown(sender As Object, e As MouseEventArgs) Handles SSTab.MouseDown
        For i As Integer = 0 To SSTab.TabCount - 1
            If SSTab.GetTabRect(i).Contains(e.X, e.Y) Then
                SSTab.SelectedIndex = i
                Return
            End If
        Next i
    End Sub


    Private Sub popupTabMenu()
        If Grid_Total.TabClickEnabled = True Then
            If Grid_Total.LayerNum = 1 Then
                mnuTabRightMenuDeleteTab.Enabled = False
                mnuTabRightMenuMoveTab.Enabled = False
            Else
                If Grid_Total.Layer = Grid_Total.LayerNum - 1 Then
                    mnuTabRightMenuMoveTabRear.Enabled = False
                    mnuTabRightMenuMoveTabRight.Enabled = False
                Else
                    mnuTabRightMenuMoveTabRear.Enabled = True
                    mnuTabRightMenuMoveTabRight.Enabled = True
                End If
                If Grid_Total.Layer = 0 Then
                    mnuTabRightMenuMoveTabAhead.Enabled = False
                    mnuTabRightMenuMoveTabLeft.Enabled = False
                Else
                    mnuTabRightMenuMoveTabAhead.Enabled = True
                    mnuTabRightMenuMoveTabLeft.Enabled = True
                End If
                mnuTabRightMenuDeleteTab.Enabled = True
                mnuTabRightMenuMoveTab.Enabled = True
            End If
            Dim p As System.Drawing.Point = System.Windows.Forms.Cursor.Position
            ContextMenuStripTab.Show(p)
        End If

    End Sub
    Private Sub SSTab_MouseUp(sender As Object, e As MouseEventArgs) Handles SSTab.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            popupTabMenu()
        End If
    End Sub

    Private Sub SSTab_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SSTab.SelectedIndexChanged
        If SSTab.Tag = "OFF" Then
            Return
        End If
        If txtTextBox.Visible = True Then
            txtTextBox.Visible = False
            Call Set_Data_from_txtBox_To_Grid()
        End If
        Dim PreviousTab As Integer = Grid_Total.Layer
        Grid_Total.Layer = SSTab.SelectedIndex
        With Grid_Property(Grid_Total.Layer)
            GX = .LeftCell
            GY = .TopCell
        End With
        Call Print_Grid_ViewSize()
        Call VScrollGrid_ValueSet()
        Call Print_Grid_Data()
        Call Menu_Enable()
        RaiseEvent Change_LayerSelect(Grid_Total.Layer, PreviousTab)
    End Sub
    Private Sub Menu_Enable()


        With Grid_Property(Grid_Total.Layer).Ope
            If .RightClickAllEnabled = False Then
                picGrid.ContextMenuStrip = Nothing
            Else
                picGrid.ContextMenuStrip = ContextMenuStripTabGrid
            End If
            mnuPaste.Enabled = .RightClickEnabled
            mnuCut.Enabled = .RightClickEnabled
            mnuInsertCol.Enabled = .GridColEnabled
            mnuInsertRow.Enabled = .GridRowEnabled

            mnuDeleteCol.Enabled = .GridColEnabled
            mnuDeleteRow.Enabled = .GridRowEnabled

            mnuRowNumber.Enabled = .GridRowEnabled
            mnuColNumber.Enabled = .GridColEnabled
        End With

    End Sub

    Private Sub KTGISGrid_Load(sender As Object, e As EventArgs) Handles Me.Load
        ToolTip1 = New ToolTip()
        'ToolTipが表示されるまでの時間
        ToolTip1.InitialDelay = 200
        'ToolTipが表示されている時に、別のToolTipを表示するまでの時間
        ToolTip1.ReshowDelay = 1000
        'ToolTipを表示する時間
        ToolTip1.AutoPopDelay = 10000
        'フォームがアクティブでない時でもToolTipを表示する
        ToolTip1.ShowAlways = True
    End Sub

 




    Private Sub ktGrid3_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        Print_Grid_ViewSize()
        If Grid_Total.LayerNum <> 0 Then
            Call Print_Grid_Data()
        End If
    End Sub

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        '色の定義
        With Grid_Total
            .initOK = False
            .GridFont = Me.Font
            .DefaultFixedXNumberingWidth = 50
            .DefaultFixedXWidth = 150
            .DefaultGridWidth = 100
            .DefaultFixedUpperLeftAlligntment = HorizontalAlignment.Left
            .DefaultFixedXSAllignment = HorizontalAlignment.Left
            .DefaultFixedYSAllignment = HorizontalAlignment.Left
            .DefaultGridAlligntment = HorizontalAlignment.Right
            .DefaultNumberingAlligntment = HorizontalAlignment.Center
            With .Color
                .TextBox = Drawing.Color.FromArgb(200, 220, 240)
                .GridLine = Drawing.Color.FromArgb(&H80, &H80, &H80)
                .Grid = Drawing.Color.White
                .FixedGrid = Drawing.Color.FromArgb(&HAA, &HFF, &HAA)
                .Frame = Drawing.Color.FromArgb(&HCA, &HCA, &HCA)    '上端、左端の固定部分
            End With
            .MsgBoxTitle = ""
        End With
        SSTab.Tag = "OFF"
        SSTab.Left = 0
        SSTab.TabPages.Clear()
        SSTab.TabPages.Add("KtgisGrid")
        SSTab.SelectedIndex = 0
        SSTab.Visible = True
        SSTab.Tag = ""
        picGrid.Visible = False
        txtTextBox.Visible = False
    End Sub

    Private Sub VscrollGrid_GotFocus(sender As Object, e As EventArgs) Handles VscrollGrid.GotFocus
        picGrid.Focus()
    End Sub



    Private Sub VscrollGrid_ValueChanged(sender As Object, e As EventArgs) Handles VscrollGrid.ValueChanged
        If VscrollGrid.Tag = "" Then
            If txtTextBox.Visible = True Then
                Call Set_Data_from_txtBox_To_Grid()
                txtTextBox.Visible = False
            End If
            With Grid_Property(Grid_Total.Layer)
                .TopCell = VscrollGrid.Value
            End With
            Call Print_Grid_Data()
        End If
    End Sub
    Private Sub VScrollGrid_ValueSet()
        VscrollGrid.Tag = "OFF"
        With Grid_Property(Grid_Total.Layer)
            VscrollGrid.Value = .TopCell
        End With
        VscrollGrid.Tag = ""
    End Sub
    Private Sub HScrollGrid_ValueSet()
        HScrollGrid.Tag = "OFF"
        With Grid_Property(Grid_Total.Layer)
            HScrollGrid.Value = .LeftCell
        End With
        HScrollGrid.Tag = ""
    End Sub

    Private Sub HScrollGrid_GotFocus(sender As Object, e As EventArgs) Handles HScrollGrid.GotFocus
        picGrid.Focus()
    End Sub


    Private Sub HScrollGrid_ValueChanged(sender As Object, e As EventArgs) Handles HScrollGrid.ValueChanged
        If HScrollGrid.Tag = "" Then
            If txtTextBox.Visible = True Then
                Call Set_Data_from_txtBox_To_Grid()
                txtTextBox.Visible = False
            End If
            With Grid_Property(Grid_Total.Layer)
                .LeftCell = HScrollGrid.Value
            End With
            Call Print_Grid_Data()
        End If
    End Sub

    Private Sub Print_Grid_Data()



        Dim MBX1 As Integer, MBY1 As Integer, MBX2 As Integer, MBY2 As Integer

        Dim picW As Integer = picGrid.Width
        Dim picH As Integer = picGrid.Height
        If picW = 0 Or picH = 0 Then
            Return
        End If

        Dim txtF As Boolean = txtTextBox.Visible
        Dim xs As Integer
        Dim ys As Integer
        With Grid_Property(Grid_Total.Layer)
            xs = .Xmax
            ys = .YMax
            Dim TopCell As Integer = .TopCell
            If TopCell > ys - 1 Then
                TopCell = ys - 1
                .TopCell = TopCell
            End If
            Dim LeftCell As Integer = .LeftCell
            If LeftCell > xs - 1 Then
                LeftCell = xs - 1
                .LeftCell = LeftCell
            End If

            HScrollGrid.Tag = "OFF"
            VscrollGrid.Tag = "OFF"
            If LeftCell < 0 Then
                HScrollGrid.Value = 0
            Else
                HScrollGrid.Value = LeftCell
            End If
            If TopCell < 0 Then
                VscrollGrid.Value = 0
            Else
                VscrollGrid.Value = TopCell
            End If
            HScrollGrid.Tag = ""
            VscrollGrid.Tag = ""

            MBX1 = Math.Min(.MouseDownX, .MouseUpX)
            MBX2 = Math.Max(.MouseDownX, .MouseUpX)
            MBY1 = Math.Min(.MouseDownY, .MouseUpY)
            MBY2 = Math.Max(.MouseDownY, .MouseUpY)
        End With

        Dim canvas As New Bitmap(picW, picH)
        Dim g As Graphics = Graphics.FromImage(canvas)
        With Grid_Property(Grid_Total.Layer)

            Dim font As Font = Grid_Total.GridFont

            Dim Y As Integer = .FixedDataItemHeight
            Dim j As Integer = TopCell()
            Do
                Dim X As Integer = .FixedObjectNameDataWidth
                Dim i As Integer = LeftCell()
                Dim bkCol As System.Drawing.Color
                Do
                    If .Grid_Text(i, j).colorSetF = False Then
                        bkCol = Grid_Total.Color.Grid
                    Else
                        bkCol = .Grid_Text(i, j).Color
                    End If
                    If .SelectedF = True Then
                        If (MBX1 <= i And i <= MBX2) And (MBY1 <= j And j <= MBY2) Then
                            bkCol = Grid_Total.Color.SelectedGrid
                        End If
                    End If
                    Print_Data(.Grid_Text(i, j).Text, .DataItemData(i).Allignment, X, Y, .DataItemData(i).Width, .CellHeight(j), .GridLineCol, bkCol, 0, font, g)
                    X += .DataItemData(i).Width
                    i += 1
                Loop While X < picW And i < xs
                'オブジェクト名
                X = 0
                For i = 0 To Grid_Total.FixedObjectName_n - 1
                    If .FixedObjectName(i, j).colorSetF = True Then
                        bkCol = .FixedObjectName(i, j).Color
                    Else
                        If i < Grid_Total.FixedObjectName_n2 Then
                            bkCol = Grid_Total.Color.Frame
                        Else
                            bkCol = Grid_Total.Color.FixedGrid
                        End If
                    End If
                    If .SelectedF = True Then
                        If (MBY1 <= j And j <= MBY2) Then
                            If (MBX1 <= -(Grid_Total.FixedObjectName_n - i) And -(Grid_Total.FixedObjectName_n - i) <= MBX2) Then
                                bkCol = GetDarkColor(bkCol)
                            Else
                                If i < Grid_Total.FixedObjectName_n2 Then
                                    bkCol = GetDarkColor(bkCol)
                                End If
                            End If
                        End If
                    End If
                    Print_Data(.FixedObjectName(i, j).Text, .FixedObjectNameData(i).Allignment, X, Y, .FixedObjectNameData(i).Width, .CellHeight(j), .GridLineCol, bkCol, 1, font, g)
                    X += .FixedObjectNameData(i).Width
                Next
                Y += .CellHeight(j)
                j += 1
            Loop While Y < picH And j < ys
            If Y >= picH Then
                .BottomCell = j - 2
            Else
                .BottomCell = j - 1
            End If
        End With

        With Grid_Property(Grid_Total.Layer)
            'データ項目
            Dim X As Integer = .FixedObjectNameDataWidth
            Dim i As Integer = LeftCell()
            Do
                Dim Y As Integer = 0
                For j As Integer = 0 To Grid_Total.FixedDataItem_n - 1
                    Dim bkCol As System.Drawing.Color
                    If .FixedDataItem(i, j).colorSetF = True Then
                        bkCol = .FixedDataItem(i, j).Color
                    Else
                        If j < Grid_Total.FixedDataItem_n2 Then
                            bkCol = Grid_Total.Color.Frame
                        Else
                            bkCol = Grid_Total.Color.FixedGrid
                        End If
                    End If

                    If .SelectedF = True Then
                        If (MBX1 <= i And i <= MBX2) Then
                            If (MBY1 <= -(Grid_Total.FixedDataItem_n - j) And -(Grid_Total.FixedDataItem_n - j) <= MBY2) Then
                                bkCol = GetDarkColor(bkCol)
                            Else
                                If j < Grid_Total.FixedDataItem_n2 Then
                                    bkCol = GetDarkColor(bkCol)
                                End If
                            End If
                        End If
                    End If
                    Print_Data(.FixedDataItem(i, j).Text, .FixedDataItemData(j).Allignment, X, Y, .DataItemData(i).Width, .FixedDataItemData(j).Height, .GridLineCol, bkCol, 0, Font, g)
                    Y += .FixedDataItemData(j).Height
                Next
                X += .DataItemData(i).Width
                i += 1
            Loop While X < picW And i < xs
            If X >= picW Then
                If .Xmax = 1 Then
                    .RightCell = 0
                Else
                    .RightCell = i - 2
                End If
            Else
                .RightCell = i - 1
            End If
        End With

        With Grid_Property(Grid_Total.Layer)
            '左上固定部分
            Dim X As Integer = 0
            For i As Integer = 0 To Grid_Total.FixedObjectName_n - 1
                Dim Y As Integer = 0
                For j As Integer = 0 To Grid_Total.FixedDataItem_n - 1
                    Dim bkCol As System.Drawing.Color
                    If i < Grid_Total.FixedObjectName_n2 Or j < Grid_Total.FixedDataItem_n2 Then
                        bkCol = Grid_Total.Color.Frame
                    Else
                        bkCol = Grid_Total.Color.FixedGrid
                    End If
                    If .SelectedF = True Then
                        Dim dkf As Boolean = False
                        If (MBX1 <= -(Grid_Total.FixedObjectName_n - i) And -(Grid_Total.FixedObjectName_n - i) <= MBX2) _
                            And (MBY1 <= -(Grid_Total.FixedDataItem_n - j) And -(Grid_Total.FixedDataItem_n - j) <= MBY2) Then
                            dkf = True
                        End If
                        If (MBX1 <= -(Grid_Total.FixedObjectName_n - i) And -(Grid_Total.FixedObjectName_n - i) <= MBX2) Then
                            If j < Grid_Total.FixedDataItem_n2 Then
                                dkf = True
                            End If
                        End If
                        If (MBY1 <= -(Grid_Total.FixedDataItem_n - j) And -(Grid_Total.FixedDataItem_n - j) <= MBY2) Then
                            If i < Grid_Total.FixedObjectName_n2 Then
                                dkf = True
                            End If
                        End If
                        If dkf = True Then
                            bkCol = GetDarkColor(bkCol)
                        End If
                    End If
                    Call Print_Data(.FixedUpperLeft(i, j).Text, .FixedUpperLeftAllignment, X, Y, .FixedObjectNameData(i).Width, .FixedDataItemData(j).Height, .GridLineCol, bkCol, 0, Font, g)
                    Y += .FixedDataItemData(j).Height
                Next
                X += .FixedObjectNameData(i).Width
            Next

            picGrid.Refresh()


            If txtF = True Then
                If .MouseDownX > .RightCell Or .MouseDownX < .LeftCell Or .MouseDownY > .BottomCell Or .MouseDownY < .TopCell Then
                Else
                    txtTextBox.Visible = True
                    Call SetTextBox(.MouseDownX, .MouseDownY)
                End If
            End If
            picGrid.Image = canvas
            picGrid.Refresh()
            g.Dispose()

        End With

    End Sub

    Private Sub Get_Object_to_Cell_Size(ByVal X As Integer, ByVal Y As Integer, ByRef Obj As Object)
        'テキストボックス、コンボボックスを指定した位置のセルのサイズに合わせる
        Dim i As Integer, w As Integer, H As Integer, lef As Integer, tp As Integer
        Dim n As Integer

        With Grid_Property(Grid_Total.Layer)
            If X < 0 Then
                lef = 0
                n = X + Grid_Total.FixedObjectName_n
                For i = 0 To n - 1
                    lef += .FixedObjectNameData(i).Width
                Next
                With .FixedObjectNameData(n)
                    w = .Width
                End With
            Else
                lef = .FixedObjectNameDataWidth
                For i = .LeftCell To X - 1
                    lef = lef + .DataItemData(i).Width
                Next
                With .DataItemData(X)
                    w = .Width
                End With
            End If
            If Y < 0 Then
                n = Y + Grid_Total.FixedDataItem_n
                tp = 0
                For i = 0 To n - 1
                    tp += .FixedDataItemData(i).Height
                Next
                H = .FixedDataItemData(n).Height
            Else
                tp = .FixedDataItemHeight
                For i = .TopCell To Y - 1
                    tp += .CellHeight(i)
                Next
                H = .CellHeight(Y)
            End If
        End With

        On Error Resume Next
        With Obj
            .Width = w - 1
            .Left = lef + 1
            .Height = H - 1
            .Top = tp + 1
        End With
        On Error GoTo 0
    End Sub

    Private Sub SetTextBox(ByVal X As Integer, Y As Integer)
        Dim AL As Windows.Forms.HorizontalAlignment, n As Integer

        Call Get_Object_to_Cell_Size(X, Y, txtTextBox)

        With Grid_Property(Grid_Total.Layer)
            If X < 0 Then
                n = X + Grid_Total.FixedObjectName_n
                With .FixedObjectNameData(n)
                    AL = .Allignment
                End With
            Else
                With .DataItemData(X)
                    AL = .Allignment
                End With
            End If
        End With

        Dim tx As String = Get_Data_from_Grid(Grid_Total.Layer, X, Y)
        With txtTextBox
            .TextAlign = AL
            .Text = tx$
            .SelectionStart = Len(tx)
            .BackColor = Grid_Total.Color.TextBox
            .Visible = True
            .Focus()
        End With
    End Sub
    Private Sub Print_Data(ByVal ST As String, ByVal Allignment As Windows.Forms.HorizontalAlignment,
                ByVal X As Integer, ByVal Y As Integer,
                ByVal CellW As Integer, ByVal CellHeight As Integer, ByVal BorderColor As Color,
                ByVal Fillcolor As Color, ByVal BorderWidth As Integer, ByRef font As Font, ByRef g As Graphics)

        Dim txtw As Integer, txth As Integer
        Dim i As Integer, H As Integer

        Dim ST2 As String

        Dim rect As New Rectangle(X, Y, CellW + 1, CellHeight + 1)
        Dim Cliprect As New Rectangle(X, Y, CellW + 2, CellHeight + 2)
        g.SetClip(Cliprect)

        g.FillRectangle(New SolidBrush(Fillcolor), rect)
        g.DrawRectangle(New Pen(BorderColor, 1), rect)

        Dim S_Size As Size = g.MeasureString(ST, font).ToSize
        txtw = Int(S_Size.Width)
        txth = Int(S_Size.Height)
        If txtw >= CellW - 4 And Allignment = Windows.Forms.HorizontalAlignment.Center Then
            Allignment = Windows.Forms.HorizontalAlignment.Left
        End If
        Select Case Allignment
            Case Windows.Forms.HorizontalAlignment.Left '左詰
                If txtw <= CellW - 4 And InStr(ST, vbCrLf) = 0 Then
                    g.DrawString(ST, font, Brushes.Black, X + 2, Y + 2)
                Else
                    H = 0
                    Do
                        i = 1
                        Do
                            ST2 = Microsoft.VisualBasic.Left(ST, i)
                            S_Size = g.MeasureString(ST2, font).ToSize
                            i += 1
                        Loop While S_Size.Width < CellW - 4 And i <= Len(ST) And Mid(ST, i - 1) <> vbCrLf
                        If S_Size.Width >= CellW - 4 Then
                            i -= 1
                        End If
                        ST2 = Microsoft.VisualBasic.Left(ST, i - 1)
                        g.DrawString(ST2, font, Brushes.Black, X + 2, Y + H + 2)
                        If Mid(ST, i) = vbCrLf Then
                            ST = Mid(ST, i - 1)
                        Else
                            ST = Mid(ST, i)
                        End If
                        H += txth
                    Loop While H < CellHeight - 4 And ST$ <> ""
                End If
            Case Windows.Forms.HorizontalAlignment.Center
                g.DrawString(ST, font, Brushes.Black, X + CellW / 2 - txtw / 2, Y + 2)

            Case Windows.Forms.HorizontalAlignment.Right
                If txtw < CellW - 4 Then
                    g.DrawString(ST, font, Brushes.Black, X + CellW - 2 - txtw, Y + 2)
                Else
                    i = 1
                    Do
                        ST2 = Microsoft.VisualBasic.Right(ST, i)
                        S_Size = g.MeasureString(ST2, font).ToSize
                        i += 1
                    Loop While S_Size.Width < CellW - 4
                    ST2 = Microsoft.VisualBasic.Right(ST, i - 1)
                    g.DrawString(ST2, font, Brushes.Black, X + CellW - 2 - S_Size.Width, Y + 2)
                End If
        End Select
        g.ResetClip()


    End Sub
    ''' <summary>
    ''' 初期化
    ''' </summary>
    ''' <param name="LayerCaption">タブのキャプション</param>
    ''' <param name="RowCaption">行のキャプション</param>
    ''' <param name="ColumnCaption">列のキャプション</param>
    ''' <param name="FixedXs">左端固定列数</param>
    ''' <param name="FixedXs2">左端固定列数のうち左側の固定数</param>
    ''' <param name="FixedYs">状態固定行数</param>
    ''' <param name="FixedYS2">状態固定行数のうち上側の行数</param>
    ''' <param name="RightClickEnabled">右クリックメニュー(コピー以外)可能フラグ</param>
    ''' <param name="RightClickAllEnabled">右クリックメニュー(全体)可能フラグ</param>
    ''' <param name="InputEnabled">入力可能フラグ</param>
    ''' <param name="GridRowEnabled">行削除・挿入メニュー可能フラグ</param>
    ''' <param name="GridColEnabled">列削除・挿入メニュー可能フラグ</param>
    ''' <param name="FixedXSEnabled">列固定領域の変更可能フラグ</param>
    ''' <param name="FixedYSEnabled">行固定領域の変更可能フラグ</param>
    ''' <param name="FixedUpperLeftEnabeld">左上固定領域の変更能フラグ</param>
    ''' <param name="TABvisible">タブを表示するか</param>
    ''' <param name="TabClickEnabled">タブクリックメニューの表示フラグ</param>
    ''' <remarks></remarks>
    Public Sub init(ByVal LayerCaption As String, ByVal RowCaption As String, ByVal ColumnCaption As String, ByVal FixedXs As Integer, ByVal FixedXs2 As Integer, ByVal FixedYs As Integer, ByVal FixedYS2 As Integer, _
                            ByVal RightClickEnabled As Boolean, ByVal RightClickAllEnabled As Boolean, ByVal InputEnabled As Boolean, _
                            ByVal GridRowEnabled As Boolean, ByVal GridColEnabled As Boolean, _
                            ByVal FixedXSEnabled As Boolean, ByVal FixedYSEnabled As Boolean, _
                            ByVal FixedUpperLeftEnabeld As Boolean, ByVal TABvisible As Boolean,
                            ByVal TabClickEnabled As Boolean)

        With Grid_Total
            .LayerCaption = LayerCaption
            .RowCaption = RowCaption
            .ColumnCaption = ColumnCaption
            mnuTabRightMenuChangeTabName.Text = LayerCaption & "名の変更"
            mnuTabRightMenuMoveTab.Text = LayerCaption & "の移動"
            mnuTabRightMenuInsertTab.Text = "新しい" & LayerCaption & "の挿入"
            mnuTabRightMenuDeleteTab.Text = LayerCaption & "の削除"
            mnuRowNumber.Text = RowCaption & "数の指定"
            mnuInsertRow.Text = RowCaption & "の挿入"
            mnuDeleteRow.Text = RowCaption & "の削除"
            mnuColNumber.Text = ColumnCaption & "数の指定"
            mnuInsertCol.Text = ColumnCaption & "の挿入"
            mnuDeleteCol.Text = ColumnCaption & "の削除"

            .FixedObjectName_n = FixedXs
            .FixedObjectName_n2 = FixedXs2
            .FixedDataItem_n = FixedYs
            .FixedDataItem_n2 = FixedYS2
            .tOpe.RightClickEnabled = RightClickEnabled
            .tOpe.RightClickAllEnabled = RightClickAllEnabled
            .tOpe.InputEnabled = InputEnabled
            If RightClickEnabled = False Then
                GridRowEnabled = False
                GridColEnabled = False
            End If
            If FixedXSEnabled = False Then
                GridRowEnabled = False
            End If
            If FixedYSEnabled = False Then
                GridColEnabled = False
            End If
            .tOpe.GridRowEnabled = GridRowEnabled
            .tOpe.GridColEnabled = GridColEnabled
            .tOpe.FixedXSEnabled = FixedXSEnabled
            .tOpe.FixedYSEnabled = FixedYSEnabled
            .tOpe.FixedUpperLeftEnabeld = FixedUpperLeftEnabeld
            .TabVisible = TABvisible
            .TabClickEnabled = TabClickEnabled
            'If TabClickEnabled = False Then
            '    SSTab.ContextMenuStrip = Nothing
            'Else
            '    SSTab.ContextMenuStrip = ContextMenuStripTab
            'End If

            .Layer = 0
            .LayerNum = 0
            With .Color
                .SelectedFixedGrid = GetDarkColor(.FixedGrid)
                .SelectedFrame = GetDarkColor(.Frame)
                .SelectedGrid = GetDarkColor(.Grid)
            End With
        End With
        GX = 0
        GY = 0
        UndoArray.Clear()
        ReDim Grid_Property(0)

    End Sub
    ''' <summary>
    ''' レイヤ挿入
    ''' </summary>
    ''' <param name="LayName"></param>
    ''' <param name="lay"></param>
    ''' <param name="OriginalLayerNumber"></param>
    ''' <param name="xs"></param>
    ''' <param name="ys"></param>
    ''' <param name="OperationEnable"></param>
    ''' <remarks></remarks>
    Private Sub Insert_Layer(ByVal LayName As String, ByVal lay As Integer, ByVal OriginalLayerNumber As Integer,
                             ByVal xs As Integer, ByVal ys As Integer, ByVal OperationEnable As Operation_enable_info)


        With Grid_Total
            .LayerNum += 1
            ReDim Preserve Grid_Property(.LayerNum - 1)
            If .LayerNum <> 1 Then
                For i As Integer = .LayerNum - 1 To lay + 1 Step -1
                    Grid_Property(i) = Grid_Property(i - 1).Clone
                Next
            End If
        End With

        With Grid_Property(lay)
            .OriginalLayerNumber = OriginalLayerNumber
            ReDim .Grid_Text(xs - 1, ys - 1)
            ReDim .FixedObjectName(Grid_Total.FixedObjectName_n - 1, ys - 1)
            ReDim .FixedDataItem(xs - 1, Grid_Total.FixedDataItem_n - 1)
            ReDim .FixedUpperLeft(Grid_Total.FixedObjectName_n - 1, Grid_Total.FixedDataItem_n - 1)

            .Ope = OperationEnable
            .LayerName = LayName
            .LayerData = New Dictionary(Of String, Object)

            .Xmax = xs
            .YMax = ys
            ReDim .CellHeight(ys)

        End With
        With Grid_Total
            Call Set_SSTAB_Name()
            If .Layer >= lay And .LayerNum > 1 Then
                .Layer += 1
                SSTab.Tag = "OFF"
                SSTab.SelectedIndex = .Layer
                SSTab.Tag = ""
            End If
        End With
        Call Init_Grid(lay)
    End Sub

    Private Sub Set_SSTAB_Name()

        SSTab.Tag = "OFF"
        If SSTab.TabCount < Grid_Total.LayerNum Then
            Dim mxt As Integer
            mxt = Grid_Total.LayerNum
            For i As Integer = SSTab.TabCount To Grid_Total.LayerNum - 1
                SSTab.TabPages.Add("")
            Next
        ElseIf SSTab.TabCount > Grid_Total.LayerNum Then
            For i As Integer = SSTab.TabCount - 1 To Grid_Total.LayerNum Step -1
                SSTab.TabPages.RemoveAt(i)
            Next
        End If
        SSTab.Tag = ""
        For i As Integer = 0 To Grid_Total.LayerNum - 1
            Dim tpage As System.Windows.Forms.TabPage
            tpage = SSTab.TabPages(i)
            tpage.Text = Grid_Property(i).LayerName
        Next
    End Sub

    Private Sub Init_Grid(ByVal L As Integer)

        With Grid_Property(L)
            ReDim .DataItemData(.Xmax - 1)
            ReDim .CellHeight(.YMax - 1)
            ReDim .FixedObjectNameData(Grid_Total.FixedObjectName_n - 1)
            ReDim .FixedDataItemData(Grid_Total.FixedDataItem_n - 1)
            .SelectedF = False
            Dim xs As Integer = .Xmax
            Dim ys As Integer = .YMax
            .GridLineCol = Grid_Total.Color.GridLine

            Dim S_Size As SizeF = picGrid.CreateGraphics.MeasureString("A", Grid_Total.GridFont)
            Dim H As Integer = S_Size.Height + 4
            For i As Integer = 0 To ys - 1
                .CellHeight(i) = H
            Next
            For i As Integer = 0 To Grid_Total.FixedDataItem_n - 1
                With .FixedDataItemData(i)
                    .Height = H
                    If i = 0 Then
                        .Allignment = Grid_Total.DefaultNumberingAlligntment
                    Else
                        .Allignment = Grid_Total.DefaultFixedYSAllignment
                    End If
                End With
            Next

            For i As Integer = 0 To .Xmax - 1
                With .DataItemData(i)
                    .Width = Grid_Total.DefaultGridWidth
                    .Allignment = Grid_Total.DefaultGridAlligntment
                End With
            Next

            For i As Integer = 0 To Grid_Total.FixedObjectName_n - 1
                With .FixedObjectNameData(i)
                    If i = 0 Then
                        .Width = Grid_Total.DefaultFixedXNumberingWidth
                        .Allignment = Grid_Total.DefaultNumberingAlligntment
                    Else
                        .Width = Grid_Total.DefaultFixedXWidth
                        .Allignment = Grid_Total.DefaultFixedXSAllignment
                    End If
                End With
            Next

            .FixedUpperLeftAllignment = Grid_Total.DefaultFixedUpperLeftAlligntment
            txtTextBox.Font = Grid_Total.GridFont
        End With
        Call Set_FixedCell_Words(L)

    End Sub
    ''' <summary>
    ''' マウス押下位置を取得、falseの場合は選択無し
    ''' </summary>
    ''' <param name="Layer"></param>
    ''' <param name="pos"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetXY(ByVal Layer As Integer, ByRef pos As Point) As Boolean
        With Grid_Property(Layer)
            pos = New Point(.MouseDownX, .MouseDownY)

            Return .SelectedF
        End With

    End Function

    Private Sub Print_Grid_ViewSize()
        Dim gw = pnlGridFrame.Width
        Dim gh As Integer = pnlGridFrame.Height
        If Grid_Total.LayerNum = 0 Then

            picGrid.Top = 0
            picGrid.Left = 0
            picGrid.Width = gw
            picGrid.Height = gh
            Exit Sub
        End If

        With Grid_Property(Grid_Total.Layer)
            Dim xs As Integer = .Xmax
            Dim ys As Integer = .YMax
            txtTextBox.Font = Grid_Total.GridFont

            Dim w As Integer = .FixedObjectNameDataWidth
            Dim H As Integer = .FixedDataItemHeight
            picGrid.Top = 0
            picGrid.Left = 0
            VscrollGrid.Top = H
            VscrollGrid.Left = gw - VscrollGrid.Width
            HScrollGrid.Top = gh - HScrollGrid.Height
            HScrollGrid.Left = w
            VscrollGrid.Height = gh - H
            HScrollGrid.Width = Math.Max(1, gw - w)

            w = .FixedObjectNameDataWidth
            For i As Integer = 0 To xs - 1
                w += .DataItemData(i).Width
            Next
            If w > picGrid.Width Then
                HScrollGrid.Visible = True
                HScrollGrid.Maximum = xs - 1
                picGrid.Height = gh - HScrollGrid.Height
            Else
                HScrollGrid.Visible = False
                picGrid.Height = gh
            End If

            H = .FixedDataItemHeight
            For i As Integer = 0 To ys - 1
                H += .CellHeight(i)
            Next
            If H > picGrid.Height Then
                VscrollGrid.Visible = True
                VscrollGrid.Maximum = ys - 1
                picGrid.Width = gw - VscrollGrid.Width
            Else
                VscrollGrid.Visible = False
                .TopCell = 0
                picGrid.Width = gw
            End If
        End With

    End Sub



    Private Sub picGrid_MouseDown1(sender As Object, e As MouseEventArgs) Handles picGrid.MouseDown
        Dim x As Integer = e.X, y As Integer = e.Y
        Dim xx As Integer, yy As Integer
        Dim i As Integer


        If GridResize.Enable <> 0 Then
            With GridResize
                .Enable = check_Width_Height_Change(x, y, .GridX, .GridY, .LeftX, .TopY)
                .firstF = True
            End With
            GridMouseDown = True
        Else
            If txtTextBox.Visible = True Then
                txtTextBox.Visible = False
                Call Set_Data_from_txtBox_To_Grid()
                Call Print_Grid_Data()
            End If
            With Grid_Property(Grid_Total.Layer)
                Call GetGridXY(x, y, xx, yy, True)
                If e.Button = Windows.Forms.MouseButtons.Left Then '左クリック
                    If xx < -Grid_Total.FixedObjectName_n + Grid_Total.FixedObjectName_n2 _
                        And yy < -Grid_Total.FixedDataItem_n + Grid_Total.FixedDataItem_n2 Then '左上端をクリック
                        .MouseDownX = -Grid_Total.FixedObjectName_n
                        .MouseDownY = -Grid_Total.FixedDataItem_n
                        .MouseUpX = .Xmax - 1
                        .MouseUpY = .YMax - 1
                    Else
                        If xx = -Grid_Total.FixedObjectName_n And 0 < Grid_Total.FixedObjectName_n Then  '左端をクリック
                            .MouseDownX = -Grid_Total.FixedObjectName_n
                            .MouseDownY = yy
                            .MouseUpX = .Xmax - 1
                            .MouseUpY = .MouseDownY
                            .MouseDown_Mode = 2
                            GridMouseDown = True
                        ElseIf yy = -Grid_Total.FixedDataItem_n And 0 < Grid_Total.FixedDataItem_n Then  '上端をクリック
                            .MouseDownX = xx
                            .MouseDownY = -Grid_Total.FixedDataItem_n
                            .MouseUpX = .MouseDownX
                            .MouseUpY = .YMax - 1
                            .MouseDown_Mode = 1
                            GridMouseDown = True
                        ElseIf yy < -Grid_Total.FixedDataItem_n + Grid_Total.FixedDataItem_n2 And xx >= 0 Then
                            '上端の固定部分その２　ClickFixedYS2イベントを発生させる
                            .MouseDownX = xx
                            .MouseDownY = yy
                            .MouseUpX = xx
                            .MouseUpY = yy
                            GridMouseDown = False
                            Dim CellLeft As Integer = .FixedObjectNameDataWidth
                            For i = .LeftCell To xx - 1
                                CellLeft += .DataItemData(i).Width
                            Next
                            Dim Celltop As Single = pnlGridFrame.Top
                            Dim Y2 As Integer = Grid_Total.FixedDataItem_n + yy
                            For i = 0 To Y2 - 1
                                Celltop += .FixedDataItemData(i).Height
                            Next
                            RaiseEvent Click_FixedYS2(Grid_Total.Layer, xx, Y2, .FixedDataItem(xx, Y2).Text, Celltop, CellLeft, .DataItemData(xx).Width, .FixedDataItemData(Y2).Height)
                            Exit Sub
                        Else
                            'グリッドのデータ部分
                            .MouseDownX = xx
                            .MouseDownY = yy
                            .MouseUpX = xx
                            .MouseUpY = yy
                            .MouseDown_Mode = 0
                            GridMouseDown = True
                        End If
                        picGrid.Capture = True
                    End If
                    If xx < 0 Then
                        .LeftCell = 0
                        HScrollGrid_ValueSet()
                    End If
                    If yy < 0 Then
                        .TopCell = 0
                        Call VScrollGrid_ValueSet()
                    End If
                    .SelectedF = True
                    Call Print_Grid_Data()

                ElseIf e.Button = Windows.Forms.MouseButtons.Right Then '右クリック
                    If .SelectedF = True And ((.MouseDownX <= xx And xx <= .MouseUpX) Or (.MouseDownX >= xx And xx >= .MouseUpX)) And _
                            ((.MouseDownY <= yy And yy <= .MouseUpY) Or (.MouseDownY >= yy And yy >= .MouseUpY)) Then
                        '右クリックで、選択範囲内をクリックした場合は範囲を変更しない
                    Else
                        If xx = -Grid_Total.FixedObjectName_n And yy = -Grid_Total.FixedDataItem_n Then '左上端をクリック
                            .MouseDownX = xx
                            .MouseDownY = yy
                            .MouseUpX = .Xmax - 1
                            .MouseUpY = .YMax - 1
                        Else
                            If xx = -Grid_Total.FixedObjectName_n Then '左端をクリック
                                .MouseDownX = xx
                                .MouseDownY = yy
                                .MouseUpX = .Xmax - 1
                                .MouseUpY = .MouseDownY
                                .MouseDown_Mode = 2
                            ElseIf yy = -Grid_Total.FixedDataItem_n Then '上端をクリック
                                .MouseDownX = xx
                                .MouseDownY = yy
                                .MouseUpX = .MouseDownX
                                .MouseUpY = .YMax - 1
                                .MouseDown_Mode = 1
                            Else
                                .MouseDownX = xx
                                .MouseDownY = yy
                                .MouseUpX = xx
                                .MouseUpY = yy
                            End If
                        End If
                    End If
                    .SelectedF = True
                    Call Print_Grid_Data()
                    'PopupMenu mnuGridRightButton
                End If
            End With
        End If
    End Sub

    Private Sub picGrid_MouseMove(sender As Object, e As MouseEventArgs) Handles picGrid.MouseMove
        Dim x As Integer = e.X
        Dim y As Integer = e.Y
        Dim xx As Integer
        Dim yy As Integer

        Dim Dx As Integer
        Dim Dy As Integer

        If txtTextBox.Visible = False Then
            picGrid.Focus()

        End If
        If GridMouseDown = False Then
            GridResize.Enable = check_Width_Height_Change(x, y, Dx, Dy, Dx, Dy)
            If GridResize.Enable <> 0 Then
                picGrid.Cursor = Cursors.Cross
            Else
                picGrid.Cursor = Cursors.Default
                'マウス移動中にセルの値ツールチップ表示
                GetGridXY(x, y, xx, yy, True)
                Dim v As String = Get_Data_from_Grid(Grid_Total.Layer, xx, yy)
                If ToolTip1.GetToolTip(picGrid) <> v Then
                    ToolTip1.SetToolTip(picGrid, v)
                End If
            End If
            Return
        End If

        If GridResize.Enable <> 0 Then
            Call Print_GridResizeLine(x, y)
        Else
            With Grid_Property(Grid_Total.Layer)
                Dim f As Boolean
                If .MouseDownX = -Grid_Total.FixedObjectName_n Or .MouseDownY = -Grid_Total.FixedDataItem_n Then
                    f = True
                Else
                    f = False
                End If
                If (y > picGrid.Height Or x > picGrid.Width Or y < .FixedDataItemHeight Or x < .FixedObjectNameDataWidth) Then
                    TimerMouse.Enabled = True
                    TimerMouse.Interval = 5
                    TimerVX = x
                    TimerVY = y
                Else
                    TimerMouse.Enabled = False
                    Call GetGridXY(x, y, xx, yy, f)
                    Select Case .MouseDown_Mode
                        Case 0
                            .MouseUpX = xx
                            .MouseUpY = yy
                        Case 1
                            .MouseUpX = xx
                        Case 2
                            .MouseUpY = yy
                    End Select
                    Call Print_Grid_Data()
                End If
            End With
        End If
    End Sub


    Private Sub picGrid_MouseUp(sender As Object, e As MouseEventArgs) Handles picGrid.MouseUp
        Dim xx As Integer, yy As Integer
        Dim f As Boolean
        Dim x As Integer = e.X, y As Integer = e.Y

        If GridMouseDown = True Then
            GridMouseDown = False
            If GridResize.Enable <> 0 Then
                Call Grid_Resize_MouseUp(x, y)
                GridResize.Enable = 0

            Else
                TimerMouse.Enabled = False
                picGrid.Capture = False
                With Grid_Property(Grid_Total.Layer)
                    Call GetGridXY(x, y, xx, yy, f)
                    If .MouseDownX = xx And .MouseDownY = yy And .MouseDown_Mode = 0 Then
                        If yy = .BottomCell And yy < .YMax - 1 Then
                            .TopCell += 1
                            Call VScrollGrid_ValueSet()
                            Call Print_Grid_Data()
                        End If
                        If xx = .RightCell And xx < .Xmax - 1 Then
                            .LeftCell += 1
                            HScrollGrid_ValueSet()
                            Call Print_Grid_Data()
                        End If
                        If GX = xx And GY = yy And Grid_Property(Grid_Total.Layer).Ope.InputEnabled = True Then
                            If (xx < 0 And yy >= 0 And Grid_Property(Grid_Total.Layer).Ope.FixedXSEnabled = False) Or _
                                    (xx >= 0 And yy < 0 And Grid_Property(Grid_Total.Layer).Ope.FixedYSEnabled = False) Or _
                                    (xx < 0 And yy < 0 And Grid_Property(Grid_Total.Layer).Ope.FixedUpperLeftEnabeld = False) Then
                                '固定部分が編集できない状態
                            Else
                                Call Print_Grid_Data()
                                Call SetTextBox(.MouseDownX, .MouseDownY)
                            End If
                        Else
                            GX = xx
                            GY = yy
                            If xx >= 0 And yy >= 0 And Grid_Property(Grid_Total.Layer).Ope.InputEnabled = False Then
                                Dim CellLeft As Integer = .FixedObjectNameDataWidth
                                For i = .LeftCell To xx - 1
                                    CellLeft += .DataItemData(i).Width
                                Next
                                Dim Celltop As Single = .FixedDataItemHeight
                                For i = 0 To yy - 1
                                    Celltop += .CellHeight(i)
                                Next
                                RaiseEvent Click_DataGrid(Grid_Total.Layer, xx, yy, .Grid_Text(xx, yy).Text, Celltop, CellLeft, .DataItemData(xx).Width, .CellHeight(yy))
                            End If

                        End If
                    End If
                End With
            End If
        End If
    End Sub

    Private Sub GetGridXY(ByVal MouseX As Single, ByVal MouseY As Single,
                          ByRef GridX As Integer, ByRef GridY As Integer, ByVal ZaroLine_F As Boolean)
        With Grid_Property(Grid_Total.Layer)
            GridX = GetGridX(MouseX, ZaroLine_F)
            GridY = GetGridY(MouseY, ZaroLine_F)
        End With
    End Sub
    Private Function GetGridX(ByVal MouseX As Single, ByVal ZaroLine_F As Boolean) As Integer

        Dim GridX As Integer
        With Grid_Property(Grid_Total.Layer)
            Dim w As Integer
            If MouseX < .FixedObjectNameDataWidth Then
                w = 0
                For i As Integer = 0 To Grid_Total.FixedObjectName_n - 1
                    w += .FixedObjectNameData(i).Width
                    If MouseX < w Then
                        GridX = -(Grid_Total.FixedObjectName_n - i)
                        Exit For
                    End If
                Next
                If ZaroLine_F = False And GridX < -Grid_Total.FixedObjectName_n + Grid_Total.FixedObjectName_n2 Then
                    GridX = -Grid_Total.FixedObjectName_n + Grid_Total.FixedObjectName_n2
                End If
            Else
                w = .FixedObjectNameDataWidth
                GridX = .RightCell
                For i As Integer = .LeftCell To .RightCell
                    w += .DataItemData(i).Width
                    If MouseX < w Then
                        GridX = i
                        Exit For
                    End If
                Next
            End If
        End With
        Return GridX
    End Function

    Private Function GetGridY(ByVal MouseY As Single, ByVal ZaroLine_F As Boolean) As Integer
        Dim GridY As Integer
        Dim H As Integer

        With Grid_Property(Grid_Total.Layer)
            If MouseY < .FixedDataItemHeight Then
                H = 0
                For i As Integer = 0 To Grid_Total.FixedDataItem_n - 1
                    H += +.FixedDataItemData(i).Height
                    If MouseY < H Then
                        GridY = -(Grid_Total.FixedDataItem_n - i)
                        Exit For
                    End If
                Next
                If ZaroLine_F = False And GridY < -Grid_Total.FixedDataItem_n + Grid_Total.FixedDataItem_n2 Then
                    GridY = -Grid_Total.FixedDataItem_n + Grid_Total.FixedDataItem_n2
                End If
            Else
                H = .FixedDataItemHeight
                GridY = .BottomCell
                For i As Integer = .TopCell To .BottomCell
                    H += .CellHeight(i)
                    If MouseY < H Then
                        GridY = i
                        Exit For
                    End If
                Next
            End If
        End With
        Return GridY
    End Function

    ''' <summary>
    ''' グリッド上のマウスカーソルが列・行の境界線上にあるかどうかを調べる 0を返す/変更できない位置、1/行高変更可、2/列幅変更可
    ''' </summary>
    ''' <param name="mx"></param>
    ''' <param name="my"></param>
    ''' <param name="ResizeX"></param>
    ''' <param name="ResizeY"></param>
    ''' <param name="lX"></param>
    ''' <param name="lY"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function check_Width_Height_Change(ByVal mx As Single, my As Single, ByRef ResizeX As Integer, ByRef ResizeY As Integer, ByRef lX As Integer, ByRef lY As Integer) As Integer

        Dim i As Integer, s As Integer
        Dim H As Integer, w As Integer
        Dim OV As Integer

        check_Width_Height_Change = 0
        With Grid_Property(Grid_Total.Layer)
            If mx < .FixedObjectNameDataWidth Then
                '行高変更位置
                If my <= .FixedDataItemHeight + 2 And 0 < .FixedDataItemHeight Then
                    i = 0
                    H = 0
                    Do
                        OV = H
                        H = H + .FixedDataItemData(i).Height
                        s = Math.Abs(my - H)
                        i += 1
                    Loop While i < Grid_Total.FixedDataItem_n And s > 2
                    i = (i - 1) - Grid_Total.FixedDataItem_n
                Else
                    i = .TopCell
                    H = .FixedDataItemHeight
                    Do
                        OV = H
                        H = H + .CellHeight(i)
                        s = Math.Abs(my - H)
                        i += 1
                    Loop While i <= .BottomCell And s > 2
                    i -= 1
                End If
                If s <= 2 Then
                    check_Width_Height_Change = 1
                    lY = OV
                    ResizeY = i
                End If
            End If
            If my < .FixedDataItemHeight Then
                '列幅変更位置
                If mx <= .FixedObjectNameDataWidth + 2 Then
                    i = 0
                    w = 0
                    Do
                        OV = w
                        w = w + .FixedObjectNameData(i).Width
                        s = Math.Abs(mx - w)
                        i += 1
                    Loop While i < Grid_Total.FixedObjectName_n And s > 2
                    i = (i - 1) - Grid_Total.FixedObjectName_n
                Else
                    i = .LeftCell
                    w = .FixedObjectNameDataWidth
                    Do
                        OV = w
                        w = w + .DataItemData(i).Width
                        s = Math.Abs(mx - w)
                        i += 1
                    Loop While i <= .RightCell And s > 2
                    i -= 1
                End If
                If s <= 2 Then
                    check_Width_Height_Change = 2
                    lX = OV
                    ResizeX = i
                End If
            End If
        End With

    End Function

    Private Sub Print_GridResizeLine(ByVal mox As Single, ByVal moy As Single)

        Dim w As Integer, H As Integer
        Dim pen As New Pen(Color.Black)
        With GridResize
            If GridResize.firstF = False Then
                picGrid.Refresh()
            End If
            Dim g As Graphics = picGrid.CreateGraphics
            Select Case .Enable
                Case 1
                    '1/行高変更可
                    w = picGrid.Width
                    g.DrawLine(pen, 0, .TopY, w, .TopY)
                    If moy <= .TopY Then
                        moy = .TopY + 1
                    End If
                    g.DrawLine(pen, 0, moy, w, moy)

                Case 2
                    '2/列幅変更
                    H = picGrid.Height
                    g.DrawLine(pen, .LeftX, 0, .LeftX, H)
                    If mox <= .LeftX Then
                        mox = .LeftX + 1
                    End If
                    g.DrawLine(pen, mox, 0, mox, H)

            End Select
        End With
        pen.Dispose()
        GridResize.firstF = False
    End Sub

    Private Sub Grid_Resize_MouseUp(ByVal X As Single, Y As Single)
        Dim S1 As Integer, s2 As Integer, T As Integer, TY As Integer, tx As Integer
        Dim w As Integer, H As Integer

        With GridResize
            Select Case .Enable
                Case 1
                    '行高変更位置
                    TY = .GridY
                    T = .TopY
                    H = (Y - T) + 1
                    H = Math.Max(2, H)
                    With Grid_Property(Grid_Total.Layer)
                        If .SelectedF = True Then
                            Dim rect As Rectangle = .MouseUpDownRect
                            S1 = rect.Top
                            s2 = rect.Bottom
                            If S1 <= TY And TY <= s2 Then
                            Else
                                S1 = TY
                                s2 = TY
                            End If
                        Else
                            S1 = TY
                            s2 = TY
                        End If
                        SetUndo_ChangeRowHeight(Grid_Total.Layer, S1, s2)
                        ChangeRowHeight(Grid_Total.Layer, S1, s2, H)
                    End With
                Case 2
                    '列幅変更位置
                    tx = .GridX
                    T = .LeftX
                    w = (X - T) + 1
                    w = Math.Max(3, w)
                    With Grid_Property(Grid_Total.Layer)
                        If .SelectedF = True Then
                            Dim rect As Rectangle = .MouseUpDownRect
                            S1 = rect.Left
                            s2 = rect.Right
                            If S1 <= tx And tx <= s2 Then
                            Else
                                S1 = tx
                                s2 = tx
                            End If
                        Else
                            S1 = tx
                            s2 = tx
                        End If
                        SetUndo_ChangeColumnWidth(Grid_Total.Layer, S1, s2)
                        ChangeColumnWidth(Grid_Total.Layer, S1, s2, w)
                    End With
            End Select
        End With
        Call Print_Grid_ViewSize()
        Call Print_Grid_Data()

    End Sub



    Private Function Get_Data_from_Grid(ByVal Grid_Lay As Integer, ByVal X As Integer, ByVal Y As Integer) As String
        '指定された位置のグリッド配列のデータを取得する
        Dim tx As String

        With Grid_Property(Grid_Lay)
            If X < 0 And Y < 0 Then
                tx = .FixedUpperLeft(X + Grid_Total.FixedObjectName_n, Y + Grid_Total.FixedDataItem_n).Text
            ElseIf X < 0 Then
                tx = .FixedObjectName(X + Grid_Total.FixedObjectName_n, Y).Text
            ElseIf Y < 0 Then
                tx = .FixedDataItem(X, Y + Grid_Total.FixedDataItem_n).Text
            Else
                tx = .Grid_Text(X, Y).Text
            End If
            If tx Is Nothing = True Then
                tx = ""
            End If
        End With
        Return tx
    End Function

    Sub Grid_Clear(ByVal Caption As String)

        If Grid_Property(Grid_Total.Layer).SelectedF = False Then
            Exit Sub
        End If

        With Grid_Property(Grid_Total.Layer)
            Dim rect As Rectangle = .MouseUpDownRect
            Dim r1 As Integer = Math.Max(-Grid_Total.FixedObjectName_n + Grid_Total.FixedObjectName_n2, rect.Left)
            Dim r2 = rect.Right
            Dim c1 As Integer = Math.Max(-Grid_Total.FixedDataItem_n + Grid_Total.FixedDataItem_n2, rect.Top)
            Dim c2 As Integer = rect.Bottom
            SetUndo_CopyPasteCutClear(Caption)
            For i As Integer = r1 To r2
                For j As Integer = c1 To c2
                    Call Set_Data_To_Grid(Grid_Total.Layer, i, j, "", True)
                Next
            Next
            Call Print_Grid_Data()
            Call Check_ChangeEventRange(r1, c1, r2 - r1 + 1, c2 - c1 + 1)
        End With
    End Sub


    Private Sub SetUndo_ChangeRowHeight(ByVal Layer As Integer, ByVal top As Integer, ByVal bottom As Integer)
        Dim UndoData As Undo_ChangeRowHeight
        Dim oldH(bottom - top) As Integer
        With Grid_Property(Layer)
            For i As Integer = top To bottom
                If i < 0 Then
                    oldH(i - top) = .FixedDataItemData(i + Grid_Total.FixedDataItem_n).Height
                Else
                    oldH(i - top) = .CellHeight(i)
                End If
            Next
        End With
        With UndoData
            .caption = "高さ変更"
            .Layer = Layer
            .Height = oldH.Clone
            .Top = top
            .Bottom = bottom
        End With
        UndoArray.Add(UndoData)
        SetUndoMenu()
    End Sub



    Private Sub SetUndo_CopyPasteCutClear(ByVal Caption As String)
        '切り取り、貼り付け、入力、クリア
        Dim rect As Rectangle = Grid_Property(Grid_Total.Layer).MouseUpDownRect
        Dim sb As New System.Text.StringBuilder()
        With rect
            For i As Integer = .Top To .Bottom
                For j As Integer = .Left To .Right
                    sb.Append(Get_Data_from_Grid(Grid_Total.Layer, j, i))
                    sb.Append(vbTab)
                Next
            Next
        End With
        Dim UndoInput As Undo_InputCopyPasteClearInfo
        With UndoInput
            .Layer = Grid_Total.Layer
            .GridData = sb.ToString
            .Rect = rect
            .caption = Caption
        End With
        UndoArray.Add(UndoInput)
        SetUndoMenu()
    End Sub
    Private Sub SetUndo_Input(ByVal X As Integer, ByVal Y As Integer, ByVal Caption As String)

        Dim UndoInput As Undo_InputCopyPasteClearInfo
        With UndoInput
            .Layer = Grid_Total.Layer
            .GridData = Get_Data_from_Grid(Grid_Total.Layer, X, Y)
            .Rect = Rectangle.FromLTRB(X, Y, X, Y)
            .caption = Caption
        End With
        UndoArray.Add(UndoInput)
        SetUndoMenu()
    End Sub
    Private Sub SetUndo_InsertRows(ByVal Caption As String, ByVal y As Integer, ByVal InsertNum As Integer)
        '行挿入
        Dim UndoInsertRows As Undo_InsertRows
        With UndoInsertRows
            .caption = Caption
            .Top = y
            .Bottom = y + InsertNum - 1
            .Layer = Grid_Total.Layer
        End With
        UndoArray.Add(UndoInsertRows)
        SetUndoMenu()
    End Sub
    Private Sub SetUndo_InsertColumns(ByVal Caption As String, ByVal x As Integer, ByVal InsertNum As Integer)
        '列挿入
        Dim UndoInsertRows As Undo_InsertColumns
        With UndoInsertRows
            .caption = Caption
            .Left = x
            .Right = x + InsertNum - 1
            .Layer = Grid_Total.Layer
        End With
        UndoArray.Add(UndoInsertRows)
        SetUndoMenu()
    End Sub
    ''' <summary>
    ''' 行削除のundo
    ''' </summary>
    ''' <param name="Caption">元に戻すメニューで表示されるキャプション</param>
    ''' <param name="Y"></param>
    ''' <param name="DeleteNum"></param>
    ''' <remarks></remarks>
    Private Sub SetUndo_DeleteRows(ByVal Caption As String, ByVal Y As Integer, ByVal DeleteNum As Integer)
        Dim sb As New System.Text.StringBuilder()


        For i As Integer = Y To Y + DeleteNum - 1
            For j As Integer = -Grid_Total.FixedObjectName_n To Grid_Property(Grid_Total.Layer).Xmax - 1
                sb.Append(Get_Data_from_Grid(Grid_Total.Layer, j, i) + vbTab)
            Next
        Next

        Dim UndoDeleteRows As Undo_DeleteRows
        With UndoDeleteRows
            .Top = Y
            .Bottom = Y + DeleteNum - 1
            .caption = Caption
            .Layer = Grid_Total.Layer
            .GridData = sb.ToString
        End With
        UndoArray.Add(UndoDeleteRows)
        SetUndoMenu()
    End Sub
    ''' <summary>
    ''' 列削除のundo
    ''' </summary>
    ''' <param name="Caption">元に戻すメニューで表示されるキャプション</param>
    ''' <param name="X"></param>
    ''' <param name="DeleteNum"></param>
    ''' <remarks></remarks>
    Private Sub SetUndo_DeleteColumns(ByVal Caption As String, ByVal X As Integer, ByVal DeleteNum As Integer)

        Dim sb As New System.Text.StringBuilder()

        For i As Integer = -Grid_Total.FixedDataItem_n To Grid_Property(Grid_Total.Layer).YMax - 1
            For j As Integer = X To X + DeleteNum - 1
                sb.Append(Get_Data_from_Grid(Grid_Total.Layer, j, i) + vbTab)
            Next
        Next

        Dim UndoDeleteColumns As Undo_DeleteColumns
        With UndoDeleteColumns
            .Left = X
            .Right = X + DeleteNum - 1
            .caption = Caption
            .Layer = Grid_Total.Layer
            .GridData = sb.ToString
        End With
        UndoArray.Add(UndoDeleteColumns)
        SetUndoMenu()
    End Sub
    Private Sub SetUndoMenu()
        Dim n As Integer = UndoArray.Count
        If n = 0 Then
            mnuUndo.Enabled = False
            mnuUndo.Text = "元に戻す(&Z)"
        Else
            mnuUndo.Text = "元に戻す(&Z)（" & UndoArray.Item(n - 1).caption & "）"
            mnuUndo.Enabled = True
        End If

    End Sub

    Private Sub Grid_Paste(ByVal clipText As String, ByVal UndoMode_Flag As Boolean)
        Dim Xn As Integer, Yn As Integer
        Dim ygmax As Integer, xgmax As Integer
        Dim xg As Integer, xg2 As Integer
        Dim yg As Integer, yg2 As Integer
        Dim ygs As Integer, xgs As Integer
        Dim f As Boolean

        With Grid_Property(Grid_Total.Layer)

            'クリップボードのデータ分解
            If Microsoft.VisualBasic.Right(clipText, 2) = vbCrLf Then
                clipText = clipText.Substring(0, clipText.Length - 2)
            End If
            Dim pastData(,) As String
            clipText = clipText.Replace(vbCrLf, vbCr)
            Dim spCRLF() As String = clipText.Split(vbCr)
            Dim spData As New ArrayList
            Dim mxxs As Integer = 0
            For i As Integer = 0 To spCRLF.GetUpperBound(0)
                Dim sp2() As String = spCRLF(i).Split(vbTab)
                mxxs = Math.Max(mxxs, sp2.GetLength(0))
                spData.Add(sp2)
            Next
            Xn = mxxs
            Yn = spCRLF.GetUpperBound(0) + 1
            ReDim pastData(Xn - 1, Yn - 1)
            Dim n As Integer
            For Each Str() As String In spData
                For j As Integer = 0 To Str.GetUpperBound(0)
                    If Str(j).IndexOf(vbCrLf) = -1 And Str(j).IndexOf(vbLf) <> -1 Then
                        Str(j) = Str(j).Replace(vbLf, vbCrLf)
                    End If
                    If Len(Str(j)) >= 2 And Microsoft.VisualBasic.Left(Str(j), 1) = ControlChars.Quote And Microsoft.VisualBasic.Right(Str(j), 1) = ControlChars.Quote Then
                        pastData(j, n) = Mid(Str(j), 2, Len(Str(j)) - 2)
                    Else
                        pastData(j, n) = Str(j)
                    End If
                Next
                n += 1
            Next



            ygmax = .YMax
            xgmax = .Xmax
            Dim rect As Rectangle = .MouseUpDownRect
            With Grid_Total
                xg = Math.Max(-.FixedObjectName_n + .FixedObjectName_n2, rect.Left)
                yg = Math.Max(-.FixedDataItem_n + .FixedDataItem_n2, rect.Top)
                xg2 = Math.Max(-.FixedObjectName_n + .FixedObjectName_n2, rect.Right)
                yg2 = Math.Max(-.FixedDataItem_n + .FixedDataItem_n2, rect.Bottom)
            End With

            ygs = yg2 - yg + 1
            xgs = xg2 - xg + 1



            If yg + Yn - 1 >= ygmax Then
                Yn = ygmax - yg
            End If
            If xg + Xn - 1 >= xgmax Then
                Xn = xgmax - xg
            End If

            f = True
            If Yn = 1 And Xn = 1 Then
                If UndoMode_Flag = False Then
                    SetUndo_CopyPasteCutClear("貼り付け")

                End If
                For i As Integer = yg To yg2
                    For j As Integer = xg To xg2
                        Call Set_Data_To_Grid(Grid_Total.Layer, j, i, pastData(0, 0), True)
                    Next
                Next
                Xn = xgs
                Yn = ygs
            ElseIf (ygs = 1 And xgs = 1) Or (Xn = xgs And Yn = ygs) Then
                If UndoMode_Flag = False Then
                    .MouseDownX = xg
                    .MouseDownY = yg
                    .MouseUpX = xg + Xn - 1
                    .MouseUpY = yg + Yn - 1
                    SetUndo_CopyPasteCutClear("貼り付け")
                End If
                For i = 0 To Yn - 1
                    For j = 0 To Xn - 1
                        Call Set_Data_To_Grid(Grid_Total.Layer, j + xg, i + yg, pastData(j, i), True)
                    Next
                Next
            ElseIf xgs = 1 And ygs > 1 And Yn = 1 And Xn > 1 Then
                If UndoMode_Flag = False Then
                    .MouseDownX = xg
                    .MouseDownY = yg
                    .MouseUpX = xg + Xn - 1
                    .MouseUpY = yg + ygs - 1
                    SetUndo_CopyPasteCutClear("貼り付け")
                End If
                For i = 0 To ygs - 1
                    For j = 0 To Xn - 1
                        Call Set_Data_To_Grid(Grid_Total.Layer, j + xg, i + yg, pastData(j, 0), True)
                    Next
                Next
                Yn = ygs
            ElseIf ygs = 1 And xgs > 1 And Xn = 1 And Yn > 1 Then
                If UndoMode_Flag = False Then
                    .MouseDownX = xg
                    .MouseDownY = yg
                    .MouseUpX = xg + xgs - 1
                    .MouseUpY = yg + Yn - 1
                    SetUndo_CopyPasteCutClear("貼り付け")
                End If
                For i As Integer = 0 To xgs - 1
                    For j As Integer = 0 To Yn - 1
                        Call Set_Data_To_Grid(Grid_Total.Layer, i + xg, j + yg, pastData(0, j), True)
                    Next
                Next
                Xn = xgs
            ElseIf xgs = 1 And ygs = Yn Then
                If UndoMode_Flag = False Then
                    .MouseDownX = xg
                    .MouseDownY = yg
                    .MouseUpX = xg + Xn - 1
                    .MouseUpY = yg + ygs - 1
                    SetUndo_CopyPasteCutClear("貼り付け")
                End If
                For i As Integer = 0 To Yn - 1
                    For j As Integer = 0 To Xn - 1
                        Call Set_Data_To_Grid(Grid_Total.Layer, j + xg, i + yg, pastData(j, i), True)
                    Next
                Next
            ElseIf ygs = 1 And xgs = Xn Then
                If UndoMode_Flag = False Then
                    .MouseDownX = xg
                    .MouseDownY = yg
                    .MouseUpX = xg + xgs - 1
                    .MouseUpY = yg + Yn - 1
                    SetUndo_CopyPasteCutClear("貼り付け")
                End If
                For i As Integer = 0 To Yn - 1
                    For j As Integer = 0 To Xn - 1
                        Call Set_Data_To_Grid(Grid_Total.Layer, j + xg, i + yg, pastData(j, i), True)
                    Next
                Next
            Else
                MsgBox("この形状には対応していません。", MsgBoxStyle.Exclamation, Grid_Total.MsgBoxTitle)
                f = False
            End If
        End With

        If f = True Then
            Call Print_Grid_Data()
            Call Check_ChangeEventRange(xg, yg, Xn, Yn)
        End If
    End Sub
    Private Sub Set_Data_To_Grid(ByVal Grid_Lay As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal tx As String, ByVal Check_F As Boolean)
        '指定された位置のグリッド配列にﾃﾞｰﾀをｾｯﾄする
        'Check_F:変更できるかどうかチェックする

        If Check_F = True Then
            With Grid_Property(Grid_Total.Layer).Ope
                If (Y < 0 And X >= 0 And .FixedYSEnabled = False) Or _
                    (X < 0 And Y >= 0 And .FixedXSEnabled = False) Or _
                    (X < 0 And Y < 0 And .FixedUpperLeftEnabeld = False) Then
                    Exit Sub
                End If
            End With
        End If

        With Grid_Property(Grid_Lay)
            If X < 0 And Y < 0 Then
                .FixedUpperLeft(X + Grid_Total.FixedObjectName_n, Y + Grid_Total.FixedDataItem_n).Text = tx
            ElseIf X < 0 Then
                .FixedObjectName(X + Grid_Total.FixedObjectName_n, Y).Text = tx
            ElseIf Y < 0 Then
                .FixedDataItem(X, Y + Grid_Total.FixedDataItem_n).Text = tx
            Else
                .Grid_Text(X, Y).Text = tx
            End If
        End With

    End Sub
    Private Function Get_XYData(ByVal Layer As Integer, ByVal X As Integer, ByVal Y As Integer) As String
        With Grid_Property(Layer)
            If X < 0 And Y < 0 Then
                Get_XYData = .FixedUpperLeft(X + Grid_Total.FixedObjectName_n, Y + Grid_Total.FixedDataItem_n).Text
            ElseIf X < 0 Then
                Get_XYData = .FixedObjectName(X + Grid_Total.FixedObjectName_n, Y).Text
            ElseIf Y < 0 Then
                Get_XYData = .FixedDataItem(X, Y + Grid_Total.FixedDataItem_n).Text
            Else
                Get_XYData = .Grid_Text(X, Y).Text
            End If
        End With
    End Function
    Private Sub Grid_Copy()

        Cursor.Current = Cursors.WaitCursor
        Clipboard.Clear()

        With Grid_Property(Grid_Total.Layer)
            Dim sb As New System.Text.StringBuilder()
            Dim rect As Rectangle = .MouseUpDownRect
            Dim r1 As Integer = Math.Max(-Grid_Total.FixedObjectName_n + Grid_Total.FixedObjectName_n2, rect.Left)
            Dim r2 As Integer = rect.Right
            Dim c1 As Integer = Math.Max(-Grid_Total.FixedDataItem_n + Grid_Total.FixedDataItem_n2, rect.Top)
            Dim c2 As Integer = rect.Bottom
            For i As Integer = c1 To c2
                For j As Integer = r1 To r2
                    Dim PlusCell As String = Get_Data_from_Grid(Grid_Total.Layer, j, i)
                    If PlusCell.IndexOf(vbCrLf) <> -1 Then
                        PlusCell = PlusCell.Replace(vbCrLf, vbLf)
                        '改行が含まれるセルは""で囲む
                        PlusCell = ControlChars.Quote + PlusCell + ControlChars.Quote
                    End If
                    sb.Append(PlusCell)
                    If j <> r2 Then
                        sb.Append(vbTab)
                    End If
                Next
                If c1 <> c2 Then
                    sb.Append(vbCrLf)
                End If
            Next
            If sb.Length <> 0 Then
                Clipboard.SetText(sb.ToString)
            End If
        End With
        Cursor.Current = Cursors.Default

    End Sub


    Private Sub mnuTabRightMenuInsertTabLeft_Click(sender As Object, e As EventArgs) Handles mnuTabRightMenuInsertTabLeft.Click, mnuTabRightMenuInsertTabRight.Click

        Dim nt As Integer
        If sender.name = "mnuTabRightMenuInsertTabLeft" Then
            nt = SSTab.SelectedIndex
        Else
            nt = SSTab.SelectedIndex + 1
        End If
        Dim UndoData As Undo_InsertLayer
        With UndoData
            .Layer = nt
            .caption = Grid_Total.LayerCaption + "の挿入"
        End With
        UndoArray.Add(UndoData)
        SetUndoMenu()

        Dim existLayer(Grid_Total.LayerNum - 1) As String
        For i As Integer = 0 To Grid_Total.LayerNum - 1
            existLayer(i) = Grid_Property(i).LayerName
        Next
        Dim newName As String = Get_New_Numbering_Strings("新しい" + Grid_Total.LayerCaption, existLayer)
        Insert_Layer(newName, nt, -1, 5, 50, Grid_Total.tOpe)
        RaiseEvent Add_Layer(nt)
        SSTab.SelectedIndex = nt

    End Sub


    Private Sub mnuTabRightMenuMoveTabLeft_Click(sender As Object, e As EventArgs) Handles mnuTabRightMenuMoveTabLeft.Click

        Dim nt = SSTab.SelectedIndex
        If nt <> 0 Then
            Dim UndoData As Undo_SwapLayer
            UndoData.Layer1 = nt
            UndoData.Layer2 = nt - 1
            UndoData.caption = "レイヤ移動"
            UndoArray.Add(UndoData)
            SetUndoMenu()
            Call Swap_GridLay(nt, nt - 1)
            RaiseEvent Change_Layer(False, True, False)
            Call Set_SSTAB_Name()
            SSTab.Tag = "OFF"
            Grid_Total.Layer = nt - 1
            SSTab.SelectedIndex = nt - 1
            SSTab.Tag = ""
        End If
    End Sub

    Private Sub mnuTabRightMenuMoveTabRight_Click(sender As Object, e As EventArgs) Handles mnuTabRightMenuMoveTabRight.Click
        Dim nt = SSTab.SelectedIndex
        Dim mxt = SSTab.TabCount
        If nt <> mxt - 1 Then
            Dim UndoData As Undo_SwapLayer
            UndoData.Layer1 = nt
            UndoData.Layer2 = nt + 1
            UndoData.caption = "レイヤ移動"
            UndoArray.Add(UndoData)
            SetUndoMenu()
            Call Swap_GridLay(nt, nt + 1)
            RaiseEvent Change_Layer(False, True, False)
            Call Set_SSTAB_Name()
            SSTab.Tag = "OFF"
            Grid_Total.Layer = nt + 1
            SSTab.SelectedIndex = nt + 1
            SSTab.Tag = ""
        End If
    End Sub

    Private Sub mnuTabRightMenuMoveTabAhead_Click(sender As Object, e As EventArgs) Handles mnuTabRightMenuMoveTabAhead.Click
        Dim nt = SSTab.SelectedIndex
        If nt <> 0 Then
            Dim UndoData As Undo_MoveLayer
            UndoData.OriginLay = nt
            UndoData.DestLay = 0
            UndoData.caption = "レイヤ移動"
            UndoArray.Add(UndoData)
            SetUndoMenu()
            Dim TempGridLay As Grid_Info = Grid_Property(nt).Clone
            For i As Integer = nt To 1 Step -1
                Grid_Property(i) = Grid_Property(i - 1).Clone
            Next
            Grid_Property(0) = TempGridLay
            RaiseEvent Change_Layer(False, True, False)
            Call Set_SSTAB_Name()
            SSTab.Tag = "OFF"
            Grid_Total.Layer = 0
            SSTab.SelectedIndex = 0
            SSTab.Tag = ""
        End If
    End Sub

    Private Sub mnuTabRightMenuMoveTabRear_Click(sender As Object, e As EventArgs) Handles mnuTabRightMenuMoveTabRear.Click
        Dim nt = SSTab.SelectedIndex
        Dim mxt = SSTab.TabCount
        If nt <> mxt - 1 Then
            Dim UndoData As Undo_MoveLayer
            UndoData.OriginLay = nt
            UndoData.DestLay = mxt - 1
            UndoData.caption = "レイヤ移動"
            UndoArray.Add(UndoData)
            SetUndoMenu()
            Dim TempGridLay As Grid_Info = Grid_Property(nt).Clone
            For i As Integer = nt To mxt - 2
                Grid_Property(i) = Grid_Property(i + 1).Clone
            Next
            Grid_Property(mxt - 1) = TempGridLay
            RaiseEvent Change_Layer(False, True, False)
            Call Set_SSTAB_Name()
            SSTab.Tag = "OFF"
            Grid_Total.Layer = mxt - 1
            SSTab.SelectedIndex = mxt - 1
            SSTab.Tag = ""
        End If
    End Sub
    Private Sub Swap_GridLay(ByVal L1 As Integer, L2 As Integer)
        Dim TempGridLay As Grid_Info

        TempGridLay = Grid_Property(L1).Clone
        Grid_Property(L1) = Grid_Property(L2).Clone
        Grid_Property(L2) = TempGridLay
    End Sub
    Private Sub UndoArray_Clear()
        UndoArray.Clear()
        SetUndoMenu()
    End Sub
    Private Sub Delete_Layer(ByVal DLay As Integer)
        'レイヤを削除

        With Grid_Total
            .LayerNum -= 1
            For i As Integer = DLay To .LayerNum - 1
                Grid_Property(i) = Grid_Property(i + 1).Clone
            Next
            ReDim Preserve Grid_Property(.LayerNum - 1)
        End With

    End Sub

    Private Sub txtTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTextBox.KeyPress
        'EnterやEscapeキーでビープ音が鳴らないようにする
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Or _
            e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Escape) Or
            e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Tab) Then
            e.Handled = True
        End If
    End Sub


    Private Sub txtTextBox_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtTextBox.PreviewKeyDown
        If e.KeyValue = Keys.Tab Then
            e.IsInputKey = True
        End If
    End Sub

    Private Sub txtTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTextBox.KeyDown

        Dim Tx As String
        With Grid_Property(Grid_Total.Layer)
            GX = .MouseDownX
            GY = .MouseDownY
        End With
        Dim ALT_Key As Boolean = My.Computer.Keyboard.AltKeyDown
        Dim Shift_Key As Boolean = My.Computer.Keyboard.ShiftKeyDown
        txtTextBox.Tag = ""
        If e.KeyValue = Keys.Enter And ALT_Key = True Then 'Alt+Enter
            Tx = txtTextBox.Text
            Dim a As Integer = txtTextBox.SelectionStart
            If Len(Tx) = a Then
                txtTextBox.Text = Tx & vbCrLf
            ElseIf a = 0 Then
                txtTextBox.Text = vbCrLf & Tx
            Else
                txtTextBox.Text = Mid(Tx, 1, a) & vbCrLf & Mid(Tx, a + 1)
            End If
            txtTextBox.SelectionStart = a
            Exit Sub
        End If

        With Grid_Property(Grid_Total.Layer)
            Select Case e.KeyValue
                Case Keys.Left, Keys.Up, Keys.Right, Keys.Down, Keys.Enter, Keys.Tab
                    If Me.Ysize(Grid_Total.Layer) = 1 And GX = 0 And GY = 0 And (e.KeyValue = Keys.Enter Or e.KeyValue = Keys.Tab) Then
                        Call Set_Data_from_txtBox_To_Grid()
                        txtTextBox.Visible = False
                        Call Print_Grid_Data()
                        Return
                    End If
                    With txtTextBox
                        If .SelectionStart <> Len(.Text) And (e.KeyValue = Keys.Left Or e.KeyValue = Keys.Right) Then
                            Return
                        End If
                    End With
                    If e.KeyValue = Keys.Left Then
                        Return
                    End If

                    Call Set_Data_from_txtBox_To_Grid()
                    If e.KeyValue = Keys.Tab And Shift_Key = True Then
                        Call Key_Move(e.KeyValue, True)
                    Else
                        Call Key_Move(e.KeyValue, False)
                    End If
                    If e.KeyValue = Keys.Up And GY = -1 And ((Grid_Property(Grid_Total.Layer).Ope.FixedYSEnabled = False And GX >= 0) _
                                    Or (Grid_Property(Grid_Total.Layer).Ope.FixedUpperLeftEnabeld = False And GX < 0)) Then
                        .SelectedF = True
                        Call SetTextBox(GX, GY)
                        txtTextBox.Visible = False
                    Else
                        Call SetTextBox(GX, GY)
                    End If
                Case Keys.Escape
                    .SelectedF = True
                    txtTextBox.Visible = False
                    Call Print_Grid_Data()
            End Select
        End With
    End Sub

    Private Sub mnuUndo_Click(sender As Object, e As EventArgs) Handles mnuUndo.Click
        Call Undo()
    End Sub

    Private Sub SetGrid_UndoData(ByVal Layer As Integer, ByVal Rect As Rectangle, ByRef GridData As String)
        Dim SN As Integer = 0
        Dim cst() As String = GridData.Split(vbTab)
        For i As Integer = Rect.Top To Rect.Bottom
            For j As Integer = Rect.Left To Rect.Right
                Call Set_Data_To_Grid(Layer, j, i, cst(SN), True)
                SN += 1
            Next
        Next
        With Rect
            Call Check_ChangeEventRange(.Left, .Top, .Width + 1, .Height + 1)
        End With

    End Sub
    Private Sub Undo()

        Dim n As Integer = UndoArray.Count - 1
        If n = -1 Then
            Return
        End If
        Select Case True
            Case TypeOf UndoArray.Item(n) Is Undo_InputCopyPasteClearInfo
                Dim UndoData As Undo_InputCopyPasteClearInfo = CType(UndoArray.Item(n), Undo_InputCopyPasteClearInfo)
                With UndoData
                    SetGrid_UndoData(.Layer, .Rect, .GridData)
                End With
            Case TypeOf UndoArray.Item(n) Is Undo_InsertRows
                Dim UndoData As Undo_InsertRows = CType(UndoArray.Item(n), Undo_InsertRows)
                With UndoData
                    Call DeleteRows(.Layer, .Top, .Bottom - .Top + 1)
                    RaiseEvent Change_FixedXS()
                End With
            Case TypeOf UndoArray.Item(n) Is Undo_InsertColumns
                Dim UndoData As Undo_InsertColumns = CType(UndoArray.Item(n), Undo_InsertColumns)
                With UndoData
                    Call DeleteColumns(.Layer, .Left, .Right - .Left + 1)
                    RaiseEvent Change_FixedYS()
                End With
            Case TypeOf UndoArray.Item(n) Is Undo_DeleteRows
                Dim UndoData As Undo_DeleteRows = CType(UndoArray.Item(n), Undo_DeleteRows)
                With UndoData
                    Dim Dn As Integer = .Bottom - .Top + 1
                    Call InsertRows(.Layer, .Top, Dn)
                    RaiseEvent Change_FixedXS()
                    SetGrid_UndoData(.Layer, Rectangle.FromLTRB(-Grid_Total.FixedObjectName_n, .Top, Grid_Property(.Layer).Xmax - 1, .Bottom), .GridData)
                End With
            Case TypeOf UndoArray.Item(n) Is Undo_DeleteColumns
                Dim UndoData As Undo_DeleteColumns = CType(UndoArray.Item(n), Undo_DeleteColumns)
                With UndoData
                    Dim Dn As Integer = .Right - .Left + 1
                    Call InsertColumns(.Layer, .Left, Dn)
                    RaiseEvent Change_FixedYS()
                    SetGrid_UndoData(.Layer, Rectangle.FromLTRB(.Left, -Grid_Total.FixedDataItem_n, .Right, Grid_Property(.Layer).YMax - 1), .GridData)
                End With
            Case TypeOf UndoArray.Item(n) Is Undo_ChangeRowHeight
                Dim UndoData As Undo_ChangeRowHeight = CType(UndoArray.Item(n), Undo_ChangeRowHeight)
                With UndoData
                    ChangeRowHeight(.Layer, .Top, .Bottom, .Height)
                End With
                Call Print_Grid_ViewSize()
            Case TypeOf UndoArray.Item(n) Is Undo_ChangeColumnWidth
                Dim UndoData As Undo_ChangeColumnWidth = CType(UndoArray.Item(n), Undo_ChangeColumnWidth)
                With UndoData
                    ChangeColumnWidth(.Layer, .Left, .Right, .Width)
                End With
                Call Print_Grid_ViewSize()
            Case TypeOf UndoArray.Item(n) Is Undo_ChangeLayerName
                Dim UndoData As Undo_ChangeLayerName = CType(UndoArray.Item(n), Undo_ChangeLayerName)
                With UndoData
                    Dim tab As TabPage = SSTab.TabPages(.Layer)
                    tab.Text = .Name
                    Grid_Property(.Layer).LayerName = .Name
                End With
            Case TypeOf UndoArray.Item(n) Is Undo_SwapLayer
                Dim UndoData As Undo_SwapLayer = CType(UndoArray.Item(n), Undo_SwapLayer)
                With UndoData
                    Call Swap_GridLay(.Layer1, .Layer2)
                    Call Set_SSTAB_Name()
                    SSTab.SelectedIndex = .Layer1
                End With
            Case TypeOf UndoArray.Item(n) Is Undo_MoveLayer
                Dim UndoData As Undo_MoveLayer = CType(UndoArray.Item(n), Undo_MoveLayer)
                With UndoData
                    Dim TempGridLay As Grid_Info = Grid_Property(.DestLay).Clone
                    If .OriginLay < .DestLay Then
                        For i As Integer = .DestLay To .OriginLay + 1 Step -1
                            Grid_Property(i) = Grid_Property(i - 1).Clone
                        Next
                    Else
                        For i As Integer = .DestLay To .OriginLay - 1
                            Grid_Property(i) = Grid_Property(i + 1).Clone
                        Next
                    End If
                    Grid_Property(.OriginLay) = TempGridLay
                    Call Set_SSTAB_Name()
                    SSTab.SelectedIndex = .OriginLay
                End With
            Case TypeOf UndoArray.Item(n) Is Undo_deleteLayer
                Dim UndoData As Undo_deleteLayer = CType(UndoArray.Item(n), Undo_deleteLayer)
                With UndoData
                    Grid_Total.LayerNum += 1
                    Dim mxt As Integer = SSTab.TabCount + 1
                    ReDim Preserve Grid_Property(mxt - 1)
                    For i As Integer = mxt - 1 To .OriginLay + 1 Step -1
                        Grid_Property(i) = Grid_Property(i - 1).Clone
                    Next
                    Grid_Property(.OriginLay) = .GridData.Clone
                    Call Set_SSTAB_Name()
                    SSTab.SelectedIndex = .OriginLay
                End With
            Case TypeOf UndoArray.Item(n) Is Undo_InsertLayer
                Dim UndoData As Undo_InsertLayer = CType(UndoArray.Item(n), Undo_InsertLayer)
                With UndoData
                    Delete_Layer(.Layer)
                    Call Set_SSTAB_Name()
                    Dim nnt As Integer
                    If .Layer = Grid_Total.LayerNum Then
                        nnt = .Layer - 1
                    Else
                        nnt = .Layer
                    End If
                    SSTab.SelectedIndex = nnt
                End With
        End Select
        UndoArray.RemoveAt(n)
        SetUndoMenu()
        Call Print_Grid_Data()
    End Sub

    ''' <summary>
    ''' 行削除
    ''' </summary>
    ''' <param name="GridLay">レイヤ</param>
    ''' <param name="DeletePoint">行位置</param>
    ''' <param name="DeleteNum">削除行数</param>
    ''' <remarks></remarks>
    Private Sub DeleteRows(ByVal GridLay As Integer, ByVal DeletePoint As Integer, ByVal DeleteNum As Integer)

        Dim ys As Integer
        Dim oldYs As Integer

        'データ部分を削除
        With Grid_Property(GridLay)
            oldYs = .YMax
            ys = .YMax - DeleteNum
            .YMax = ys
            Dim xs As Integer = .Xmax

            For j As Integer = DeletePoint + DeleteNum To oldYs - 1
                .CellHeight(j - DeleteNum) = .CellHeight(j)
            Next
            ReDim Preserve .CellHeight(ys - 1)
            Dim GTempText(,) As GridTextColor_Info = .Grid_Text.Clone
            ReDim .Grid_Text(xs - 1, ys - 1)
            For i As Integer = 0 To xs - 1
                For j As Integer = 0 To DeletePoint - 1
                    .Grid_Text(i, j) = GTempText(i, j)
                Next
                For j As Integer = DeletePoint + DeleteNum To oldYs - 1
                    .Grid_Text(i, j - DeleteNum) = GTempText(i, j)
                Next
            Next

        End With
        With Grid_Property(GridLay)
            'オブジェクト名部分を削除
            Dim GTempText(,) As GridTextColor_Info = .FixedObjectName.Clone
            Dim xs As Integer = Grid_Total.FixedObjectName_n
            ReDim .FixedObjectName(xs - 1, ys - 1)
            For i As Integer = -xs To -1
                For j As Integer = 0 To DeletePoint - 1
                    .FixedObjectName(i + xs, j) = GTempText(i + xs, j)
                Next
                For j As Integer = DeletePoint + DeleteNum To oldYs - 1
                    .FixedObjectName(i + xs, j - DeleteNum) = GTempText(i + xs, j)
                Next
            Next

        End With

        Call Set_FixedCell_Words(GridLay)
        Call Print_Grid_ViewSize()
    End Sub
    Private Sub InsertRows(ByVal GridLay As Integer, ByVal InsertPoint As Integer, ByVal InsertNum As Integer)

        Dim ys As Integer
        Dim oldYs As Integer

        'データ部分を挿入
        With Grid_Property(GridLay)
            oldYs = .YMax
            ys = .YMax + InsertNum
            .YMax = ys
            Dim xs As Integer = .Xmax
            Dim GTempText(,) As GridTextColor_Info = .Grid_Text.Clone
            ReDim .Grid_Text(xs - 1, ys - 1)
            For i As Integer = 0 To xs - 1
                For j As Integer = 0 To InsertPoint - 1
                    .Grid_Text(i, j) = GTempText(i, j)
                Next
                For j As Integer = InsertPoint To oldYs - 1
                    .Grid_Text(i, j + InsertNum) = GTempText(i, j)
                Next
            Next

            ReDim Preserve .CellHeight(ys - 1)
            If InsertPoint = oldYs Then
                For i As Integer = InsertPoint To InsertPoint + InsertNum - 1
                    .CellHeight(i) = .CellHeight(InsertPoint - 1)
                Next
            Else
                For j As Integer = oldYs - 1 To InsertPoint Step -1
                    .CellHeight(j + InsertNum) = .CellHeight(j)
                Next
                For j As Integer = InsertPoint + 1 To InsertPoint + InsertNum - 1
                    .CellHeight(j) = .CellHeight(InsertPoint)
                Next
            End If
        End With


        'オブジェクト名部分を挿入
        With Grid_Property(GridLay)
            Dim xs As Integer = Grid_Total.FixedObjectName_n
            Dim GTempText(,) As GridTextColor_Info = .FixedObjectName.Clone
            ReDim .FixedObjectName(xs - 1, ys - 1)
            For i As Integer = -xs To -1
                For j As Integer = 0 To InsertPoint - 1
                    .FixedObjectName(i + xs, j) = GTempText(i + xs, j)
                Next
                For j As Integer = InsertPoint To oldYs - 1
                    .FixedObjectName(i + xs, j + InsertNum) = GTempText(i + xs, j)
                Next
            Next
        End With


        Call Set_FixedCell_Words(GridLay)
        Call Print_Grid_ViewSize()
    End Sub

    Private Sub DeleteColumns(ByVal GridLay As Integer, ByVal DeletePoint As Integer, ByVal DeleteNum As Integer)
 
        Dim xs As Integer
        Dim oldXs As Integer


        'データ部分を挿入
        With Grid_Property(GridLay)
            oldXs = .Xmax
            Dim ys As Integer = .YMax
            xs = .Xmax - DeleteNum
            .Xmax = xs

            Dim GTempText(,) As GridTextColor_Info = .Grid_Text.Clone
            ReDim .Grid_Text(xs - 1, ys - 1)
            For i As Integer = 0 To ys - 1
                For j As Integer = 0 To DeletePoint - 1
                    .Grid_Text(j, i) = GTempText(j, i)
                Next
                For j As Integer = DeletePoint + DeleteNum To oldXs - 1
                    .Grid_Text(j - DeleteNum, i) = GTempText(j, i)
                Next
            Next

            For j As Integer = DeletePoint + DeleteNum To oldXs - 1
                .DataItemData(j - DeleteNum) = .DataItemData(j)
            Next
            ReDim Preserve .DataItemData(xs - 1)
        End With


        'データ項目部分を削除
        With Grid_Property(GridLay)
            Dim ys As Integer = Grid_Total.FixedDataItem_n
            Dim GTempText(,) As GridTextColor_Info = .FixedDataItem.Clone
            ReDim .FixedDataItem(xs - 1, ys - 1)
            For i As Integer = -ys To -1
                For j As Integer = 0 To DeletePoint - 1
                    .FixedDataItem(j, i + ys) = GTempText(j, i + ys)
                Next
                For j As Integer = DeletePoint + DeleteNum To oldXs - 1
                    .FixedDataItem(j - DeleteNum, i + ys) = GTempText(j, i + ys)
                Next
            Next
        End With


        Call Set_FixedCell_Words(GridLay)
        Call Print_Grid_ViewSize()
    End Sub

    Private Sub InsertColumns(ByVal GridLay As Integer, ByVal InsertPoint As Integer, ByVal InsertNum As Integer)

        Dim xs As Integer
        Dim oldXs As Integer

        'データ部分を挿入
        With Grid_Property(GridLay)
            oldXs = .Xmax
            Dim ys As Integer = .YMax
            xs = .Xmax + InsertNum
            .Xmax = xs
            Dim GTempText(,) As GridTextColor_Info = .Grid_Text.Clone
            ReDim .Grid_Text(xs - 1, ys - 1)
            For i As Integer = 0 To ys - 1
                For j = 0 To InsertPoint - 1
                    .Grid_Text(j, i) = GTempText(j, i)
                Next
                For j As Integer = InsertPoint To oldXs - 1
                    .Grid_Text(j + InsertNum, i) = GTempText(j, i)
                Next
            Next
        End With

        With Grid_Property(GridLay)
            ReDim Preserve .DataItemData(xs - 1)
            If InsertPoint = oldXs Then
                For i As Integer = InsertPoint To InsertPoint + InsertNum - 1
                    .DataItemData(i) = .DataItemData(InsertPoint - 1)
                Next
            Else
                For j As Integer = oldXs - 1 To InsertPoint Step -1
                    .DataItemData(j + InsertNum) = .DataItemData(j)
                Next
                For j As Integer = InsertPoint + 1 To InsertPoint + InsertNum - 1
                    .DataItemData(j) = .DataItemData(InsertPoint)
                Next
            End If

        End With


        'データ項目部分を挿入
        With Grid_Property(GridLay)
            Dim ys As Integer = Grid_Total.FixedDataItem_n
            Dim GTempText(,) As GridTextColor_Info = .FixedDataItem.Clone
            ReDim .FixedDataItem(xs - 1, ys - 1)
            For i = -ys To -1
                For j = 0 To InsertPoint - 1
                    .FixedDataItem(j, i + ys) = GTempText(j, i + ys)
                Next
                For j = InsertPoint To oldXs - 1
                    .FixedDataItem(j + InsertNum, i + ys) = GTempText(j, i + ys)
                Next
            Next
        End With


        Call Set_FixedCell_Words(GridLay)
        Call Print_Grid_ViewSize()
    End Sub


    Private Sub Set_FixedCell_Words(ByVal L As Integer)
        '固定行・列に番号をふる

        With Grid_Property(L)
            If Grid_Total.FixedObjectName_n > 0 Then
                For i As Integer = 0 To .YMax - 1
                    Call Set_Data_To_Grid(L, -Grid_Total.FixedObjectName_n, i, CStr(i + 1), False)
                Next
            End If
            If Grid_Total.FixedDataItem_n2 > 0 Then
                For i As Integer = 0 To .Xmax - 1
                    Call Set_Data_To_Grid(L, i, -Grid_Total.FixedDataItem_n, CStr(i + 1), False)
                Next
            End If
        End With
    End Sub





    Private Sub mnuInsertColRight_Click(sender As Object, e As EventArgs) Handles mnuInsertColRight.Click, mnuInsertColLeft.Click

        With Grid_Property(Grid_Total.Layer)
            Dim rect As Rectangle = .MouseUpDownRect
            If rect.Left < 0 Then
                Return
            End If

            Dim ip As Integer
            Dim r As Integer = rect.Right - rect.Left + 1
            If sender.name = "mnuInsertColRight" Then
                ip = rect.Right + 1
            Else
                ip = rect.Left
                .MouseDownX = .MouseDownX + r
                .MouseUpX = .MouseUpX + r
            End If
            SetUndo_InsertColumns("列挿入", ip, r)
            Call InsertColumns(Grid_Total.Layer, ip, r)
            RaiseEvent Change_FixedYS()
            RaiseEvent Change_Data()
        End With
        Call Print_Grid_Data()

    End Sub


    Private Sub TimerMouse_Tick(sender As Object, e As EventArgs) Handles TimerMouse.Tick


        With Grid_Property(Grid_Total.Layer)
            Dim V As Integer = .TopCell
            Dim H As Integer = .LeftCell
            If .MouseDownX <> -Grid_Total.FixedObjectName_n Then
                If TimerVX < .FixedObjectNameDataWidth Then
                    If H = 0 Then
                        .MouseUpX = GetGridX(TimerVX, False)
                    Else
                        .LeftCell = H - 1
                        HScrollGrid_ValueSet()
                        .MouseUpX = H - 1
                    End If
                ElseIf TimerVX > picGrid.Width Then
                    If H < .Xmax - 1 Then
                        .LeftCell = H + 1
                        HScrollGrid_ValueSet()
                        .MouseUpX = .RightCell
                    End If
                Else
                    .MouseUpX = GetGridX(TimerVX, False)
                End If
            End If

            If .MouseDownY >= -Grid_Total.FixedDataItem_n + Grid_Total.FixedDataItem_n2 Then
                Dim sa As Integer
                If TimerVY < .FixedDataItemHeight Then
                    If V = 0 Then
                        .MouseUpY = GetGridY(TimerVY, False)
                    Else
                        sa = (.FixedDataItemHeight - TimerVY) \ 10 + 1
                        .TopCell = V - sa
                        If .TopCell < 0 Then
                            .TopCell = 0
                        End If
                        Call VScrollGrid_ValueSet()
                        .MouseUpY = V - sa
                    End If
                ElseIf TimerVY > picGrid.Height Then
                    If V < .YMax - 1 Then
                        sa = ((TimerVY - picGrid.Height) \ 5) ^ 2 + 1
                        .TopCell = V + sa
                        If .TopCell > .YMax - 1 Then
                            .TopCell = .YMax - 1
                        End If
                        Call VScrollGrid_ValueSet()
                        .MouseUpY = .BottomCell
                    End If
                Else
                    .MouseUpY = GetGridY(TimerVY, False)
                End If
            End If
            Select Case .MouseDown_Mode
                Case 1
                    .MouseUpY = .YMax - 1
                Case 2
                    .MouseUpX = .Xmax - 1
            End Select

        End With
        Call Print_Grid_Data()
    End Sub

    Private Sub picGrid_MouseWheel(sender As Object, e As MouseEventArgs) Handles picGrid.MouseWheel
        Dim v As Integer = VscrollGrid.Value - Math.Sign(e.Delta)
        v = Math.Max(0, v)
        v = Math.Min(VscrollGrid.Maximum, v)
        VscrollGrid.Value = v
    End Sub

    Private Sub mnuTabRightMenuChangeTabName_Click(sender As Object, e As EventArgs) Handles mnuTabRightMenuChangeTabName.Click


        Dim tab As TabPage = SSTab.SelectedTab

        Dim newLayerName As String = InputBox("新しい" + Grid_Total.LayerCaption + "名", Grid_Total.MsgBoxTitle, tab.Text)
        If newLayerName Is "" Then
        Else
            Dim UndoData As Undo_ChangeLayerName
            With UndoData
                .Layer = Grid_Total.Layer
                .caption = Grid_Total.LayerCaption + "名の変更"
                .Name = Grid_Property(Grid_Total.Layer).LayerName
            End With
            UndoArray.Add(UndoData)
            SetUndoMenu()
            tab.Text = newLayerName
            Grid_Property(Grid_Total.Layer).LayerName = newLayerName
            RaiseEvent Change_Layer(True, False, False)
        End If

    End Sub



    Private Sub mnuTabRightMenuDeleteTab_Click(sender As Object, e As EventArgs) Handles mnuTabRightMenuDeleteTab.Click
        Dim mxt As Integer = SSTab.TabCount
        If mxt = 1 Then Exit Sub
        Dim nt As Integer = SSTab.SelectedIndex
        Dim UndoData As Undo_deleteLayer
        With UndoData
            .OriginLay = nt
            .GridData = Grid_Property(Grid_Total.Layer).Clone
            .caption = Grid_Total.LayerCaption + "の削除"
        End With
        UndoArray.Add(UndoData)
        SetUndoMenu()
        Call Delete_Layer(nt)
        Call Set_SSTAB_Name()
        Dim nnt As Integer
        If nt = mxt - 1 Then
            nnt = nt - 1
        Else
            nnt = nt
        End If
        SSTab.SelectedIndex = nnt
        Grid_Total.Layer = nnt
        RaiseEvent Change_Layer(False, False, True)
        Call Print_Grid_ViewSize()
        Call Print_Grid_Data()
    End Sub

    ''' <summary>
    ''' 文字列配列をチェックして「新規1」「新規2」など連番を付ける
    ''' </summary>
    ''' <param name="CheckWords">調べる文字</param>
    ''' <param name="Words">既存の文字列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Get_New_Numbering_Strings(ByVal CheckWords As String, ByVal Words() As String) As String


        Dim L As Integer = Len(CheckWords)
        Dim V As Integer = 0
        For i As Integer = 0 To Words.GetUpperBound(0)
            If Microsoft.VisualBasic.Left(Words(i), L) = CheckWords Then
                V = Math.Max(Val(Mid(Words(i), L + 1)), V)
            End If
        Next
        Return CheckWords & CStr(V + 1)
    End Function

    Private Function GetDarkColor(ByVal Col As Color) As Color
        Dim rate As Single = 0.85
        Dim r As Byte = Col.R * rate
        Dim g As Byte = Col.G * rate
        Dim b As Byte = Col.B * rate
        Return Color.FromArgb(r, g, b)
    End Function


    Private Sub txtTextBox_Leave(sender As Object, e As EventArgs) Handles txtTextBox.Leave
        txtTextBox.Visible = False
        Call Set_Data_from_txtBox_To_Grid()
        Print_Grid_Data()
    End Sub

End Class
