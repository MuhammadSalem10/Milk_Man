using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MilkMan.Shared.Interfaces;

namespace MilkMan.API.Filters
{
    public class ModelValidationFilter(ILoggerManager logger) : IAsyncActionFilter
    {
        private readonly ILoggerManager _logger = logger;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                _logger.Warning($"Request validation failed for action {context.ActionDescriptor.DisplayName}");
                foreach (var modelStateEntry in context.ModelState.Values)
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        _logger.Warning($" - {error.ErrorMessage}");
                    }
                }
                // Return BadRequest with details about validation errors
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
            else
            {
                await next();
            }
        }
    }



}
