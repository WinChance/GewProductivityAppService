namespace GewProductivityAppService.DAL.MIS01.FNMDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("fnHold")]
    public partial class fnHold
    {
        [Key]
        public int Iden { get; set; }

        public int GF_ID { get; set; }

        [Required]
        [StringLength(3)]
        public string Operation_Code { get; set; }

        [Required]
        [StringLength(12)]
        public string FN_Card { get; set; }

        [Required]
        [StringLength(200)]
        public string Hold_Reason { get; set; }

        [Required]
        [StringLength(20)]
        public string Holder { get; set; }

        public DateTime Hold_Time { get; set; }

        [Required]
        [StringLength(200)]
        public string Release_Reason { get; set; }

        [Required]
        [StringLength(20)]
        public string Releaser { get; set; }

        public DateTime? Release_Time { get; set; }

        public bool Is_EffectRepair { get; set; }

        public int? plan_check_status { get; set; }
    }
}
