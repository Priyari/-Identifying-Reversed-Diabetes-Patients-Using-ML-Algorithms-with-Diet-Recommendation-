<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="frmAdminHome.aspx.cs" Inherits="diseasePrediction.Admin.frmAdminHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="panelAdminHome" runat="server">
  <div class="article">
			<h2>Welcome Admin Home!!!</h2>
            <table style="width: 80%; height: 124px;">
                <tr>
                    <td class="style2">
                        <strong>User Type</strong></td>
                    <td class="style4">
                        <asp:DropDownList ID="DropDownListType" runat="server">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>Doctor</asp:ListItem>
                            <asp:ListItem>Receptionist</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                            ControlToValidate="DropDownListType" ErrorMessage="*" Font-Size="X-Small" 
                            ForeColor="#FF3300" Operator="NotEqual" ToolTip="Select User Type" 
                            ValidationGroup="user" ValueToCompare="Select">Select User Type</asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong>UserId</strong></td>
                    <td class="style4">
                        <asp:TextBox ID="txtUserId" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtUserId" ErrorMessage="*" Font-Size="X-Small" 
                            ForeColor="#FF3300" ToolTip="Enter UserId" ValidationGroup="user">Enter UserId</asp:RequiredFieldValidator>
                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator5" 
                            runat="server" ControlToValidate="txtUserId" CssClass="error" ErrorMessage="*" 
                            ToolTip="Only Alphabets and Numbers Allowed" 
                            ValidationExpression="[a-zA-Z0-9]*$" ValidationGroup="user">Only Alphabets and Numbers Allowed</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong>Password</strong></td>
                    <td class="style4">
                        <asp:TextBox ID="txtPassword" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtPassword" ErrorMessage="*" Font-Size="X-Small" 
                            ForeColor="#FF3300" ToolTip="Enter Password" ValidationGroup="user">Enter Password</asp:RequiredFieldValidator>
                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator6" 
                            runat="server" ControlToValidate="txtPassword" CssClass="error" 
                            ErrorMessage="*" ToolTip="Minimum 6 Chars" 
                            ValidationExpression="[0-9a-zA-Z]{6,}" ValidationGroup="user">Minimum 6 Chars</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong>EmailId</strong></td>
                    <td class="style4">
                        <asp:TextBox ID="txtEmailId" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtEmailId" ErrorMessage="*" Font-Size="X-Small" 
                            ForeColor="#FF3300" ToolTip="Enter EmailId" ValidationGroup="user">Enter EmailId</asp:RequiredFieldValidator>
                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                            runat="server" ControlToValidate="txtEmailId" ErrorMessage="*" 
                            Font-Size="X-Small" ForeColor="#FF3300" ToolTip="Invalid EmailId" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                            ValidationGroup="user">Invalid EmailId</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;</td>
                    <td class="style4">
                        <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                            Text="Submit" ValidationGroup="user" />
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
    <br />
           
   

        <br />

   
                        <h2 class="title"><span>View Existing Staffs</span></h2>
                            <table style="width:100%;">

                                <tr>
                                    <td>
                                        <asp:Table ID="tableStaffs" runat="server">
                                        </asp:Table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                                 <br />
         
   
   </div>
 

        <br />
         
   
 
   </asp:Panel>

</asp:Content>
