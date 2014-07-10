<%@ Application Language="VB" %>
<%@ Import Namespace="System.Data" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
  End Sub

  Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        Dim CodeSplit() As String = Request.Url.ToString.Split("/")
        
        Dim Code As String = CodeSplit(CodeSplit.Length - 1)

    If Code.Contains(".") Then Exit Sub
    Dim oRegEx As New Regex("^.*(?=.*[a-zA-Z])[a-zA-Z0-9]+$")
    If Not oRegEx.IsMatch(Code) Then Exit Sub
    Dim oTable As DataTable = New DBOperations(True).GetData("URLShortner_Code_Select", New String() {Code})
    If oTable IsNot Nothing AndAlso oTable.Rows.Count > 0 Then Response.Redirect(Server.UrlDecode(oTable.Rows(0)(0)))
  End Sub
  
</script>