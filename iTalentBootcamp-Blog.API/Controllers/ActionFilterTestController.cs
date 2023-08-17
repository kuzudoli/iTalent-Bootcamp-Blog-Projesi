using iTalentBootcamp_Blog.API.Filters;
using iTalentBootcamp_Blog.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace iTalentBootcamp_Blog.API.Controllers
{
    [Route("api/{merchCode}/[controller]")]
    [ApiController]
    public class ActionFilterTestController : ControllerBase
    {
        [HttpGet("[action]")]
        public IActionResult GetData([FromQuery] string merchCode)
        {
            return Ok($"MerchantID: {merchCode} için datalar getirildi!");
        }

        [HttpPost("[action]")]
        [MerchantCodeActionFilter]
        public IActionResult Update(UpdateMerchantRequestModel requestModel)
        {
            return Ok($"MerchantID: {requestModel.MerchantCode} - {requestModel.Name} için update komutu çalıştırıldı!");
        }
    }
}
