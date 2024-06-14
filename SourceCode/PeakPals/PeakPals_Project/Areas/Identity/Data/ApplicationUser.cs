using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using ScottPlot.Renderable;
#nullable enable

namespace PeakPals_Project.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Range(1, 125,
            ErrorMessage = "Age must be between 1 and 125 years")]
        public int? Age { get; set; }

        [PersonalData]
        [StringLength(20)]
        public string? Gender { get; set; }

        [PersonalData]
        [Range(12, 120,
            ErrorMessage = "Height must be between 12 and 120 inches")]
        public int? Height { get; set; }

        [PersonalData]
        [Range(50, 1000,
            ErrorMessage = "Weight must be between 10 and 1000 pounds")]
        public int? Weight { get; set; }
        
        [PersonalData]
        [StringLength(15)]
        public string? ClimbingExperience { get; set; }

        [PersonalData]
        [StringLength(5,
            ErrorMessage = "Climbing grade must be 5 characters or less")]
        public int? MaxClimbGrade { get; set; }
    }
}
