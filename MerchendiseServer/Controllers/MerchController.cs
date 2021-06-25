using Domain.Core.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MerchendiseServer.Controllers
{

    public class MerchController : Controller
    {
        private readonly IRepository<Merchendiser> merchendisers;
        private readonly IAuthenticator<Coordinator> authenticatorClient;
        private readonly IEncryptor encryptor;

        public MerchController(IRepository<Merchendiser> merchendisers, IAuthenticator<Coordinator> authenticatorClient, IEncryptor encryptor)
        {
            this.merchendisers = merchendisers;
            this.authenticatorClient = authenticatorClient;
            this.encryptor = encryptor;
        }

        [NonAction]
        public async Task<bool> Authenticate(string login, string password)
        {
            return await authenticatorClient.IsCorrect(login, password);
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> Get(string login = "", string password = "")
        {
            if (await Authenticate(login, password))
            {
                var ret = await Task.Run(() => merchendisers.All());
                return Json(ret);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("users/{id:int}")]
        public async Task<IActionResult> Get(int id, string login = "", string password = "")
        {
            if (await Authenticate(login, password))
            {
                try
                {
                    return Json(await Task.Run(() => merchendisers.Get(id)));
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
        [Route("users")]
        public async Task<IActionResult> Post([FromBody] Authed<Merchendiser> model)
        {
            if (await Authenticate(model.Login, model.Password))
            {
                try
                {
                    ModelState.Clear();
                    if (TryValidateModel(model.InnerData, nameof(model.InnerData)) && !merchendisers.All().Any(m => string.Compare(m.Login, model.InnerData.Login) == 0))
                    {
                        model.InnerData.Password = encryptor.Encrypt(model.InnerData.Password);
                        await Task.Run(() => merchendisers.Add(model.InnerData));
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
        [Route("users")]
        public async Task<IActionResult> Put([FromBody] Authed<Merchendiser> model)
        {
            if (await Authenticate(model.Login, model.Password))
            {
                try
                {
                    bool doHash = true;

                    if (model != null && model.InnerData != null)
                    {
                        doHash = !string.IsNullOrEmpty(model.InnerData.Password);

                        if (!doHash)
                            model.InnerData.Password = merchendisers.Get(model.InnerData.Id).Password;
                    }

                    ModelState.Clear();
                    if (TryValidateModel(model.InnerData, nameof(model.InnerData)) && !merchendisers.All().Any(m => string.Compare(m.Login, model.InnerData.Login) == 0))
                    {
                        if (doHash)
                            model.InnerData.Password = encryptor.Encrypt(model.InnerData.Password);

                        await Task.Run(() => merchendisers.Update(model.InnerData));
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
        [Route("users/{id:int}")]
        public async Task<IActionResult> Delete(int id, string login = "", string password = "")
        {
            if (await Authenticate(login, password))
            {
                try
                {
                    await Task.Run(() => merchendisers.Remove(new Merchendiser { Id = id }));
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
