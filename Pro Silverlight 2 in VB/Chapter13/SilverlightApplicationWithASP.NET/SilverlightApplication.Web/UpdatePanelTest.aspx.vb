Partial Public Class UpdatePanelTest
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)


    End Sub

    Protected Sub cmdUpdatePost_Click(ByVal sender As Object, ByVal e As EventArgs)
        lbl.Text = "This label was refreshed at " & DateTime.Now.ToLongTimeString()
    End Sub
    Protected Sub cmdUpdateNoPost_Click(ByVal sender As Object, ByVal e As EventArgs)
        lbl.Text = "This label was refreshed at " & DateTime.Now.ToLongTimeString()
    End Sub
End Class
