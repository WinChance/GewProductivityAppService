namespace PDMDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hlProductionStaff")]
    public partial class hlProductionStaff
    {
        [Key]
        public int Iden { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Post { get; set; }

        [StringLength(20)]
        public string Class { get; set; }
    }
}
