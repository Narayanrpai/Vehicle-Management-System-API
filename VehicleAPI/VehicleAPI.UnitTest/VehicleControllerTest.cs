using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using VehicleAPI.Controllers;
using VehicleAPI.Models;
using VehicleAPI.Services;
using Xunit;

namespace VehicleAPI.UnitTest
{
    public class VehicleControllerTest
    {
        [Fact]

        public void GetVehicles_Test()
        {
            var mock = new Mock<IVehicleService>();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Add("Authentication", $"Bearer {GetTestToken()}");

            mock.Setup(x => x.GetVehicles()).Returns(() =>
            {
                return new List<VehicleCategory>
                {
                    new VehicleCategory
                    {
                        VehicleId = 1,
                        VehicleName = "Tesla Model S",
                        VehicleType = "Four-Wheeler",
                        CategoryName = "Car",
                        VehicleComplaint = "Wind Shield",
                        RepairCost = 12500,
                        CreatedBy = "Narayan",
                        CreatedDate = new DateTime(2019, 04, 09, 10, 30, 20)
                    }
                };
            });

            var vehicleController = new VehicleController(mock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };

            var vehicles = vehicleController.GetVehicles() as OkObjectResult;
            var vehicleList = (List<VehicleCategory>)vehicles.Value;
            var count = vehicleList.Count;


        }


        public string GetTestToken()
        {


            var httpContext = new DefaultHttpContext();

            var inMemorySettings = new Dictionary<string, string> {
                     {"JWT:Secret", "pwc-training-secret"},
                      {"JWT:ValidIssuer", "http://localhost:20224"},

                     };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var authencticationController = new AuthenticationController(null, configuration)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                }
            };
            return authencticationController.Token();
        }

    }
}
