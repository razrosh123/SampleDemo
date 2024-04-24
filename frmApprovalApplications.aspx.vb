Imports System.Data
Imports System.Data.SqlClient
Imports NUStaff
Public Class frmApprovalApplications
    Inherits System.Web.UI.Page
    '---***** Created on 12-Mar-2020 - SFR C 618 ********---
    Dim objApprovalApp As New clsApprovalApp
    Dim ConstrStaff As String
    Dim ConStaff As SqlConnection
#Region "Function"
    Public Sub DropDownBinding(ByVal dropdown As DropDownList, ByVal dropdown_ds As DataSet, ByVal text As String, ByVal value As String)
        dropdown.DataSource = dropdown_ds
        dropdown.DataTextField = text
        dropdown.DataValueField = value
        dropdown.DataBind()
        dropdown.Items.Insert(0, "--Select--")
    End Sub
    Private Sub Bind_DropDown()
        With objApprovalApp
            Using ConStaff As New SqlConnection(ConstrStaff)
                '-------- Application Type ----------------------------
                Dim ds As New DataSet
                ds = .BindAppType(ConStaff)
                DropDownBinding(ddl_AppType, ds, "AppType", "AppType")
            End Using
        End With
    End Sub
    Private Sub BindPageLoad_App()
        Dim dt As New Data.DataTable
        dt.Columns.Add(New System.Data.DataColumn("AppDate", GetType([String])))
        dt.Columns.Add(New System.Data.DataColumn("AppNo", GetType([String])))
        dt.Columns.Add(New System.Data.DataColumn("Scode", GetType([String])))
        dt.Columns.Add(New System.Data.DataColumn("SName", GetType([String])))
        dt.Columns.Add(New System.Data.DataColumn("Status", GetType([String])))
        gdvApprovalApp.DataSource = dt
        gdvApprovalApp.DataBind()
        ViewState("CurrentState") = dt
    End Sub
    Private Sub BindApplication()
        Dim ds As New DataSet
        With objApprovalApp
            Using ConStaff As New SqlConnection(ConstrStaff)
                ConStaff.Open()
                If ddl_AppType.SelectedValue <> "--Select--" Then
                    .AppType = ddl_AppType.SelectedValue.Trim()
                Else
                    .AppType = ""
                End If
                If ddl_Status.SelectedValue <> "--Select--" Then
                    .AStatus = ddl_Status.SelectedValue.Trim()
                Else
                    .AStatus = ""
                End If
                If txt_FromDate.Text <> "" Then
                    .FromDate = CType(txt_FromDate.Text.Trim(), DateTime).ToString("dd-MMM-yyyy")
                Else
                    .FromDate = "01-Jan-1900"
                End If
                If txt_ToDate.Text <> "" Then
                    .ToDate = CType(txt_ToDate.Text.Trim(), DateTime).ToString("dd-MMM-yyyy")
                Else
                    .ToDate = "01-Jan-1900"
                End If
                '-------------- Added on 09-May-2022 -S- As per mail ---------------
                If txt_StudentID.Text <> "" Then
                    .Scode = txt_StudentID.Text
                Else
                    .Scode = ""
                End If
                '-------------------------------------------------------------------
                ds = .BindApplication(ConStaff)
                If ds.Tables(0).Rows.Count > 0 Then
                    gdvApprovalApp.DataSource = ds
                    gdvApprovalApp.DataBind()
                    ViewState("CurrentState") = ds.Tables(0)
                Else
                    BindPageLoad_App()
                End If
                ds.Dispose()
            End Using
        End With
    End Sub
    '------------------- Added on 09-May-2022 - As per mail -----------------------
    <System.Web.Script.Services.ScriptMethod(), _
System.Web.Services.WebMethod()> _
    Public Shared Function GetCompletionList(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)
        Dim cmd As SqlCommand
        Dim sdr As SqlDataReader
        ' Dim contextKey As String = contextKey.ToString
        Dim ConPromis As SqlConnection
        Dim ConString As String = ConfigurationManager.ConnectionStrings("ConStaff").ConnectionString()
        ConPromis = New SqlConnection(ConString)
        If ConPromis.State = ConnectionState.Closed Then ConPromis.Open()
        Dim customers As List(Of String) = New List(Of String)
        Dim ssql As String
        If contextKey = "StudentID" Then
            ssql = "select distinct a.Scode,s.Sname from applications a inner join Students s on a.scode=s.scode where a.scode like @Name+'%' order by a.scode"
            cmd = New SqlCommand(ssql, ConPromis)
            cmd.Parameters.AddWithValue("@Name", EscapeLikeValue(prefixText))
            sdr = cmd.ExecuteReader
            While sdr.Read
                customers.Add(sdr("Scode") + " => " + sdr("SName"))
            End While
            sdr.Close()
        ElseIf contextKey = "StudentName" Then
            ssql = "select distinct a.Scode,s.Sname from applications a inner join Students s on a.scode=s.scode where s.Sname like @Name+'%' order by s.Sname"
            cmd = New SqlCommand(ssql, ConPromis)
            cmd.Parameters.AddWithValue("@Name", EscapeLikeValue(prefixText))
            sdr = cmd.ExecuteReader
            While sdr.Read
                customers.Add(sdr("Sname") + " => " + sdr("Scode"))
            End While
            sdr.Close()
        End If

        ConPromis.Close()
        Return customers
    End Function
    Public Shared Function EscapeLikeValue(ByVal valueWithoutWildcards As String) As String
        Dim sb As New StringBuilder()
        For i As Integer = 0 To valueWithoutWildcards.Length - 1
            Dim c As Char = valueWithoutWildcards(i)
            If c = "*"c OrElse c = "%"c OrElse c = "["c OrElse c = "]"c Then
                sb.Append("[").Append(c).Append("]")
            ElseIf c = "'"c Then
                sb.Append("''")
            Else
                sb.Append(c)
            End If
        Next
        Return sb.ToString()
    End Function
    '------------------------------------------------------------------------------
