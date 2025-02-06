using Church_GIDS.Data;
using Church_GIDS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Church_GIDS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChurchController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChurchController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("nearest")]
        public async Task<IActionResult> GetNearestChurch([FromBody] Person person)
        {
            
            var churches = new  List<Church>()
        {
              new Church { Id = 1, Name = "Grace Church", Address = "123 Main St, City", Latitude = 40.7128, Longitude = -74.0060 },
                new Church { Id = 2, Name = "Hope Church", Address = "456 Elm St, Town", Latitude = 34.0522, Longitude = -118.2437 },
                new Church { Id = 3, Name = "Faith Church", Address = "789 Oak St, Village", Latitude = 41.8781, Longitude = -87.6298 },
                new Church { Id = 4, Name = "Charity Church", Address = "101 Maple Ave, Metropolis", Latitude = 37.7749, Longitude = -122.4194 },
                new Church { Id = 5, Name = "Unity Church", Address = "202 Birch Rd, Suburb", Latitude = 39.9526, Longitude = -75.1652 },
                new Church { Id = 6, Name = "Trinity Church", Address = "303 Cedar St, Hamlet", Latitude = 29.7604, Longitude = -95.3698 },
                new Church { Id = 7, Name = "Salvation Church", Address = "404 Pine Blvd, Countryside", Latitude = 33.4484, Longitude = -112.0740 },
                new Church { Id = 8, Name = "New Life Church", Address = "505 Oak Ln, Seaside", Latitude = 47.6062, Longitude = -122.3321 }
        }; 

            
            var nearestChurch = churches
                .Select(c => new
                {
                    Church = c,
                    Distance = CalculateDistance(person.Latitude, person.Longitude, c.Latitude, c.Longitude)
                })
                .OrderBy(c => c.Distance)
                .FirstOrDefault();

            if (nearestChurch == null)
                return NotFound();

            return Ok(new
            {
                Church = nearestChurch.Church,
                Distance = nearestChurch.Distance
            });
        }

        private double CalculateDistance(double lat1, double lng1, double lat2, double lng2)
        {
           
            var dLat = (lat2 - lat1) * (Math.PI / 180);
            var dLng = (lng2 - lng1) * (Math.PI / 180);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(lat1 * (Math.PI / 180)) * Math.Cos(lat2 * (Math.PI / 180)) *
                    Math.Sin(dLng / 2) * Math.Sin(dLng / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return 6371 * c; 
        }
    }
}
