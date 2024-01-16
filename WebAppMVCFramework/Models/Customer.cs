using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace WebAppMVCFramework.Models
{
    public class Customer
    {
        public int Id {  get; set; }
       
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Cognome { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Min18YearsIfMember] //Custom Attribute
        public DateTime? Birthdate { get; set; }
       
        public bool IsSubscribedToNewsletter { get; set; }

        //Foreign Key property, Entity rinconosce questa sintassi standard 
        [Required]
        public byte MembershipTypeId { get; set; }
        
        //Navigation Property Customer -> membershipType
        public MembershipType MembershipType { get; set; }
    }
}