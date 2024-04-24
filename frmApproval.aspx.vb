Imports System.Data.SqlClient
Imports NUStaff
Imports System.IO
Imports System.Net

Public Class frmApproval
    Inherits System.Web.UI.Page
    Dim conString As String
    Dim Constf As String
    Dim ConStaff As SqlConnection
    Dim objApprovalSettings As New ClsApprovalforLateAttendance
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Scode") <> Nothing Then
            If Not IsPostBack Then
                BindPageLoad_ApprovalDetails()
                Dim s As Integer = 0
                TabContainer2.ActiveTabIndex = 0
                ClearAll()
                ddl_Semester.Focus()
                btn_cancel.Attributes.Add("onkeydown", _
                               "if (event.which || event.keyCode) { if ((event.which == 9) || (event.keyCode == 9)) " & _
                               "{ if (document.getElementById('" & ddl_Semester.ClientID & "').disabled == false) " & _
                               "{document.getElementById('" & ddl_Semester.ClientID & "').focus();return false;}else if " & _
                               "(document.getElementById('" & txt_FacultyID.ClientID & "').disabled == false) " & _
                               "{document.getElementById('" & txt_FacultyID.ClientID & "').focus();return false;} else if " & _
                               "(document.getElementById('" & btn_Save.ClientID & "').disabled == false) " & _
                               "{document.getElementById('" & btn_Save.ClientID & "').focus();return false;}} " & _
                               "else if  ((event.which == 8) || (event.keyCode == 8)){return (event.keyCode!=8);} }")
                Bind_DropDown()
                If ddl_Mode.SelectedValue = "A" Then
                    txt_AssignID.Enabled = False
                    ddlAssName.Enabled = False
                    txt_StudentID.Enabled = False
                    ddl_StudName.Enabled = False
                    '------ Added on 06-Oct-2020 - As per SSA M 1046 --------------
                    txt_AssignID.Visible = False
                    ddlAssName.Visible = False
                    txt_StudentID.Visible = False
                    ddl_StudName.Visible = False
                    lblStudentID.Visible = False
                    lblAssignmentID.Visible = False
                    lbl_ExpiredOn.Visible = True
                    txt_Expired.Visible = True
                    Session("AppType") = "A"
                    '--------- Added on 11-Dec-2023 -SJ- As per chat ---------------
                ElseIf ddl_Mode.SelectedValue = "H" Then
                    txt_StudentID.Enabled = True
                    ddl_StudName.Enabled = True
                    txt_StudentID.Visible = True
                    ddl_StudName.Visible = True
                    lblStudentID.Visible = True
                    Session("AppType") = "H"
                    Panel1.Visible = False
                    lbl_ExpiredOn.Visible = False
                    txt_Expired.Visible = False
                    lbl_Title.InnerText = "Attendance Approval for HallTicket"
                    '--------------------------------------------------------------
                ElseIf ddl_Mode.SelectedValue = "P" Then 'Added on 30-Jan-2024 -SJ- As per SFR C 777
                    txt_StudentID.Enabled = True
                    ddl_StudName.Enabled = True
                    txt_StudentID.Visible = True
                    ddl_StudName.Visible = True
                    lblStudentID.Visible = True
                    Session("AppType") = "P"
                    Panel1.Visible = False
                    lbl_ExpiredOn.Visible = True
                    txt_Expired.Visible = True
                    lbl_Title.InnerText = "Exempt Pre-requisites"
                    '---------- Added on 11-Apr-2024 -SJ- As per mail -------------
                    lbl_CSID.Text = "Course to be Registered"
                    '--------------------------------------------------------------
                Else
                    '--- Commented on 16-Oct-2023 -SJ- As per call ---
                    'txt_AssignID.Enabled = True
                    'ddlAssName.Enabled = True
                    'txt_AssignID.Visible = True
                    'ddlAssName.Visible = True
                    'lblAssignmentID.Visible = True
                    '--------------------------------
                    txt_StudentID.Enabled = True
                    ddl_StudName.Enabled = True
                    '------ Added on 06-Oct-2020 - As per SSA M 1046 --------------
                    txt_StudentID.Visible = True
                    ddl_StudName.Visible = True
                    lblStudentID.Visible = True
                    Session("AppType") = "M"
                    Panel1.Visible = False 'Added on 16-Oct-2023 -SJ- As per call 
                    lbl_ExpiredOn.Visible = True
                    txt_Expired.Visible = True
                    lbl_Title.InnerText = "Marks Correction After Result Published"
                    '--------------------------------------------------------------
                End If
            End If
            Dim ScriptManager As ScriptManager = ScriptManager.GetCurrent(Page)
            ScriptManager.RegisterPostBackControl(gdv_ViewAttachdDoc)
        Else
            Session.Clear()
            Response.Redirect("../NUStaffLogin.aspx")
        End If

    End Sub
    Sub getconnection()
        conString = ConfigurationManager.ConnectionStrings("ConStaff").ConnectionString()
        ConStaff = New SqlConnection(conString)
    End Sub
    <System.Web.Script.Services.ScriptMethod(), _
