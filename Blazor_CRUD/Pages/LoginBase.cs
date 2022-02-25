using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazor_CRUD.Data;
using DataService.Entity;
using DataService.IService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Blazor_CRUD.Pages
{
    public class LoginBase : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        public User user { get; set; } = new User();
        public string LoginMesssage { get; set; }
        ClaimsPrincipal claimsPrincipal;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            user = new User();

            claimsPrincipal = (await authenticationStateTask).User;

            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                ((CustomAuthStateProvider)AuthenticationStateProvider).UserIsLoggedOut();
            }
            

        }

        protected async Task<bool> ValidateUser()
        {
            var loginResult = await UserService.CheckLoginAsync(user);
            if (loginResult != null)
            {
                if (loginResult.Status)
                {
                     ((CustomAuthStateProvider)AuthenticationStateProvider).UserAuthenticated(loginResult);
                     NavigationManager.NavigateTo("/");
                }
                else
                {
                    LoginMesssage = "User Inactivated";
                }
            }
            else
            {
                LoginMesssage = "Invalid username or password";
            }
            return await Task.FromResult(true);

            

        }
    }
}
