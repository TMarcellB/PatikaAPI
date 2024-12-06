﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatikaAPI.Models;
using PatikaAPI.Services;
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

        
    }
}
