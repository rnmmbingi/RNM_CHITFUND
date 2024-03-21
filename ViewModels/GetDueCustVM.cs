using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RNM_CHITFUND.ViewModels
{
    public class GetDueCustVM
    {
        [Required]
        public string Chitt_ID { get; set; }
        public List<SelectListItem> ChittiSchemes { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }

    }
}