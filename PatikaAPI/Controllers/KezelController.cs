using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using PatikaAPI.Models;
using System.Text.Json.Serialization;

namespace PatikaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KezelController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            using(var context = new PatikaContext())
            {
                try
                {
                    List<Kezel> kezels = context.Kezels.ToList();
                    return Ok(kezels);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        }
        [HttpPost("Ujkezel")]
        public IActionResult Postos(Kezel ujkezel)
        {
            using (var context = new PatikaContext())
            {
                try
                {
                    

                    context.Kezels.Add(ujkezel);
                    context.SaveChanges();
                    return Ok("sikeres cuccos");
                }
                catch (Exception ex)
                {

                    return BadRequest($"{ex.Message}");
                }
            }


        }

        
    }
}
