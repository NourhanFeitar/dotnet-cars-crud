using API_Lab_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API_Lab_1.Filter
{
    public class ValidateCarTypeAttribute: ActionFilterAttribute
    {
        //Runs Before Endpoint while Executed Runs After Endpoint
       
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Car? car = context.ActionArguments["car"] as Car;
            var AllowedTypes = "Gas|Hybrid|Electric|Diesel".Split("|");
            if (car is null || !AllowedTypes.Contains(car.Type))
            {
                //
                context.ModelState.AddModelError("Type", "Type Is Not Valid");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
