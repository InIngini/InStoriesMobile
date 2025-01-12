using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Курсач.Core.Errors
{
    public static class ErrorsDeserialization
    {
        public static async Task<string> Deserialization(Exception ex)
        {
            string errorMessage = $"Произошла ошибка: {ex.Message}";

            if (ex.Data != null && ex.Data.Contains("Content"))
            {
                var content = ex.Data["Content"].ToString();

                var apiError = JsonConvert.DeserializeObject<ApiError>(content);
                errorMessage = $"{apiError.Message}\n{string.Join("\n", apiError.Details ?? Enumerable.Empty<string>())}";
            }

            return errorMessage;
        }
    }
}
