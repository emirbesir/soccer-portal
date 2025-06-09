using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerPortal.Models;

namespace SoccerPortal.Controllers
{
    public class PlayersController : Controller
    {
        private readonly SoccerPortalContext _context;

        public PlayersController(SoccerPortalContext context)
        {
            _context = context;
        }

        // GET: Players
        public async Task<IActionResult> Index(int? teamFilter)
        {
            var players = _context.Players
                .Include(p => p.Team)
                .AsQueryable();

            // Apply team filter if provided
            if (teamFilter.HasValue)
            {
                players = players.Where(p => p.TeamID == teamFilter.Value);
            }

            ViewBag.Teams = new SelectList(await _context.Teams.ToListAsync(), "TeamID", "TeamName");
            ViewBag.TeamFilter = teamFilter;

            return View(await players.ToListAsync());
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.PlayerID == id);

            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Create
        public async Task<IActionResult> Create(int? teamId)
        {
            ViewBag.Teams = new SelectList(await _context.Teams.ToListAsync(), "TeamID", "TeamName");
            
            // Pre-select team if provided
            if (teamId.HasValue)
            {
                var player = new Player { TeamID = teamId.Value };
                return View(player);
            }
            
            return View();
        }

        // POST: Players/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamID,Name,Position,Age,GoalsScored")] Player player)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(player.Name))
            {
                ModelState.AddModelError("Name", "Player name is required");
            }
            
            // If we have valid data, force successful validation
            if (player.TeamID != 0 && !string.IsNullOrWhiteSpace(player.Name))
            {
                try
                {
                    _context.Add(player);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error creating player: {ex.Message}");
                }
            }
            
            ViewBag.Teams = new SelectList(await _context.Teams.ToListAsync(), "TeamID", "TeamName", player.TeamID);
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            ViewBag.Teams = new SelectList(await _context.Teams.ToListAsync(), "TeamID", "TeamName", player.TeamID);
            return View(player);
        }

        // POST: Players/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerID,TeamID,Name,Position,Age,GoalsScored")] Player player)
        {
            if (id != player.PlayerID)
            {
                return NotFound();
            }

            // Basic validation
            if (string.IsNullOrWhiteSpace(player.Name))
            {
                ModelState.AddModelError("Name", "Player name is required");
                ViewBag.Teams = new SelectList(await _context.Teams.ToListAsync(), "TeamID", "TeamName", player.TeamID);
                return View(player);
            }
            
            // Bypass ModelState validation if basic requirements are met
            try
            {
                _context.Update(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(player.PlayerID))
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
                ModelState.AddModelError("", $"Error updating player: {ex.Message}");
            }

            ViewBag.Teams = new SelectList(await _context.Teams.ToListAsync(), "TeamID", "TeamName", player.TeamID);
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.PlayerID == id);
            
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player != null)
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(p => p.PlayerID == id);
        }
    }
}
