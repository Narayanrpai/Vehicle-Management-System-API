using System;

namespace VehicleAPI.Models
{
    public class VehicleCategory
    {
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }
        public string VehicleType { get; set; }
        public string CategoryName { get; set; }
        public string VehicleComplaint { get; set; }
        public DateTime? CreatedDate { get; set; }
        public double RepairCost { get; set; }
        public string CreatedBy { get; set; }

    }
}
