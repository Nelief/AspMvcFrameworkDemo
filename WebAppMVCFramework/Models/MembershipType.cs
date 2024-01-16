using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppMVCFramework.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }  
        public byte DiscoutRate {  get; set; }

        //Definizione statica dei tipi di abbonamento per DropDownlist e validazione
        public static readonly byte notSelected = 0;
        public static readonly byte Daily = 1;
        public static readonly byte Monthly = 2;
        public static readonly byte TriMonthly = 3;
        public static readonly byte Yearly = 4;
    }
}