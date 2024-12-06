using System;
using System.Collections.Generic;
using PatikaAPI.Models;
namespace PatikaAPI.DTOs
{
    public class GyogyszerNevHatoanyag
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Hatoanyag { get; set; }=string.Empty;
    }
}
