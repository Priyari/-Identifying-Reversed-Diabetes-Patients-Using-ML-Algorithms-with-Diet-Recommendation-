<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/Doctor.Master" AutoEventWireup="true" CodeBehind="_RFAlgorithm.aspx.cs" Inherits="diseasePrediction.Doctor._RFAlgorithm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="panelAdminAccount" runat="server">
  <div class="article">
			<h2>Diabetes Disease Prediction Using RF Algorithm</h2>

            <table style="width:100%;">
                <tr>
                    <td>
                        <h3>
                            Result Analysis</h3>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Table ID="tableResults" runat="server">
                        </asp:Table>
                    </td>
                </tr>
            </table>
            <br />

            <table style="width:100%;">
                <tr>
                    <td>
                        <asp:Table ID="Table1" runat="server">
                        </asp:Table>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <br />
            <br />
            
    <br />
           
    

        <br />
         
   
   </div>
 
   </asp:Panel>
</asp:Content>
