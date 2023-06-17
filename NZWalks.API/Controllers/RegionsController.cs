using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOS;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // https://localhost.1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbcontext;
        private readonly IRegionRepository _regionRepository;
        private IEnumerable<object> _regions;
        private IMapper _mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository,IMapper mapper)
        {
            _dbcontext = dbContext;
            _regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionDomainList = await _regionRepository.GetAllAsync();

            // var regionsDTOList = new List<RegionsDTO>();

            //foreach (var region in regionDomainList)
            //{
            //    regionsDTOList.Add(new RegionsDTO
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        RegionImageUrl = region.RegionImageUrl,
            //    });

            //}

            return Ok(_mapper.Map<List<RegionsDTO>>(regionDomainList));
        }

        [Route("{id:guid}")]
        [HttpGet]
        public async Task<IActionResult> GetbyId([FromRoute] Guid id)
        {
            var regions = await _regionRepository.GetByIdAsync(id);
            if (regions == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RegionsDTO>(regions));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            var regionDomainmodel = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl,
            };
           regionDomainmodel=  await _regionRepository.CreateAsync(regionDomainmodel);
            await _dbcontext.SaveChangesAsync();

            var regionDto = new RegionsDTO
            {
                Id = regionDomainmodel.Id,
                Code = regionDomainmodel.Code,
                Name = regionDomainmodel.Name,
                RegionImageUrl = regionDomainmodel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetbyId), new { id = regionDomainmodel.Id }, regionDomainmodel);
        }

        [Route("{id:guid}")]
        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            var regionDomainmodel = new Region
            {
                Code = updateRegionRequestDTO.Code,
                Name = updateRegionRequestDTO.Name,
                RegionImageUrl = updateRegionRequestDTO.RegionImageUrl,

            };
            regionDomainmodel=await _regionRepository.UpdateAsync(id, regionDomainmodel);
            if(regionDomainmodel == null)
            {
                return NotFound();
            }
            regionDomainmodel.Code=updateRegionRequestDTO.Code;
            regionDomainmodel.Name=updateRegionRequestDTO.Name;
            regionDomainmodel.RegionImageUrl = updateRegionRequestDTO.RegionImageUrl;

            _dbcontext.SaveChanges();

            var regiondto = new RegionsDTO
            {
                Id = regionDomainmodel.Id,
                Code = regionDomainmodel.Code,
                Name = regionDomainmodel.Name,
                RegionImageUrl = regionDomainmodel.RegionImageUrl,
            };

            return Ok(regiondto);

        }

        [Route("{id:guid}")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] Guid id) 
        { 
            var regionDomainmodel= await _regionRepository.DeleteAsync(id);
            if (regionDomainmodel == null)
            {
                return NotFound();
            }
            _dbcontext.regions.Remove(regionDomainmodel);
           await _dbcontext.SaveChangesAsync();

            var regionDto = new RegionsDTO
            {
                Id = regionDomainmodel.Id,
                Code = regionDomainmodel.Code,
                Name = regionDomainmodel.Name,
                RegionImageUrl = regionDomainmodel.RegionImageUrl,
            };

            return Ok(regionDto);
        }
    }
    



}



    
