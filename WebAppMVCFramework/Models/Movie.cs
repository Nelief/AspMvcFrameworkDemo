using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppMVCFramework.Models
{
    public class Movie
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Inserire un titolo")]
        [StringLength(100)]
        public string Title { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required]
        public DateTime? ReleaseDate { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required]
        public DateTime? DateAdded { get; set; }
        
        [Required]
        [Range(1,20)]
        public int Stock {  get; set; }
        
        [Required]
        public byte GenreId { get; set; }
       
        public Genre Genre { get; set; }
    }
}