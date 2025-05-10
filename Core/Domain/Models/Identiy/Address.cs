﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Identiy
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;   
        public ApplicationUser User { get; set; } = null!;
        [ForeignKey("User")]
        public string UserId { get; set; } = null!;

    }
}