using Domain.Core.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MerchendiseServer.Controllers
{
    [ApiController]
    public class WorkshiftController : Controller
    {
        private readonly IRepository<Workshift> workshifts;
        private readonly IAuthenticator<Coordinator> authenticatorClient;

        public WorkshiftController(IRepository<Workshift> workshifts, IAuthenticator<Coordinator> authenticatorClient)
        {
            this.workshifts = workshifts;
            this.authenticatorClient = authenticatorClient;
        }

        [NonAction]
        public async Task<bool> Authenticate(string login, string password)
        {
            return await authenticatorClient.IsCorrect(login, password);
        }

        [HttpGet]
        [Route("shifts")]
        public async Task<IActionResult> Get(string login = "", string password = "")
        {
            if (await Authenticate(login, password))
            {
                var ret = await Task.Run(() => workshifts.All());
                return Json(ret);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("shifts/{id:int}")]
        public async Task<IActionResult> Get(int id, string login = "", string password = "")
        {
            if (await Authenticate(login, password))
            {
                try
                {
                    var ret = await Task.Run(() => workshifts.Get(id));
                    return Json(ret);
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut]
        [Route("shifts")]
        public async Task<IActionResult> Put([FromBody] Authed<Workshift> model)
        {
            if (await Authenticate(model.Login, model.Password))
            {
                try
                {
                    ModelState.Clear();
                    if (TryValidateModel(model.InnerData, nameof(model.InnerData)))
                    {
                        await Task.Run(() => workshifts.Update(model.InnerData));
                        return Ok();
                    }
                    else
                        return BadRequest();
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete]
        [Route("shifts/{id:int}")]
        public async Task<IActionResult> Delete(int id, string login = "", string password = "")
        {
            if (await Authenticate(login, password))
            {
                try
                {
                    await Task.Run(() => workshifts.Remove(new Workshift { Id = id }));
                    return Ok();
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
