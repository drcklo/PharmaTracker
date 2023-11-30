﻿using PharmaTracker.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaTracker.Shared
{
    public class CestaDCompra
    {
        [Key]
        public int CestaDCompraId { get; set; }
        [ForeignKey("ClienteId")]
        public int ClienteId { get; set; }
        public Clientes Cliente { get; set; }
        public List<Productos> productos { get; set; } = new List<Productos>(); 

        public bool Pago { get; set; }

        public void Add(Productos producto) => productos.Add(producto);
        public void Remove(Productos producto) => productos.Remove(producto);

    }
}

