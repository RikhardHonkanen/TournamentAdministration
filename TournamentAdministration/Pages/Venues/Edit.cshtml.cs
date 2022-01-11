using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TournamentAdmin.Models;
using TournamentAdministration.Data;

namespace TournamentAdministration.Pages.Venues
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

        public Venue Venue { get; set; }
        public Coordinate Coordinate { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public async Task<IActionResult> OnPostAsync(int id, Venue venue, double longitude, double latitude)
        {
            Venue = await database.Venue.FindAsync(id);

            var result = database.Venue.FirstOrDefault(n => n.VenueName == venue.VenueName);

            //Should be == null but than you cant have same name as before
            if (result == null)
            {
                Venue.Coordinate.Longitude = longitude;
                Venue.Coordinate.Latitude = latitude;
                Venue.VenueName = venue.VenueName;
                //Venue.Coordinate = Coordinate;

                await database.SaveChangesAsync();
                return RedirectToPage("/Venues/Create");
            }
            else if (result != null)
            {
                Venue.Coordinate.Longitude = longitude;
                Venue.Coordinate.Latitude = latitude;
                Venue.VenueName = venue.VenueName;
                //Venue.Coordinate = Coordinate;

                await database.SaveChangesAsync();
                return RedirectToPage("/Venues/Create");
            }
            else
            {
                ViewData["Message"] = ("Venue name already exists");
                return Page();
            }
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Venue = await database.Venue.FindAsync(id);
            return Page();
        }
    }
}
