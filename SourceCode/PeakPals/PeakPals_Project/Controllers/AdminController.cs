using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeakPals_Project.Areas.Identity.Data;
using PeakPals_Project.Data;
using PeakPals_Project.Models;
using SendGrid.Helpers.Mail;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly PeakPalsContext _context;

    public AdminController(UserManager<ApplicationUser> userManager, PeakPalsContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public class UserClimberPair
    {
        public ApplicationUser User { get; set; }
        public Climber Climber { get; set; }
    }

    public class UserClimberViewModel
    {
        public List<UserClimberPair> UserClimberPairs { get; set; }
    }

    public async Task<IActionResult> UserList()
    {
        var users = await _userManager.Users.ToListAsync();
        var viewModel = new UserClimberViewModel
        {
            UserClimberPairs = users.Select(user => new UserClimberPair
            {
                User = user,
                Climber = _context.Climber.FirstOrDefault(c => c.AspnetIdentityId == user.Id) ?? new Climber()
            })
            .ToList()
        };

        return View(viewModel);
    }


}
