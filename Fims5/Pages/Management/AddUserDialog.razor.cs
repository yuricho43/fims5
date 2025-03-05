﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Telerik.Blazor;
using Telerik.Blazor.Components;
using Telerik.DataSource;
using static Telerik.Blazor.ThemeConstants;

using Microsoft.AspNetCore.Components.Authorization;
using Fims5.Data.Identity;

namespace Fims5.Pages.Management
{
    public partial class AddUserDialog
    {
        //[CascadingParameter]
        //public Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Parameter]
        public EventCallback<RegisterRequestModel> AddUserFinished { get; set; }

        public RegisterRequestModel NewUserRegisterRequestModel { get; set; } = new RegisterRequestModel();
        private List<string> Roles { get; set; } = new List<string> { "Inspector", "Closer", "Reporter", "Manager", "Admin"};

        public TelerikForm AddUserFormRef { get; set; }
        public TelerikNotification AddUserNotificationComponent { get; set; }
        public bool ShowErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }


        //protected override void OnInitialized()
        //{
        //    base.OnInitialized();
        //}
        //
        //protected override async Task OnInitializedAsync()
        //{
        //    var state = await this.AuthStateProvider.GetAuthenticationStateAsync();
        //    var user = state.User;
        //    //var authState = await AuthenticationStateTask;
        //    //var user = authState.User;
        //
        //    ProductModels = await TSheetSpecsClientService.GetEquipmentModelsAsync();
        //}

        public FormValidationMessageType ValidationMessageType { get; set; } = FormValidationMessageType.Tooltip;
        public List<FormValidationMessageType> ValidationMessageTypes { get; set; } = new List<FormValidationMessageType>()
        {
            FormValidationMessageType.None,
            FormValidationMessageType.Inline,
            FormValidationMessageType.Tooltip
        };
        public bool ValidSubmit { get; set; } = false;

        async void HandleValidSubmit()
        {
            ValidSubmit = true;

            var result = await this.IdentitySvc.RegisterUserAsync(this.NewUserRegisterRequestModel);

            if (result.Succeeded)
            {
                await ActivateAlert("사용자등록 성공", NewUserRegisterRequestModel.HangulName + ", Id=" + NewUserRegisterRequestModel.UserName);
                this.ShowErrors = false;

                AddUserNotificationComponent.Show(new NotificationModel
                {
                    Text = "사용자 등록 성공",
                    ThemeColor = "error",
                    CloseAfter = 3000
                });

                await AddUserFinished.InvokeAsync(NewUserRegisterRequestModel); // pass Param to parent, by calling EventCallback
                //this.NavigationManager.NavigateTo("/account/login");
            }
            else
            {
                this.Errors = (IEnumerable<string>)(result.Errors);
                this.ShowErrors = true;
                await ActivateAlert("사용자등록 실패", "서버연결상태를 점검하세요.");
            }

            ValidSubmit = false;

            StateHasChanged();
        }

        void HandleInvalidSubmit()
        {
            ValidSubmit = false;
        }

        void OnAddUserDialogOK()
        {
            //ValidSubmit = false;
        }

        void OnAddUserDialogCancel()
        {
            //ValidSubmit = false;
            AddUserFinished.InvokeAsync(NewUserRegisterRequestModel); // pass Param to parent, by calling EventCallback
        }


        [CascadingParameter]
        public DialogFactory Dialogs { get; set; }
        public async Task ActivateAlert(string title, string message)
        {
            if (string.IsNullOrWhiteSpace(title)) title = "Warning!";
            if (string.IsNullOrWhiteSpace(message)) message = "Something went wrong!";

            await Dialogs.AlertAsync(message, title);
        }
    }
}
