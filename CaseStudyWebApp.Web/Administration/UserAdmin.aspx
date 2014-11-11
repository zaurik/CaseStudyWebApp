<%@ Page Title="User Administration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserAdmin.aspx.cs"
    Inherits="CaseStudyWebApp.Web.Administration.UserAdmin" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <div class="row">
        <section id="usersGridSection">
            <h3>Users</h3>
            <div class="form-horizontal">
                <asp:GridView runat="server" ID="AllUsersGrid" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false"
                    CssClass="userGrid" DataKeyNames="Id" Width="30%"
                    OnRowDataBound="AllUsersGrid_RowDataBound" OnRowCreated="AllUsersGrid_RowCreated"
                    OnSelectedIndexChanged="AllUsersGrid_SelectedIndexChanged">
                    <EmptyDataTemplate>
                        <label>No users found!</label>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField HeaderText="User Name" DataField="UserName" />
                        <asp:BoundField HeaderText="Email Address" DataField="Email" />
                    </Columns>
                </asp:GridView>
            </div>
        </section>
        <hr />
        <section>
            <h3>User Details</h3>
            <div class="row">
                <asp:ValidationSummary runat="server" DisplayMode="BulletList" CssClass="text-danger" />
            </div>
            <div class="div-table">
                <div>
                    <asp:HiddenField runat="server" ID="UserId" />
                    <asp:HiddenField runat="server" ID="Action" />
                    <span>
                        <label>User name</label>
                    </span>
                    <span>
                        <asp:TextBox runat="server" ID="UserNameTextBox" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="UserNameTextBox" Display="None"
                            ErrorMessage="The user name field is required." />
                    </span>
                    <span>
                        <label>Password</label>
                    </span>
                    <span>
                        <asp:TextBox runat="server" ID="PasswordTextBox" TextMode="Password" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="PasswordTextBox" Display="None"
                            ErrorMessage="The password field is required." ValidationGroup="password" />
                    </span>
                </div>
                <div>
                    <span>
                        <label>Email Address</label>
                    </span>
                    <span>
                        <asp:TextBox runat="server" ID="EmailTextBox" TextMode="Email" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="EmailTextBox" Display="None"
                            ErrorMessage="The email field is required." ValidationGroup="password" />
                    </span>
                    <span>
                        <label>Confirm Password</label>
                    </span>
                    <span>
                        <asp:TextBox runat="server" ID="ConfirmPasswordTextBox" TextMode="Password" />
                    </span>
                </div>
                <div>
                    <span>
                        <label>User Role</label>
                    </span>
                    <span>
                        <asp:DropDownList runat="server" ID="UserRoleList"></asp:DropDownList>
                    </span>
                </div>
            </div>

            <div class="buttons-row">
                <asp:Button runat="server" ID="AddButton" Text="Add" OnClick="AddButton_Click" CausesValidation="false" />
                <asp:Button runat="server" ID="EditButton" Text="Edit" OnClick="EditButton_Click" CausesValidation="false" />
                <asp:Button runat="server" ID="SaveButton" Text="Save" OnClick="SaveButton_Click" />
                <asp:Button runat="server" ID="DeleteButton" Text="Delete" OnClick="DeleteButton_Click" CausesValidation="false" />
                <asp:Button runat="server" ID="ChangePasswordButton" Text="Change Password" OnClick="ChangePasswordButton_Click" />
            </div>

        </section>
    </div>
</asp:Content>
