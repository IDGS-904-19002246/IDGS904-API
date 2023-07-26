﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IDGS904_API.Models
{
    public class Productos
    {
        [Key]
        public int? id_producto { get; set; }
        public string? nombre { get; set; }
        public int precio { get; set; }
        public int cantidad { get; set; }
        public int cantidad_min { get; set; }
        public string? descripcion { get; set; }
        public string? estado { get; set; }
        public int pendientes { get; set; }
        [NotMapped]
        public List<string>? img
        {
            get => Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(imgJson);
            set => imgJson = Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }
        [Column("img")]
        public string? imgJson { get; set; }
        [NotMapped]
        public List<Foto>? fotos { get; set; }

    }
    public class Foto
    {
        public string nombreFoto { get; set; }
        public byte[] file64Foto { get; set; }
    }

    public class insumo_producto
    {
        [Key]
        public int? id_insumo_producto { get; set; }
        public int fk_id_insumo { get; set; }
        public int fk_id_producto { get; set; }
        //.........................................
        public int cantidad { get; set; }
        public int precio { get; set; }
        public DateTime fecha { get; set; }
    }
    public class insumo_productos {
        public int id { get; set; }
        public int cantidad { get; set; }

        public List<insumo_producto> lista { get; set; }
    }
}
