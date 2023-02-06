using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Week6_Lectorial.ViewModels;

namespace Week6_Lectorial.Controllers;

public class MultistepFormWithSessionController : Controller
{
    private const string SessionKey_FirstName = $"{nameof(MultistepFormWithSessionController)}_FirstName";
    private const string SessionKey_LastName = $"{nameof(MultistepFormWithSessionController)}_LastName";

    public IActionResult Step1() => View(GetMultiStepFormViewModel());

    [HttpPost]
    public IActionResult Step1(MultiStepFormViewModel viewModel)
    {
        // Only check validation for the relevant field of this step instead of all fields.
        if(ModelState.GetValidationState(nameof(viewModel.FirstName)) != ModelValidationState.Valid)
            return View(viewModel);

        HttpContext.Session.SetString(SessionKey_FirstName, viewModel.FirstName);

        return RedirectToAction(nameof(Step2));
    }

    public IActionResult Step2() => View(GetMultiStepFormViewModel());

    [HttpPost]
    public IActionResult Step2(MultiStepFormViewModel viewModel)
    {
        // Only check validation for the relevant field of this step instead of all fields.
        if(ModelState.GetValidationState(nameof(viewModel.LastName)) != ModelValidationState.Valid)
            return View(viewModel);

        HttpContext.Session.SetString(SessionKey_LastName, viewModel.LastName);

        return RedirectToAction(nameof(Complete));
    }

    public IActionResult Complete() => View(GetMultiStepFormViewModel());

    private MultiStepFormViewModel GetMultiStepFormViewModel() =>
        new()
        {
            FirstName = HttpContext.Session.GetString(SessionKey_FirstName),
            LastName = HttpContext.Session.GetString(SessionKey_LastName)
        };
}
