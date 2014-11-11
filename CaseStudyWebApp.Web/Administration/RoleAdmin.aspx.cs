using CaseStudyWebApp.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CaseStudyWebApp.Web.Administration
{
    public partial class RoleAdministration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
                DisableDetailsForm();
                this.EditButton.Enabled = false;
                this.SaveButton.Enabled = false;
                this.DeleteButton.Enabled = false;
            }
        }

        protected void AllRolesGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.AllRolesGrid, "Select$" + e.Row.RowIndex);
            }
        }

        protected void AllRolesGrid_RowCreated(object sender, GridViewRowEventArgs e)
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

        protected void AllRolesGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRoleId = ((GridView)sender).SelectedDataKey.Value.ToString();
            ApplicationDbContext context = new ApplicationDbContext();
            ShowRoleInfo(context.Roles.Where(u => u.Id == selectedRoleId).FirstOrDefault());
            this.EditButton.Enabled = true;
            this.DeleteButton.Enabled = true;
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            this.RoleNameTextBox.Enabled = true;
            this.RoleNameTextBox.Text = string.Empty;
            this.RoleId.Value = string.Empty;
            this.Action.Value = "Add";
            this.DeleteButton.Text = "Cancel";
            this.AddButton.Enabled = false;
            this.EditButton.Enabled = false;
            this.SaveButton.Enabled = true;
            this.DeleteButton.Enabled = true;
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            EnableDetailsForm();
            this.Action.Value = "Edit";
            this.AddButton.Enabled = false;
            this.EditButton.Enabled = false;
            this.SaveButton.Enabled = true;
            this.DeleteButton.Text = "Cancel";
            this.DeleteButton.Enabled = true;
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            switch (this.Action.Value)
            {
                case "Add":
                    AddNewRole();
                    break;
                case "Edit":
                    break;
                default:
                    break;
            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            switch (this.Action.Value.ToString())
            {
                case "Add":
                    ClearDetailsForm(true);
                    DisableDetailsForm();
                    this.AddButton.Enabled = true;
                    this.EditButton.Enabled = false;
                    this.DeleteButton.Enabled = false;
                    break;
                case "Edit":
                    this.AddButton.Enabled = true;
                    this.EditButton.Enabled = true;
                    this.DeleteButton.Enabled = true;
                    DisableDetailsForm();
                    ApplicationDbContext dbContext = new ApplicationDbContext();
                    IdentityRole role = dbContext.Roles.Where(r => r.Id == this.RoleId.Value.ToString()).FirstOrDefault();
                    ShowRoleInfo(role);
                    break;
                default:
                    throw new NotImplementedException();
            }

            this.DeleteButton.Text = "Delete";
            this.SaveButton.Enabled = false;
        }

        private List<IdentityRole> GetAllRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            return context.Roles.ToList();
        }

        private void BindGridView()
        {
            this.AllRolesGrid.DataSource = GetAllRoles();
            this.AllRolesGrid.DataBind();
        }

        private void ShowRoleInfo(IdentityRole selectedRole)
        {
            this.RoleId.Value = selectedRole.Id;
            this.RoleNameTextBox.Text = selectedRole.Name;
        }

        private void AddNewRole()
        {
            IdentityRole newRole = new IdentityRole()
            {
                Name = this.RoleNameTextBox.Text
            };

            ApplicationDbContext dbContext = new ApplicationDbContext();
            var store = new RoleStore<IdentityRole>(dbContext);
            var manager = new RoleManager<IdentityRole>(store);

            manager.CreateAsync(newRole);
            store.Context.SaveChanges();

            BindGridView();
            ClearDetailsForm(true);
            DisableDetailsForm();
            this.DeleteButton.Text = "Delete";
            this.AddButton.Enabled = true;
            this.EditButton.Enabled = false;
            this.SaveButton.Enabled = false;
            this.DeleteButton.Enabled = false;
        }

        private void CancelAddEdit()
        {
            switch (this.Action.Value.ToString())
            {
                case "Add":
                    ClearDetailsForm(true);
                    DisableDetailsForm();
                    this.AddButton.Enabled = true;
                    this.EditButton.Enabled = true;
                    this.SaveButton.Enabled = false;
                    this.DeleteButton.Enabled = true;
                    break;
                case "Edit":
                    //TODO - Get role details
                    DisableDetailsForm();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void ClearDetailsForm(bool clearHiddenFields)
        {
            if (clearHiddenFields)
            {
                this.RoleId.Value = string.Empty;
                this.Action.Value = string.Empty;
            }

            this.RoleNameTextBox.Text = string.Empty;
        }

        private void EnableDetailsForm()
        {
            this.RoleNameTextBox.Enabled = true;
        }

        private void DisableDetailsForm()
        {
            this.RoleNameTextBox.Enabled = false;
        }
    }
}