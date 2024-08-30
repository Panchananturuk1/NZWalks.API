using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //GET ALL REGIONS
        //https://localhost:portnumber/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get Data from the Database
            var regionsDomain = dbContext.Regions.ToList();

            //Map domain models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegoinImageUrl = regionDomain.RegoinImageUrl
                });
            }

            return Ok(regionsDto);
        }


        //GETS A SPECIFIC REGIONS BY PRVODONG ID
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var regionsDomain = dbContext.Regions.Find(id)

            // Get Data from the Database
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //Convert Region Domain models to Region DTOs
            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegoinImageUrl = regionDomain.RegoinImageUrl
            };

            //Return DTO Back to client
            return Ok(regionDto);
        }

        //POST IMPLEMENTATION

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            //Map/Convert DTO to Domain Model
            var regionDomainModel = new Region
            { 
                Code = addRegionRequestDto.Code,   
                Name = addRegionRequestDto.Name,
                RegoinImageUrl = addRegionRequestDto.RegoinImageUrl
            };

            // Use domain model to create region
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            //Map domain Model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegoinImageUrl = regionDomainModel.RegoinImageUrl
            };


            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDto);
        }

    }
}
