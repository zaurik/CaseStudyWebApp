<%@ Page Title="Calculator" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Calculator.aspx.cs" Inherits="CaseStudyWebApp.Web.Calculator.Calculator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="div-table" style="width:35%">
            <div>
                <span>
                    <asp:TextBox runat="server" ID="FirstValueTextBox" Style="width: 30px;" required />
                </span>
                <span>
                    <asp:DropDownList runat="server" ID="OperationDropDownList">
                        <asp:ListItem Value="+">Add</asp:ListItem>
                        <asp:ListItem Value="-">Subtract</asp:ListItem>
                        <asp:ListItem Value="*">Multiply</asp:ListItem>
                        <asp:ListItem Value="/">Divide By</asp:ListItem>
                    </asp:DropDownList></span>
                <span>
                    <asp:TextBox runat="server" ID="SecondValueTextBox" Style="width: 30px;" required/></span>
                <span>
                    <asp:Button runat="server" ID="CalculateButton" Text="Calculate" OnClick="CalculateButton_Click" />
                    <asp:Button runat="server" ID="ResetButton" Text="Reset" formnovalidate OnClick="ResetButton_Click" />
                </span>
            </div>
            <div>
                <span style="column-span: all; text-align: center">
                    <asp:Label runat="server" ID="ResultLabel"></asp:Label></span>
            </div>
        </div>
    </div>
</asp:Content>
