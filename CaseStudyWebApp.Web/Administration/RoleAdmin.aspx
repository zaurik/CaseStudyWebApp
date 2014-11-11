<%@ Page Title="Role Administration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="RoleAdmin.aspx.cs" Inherits="CaseStudyWebApp.Web.Administration.RoleAdministration" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <div class="row">
        <section id="usersGridSection">
            <h3>Users</h3>
            <div class="form-horizontal">
                <asp:GridView runat="server" ID="AllRolesGrid" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" 
                    CssClass="userGrid" DataKeyNames="Id" Width="30%"
                    OnRowDataBound="AllRolesGrid_RowDataBound" OnRowCreated="AllRolesGrid_RowCreated" 
                    OnSelectedIndexChanged="AllRolesGrid_SelectedIndexChanged">
                    <EmptyDataTemplate>
                        <label>No roles found!</label>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField HeaderText="Role" DataField="Name" />
                    </Columns>
                </asp:GridView>
            </div>
        </section>
        <hr />
        <section>
            <h3>Role Details</h3>
            <div class="div-table">
                <div>
                    <asp:HiddenField runat="server" ID="RoleId" />
                    <asp:HiddenField runat="server" ID="Action" />
                    <span>
                        <label>Role name</label>
                    </span>
                    <span>
                        <asp:TextBox runat="server" ID="RoleNameTextBox" />
                    </span>                    
                </div>                
            </div>

            <div class="buttons-row">
                <asp:Button runat="server" ID="AddButton" Text="Add" OnClick="AddButton_Click" />
                <asp:Button runat="server" ID="EditButton" Text="Edit" OnClick="EditButton_Click" />
                <asp:Button runat="server" ID="SaveButton" Text="Save" OnClick="SaveButton_Click" />
                <asp:Button runat="server" ID="DeleteButton" Text="Delete" OnClick="DeleteButton_Click" />
            </div>

        </section>
    </div>
</asp:Content>
