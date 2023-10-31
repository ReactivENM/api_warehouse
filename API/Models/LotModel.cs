using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class LotModel
    {
        [Required]
        public string IdAlmacen;
        [Required]
        public string Estado;
    }
}