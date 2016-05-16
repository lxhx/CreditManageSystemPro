using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreditManageSystemPro.Admin.Utility
{
    public class ResultInfo<T> where T:new()
    {
        public bool Result{get;set;}

        public T Data { get; set; }

        public string Msg { get; set; }
    }
}