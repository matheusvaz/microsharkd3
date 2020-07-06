using Common.Domain.Multi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace Api.Filters
{
    public class NotificationErrorFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context?.Controller as Controllers.Controller;

            if (controller != null && controller.Invalid)
            {
                var errorResult = new ErrorResult()
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = Translation.Key("VALIDATION.RESPONSE_TITLE"),
                    Status = 400,
                    TraceId = context.HttpContext.TraceIdentifier,
                    Errors = controller.GetNotifications()
                };

                context.Result = new BadRequestObjectResult(errorResult);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context) { }
    }

    public class ErrorResult
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }
    }
}
