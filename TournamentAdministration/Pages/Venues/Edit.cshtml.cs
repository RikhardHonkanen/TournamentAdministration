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
        public Address Address { get; set; }

        public async Task<IActionResult> OnPostAsync(int id, Venue venue)
        {
            Venue = await database.Venue.FindAsync(id);

            var result = database.Venue.FirstOrDefault(n => n.VenueName == venue.VenueName);

            if (result != null && venue.VenueName != Venue.VenueName)
            {
                ViewData["Message"] = ("Venue name already exists");
                return Page();
            }
            else
            {
                Venue.Coordinate.Longitude = venue.Coordinate.Longitude;
                Venue.Coordinate.Latitude = venue.Coordinate.Latitude;
                Venue.Address.Street = venue.Address.Street;
                Venue.Address.Postcode = venue.Address.Postcode;
                Venue.Address.City = venue.Address.City;
                Venue.Address.Country = venue.Address.Country;
                Venue.VenueName = venue.VenueName;

                await database.SaveChangesAsync();
                return RedirectToPage("/Venues/Create");  
            }
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Venue = await database.Venue.FindAsync(id);
            return Page();
        }
    }
}
