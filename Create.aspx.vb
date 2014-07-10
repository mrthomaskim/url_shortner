Imports System.Data

Partial Class Create
  Inherits System.Web.UI.Page

  Const MaxCodeLength As Integer = 5
  Const IsDev As Boolean = True

  Dim oGenerator As New RandomStringGenerator
  Dim oDB As DBOperations

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Dim Root As String = "http://"
    Dim Splits() As String = Request.Url.ToString.Split("/")
    For i As Integer = 2 To Splits.Length - 2
      Root &= Splits(i) & "/"
    Next

    lblRoot.Text = Root
    oDB = New DBOperations(IsDev)
  End Sub

  Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
    ShortenURL()
  End Sub

  Protected Sub ShortenURL()
    Dim CodeCount As Long
    Dim Code As String = txtCode.Text
    Dim ExistingCode As String = ""
    Try
      lblMessage.Text = ""
      hlShortURL.Text = ""
      hlShortURL.NavigateUrl = hlShortURL.Text

      If txtURL.Text.Length = 0 Then Throw New Exception("URL field cannot be empty.")
            If Not (txtURL.Text.ToLower.StartsWith("http://") Or txtURL.Text.ToLower.StartsWith("https://")) Then txtURL.Text = "http://" & txtURL.Text
      'ExistingCode = ValidateURL(Server.UrlEncode(txtURL.Text))
      If ExistingCode.Length = 0 Then
        If Code.Length > 0 Then
          CodeCount = ValidateCode(Code)
          If CodeCount < 0 Then Throw oDB.Err
          If CodeCount > 0 Then Throw New Exception("Code is already reserved.")
        Else
          Code = GetCode()
          If Code.Length = 0 Then Exit Sub
        End If

        If Not oDB.AddUpdateDelete("URLShortner_Insert", New String() {Server.UrlEncode(txtURL.Text), Code}) Then Throw oDB.Err
      Else
        Code = ExistingCode
      End If

      txtCode.Text = Code
      hlShortURL.NavigateUrl = lblRoot.Text & Code
      hlShortURL.Text = hlShortURL.NavigateUrl
            lblMessage.Text = "Short URL created successfully. See below </br>"
      lblMessage.ForeColor = Drawing.Color.Green

    Catch ex As Exception
      ShowError(ex)
    End Try
  End Sub

  Protected Function GetCode() As String
    Dim Code As String = ""
    Dim CodeCount As Integer
    Try
      While True
        Code = oGenerator.Generate(MaxCodeLength)
        CodeCount = ValidateCode(Code)
        If CodeCount < 0 Then Throw oDB.Err
        If CodeCount = 0 Then Exit While
      End While
    Catch ex As Exception
      ShowError(ex)
    End Try

    Return Code
  End Function

  Protected Function ValidateCode(ByRef Code As String) As Long
    Return oDB.GetCount("URLShortner_GetCount", New String() {Code})
  End Function

  Protected Function ValidateURL(ByRef URL As String) As String
    Dim oTable As DataTable = oDB.GetData("URLShortner_URL_Select", New String() {URL})
    If oTable Is Nothing Then Return ""
    If oTable.Rows.Count = 0 Then Return ""
    Return oTable.Rows(0)(0)
  End Function

  Protected Sub ShowError(ByRef Ex As Exception)
    lblMessage.Text = Ex.Message
    lblMessage.ForeColor = Drawing.Color.Red
  End Sub

End Class
