using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class MobilePODetailsDTO
    {
        public int PONumber { get; set; }
        public string PONumberString 
        { 
            get
            {
                string result = "00000" + PONumber.ToString();
                return result.Substring(result.Length - 8);
            }                
        }
        public string SupervisorName { get; set; }
        public string CreationDate { get; set; }
        public string Status{ get; set; }
        public int NumOfItems { get; set; }
        public decimal GrandTotal { get; set; }
    }
}