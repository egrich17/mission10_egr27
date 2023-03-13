using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9.Models
{
    public class Purchase
    {
        [Key]
        // want this to be secure, don't pass through URL
        [BindNever]
        public int PurchaseId { get; set; }

        [BindNever]
        public ICollection<BasketLineItem> Lines { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter the first address line.")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "Please enter a city name.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter a state name.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter a zip code.")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter a country name.")]
        public string Country { get; set; }

    }
}