#End Region

    Private Sub frmApprovalApplications_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try

            ConstrStaff = ConfigurationManager.ConnectionStrings("ConStaff").ConnectionString
        Catch ex As Exception
            lblMSG.Text = ex.Message
            lblMSG.Visible = True
            lblMSG.CssClass = "redMessage"
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Scode") <> Nothing Then
                btn_Close.Attributes.Add("onkeydown", _
       "if (event.which || event.keyCode) { if ((event.which == 9) || (event.keyCode == 9)) " & _
       "{ if (document.getElementById('" & txt_StudentID.ClientID & "').disabled == false) " & _
       "{document.getElementById('" & txt_StudentID.ClientID & "').focus();return false;}else if " & _
       "(document.getElementById('" & ddl_AppType.ClientID & "').disabled == false) " & _
       "{document.getElementById('" & ddl_AppType.ClientID & "').focus();return false;}}}")
                If Not IsPostBack Then
                    Bind_DropDown()
                    'BindPageLoad_App()
                    BindApplication()
                    txt_StudentID.Focus()
                    Dim Ks As Integer = 1000
                End If
            Else
                Session.Clear()
                Response.Redirect("../NUStaffLogin.aspx")
            End If
        Catch ex As Exception
            lblMSG.Text = ex.Message
            lblMSG.Visible = True
            lblMSG.CssClass = "redMessage"
        End Try
    End Sub

    Protected Sub ddl_AppType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddl_AppType.SelectedIndexChanged
        Try
            lblMSG.Text = ""
            BindApplication()
        Catch ex As Exception
            lblMSG.Text = ex.Message
            lblMSG.Visible = True
            lblMSG.CssClass = "redMessage"
        End Try
    End Sub

    Protected Sub txt_FromDate_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txt_FromDate.TextChanged
        Try
            lblMSG.Text = ""
            If IsDate(txt_FromDate.Text) Then
                'If CType(txt_FromDate.Text, DateTime) > CType(txt_ToDate.Text, DateTime) Then
                '    lblMSG.Visible = True
                '    lblMSG.CssClass = "redMessage"
                '    lblMSG.Text = "To Date is less than From Date...!"
                '    Exit Sub
                'End If
                BindApplication()
                txt_ToDate.Focus()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "script", "alert('Enter valid date');", True)
                txt_FromDate.Text = ""
                txt_FromDate.Focus()
            End If
        Catch ex As Exception
            lblMSG.Text = ex.Message
            lblMSG.Visible = True
            lblMSG.CssClass = "redMessage"
        End Try
    End Sub

    Protected Sub txt_ToDate_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txt_ToDate.TextChanged
        Try
            lblMSG.Text = ""
            If IsDate(txt_ToDate.Text) Then
                BindApplication()
                gdvApprovalApp.Focus()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "script", "alert('Enter valid date');", True)
                txt_ToDate.Text = ""
                txt_ToDate.Focus()
            End If
        Catch ex As Exception
            lblMSG.Text = ex.Message
            lblMSG.Visible = True
            lblMSG.CssClass = "redMessage"
        End Try
    End Sub

    Protected Sub ddl_Status_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddl_Status.SelectedIndexChanged
        Try
            lblMSG.Text = ""
            BindApplication()
        Catch ex As Exception
            lblMSG.Text = ex.Message
            lblMSG.Visible = True
            lblMSG.CssClass = "redMessage"
        End Try
    End Sub

    Private Sub btn_Close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Close.Click
        Try
            lblMSG.Text = ""
            Response.Redirect("~/StaffPanel/frmProfile.aspx")
        Catch ex As Exception
            lblMSG.Text = ex.Message
            lblMSG.Visible = True
            lblMSG.CssClass = "redMessage"
        End Try
    End Sub

    Private Sub gdvApprovalApp_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvApprovalApp.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim strArgumentACode As String = e.Row.RowIndex
                e.Row.Attributes.Add("ondblclick", "ShowDetails(" & strArgumentACode & ");")
            End If
        Catch ex As Exception
            lblMSG.Text = ex.Message
            lblMSG.Visible = True
            lblMSG.CssClass = "redMessage"
        End Try
    End Sub

    Private Sub btn_Confirm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Confirm.Click
        Try
            lblMSG.Text = ""
            If hdn_Find.Value = "ShowDetails" Then
                If gdvApprovalApp.Rows.Count > 0 Then
                    gdvApprovalApp.SelectedIndex = Hdn_Value.Value
                    If gdvApprovalApp.Rows(Hdn_Value.Value).Cells(5).Text.Trim() = "Discontinued" Or gdvApprovalApp.Rows(Hdn_Value.Value).Cells(5).Text.Trim() = "Postponed" Or gdvApprovalApp.Rows(Hdn_Value.Value).Cells(5).Text.Trim() = "Studying" Then
                        Session("AppNo") = gdvApprovalApp.Rows(Hdn_Value.Value).Cells(1).Text.Trim()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Script", "OpenChangeofStatus('" & gdvApprovalApp.Rows(Hdn_Value.Value).Cells(5).Text.Trim() & "');", True)
                    ElseIf gdvApprovalApp.Rows(Hdn_Value.Value).Cells(5).Text.Trim() = "WithDrawn" Then
                        Session("AppNo") = gdvApprovalApp.Rows(Hdn_Value.Value).Cells(1).Text.Trim()
                        Hdn_Appno.Value = Session("AppNo")
                        Hdn_scode.Value = gdvApprovalApp.Rows(Hdn_Value.Value).Cells(2).Text.Trim()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Script", "OpenModuleWithDrawal();", True)
                    ElseIf gdvApprovalApp.Rows(Hdn_Value.Value).Cells(5).Text.Trim() = "Change MOS" Then
                        Session("AppNo") = gdvApprovalApp.Rows(Hdn_Value.Value).Cells(1).Text.Trim()
                        Hdn_Value.Value = Session("AppNo")
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Script", "OpenChangeMOS();", True)
                    ElseIf gdvApprovalApp.Rows(Hdn_Value.Value).Cells(5).Text.Trim() = "Change of Program" Then
                        Session("AppNo") = gdvApprovalApp.Rows(Hdn_Value.Value).Cells(1).Text.Trim()
                        Hdn_Value.Value = Session("AppNo")
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Script", "OpenChangeMOS();", True)
                    ElseIf gdvApprovalApp.Rows(Hdn_Value.Value).Cells(5).Text.Trim() = "Mitigation Requests" Then
                        Session("AppNo") = gdvApprovalApp.Rows(Hdn_Value.Value).Cells(1).Text.Trim()
                        Hdn_Value.Value = Session("AppNo")
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Script", "OpenMitigations('" & gdvApprovalApp.Rows(gdvApprovalApp.SelectedIndex).Cells(4).Text.Trim() & "');", True)
                    End If
                End If
            ElseIf hdn_Find.Value = "RefreshRequests" Then
                BindApplication()
            End If
        Catch ex As Exception
            lblMSG.Text = ex.Message
            lblMSG.Visible = True
            lblMSG.CssClass = "redMessage"
        End Try
    End Sub

    Private Sub txt_StudentID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_StudentID.TextChanged
        Try
            lblMSG.Text = ""
            With objApprovalApp
                Dim Student As String
                Using ConStaff As New SqlConnection(ConstrStaff)
                    ConStaff.Open()
                    Student = .Check_ExitOrNot(ConStaff, "Students", "SName", "SCode", txt_StudentID.Text)
                End Using
                If Student <> "" Then
                    ddl_AppType.Focus()
                Else
                    lblMSG.Text = "Invalid Student...!"
                    lblMSG.Visible = True
                    lblMSG.CssClass = "redMessage"
                    txt_StudentID.Text = ""
                    txt_StudentName.Text = ""
                    txt_StudentID.Focus()
                End If
            End With
            BindApplication()
        Catch ex As Exception
            lblMSG.Text = ex.Message
            lblMSG.Visible = True
            lblMSG.CssClass = "redMessage"
        End Try
    End Sub

    Private Sub txt_StudentName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_StudentName.TextChanged
        Try
            lblMSG.Text = ""
            If txt_StudentName.Text <> "" Then
                With objApprovalApp
                    Dim Student As String
                    Using ConStaff As New SqlConnection(ConstrStaff)
                        ConStaff.Open()
                        Student = .Check_ExitOrNot(ConStaff, "Students", "SCode", "SName", txt_StudentName.Text)
                    End Using
                    If Student <> "" Then
                        ddl_AppType.Focus()
                    Else
                        lblMSG.Text = "Invalid Student...!"
                        lblMSG.Visible = True
                        lblMSG.CssClass = "redMessage"
                        txt_StudentID.Text = ""
                        txt_StudentName.Text = ""
                        txt_StudentName.Focus()
                    End If
                End With
                BindApplication()
            Else
                txt_StudentID.Text = ""
            End If
        Catch ex As Exception
            lblMSG.Text = ex.Message
            lblMSG.Visible = True
            lblMSG.CssClass = "redMessage"
        End Try
    End Sub
End Class