using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleAPI.Models;
using VehicleAPI.Services;

namespace VehicleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {

        IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        // GET: api/<VehicleController>
        [HttpGet]
        [Route("getvehicles")]
        public IActionResult GetVehicles()
        {
            var vehiclesToBeReturned = _vehicleService.GetVehicles();
            if(vehiclesToBeReturned == null)
            {
                return Ok("No Records Found");
            }
            return Ok(vehiclesToBeReturned);
        }

        [HttpGet]
        [Route("getCategory/{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var categoryToBeReturned = _vehicleService.GetCategoryById(id);
            if (categoryToBeReturned == null)
            {
                return Ok("No Records Found");
            }
            return Ok(categoryToBeReturned);
        }

        [HttpGet]
        [Route("getCategoryId/{name}")]
        public IActionResult GetCategoryName(string name)
        {
            var categoryIdToBeReturned = _vehicleService.GetCategoryId(name);

            return Ok(categoryIdToBeReturned);
        }

        // GET api/<VehicleController>/5
        [HttpGet("{id}", Name = "GetVehicle")]
        public IActionResult GetVehicle(int id)
        {
            var vehicle = _vehicleService.GetVehicleById(id);
            if (vehicle == null)
                return NotFound($"Vehicle with id {id} does not exist");

            return Ok(vehicle);
        }

        // POST api/<VehicleController>
        [HttpPost]
        [Route("addvehicle")]
        public IActionResult AddVehicle([FromBody] Vehicle vehicle)
        {
            _vehicleService.AddVehicle(vehicle);
            return CreatedAtRoute("GetVehicle", new { id = vehicle.VehicleId }, new { Message = "Vehicle Added Successfully" });
        }

        [HttpGet("getAllCategories")]

        public IActionResult GetAllCategories()
        {
            var categories = _vehicleService.GetAllCategories();
            if (categories == null)
            {
                return NotFound("No Categories Found");
            }
            return Ok(categories);

        }
        [HttpPost("addCategory")]

        public IActionResult AddCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            else
            {
                _vehicleService.AddCategory(category);
                return Created("Create category", new { message = "category created successfully" });
            }


        }

        // PUT api/<VehicleController>/5
        [HttpPut("updatevehicle/{id}")]
        public IActionResult UpdateVehicle(int id, [FromBody] Vehicle vehicle)
        {
            var vehicleToBeUpdated = GetVehicle(id);
            if (vehicleToBeUpdated is NotFoundObjectResult)
            {
                return NotFound($"Vehicle with Id {id} not found");
            }
            _vehicleService.UpdateVehicle(id, vehicle);
            return Ok("Vehicle updated successfully");
        }


        // DELETE api/<VehicleController>/5
        [HttpDelete("removevehicle/{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            var vehicleToBeDeleted = GetVehicle(id);
            if (vehicleToBeDeleted is NotFoundObjectResult)
            {
                return NotFound($"Vehicle with Id {id} not found");
            }
            _vehicleService.DeleteVehicle(id);
            return Ok("Vehicle deleted successfully");
        }

        // GET: api/<VehicleController>
        [HttpGet]
        [Route("searchvehicles/{name}")]
        public IActionResult SearchVehicles(string name)
        {
            var vehiclesToBeReturned = _vehicleService.SearchVehicles(name);
            if (vehiclesToBeReturned == null)
            {
                return Ok("No Records Found");
            }
            return Ok(vehiclesToBeReturned);
        }
    }
}
