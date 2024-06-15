// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// #nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PeakPals_Project.Areas.Identity.Data;
using PeakPals_Project.DAL.Abstract;
#nullable enable

namespace PeakPals_Project.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IClimberRepository _climberRepository;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IClimberRepository climberRepository) // Add IClimberRepository as a parameter
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _climberRepository = climberRepository; // Initialize _climberRepository
        }


        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; } = "";

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; } = "";

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; } = "";

            [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username can only contain alphanumeric characters and underscores.")]
            public string? UserName { get; set; }
            public int? Age { get; set; }

            public string? Gender { get; set; }

            [Display(Name = "Height (in)")]
            public int? Height { get; set; }

            [Display(Name = "Weight (lbs)")]
            public int? Weight { get; set; }

            [Display(Name = "Climbing Experience")]
            public string? ClimbingExperience { get; set; }

            [Display(Name = "Max Climbing Grade")]
            public int? MaxClimbGrade { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName ?? throw new InvalidOperationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

            Input = new InputModel
            {
                UserName = userName,
                Age = user.Age,
                Gender = user.Gender,
                Height = user.Height,
                Weight = user.Weight,
                ClimbingExperience = user.ClimbingExperience,
                MaxClimbGrade = user.MaxClimbGrade,
                PhoneNumber = phoneNumber?? ""
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            // validate phone number to make sure it has no obvious XSS vulnerabilities
            if (!Regex.IsMatch(Input.PhoneNumber, @"^[0-9]+$"))
            {
                StatusMessage = "Phone number can only contain numbers.";
                return RedirectToPage();
            }

            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            // Update the username
            if (Input.UserName != null && Input.UserName != user.UserName)
            {
                // validate the username to make sure it has no obvious XSS vulnerabilities
                if (!Regex.IsMatch(Input.UserName, @"^[a-zA-Z0-9_]+$"))
                {
                    StatusMessage = "Username can only contain alphanumeric characters and underscores.";
                    return RedirectToPage();
                }
                var existingUser = await _userManager.FindByNameAsync(Input.UserName);
                if (existingUser != null)
                {
                    StatusMessage = "Username already exists. Please choose a different one.";
                    return RedirectToPage();
                }

                user.UserName = Input.UserName;
                var setUserNameResult = await _userManager.UpdateAsync(user);
                if (!setUserNameResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set user name.";
                    return RedirectToPage();
                }

                // Refresh the sign-in cookie
                await _signInManager.RefreshSignInAsync(user);

                // Update the username in the Climber model
                var climber = _climberRepository.GetClimberModelByAspNetIdentityId(user.Id);
                if (climber != null)
                {
                    climber.UserName = Input.UserName;
                    _climberRepository.UpdateUserName(user.Id, Input.UserName);
                }
            }

            if (Input.Age != user.Age)
            {
                user.Age = Input.Age;
            }

            if (Input.Gender != user.Gender)
            {
                user.Gender = Input.Gender;
            }

            if (Input.Height != user.Height)
            {
                user.Height = Input.Height;
            }

            if (Input.Weight != user.Weight)
            {
                user.Weight = Input.Weight;
            }

            if (Input.ClimbingExperience != user.ClimbingExperience)
            {
                user.ClimbingExperience = Input.ClimbingExperience;
            }

            if (Input.MaxClimbGrade != user.MaxClimbGrade)
            {
                user.MaxClimbGrade = Input.MaxClimbGrade;
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to update your profile.";
                return RedirectToPage();
            }

            await _signInManager.RefreshSignInAsync(user);

            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

    }
}
