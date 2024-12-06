<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/Doctor.Master" AutoEventWireup="true" CodeBehind="Individual_KNN.aspx.cs" Inherits="diseasePrediction.Doctor.Individual_KNN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="panelAdminHome" runat="server">
 <div class="article">
			<h2>Diabetes and Reverse Diabetes Using KNN Algorithm</h2>
            <table style="width: 70%; height: 124px;">
                <tr>
                    <td class="style2">
                        <strong>Enter Patient Id</strong></td>
                    <td class="style4">
                       
                         <asp:DropDownList ID="DropDownListPatients" runat="server" 
                            Width="200px" 
                            >
                        </asp:DropDownList>
                       
                       
                       </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;</td>
                    <td class="style4">
                        <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" 
                            Text="Search" />
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;</td>
                    <td class="style4">
                        &nbsp;</td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong>Name</strong></td>
                    <td class="style4">
                        <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong>Mobile</strong></td>
                    <td class="style4">
                        <asp:Label ID="lblMobile" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong>Address</strong></td>
                    <td class="style4">
                        <asp:Label ID="lblAddress" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;</td>
                    <td class="style4">
                        &nbsp;</td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong>Gender</strong></td>
                    <td class="style4">
                        <asp:TextBox ID="txtGender" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtGender" ErrorMessage="*" Font-Size="X-Small" 
                            ForeColor="#FF3300" ToolTip="Enter Gender" ValidationGroup="user">Enter Gender</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="txtGender" CssClass="error" ErrorMessage="Invalid" 
                            ToolTip="Invalid" ValidationExpression="^[12]$" ValidationGroup="user">Invalid</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong>Age</strong></td>
                    <td class="style4">
                        <asp:TextBox ID="txtAge" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                            ControlToValidate="txtAge" ErrorMessage="*" Font-Size="X-Small" 
                            ForeColor="#FF3300" ToolTip="Enter Age" ValidationGroup="user">Enter Age</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                            ControlToValidate="txtAge" CssClass="error" ErrorMessage="Invalid" 
                            ToolTip="Invalid" ValidationExpression="\d+" ValidationGroup="user">Invalid</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <strong>Hypertension</strong></td>
                    <td class="style1">
                        <asp:TextBox ID="txtHypertension" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="style1">
                        &nbsp;</td>
                    <td class="style1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                            ControlToValidate="txtHypertension" ErrorMessage="*" Font-Size="X-Small" 
                            ForeColor="#FF3300" ToolTip="Enter Hypertension" ValidationGroup="user">Enter Hypertension</asp:RequiredFieldValidator>
                    </td>
                    <td class="style1">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                            ControlToValidate="txtHypertension" CssClass="error" ErrorMessage="Invalid" 
                            ToolTip="Invalid" ValidationExpression="^[01]$" ValidationGroup="user">Invalid</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong>Heart Disease</strong></td>
                    <td class="style4">
                        <asp:TextBox ID="txtHeartdisease" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtHeartdisease" ErrorMessage="*" Font-Size="X-Small" 
                            ForeColor="#FF3300" ToolTip="Enter Heartdisease" ValidationGroup="user">Enter Heartdisease</asp:RequiredFieldValidator>
                        &nbsp;</td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                            ControlToValidate="txtHeartdisease" CssClass="error" ErrorMessage="Invalid" 
                            ToolTip="Invalid" ValidationExpression="^[01]$" ValidationGroup="user">Invalid</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong>Smoking Hisotory</strong></td>
                    <td class="style4">
                        <asp:TextBox ID="txtSmoking" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtSmoking" ErrorMessage="*" Font-Size="X-Small" 
                            ForeColor="#FF3300" ToolTip="Enter Smoking History" ValidationGroup="user">Enter Smoking History</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                            ControlToValidate="txtSmoking" CssClass="error" ErrorMessage="Invalid" 
                            ToolTip="Invalid" ValidationExpression="\d+" ValidationGroup="user">Invalid</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                       <strong> BMI</strong></td>
                    <td class="style4">
                        <asp:TextBox ID="txtBMI" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                            ControlToValidate="txtBMI" ErrorMessage="*" Font-Size="X-Small" 
                            ForeColor="#FF3300" ToolTip="Enter BMI" ValidationGroup="user">Enter BMI</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
                            ControlToValidate="txtBMI" CssClass="error" ErrorMessage="Invalid" 
                            ToolTip="Invalid" ValidationExpression="^(0|[1-9]\d*)(\.\d+)?$" 
                            ValidationGroup="user">Invalid</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong> HbA1c_level</strong></td>
                    <td class="style4">
                        <asp:TextBox ID="txtHBA1C" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                            ControlToValidate="txtHBA1C" ErrorMessage="*" Font-Size="X-Small" 
                            ForeColor="#FF3300" ToolTip="Enter HbA1c" 
                            ValidationGroup="user">Enter HbA1c</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" 
                            ControlToValidate="txtHBA1C" CssClass="error" ErrorMessage="Invalid" 
                            ToolTip="Invalid" ValidationExpression="^(0|[1-9]\d*)(\.\d+)?$" 
                            ValidationGroup="user">Invalid</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <strong> Blood_glucose_level</strong></td>
                    <td class="style4">
                        <asp:TextBox ID="txtBloodGlucose" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ControlToValidate="txtBloodGlucose" ErrorMessage="*" Font-Size="X-Small" 
                            ForeColor="#FF3300" ToolTip="Enter Glucose" ValidationGroup="user">Enter Glucose</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" 
                            ControlToValidate="txtBloodGlucose" CssClass="error" ErrorMessage="Invalid" 
                            ToolTip="Invalid" ValidationExpression="^(0|[1-9]\d*)(\.\d+)?$" 
                            ValidationGroup="user">Invalid</asp:RegularExpressionValidator>
                    </td>
                </tr>
                
                <tr>
                    <td class="style2">
                        &nbsp;</td>
                    <td class="style4">
                        &nbsp;</td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;</td>
                    <td class="style4">
                        &nbsp;</td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;</td>
                    <td class="style4">
                        <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                            Text="Diabetes Prediction" ValidationGroup="user" Height="50px" 
                            Width="200px" />
                    </td>
                    <td class="style5">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
    <br />
           
   

                        <h2 class="title"><span>
                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label></span></h2>
                                 <br />
                                 <br />


                                 <asp:Button ID="btnTime" runat="server" Height="50px" 
                onclick="btnTime_Click" Text="Click here for Reverse Diabetes Prediction" ValidationGroup="user" 
                Visible="False" Width="450px" />
                                 <br />
            <br />
            <h2 class="title"> <span>
            <asp:Label ID="lblReverse" runat="server"></asp:Label>

            </span>
   
                <h2 class="title">
                    <br />
                </h2>
         
   
   </div>
 
   </asp:Panel>
</asp:Content>
