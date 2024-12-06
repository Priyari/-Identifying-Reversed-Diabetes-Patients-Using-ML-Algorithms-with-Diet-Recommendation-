<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="frmParameters.aspx.cs" Inherits="diseasePrediction.Admin.frmParameters" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="panelFeatureTypes" runat="server">
  <div class="article">
			<h2>Parameters used to Predict Diabetes and Reverse Diabetes</h2>
            <asp:Panel ID="Panel1" runat="server" Visible="False">
                <table style="width: 50%; ">
                    <tr>
                        <td class="style2">
                            &nbsp;</td>
                        <td class="style4">
                            &nbsp;</td>
                        <td class="style5">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <strong>Parameter</strong></td>
                        <td class="style4">
                            <asp:TextBox ID="txtType" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td class="style5">
                            &nbsp;</td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtType" ErrorMessage="*" Font-Size="X-Small" 
                                ForeColor="#FF3300" ToolTip="Enter Parameter" ValidationGroup="user">Enter Type</asp:RequiredFieldValidator>
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
            </asp:Panel>
    
   
                       <%-- <h2 class="title"><span>View Parameters</span></h2>--%>
                            <table style="width:100%;">

                                <tr>
                                    <td>
                                        <asp:Table ID="tableParameters" runat="server">
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
 

       
   
  
   </asp:Panel>
</asp:Content>
