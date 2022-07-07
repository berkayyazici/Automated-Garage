using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatedGarage.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public int PlateNumber { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public int ParkSpotId { get; set; }
        public int ParkSpotFloor { get; set; }
        public int ticketNumber { get; set; }
        public string ParkSpotName { get; set; }
        public DateTime ParkSpotDate { get; set; }


    }
}
