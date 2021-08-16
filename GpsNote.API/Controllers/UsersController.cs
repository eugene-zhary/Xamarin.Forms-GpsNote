using GpsNote.API.Models;
using GpsNote.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GpsNote.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IRepositoryService _repositoryService;

        public UsersController(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsersAsync()
        {
            try
            {
                var result = await _repositoryService.GetAllAsync<UserModel>();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<UserModel>> GetUserAsync(string email)
        {
            try
            {
                var result = await _repositoryService.GetFirstOrDefaultAsync<UserModel>(x => x.Email.Equals(email));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostUserAsync([FromBody] UserModel user)
        {
            try
            {
                if (!ModelState.IsValid || user == null)
                {
                    return BadRequest();
                }

                await _repositoryService.AddAsync(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> PutUserAsync([FromBody] UserModel user)
        {
            try
            {
                if (!ModelState.IsValid || user == null)
                {
                    return BadRequest();
                }

                await _repositoryService.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync([FromBody] UserModel user)
        {
            try
            {
                if (!ModelState.IsValid || user == null)
                {
                    return BadRequest();
                }

                await _repositoryService.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
