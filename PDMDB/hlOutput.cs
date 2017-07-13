namespace PDMDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hlOutput")]
    public partial class hlOutput
    {
        [Key]
        public int Iden { get; set; }

        [StringLength(50)]
        public string HL_No { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Class { get; set; }

        [StringLength(20)]
        public string Post { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Sys_Score { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Dync_Score { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ProValue { get; set; }

        public DateTime? InputTime { get; set; }

        [StringLength(30)]
        public string ModifyName { get; set; }

        public DateTime? ModifyTime { get; set; }

        [StringLength(300)]
        public string Remark { get; set; }

        public int? IsLargeType { get; set; }

        public int? IsMore { get; set; }

        public int? IsCalico { get; set; }
    }
}
