using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppMVCFramework.Models;

namespace WebAppMVCFramework.DTO
{
    public class MovieFormDTO
    {
        public IEnumerable<Genre> Genres { get; set; }
        public Movie Movie { get; set; }
    }
}