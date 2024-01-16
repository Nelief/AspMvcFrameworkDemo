using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppMVCFramework.Models;

namespace WebAppMVCFramework.DTO
{
    public class CustomerFormDTO
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }
    }
}