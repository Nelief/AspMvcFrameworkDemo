using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAppMVCFramework.Models;

namespace WebAppMVCFramework.DTO.ApiDTO
{
    public class CustomerApiDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Cognome { get; set; }

        //[Min18YearsIfMember] 
        public DateTime? Birthdate { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }
        
        [Required]
        public byte MembershipTypeId { get; set; }

        public MembershipTypeApiDTO MembershipType { get; set; }

    }
}