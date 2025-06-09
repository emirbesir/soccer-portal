using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerPortal.Models;

namespace SoccerPortal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SoccerPortalContext _context;

    public HomeController(ILogger<HomeController> logger, SoccerPortalContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Get upcoming matches (ordered by date)
        var upcomingMatches = await _context.Matches
            .Include(m => m.Team1)
            .Include(m => m.Team2)
            .Include(m => m.Venue)
            .Where(m => m.MatchDate > DateTime.Now)
            .OrderBy(m => m.MatchDate)
            .Take(5)
            .ToListAsync();

        // Get recent results (ordered by date descending)
        var pastMatches = await _context.Matches
            .Include(m => m.Team1)
            .Include(m => m.Team2)
            .Include(m => m.Venue)
            .Where(m => m.MatchDate <= DateTime.Now && !string.IsNullOrEmpty(m.Score))
            .OrderByDescending(m => m.MatchDate)
            .Take(5)
            .ToListAsync();

        var viewModel = new HomeViewModel
        {
            UpcomingMatches = upcomingMatches,
            PastMatches = pastMatches
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

