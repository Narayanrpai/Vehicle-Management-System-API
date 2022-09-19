using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleAPI.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "Vehicle name must be Added")]
        public string VehicleName { get; set; }

        [Required(ErrorMessage = "Vehicle type must be Added")]
        public string VehicleType { get; set; }

        [Required(ErrorMessage = "Vehicle complaint must be Added")]
        public string VehicleComplaint { get; set; }

        public DateTime? CreatedDate { get; set; }

        //[MinimumRepairCost]
        [Required(ErrorMessage = "Vehicle repaircost must be Added")]
        public double RepairCost { get; set; }
        public string CreatedBy { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        //public Vehicle()
        //{
        //    Category = new Category();
        //}
    }
}
