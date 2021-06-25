using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Core.Models
{
    public class Workshift
    {
        public int Id { get; set; }

        public int? ShopId { get; set; }

        [Required]
        public Shop Shop { get; set; }

        public int? MerchendiserId { get; set; }

        [Required]
        public Merchendiser Merchendiser { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
