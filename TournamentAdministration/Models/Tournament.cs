﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentAdmin.Models
{
    public class Tournament
    {
        public int ID { get; set; }
        public string TournamentName { get; set; }
        public DateTime EventTime { get; set; }
        public List<Player> Players { get; set; }
        public Game Game { get; set; }
        public Venue Venue {get;set;}
        [Required]
        public string UserID { get; set; }
        public IdentityUser User { get; set; }
    }
}
