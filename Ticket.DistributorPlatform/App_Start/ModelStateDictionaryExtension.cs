using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ticket.DistributorPlatform.App_Start
{
    public static class ModelStateDictionaryExtension
    {
        public static string BuildErrorMessage(this ModelStateDictionary modelStates)
        {
            IList<string> errorMessages = (from modelState in modelStates.Values
                                           from error in modelState.Errors
                                           select error.ErrorMessage).ToList();
            return string.Join(" ", errorMessages);
        }
    }
}