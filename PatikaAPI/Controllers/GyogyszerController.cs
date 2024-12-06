﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatikaAPI.DTOs;
using PatikaAPI.Models;

namespace PatikaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GyogyszerController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            using(var context = new PatikaContext())
            {
                try
                {
                    List<Gyogyszer> result = context.Gyogyszers.ToList();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    List<Gyogyszer> result = new List<Gyogyszer>();
                    Gyogyszer hiba = new Gyogyszer()
                    {
                        Id = -1,
                        Nev = ex.Message
                    };
                    result.Add(hiba);
                    return BadRequest(result);

                }
            }
        }

        [HttpGet("ById")]
        public IActionResult Get(int bid)
        {
            using (var context = new PatikaContext())
            {
                try
                {
                    Gyogyszer result = context.Gyogyszers.FirstOrDefault(x => x.Id == bid);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    Gyogyszer hiba = new Gyogyszer()
                    {
                        Id = -1,
                        Nev = ex.Message
                    };               
                    return BadRequest(hiba);

                }
            }
        }

        [HttpGet("ToBetegsegName")]
        public IActionResult Get(string bname)
        {
            using(var context = new PatikaContext())
            {
                try
                {
                    List<Gyogyszer> result = context.Kezels.Include(k => k.Gyogyszer).Include(k => k.Betegseg).Where(k => k.Betegseg.Megnevezes == bname).Select(k => k.Gyogyszer).ToList();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    List<Gyogyszer> result = new List<Gyogyszer>();
                    Gyogyszer hiba = new()
                    {
                        Id = -1,
                        Nev = ex.Message
                    };
                    result.Add(hiba);
                    return BadRequest(result);
                }
            }
        }

        [HttpGet("ToBetegsegId")]
        public IActionResult GetById(int id)
        {
            using (var context = new PatikaContext())
            {
                try
                {
                    List<Gyogyszer> result = context.Kezels.Include(k => k.Gyogyszer).Include(k => k.Betegseg).Where(k => k.Betegseg.Id == id).Select(k => k.Gyogyszer).ToList();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    List<Gyogyszer> result = new List<Gyogyszer>();
                    Gyogyszer hiba = new()
                    {
                        Id = -1,
                        Nev = ex.Message
                    };
                    result.Add(hiba);
                    return BadRequest(result);
                }
            }
        }
        [HttpGet("GyogyszerDTO")]
        public IActionResult GetGyogyszerDTO()
        {
            using (var context=new PatikaContext())
            {
                try
                {
                    List<GyogyszerNevHatoanyag> result = context.Gyogyszers.Select(gy => new GyogyszerNevHatoanyag()
                    {
                        Id=gy.Id,
                        Name=gy.Nev,
                        Hatoanyag=gy.Hatoanyag

                    }).ToList();
                    return Ok(result);

                }
                catch (Exception ex)
                {
                    List<GyogyszerNevHatoanyag> hibalista = new();
                    GyogyszerNevHatoanyag hiba = new GyogyszerNevHatoanyag()
                    {
                        Id = -1,
                        Name = ex.Message
                    };
                    hibalista.Add(hiba);
                    return BadRequest(hibalista);
                }
            }
        }
    }
}