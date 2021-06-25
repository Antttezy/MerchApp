using Domain.Core.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MerchendiseServer.Controllers
{
    [ApiController]
    public class MerchendiserShiftController : Controller
    {
        private readonly IRepository<Workshift> workshiftRepository;
        private readonly IRepository<Merchendiser> merchendiserRepository;
        private readonly IRepository<Shop> shopRepository;
        private readonly IAuthenticator<Merchendiser> authenticator;

        public MerchendiserShiftController(IRepository<Workshift> workshiftRepository, IRepository<Merchendiser> merchendiserRepository, IRepository<Shop> shopRepository, IAuthenticator<Merchendiser> authenticator)
        {
            this.workshiftRepository = workshiftRepository;
            this.merchendiserRepository = merchendiserRepository;
            this.shopRepository = shopRepository;
            this.authenticator = authenticator;
        }

        [HttpGet]
        [Route("merch/shifts")]
        public async Task<IActionResult> GetMerchendiserWorkshift(string login, string password)
        {
            if (await authenticator.IsCorrect(login, password))
            {
                var merch = (await Task.Run(() => merchendiserRepository.All())).First(m => string.Compare(m.Login, login, StringComparison.OrdinalIgnoreCase) == 0);
                var shifts = from s in workshiftRepository.All()
                             where s.MerchendiserId == merch.Id
                             select s;

                return Json(await Task.Run(() => shifts.ToList()));
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("merch/shops")]
        public async Task<IActionResult> GetShops(string login, string password)
        {
            if (await authenticator.IsCorrect(login, password))
            {
                var shops = await Task.Run(() => shopRepository.All());
                return Json(shops);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("merch/start")]
        public async Task<IActionResult> StartMerchendiserWorkshift([FromBody] Authed<int> shopId)
        {
            if (await authenticator.IsCorrect(shopId.Login, shopId.Password))
            {
                var merch = (await Task.Run(() => merchendiserRepository.All())).First(m => string.Compare(m.Login, shopId.Login, StringComparison.OrdinalIgnoreCase) == 0);

                if (merch.CurrentShiftId != null || merch.CurrentShift != null)
                    return BadRequest("Can't start a second workshift");

                var workshift = new Workshift
                {
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now,
                    Merchendiser = merch,
                    MerchendiserId = merch.Id,
                    ShopId = shopId.InnerData
                };

                workshiftRepository.Add(workshift);
                var shifts = await Task.Run(() => workshiftRepository.All());

                var created = workshiftRepository.All()
                    .OrderByDescending(x => x.Id)
                    .First(x => x.MerchendiserId == workshift.MerchendiserId);

                merch.CurrentShift = created;
                merch.CurrentShiftId = created.Id;
                merchendiserRepository.Update(merch);

                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("merch/end")]
        public async Task<IActionResult> EndMerchendiserWorkshift([FromBody] Authed data)
        {
            if (await authenticator.IsCorrect(data.Login, data.Password))
            {
                var merch = (await Task.Run(() => merchendiserRepository.All())).First(m => string.Compare(m.Login, data.Login, StringComparison.OrdinalIgnoreCase) == 0);

                if (merch.CurrentShiftId == null && merch.CurrentShift == null)
                    return BadRequest("You are not doing any workshifts");

                var workshift = merch.CurrentShift;
                workshift.EndTime = DateTime.Now;
                merch.CurrentShift = null;
                merch.CurrentShiftId = null;

                workshiftRepository.Update(workshift);
                merchendiserRepository.Update(merch);

                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
