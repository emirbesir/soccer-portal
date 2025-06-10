using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerPortal.Models;

namespace SoccerPortal.Controllers
{
    public class MatchesController : Controller
    {
        private readonly SoccerPortalContext _context;

        public MatchesController(SoccerPortalContext context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index(string? teamFilter, string? venueFilter, DateTime? dateFilter)
        {
            var matches = _context.Matches
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Include(m => m.Venue)
                .AsQueryable();

            // Apply filters if provided
            if (!string.IsNullOrEmpty(teamFilter))
            {
                int teamId;
                if (int.TryParse(teamFilter, out teamId))
                {
                    matches = matches.Where(m => m.Team1ID == teamId || m.Team2ID == teamId);
                }
            }

            if (!string.IsNullOrEmpty(venueFilter))
            {
                int venueId;
                if (int.TryParse(venueFilter, out venueId))
                {
                    matches = matches.Where(m => m.VenueID == venueId);
                }
            }

            if (dateFilter.HasValue)
            {
                matches = matches.Where(m => m.MatchDate.Date == dateFilter.Value.Date);
            }

            // Get all teams and venues for filter dropdowns
            ViewBag.Teams = new SelectList(await _context.Teams.ToListAsync(), "TeamID", "TeamName");
            ViewBag.Venues = new SelectList(await _context.Venues.ToListAsync(), "VenueID", "VenueName");

            // Order by match date with upcoming matches first, then past matches
            var orderedMatches = await matches
                .OrderBy(m => m.MatchDate < DateTime.Now) // False (upcoming) comes before True (past)
                .ThenBy(m => m.MatchDate)
                .ToListAsync();

            ViewBag.TeamFilter = teamFilter;
            ViewBag.VenueFilter = venueFilter;
            ViewBag.DateFilter = dateFilter?.ToString("yyyy-MM-dd");

            return View(orderedMatches);
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Include(m => m.Venue)
                .Include(m => m.Fixtures)
                .FirstOrDefaultAsync(m => m.MatchID == id);

            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }        // GET: Matches/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Teams = new SelectList(await _context.Teams.ToListAsync(), "TeamID", "TeamName");
            ViewBag.Venues = new SelectList(await _context.Venues.ToListAsync(), "VenueID", "VenueName");
            return View();
        }

        // POST: Matches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Team1ID,Team2ID,MatchDate,Score,VenueID")] Match match)
        {
            // Basic validation
            if (match.Team1ID == match.Team2ID && match.Team1ID != 0)
            {
                ModelState.AddModelError("Team2ID", "Home team and away team cannot be the same.");
            }

            // Ensure Score can be empty string or null for future matches
            if (match.Score == null || match.Score.Trim() == "")
            {
                match.Score = string.Empty; // Explicitly set to empty string for upcoming matches
            }

            // Force ModelState to be valid if basic requirements are met
            if (match.Team1ID != 0 && match.Team2ID != 0 && match.VenueID != 0 && match.Team1ID != match.Team2ID)
            {
                try
                {
                    _context.Add(match);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error creating match: {ex.Message}");
                }
            }
            
            // If we got this far, something failed, redisplay form
            ViewBag.Teams = new SelectList(await _context.Teams.ToListAsync(), "TeamID", "TeamName");
            ViewBag.Venues = new SelectList(await _context.Venues.ToListAsync(), "VenueID", "VenueName");
            return View(match);
        }        // GET: Matches/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Include(m => m.Venue)
                .FirstOrDefaultAsync(m => m.MatchID == id);
                
            if (match == null)
            {
                return NotFound();
            }

            ViewBag.Teams = new SelectList(await _context.Teams.ToListAsync(), "TeamID", "TeamName");
            ViewBag.Venues = new SelectList(await _context.Venues.ToListAsync(), "VenueID", "VenueName");
            return View(match);
        }

        // POST: Matches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("MatchID,Team1ID,Team2ID,MatchDate,Score,VenueID")] Match match)
        {
            if (id != match.MatchID)
            {
                return NotFound();
            }

            // Basic validation only for same teams
            if (match.Team1ID == match.Team2ID)
            {
                ModelState.AddModelError("Team2ID", "Home team and away team cannot be the same.");
                ViewBag.Teams = new SelectList(await _context.Teams.ToListAsync(), "TeamID", "TeamName");
                ViewBag.Venues = new SelectList(await _context.Venues.ToListAsync(), "VenueID", "VenueName");
                return View(match);
            }

            // Bypass ModelState validation if we have basic requirements met
            try
            {
                _context.Update(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(match.MatchID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                // Add error message to be displayed in the view
                ModelState.AddModelError("", $"Error updating match: {ex.Message}");
            }

            ViewBag.Teams = new SelectList(await _context.Teams.ToListAsync(), "TeamID", "TeamName");
            ViewBag.Venues = new SelectList(await _context.Venues.ToListAsync(), "VenueID", "VenueName");
            return View(match);
        }        // GET: Matches/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Include(m => m.Venue)
                .FirstOrDefaultAsync(m => m.MatchID == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                _context.Matches.Remove(match);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.MatchID == id);
        }
    }
}
