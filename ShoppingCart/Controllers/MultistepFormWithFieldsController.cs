using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Week6_Lectorial.ViewModels;

namespace Week6_Lectorial.Controllers;

public class MultistepFormWithFieldsController : Controller
{
    public IActionResult Step1() => View();

    [HttpPost]
    public IActionResult Step2(MultiStepFormViewModel viewModel)
    {
        // Only check validation for the relevant fields of the previous step instead of all fields.
        if(ModelState.GetValidationState(nameof(viewModel.FirstName)) != ModelValidationState.Valid)
            return View(nameof(Step1), viewModel);

        // Clear validation state for last name - last name is validated in the next step (after form submission).
        ModelState.ClearValidationState(nameof(viewModel.LastName));

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Complete(MultiStepFormViewModel viewModel)
    {
        // Only check validation for the relevant fields of the previous step instead of all fields.
        if(ModelState.GetValidationState(nameof(viewModel.LastName)) != ModelValidationState.Valid)
            return View(nameof(Step2), viewModel);

        // Validate all fields and go back to Step 1 if any data is invalid.
        if(!ModelState.IsValid)
            return View(nameof(Step1), viewModel);

        return View(viewModel);
    }
}
