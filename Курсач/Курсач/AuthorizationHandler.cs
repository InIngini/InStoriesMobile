using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Курсач.Services;
using Курсач.Core.Interfaces;

namespace Курсач
{
    public class AuthorizationHandler : DelegatingHandler
    {
        private readonly IUserService UserService;

        public AuthorizationHandler(UserService userService)
        {
            UserService = userService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = UserService.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }

}
