using System.Collections.Generic;
using VehicleAPI.Models;

namespace VehicleAPI.Services
{
    public interface IVehicleService
    {
        List<Vehicle> GetVehiclesTable();
        List<VehicleCategory> GetVehicles();
        Vehicle GetVehicleById(int id);
        void AddVehicle(Vehicle vehicle);
        void UpdateVehicle(int id, Vehicle vehicle);
        void DeleteVehicle(int id);
        List<VehicleCategory> SearchVehicles(string id);
        string GetCategoryById(int id);
        IEnumerable<string> GetAllCategories();
        void AddCategory(Category category);
        int GetCategoryId(string name);

    }
}
