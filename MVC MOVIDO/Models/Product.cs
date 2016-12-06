using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoMoya.Models
{
   
    public class Product
    {
        public int ID { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public double Costo { get; set; }
        [Required]
        public string Imagen { get; set; }
        [Required]
        public string Categoria { get; set; }        
    }
}