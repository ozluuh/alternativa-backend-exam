using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = new Dictionary<string, string>();

                IEnumerable<string> errorsMessage = context.ModelState
                    .Keys.SelectMany(key => context.ModelState[key]
                        .Errors.Select(x => $"{key.ToLower()}|{x.ErrorMessage}")
                    );

                errors = errorsMessage
                    .ToDictionary(key => key.Split("|")[0],
                                value => value.Split("|")[1]);

                context.Result = new BadRequestObjectResult(errors);
            }
        }
    }
}
