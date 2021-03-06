using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TournamentAdmin.Models;
using TournamentAdministration.Data;

namespace TournamentAdministration.Pages.Players
{
    public class EditModel : PageModel
    {
        private readonly TournamentAdminContext database;
        private readonly AccessControl accessControl;

        public EditModel(TournamentAdminContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }

        public Player Player { get; set; }

        public async Task<IActionResult> OnPostAsync(int id, Player player)
        {
            Player = await database.Player.FindAsync(id);

            Player.FirstName = player.FirstName;
            Player.LastName = player.LastName;
            Player.GameHandle = player.GameHandle;
            Player.CountryOfOrigin = player.CountryOfOrigin;
            Player.HomeTown = player.HomeTown;

            await database.SaveChangesAsync();
            return RedirectToPage("/Players/Create");
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Player = await database.Player.FindAsync(id);
            return Page();
        }
    }
}
