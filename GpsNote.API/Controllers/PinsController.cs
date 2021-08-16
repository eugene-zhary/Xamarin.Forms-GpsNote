using GpsNote.API.Models;
using GpsNote.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace GpsNote.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PinsController : Controller
    {
        private readonly IRepositoryService _repositoryService;

        public PinsController(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PinModel>>> GetPinsAsync()
        {
            try
            {
                var result = await _repositoryService.GetAllAsync<PinModel>();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<PinModel>>> GetPinsAsync(int userId)
        {
            try
            {
                var result = await _repositoryService.GetAllAsync<PinModel>(x => x.UserId == userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}/{search}")]
        public async Task<ActionResult<IEnumerable<PinModel>>> GetPinsAsync(int userId, string search)
        {
            try
            {
                var result = await _repositoryService.GetAllAsync<PinModel>(x => x.UserId == userId);

                result = result?.Where(x =>
                    x.Label.Contains(search, StringComparison.OrdinalIgnoreCase)
                    || x.Address.Contains(search, StringComparison.OrdinalIgnoreCase));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostPinAsync([FromBody] PinModel pin)
        {
            try
            {
                if (!ModelState.IsValid || pin == null)
                {
                    return BadRequest();
                }

                await _repositoryService.AddAsync(pin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> PutPinAsync([FromBody] PinModel pin)
        {
            try
            {
                if (!ModelState.IsValid || pin == null)
                {
                    return BadRequest();
                }

                await _repositoryService.UpdateAsync(pin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePinAsync([FromBody] PinModel pin)
        {
            try
            {
                if (!ModelState.IsValid || pin == null)
                {
                    return BadRequest();
                }

                await _repositoryService.DeleteAsync(pin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
