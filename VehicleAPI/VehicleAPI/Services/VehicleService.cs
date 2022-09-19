using System;
using System.Collections.Generic;
using System.Linq;
using VehicleAPI.Data;
using VehicleAPI.Models;

namespace VehicleAPI.Services
{
    public class VehicleService : IVehicleService
    {
        private VehicleDbContext _context;
        public VehicleService(VehicleDbContext context)
        {
            _context = context;
        }
        public void AddVehicle(Vehicle vehicle)
        {
            vehicle.CreatedDate = DateTime.Now;
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }

        public void DeleteVehicle(int id)
        {
            var vehicleToBeDeleted = GetVehicleById(id);
            _context.Vehicles.Remove(vehicleToBeDeleted);
            _context.SaveChanges();
        }

        public IEnumerable<string> GetAllCategories()
        {
            var list = _context.Categories.Select(x => x.CategoryName);
            return list;

        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public Vehicle GetVehicleById(int id)
        {
            return _context.Vehicles.FirstOrDefault(v => v.VehicleId == id);
            //return GetVehicles().Find(v => v.VehicleId == id);
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public List<Vehicle> GetVehiclesTable()
        {
            return _context.Vehicles.ToList();
        }

        public List<VehicleCategory> GetVehicles()
        {
            var vehicleCategory = new List<VehicleCategory>();
            var vehicles = GetVehiclesTable();
            foreach(Vehicle vehicle in vehicles)
            {
                var vehicleCategoryObject = new VehicleCategory();
                var categoryName = GetCategoryById(vehicle.CategoryId);
                vehicleCategoryObject.VehicleId = vehicle.VehicleId;
                vehicleCategoryObject.VehicleName = vehicle.VehicleName;
                vehicleCategoryObject.VehicleType = vehicle.VehicleType;
                vehicleCategoryObject.CategoryName = categoryName;
                vehicleCategoryObject.VehicleComplaint = vehicle.VehicleComplaint;
                vehicleCategoryObject.CreatedDate = vehicle.CreatedDate;
                vehicleCategoryObject.RepairCost = vehicle.RepairCost;
                vehicleCategoryObject.CreatedBy = vehicle.CreatedBy;
                vehicleCategory.Add(vehicleCategoryObject);
            }
            return vehicleCategory;
        }

        public List<VehicleCategory> SearchVehicles(string name)
        {
            var vehicleCategory = GetVehicles();
            var vehiclesToBeReturned = vehicleCategory.Where(v => v.VehicleName.ToLower().Contains(name.ToLower())).ToList();
            return vehiclesToBeReturned;
        }

        public void UpdateVehicle(int id, Vehicle vehicle)
        {
            var vehicleToBeUpdated = GetVehicleById(id);
            vehicleToBeUpdated.VehicleName = vehicle.VehicleName;
            vehicleToBeUpdated.VehicleType = vehicle.VehicleType;
            vehicleToBeUpdated.VehicleComplaint = vehicle.VehicleComplaint;
            vehicleToBeUpdated.CategoryId = vehicle.CategoryId;
            vehicleToBeUpdated.RepairCost = vehicle.RepairCost;
            _context.SaveChanges();
        }

        public int GetCategoryId(string name)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryName == name).CategoryId;
        }
        public string GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id).CategoryName;
        }
    }
}
