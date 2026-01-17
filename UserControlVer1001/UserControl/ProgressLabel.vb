Public Class KTGISProgressLabel
    Dim Max_Value As Single
    Dim ShowValue As Boolean
    Dim tx As String
    Public Sub Start(ByVal MaxValue As Integer, ByVal Text As String, ByVal show_Value_f As Boolean)
        Max_Value = MaxValue
        tx = Text
        ProgressBar.Maximum = 100
        ShowValue = show_Value_f
        Label.Text = getText(0)
        Me.Visible = True
    End Sub
    Public Sub SetValue(ByVal Value As Single)
        If Value <= Max_Value Then
            ProgressBar.Value = Value / Max_Value * 100
        Else
            ProgressBar.Value = 100
        End If

        Label.Text = getText(Value)
        Me.Update()
    End Sub
    Private Function getText(ByVal Value As Single) As String
        Dim t As String = tx
        If ShowValue = True Then
            t = t + " " + Value.ToString + "/" + Max_Value.ToString
        End If
        Return t
    End Function
    Public Sub Finish()
        Me.Visible = False
    End Sub
    <System.ComponentModel.DefaultValue(GetType(System.Drawing.Color), "Red")> _
    Public Property LabelColor() As System.Drawing.Color
        Get
            Return Label.BackColor
        End Get
        Set(value As System.Drawing.Color)
            Label.BackColor = value
        End Set
    End Property
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Me.Visible = False
    End Sub


End Class
