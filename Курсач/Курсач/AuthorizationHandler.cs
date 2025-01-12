using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Курсач.Common;

namespace Курсач
{
    public class AuthorizationHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var token = CommonData.Token;

                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                return await base.SendAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка с передачей токена");
            }

        }
    }

}
