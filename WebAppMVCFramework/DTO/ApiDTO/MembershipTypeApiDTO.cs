﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppMVCFramework.DTO.ApiDTO
{
    public class MembershipTypeApiDTO
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public byte DiscoutRate { get; set; }
    }
}