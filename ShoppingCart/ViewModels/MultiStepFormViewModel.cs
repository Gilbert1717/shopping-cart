using System.ComponentModel.DataAnnotations;

namespace Week6_Lectorial.ViewModels;

public class MultiStepFormViewModel
{
    [Display(Name = "First name")]
    [Required, StringLength(30)]
    public string FirstName { get; set; }

    [Display(Name = "Last name")]
    [Required, StringLength(30)]
    public string LastName { get; set; }
}
