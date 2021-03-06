using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TournamentAdministration.Data;
using TournamentAdmin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TournamentAdministration.Pages.Tournaments
{
    public class CreateModel : PageModel
    {
        private readonly TournamentAdminContext database;
        private readonly AccessControl accessControl;

        public CreateModel(TournamentAdminContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }

        public List<Venue> Venues { get; set; }
        public List<Game> Games { get; set; }
        public List<Player> Players { get; set; }
        public Tournament Tournament { get; set; }
        public Game Game { get; private set; }
        public Venue Venue { get; private set; }
        public Player Player { get; private set; }

        private async Task GetModelData()
        {
            Venues = await database.Venue.ToListAsync();
            Games = await database.Game.ToListAsync();
            Players = await database.Player.ToListAsync();
        }        

        public async Task<IActionResult> OnPostAsync(Tournament tournament, Game game, Venue venue)
        {
            var result = database.Tournament.FirstOrDefault(t => t.TournamentName == tournament.TournamentName);

            if (tournament.EventTime < DateTime.Today)
            {
                ViewData["Message"] = "Tournament date cant be earlier than today";
                await GetModelData();
                return Page();
            }
            else if (result != null)
            {
                ViewData["Message"] = "Tournament name already exists";
                await GetModelData();
                return Page();
            }
            else
            {
                Tournament = new Tournament
                {
                    UserID = accessControl.LoggedInUserID,
                    TournamentName = tournament.TournamentName,
                    Description = tournament.Description,
                    EventTime = tournament.EventTime,
                    Game = await database.Game.Where(g => g.ID == game.ID).SingleAsync(),
                    Venue = await database.Venue.Where(v => v.ID == venue.ID).SingleAsync()
                };

                await database.Tournament.AddAsync(Tournament);
                await database.SaveChangesAsync();
                return RedirectToPage("/Index");
            }
        }

        public async Task<IActionResult> OnGetDelete(int id)
        {
            var tournament = await database.Tournament.FindAsync(id);

            database.Tournament.Remove(tournament);
            await database.SaveChangesAsync();
            await GetModelData();
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await GetModelData();
            return Page();
        }
    }
}
