using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatikaAPI.DTOs;
using PatikaAPI.Models;
using System.Security.Cryptography.Xml;

namespace PatikaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BetegsegController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            using (var context = new PatikaContext())
            {
                try
                {
                    List<Betegseg> result = context.Betegsegs.ToList();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    List<Betegseg> result =
                    [
                        new Betegseg
                        {
                            Id = -1,
                            Megnevezes = ex.Message
                        },
                    ];
                    return StatusCode(400,result);
                }
            }
        }

        [HttpGet("ById")]
        public IActionResult Get(int id)
        {
            using (var context = new PatikaContext())
            {
                try
                {
                    Betegseg result = context.Betegsegs.FirstOrDefault(b => b.Id == id);
                    if (result == null)
                        return NotFound("Nincs ilyen azonosítójú betegség");
                    else
                        return Ok(result);
                }
                catch (Exception ex)
                {
                    Betegseg hiba = new Betegseg
                    {
                        Id = -1,
                        Megnevezes = ex.Message
                    };
                    return StatusCode(400, hiba);
                }
            }
        }

        [HttpGet("ToGyogyszerNev")]
        public IActionResult Get(string gynev)
        {
            using (var context = new PatikaContext())
            {
                try
                {
                    List<Betegseg> result = context.Kezels.Include(k => k.Gyogyszer).Include(k => k.Betegseg).Where(k => k.Gyogyszer.Nev == gynev).Select(k => k.Betegseg).ToList();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    List<Betegseg> result = new List<Betegseg>();
                    result.Add(new Betegseg
                    {
                        Id = -1,
                        Megnevezes=ex.Message
                    });
                    return StatusCode(400, result);
                }
            }
        }

        [HttpGet("ToGyogyszerId")]
        public IActionResult GetById(int id)
        {
            using (var context = new PatikaContext())
            {
                try
                {
                    List<Betegseg> result = context.Kezels.Include(k => k.Gyogyszer).Include(k => k.Betegseg).Where(k => k.Gyogyszer.Id == id).Select(k => k.Betegseg).ToList();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    List<Betegseg> result = new List<Betegseg>();
                    result.Add(new Betegseg
                    {
                        Id = -1,
                        Megnevezes = ex.Message
                    });
                    return StatusCode(400, result);
                }
            }
        }
        [HttpGet("BetegsegDTO")]
        public IActionResult GetBetegsegDTP()
        {
            using (var context = new PatikaContext())
            {
                try
                {
                    List<BetegsegDTO> result = context.Betegsegs.Select(x => new BetegsegDTO
                    {
                        id = x.Id,
                        name = x.Megnevezes
                    }).ToList();
                    return Ok(result);

                }
                catch (Exception ex)
                {

                    List<BetegsegDTO> hibalist = new();
                    BetegsegDTO hiba = new BetegsegDTO()
                    {
                        id = -1,
                        name = ex.Message
                    };
                    hibalist.Add(hiba);
                    return BadRequest(hibalist);
                }
            }
        }
            [HttpPost("UjBetegseg")]
            public IActionResult Post(Betegseg ujBetegseg)
            {
                using (var context = new PatikaContext())
                {
                    try
                    {
                        context.Betegsegs.Add(ujBetegseg);
                        context.SaveChanges();
                        return Ok("sikeres rögzítés");

                    }
                    catch (Exception ex)
                    {

                        return BadRequest(ex.Message);
                    }
                }
            }
            [HttpDelete("DelBetegseg")]
            public IActionResult Del(int id)
            {
                using (var context = new PatikaContext())
                {
                    try
                    {
                        Betegseg torl = new Betegseg()
                        {
                            Id = id
                        };
                        context.Betegsegs.Remove(torl);
                        context.SaveChanges();
                        return Ok("sikeres törlés");
                    }
                    catch (Exception ex)
                    {

                        return BadRequest(ex.Message);
                    }
                }
            }
            [HttpPut("BetegsegUpdate")]
            public IActionResult Put(Betegseg mod)
            {
                using (var context = new PatikaContext())
                {
                    try
                    {
                        if (context.Betegsegs.Contains(mod))
                        {
                            context.Betegsegs.Update(mod);
                            context.SaveChanges();
                            return Ok("Sikeres");
                        }
                        else
                        {
                            return NotFound("Nincs ilyen azonosito");
                        }
                    }
                    catch (Exception ex)
                    {

                        return BadRequest(ex.Message);
                    }
                }
            
        }


        
    }
}
