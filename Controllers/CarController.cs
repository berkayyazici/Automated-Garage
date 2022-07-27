using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutomatedGarage.Data;
using AutomatedGarage.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AutomatedGarage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {

        private ApiDbContext _dbContext;

        public static int smallSpots = 50;
        public static int largeSpots = 30;
        public static int mediumSpots = 100;

        //public List<int> spotIdList = new List<int>();
        //public List<int> ticketNumberList = new List<int>();

        public CarController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Car car) {

            Random random = new Random();   
            car.ParkSpotId = random.Next(1,180);
            car.ParkSpotFloor = random.Next(1, 10);
            car.ticketNumber = random.Next(1000, 8000);
            car.ParkSpotDate = DateTime.Now;
            car.ParkSpotName = car.Brand.First() + car.ParkSpotId.ToString();

            //spotIdList.Add(car.ParkSpotId);  // checking if the given spotId is taken, generate another id
            //ticketNumberList.Add(car.ticketNumber);

            car.Size = car.Size.ToLower();


            int platenumber = await _dbContext.Cars.Where(u => u.PlateNumber == car.PlateNumber)
                .Select(u => u.PlateNumber).SingleOrDefaultAsync();

            int ticketNo = await _dbContext.Cars.Where(u => u.ticketNumber == car.ticketNumber)
                .Select(u => u.ticketNumber).SingleOrDefaultAsync();

            int spotNo = await _dbContext.Cars.Where(u => u.ParkSpotId == car.ParkSpotId)
                .Select(u => u.ParkSpotId).SingleOrDefaultAsync();

            /*string spotname = await _dbContext.Cars.Where(u => u.ParkSpotName == car.ParkSpotName)
                .Select(u => u.ParkSpotName).SingleOrDefaultAsync();

            if(spotname == car.ParkSpotName)
            {
                return BadRequest("Spot is taken.");
            }*/


            if (platenumber == car.PlateNumber)
            {
                return BadRequest("Another car belonging to this plate is parked.");
            }
            

            if (car.Size == "small")
            {
                if (spotNo == car.ParkSpotId)
                {
                    return Ok("Try again for another spot!");
                }

                if (ticketNo == car.ticketNumber)
                {
                    return Ok("Try again for another ticket number!");
                }
                
                if(smallSpots <= 0)
                {
                    return Ok("There is no more spots for small cars.");
                }

                await _dbContext.Cars.AddAsync(car);
                smallSpots -= 1;
            }


            else if(car.Size == "medium")
            {
                if (spotNo == car.ParkSpotId)
                {
                    return Ok("Try again for another spot!");
                }

                if (ticketNo == car.ticketNumber)
                {
                    return Ok("Try again for another ticket number!");
                }

                if (mediumSpots<= 0)
                {
                    return Ok("There is no more spots for medium cars.");
                }

                await _dbContext.Cars.AddAsync(car);

                mediumSpots -= 1;
            }

            else
            {
                if (spotNo == car.ParkSpotId)
                {
                    return Ok("Try again for another spot!");
                }

                if (ticketNo == car.ticketNumber)
                {
                    return Ok("Try again for another ticket number!");
                }

                if (largeSpots <= 0)
                {
                    return Ok("There is no more spots for large cars.");
                }

                await _dbContext.Cars.AddAsync(car);

                largeSpots -= 1;

            }

            await _dbContext.SaveChangesAsync();
            return Ok("Car succesfully parked."
                +"\nCar's spot Id : " + car.ParkSpotId + "\nCar's spot name : " + car.ParkSpotName 
                +"\nCar's spot floor : " + car.ParkSpotFloor + "\nCar's ticket number : " + car.ticketNumber
                +"\nCurrent Small Spots : " + smallSpots + "\nCurrent Medium Spots : " + mediumSpots 
                +"\nCurrent Large Spots : " + largeSpots);


        }

        /*[HttpGet("{PlateNumber}")]
        public async Task<IActionResult> GetCarbyPlateNumber(int plateNumber)
        {
            var car = await _dbContext.Cars.FindAsync(plateNumber);

            if(car == null)
            {
                return NotFound("No record for this plate number.");
            }

            return Ok(car);
        }*/

        [HttpGet("{ticketNumber}")]
        public async Task<IActionResult> GetCarbyTicketNumber(int ticketNumber)
        {
            var car = await _dbContext.Cars.SingleOrDefaultAsync(x => x.ticketNumber == ticketNumber);

            if (car == null)
            {
                return NotFound("No record for this ticket number.");
            }


            return Ok(car);
        }
        
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCarbyPlateandTicketNumber(int ticketNumber, int platenumber)
        {
            var car = await (from cars in _dbContext.Cars
                             where cars.ticketNumber == ticketNumber
                             where cars.PlateNumber == platenumber
                             select new
                             {
                                 Id = cars.Id,
                                 PlateNumber = cars.PlateNumber,
                                 Brand = cars.Brand,
                                 Size = cars.Size,
                                 ParkSpotId = cars.ParkSpotId,
                                 ParkSpotFloor = cars.ParkSpotFloor,
                                 ticketNumber = cars.ticketNumber,
                                 ParkSpotName = cars.ParkSpotName,
                                 ParkSpotDate = cars.ParkSpotDate
                             }).ToListAsync();
            return Ok(car);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            
            return Ok(await _dbContext.Cars.ToListAsync());
        }


        [HttpDelete("{plateNumber}")]
        public async Task<IActionResult> DeleteByPlateNumber(int plateNumber)
        {

            var car = await _dbContext.Cars.SingleOrDefaultAsync(x => x.PlateNumber == plateNumber);
            

            if (car == null)
            {
                return NotFound("No record for this plate number");
            }

            else
            {
                if(car.Size == "small")
                {
                    _dbContext.Cars.Remove(car);
                    smallSpots += 1;
                }

                else if (car.Size == "medium")
                {
                    _dbContext.Cars.Remove(car);
                    mediumSpots += 1;
                }

                else
                {
                    _dbContext.Cars.Remove(car);
                    largeSpots += 1;
                }

                await _dbContext.SaveChangesAsync();
                return Ok("Record deleted succesfully." + "\nCurrent Small Spots : " + smallSpots + "\nCurrent Medium Spots : " + mediumSpots + "\nCurrent Large Spots : " + largeSpots);
            }
        }


    }
}
