using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class PackageModel
    {
        public string IdExterno;
        [Required]
        public string IdCliente;
        [Required]
        public string Peso;
        [Required]
        public string DirEnvio;
        [Required]
        public string Estado = "en_espera";
    }
}