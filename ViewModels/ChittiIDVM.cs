using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RNM_CHITFUND.ViewModels
{
    public class ChittiIDVM
    {
        [Required]
        public string Chitt_ID { get; set; }
        public List<SelectListItem> ChittiSchemes { get; set; }
    }
}