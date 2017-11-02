namespace GewProductivityAppService.DAL.MIS01.WVMDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PrdAbandonYarn")]
    public partial class PrdAbandonYarn
    {
        [Key]
        public int Iden { get; set; }

        [StringLength(10)]
        public string Department { get; set; }

        [StringLength(10)]
        public string Process { get; set; }

        [StringLength(2)]
        public string WorkerClass { get; set; }

        [StringLength(10)]
        public string Type { get; set; }

        public decimal? Weight { get; set; }

        [StringLength(10)]
        public string Operator { get; set; }

        [Column(TypeName = "date")]
        public DateTime? YieldDate { get; set; }

        public DateTime InputTime { get; set; }
    }
}
