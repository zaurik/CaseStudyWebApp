using CaseStudyWebApp.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;

namespace CaseStudyWebApp.Web.Administration
{
    public partial class UserAdmin : System.Web.UI.Page
    {
        ApplicationDbContext dbContext;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
                GetAllRoles();
                DisableDetailsForm();
                SetButtonState(true, false, false, false, false);
            }
        }

        protected void AllUsersGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.AllUsersGrid, "Select$" + e.Row.RowIndex);
            }
        }

        protected void AllUsersGrid_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal)
                {
                    e.Row.CssClass = "normal";
                }
                else if (e.Row.RowState == DataControlRowState.Alternate)
                {
                    e.Row.CssClass = "alternate";
                }
            }
        }

        protected void AllUsersGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedUserId = ((GridView)sender).SelectedDataKey.Value.ToString();
            dbContext = new ApplicationDbContext();
            ShowUserInfo(dbContext.Users.Where(u => u.Id == selectedUserId).FirstOrDefault());
            SetButtonState(true, true, false, true, true);
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            this.UserId.Value = string.Empty;
            this.Action.Value = "Add";
            this.DeleteButton.Text = "Cancel";

            ClearDetailsForm();
            EnableDetailsForm();
            EnablePasswordFields();
            SetButtonState(false, false, true, true, false);
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            this.Action.Value = "Edit";
            EnablePasswordFields();
            this.DeleteButton.Text = "Cancel";

            SetButtonState(false, false, true, true, false);
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            switch (this.Action.Value.ToString())
            {
                case "Add":
                    AddNewUser();

                    DisableDetailsForm();
                    SetButtonState(true, true, false, false, true);                    
                    break;
                case "Edit":
                    dbContext = new ApplicationDbContext();
                    
                    UpdateUserInfo(this.UserId.Value);
                    DisableDetailsForm();
                    SetButtonState(true, true, false, true, true);
                    ShowUserInfo(dbContext.Users.Where(u => u.Id == this.UserId.Value.ToString()).FirstOrDefault());
                    break;
                default:
                    throw new NotImplementedException();
            }

            this.DeleteButton.Text = "Delete";
            this.Action.Value = string.Empty;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            switch (this.Action.Value.ToString())
            {
                case "Add":
                    ClearDetailsForm();
                    SetButtonState(true, false, false, false, false);
                    break;
                case "Edit":
                    SetButtonState(true, true, false, true, true);
                    dbContext = new ApplicationDbContext();
                    ShowUserInfo(dbContext.Users.Where(u => u.Id == this.UserId.Value.ToString()).FirstOrDefault());
                    break;
                default:
                    DeleteUser();
                    ClearDetailsForm();
                    SetButtonState(true, false, false, false, false);
                    break;
            }

            DisableDetailsForm();
        }

        protected void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            manager.RemovePassword(this.UserId.Value.ToString());
            manager.AddPassword(this.UserId.Value.ToString(), this.PasswordTextBox.Text);
        }

        private List<ApplicationUser> GetAllUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            return context.Users.ToList();
        }

        private void BindGridView()
        {
            this.AllUsersGrid.DataSource = GetAllUsers();
            this.AllUsersGrid.DataBind();
        }

        private void ShowUserInfo(ApplicationUser selectedUser)
        {
            if (selectedUser != null)
            {
                this.UserId.Value = selectedUser.Id;
                this.UserNameTextBox.Text = selectedUser.UserName;
                this.EmailTextBox.Text = selectedUser.Email;
                this.PasswordTextBox.Enabled = false;
                this.ConfirmPasswordTextBox.Enabled = false;

                GetAllRoles();

                if (selectedUser.Roles.Count > 0)
                {
                    for (int i = 0; i < UserRoleList.Items.Count; i++)
                    {
                        if (UserRoleList.Items[i].Value == selectedUser.Roles.FirstOrDefault().RoleId)
                        {
                            UserRoleList.Items[i].Selected = true;
                        }
                    }
                }
            }
        }

        private void AddNewUser()
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = new ApplicationUser()
            {
                UserName = this.UserNameTextBox.Text,
                Email = this.EmailTextBox.Text,
            };

            IdentityResult result = manager.Create(user, this.PasswordTextBox.Text);

            if (result.Succeeded)
            {
                dbContext = new ApplicationDbContext();
                ApplicationUser createdUser = dbContext.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();

                if (this.UserRoleList.SelectedIndex != 0)
                {
                    var store = new UserStore<ApplicationUser>(dbContext);
                    var roleManager = new UserManager<ApplicationUser>(store);

                    IdentityUserRole selectedUserRole = new IdentityUserRole()
                    {
                        RoleId = this.UserRoleList.SelectedValue,
                        UserId = this.UserId.Value.ToString()
                    };

                    createdUser.Roles.Add(selectedUserRole);
                    createdUser = dbContext.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();

                    manager.UpdateAsync(createdUser);
                    store.Context.SaveChanges();
                }

                this.ShowUserInfo(createdUser);
                BindGridView();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void UpdateUserInfo(string userId)
        {
            dbContext = new ApplicationDbContext();
            var store = new UserStore<ApplicationUser>(dbContext);
            var manager = new UserManager<ApplicationUser>(store);

            var userToUpdate = dbContext.Users.Where(u => u.Id == userId).FirstOrDefault();

            userToUpdate.UserName = this.UserNameTextBox.Text;
            userToUpdate.Email = this.EmailTextBox.Text;
            userToUpdate.Roles.Clear();

            if (this.UserRoleList.SelectedIndex != 0)
            {
                IdentityUserRole selectedUserRole = new IdentityUserRole()
                {
                    RoleId = this.UserRoleList.SelectedValue,
                    UserId = this.UserId.Value.ToString()
                };

                userToUpdate.Roles.Add(selectedUserRole);
            }

            manager.UpdateAsync(userToUpdate);
            store.Context.SaveChanges();
        }

        private void DeleteUser()
        {
            dbContext = new ApplicationDbContext();
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = manager.FindById(this.UserId.Value.ToString());
            //var user = dbContext.Users.Where(u => u.Id == this.UserId.Value.ToString()).FirstOrDefault();

            IdentityResult result = manager.Delete(user);

            if (result.Succeeded)
            {
                BindGridView();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void GetAllRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            this.UserRoleList.DataTextField = "Name";
            this.UserRoleList.DataValueField = "Id";
            this.UserRoleList.DataSource = context.Roles.ToList();
            this.UserRoleList.DataBind();

            this.UserRoleList.Items.Insert(0, new ListItem("- Select a role -", "0"));
        }

        private void ClearDetailsForm()
        {
            this.UserNameTextBox.Text = string.Empty;
            this.EmailTextBox.Text = string.Empty;
            this.PasswordTextBox.Text = string.Empty;
            this.ConfirmPasswordTextBox.Text = string.Empty;
            this.UserRoleList.SelectedIndex = 0;
        }

        private void DisableDetailsForm()
        {
            this.UserNameTextBox.Enabled = false;
            this.EmailTextBox.Enabled = false;
            this.UserRoleList.Enabled = false;
            this.PasswordTextBox.Enabled = false;
            this.ConfirmPasswordTextBox.Enabled = false;
        }

        private void EnableDetailsForm()
        {
            this.UserNameTextBox.Enabled = true;
            this.EmailTextBox.Enabled = true;
            this.UserRoleList.Enabled = true;
        }

        private void EnablePasswordFields()
        {
            this.PasswordTextBox.Enabled = true;
            this.ConfirmPasswordTextBox.Enabled = true;
        }

        private void SetButtonState(bool isAddEnable, bool isEditEnable, bool isSaveEnable, bool isDeleteEnable, bool isChangePasswordEnable)
        {
            this.AddButton.Enabled = isAddEnable;
            this.EditButton.Enabled = isEditEnable;
            this.SaveButton.Enabled = isSaveEnable;
            this.DeleteButton.Enabled = isDeleteEnable;
            this.ChangePasswordButton.Enabled = isChangePasswordEnable;
        }

    }
}