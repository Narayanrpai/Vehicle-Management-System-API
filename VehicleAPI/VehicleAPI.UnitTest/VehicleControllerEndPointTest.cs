using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using VehicleAPI.Controllers;
using VehicleAPI.Models;
using VehicleAPI.Services;
using Xunit;

namespace VehicleAPI.UnitTest
{
    public class VehicleControllerEndPointTest
    {
        [Fact]
        public void GetVehicles_Returns_OkResult()
        {
            var mockVehicleService = new Mock<IVehicleService>();
            mockVehicleService.Setup(x => x.GetVehicles()).Returns(() =>
            {
                return new List<VehicleCategory>
                {
                    new VehicleCategory { VehicleId = 1, VehicleName = "Tesla Model S", VehicleType = "Four-Wheeler",
                    CategoryName = "Car", VehicleComplaint = "Wind Shield", RepairCost = 12500, CreatedBy = "Narayan",
                    CreatedDate = new DateTime(2019,04,09,10,30,20) }
                };
            });
          

            var vehicleController = new VehicleController(mockVehicleService.Object);

            var result = vehicleController.GetVehicles() as OkObjectResult;
            var vehicles = (List<VehicleCategory>)result.Value;
            var vehicleCount = vehicles.Count;

            Assert.Equal<int>(1, vehicleCount);
        }

        [Fact]
        public void GetVehicleById_Returns_Vehicle_Test()
        {
            var mockVehicleService = new Mock<IVehicleService>();
            mockVehicleService.Setup(x => x.GetVehicleById(It.IsAny<int>())).Returns(() => 
            {
                return new Vehicle
                {
                    VehicleId = 1,
                    VehicleName = "Tesla Model S",
                    VehicleType = "Four-Wheeler",
                    CategoryId = 3,
                    VehicleComplaint = "Wind Shield",
                    RepairCost = 12500,
                    CreatedBy = "Narayan",
                    CreatedDate = new DateTime(2019, 04, 09, 10, 30, 20)
                };

            });

            var vehicleController = new VehicleController(mockVehicleService.Object);
            var result = vehicleController.GetVehicle(10) as OkObjectResult;
            var vehicle = (Vehicle)result.Value;
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetEmployeeById_Returns_Null_Test()
        {
            var mockVehicleService = new Mock<IVehicleService>();
            mockVehicleService.Setup(x => x.GetVehicleById(It.IsAny<int>())).Returns(() => null);

            var vehicleController = new VehicleController(mockVehicleService.Object);
            var result = vehicleController.GetVehicle(10) as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void AddVehicle_ValidData_Returns_CreatedAtRoute()
        {
            var mockVehicleService = new Mock<IVehicleService>();
            var vehiclesInserted = new List<Vehicle>();
            var vehicle = new Vehicle()
            {
                VehicleName = "Tesla Model S",
                VehicleType = "Four-Wheeler",
                CategoryId = 2,
                VehicleComplaint = "Wind Shield",
                RepairCost = 12500,
                CreatedBy = "Narayan",
                CreatedDate = new DateTime(2019, 04, 09, 10, 30, 20)
            };
            mockVehicleService.Setup(x => x.AddVehicle(vehicle)).Callback((Vehicle vehicle) =>
            vehiclesInserted.Add(vehicle));

            var vehicleController = new VehicleController(mockVehicleService.Object);
            var result = vehicleController.AddVehicle(vehicle) as OkObjectResult;

            Vehicle vehicleTest = new Vehicle()
            {
                VehicleId = 1,
                VehicleName = "Tesla Model S",
                VehicleType = "Four-Wheeler",
                CategoryId= 3,
                VehicleComplaint = "Wind Shield",
                RepairCost = 12500,
                CreatedBy = "Narayan",
                CreatedDate = new DateTime(2019, 04, 09, 10, 30, 20)
            };
         
            var createdResponse = vehicleController.AddVehicle(vehicleTest);
      
            Assert.IsType<CreatedAtRouteResult>(createdResponse);

    
        }

        [Fact]

        public void RemoveVehicle_Test()
        {
            int vehicleId = 123;
            var mockVehicelService = new Mock<IVehicleService>();
            var mockVehicleController = new VehicleController(mockVehicelService.Object);
            mockVehicleController.DeleteVehicle(vehicleId);
            mockVehicelService.Verify(v => v.DeleteVehicle(vehicleId), Times.Never());
        }


        [Fact]
        public void SearchVehicle_Test()
        {
            string vehicleName = "Tesla Model S";
            var mockVehicelService = new Mock<IVehicleService>();
            var mockVehicleController = new VehicleController(mockVehicelService.Object);
            mockVehicleController.SearchVehicles(vehicleName);
            mockVehicelService.Verify(v => v.SearchVehicles(vehicleName), Times.Once());
        }

        [Fact]
        public void UpdateVehicle_Test()
        {
            int vehicleId = 1;
            Vehicle vehicleTest = new Vehicle()
            {
                VehicleName = "Tesla Model S",
                VehicleType = "Four-Wheeler",
                CategoryId = 3,
                VehicleComplaint = "Wind Shield",
                RepairCost = 12500,
                CreatedBy = "Narayan",
                CreatedDate = new DateTime(2019, 04, 09, 10, 30, 20)
            };

            var mockVehicelService = new Mock<IVehicleService>();
            var mockVehicleController = new VehicleController(mockVehicelService.Object);
            mockVehicleController.UpdateVehicle(vehicleId,vehicleTest);
            mockVehicelService.Verify(v => v.UpdateVehicle(vehicleId,vehicleTest), Times.Never());
        }
    }
}
