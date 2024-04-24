<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/StaffPanel/OMCStaff.Master"
    CodeBehind="frmApproval.aspx.vb" Inherits="OMC.frmApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="metaContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Header
        {
            font-family: Segoe UI Semibold;
            font-size: 16px;
            color: White;
            background-color: #004d9c;
            text-align: left;
            border-radius: 5px 5px 5px 5px;
        }
        .textbox
        {
            margin-left: 0px;
            margin-right: 0px;
        }
        .grid
        {
            height: 27px;
            background-color: #338ADE;
            background-image: linear-gradient(#88B7E8, #1057A1);
            color: White;
            font-size: 11px;
        }
        .button
        {
            padding: 15px 25px;
            font-size: 24px;
            text-align: center;
            cursor: pointer;
            outline: none;
            color: #fff;
            background-color: #004d9c;
            border: none;
            border-radius: 15px;
        }
        .button:hover
        {
            background-color: #001a33;
        }
        .button:active
        {
            background-color: #001a33;
            transform: translateY(4px);
        }
        
        .disabled
        {
            opacity: 0.6;
            cursor: not-allowed;
        }
        .confirm
        {
            display: none;
        }
        .LabelHeader
        {
            font-family: Times New Roman;
            font-size: 17px;
            color: #02365B;
            background-color: #74B8F6;
            text-align: center;
            border-radius: 5px 5px 5px 5px;
            font-weight: bold;
        }
        .tabc
        {
            text-align: right;
            color: #ffffff;
            font-size: small;
            font-family: 'Times New Roman' , Times, serif;
        }
        
        .MyTabStyle .ajax__tab_header
        {
            font-family: "Helvetica Neue" , Arial, Sans-Serif;
            font-size: 14px;
            font-weight: bold;
            display: block;
            background-color: #74B8F6;
        }
        .MyTabStyle .ajax__tab_header .ajax__tab_outer
        {
            border-color: #222;
            color: #ffffff;
            padding-left: 10px;
            margin-right: 3px;
            border: solid 1px #d7d7d7;
        }
        .MyTabStyle .ajax__tab_header .ajax__tab_inner
        {
            border-color: #666;
            color: #0E4C85;
            padding: 3px 10px 2px 0px;
        }
        .MyTabStyle .ajax__tab_hover .ajax__tab_outer
        {
            background-color: #666;
        }
        .MyTabStyle .ajax__tab_hover .ajax__tab_inner
        {
            color: #fff;
        }
        .MyTabStyle .ajax__tab_active .ajax__tab_outer
        {
            border-bottom-color: #ffffff;
            background-color: #d7d7d7;
        }
        .MyTabStyle .ajax__tab_active .ajax__tab_inner
        {
            color: #027FE8;
            border-color: #333;
        }
        .MyTabStyle .ajax__tab_body
        {
            font-family: verdana,tahoma,helvetica;
            font-size: 10pt;
            background-color: #fff;
            border-top-width: 0;
            border: solid 1px #d7d7d7;
            border-top-color: #ffffff;
        }
        .style1
        {
            height: 328px;
        }
        .ULCustomerCode
        {
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            height: 200px;
            overflow: auto;
            background-color: #FFFFFF;
            font-family: Verdana;
            font-size: 10px;
            width: 390px !important;
            text-align: left;
            visibility: hidden;
        }
        
        .ULCustomerName
        {
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            height: 200px;
            overflow: auto;
            background-color: #FFFFFF;
            font-family: Verdana;
            font-size: 10px;
            width: 283px !important;
            text-align: left;
            visibility: hidden;
        }
        .completionList
        {
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            height: 200px;
            overflow: auto;
            background-color: #FFFFFF;
            font-family: Verdana;
            font-size: 10px;
            width: 430px !important;
            visibility: hidden;
            width: 120px !important;
        }
        .itemHighlighted
        {
            background-color: #ffc0c0;
        }
        .itemHighlighted
        {
            background-color: #ffc0c0;
        }
        .smallcompletionList
        {
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            height: 200px;
            overflow: auto;
            background-color: #FFFFFF;
            font-family: Verdana;
            font-size: 10px;
            width: 420px !important;
            visibility: hidden;
        }
        .listItem
        {
            color: #1C1C1C;
        }
        .style5
        {
            width: 143px;
        }
        .style6
        {
            width: 136px;
        }
        .style7
        {
            width: 109px;
        }
        .style11
        {
            width: 350px;
        }
        .style19
        {
            width: 8px;
            font-size: 12px;
        }
        .style22
        {
            width: 373px;
        }
        .style24
        {
            width: 93px;
        }
        .style33
        {
            width: 131px;
        }
        .style47
        {
            width: 206px;
        }
        .style52
        {
            height: 328px;
            width: 8px;
        }
        .style59
        {
            width: 8px;
        }
        .style73
        {
            width: 69px;
            font-size: 12px;
        }
        .style75
        {
            width: 255px;
        }
        .style85
        {
            width: 298px;
        }
        .style90
        {
            width: 200px;
            font-size: 12px;
        }
        .style91
        {
            width: 464px;
        }
        .style93
        {
            width: 105px;
        }
        .style94
        {
            width: 242px;
        }
        .popupSub
        {
            position: absolute;
            width: 885px;
            height: 230px;
            background: #fff;
            left: 40% !important;
            top: 50% !important;
            border-radius: 12px;
            padding: 0;
            margin-left: -275px; /* width/2 + padding-left */
            margin-top: -150px; /* height/2 + padding-top */
            text-align: center;
            box-shadow: 0 0 10px 0 #000;
            border: 3px solid #305473;
        }
        .headerPop
        {
            /*background-color: #2FBDF1;*/
            background: linear-gradient(to bottom, #000099 5%, #000066 100%);
            height: 30px;
            color: White;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        function SetSelectedValue(sender, eventArgs) {
            if (sender.get_id() == '<%=txt_SFacultyID_Extender.ClientID %>') {
                val = document.getElementById('<%=txt_SFacultyID.ClientID %>').value;
                splitVal = val.split(' => ');
                document.getElementById('<%=txt_SFacultyID.ClientID %>').value = splitVal[0];
                document.getElementById('<%=txt_SFacultyName.ClientID %>').value = splitVal[1];
            }
        }
        function SetSelectedValueName(sender, eventArgs) {
            if (sender.get_id() == '<%=txt_SFacultyName_Extender.ClientID %>') {
                val = document.getElementById('<%=txt_SFacultyName.ClientID %>').value;
                splitVal = val.split(' => ');
                document.getElementById('<%=txt_SFacultyID.ClientID %>').value = splitVal[1];
                document.getElementById('<%=txt_SFacultyName.ClientID %>').value = splitVal[0];
            }
        }
        function SetStudentValue(sender, eventArgs) {
            if (sender.get_id() == '<%=txt_SStudentID_Extender.ClientID %>') {
                val = document.getElementById('<%=txt_SStudentID.ClientID %>').value;
                splitVal = val.split(' => ');
                document.getElementById('<%=txt_SStudentID.ClientID %>').value = splitVal[0];
                document.getElementById('<%=txt_SStudentName.ClientID %>').value = splitVal[1];
            }
        }

        function SetStudentNameValue(sender, eventArgs) {
            if (sender.get_id() == '<%=txt_SStudentName_Extender.ClientID %>') {
                val = document.getElementById('<%=txt_SStudentName.ClientID %>').value;
                splitVal = val.split(' => ');
                document.getElementById('<%=txt_SStudentID.ClientID %>').value = splitVal[1];
                document.getElementById('<%=txt_SStudentName.ClientID %>').value = splitVal[0];
            }
        }
        function SetSelectedValue(sender, eventArgs) {
            // alert(sender.get_id());
            var val, splitVal;

            if (sender.get_id() == '<%=txt_SFacultyID_Extender.ClientID %>') {
                val = document.getElementById('<%=txt_SFacultyID.ClientID %>').value;
                splitVal = val.split(' => ');
                document.getElementById('<%=txt_SFacultyID.ClientID %>').value = splitVal[0];
                document.getElementById('<%=txt_SFacultyName.ClientID %>').value = splitVal[1];
            }
            if (sender.get_id() == '<%=txt_SFacultyName_Extender.ClientID %>') {
                val = document.getElementById('<%=txt_SFacultyName.ClientID %>').value;
                splitVal = val.split(' => ');
                document.getElementById('<%=txt_SFacultyID.ClientID %>').value = splitVal[1];
                document.getElementById('<%=txt_SFacultyName.ClientID %>').value = splitVal[0];
            }

            if (sender.get_id() == '<%=txt_SStudentID_Extender.ClientID %>') {
                val = document.getElementById('<%=txt_SStudentID.ClientID %>').value;
                splitVal = val.split(' => ');
                document.getElementById('<%=txt_SStudentID.ClientID %>').value = splitVal[0];
                document.getElementById('<%=txt_SStudentName.ClientID %>').value = splitVal[1];
            }
            if (sender.get_id() == '<%=txt_SStudentName_Extender.ClientID %>') {
                val = document.getElementById('<%=txt_SStudentName.ClientID %>').value;
                splitVal = val.split(' => ');
                document.getElementById('<%=txt_SStudentID.ClientID %>').value = splitVal[1];
                document.getElementById('<%=txt_SStudentName.ClientID %>').value = splitVal[0];
            }
            if (sender.get_id() == '<%=ace_SMID.ClientID %>') {
                val = document.getElementById('<%=txt_SMID.ClientID %>').value;
                splitVal = val.split(' => ');
                document.getElementById('<%=txt_SMID.ClientID %>').value = splitVal[0];
                document.getElementById('<%=txt_SMName.ClientID %>').value = splitVal[1];
            }
            if (sender.get_id() == '<%=ace_SMName.ClientID %>') {
                val = document.getElementById('<%=txt_SMName.ClientID %>').value;
                splitVal = val.split(' => ');
                document.getElementById('<%=txt_SMID.ClientID %>').value = splitVal[1];
                document.getElementById('<%=txt_SMName.ClientID %>').value = splitVal[0];
            }
            if (sender.get_id() == '<%=txt_FacultyID_AutoCompleteExtender.ClientID %>') {
                val = document.getElementById('<%=txt_FacultyID.ClientID %>').value;
                splitVal = val.split(' => ');
                document.getElementById('<%=txt_FacultyID.ClientID %>').value = splitVal[0];
                document.getElementById('<%=txt_FacultyName.ClientID %>').value = splitVal[1];
            }

            if (sender.get_id() == '<%=txt_FacultyName_Extender.ClientID %>') {
                val = document.getElementById('<%=txt_FacultyName.ClientID %>').value;
                splitVal = val.split(' => ');
                document.getElementById('<%=txt_FacultyID.ClientID %>').value = splitVal[1];
                document.getElementById('<%=txt_FacultyName.ClientID %>').value = splitVal[0];
            }

            if (sender.get_id() == '<%=ace_CourseID.ClientID %>') {
                val = document.getElementById('<%=txt_CourseID.ClientID %>').value;
                splitVal = val.split(' => ');
                document.getElementById('<%=txt_CourseID.ClientID %>').value = splitVal[0];
                document.getElementById('<%=ddlCourseName.ClientID %>').value = splitVal[1];
                document.getElementById('<%=txt_StudentID.ClientID%>').focus();
            }

            if (sender.get_id() == '<%=ace_PreReqCSID.ClientID %>') {
                val = document.getElementById('<%=txt_PreReqCSID.ClientID %>').value;
                splitVal = val.split(' => ');
                document.getElementById('<%=txt_PreReqCSID.ClientID %>').value = splitVal[0];
                document.getElementById('<%=ddl_PreReqCSName.ClientID %>').value = splitVal[1];
                document.getElementById('<%=txt_Comments.ClientID%>').focus();
            }

            if (sender.get_id() == '<%=ACE_Stud.ClientID %>') {
                val = document.getElementById('<%=txt_StudentID.ClientID %>').value;
                splitVal = val.split(' => ');
                document.getElementById('<%=ddl_StudName.ClientID %>').value = splitVal[1];
                document.getElementById('<%=txt_StudentID.ClientID %>').value = splitVal[0];
                document.getElementById('<%=txt_AssignID.ClientID%>').focus();
            }



            if (sender.get_id() == '<%=ace_AssID.ClientID %>') {
                val = document.getElementById('<%=txt_AssignID.ClientID %>').value;
                splitVal = val.split(' => ');
                document.getElementById('<%=txt_AssignID.ClientID %>').value = splitVal[0];
                document.getElementById('<%=ddlAssName.ClientID %>').value = splitVal[1];

            }


        }

        function Print_Report() {
            var optionString = "left=0,top=0,menubar=no,toolbar=no,scrollbars=yes,location=no,resizable=yes";
            var Mode = document.getElementById('<%=hdn_Mode.ClientID %>').value;
            if (Mode == "M" || Mode == "H" || Mode == "P") {
                var SCode = document.getElementById('<%=txt_SStudentID.ClientID %>').value;
                var newWindow15 = window.open("Report.aspx?path=~/StaffPanel/Reports/rptApproval.rpt&type=Approval&&Mode=" + Mode + "&SCode=" + SCode, "ReportS", optionString);
            }
            else {
                var faculty = document.getElementById('<%=txt_SFacultyID.ClientID %>').value;
                var SCode = document.getElementById('<%=txt_SStudentID.ClientID %>').value;
                var newWindow15 = window.open("Report.aspx?path=~/StaffPanel/Reports/rptApproval.rpt&type=Approval&&Mode=" + Mode + "&faculty=" + faculty + "&SCode=" + SCode, "ReportS", optionString);
            }


            newWindow15.resizeTo(screen.availWidth, screen.availHeight)
        }
        function ShowDetails(rowindex) {

            document.getElementById('<%=hdn_Index.ClientID %>').value = rowindex
            document.getElementById('<%=hdn_Value.ClientID %>').value = 'GridSelect';
            document.getElementById('<%=btn_Confirm.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanelCourseMaster" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td class="Header">
                        <div style="position: relative; clear: both; left: 30px; top: 0; width: 350px; text-shadow: 2px 0px 10px #000000;
                            height: 30px;" id="lbl_Title" runat="server">
                            <span style="position: relative; top: 2px;">Approval Explorer</span></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset style="background-color: #DBDBDB;">
                            <fieldset style="border-radius: 10px; border: 1px solid gray; background-color: #DBDBDB;">
                                <table width="100%">
                                    <tr>
                                        <td class="style59">
                                        </td>
                                        <td>
                                            <asp:Panel ID="Panel1" runat="server" Width="1000px" BackColor="#F7F6F3" BorderColor="Silver"
                                                BorderStyle="Solid" BorderWidth="1px">
                                                <table>
                                                    <tr>
                                                        <%--<td class="style22" style="text-align: right; color: #003366; font-size: 12px;">
                                                            Approve Mode:
                                                        </td>--%>
                                                        <td class="style22" style="text-align: right; color: #003366; font-size: 12px; font-family: verdana,tahoma,helvetica;">
                                                            Faculty:
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddl_Mode" runat="server" CssClass="dropdownlist" Width="320px"
                                                                TabIndex="1" Enabled="False">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="A">Approve Attendance Entry After Due Period</asp:ListItem>
                                                                <asp:ListItem Value="M">Allow Marks Correction After Approval</asp:ListItem>
                                                                <asp:ListItem Value="H">Attendance Approval for HallTicket</asp:ListItem>
                                                                <asp:ListItem Value="P">Exempt Pre-Requisites</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style52">
                                        </td>
                                        <td class="style1">
                                            <%--lll--%>
                                            <asp:TabContainer ID="TabContainer2" runat="server" Width="1020px" Height="410px"
                                                CssClass="MyTabStyle" ActiveTabIndex="1" AutoPostBack="True" BorderStyle="Solid"
                                                BorderColor="#999966" BorderWidth="1px" TabIndex="2">
                                                <asp:TabPanel ID="TbApproval" runat="server" HeaderText="Approval">
                                                    <HeaderTemplate>
                                                        Approval
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <div style="width: 990px">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="right" class="style6">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style6">
                                                                        <asp:Panel ID="Panel4" runat="server" BackColor="#F7F6F3" BorderColor="Silver" BorderStyle="Solid"
                                                                            BorderWidth="1px" Width="990px" Style="margin-left: 0px">
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td class="style85" style="text-align: right; color: #003366; font-size: 12px;">
                                                                                        Semester
                                                                                    </td>
                                                                                    <td class="style47" style="text-align: right; color: #003366; font-size: small; font-family: 'Times New Roman', Times, serif;">
                                                                                        <asp:DropDownList ID="ddl_Semester" runat="server" CssClass="dropdownlist" TabIndex="2"
                                                                                            Width="220px" AutoPostBack="True">
                                                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                                            <asp:ListItem Value="1">Attendance Entry Approval</asp:ListItem>
                                                                                            <asp:ListItem Value="2">Marks Entry Approval</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td style="text-align: right; color: #003366; font-size: 12px;" class="style7">
                                                                                        <asp:Label ID="lbl_ExpiredOn" runat="server" Text="Expired On:"></asp:Label>
                                                                                    </td>
                                                                                    <td align="left">
                                                                                        <asp:TextBox ID="txt_Expired" runat="server" CssClass="textbox" TabIndex="3" Width="125px"
                                                                                            AutoPostBack="True" Enabled="False"></asp:TextBox>
                                                                                        <asp:CalendarExtender ID="txt_Expired_CalendarExtender" runat="server" Enabled="True"
                                                                                            Format="dd-MMM-yyyy" TargetControlID="txt_Expired">
                                                                                        </asp:CalendarExtender>
                                                                                    </td>
                                                                                    <td style="width: 50px;">
                                                                                        &#160;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="style85" style="text-align: right; padding-bottom: 10px; color: #003366;
                                                                                        font-size: 12px;" id="lbl_AppFaculty" runat="server">
                                                                                        Faculty
                                                                                    </td>
                                                                                    <td class="style47" style="text-align: left; color: #003366; padding-bottom: 10px;
                                                                                        font-size: 14px; font-family: 'Times New Roman', Times, serif;" colspan="4" align="left">
                                                                                        <asp:TextBox ID="txt_FacultyID" runat="server" CssClass="textbox" TabIndex="4" Width="158px"
                                                                                            AutoPostBack="True"></asp:TextBox>
                                                                                        <asp:AutoCompleteExtender ID="txt_FacultyID_AutoCompleteExtender" runat="server"
                                                                                            CompletionInterval="100" CompletionListCssClass="smallcompletionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                                                            CompletionListItemCssClass="listItem" ContextKey="Faculty" DelimiterCharacters=""
                                                                                            Enabled="True" EnableViewState="False" FirstRowSelected="True" MinimumPrefixLength="0"
                                                                                            OnClientItemSelected="SetSelectedValue" ServiceMethod="GetCompletionList" ServicePath=""
                                                                                            TargetControlID="txt_FacultyID" UseContextKey="True">
                                                                                        </asp:AutoCompleteExtender>
                                                                                        <asp:TextBox ID="txt_FacultyName" runat="server" CssClass="textbox" TabIndex="4"
                                                                                            Width="296px" AutoPostBack="True"></asp:TextBox>
                                                                                        <asp:AutoCompleteExtender ID="txt_FacultyName_Extender" runat="server" CompletionInterval="100"
                                                                                            CompletionListCssClass="smallcompletionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                                                            CompletionListItemCssClass="listItem" ContextKey="FacultyN" DelimiterCharacters=""
                                                                                            Enabled="True" EnableViewState="False" FirstRowSelected="True" MinimumPrefixLength="0"
                                                                                            OnClientItemSelected="SetSelectedValue" ServiceMethod="GetCompletionList" ServicePath=""
                                                                                            TargetControlID="txt_FacultyName" UseContextKey="True">
                                                                                        </asp:AutoCompleteExtender>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="Panel2" runat="server" BackColor="#F7F6F3" BorderColor="Silver" BorderStyle="Solid"
                                                                            BorderWidth="1px" Width="990px" Style="margin-left: 0px">
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td class="style90" align="right">
                                                                                        <asp:Label ID="lblStudentID" runat="server" Text="Student ID"></asp:Label>
                                                                                    </td>
                                                                                    <td class="style33">
                                                                                        <asp:TextBox ID="txt_StudentID" runat="server" CssClass="textbox" TabIndex="5" Width="125px"
                                                                                            AutoPostBack="True"></asp:TextBox>
                                                                                        <asp:AutoCompleteExtender ID="ACE_Stud" runat="server" CompletionInterval="250" CompletionListCssClass="smallcompletionList"
                                                                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                                            CompletionSetCount="20" ContextKey="StudID" EnableViewState="False" FirstRowSelected="True"
                                                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList" TargetControlID="txt_StudentID"
                                                                                            UseContextKey="True" OnClientItemSelected="SetSelectedValue" DelimiterCharacters=""
                                                                                            Enabled="True" ServicePath="">
                                                                                        </asp:AutoCompleteExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddl_StudName" runat="server" Width="330px" AutoPostBack="True"
                                                                                            CssClass="dropdownlist" CausesValidation="True" TabIndex="5">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="style90" align="right" style="padding-top: 10px;">
                                                                                        <asp:Label ID="lbl_CSID" runat="server" Text="Course ID"></asp:Label>
                                                                                    </td>
                                                                                    <td class="style33" style="padding-top: 10px;">
                                                                                        <asp:TextBox ID="txt_CourseID" runat="server" CssClass="textbox" TabIndex="6" Width="125px"
                                                                                            AutoPostBack="True"></asp:TextBox>
                                                                                        <asp:AutoCompleteExtender ID="ace_CourseID" runat="server" CompletionInterval="250"
                                                                                            CompletionListCssClass="smallcompletionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                                                            CompletionListItemCssClass="listItem" CompletionSetCount="20" ContextKey="CsID"
                                                                                            EnableViewState="False" FirstRowSelected="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionList"
                                                                                            TargetControlID="txt_CourseID" OnClientItemSelected="SetSelectedValue" UseContextKey="True"
                                                                                            DelimiterCharacters="" Enabled="True" ServicePath="">
                                                                                        </asp:AutoCompleteExtender>
                                                                                    </td>
                                                                                    <td style="padding-top: 10px;">
                                                                                        <asp:DropDownList ID="ddlCourseName" runat="server" Width="330px" AutoPostBack="True"
                                                                                            CssClass="dropdownlist" CausesValidation="True" TabIndex="6">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <% If Session("AppType") = "P" Then%>
                                                                                <tr>
                                                                                    <td class="style90" align="right" style="padding-top: 10px;">
                                                                                        <asp:Label ID="lbl_PreReqCSD" runat="server" Text="Pre-Requisites Course ID"></asp:Label>
                                                                                    </td>
                                                                                    <td class="style33" style="padding-top: 10px;">
                                                                                        <asp:TextBox ID="txt_PreReqCSID" runat="server" CssClass="textbox" TabIndex="6" Width="125px"
                                                                                            AutoPostBack="True"></asp:TextBox>
                                                                                        <asp:AutoCompleteExtender ID="ace_PreReqCSID" runat="server" CompletionInterval="250"
                                                                                            CompletionListCssClass="smallcompletionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                                                            CompletionListItemCssClass="listItem" CompletionSetCount="20" ContextKey="PreReqCsID"
                                                                                            EnableViewState="False" FirstRowSelected="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionList"
                                                                                            TargetControlID="txt_PreReqCSID" OnClientItemSelected="SetSelectedValue" UseContextKey="True"
                                                                                            DelimiterCharacters="" Enabled="True" ServicePath="">
                                                                                        </asp:AutoCompleteExtender>
                                                                                    </td>
                                                                                    <td style="padding-top: 10px;">
                                                                                        <asp:DropDownList ID="ddl_PreReqCSName" runat="server" Width="330px" AutoPostBack="True"
                                                                                            CssClass="dropdownlist" CausesValidation="True" TabIndex="6">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <% End If%>
                                                                                <tr>
                                                                                    <td class="style90" align="right">
                                                                                        <asp:Label ID="lblAssignmentID" runat="server" Text="Assignment ID"></asp:Label>
                                                                                    </td>
                                                                                    <td class="style33">
                                                                                        <asp:TextBox ID="txt_AssignID" runat="server" CssClass="textbox" TabIndex="7" Width="125px"
                                                                                            AutoPostBack="True"></asp:TextBox>
                                                                                        <asp:AutoCompleteExtender ID="ace_AssID" runat="server" CompletionInterval="250"
                                                                                            CompletionListCssClass="smallcompletionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                                                            CompletionListItemCssClass="listItem" CompletionSetCount="20" ContextKey="AsgID"
                                                                                            EnableViewState="False" FirstRowSelected="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionList"
                                                                                            TargetControlID="txt_AssignID" UseContextKey="True" OnClientItemSelected="SetSelectedValue"
                                                                                            DelimiterCharacters="" Enabled="True" ServicePath="">
                                                                                        </asp:AutoCompleteExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddlAssName" runat="server" Width="330px" AutoPostBack="True"
                                                                                            CssClass="dropdownlist" CausesValidation="True" TabIndex="7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="style90" align="right">
                                                                                        Details/Comments
                                                                                    </td>
                                                                                    <td class="style5" colspan="5">
                                                                                        <asp:TextBox ID="txt_Comments" runat="server" CssClass="textbox" Height="60px" MaxLength="500"
                                                                                            TabIndex="8" TextMode="MultiLine" Width="458px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <% If Session("AppType") = "P" Then%>
                                                                                <tr>
                                                                                    <td class="style90" align="right">
                                                                                        Registered Date:
                                                                                    </td>
                                                                                    <td colspan="2">
                                                                                        <table style="width: 437px">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_RegDate" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td align="right">
                                                                                                    Status:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lbl_Status" runat="server"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <% End If%>
                                                                                <tr>
                                                                                    <td class="style90" align="right" runat="server" id="lbl_AttachDoc">
                                                                                        Attach Document
                                                                                    </td>
                                                                                    <td class="style5" colspan="5">
                                                                                        <table cellpadding="1" cellspacing="0">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:FileUpload ID="FileUploaDoc" runat="server" TabIndex="9" />
                                                                                                        </ContentTemplate>
                                                                                                        <Triggers>
                                                                                                            <asp:PostBackTrigger ControlID="btn_UploadDoc" />
                                                                                                        </Triggers>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Button ID="btn_UploadDoc" runat="server" CssClass="myButton" Height="27px" TabIndex="10"
                                                                                                        Text="Upload" Width="85px" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right" class="style91">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td class="style33">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td class="style24">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td class="style19">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td class="style11">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td>
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="6" align="center">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Button ID="btn_ViewAttachments" runat="server" CssClass="button" TabIndex="15"
                                                                                                        Text="View Attachments" UseSubmitBehavior="False" Width="130px"></asp:Button>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Button ID="btn_Save" runat="server" CssClass="button" TabIndex="11" Text="Save"
                                                                                                        UseSubmitBehavior="False" Width="100px" />
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Button ID="btn_cancel" runat="server" CssClass="button" TabIndex="12" Text="Cancel"
                                                                                                        Width="100px" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right" class="style91">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td class="style33">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td class="style24">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td class="style19">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td class="style11">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td>
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="6" style="padding-left: 65px" class="TextCSS">
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:TabPanel>
                                                <asp:TabPanel ID="TbSearch" runat="server" HeaderText="Search">
                                                    <HeaderTemplate>
                                                        Search
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <div>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="right">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="Panel5" runat="server" BackColor="#F7F6F3" BorderColor="Silver" BorderStyle="Solid"
                                                                            BorderWidth="1px" Width="100%">
                                                                            <table style="width: 90%">
                                                                                <tr>
                                                                                    <td class="style75" style="text-align: right; color: #003366; font-size: 12px;" id="lbl_SFaculty"
                                                                                        runat="server">
                                                                                        Faculty:
                                                                                    </td>
                                                                                    <td style="text-align: left; color: #003366; font-size: 14px; font-family: 'Times New Roman', Times, serif;"
                                                                                        align="left">
                                                                                        <asp:TextBox ID="txt_SFacultyID" runat="server" CssClass="textbox" TabIndex="11"
                                                                                            Width="125px"></asp:TextBox>
                                                                                        <asp:AutoCompleteExtender ID="txt_SFacultyID_Extender" runat="server" CompletionInterval="100"
                                                                                            CompletionListCssClass="smallcompletionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                                                            CompletionListItemCssClass="listItem" ContextKey="Faculty" DelimiterCharacters=""
                                                                                            Enabled="True" EnableViewState="False" FirstRowSelected="True" MinimumPrefixLength="0"
                                                                                            OnClientItemSelected="SetSelectedValue" ServiceMethod="GetCompletionList" ServicePath=""
                                                                                            TargetControlID="txt_SFacultyID" UseContextKey="True">
                                                                                        </asp:AutoCompleteExtender>
                                                                                    </td>
                                                                                    <td style="text-align: left; color: #003366; font-size: 14px; font-family: 'Times New Roman', Times, serif;">
                                                                                        <asp:TextBox ID="txt_SFacultyName" runat="server" CssClass="textbox" TabIndex="11"
                                                                                            Width="283px"></asp:TextBox>
                                                                                        <asp:AutoCompleteExtender ID="txt_SFacultyName_Extender" runat="server" CompletionInterval="100"
                                                                                            CompletionListCssClass="ULCustomerName" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                                                            CompletionListItemCssClass="listItem" ContextKey="FacultyN" DelimiterCharacters=""
                                                                                            Enabled="True" EnableViewState="False" FirstRowSelected="True" MinimumPrefixLength="0"
                                                                                            OnClientItemSelected="SetSelectedValue" ServiceMethod="GetCompletionList" ServicePath=""
                                                                                            TargetControlID="txt_SFacultyName" UseContextKey="True">
                                                                                        </asp:AutoCompleteExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="style75" style="text-align: right; color: #003366; font-size: 12px;">
                                                                                        Student:
                                                                                    </td>
                                                                                    <td style="text-align: left; color: #003366; font-size: small; font-family: 'Times New Roman', Times, serif;">
                                                                                        <asp:TextBox ID="txt_SStudentID" runat="server" CssClass="textbox" TabIndex="12"
                                                                                            Width="125px" AutoPostBack="True"></asp:TextBox>
                                                                                        <asp:AutoCompleteExtender ID="txt_SStudentID_Extender" runat="server" CompletionInterval="100"
                                                                                            CompletionListCssClass="smallcompletionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                                                            CompletionListItemCssClass="listItem" ContextKey="StudentS" DelimiterCharacters=""
                                                                                            Enabled="True" EnableViewState="False" FirstRowSelected="True" MinimumPrefixLength="0"
                                                                                            OnClientItemSelected="SetSelectedValue" ServiceMethod="GetCompletionList" ServicePath=""
                                                                                            TargetControlID="txt_SStudentID" UseContextKey="True">
                                                                                        </asp:AutoCompleteExtender>
                                                                                    </td>
                                                                                    <td style="color: #003366; font-size: small; font-family: 'Times New Roman', Times, serif;
                                                                                        text-align: left" align="left" class="style94">
                                                                                        <asp:TextBox ID="txt_SStudentName" runat="server" CssClass="textbox" TabIndex="12"
                                                                                            Width="283px" AutoPostBack="True"></asp:TextBox>
                                                                                        <asp:AutoCompleteExtender ID="txt_SStudentName_Extender" runat="server" CompletionInterval="100"
                                                                                            CompletionListCssClass="ULCustomerName" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                                                            CompletionListItemCssClass="listItem" ContextKey="StudentNameS" DelimiterCharacters=""
                                                                                            Enabled="True" EnableViewState="False" FirstRowSelected="True" MinimumPrefixLength="0"
                                                                                            OnClientItemSelected="SetSelectedValue" ServiceMethod="GetCompletionList" ServicePath=""
                                                                                            TargetControlID="txt_SStudentName" UseContextKey="True">
                                                                                        </asp:AutoCompleteExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="style75" style="text-align: right; color: #003366; font-size: 12px;">
                                                                                        <asp:Label ID="lbl_Semester" runat="server" Text="Semester:"></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left; color: #003366; font-size: small; font-family: 'Times New Roman', Times, serif;">
                                                                                        <asp:TextBox ID="txt_SMID" runat="server" CssClass="textbox" TabIndex="12" Width="125px"
                                                                                            AutoPostBack="True"></asp:TextBox>
                                                                                        <asp:AutoCompleteExtender ID="ace_SMID" runat="server" CompletionInterval="100" CompletionListCssClass="smallcompletionList"
                                                                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                                                            ContextKey="SMID" DelimiterCharacters="" Enabled="True" EnableViewState="False"
                                                                                            FirstRowSelected="True" MinimumPrefixLength="0" OnClientItemSelected="SetSelectedValue"
                                                                                            ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txt_SMID" UseContextKey="True">
                                                                                        </asp:AutoCompleteExtender>
                                                                                    </td>
                                                                                    <td style="color: #003366; font-size: small; font-family: 'Times New Roman', Times, serif;
                                                                                        text-align: left" align="left" class="style94">
                                                                                        <asp:TextBox ID="txt_SMName" runat="server" CssClass="textbox" TabIndex="12" Width="283px"
                                                                                            AutoPostBack="True"></asp:TextBox>
                                                                                        <asp:AutoCompleteExtender ID="ace_SMName" runat="server" CompletionInterval="100"
                                                                                            CompletionListCssClass="ULCustomerName" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                                                            CompletionListItemCssClass="listItem" ContextKey="SMName" DelimiterCharacters=""
                                                                                            Enabled="True" EnableViewState="False" FirstRowSelected="True" MinimumPrefixLength="0"
                                                                                            OnClientItemSelected="SetSelectedValue" ServiceMethod="GetCompletionList" ServicePath=""
                                                                                            TargetControlID="txt_SMName" UseContextKey="True">
                                                                                        </asp:AutoCompleteExtender>
                                                                                    </td>
                                                                                    <td class="style73" style="text-align: center; color: #003366; font-size: small;
                                                                                        font-family: 'Times New Roman', Times, serif;">
                                                                                        <asp:Button ID="btn_Search" runat="server" CssClass="button" TabIndex="13" Text="Search"
                                                                                            UseSubmitBehavior="False" Width="80px"></asp:Button>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Button ID="btn_ClearS" runat="server" CssClass="button" TabIndex="14" Text="Clear"
                                                                                            UseSubmitBehavior="False" Width="80px" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="LabelHeader">
                                                                        <asp:Label ID="Label1" runat="server" Text="Approval Details"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Panel ID="PanelCourseMaster0" runat="server" Height="250px" ScrollBars="Both"
                                                                            Style="float: left; color: Blue; font-family: Times New Roman; background-color: White;"
                                                                            Width="100%">
                                                                            <asp:GridView ID="gdv_ApprovalSettings" runat="server" AutoGenerateColumns="False"
                                                                                Font-Names="Verdana" Font-Size="11px" ShowHeaderWhenEmpty="True" CellPadding="3"
                                                                                TabIndex="-1" BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px" Width="980px"
                                                                                Style="font-family: 'Times New Roman', Times, serif" BorderStyle="None" AllowPaging="True">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="DocID" HeaderText="Doc. ID" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="DocDate" HeaderText="Date" HtmlEncode="False" HtmlEncodeFormatString="False"
                                                                                        DataFormatString="{0:dd-MMM-yyyy}">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                                                                        <ItemStyle HorizontalAlign="Left" Width="110px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="SmID" HeaderText="Sem. ID" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="EmpID" HeaderText="Emp. ID" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                                                        <ItemStyle HorizontalAlign="Left" Width="70px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="Teacher" HeaderText="Teacher" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                                                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="CSID" HeaderText="Course" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                                                                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="CourseName" HeaderText="Course" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                                                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="PreCourseID" HeaderText="Pre-Requisites Course ID" HtmlEncode="False"
                                                                                        HtmlEncodeFormatString="False">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                                                                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="PreReqCSName" HeaderText="Pre-Requisites Course Name"
                                                                                        HtmlEncode="False" HtmlEncodeFormatString="False">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="Scode" HeaderText="Student ID" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                                                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="StudentName" HeaderText="Student Name" HtmlEncode="False"
                                                                                        HtmlEncodeFormatString="False">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                                                                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="Comments" HeaderText="Comments" HtmlEncode="False" HtmlEncodeFormatString="False" />
                                                                                    <asp:BoundField DataField="Regdate" HeaderText="Reg. Date" HtmlEncode="False" HtmlEncodeFormatString="False"
                                                                                        DataFormatString="{0:dd-MMM-yyyy}">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                                                                        <ItemStyle HorizontalAlign="Left" Width="110px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="Status" HeaderText="Status" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                                                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:CommandField ShowDeleteButton="True">
                                                                                        <HeaderStyle Width="50px" />
                                                                                        <ItemStyle ForeColor="Red" Width="50px" />
                                                                                    </asp:CommandField>
                                                                                </Columns>
                                                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                                                <HeaderStyle CssClass="Grid" BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                                                <RowStyle ForeColor="#000066" />
                                                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Button ID="btn_Print" runat="server" CssClass="button" TabIndex="15" Text="Print"
                                                                            UseSubmitBehavior="False" Width="80px"></asp:Button>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:TabPanel>
                                            </asp:TabContainer>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style19">
                                        </td>
                                        <td>
                                            <table width="100%">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lbl_msg" runat="server" align="center"></asp:Label>
                                                        <asp:Button ID="btnConfirm" runat="server" Text="Close" Style="display: none;" />
                                                        <asp:HiddenField ID="Hdn_Grid" runat="server" />
                                                        <asp:HiddenField ID="hdn_index" runat="server" />
                                                        <asp:HiddenField ID="hdn_Control" runat="server" />
                                                        <asp:HiddenField ID="hdn_TeachersName" runat="server" />
                                                        <asp:HiddenField ID="hdn_SemsName" runat="server" />
                                                        <asp:HiddenField ID="hdn_exportExcel" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <%-- <asp:UpdateProgress ID="UpdateProgressAll" runat="server" AssociatedUpdatePanelID="UpdatePanelAll">
                                            <ProgressTemplate>
                                                <div style="width: 100%;">
                                                    <img src="../Images/load.gif" alt="" width="100px" height="100px" align="middle"
                                                        style="color: White" />
                                                    &nbsp;<font color="white"><b>Please Wait...!</b></font>
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>--%>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hdn_ViewAttchDoc" runat="server" />
                        <asp:ModalPopupExtender ID="ModalPopupExtenderViewAttachDoc" runat="server" PopupControlID="DivViewAttach"
                            BackgroundCssClass="modalBackground" TargetControlID="hdn_ViewAttchDoc">
                        </asp:ModalPopupExtender>
                        <div id="DivViewAttach" style="display: none;" runat="server" class="popupSub">
                            <div class="headerPop" align="center">
                                View Attached Documents
                            </div>
                            <div class="body">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnl_ViewAttchdDoc" runat="server" BackColor="#F7F6F3" Height="160px"
                                                ScrollBars="Both" Width="880px">
                                                <asp:GridView ID="gdv_ViewAttachdDoc" runat="server" AutoGenerateColumns="False"
                                                    Font-Names="Verdana" Font-Size="11px" ShowHeaderWhenEmpty="True" CellPadding="3"
                                                    TabIndex="-1" BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px" Width="850px"
                                                    Style="font-family: 'Times New Roman', Times, serif" BorderStyle="None" HeaderStyle-CssClass="Grid">
                                                    <Columns>
                                                        <asp:BoundField DataField="DocDate" HeaderText="Document Date" DataFormatString="{0:dd-MMM-yyyy}">
                                                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                            <ItemStyle HorizontalAlign="left" Width="80px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Semester" HeaderText="Semester">
                                                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                            <ItemStyle HorizontalAlign="left" Width="70px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="StudentID" HeaderText="Student ID">
                                                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                            <ItemStyle HorizontalAlign="left" Width="70px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="StudentName" HeaderText="Student Name" HtmlEncode="False"
                                                            HtmlEncodeFormatString="False">
                                                            <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                                            <ItemStyle HorizontalAlign="left" Width="250px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FileName" HeaderText="File Name" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                            <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                            <ItemStyle HorizontalAlign="left" Width="150px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FilePath" HeaderText="File Path" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                            <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                            <ItemStyle HorizontalAlign="left" Width="150px" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk_View" ForeColor="#474747" Font-Underline="false" Font-Bold="true"
                                                                    runat="server" Text="View" OnClick="lnk_View_Click">
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="73px" />
                                                            <ItemStyle Width="73px"></ItemStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                    <RowStyle ForeColor="#000066" />
                                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btn_CloseRegStud" runat="server" Style="height: 25px; text-align: center"
                                                TabIndex="20" Text="Close" Width="80px" CssClass="button" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hdn_Find" runat="server" />
                        <asp:HiddenField ID="hdn_DocID" runat="server" />
                        <asp:Button ID="btn_Confirm" runat="server" CssClass="confirm" Text="Button" />
                        <asp:HiddenField ID="Hdn_Value" runat="server" />
                        <asp:ValidationSummary ID="ValidationSummaryCourse" runat="server" ShowSummary="false"
                            ShowMessageBox="true" ValidationGroup="Course" />
                        <asp:HiddenField ID="hdn_Mode" runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
