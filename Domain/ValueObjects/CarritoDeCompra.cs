﻿using Domain.Entities.Producto;
using System;
using System.Collections.Generic;

namespace Domain.ValueObjects
{
    class CarritoDeCompra
    {
        public virtual List<Producto> Productos { set; get; }
        public Guid Cliente_Id { set; get; }

        public CarritoDeCompra()
        {

        }

        public CarritoDeCompra(Guid Cliente_Id, List<Producto> Productos)
        {
            this.Productos = Productos;
            this.Cliente_Id = Cliente_Id;
        }
    }
}
