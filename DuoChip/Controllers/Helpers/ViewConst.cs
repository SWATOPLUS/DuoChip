using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc.Ajax;

namespace DuoChip.Controllers.Helpers
{
    public class ViewConst
    {
        public static readonly AjaxOptions SearchOptions = new AjaxOptions
        {
            UpdateTargetId = "res-list",
            OnBegin = "onReqestBegin",
            OnComplete = "onReqestEnd",
            HttpMethod = "get"
        };
    }
}