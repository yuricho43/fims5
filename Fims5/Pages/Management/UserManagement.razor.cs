using Fims5.Common;
using Fims5.Data.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Blazor;
using Telerik.Blazor.Components;
using Fims5.Data.Entities;

namespace Fims5.Pages.Management
{
    public partial class UserManagement
    {
        int PageSize = 15;
        public List<UserAuthInfoModel> UserAuthInfoModels { get; set; }
        public List<FimsRole> UserRoles { get; set; }
        public List<string> UserRoleNames { get; set; } = new List<string>();
        public string UserRoleName { get; set; }
        public bool EnabledValidation { get; set; } = true;
        public bool AddUserDialogVisible { get; set; } = false;
        public TelerikNotification UserManagementNotificationComponent { get; set; }
        public bool ShowErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }

        //--- Roles
        private IList<string> currentUserRoles;
        private bool bIsAdmin { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            currentUserRoles = await IdentitySvc.GetCurrentUserRolesAsync();
            string role = currentUserRoles.First().ToString();
            if (role == "Admin")
                bIsAdmin = true;
            else
                bIsAdmin = false;

            await LoadData();

            UserRoleNames.Clear();
            List<IdentityRole> UserRoles = IdentitySvc.GetAllRoles();
            foreach (var userRole in UserRoles)
            {
                UserRoleNames.Add(userRole.Name);
            }

            StateHasChanged();
        }

        private async Task LoadData()
        {
            if (bIsAdmin == false)
                return;

            UserAuthInfoModels = await IdentitySvc.AllUsers();
            UserAuthInfoModels = UserAuthInfoModels.OrderBy(n => n.UserName).ToList();
            return;

            /*
            UserAuthInfoModels.Clear();
            var allUsers = UserManager.Users.ToList();
            foreach (var user in allUsers)
            {
                var roles = await UserManager.GetRolesAsync(user);
                UserAuthInfoModels.Add(new UserAuthInfoModel
                {
                    HangulName = user.HangulName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = roles.ToList().Last()
                });
            }

            UserAuthInfoModels = UserAuthInfoModels.OrderBy(n => n.UserName).ToList();
            */
        }

        private async void ChangeUserRole(GridCommandEventArgs args)
        {
            var model = (UserAuthInfoModel)args.Item;
            var result = await this.IdentitySvc.ChangeUserRoleAsync(model);

            if (result.Succeeded)
            {
                this.ShowErrors = false;

                UserManagementNotificationComponent.Show(new NotificationModel
                {
                    Text = $"사용자({model.UserName}) 권한변경({model.Role}) 성공",
                    ThemeColor = "success",
                    //CloseAfter = 3000
                });

                await LoadData();
                StateHasChanged();
            }
            else
            {
                this.Errors = (IEnumerable<string>)(result.Errors);
                this.ShowErrors = true;
            }
        }

        private async Task ResetPassword(UserAuthInfoModel model)
        {
            model.Password = Constants.FimsDefaultPassword;
            var result = await this.IdentitySvc.ResetPasswordAsync(model.UserName, model.Password);

            if (result.Succeeded)
            {
                this.ShowErrors = false;

                UserManagementNotificationComponent.Show(new NotificationModel
                {
                    Text = $"사용자({model.UserName}) 암호초기화({Constants.FimsDefaultPassword}) 성공",
                    ThemeColor = "success",
                    //CloseAfter = 3000
                });
            }
            else
            {
                this.Errors = (IEnumerable<string>) (result.Errors);
                this.ShowErrors = true;
            }
        }

        private async Task DeleteUser(GridCommandEventArgs args)
        {
            bool confirmed = await Dialogs.ConfirmAsync("해당 계정은 영구 삭제 됩니다.", "계정 삭제");

            if (confirmed)
            {
                var userinfo = (UserAuthInfoModel)args.Item;

                var result = await this.IdentitySvc.DeleteUserAsync(userinfo.UserName);

                if (result.Succeeded)
                {
                    this.ShowErrors = false;

                    UserManagementNotificationComponent.Show(new NotificationModel
                    {
                        Text = "사용자 계정삭제 성공",
                        ThemeColor = "success",
                        //CloseAfter = 3000
                    });
                }
                else
                {
                    this.Errors = (IEnumerable<string>)(result.Errors);
                    this.ShowErrors = true;
                }

                await LoadData();
            }

        }

        private void ShowAddUserDialog(GridCommandEventArgs args)
        {
            if (bIsAdmin == false)
                return;
            AddUserDialogVisible = true;
            //StateHasChanged();
        }

        private async void OnAddUserFinished(RegisterRequestModel newRegisterRequestModel)
        {
            AddUserDialogVisible = false;
            await LoadData();
            StateHasChanged();
        }


        [CascadingParameter]
        public DialogFactory Dialogs { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public async Task ActivateAlert()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                await Dialogs.AlertAsync("Something went wrong!");
            }
            else
            {
                await Dialogs.AlertAsync("Something went wrong!", Title);
            }

            Message = "The user saw and closed an Alert dialog.";
        }

        public async Task ActivateConfirm()
        {
            bool confirmed;
            if (string.IsNullOrWhiteSpace(Title))
            {
                confirmed = await Dialogs.ConfirmAsync("Are you sure?");
            }
            else
            {
                confirmed = await Dialogs.ConfirmAsync("Are you sure?", Title);
            }

            var confirmMessage = confirmed ? "confirmed" : "canceled";

            Message = $"The user {confirmMessage} the dialog.";
        }

        public async Task ActivatePrompt()
        {
            string input;
            if (string.IsNullOrWhiteSpace(Title))
            {
                input = await Dialogs.PromptAsync("Please, enter your answer.");
            }
            else
            {
                input = await Dialogs.PromptAsync("Please, enter your answer.", Title);
            }

            Message = $"The returned user input is \"{input}\".";
        }

    }
}