System.Web.Services.WebMethod()> _
    Public Shared Function GetCompletionList(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)
        Dim cmd As SqlCommand
        Dim sdr As SqlDataReader
        ' Dim contextKey As String = contextKey.ToString
        Dim ConStaff As SqlConnection
        Dim ConString As String = ConfigurationManager.ConnectionStrings("ConStaff").ConnectionString()
        ConStaff = New SqlConnection(ConString)
        If ConStaff.State = ConnectionState.Closed Then ConStaff.Open()
        Dim customers As List(Of String) = New List(Of String)
        Dim ssql As String
        If contextKey = "Faculty" Then
            ssql = "select Empid,EmpName from employees where Empid like @Name+'%'  ORDER BY EmpName "
            cmd = New SqlCommand(ssql, ConStaff)
            cmd.Parameters.AddWithValue("@Name", EscapeLikeValue(prefixText))
            sdr = cmd.ExecuteReader
            While sdr.Read
                customers.Add(sdr("Empid") + " => " + sdr("EmpName"))
            End While
            sdr.Close()
        ElseIf contextKey = "FacultyN" Then
            ssql = "select Empid,EmpName from employees where EmpName like @Name+'%'  ORDER BY EmpName "
            cmd = New SqlCommand(ssql, ConStaff)
            cmd.Parameters.AddWithValue("@Name", EscapeLikeValue(prefixText))
            sdr = cmd.ExecuteReader
            While sdr.Read
                customers.Add(sdr("EmpName") + " => " + sdr("Empid"))
            End While
            sdr.Close()

        ElseIf contextKey = "StudentS" Then
            ssql = "select Scode,Sname from students where Scode like @Name+'%'  ORDER BY Sname "
            cmd = New SqlCommand(ssql, ConStaff)
            cmd.Parameters.AddWithValue("@Name", EscapeLikeValue(prefixText))
            sdr = cmd.ExecuteReader
            While sdr.Read
                customers.Add(sdr("Scode") + " => " + sdr("Sname"))
            End While
            sdr.Close()
        ElseIf contextKey = "StudentNameS" Then
            ssql = "select Scode,Sname from students where Sname like @Name+'%'  ORDER BY Sname "
            cmd = New SqlCommand(ssql, ConStaff)
            cmd.Parameters.AddWithValue("@Name", EscapeLikeValue(prefixText))
            sdr = cmd.ExecuteReader
            While sdr.Read
                customers.Add(sdr("Sname") + " => " + sdr("Scode"))
            End While
            sdr.Close()
        ElseIf contextKey = "CsID" Then
            If HttpContext.Current.Session("SMID").ToString <> "" Then
                'cmd = New SqlCommand("select CsID,CSName from Courses(Nolock) where CSID in (Select CSID from AsgTeachersEligibility where " & _
                '                                       " SMID='" & HttpContext.Current.Session("SMID").ToString & "') and CsID like @Name+'%'", ConStaff)
                '--------------- Added on 06-Oct-2020 - As per SSA M 1046 -----------------------
                If HttpContext.Current.Session("AppType").ToString = "M" Or HttpContext.Current.Session("AppType").ToString = "H" Then
                    'cmd = New SqlCommand("select CsID,CSName from Courses(Nolock) where CSID in (Select CSID from AsgTeachersEligibility where " & _
                    '                                   " SMID='" & HttpContext.Current.Session("SMID").ToString & "' and " & _
                    '                                   "EMPID='" & HttpContext.Current.Session("FacultyID").ToString & "') and CsID like @Name+'%'", ConStaff)
                    '--------------- Modified on 16-Oct-2023 -SJ- As per call --------------------------------
                    cmd = New SqlCommand("select CsID,CSName from Courses(Nolock) where CSID in (Select CSID from STCOURSES with(Nolock) where " & _
                                         " SMID='" & HttpContext.Current.Session("SMID").ToString & "' and " & _
                                         "Scode='" & HttpContext.Current.Session("StudentID").ToString & "') and CsID like @Name+'%'", ConStaff)
                ElseIf HttpContext.Current.Session("AppType").ToString = "P" Then 'Added on 30-Jan-2024 -SJ- As per SFR C 777
                    cmd = New SqlCommand("select CsID,CSName from Courses(Nolock) where CSID in (select Distinct CsID from TTCourseAllotment with(Nolock) where " & _
                                         " SMID='" & HttpContext.Current.Session("SMID").ToString & "') and CsID like @Name+'%'", ConStaff)
                Else
                    cmd = New SqlCommand("select CsID,CSName from Courses(Nolock) where CSID in (Select CSID from TeachersEligibility where " & _
                                                       " SMID='" & HttpContext.Current.Session("SMID").ToString & "' and " & _
                                                       "EMPID='" & HttpContext.Current.Session("FacultyID").ToString & "') and CsID like @Name+'%'", ConStaff)
                End If
                '--------------------------------------------------------------------------------
                cmd.Parameters.AddWithValue("@Name", EscapeLikeValue(prefixText))
                sdr = cmd.ExecuteReader
                While sdr.Read
                    customers.Add(sdr("CsID") + " => " + sdr("CSName"))
                End While
                sdr.Close()
            End If
        ElseIf contextKey = "AsgID" Then
            If HttpContext.Current.Session("SMID").ToString <> "" And HttpContext.Current.Session("ModID").ToString <> "" Then
                cmd = New SqlCommand(" select AsgID,AsgName from SemesterAsg(Nolock) where SMID='" & HttpContext.Current.Session("SMID").ToString & "' " & _
                                     " and CSID='" & HttpContext.Current.Session("ModID").ToString & "' and AsgID like @Name+'%'", ConStaff)
                cmd.Parameters.AddWithValue("@Name", EscapeLikeValue(prefixText))
                sdr = cmd.ExecuteReader
                While sdr.Read
                    customers.Add(sdr("AsgID") + " => " + sdr("AsgName"))
                End While
                sdr.Close()
            End If
        ElseIf contextKey = "StudID" Then
            Dim counts = 0
            If HttpContext.Current.Session("AppType").ToString = "M" Or HttpContext.Current.Session("AppType").ToString = "H" Then 'Added on 16-Oct-2023 -SJ- As per call
                If HttpContext.Current.Session("SMID").ToString <> "" Then
                    cmd = New SqlCommand("select distinct st.Scode,s.SName from STCOURSES st with(Nolock) inner join Students s with(Nolock) on st.Scode=s.Scode where " & _
                                         " st.SmID='" & HttpContext.Current.Session("SMID").ToString & "' " & _
                                         " and st.Scode like @Name+'%' order by S.SName", ConStaff)
                    counts = 1
                End If
            ElseIf HttpContext.Current.Session("AppType").ToString = "P" Then 'Added on 30-Jan-2024 -SJ- As per SFR C 777
                cmd = New SqlCommand("select distinct Scode,SName from Students with(Nolock) where Status='A' and Scode like @Name+'%' order by SName", ConStaff)
                counts = 1
            Else
                If HttpContext.Current.Session("SMID").ToString <> "" Then
                    cmd = New SqlCommand("select distinct st.Scode,s.SName from StMarks st inner join Students s on st.Scode=s.Scode where " & _
                                         " st.SmID='" & HttpContext.Current.Session("SMID").ToString & "' " & _
                                         " and st.Scode like @Name+'%' order by S.SName", ConStaff)
                    counts = 1
                End If

                If HttpContext.Current.Session("SMID").ToString <> "" And HttpContext.Current.Session("ModID").ToString <> "" Then

                    cmd = New SqlCommand("select distinct st.Scode,s.SName from StMarks st inner join Students s on st.Scode=s.Scode where " & _
                                         " st.SmID='" & HttpContext.Current.Session("SMID").ToString & "' and st.CsId='" & HttpContext.Current.Session("ModID").ToString & "' " & _
                                         " and st.Scode like @Name+'%' order by S.SName", ConStaff)
                    counts = 1
                End If
                If HttpContext.Current.Session("SMID").ToString <> "" And HttpContext.Current.Session("ModID").ToString <> "" And HttpContext.Current.Session("AssID").ToString <> "" Then

                    cmd = New SqlCommand("select distinct st.Scode,s.SName from StMarks st inner join Students s on st.Scode=s.Scode where " & _
                                         " st.SmID='" & HttpContext.Current.Session("SMID").ToString & "' and st.CsId='" & HttpContext.Current.Session("ModID").ToString & "' " & _
                                         " and st.AsgId='" & HttpContext.Current.Session("AssID").ToString & "' and " & _
                                         " st.Scode like @Name+'%'  order by S.SName", ConStaff)
                    counts = 1
                End If
            End If

            If counts = 1 Then
                cmd.Parameters.AddWithValue("@Name", EscapeLikeValue(prefixText))
                sdr = cmd.ExecuteReader
                While sdr.Read
                    customers.Add(sdr("Scode") + " => " + sdr("SName"))
                End While
                sdr.Close()
            End If
            '------ Added on 26-Feb-2024 -SJ- As per call -----------------
        ElseIf contextKey = "SMID" Then
            ssql = "select SMID,SMName from Semesters where SMID like @Name+'%'  ORDER BY SMID"
            cmd = New SqlCommand(ssql, ConStaff)
            cmd.Parameters.AddWithValue("@Name", EscapeLikeValue(prefixText))
            sdr = cmd.ExecuteReader
            While sdr.Read
                customers.Add(sdr("SMID") + " => " + sdr("SMName"))
            End While
            sdr.Close()
        ElseIf contextKey = "SMName" Then
            ssql = "select SMID,SMName from Semesters where SMName like @Name+'%'  ORDER BY SMName"
            cmd = New SqlCommand(ssql, ConStaff)
            cmd.Parameters.AddWithValue("@Name", EscapeLikeValue(prefixText))
            sdr = cmd.ExecuteReader
            While sdr.Read
                customers.Add(sdr("SMName") + " => " + sdr("SMID"))
            End While
            sdr.Close()
            '------------- Added on 12-Apr-2024 -SJ- As per mail -------------------
        ElseIf contextKey = "PreReqCsID" Then
            If HttpContext.Current.Session("ModID").ToString <> "" Then
                If HttpContext.Current.Session("AppType").ToString = "P" Then
                    cmd = New SqlCommand("select * from (select cq.SubID1 as CSID,c.CsName from courseQualification cq inner join courses c on cq.SubID1=c.CsID where " & _
                                         "cq.CsID='" & HttpContext.Current.Session("ModID").ToString & "' union " & _
                                 "select cq.SubID2 as CSID,c.CsName from courseQualification cq inner join courses c on cq.SubID2=c.CsID where " & _
                                 "cq.CsID='" & HttpContext.Current.Session("ModID").ToString & "' union " & _
                                 "select cq.SubID3 as CSID,c.CsName from courseQualification cq inner join courses c on cq.SubID3=c.CsID where " & _
                                 "cq.CsID='" & HttpContext.Current.Session("ModID").ToString & "' " & _
                                 ")t where t.CSID like @Name+'%' order by CsName", ConStaff)
                End If
                cmd.Parameters.AddWithValue("@Name", EscapeLikeValue(prefixText))
                sdr = cmd.ExecuteReader
                While sdr.Read
                    customers.Add(sdr("CsID") + " => " + sdr("CSName"))
                End While
                sdr.Close()
            End If
            '--------------------------------
        End If
        'cmd = New SqlCommand(ssql, ConStaff)
        'cmd.Parameters.AddWithValue("@Name", EscapeLikeValue(prefixText))
        'sdr = cmd.ExecuteReader
        'While sdr.Read
        '    If contextKey = "Faculty" Then
        '        customers.Add(sdr("Empid") + " => " + sdr("EmpName"))
        '    ElseIf contextKey = "FacultyN" Then
        '        customers.Add(sdr("EmpName") + " => " + sdr("Empid"))
        '    ElseIf contextKey = "Student" Then
        '        customers.Add(sdr("Scode") + " => " + sdr("Sname"))
        '    ElseIf contextKey = "StudentN" Then
        '        customers.Add(sdr("Sname") + " => " + sdr("Scode"))
        '    End If

        'End While
        'sdr.Close()
        ConStaff.Close()
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
    Private Sub BindPageLoad_ApprovalDetails()
        Dim dt As New Data.DataTable
        dt.Columns.Add(New System.Data.DataColumn("DocID", GetType([String])))
        dt.Columns.Add(New System.Data.DataColumn("DocDate", GetType([String])))
        dt.Columns.Add(New System.Data.DataColumn("SmID", GetType([String])))
        dt.Columns.Add(New System.Data.DataColumn("EmpID", GetType([String])))
        dt.Columns.Add(New System.Data.DataColumn("CsID", GetType([String])))
        '---------Added on 12-Apr-2024 -SJ- As per mail ---------------
        dt.Columns.Add(New System.Data.DataColumn("CourseName", GetType([String])))
        dt.Columns.Add(New System.Data.DataColumn("PreCourseID", GetType([String])))
        dt.Columns.Add(New System.Data.DataColumn("PreReqCSName", GetType([String])))
        '-------------------------
        dt.Columns.Add(New System.Data.DataColumn("StudID", GetType([String])))
        dt.Columns.Add(New System.Data.DataColumn("StudName", GetType([String])))
        dt.Columns.Add(New System.Data.DataColumn("Comments", GetType([String]))) 'Added on 26-Feb-2024 -SJ- As per mail
        '-------------Added on 12-Apr-2024 -SJ- As per mail---------------------
        dt.Columns.Add(New System.Data.DataColumn("Regdate", GetType([String])))
        dt.Columns.Add(New System.Data.DataColumn("Status", GetType([String])))
        '-----------------------
        gdv_ApprovalSettings.DataSource = dt
        gdv_ApprovalSettings.DataBind()
        ViewState("CurrentState") = dt
        gdv_ApprovalSettings.SelectedIndex = -1
    End Sub
    Private Sub BindData_ApprovalDetails()

        Dim ds As New DataSet
        With objApprovalSettings
            getconnection()
            If txt_SFacultyID.Text <> "" Then
                ._FacultyID = txt_SFacultyID.Text
            Else
                ._FacultyID = ""
            End If
            If txt_SStudentID.Text <> "" Then
                ._StudentID = txt_SStudentID.Text
            Else
                ._StudentID = ""
            End If
            '------------- Added on 26-Feb-2024 -SJ- As per call -------------
            If txt_SMID.Text <> "" Then
                .SemID = txt_SMID.Text
            Else
                .SemID = ""
            End If
            '------------------------------------------------------------------
            ._Apptype = ddl_Mode.SelectedItem.Value
            ds = .BindData_ApprovalDetails(ConStaff)
            If ds.Tables(0).Rows.Count > 0 Then
                gdv_ApprovalSettings.DataSource = ds
                gdv_ApprovalSettings.DataBind()
                ViewState("CurrentState") = ds.Tables(0)
            Else
                BindPageLoad_ApprovalDetails()
            End If
            gdv_ApprovalSettings.SelectedIndex = -1
            ds.Dispose()
        End With

    End Sub

    Protected Sub btn_Search_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Search.Click
        lbl_msg.Text = ""
        If ddl_Mode.SelectedIndex = 0 Then
            ddl_Mode.Focus()
            lbl_msg.Text = "Please select approval type...!"
            lbl_msg.CssClass = "redMessage"
            ddl_Mode.Focus()
            Exit Sub
        End If
        BindData_ApprovalDetails()
    End Sub
    Private Sub Bind_DropDown()
        getconnection()
        With objApprovalSettings
            Dim ds_Sem As New DataSet
            If ddl_Mode.SelectedValue = "M" Then
                ds_Sem = .getSemesterM(ConStaff)
            Else
                ds_Sem = .getSemester(ConStaff)
            End If


            ddl_Semester.DataSource = ds_Sem
            ddl_Semester.DataTextField = "SMName"
            ddl_Semester.DataValueField = "SMID"

            ddl_Semester.DataBind()
            ddl_Semester.Items.Insert(0, "--select--")
            Call ddl_Semester_SelectedIndexChanged(ddl_Semester, New EventArgs)
        End With
    End Sub

    Protected Sub ddl_Semester_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddl_Semester.SelectedIndexChanged
        Try
            lbl_msg.Text = ""
            Session("SMID") = ""
            Session("ModID") = ""
            Session("AssID") = ""
            Session("StudentID") = "" 'Added on 16-Oct-2023 -SJ- As per call and chat
            If ddl_Semester.SelectedValue <> "--select--" Then
                Session("AppType") = ddl_Mode.SelectedValue
                txt_AssignID.Text = ""
                txt_CourseID.Text = ""
                Session("SMID") = ddl_Semester.SelectedValue
                With objApprovalSettings
                    getconnection()
                    Dim ds_Mod, ds_Ass, ds_Class As New DataSet
                    .SemID = ddl_Semester.SelectedValue
                    .CSID = txt_CourseID.Text
                    .EmpID = getEmpID()
                    '<-------------- Bind Modules -------------------------->
                    'ds_Mod = .getModule(ConStaff, Session("Type"))
                    'DropDownBinding(ddlCourseName, ds_Mod, "CSName", "CsID")
                    '---------- Added on 06-Oct-2020 - As per SSA M 1046 ---------------
                    If ddl_Mode.SelectedValue = "M" Then
                        'ds_Mod = .getModule(ConStaff, Session("Type"))
                        'DropDownBinding(ddlCourseName, ds_Mod, "CSName", "CsID")
                    ElseIf ddl_Mode.SelectedValue = "P" Then 'Added on 30-Jan-2024 -SJ- As per SFR C 777
                        Dim SMEDate As String = .Check_ExitOrNot(ConStaff, "Semesters", "SMEDate", "SMID", ddl_Semester.SelectedValue)
                        txt_Expired.Text = CType(SMEDate, DateTime).ToString("dd-MMM-yyyy")
                        ds_Mod = .getModule_RegisteredCourses(ConStaff)
                        DropDownBinding(ddlCourseName, ds_Mod, "CSName", "CsID")
                    Else
                        ds_Mod = .getModule_AttendanceApproval(ConStaff, Session("Type"))
                        DropDownBinding(ddlCourseName, ds_Mod, "CSName", "CsID")
                    End If
                    '----------------------------------------------------------------------
                    txt_CourseID.Text = ""
                    '<-------------- Bind Students -------------------------->
                    .AssID = txt_AssignID.Text
                    .EnteredBy = Session("EmpShForms")
                    Dim dsstud As New DataSet
                    ._Apptype = ddl_Mode.SelectedValue 'Added on 16-Oct-2023 -S- As per call and chat
                    dsstud = .BindStudents(ConStaff)
                    txt_StudentID.Text = ""
                    DropDownBinding(ddl_StudName, dsstud, "SNAME", "SCODE")
                    '---------------------------------------------------------
                    If ddl_Semester.SelectedIndex <> 0 And txt_CourseID.Text <> "" Then
                        '<-------------- Bind Assignments -------------------------->
                        ds_Ass = .getAssignment(ConStaff)
                        DropDownBinding(ddlAssName, ds_Ass, "AsgName", "AsgID")
                        txt_AssignID.Text = ""
                    End If
                End With
            Else
                ddl_Semester.SelectedIndex = 0
                txt_AssignID.Text = ""
                txt_CourseID.Text = ""

                ddlAssName.Items.Clear()
                ddlAssName.Items.Insert(0, "--select--")

                ddlCourseName.Items.Clear()
                ddlCourseName.Items.Insert(0, "--select--")

                ddl_StudName.Items.Clear()
                ddl_StudName.Items.Insert(0, "--select--")
                txt_StudentID.Text = ""
            End If
            ddl_Semester.Focus()
        Catch ex As Exception
            lbl_msg.Text = ex.Message
            lbl_msg.Visible = True
            lbl_msg.CssClass = "redMessage"
        End Try
    End Sub
    Private Function getEmpID() As String
        Dim EmpID, EmpName As String
        Dim dtEMp As DataTable
        getconnection()
        'objApprovalSettings._UserName = Session("Scode")
        objApprovalSettings._UserName = txt_FacultyID.Text
        dtEMp = objApprovalSettings.IsEmployee(ConStaff)
        If dtEMp.Rows.Count > 0 Then
            ' is faculty
            EmpID = dtEMp.Rows(0).Item(0).ToString
            EmpName = dtEMp.Rows(0).Item(1).ToString
            Session("Type") = "Faculty"
            Session("EMPID") = EmpID
            Session("EMPName") = EmpName
        Else
            ' is Not faculty
            Session("Type") = "NotFaculty"
            EmpID = ""
            EmpName = ""
            Session("EMPID") = ""
        End If
        Return EmpID
    End Function
    Public Sub DropDownBinding(ByVal dropdown As DropDownList, ByVal dropdown_ds As DataSet, ByVal text As String, ByVal value As String)
        If dropdown_ds.Tables.Count > 0 Then
            dropdown.DataSource = dropdown_ds
            dropdown.DataTextField = text
            dropdown.DataValueField = value
        Else
            dropdown.DataSource = New DataTable
        End If
        dropdown.DataBind()
        dropdown.Items.Insert(0, "--select--")
    End Sub
    Protected Sub txt_CourseID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txt_CourseID.TextChanged
        Try
            lbl_msg.Text = ""
            If ddl_Mode.SelectedValue = "M" Or ddl_Mode.SelectedValue = "H" Then
                If ddl_Semester.SelectedIndex = 0 Then
                    lbl_msg.Text = "Please select semester...!"
                    lbl_msg.CssClass = "redMessage"
                    ddl_Semester.Focus()
                    Exit Sub
                End If
                If txt_StudentID.Text = "" Then
                    lbl_msg.Text = "Please select student...!"
                    lbl_msg.CssClass = "redMessage"
                    txt_StudentID.Focus()
                    Exit Sub
                End If
            End If
            Session("AssID") = ""
            Session("ModID") = ""
            If txt_CourseID.Text <> "" Then
                getconnection()
                Session("ModID") = txt_CourseID.Text
                With objApprovalSettings
                    Dim ModName As String = .Check_ExitOrNot(ConStaff, "Courses", "CSName", "CSID", txt_CourseID.Text)
                    If ModName <> "" Then
                        '--------------- Modified on 12-Oct-2023 - As per call and chat  -----------------------------
                        If ddlCourseName.Items.IndexOf(ddlCourseName.Items.FindByValue(txt_CourseID.Text)) = -1 Then
                            ddlCourseName.SelectedIndex = 0
                        Else
                            ddlCourseName.SelectedValue = txt_CourseID.Text
                        End If
                        '---------- Added on 12-Apr-2024 -SJ- As per mail --------------
                        If ddl_Mode.SelectedValue = "P" Then
                            Dim ds, dsMod As New DataSet
                            .SemID = ddl_Semester.SelectedValue
                            ._StudentID = txt_StudentID.Text
                            .CSID = txt_CourseID.Text
                            ds = .GetRegisteredDetails(ConStaff)
                            If ds.Tables(0).Rows.Count > 0 Then
                                lbl_RegDate.Text = CType(ds.Tables(0).Rows(0)("AddDate").ToString(), DateTime).ToString("dd-MMM-yyyy")
                                lbl_Status.Text = "Registered"
                            Else 'Modified on 22-Apr-2024 -SJ- As per 22-Apr-2024 - As per call
                                ds = New DataSet
                                ds = .CheckTTSTRegCourses(ConStaff)
                                If ds.Tables(0).Rows.Count > 0 Then
                                    lbl_RegDate.Text = "NILL"
                                    lbl_Status.Text = "Not Finalized"
                                Else
                                    lbl_RegDate.Text = "NILL"
                                    lbl_Status.Text = "Not Registered"
                                End If
                            End If
                            dsMod = objApprovalSettings.getModule_PreRequisitesCourses(ConStaff)
                            DropDownBinding(ddl_PreReqCSName, dsMod, "CSName", "CsID")
                        End If
                        '----------------------------------------------------------------------------------------
                        Dim ds_Mod, ds_Ass, ds_Class As New DataSet
                        .SemID = ddl_Semester.SelectedValue
                        .CSID = txt_CourseID.Text
                        .EmpID = getEmpID()
                        If ddl_Semester.SelectedIndex <> 0 And txt_CourseID.Text <> "" Then
                            '<-------------- Bind Assignments -------------------------->
                            ds_Ass = .getAssignment(ConStaff)
                            DropDownBinding(ddlAssName, ds_Ass, "AsgName", "AsgID")
                            txt_AssignID.Text = ""
                        End If
                        txt_AssignID.Focus()
                    Else
                        lbl_msg.Text = "Invalid Course ID...!"
                        lbl_msg.CssClass = "redMessage"
                        lbl_msg.Visible = True
                        txt_CourseID.Text = ""
                        If ddlCourseName.Items.Count > 0 Then
                            ddlCourseName.SelectedIndex = 0
                        End If
                        txt_CourseID.Focus()
                        txt_AssignID.Text = ""
                        ddlAssName.DataSource = New DataTable
                        ddlAssName.DataBind()
                    End If
                End With
            Else
                ddlCourseName.SelectedIndex = 0
                txt_PreReqCSID.Text = ""
                ddl_PreReqCSName.Items.Clear()
                ddl_PreReqCSName.Items.Insert(0, "--select--")
                ddlAssName.Items.Clear()
                ddlAssName.Items.Insert(0, "--select--")
            End If
            If ddl_Mode.SelectedValue = "A" Then 'Added on 17-Oct-2023 -SJ- As  per call and chat
                '<-------------- Bind Students -------------------------->
                objApprovalSettings.SemID = ddl_Semester.SelectedValue
                objApprovalSettings.CSID = txt_CourseID.Text
                objApprovalSettings.EmpID = getEmpID()
                objApprovalSettings.AssID = txt_AssignID.Text
                objApprovalSettings.EnteredBy = Session("EmpShForms")
                objApprovalSettings._Apptype = ddl_Mode.SelectedValue 'Added on 16-Oct-2023 -S- As per call and chat
                Dim dsstud As New DataSet
                dsstud = objApprovalSettings.BindStudents(ConStaff)
                txt_StudentID.Text = ""
                DropDownBinding(ddl_StudName, dsstud, "SNAME", "SCODE")
                '---------------------------------------------------------
            End If
        Catch ex As Exception
            lbl_msg.Text = ex.Message
            lbl_msg.Visible = True
            lbl_msg.CssClass = "redMessage"
        End Try
    End Sub
    Protected Sub ddlCourseName_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlCourseName.SelectedIndexChanged
        Try
            lbl_msg.Text = ""
            Session("AssID") = ""
            Session("ModID") = ""
            If ddlCourseName.SelectedValue <> "--select--" Then
                txt_AssignID.Text = ""
                txt_CourseID.Text = ddlCourseName.SelectedValue
                Session("ModID") = txt_CourseID.Text
                With objApprovalSettings
                    Dim ds_Mod, ds_Ass, ds_Class As New DataSet
                    .SemID = ddl_Semester.SelectedValue
                    .CSID = txt_CourseID.Text
                    .EmpID = getEmpID()
                    If ddl_Semester.SelectedIndex <> 0 And txt_CourseID.Text <> "" Then
                        '<-------------- Bind Assignments -------------------------->
                        ds_Ass = .getAssignment(ConStaff)
                        DropDownBinding(ddlAssName, ds_Ass, "AsgName", "AsgID")
                        txt_AssignID.Text = ""
                    End If
                    '---------- Added on 12-Apr-2024 -SJ- As per mail --------------
                    If ddl_Mode.SelectedValue = "P" Then
                        getconnection()
                        Dim ds As New DataSet
                        .SemID = ddl_Semester.SelectedValue
                        ._StudentID = txt_StudentID.Text
                        .CSID = txt_CourseID.Text
                        ds = .GetRegisteredDetails(ConStaff)
                        If ds.Tables(0).Rows.Count > 0 Then
                            lbl_RegDate.Text = CType(ds.Tables(0).Rows(0)("AddDate").ToString(), DateTime).ToString("dd-MMM-yyyy")
                            lbl_Status.Text = "Registered"
                        Else 'Modified on 22-Apr-2024 -SJ- As per 22-Apr-2024 - As per call
                            ds = New DataSet
                            ds = .CheckTTSTRegCourses(ConStaff)
                            If ds.Tables(0).Rows.Count > 0 Then
                                lbl_RegDate.Text = "NILL"
                                lbl_Status.Text = "Not Finalized"
                            Else
                                lbl_RegDate.Text = "NILL"
                                lbl_Status.Text = "Not Registered"
                            End If
                        End If
                        ds_Mod = objApprovalSettings.getModule_PreRequisitesCourses(ConStaff)
                        DropDownBinding(ddl_PreReqCSName, ds_Mod, "CSName", "CsID")
                    End If
                    '--------------------------
                End With
            Else
                txt_CourseID.Text = ""
                txt_AssignID.Text = ""
                txt_PreReqCSID.Text = ""
                ddl_PreReqCSName.Items.Clear()
                ddl_PreReqCSName.Items.Insert(0, "--select--")
                ddlAssName.Items.Clear()
                ddlAssName.Items.Insert(0, "--select--")
            End If
            If ddl_Mode.SelectedValue = "A" Then 'Added on 16-Oct-2023 -S- As per call and chat
                '<-------------- Bind Students -------------------------->
                objApprovalSettings.SemID = ddl_Semester.SelectedValue
                objApprovalSettings.CSID = txt_CourseID.Text
                objApprovalSettings.EmpID = getEmpID()
                objApprovalSettings.AssID = txt_AssignID.Text
                objApprovalSettings.EnteredBy = Session("EmpShForms")
                objApprovalSettings._Apptype = ddl_Mode.SelectedValue 'Added on 16-Oct-2023 -S- As per call and chat
                Dim dsstud As New DataSet
                dsstud = objApprovalSettings.BindStudents(ConStaff)
                txt_StudentID.Text = ""
                DropDownBinding(ddl_StudName, dsstud, "SNAME", "SCODE")
                '---------------------------------------------------------
            End If
            ddlCourseName.Focus()
        Catch ex As Exception
            lbl_msg.Text = ex.Message
            lbl_msg.Visible = True
            lbl_msg.CssClass = "redMessage"
        End Try
    End Sub
    Protected Sub txt_AssignID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txt_AssignID.TextChanged
        Try
            lbl_msg.Text = ""
            If txt_AssignID.Text <> "" Then
                With objApprovalSettings
                    getconnection()
                    'Dim AsgName As String = .Check_ExitOrNot(ConStaff, "SemesterAsg", "AsgName", "AsgID", txt_AssignID.Text)
                    Dim AsgName As Integer = ddlAssName.Items.IndexOf(ddlAssName.Items.FindByValue(txt_AssignID.Text))
                    If AsgName <> -1 Then
                        'If AsgName <> "" Then
                        ddlAssName.SelectedValue = txt_AssignID.Text
                        txt_StudentID.Focus()
                    Else
                        lbl_msg.Text = "Invalid Assignment ID...!"
                        lbl_msg.CssClass = "redMessage"
                        lbl_msg.Visible = True
                        txt_AssignID.Text = ""
                        If ddlAssName.Items.Count > 0 Then
                            ddlAssName.SelectedIndex = 0
                        End If
                        txt_AssignID.Focus()
                    End If
                End With
            Else
                ddlAssName.SelectedIndex = 0
            End If
            If ddl_Mode.SelectedValue = "A" Then 'Added on 16-Oct-2023 -S- As per call and chat
                Session("AssID") = txt_AssignID.Text
                '<-------------- Bind Students -------------------------->
                objApprovalSettings.SemID = ddl_Semester.SelectedValue
                objApprovalSettings.CSID = txt_CourseID.Text
                objApprovalSettings.EmpID = getEmpID()
                objApprovalSettings.AssID = txt_AssignID.Text
                objApprovalSettings._Apptype = ddl_Mode.SelectedValue 'Added on 16-Oct-2023 -S- As per call and chat
                objApprovalSettings.EnteredBy = Session("EmpShForms")
                Dim dsstud As New DataSet
                dsstud = objApprovalSettings.BindStudents(ConStaff)
                txt_StudentID.Text = ""
                DropDownBinding(ddl_StudName, dsstud, "SNAME", "SCODE")
                '---------------------------------------------------------
            End If
        Catch ex As Exception
            lbl_msg.Text = ex.Message
            lbl_msg.Visible = True
            lbl_msg.CssClass = "redMessage"
        End Try
    End Sub
    Protected Sub ddlAssName_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlAssName.SelectedIndexChanged
        Try
            lbl_msg.Text = ""
            If ddlAssName.SelectedValue <> "--select--" Then
                txt_AssignID.Text = ddlAssName.SelectedValue

            Else
                txt_AssignID.Text = ""
                ddl_StudName.Items.Clear()
                ddl_StudName.Items.Insert(0, "--select--")
            End If
            If ddl_Mode.SelectedValue = "A" Then 'Added on 16-Oct-2023 -S- As per call and chat
                Session("AssID") = txt_AssignID.Text
                ddlAssName.Focus()
                '<-------------- Bind Students -------------------------->
                getconnection()
                objApprovalSettings.SemID = ddl_Semester.SelectedValue
                objApprovalSettings.CSID = txt_CourseID.Text
                objApprovalSettings.EmpID = getEmpID()
                objApprovalSettings.AssID = txt_AssignID.Text
                objApprovalSettings.EnteredBy = Session("EmpShForms")
                objApprovalSettings._Apptype = ddl_Mode.SelectedValue 'Added on 16-Oct-2023 -S- As per call and chat
                Dim dsstud As New DataSet
                dsstud = objApprovalSettings.BindStudents(ConStaff)
                txt_StudentID.Text = ""
                DropDownBinding(ddl_StudName, dsstud, "SNAME", "SCODE")
                '---------------------------------------------------------
            End If
        Catch ex As Exception
            lbl_msg.Text = ex.Message
            lbl_msg.Visible = True
            lbl_msg.CssClass = "redMessage"
        End Try
    End Sub
    Private Sub ddl_StudName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_StudName.SelectedIndexChanged
        Try
            lbl_msg.Text = ""
            If ddl_StudName.SelectedValue <> "--select--" Then
                txt_StudentID.Text = ddl_StudName.SelectedValue
                '---------- Added on 16-Oct-2023 -SJ- As per call and chat ---------------
                Session("StudentID") = txt_StudentID.Text
                If ddl_Mode.SelectedValue = "M" Or ddl_Mode.SelectedValue = "H" Then
                    txt_CourseID.Text = ""
                    ddlCourseName.Items.Clear()
                    ddlCourseName.Items.Insert(0, "--select--")
                    With objApprovalSettings
                        Dim ds_Mod As New DataSet
                        .SemID = ddl_Semester.SelectedValue
                        ._StudentID = txt_StudentID.Text
                        '<-------------- Bind Modules -------------------------->

                        getconnection()
                        ds_Mod = .getModule(ConStaff, Session("Type"))
                        DropDownBinding(ddlCourseName, ds_Mod, "CSName", "CsID")

                    End With
                ElseIf ddl_Mode.SelectedValue = "P" Then
                    Dim ds_Mod As New DataSet
                    objApprovalSettings.SemID = ddl_Semester.SelectedValue
                    getconnection()
                    ds_Mod = objApprovalSettings.getModule_RegisteredCourses(ConStaff)
                    DropDownBinding(ddlCourseName, ds_Mod, "CSName", "CsID")
                End If
                '----------------------------------------------------------------------
            Else
                txt_StudentID.Text = ""
                '---------- Added on 16-Oct-2023 -SJ- As per call and chat --------------
                Session("StudentID") = ""
                txt_CourseID.Text = ""
                ddlCourseName.Items.Clear()
                ddlCourseName.Items.Insert(0, "--select--")
                '-------------------------------------------------------------------------
            End If
            ddl_StudName.Focus()
        Catch ex As Exception
            lbl_msg.Text = ex.Message
            lbl_msg.Visible = True
            lbl_msg.CssClass = "redMessage"
        End Try
    End Sub

    Private Sub txt_StudentID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_StudentID.TextChanged
        Try
            lbl_msg.Text = ""
            If ddl_Semester.SelectedIndex = 0 Then
                lbl_msg.Text = "Please select semester...!"
                lbl_msg.CssClass = "redMessage"
                ddl_Semester.Focus()
                Exit Sub
            End If
            If txt_StudentID.Text <> "" Then
                With objApprovalSettings
                    getconnection()
                    'Dim StudCode As String = .Check_ExitOrNot(ConStaff, "Students", "SName", "SCode", txt_StudentID.Text)
                    Dim StudCode As Integer = ddl_StudName.Items.IndexOf(ddl_StudName.Items.FindByValue(txt_StudentID.Text))
                    If StudCode <> -1 Then
                        ddl_StudName.SelectedValue = txt_StudentID.Text
                        '---------- Added on 16-Oct-2023 -SJ- As per call and chat ---------------
                        Session("StudentID") = txt_StudentID.Text
                        If ddl_Mode.SelectedValue = "M" Or ddl_Mode.SelectedValue = "H" Then
                            txt_CourseID.Text = ""
                            ddlCourseName.Items.Clear()
                            ddlCourseName.Items.Insert(0, "--select--")
                            Dim ds_Mod As New DataSet
                            .SemID = ddl_Semester.SelectedValue
                            ._StudentID = txt_StudentID.Text
                            '<-------------- Bind Modules -------------------------->
                            getconnection()
                            ds_Mod = .getModule(ConStaff, Session("Type"))
                            DropDownBinding(ddlCourseName, ds_Mod, "CSName", "CsID")
                        ElseIf ddl_Mode.SelectedValue = "P" Then
                            Dim ds_Mod As New DataSet
                            .SemID = ddl_Semester.SelectedValue
                            ds_Mod = .getModule_RegisteredCourses(ConStaff)
                            DropDownBinding(ddlCourseName, ds_Mod, "CSName", "CsID")
                        End If
                        '----------------------------------------------------------------------
                        txt_StudentID.Focus()
                    Else
                        lbl_msg.Text = "Invalid Student ID...!"
                        lbl_msg.CssClass = "redMessage"
                        lbl_msg.Visible = True
                        txt_StudentID.Text = ""
                        If ddl_StudName.Items.Count > 0 Then
                            ddl_StudName.SelectedIndex = 0
                        End If
                        '---------- Added on 16-Oct-2023 -SJ- As per call and chat --------------
                        Session("StudentID") = ""
                        txt_CourseID.Text = ""
                        ddlCourseName.Items.Clear()
                        ddlCourseName.Items.Insert(0, "--select--")
                        '-------------------------------------------------------------------------
                        txt_StudentID.Focus()
                    End If
                End With
            Else
                ddl_StudName.SelectedIndex = 0
                '---------- Added on 16-Oct-2023 -SJ- As per call and chat --------------
                Session("StudentID") = ""
                txt_CourseID.Text = ""
                ddlCourseName.Items.Clear()
                ddlCourseName.Items.Insert(0, "--select--")
                '-------------------------------------------------------------------------
            End If
        Catch ex As Exception
            lbl_msg.Text = ex.Message
            lbl_msg.Visible = True
            lbl_msg.CssClass = "redMessage"
        End Try
    End Sub
    Sub ClearAll()
        Bind_DropDown()
        Session("SMID") = ""
        Session("ModID") = ""
        Session("AssID") = ""
        Session("StudentID") = "" 'Added on 16-Oct-2023 -SJ- As per call and chat
        '------ Added on 06-Oct-2020 - As per SSA M 1046 ----------
        Session("FacultyID") = ""
        Session("AppType") = ddl_Mode.SelectedValue
        If ddl_Mode.SelectedValue <> "P" Then 'Added on 31-Jan-2024 -SJ- As per SFR C 777
            txt_Expired.Text = CType(Now.Date, DateTime).AddDays(2).ToString("dd-MMM-yyyy")
        End If
        '----------------------------------------------------------
        txt_Comments.Text = ""
        txt_FacultyID.Text = ""
        txt_FacultyName.Text = ""
        ddl_Semester.SelectedIndex = 0
        txt_FacultyID.Text = ""
        txt_FacultyName.Text = ""
        txt_CourseID.Text = ""
        txt_AssignID.Text = ""
        txt_StudentID.Text = ""
        txt_Comments.Text = ""
        'ddl_Mode.SelectedIndex = 0
        ddl_Mode.SelectedValue = Request.QueryString("AppType")
        ddl_StudName.DataSource = New DataTable
        ddl_StudName.DataBind()
        ddlAssName.DataSource = New DataTable
        ddlAssName.DataBind()
        ddlCourseName.DataSource = New DataTable
        ddlCourseName.DataBind()

        txt_SFacultyID.Text = ""
        txt_SFacultyName.Text = ""
        txt_SStudentID.Text = ""
        txt_SStudentName.Text = ""
        If TabContainer2.ActiveTabIndex = 0 Then
            ddl_Semester.Focus()
        ElseIf TabContainer2.ActiveTabIndex = 1 Then
            txt_SFacultyID.Focus()
        End If
        enabledisbale(True)
        btn_Save.CssClass = "button"
        '---------Added on 26-Feb-2024 -SJ- As per call ----------
        Session("Documentexists") = 0
        txt_SMID.Text = ""
        txt_SMName.Text = ""
        If ddl_Mode.SelectedValue = "P" Then
            gdv_ApprovalSettings.HeaderRow.Cells(3).CssClass = "confirm"
            gdv_ApprovalSettings.HeaderRow.Cells(4).CssClass = "confirm"
            btn_ViewAttachments.Visible = True
            btn_ViewAttachments.Enabled = False
            btn_ViewAttachments.CssClass = "buttondis"
            '------------ Added on 12-Apr-2024 -SJ- As per mail ---------------
            txt_PreReqCSID.Text = ""
            ddl_PreReqCSName.DataSource = New DataTable
            ddl_PreReqCSName.DataBind()
            lbl_RegDate.Text = ""
            lbl_Status.Text = ""
            '---------- Added on 11-Apr-2024 -SJ- As per mail -------------
            gdv_ApprovalSettings.HeaderRow.Cells(0).CssClass = "confirm"
            gdv_ApprovalSettings.HeaderRow.Cells(5).Text = "Course to be Registered"
            gdv_ApprovalSettings.HeaderRow.Cells(6).Text = "Registered Course Name"
            gdv_ApprovalSettings.HeaderRow.Cells(7).CssClass = ""
            gdv_ApprovalSettings.HeaderRow.Cells(8).CssClass = ""
            gdv_ApprovalSettings.HeaderRow.Cells(12).CssClass = ""
            gdv_ApprovalSettings.HeaderRow.Cells(13).CssClass = ""
            '---------------------
            BindData_ApprovalDetails()
        Else
            gdv_ApprovalSettings.HeaderRow.Cells(3).CssClass = ""
            gdv_ApprovalSettings.HeaderRow.Cells(4).CssClass = ""
            btn_ViewAttachments.Visible = False
            '---------- Added on 11-Apr-2024 -SJ- As per mail -------------
            gdv_ApprovalSettings.HeaderRow.Cells(0).CssClass = "confirm"
            gdv_ApprovalSettings.HeaderRow.Cells(5).CssClass = "confirm"
            gdv_ApprovalSettings.HeaderRow.Cells(6).Text = "Course"
            gdv_ApprovalSettings.HeaderRow.Cells(7).CssClass = "confirm"
            gdv_ApprovalSettings.HeaderRow.Cells(8).CssClass = "confirm"
            gdv_ApprovalSettings.HeaderRow.Cells(12).CssClass = "confirm"
            gdv_ApprovalSettings.HeaderRow.Cells(13).CssClass = "confirm"
            '------------
            BindData_ApprovalDetails()
        End If
        '-------------------------
    End Sub
    '--------------- Added on 16-Oct-2023 -SJ- As per call ----------------
    Private Sub Logs(ByVal cmd As SqlCommand)
        With objApprovalSettings
            .logDate = CType(Now.Date, DateTime).ToString("dd-MMM-yyyy")
            .LogTime = Now.ToShortTimeString()
            ._UserName = Session("Scode")
            .CSID = txt_CourseID.Text
            .SemID = ""
            .Co = Session("CompID")
            If ddl_Mode.SelectedValue = "M" Then
                .Comments = "Mark correction After Result Pulished:- Student:" & txt_StudentID.Text & " and Course: " & txt_CourseID.Text & " and Expired on: " & CType(txt_Expired.Text, DateTime).ToString("dd-MMM-yyyy")
                .DocType = "MC"
            ElseIf ddl_Mode.SelectedValue = "H" Then 'Added on 11-Dec-2023 -SJ- As per chat 
                .Comments = "Attendance Approval for HallTicket:- Student:" & txt_StudentID.Text & " and Course: " & txt_CourseID.Text
                .DocType = "HT"
            Else
                .Comments = "Attendance Approved:- Faculty: " & txt_FacultyID.Text & "Course: " & txt_CourseID.Text & " and Expired on: " & CType(txt_Expired.Text, DateTime).ToString("dd-MMM-yyyy")
                .DocType = "AT"
            End If
            .Computer = Server.MachineName
            .DivID = "E"
            ._StudentID = txt_StudentID.Text
            .SemID = ddl_Semester.SelectedValue
            Dim ins As Integer = .Insertion_Logs(cmd)
        End With
    End Sub
    '--------------------------------------------------------------------------------------
    Private Sub btn_Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Save.Click
        lbl_msg.Text = ""
        If ddl_Mode.SelectedIndex = 0 Then
            lbl_msg.Text = "Please select approval type...!"
            lbl_msg.CssClass = "redMessage"
            ddl_Mode.Focus()
            Exit Sub
        End If
        If ddl_Semester.SelectedIndex = 0 Then
            lbl_msg.Text = "Please select semester...!"
            lbl_msg.CssClass = "redMessage"
            ddl_Semester.Focus()
            Exit Sub
        End If
        If ddl_Mode.SelectedValue = "A" Then 'Added on 16-Oct-2023 -SJ- As per call
            If txt_FacultyID.Text = "" Then
                lbl_msg.Text = "Please select faculty...!"
                lbl_msg.CssClass = "redMessage"
                txt_FacultyID.Focus()
                Exit Sub
            End If
        End If
        If txt_StudentID.Text = "" Then
            If ddl_Mode.SelectedValue <> "A" Then
                lbl_msg.Text = "Please select student...!"
                lbl_msg.CssClass = "redMessage"
                txt_StudentID.Focus()
                Exit Sub
            End If

        End If
        If txt_CourseID.Text = "" Then
            lbl_msg.Text = "Please select course...!"
            lbl_msg.CssClass = "redMessage"
            txt_CourseID.Focus()
            Exit Sub
        End If
        'If txt_AssignID.Text = "" Then
        '    lbl_msg.Text = "Please select assignment...!"
        '    lbl_msg.CssClass = "redMessage"
        '    txt_AssignID.Focus()
        '    Exit Sub
        'End If
        If ddl_Mode.SelectedValue = "P" Then
            If IsDate(txt_Expired.Text) Then
                txt_Expired.Text = CType(txt_Expired.Text, DateTime).ToString("dd-MMM-yyyy")
            Else
                lbl_msg.Text = "Invalid Expired On...!"
                lbl_msg.CssClass = "redMessage"
                txt_Expired.Focus()
                Exit Sub
            End If
            Dim startdate As DateTime = CType(Now.Date, DateTime)
            Dim expirydate As DateTime = txt_Expired.Text
            If startdate > expirydate Then
                lbl_msg.Text = "The date expired from the date of approval entry...!"
                lbl_msg.CssClass = "redMessage"
                txt_Expired.Focus()
                Exit Sub
            End If
        ElseIf ddl_Mode.SelectedValue <> "H" Then 'Addd on 13-Dec-2023 -SJ- As per chat
            If txt_Expired.Text = "" Then
                lbl_msg.Text = "Please enter expired on...!"
                lbl_msg.CssClass = "redMessage"
                txt_Expired.Focus()
                Exit Sub
            End If
            If IsDate(txt_Expired.Text) Then
                txt_Expired.Text = CType(txt_Expired.Text, DateTime).ToString("dd-MMM-yyyy")
            Else
                lbl_msg.Text = "Invalid from date...!"
                lbl_msg.CssClass = "redMessage"
                txt_Expired.Focus()
                Exit Sub
            End If
            Dim startdate As DateTime = CType(Now.Date, DateTime)
            Dim enddate As DateTime = CType(Now.Date, DateTime).AddDays(2)
            Dim expirydate As DateTime = txt_Expired.Text
            If expirydate > startdate And expirydate <= enddate Then
            Else
                lbl_msg.Text = "The date expired should be two days from the date of approval entry...!"
                lbl_msg.CssClass = "redMessage"
                txt_Expired.Focus()
                Exit Sub
            End If
        End If

        Dim insertstat As Integer = 0
        Dim updstat As Integer = 0
        getconnection()
        Dim cmd As New SqlCommand()
        Dim trans As SqlTransaction
        If ConStaff.State = ConnectionState.Closed Then ConStaff.Open()
        trans = ConStaff.BeginTransaction
        cmd.Connection = ConStaff
        cmd.Transaction = trans
        Try
            With objApprovalSettings
                .DocDate = CType(Now.Date, DateTime).ToString("dd-MMM-yyyy")
                .EmpID = txt_FacultyID.Text
                .CSID = txt_CourseID.Text
                .SemID = ddl_Semester.SelectedValue
                ._Apptype = ddl_Mode.SelectedValue
                ._StudentID = txt_StudentID.Text
                If ddl_Mode.SelectedValue = "H" Then 'Added on 13-Dec-2023 -SJ- As per chat
                    .ExpiredOn = "01-Jan-1900"
                Else
                    .ExpiredOn = CType(txt_Expired.Text, DateTime).ToString("dd-MMM-yyyy")
                End If

                .Comments = txt_Comments.Text
                .Applied = False
                .Createdon = CType(Now.Date, DateTime).ToString("dd-MMM-yyyy")
                .Createdby = Session("Scode")
                .createdPC = Server.MachineName
                .AssID = txt_AssignID.Text
                '---------Added on 12-Apr-2024 -SJ- As per mail------------
                .PreCourseID = txt_PreReqCSID.Text
                If lbl_RegDate.Text = "NILL" Then
                    .RegDate = "01-Jan-1900"
                Else
                    .RegDate = lbl_RegDate.Text
                End If
                If ddl_Mode.SelectedValue = "P" Then
                    '----------- Modified on 22-Apr-2024 -SJ- As per call -------------
                    If lbl_Status.Text = "Registered" Then
                        .Status = "R"
                    ElseIf lbl_Status.Text = "Not Finalized" Then
                        .Status = "NF"
                    ElseIf lbl_Status.Text = "Not Registered" Then
                        .Status = "NR"
                    Else
                        .Status = ""
                    End If
                    '-------
                Else
                    .Status = ""
                End If
                '----------------
                insertstat = .Insert_AttApprovedTeachers(cmd)
                '------------- Commented on 16-Oct-2023 -SJ- As per call ------------------------------
                'Dim asgnment As String = ""
                'If txt_AssignID.Text <> "" Then
                '    asgnment = " and AsgID='" & txt_AssignID.Text & "'"
                'End If
                'If ddl_Mode.SelectedValue = "M" Then
                '    cmd.CommandText = "Update StMarks set approved =0, Cancelledon = '" & Now & "', AppDt = '', Appcompname ='', Approvedby = '' WHERE Smid = '" & ddl_Semester.SelectedValue & "' " & _
                '    " and CsID = '" & txt_CourseID.Text & "' and scode = '" & txt_StudentID.Text & "' " & asgnment & ""
                '    updstat = cmd.ExecuteNonQuery

                '    Dim ds As New DataSet
                '    Dim adpt As New SqlDataAdapter
                '    cmd.CommandText = "SELECT * from STmarks where  Smid = '" & ddl_Semester.SelectedValue & "'" & _
                '    " and CsID = '" & txt_CourseID.Text & "' and scode = '" & txt_StudentID.Text & "' " & asgnment & ""

                '    adpt.SelectCommand = cmd
                '    adpt.Fill(ds)
                '    If ds.Tables(0).Rows.Count > 0 Then
                '        For i = 0 To ds.Tables(0).Rows.Count - 1
                '            Dim PrgID = "UFDN"
                '            Dim smidN = ddl_Semester.SelectedValue
                '            Dim csidN = txt_CourseID.Text
                '            Dim AsgID = ds.Tables(0).Rows(i).Item("AsgID").ToString
                '            Dim scodeN = txt_StudentID.Text
                '            Dim MarksN = ds.Tables(0).Rows(i).Item("Marks").ToString
                '            Dim attemptno = ds.Tables(0).Rows(i).Item("attemptno").ToString
                '            Dim Status = ds.Tables(0).Rows(i).Item("attemptno").ToString
                '            Dim Remarks = ds.Tables(0).Rows(i).Item("attemptno").ToString
                '            Dim DivID = Session("Division")
                '            Dim Approved = 0
                '            Dim ApprovedBy = ""
                '            Dim AppCompName = ""
                '            Dim AppDt = ""
                '            Dim CancelledOn = Now
                '            Dim Cancelledby = Session("Scode")
                '            Dim AppCancelRemarks = "Cancelled Approval from Authorization for Marks modification "


                '            cmd.CommandText = "insert into STmarkslog (PrgID,smid,csid,AsgID,scode,Marks,attemptno,Status,Remarks,DivID,Approved,ApprovedBy,AppCompName,AppDt,AppCancelRemarks,CancelledOn,Cancelledby)" & _
                '                "values('" & PrgID & "','" & smidN & "','" & csidN & "','" & AsgID & "','" & scodeN & "','" & MarksN & "','" & attemptno & "','" & Status & "'," & _
                '            "'" & Remarks & "','" & DivID & "'," & Approved & ",'" & ApprovedBy & "','" & AppCompName & "','" & AppDt & "','" & AppCancelRemarks & "'," & _
                '            "'" & CancelledOn & "','" & Cancelledby & "')"
                '            cmd.ExecuteNonQuery()
                '        Next
                '    End If
                'End If
                If insertstat > 0 Then
                    '---------- Added on 26-Feb-2024 -SJ- As per mail  and chat ------------------
                    Dim Docid As String = objApprovalSettings.GetDocID(cmd)
                    save_DocumentDetails(cmd, Docid)
                    CopyFiles_ToExemptPreReqFolder(Docid)
                    Delete_directeryfromSystem(Server.MapPath("~/TempDOC/"), txt_StudentID.Text)
                    '-------------------------------------------
                    Logs(cmd) 'Added on 16-Oct-2023 -SJ- As per Call
                    'lbl_msg.Text = "Saved successfully...!"
                    'lbl_msg.CssClass = "greenMessage"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "script", "alert('Saved successfully...!');", True)
                    ClearAll()
                End If
            End With
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            lbl_msg.Text = ex.Message.ToString
            lbl_msg.CssClass = "redMessage"
            lbl_msg.Visible = True
            insertstat = 0
        Finally
            ConStaff.Close()
        End Try

    End Sub
    Private Sub txt_Expired_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_Expired.TextChanged
        Try
            If IsDate(txt_Expired.Text) Then
                txt_Expired.Text = CType(txt_Expired.Text, DateTime).ToString("dd-MMM-yyyy")
                txt_Expired.Focus()
            Else
                lbl_msg.Text = "Invalid from date...!"
                lbl_msg.CssClass = "redMessage"
                txt_Expired.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lbl_msg.Text = e.ToString
            lbl_msg.CssClass = "redMessage"
        End Try
    End Sub

    Private Sub txt_FacultyID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_FacultyID.TextChanged
        Try
            lbl_msg.Text = ""
            If txt_FacultyID.Text <> "" Then
                With objApprovalSettings
                    getconnection()
                    Dim FacultyID As String = .Check_ExitOrNot(ConStaff, "employees", "EmpName", "EmpID", txt_FacultyID.Text)
                    If FacultyID = "" Then
                        lbl_msg.Text = "Invalid faculty id...!"
                        lbl_msg.CssClass = "redMessage"
                        lbl_msg.Visible = True
                        txt_FacultyID.Text = ""
                        txt_FacultyName.Text = ""
                        txt_FacultyID.Focus()
                    Else
                        Session("FacultyID") = txt_FacultyID.Text 'Added on 06-Oct-2020 - As per SSA M 1046
                        Call ddl_Semester_SelectedIndexChanged(ddl_Semester, New EventArgs)
                        txt_CourseID.Focus()
                    End If
                End With
            Else
                txt_FacultyID.Text = ""
                txt_FacultyName.Text = ""
                '------- Added on 06-Oct-2020 - As per SSA M 1046 ----------------------
                With objApprovalSettings
                    getconnection()
                    Dim ds_Mod As New DataSet
                    .SemID = ddl_Semester.SelectedValue
                    .EmpID = getEmpID()
                    If ddl_Mode.SelectedValue = "M" Then
                        ds_Mod = .getModule(ConStaff, Session("Type"))
                        DropDownBinding(ddlCourseName, ds_Mod, "CSName", "CsID")
                    Else
                        ds_Mod = .getModule_AttendanceApproval(ConStaff, Session("Type"))
                        DropDownBinding(ddlCourseName, ds_Mod, "CSName", "CsID")
                    End If
                    txt_CourseID.Text = ""
                End With
                '----------------------------------------------------------------------
                txt_FacultyID.Focus()
            End If
        Catch ex As Exception
            lbl_msg.Text = ex.Message
            lbl_msg.Visible = True
            lbl_msg.CssClass = "redMessage"
        End Try
    End Sub

    Private Sub txt_FacultyName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_FacultyName.TextChanged
        Try
            lbl_msg.Text = ""
            If txt_FacultyName.Text <> "" Then
                With objApprovalSettings
                    getconnection()
                    Dim FacultyID As String = .Check_ExitOrNot(ConStaff, "employees", "EmpID", "EmpName", txt_FacultyName.Text)
                    If FacultyID = "" Then
                        lbl_msg.Text = "Invalid faculty name...!"
                        lbl_msg.CssClass = "redMessage"
                        lbl_msg.Visible = True
                        txt_FacultyID.Text = ""
                        txt_FacultyName.Text = ""
                        txt_FacultyName.Focus()
                    Else
                        Session("FacultyID") = txt_FacultyID.Text 'Added on 06-Oct-2020 - As per SSA M 1046
                        Call ddl_Semester_SelectedIndexChanged(ddl_Semester, New EventArgs)
                        txt_CourseID.Focus()
                    End If
                End With
            Else
                txt_FacultyID.Text = ""
                txt_FacultyName.Text = ""
                '------- Added on 06-Oct-2020 - As per SSA M 1046 ----------------------
                With objApprovalSettings
                    getconnection()
                    Dim ds_Mod As New DataSet
                    .SemID = ddl_Semester.SelectedValue
                    .EmpID = getEmpID()
                    If ddl_Mode.SelectedValue = "M" Then
                        ds_Mod = .getModule(ConStaff, Session("Type"))
                        DropDownBinding(ddlCourseName, ds_Mod, "CSName", "CsID")
                    Else
                        ds_Mod = .getModule_AttendanceApproval(ConStaff, Session("Type"))
                        DropDownBinding(ddlCourseName, ds_Mod, "CSName", "CsID")
                    End If
                    txt_CourseID.Text = ""
                End With
                '----------------------------------------------------------------------
                txt_FacultyName.Focus()
            End If
        Catch ex As Exception
            lbl_msg.Text = ex.Message
            lbl_msg.Visible = True
            lbl_msg.CssClass = "redMessage"
        End Try
    End Sub

    Private Sub TabContainer2_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabContainer2.ActiveTabChanged
        lbl_msg.Text = ""
        ClearAll()
    End Sub

    Private Sub btn_ClearS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_ClearS.Click
        lbl_msg.Text = ""
        ClearAll()
    End Sub

    Private Sub btn_Print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Print.Click
        lbl_msg.Text = ""
        Try
            If ddl_Mode.SelectedIndex <> 0 Then
                If ddl_Mode.SelectedValue = "M" Then
                    hdn_Mode.Value = "M"
                ElseIf ddl_Mode.SelectedValue = "H" Then 'Added on 11-Dec-2023 -SJ- As per chat
                    hdn_Mode.Value = "H"
                ElseIf ddl_Mode.SelectedValue = "P" Then 'Added on 30-Jaan-2024 -SJ- As per SFR C 777
                    hdn_Mode.Value = "P"
                Else
                    hdn_Mode.Value = "A"
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Report", "Print_Report()", True)
            Else
                lbl_msg.Text = "Please select approve mode...!"
                lbl_msg.CssClass = "redMessage"
                ddl_Mode.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lbl_msg.Text = ex.ToString
            lbl_msg.CssClass = "redMessage"
        End Try
    End Sub

    Private Sub gdv_ApprovalSettings_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gdv_ApprovalSettings.PageIndexChanging
        gdv_ApprovalSettings.PageIndex = e.NewPageIndex
        gdv_ApprovalSettings.DataSource = ViewState("CurrentState")
        gdv_ApprovalSettings.DataBind()
    End Sub

    Private Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        lbl_msg.Text = ""
        ClearAll()
    End Sub
    '--------------Added on 22-Jan-2020 as per mail---------------
    Private Sub btn_Confirm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Confirm.Click
        lbl_msg.Text = ""
        If Hdn_Value.Value = "GridSelect" Then

            Dim index As Integer = hdn_index.Value
            gdv_ApprovalSettings.SelectedIndex = index
            Dim ds As New DataSet
            getconnection()
            ds = objApprovalSettings.getAttApprovedTeachersDetails(ConStaff, gdv_ApprovalSettings.Rows(index).Cells(0).Text)
            Session("SMID") = ""
            Session("ModID") = ""
            Session("AssID") = ""
            Session("AppType") = ddl_Mode.SelectedValue
            If ds.Tables(0).Rows.Count > 0 Then
                TabContainer2.ActiveTabIndex = 0

                'Bind_DropDown()
                'ddl_Semester.SelectedValue = ds.Tables(0).Rows(0).Item("SMID").ToString
                'ddl_Semester_SelectedIndexChanged(sender, e)

                'txt_Expired.Text = CType(ds.Tables(0).Rows(0).Item("ExpiredON"), DateTime).ToString("dd-MMM-yyyy")

                'txt_FacultyID.Text = ds.Tables(0).Rows(0).Item("EmpID").ToString
                'txt_FacultyName.Text = ds.Tables(0).Rows(0).Item("EmpName").ToString

                'txt_CourseID.Text = ds.Tables(0).Rows(0).Item("CSID").ToString
                'ddlCourseName.SelectedValue = ds.Tables(0).Rows(0).Item("CSID").ToString
                'ddlCourseName_SelectedIndexChanged(sender, e)


                'If ds.Tables(0).Rows(0).Item("AsgID").ToString <> "" Then
                '    txt_AssignID.Text = ds.Tables(0).Rows(0).Item("AsgID").ToString
                '    ddlAssName.SelectedValue = ds.Tables(0).Rows(0).Item("AsgID").ToString
                '    Session("AssID") = txt_AssignID.Text
                'Else
                '    txt_AssignID.Text = ""
                '    If ddlAssName.Items.Count > 0 Then
                '        ddlAssName.SelectedIndex = 0
                '    End If
                'End If
                'ddlAssName_SelectedIndexChanged(sender, e)

                'txt_StudentID.Text = ds.Tables(0).Rows(0).Item("SCode").ToString
                'ddl_StudName.SelectedValue = ds.Tables(0).Rows(0).Item("SCode").ToString

                hdn_DocID.Value = ds.Tables(0).Rows(0).Item("DocID").ToString() 'Added on 26-Feb-2023 -SJ- As per mail and chat
                ddl_Semester.Items.Clear()
                Dim SemName As String = ds.Tables(0).Rows(0).Item("SemName").ToString
                Dim SemNameList As ListItem
                SemNameList = New ListItem(SemName, ds.Tables(0).Rows(0).Item("SMID").ToString)
                ddl_Semester.Items.Add(SemNameList)

                txt_FacultyID.Text = ds.Tables(0).Rows(0).Item("EmpID").ToString
                txt_FacultyName.Text = ds.Tables(0).Rows(0).Item("EmpName").ToString

                txt_CourseID.Text = ds.Tables(0).Rows(0).Item("CSID").ToString
                ddlCourseName.Items.Clear()
                Dim CSNameList As ListItem
                CSNameList = New ListItem(ds.Tables(0).Rows(0).Item("CSName").ToString, ds.Tables(0).Rows(0).Item("CSID").ToString)
                ddlCourseName.Items.Add(CSNameList)

                If ds.Tables(0).Rows(0).Item("AsgID").ToString <> "" Then
                    txt_AssignID.Text = ds.Tables(0).Rows(0).Item("AsgID").ToString
                    ddlAssName.Items.Clear()
                    Dim ASgNameList As ListItem
                    ASgNameList = New ListItem(ds.Tables(0).Rows(0).Item("AsgName").ToString, ds.Tables(0).Rows(0).Item("AsgID").ToString)
                    ddlAssName.Items.Add(ASgNameList)
                Else
                    txt_AssignID.Text = ""
                    ddlAssName.Items.Clear()
                End If

                txt_StudentID.Text = ds.Tables(0).Rows(0).Item("SCode").ToString
                ddl_StudName.Items.Clear()
                Dim SNameList As ListItem
                SNameList = New ListItem(ds.Tables(0).Rows(0).Item("SName").ToString, ds.Tables(0).Rows(0).Item("SCode").ToString)
                ddl_StudName.Items.Add(SNameList)

                txt_Comments.Text = ds.Tables(0).Rows(0).Item("Comments").ToString
                txt_Expired.Text = CType(ds.Tables(0).Rows(0).Item("ExpiredON").ToString(), DateTime).ToString("dd-MMM-yyyy")
                '------------ Added on 12-Apr-2024 -SJ- As per mail ---------------------
                txt_PreReqCSID.Text = ds.Tables(0).Rows(0).Item("PreCourseID").ToString()
                ddl_PreReqCSName.Items.Clear()
                Dim PreReqCSNameList As ListItem
                PreReqCSNameList = New ListItem(ds.Tables(0).Rows(0).Item("PreReqCSName").ToString, ds.Tables(0).Rows(0).Item("PreCourseID").ToString)
                ddl_PreReqCSName.Items.Add(PreReqCSNameList)
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Regdate")) Then
                    If CType(ds.Tables(0).Rows(0).Item("Regdate").ToString(), DateTime).ToString("dd-MMM-yyyy") <> "01-Jan-1900" Then
                        lbl_RegDate.Text = CType(ds.Tables(0).Rows(0).Item("Regdate").ToString(), DateTime).ToString("dd-MMM-yyyy")
                    Else
                        lbl_RegDate.Text = "NILL"
                    End If
                Else
                    lbl_RegDate.Text = "NILL"
                End If
                '----------- Modified on 22-Apr-2024 -SJ- As per call -------------
                If ds.Tables(0).Rows(0).Item("Status").ToString() = "R" Then
                    lbl_Status.Text = "Registered"
                ElseIf ds.Tables(0).Rows(0).Item("Status").ToString() = "NF" Then
                    lbl_Status.Text = "Not Finalized"
                ElseIf ds.Tables(0).Rows(0).Item("Status").ToString() = "NR" Then
                    lbl_Status.Text = "Not Registered"
                Else
                    lbl_Status.Text = ""
                End If
                '----------------------------------
                enabledisbale(False)
                '--------- Added on 26-Feb-2024 -SJ- As per mail
                txt_StudentID.Enabled = False
                ddl_StudName.Enabled = False
                FileUploaDoc.Enabled = False
                btn_UploadDoc.Enabled = False
                '------------------------------
                btn_Save.CssClass = "buttondis"
                gdv_ApprovalSettings.DataSource = New DataTable
                gdv_ApprovalSettings.DataBind()
                '------ Added on 26-Feb-2024 -SJ- As per mail and chat -------------
                If ddl_Mode.SelectedValue = "P" Then
                    btn_ViewAttachments.Visible = True
                    btn_ViewAttachments.Enabled = True
                    btn_ViewAttachments.CssClass = "button"
                Else
                    btn_ViewAttachments.Visible = False
                End If

                '--------------------------------
            Else
                ClearAll()
            End If
        End If
    End Sub

    Private Sub gdv_ApprovalSettings_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdv_ApprovalSettings.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            '------------ Added on 26-Feb-2024 -SJ- As per mail ------------------
            If ddl_Mode.SelectedValue = "P" Then
                e.Row.Cells(3).CssClass = "confirm"
                e.Row.Cells(4).CssClass = "confirm"
                gdv_ApprovalSettings.HeaderRow.Cells(3).CssClass = "confirm"
                gdv_ApprovalSettings.HeaderRow.Cells(4).CssClass = "confirm"
                '---------- Added on 11-Apr-2024 -SJ- As per mail -------------
                e.Row.Cells(0).CssClass = "confirm"
                gdv_ApprovalSettings.HeaderRow.Cells(0).CssClass = "confirm"
                gdv_ApprovalSettings.HeaderRow.Cells(5).Text = "Course to be Registered"
                gdv_ApprovalSettings.HeaderRow.Cells(6).Text = "Registered Course Name"
                '---------- Added om 01-Apr-2024 -SJ- As per chat -------------
                e.Row.Cells(14).CssClass = ""
                gdv_ApprovalSettings.HeaderRow.Cells(14).CssClass = ""
                '---------- Added on 11-Apr-2024 -SJ- As per mail -------------
                If e.Row.Cells(12).Text = "01-Jan-1900" Then
                    e.Row.Cells(12).Text = "NILL"
                End If
                If e.Row.Cells(13).Text = "Registered" Then
                    e.Row.Cells(14).Visible = False
                Else
                    e.Row.Cells(14).Visible = True
                    For Each button As LinkButton In e.Row.Cells(14).Controls.OfType(Of LinkButton)()
                        If button.CommandName = "Delete" Then
                            button.Attributes("onclick") = "if(!confirm('Do you want to delete?')){ return false; };"
                        End If
                    Next
                End If
                '----------------
            Else
                gdv_ApprovalSettings.HeaderRow.Cells(3).CssClass = ""
                e.Row.Cells(3).CssClass = ""
                e.Row.Cells(4).CssClass = ""
                '---------- Added on 11-Apr-2024 -SJ- As per mail -------------
                e.Row.Cells(0).CssClass = ""
                gdv_ApprovalSettings.HeaderRow.Cells(0).CssClass = ""
                e.Row.Cells(5).CssClass = "confirm"
                gdv_ApprovalSettings.HeaderRow.Cells(5).CssClass = "confirm"
                gdv_ApprovalSettings.HeaderRow.Cells(6).Text = "Course"
                e.Row.Cells(7).CssClass = "confirm"
                gdv_ApprovalSettings.HeaderRow.Cells(7).CssClass = "confirm"
                e.Row.Cells(8).CssClass = "confirm"
                gdv_ApprovalSettings.HeaderRow.Cells(8).CssClass = "confirm"
                e.Row.Cells(12).CssClass = "confirm"
                gdv_ApprovalSettings.HeaderRow.Cells(12).CssClass = "confirm"
                e.Row.Cells(13).CssClass = "confirm"
                gdv_ApprovalSettings.HeaderRow.Cells(13).CssClass = "confirm"
                '---------- Added om 01-Apr-2024 -SJ- As per chat -------------
                e.Row.Cells(14).CssClass = "confirm"
                gdv_ApprovalSettings.HeaderRow.Cells(14).CssClass = "confirm"
                '------------
            End If
            '---------------------
            Dim strArgumentACode As String = e.Row.RowIndex
            e.Row.Attributes.Add("ondblclick", "ShowDetails(" & strArgumentACode & ");")
        End If
    End Sub
    Sub enabledisbale(ByVal endis As Boolean)
        ddl_Semester.Enabled = endis
        If ddl_Mode.SelectedValue = "P" Then 'Added on 31-Jan-2024 -SJ- As per SFR C 777
            txt_Expired.Enabled = False
            '-------- Added on 26-Feb-2024 -SJ- As per mail ------------
            lbl_Semester.Visible = True
            txt_SMID.Visible = True
            txt_SMName.Visible = True
            lbl_AttachDoc.Visible = True
            FileUploaDoc.Visible = True
            btn_UploadDoc.Visible = True
            FileUploaDoc.Enabled = True
            btn_UploadDoc.Enabled = True
            '---------Added on 12-Apr-2024 -SJ- As per mail----------
            txt_PreReqCSID.Enabled = True
            ddl_PreReqCSName.Enabled = True
            '------------------
        Else
            txt_Expired.Enabled = endis
            '-------- Added on 26-Feb-2024 -SJ- As per mail ------------
            lbl_Semester.Visible = False
            txt_SMID.Visible = False
            txt_SMName.Visible = False
            lbl_AttachDoc.Visible = False
            FileUploaDoc.Visible = False
            btn_UploadDoc.Visible = False
            '---------Added on 12-Apr-2024 -SJ- As per mail----------
            txt_PreReqCSID.Enabled = False
            ddl_PreReqCSName.Enabled = False
            '------------
        End If
        lbl_ExpiredOn.Visible = endis
        If ddl_Mode.SelectedValue = "M" Or ddl_Mode.SelectedValue = "H" Or ddl_Mode.SelectedValue = "P" Then 'Added on 16-Oct-2023 -SJ- As per call; Added on 30-Jan-2024 -SJ- As per SFR C 777 - AppType=P
            lbl_AppFaculty.Visible = False
            txt_FacultyID.Visible = False
            txt_FacultyName.Visible = False
            lblAssignmentID.Visible = False
            txt_AssignID.Visible = False
            ddlAssName.Visible = False
            lbl_SFaculty.Visible = False
            txt_SFacultyID.Visible = False
            txt_SFacultyName.Visible = False
            If ddl_Mode.SelectedValue = "H" Then 'Added on 13-Dec-2023 -SJ- As per chat
                lbl_ExpiredOn.Visible = False
                txt_Expired.Visible = False
            Else
                lbl_ExpiredOn.Visible = True
                txt_Expired.Visible = True
            End If
        Else
            txt_FacultyID.Enabled = endis
            txt_FacultyName.Enabled = endis
            txt_AssignID.Enabled = endis
            ddlAssName.Enabled = endis
        End If
        txt_CourseID.Enabled = endis
        ddlCourseName.Enabled = endis
        txt_StudentID.Enabled = endis
        ddl_StudName.Enabled = endis
        txt_Comments.Enabled = endis
        btn_Save.Enabled = endis
        '---------Added on 12-Apr-2024 -SJ- As per mail----------
        txt_PreReqCSID.Enabled = endis
        ddl_PreReqCSName.Enabled = endis
        '------
        If ddl_Mode.SelectedValue = "A" Then
            txt_AssignID.Enabled = False
            ddlAssName.Enabled = False
            txt_StudentID.Enabled = False
            ddl_StudName.Enabled = False
        Else
            txt_AssignID.Enabled = True
            ddlAssName.Enabled = True
            txt_StudentID.Enabled = True
            ddl_StudName.Enabled = True
        End If
    End Sub
    '---------- Added on 17-Oct-2023 -SJ- As per call and chat --------------
    Protected Sub txt_SStudentID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txt_SStudentID.TextChanged
        Try
            lbl_msg.Text = ""
            If txt_SStudentID.Text <> "" Then
                With objApprovalSettings
                    getconnection()
                    Dim StudCode As String = .Check_ExitOrNot(ConStaff, "Students", "Scode", "Scode", txt_SStudentID.Text)
                    If StudCode <> "" Then
                        btn_Search.Focus()
                    Else
                        lbl_msg.Text = "Invalid Student ID...!"
                        lbl_msg.CssClass = "redMessage"
                        lbl_msg.Visible = True
                        txt_SStudentID.Text = ""
                        txt_SStudentName.Text = ""
                        txt_SStudentID.Focus()
                    End If
                End With
            Else
                txt_SStudentName.Text = ""
            End If
        Catch ex As Exception
            lbl_msg.Text = ex.Message
            lbl_msg.Visible = True
            lbl_msg.CssClass = "redMessage"
        End Try
    End Sub

    Protected Sub txt_SStudentName_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txt_SStudentName.TextChanged
        Try
            lbl_msg.Text = ""
            If txt_SStudentName.Text <> "" Then
                With objApprovalSettings
                    getconnection()
                    Dim StudCode As String = .Check_ExitOrNot(ConStaff, "Students", "SName", "SName", txt_SStudentName.Text)
                    If StudCode <> "" Then
                        btn_Search.Focus()
                    Else
                        lbl_msg.Text = "Invalid Student name...!"
                        lbl_msg.CssClass = "redMessage"
                        lbl_msg.Visible = True
                        txt_SStudentID.Text = ""
                        txt_SStudentName.Text = ""
                        txt_SStudentName.Focus()
                    End If
                End With
            Else
                txt_SStudentID.Text = ""
            End If
        Catch ex As Exception
            lbl_msg.Text = ex.Message
            lbl_msg.Visible = True
            lbl_msg.CssClass = "redMessage"
        End Try
    End Sub
    Private Sub DocumentDetails(ByVal _Name As String)
        Dim dt As New DataTable()
        Dim dr As DataRow
        dt.Columns.Add(New System.Data.DataColumn("Comments", GetType([String])))
        dt.Columns.Add(New System.Data.DataColumn("DocDate", GetType([String])))
        dt.Columns.Add(New System.Data.DataColumn("FileName", GetType([String])))
        dt.Columns.Add(New System.Data.DataColumn("SMID", GetType([String])))
        dt.Columns.Add(New System.Data.DataColumn("Scode", GetType([String])))
        If ViewState("CurrentDocDetails") IsNot Nothing Then
            dt = New DataTable
            dt = DirectCast(ViewState("CurrentDocDetails"), DataTable)
            dr = dt.NewRow()
            dr("Comments") = txt_Comments.Text
            dr("DocDate") = CType(Now.Date, DateTime).ToString("dd-MMM-yyyy")
            dr("FileName") = _Name
            dr("SMID") = ddl_Semester.SelectedValue
            dr("Scode") = txt_StudentID.Text
            dt.Rows.Add(dr)
            ViewState("CurrentDocDetails") = dt
        Else
            dr = dt.NewRow()
            dr("Comments") = txt_Comments.Text
            dr("DocDate") = CType(Now.Date, DateTime).ToString("dd-MMM-yyyy")
            dr("FileName") = _Name
            dr("SMID") = ddl_Semester.SelectedValue
            dr("Scode") = txt_StudentID.Text
            dt.Rows.Add(dr)
            ViewState("CurrentDocDetails") = dt
        End If
        If dt.Rows.Count > 0 Then
            lbl_msg.Text = "File Uploaded Successfully...!"
            lbl_msg.Visible = True
            lbl_msg.CssClass = "greenMessage"
            btn_Save.Focus()
        End If
    End Sub
    '-------- Added on 26-Feb-2024 -SJ- As per call --------------
    Protected Sub btn_UploadDoc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_UploadDoc.Click
        Try
            lbl_msg.Text = ""
            If txt_StudentID.Text = "" Then
                lbl_msg.Text = "Please select Student...!"
                lbl_msg.Visible = True
                lbl_msg.CssClass = "redMessage"
                txt_StudentID.Focus()
                Exit Sub
            End If
            Dim fileOK As Boolean = True
            Dim fileExtension As String
            If FileUploaDoc.HasFile Then
                Dim maxFileSize As Integer = 1048576
                Dim Filesize As Integer = FileUploaDoc.PostedFile.ContentLength
                If Filesize > maxFileSize Then
                    lbl_msg.Text = "File size must not exceed 1MB..!"
                    lbl_msg.Visible = True
                    lbl_msg.CssClass = "redMessage"
                    FileUploaDoc.Focus()
                    Exit Sub
                End If
                fileExtension = System.IO.Path. _
                    GetExtension(FileUploaDoc.FileName).ToLower()
                If fileExtension = ".pdf" Then
                    Dim Filename As String = FileUploaDoc.FileName
                    If Not IO.Directory.Exists(Server.MapPath("~/TempDOC/" + txt_StudentID.Text + "/")) Then FileIO.FileSystem.CreateDirectory(Server.MapPath("~/TempDOC/" + txt_StudentID.Text + "/"))
                    If File.Exists(Server.MapPath("~/TempDOC/" + txt_StudentID.Text + "/") & "/" & FileUploaDoc.FileName()) Then
                        Session("Documentexists") = 1
                        Exit Sub
                    End If
                    FileUploaDoc.SaveAs(Server.MapPath("~/TempDOC\" + txt_StudentID.Text + "\" & Filename))
                    DocumentDetails(FileUploaDoc.FileName)
                Else
                    lbl_msg.Text = "File type not accepted!Please upload PDF document..!"
                    lbl_msg.Visible = True
                    lbl_msg.CssClass = "redMessage"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "script", "alert('File type not accepted.Please upload PDF document..!');", True)
                    Exit Sub
                End If
            Else
                lbl_msg.Text = "Please select a document...!"
                lbl_msg.Visible = True
                lbl_msg.CssClass = "redMessage"
                FileUploaDoc.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lbl_msg.Text = ex.ToString
            lbl_msg.Visible = True
            lbl_msg.CssClass = "redMessage"
        End Try
    End Sub

    Protected Sub txt_SMID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txt_SMID.TextChanged
        Try
            lbl_msg.Text = ""
            If txt_SMID.Text <> "" Then
                With objApprovalSettings
                    getconnection()
                    Dim SMID As String = .Check_ExitOrNot(ConStaff, "Semesters", "SMID", "SMID", txt_SMID.Text)
                    If SMID <> "" Then
                        btn_Search.Focus()
                    Else
                        lbl_msg.Text = "Invalid Semester ID...!"
                        lbl_msg.CssClass = "redMessage"
                        lbl_msg.Visible = True
                        txt_SMID.Text = ""
                        txt_SMName.Text = ""
                        txt_SMID.Focus()
                    End If
                End With
            Else
                txt_SMName.Text = ""
            End If
        Catch ex As Exception
            lbl_msg.Text = ex.Message
            lbl_msg.Visible = True
            lbl_msg.CssClass = "redMessage"
        End Try
    End Sub

    Protected Sub txt_SMName_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txt_SMName.TextChanged
        Try
            lbl_msg.Text = ""
            If txt_SMName.Text <> "" Then
                With objApprovalSettings
                    getconnection()
                    Dim SMName As String = .Check_ExitOrNot(ConStaff, "Semesters", "SMName", "SMName", txt_SMName.Text)
                    If SMName <> "" Then
                        btn_Search.Focus()
                    Else
                        lbl_msg.Text = "Invalid Semester name...!"
                        lbl_msg.CssClass = "redMessage"
                        lbl_msg.Visible = True
                        txt_SMID.Text = ""
                        txt_SMName.Text = ""
                        txt_SMName.Focus()
                    End If
                End With
            Else
                txt_SMID.Text = ""
            End If
        Catch ex As Exception
            lbl_msg.Text = ex.Message
            lbl_msg.Visible = True
            lbl_msg.CssClass = "redMessage"
        End Try
    End Sub
    Private Sub save_DocumentDetails(ByVal cmd As SqlCommand, ByVal Docid As String)
        Try
            Dim _Name As String
            Dim _Path As String
            If ViewState("CurrentDocDetails") IsNot Nothing Then
                Dim dt As DataTable = DirectCast(ViewState("CurrentDocDetails"), DataTable)
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        getconnection()
                        Dim GetDBfilepath As String = objApprovalSettings.GetFilepathDoc(ConStaff, "ExemptPreReq")
                        Dim Filepath As String = GetDBfilepath + txt_StudentID.Text + "\" + Docid + "\"
                        For i = 0 To dt.Rows.Count - 1
                            With objApprovalSettings
                                .Comments = txt_Comments.Text
                                .DocDate = CType(dt.Rows(i).Item("DocDate"), DateTime)
                                _Name = ReplaceSingleQuote.SafeSqlLiteral(dt.Rows(i).Item("FileName"))
                                _Path = ReplaceSingleQuote.SafeSqlLiteral(Filepath)
                                .SemID = ReplaceSingleQuote.SafeSqlLiteral(dt.Rows(i).Item("SMID"))
                                ._StudentID = ReplaceSingleQuote.SafeSqlLiteral(dt.Rows(i).Item("SCode"))
                                getconnection()
                                .InsertDocumentDetails(cmd, _Name, _Path, Docid)
                            End With
                        Next
                    End If
                End If
            End If
            ViewState.Remove("CurrentDocDetails")
        Catch ex As Exception
            lbl_msg.Text = ex.Message.ToString
            lbl_msg.CssClass = "text-danger"
        End Try
    End Sub
    Sub Delete_directeryfromSystem(ByVal folder_path As String, ByVal foldername As String)
        Try
            If Directory.Exists(folder_path + "/" + foldername) Then
                If folder_path <> "" Then
                    Dim myfolder As DirectoryInfo = New DirectoryInfo(folder_path + "/" + foldername)
                    Dim mySubfolders() As DirectoryInfo = myfolder.GetDirectories()
                    Dim strFiles() As FileInfo = myfolder.GetFiles()
                    For Each myItem As DirectoryInfo In mySubfolders
                        DeleteAllSubFolders(myItem.FullName)
                    Next
                    For Each myItem As FileInfo In strFiles
                        myItem.Delete()
                    Next
                    myfolder.Delete()
                End If
            End If
        Catch ex As Exception
            lbl_msg.Text = ex.ToString
        End Try
    End Sub
    Private Sub DeleteAllSubFolders(ByVal StartPath As String)
        Try
            Dim myfolder As DirectoryInfo = New DirectoryInfo(StartPath)
            Dim mySubfolders() As DirectoryInfo = myfolder.GetDirectories()
            Dim strFiles() As FileInfo = myfolder.GetFiles()
            For Each myItem As DirectoryInfo In mySubfolders
                DeleteAllSubFolders(myItem.FullName)
            Next
            For Each myItem As FileInfo In strFiles
                myItem.Attributes = FileAttributes.Normal
                myItem.Delete()
            Next
            myfolder.Delete()
        Catch ex As Exception
            lbl_msg.Text = ex.ToString
        End Try
    End Sub
    Sub CopyFiles_ToExemptPreReqFolder(ByVal docid As String)
        Dim copyfile As String = (Server.MapPath("~/TempDOC/" + txt_StudentID.Text))
        If IO.Directory.Exists(copyfile) Then
            getconnection()
            Dim GetExemptfilepath As String = objApprovalSettings.GetFilepathDoc(ConStaff, "ExemptPreReq")
            Dim Filepath As String = GetExemptfilepath + txt_StudentID.Text + "/" + docid
            If Not IO.Directory.Exists(Filepath) Then FileIO.FileSystem.CreateDirectory(Filepath)
            My.Computer.FileSystem.CopyDirectory(copyfile, Filepath)
        End If
    End Sub
    Protected Sub lnk_View_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            lbl_msg.Text = ""
            Dim _path As String
            Dim _FileName As String
            Dim index As Integer
            Dim Scode As String
            If gdv_ViewAttachdDoc.Rows.Count > 0 Then
                Dim gdv_ViewAttachdDoc_Row As GridViewRow = CType(CType(sender, Control).Parent.Parent,  _
                                                  GridViewRow)
                index = gdv_ViewAttachdDoc_Row.RowIndex
                Scode = gdv_ViewAttachdDoc.Rows(index).Cells(2).Text
                _path = gdv_ViewAttachdDoc.Rows(index).Cells(5).Text
                _FileName = gdv_ViewAttachdDoc.Rows(index).Cells(4).Text
                Dim open_File As String = _path + _FileName
                If open_File = "" Then
                Else
                    If open_File <> "" Then
                        If File.Exists(open_File) Then
                            Dim file_extension As String = Path.GetExtension(open_File).ToLower
                            Dim clients As New WebClient()
                            Dim newFileData As Byte() = clients.DownloadData(open_File)
                            If file_extension = ".pdf" Then
                                Response.ContentType = "application/pdf"
                                Response.AddHeader("content-length", newFileData.Length.ToString())
                                Response.AppendHeader("Content-Disposition", "attachment;filename=" & _FileName)
                                Response.BinaryWrite(newFileData)
                            End If
                        Else
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "script", "alert('File is Not Found!!');", True)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lbl_msg.Text = ex.Message
            lbl_msg.Visible = True
            lbl_msg.CssClass = "text-danger"
        End Try
    End Sub

    Private Sub BindAttachedDouments()
        Dim dt As New DataTable()
        getconnection()
        dt = objApprovalSettings.BindAttachedDouments(ConStaff, hdn_DocID.Value)
        gdv_ViewAttachdDoc.DataSource = dt
        gdv_ViewAttachdDoc.DataBind()
    End Sub
    Protected Sub btn_ViewAttachments_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_ViewAttachments.Click
        Try
            lbl_msg.Text = ""
            If hdn_DocID.Value <> "" Then
                ModalPopupExtenderViewAttachDoc.Show()
                BindAttachedDouments()
            End If
        Catch ex As Exception
            lbl_msg.Text = ex.Message
            lbl_msg.Visible = True
            lbl_msg.CssClass = "text-danger"
        End Try
    End Sub
    '----------- Added on 01-Apr-2024 -SJ- As per chat --------------------
    Private Sub gdv_ApprovalSettings_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gdv_ApprovalSettings.RowDeleting
        Try
            lbl_msg.Text = ""
            Dim index As Integer
            Dim DocID As Integer
            If gdv_ApprovalSettings.Rows.Count > 0 Then
                index = e.RowIndex
                DocID = gdv_ApprovalSettings.Rows(index).Cells(0).Text
                getconnection()
                If ConStaff.State = ConnectionState.Closed Then ConStaff.Open()
                Dim da As New SqlDataAdapter("", ConStaff)
                da.DeleteCommand = New SqlCommand("delete from AttApprovedTeachers where AppType='" & ddl_Mode.SelectedValue & "' and DOCID=" & DocID & "", ConStaff)
                da.DeleteCommand.ExecuteNonQuery()
                Dim dt As DataTable = DirectCast(ViewState("CurrentState"), DataTable)
                dt.Rows(index).Delete()
                dt.AcceptChanges()
                ViewState("CurrentState") = dt
                BindData_ApprovalDetails()
            End If
        Catch ex As Exception
            lbl_msg.Text = ex.Message
            lbl_msg.Visible = True
            lbl_msg.CssClass = "redMessage"
        End Try
    End Sub
    '---------- Added on 11-Apr-2024 -SJ- As per mail ------------------
    Private Sub txt_PreReqCSID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_PreReqCSID.TextChanged
        Try
            lbl_msg.Text = ""
            If txt_PreReqCSID.Text <> "" Then
                getconnection()
                With objApprovalSettings
                    .CSID = txt_CourseID.Text
                    Dim PreReqExist As Integer = .Check_ExitOrNot_PreRequisitesCourses(ConStaff, txt_PreReqCSID.Text)
                    If PreReqExist > 0 Then
                        If ddl_PreReqCSName.Items.IndexOf(ddl_PreReqCSName.Items.FindByValue(txt_PreReqCSID.Text)) = -1 Then
                            ddl_PreReqCSName.SelectedIndex = 0
                        Else
                            ddl_PreReqCSName.SelectedValue = txt_PreReqCSID.Text
                        End If
                        txt_Comments.Focus()
                    Else
                        lbl_msg.Text = "Invalid Pre-Requisites Course ID...!"
                        lbl_msg.CssClass = "redMessage"
                        lbl_msg.Visible = True
                        txt_PreReqCSID.Text = ""
                        If ddl_PreReqCSName.Items.Count > 0 Then
                            ddl_PreReqCSName.SelectedIndex = 0
                        End If
                        txt_PreReqCSID.Focus()
                    End If
                End With
            Else
                ddl_PreReqCSName.SelectedIndex = 0
            End If
        Catch ex As Exception
            lbl_msg.Text = ex.Message
            lbl_msg.Visible = True
            lbl_msg.CssClass = "redMessage"
        End Try
    End Sub
    Private Sub ddl_PreReqCSName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_PreReqCSName.SelectedIndexChanged
        Try
            lbl_msg.Text = ""
            If ddl_PreReqCSName.SelectedValue <> "--select--" Then
                txt_PreReqCSID.Text = ddl_PreReqCSName.SelectedValue
                txt_Comments.Focus()
            Else
                txt_PreReqCSID.Text = ""
                ddl_PreReqCSName.Focus()
            End If
        Catch ex As Exception
            lbl_msg.Text = ex.Message
            lbl_msg.Visible = True
            lbl_msg.CssClass = "redMessage"
        End Try
    End Sub
    '-------------------------------------------------------------------------
End Class