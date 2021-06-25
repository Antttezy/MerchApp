using Domain.Core.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MerchendiseServer.Controllers
{

    public class ShopController : Controller
    {
        private readonly IRepository<Shop> shops;
        private readonly IAuthenticator<Coordinator> authenticator;

        public ShopController(IRepository<Shop> shops, IAuthenticator<Coordinator> authenticator)
        {
            this.shops = shops;
            this.authenticator = authenticator;
        }

        [NonAction]
        public async Task<bool> Authenticate(string login, string password)
        {
            return await authenticator.IsCorrect(login, password);
        }

        [HttpGet]
        [Route("shops")]
        public async Task<IActionResult> Get(string login = "", string password = "")
        {
            if (await Authenticate(login, password))
            {
                var ret = await Task.Run(() => shops.All().ToList());
                return Json(ret);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("shops/{id:int}")]
        public async Task<IActionResult> Get(int id, string login = "", string password = "")
        {
            if (await Authenticate(login, password))
            {
                try
                {
                    var ret = await Task.Run(() => shops.Get(id));
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

        [HttpPost]
        [Route("shops")]
        public async Task<IActionResult> Post([FromBody] Authed<Shop> model)
        {
            if (await Authenticate(model.Login, model.Password))
            {
                try
                {
                    ModelState.Clear();
                    if (TryValidateModel(model.InnerData, nameof(model.InnerData)))
                    {
                        await Task.Run(() => shops.Add(model.InnerData));
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

        [HttpPut]
        [Route("shops")]
        public async Task<IActionResult> Put([FromBody] Authed<Shop> model)
        {
            if (await Authenticate(model.Login, model.Password))
            {
                try
                {
                    ModelState.Clear();
                    if (TryValidateModel(model.InnerData, nameof(model.InnerData)))
                    {
                        await Task.Run(() => shops.Update(model.InnerData));
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
        [Route("shops/{id:int}")]
        public async Task<IActionResult> Delete(int id, string login = "", string password = "")
        {
            if (await Authenticate(login, password))
            {
                try
                {
                    await Task.Run(() => shops.Remove(new Shop { Id = id }));
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
