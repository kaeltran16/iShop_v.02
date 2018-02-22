using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace iShop.Web.Extensions
{
    public static class ModelStateExtension
    {
        public static string GetError(this ModelStateDictionary modelState)
        {
            var error = modelState.Values.SelectMany(g => g.Errors)
                .First(e => e.ErrorMessage != string.Empty || e.Exception != null);

            return error.ErrorMessage != string.Empty ? error.ErrorMessage : error.Exception.ToString();
        }
    }
}
