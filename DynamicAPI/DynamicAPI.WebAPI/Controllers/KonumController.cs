using DynamicAPI.Business.Service;
using DynamicAPI.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DynamicAPI.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class KonumController : ControllerBase
    {
        private IKonumService _konumService;

        public KonumController(IKonumService konumService)
        {
            _konumService = konumService;
        }

        [HttpGet]
        public async Task<IActionResult> getListKonum()
        {
            var result = _konumService.getList();
            if (result.Success)
            {
                return  Ok(result.Data);
            }
            return  BadRequest(result.Message);
        }

        [HttpGet]
        public IActionResult getById(int id)
        {
            var result = _konumService.getById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
                return BadRequest(result.Message);
        }
        [HttpGet]
        public IActionResult getTodayByUserId(int id)
        {
            var result = _konumService.getTodayByUserId(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
                return BadRequest(result.Message);
        }


        [HttpPost]
        public IActionResult AddKonum(Location location)
        {
            var result = _konumService.Add(location);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPut]
        public IActionResult UpdateKonum(Location location)
        {
            var result = _konumService.Update(location);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpDelete]
        public IActionResult DeleteKonum(int id)
        {
            var removeKonum = _konumService.getById(id);
            if (removeKonum.Success)
            {
                var result = _konumService.Delete(removeKonum.Data);
                if (result.Success)
                {
                    return Ok(result.Message);
                }

                return BadRequest(result.Message);
            }

            return BadRequest(removeKonum.Message);
        }
    }
}
