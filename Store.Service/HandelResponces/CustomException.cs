using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.HandelResponces
{
    public class CustomException : Response
    {
        public CustomException(int statuseCode, string? message = null, string? details=null) 
            : base(statuseCode, message)
        {
            Details = details;
        }
        public string? Details { get; set; }


    }
}
