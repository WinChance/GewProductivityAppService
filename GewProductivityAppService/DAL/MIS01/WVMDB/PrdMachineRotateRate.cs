using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GewProductivityAppService.DAL.MIS01.WVMDB
{
    [Table("PrdMachineRotateRate")]
    public partial class PrdMachineRotateRate
    {
        [Key]
        public int Iden { get; set; }

        [StringLength(10)]
        public string Department { get; set; }

        [StringLength(10)]
        public string Process { get; set; }

        [StringLength(2)]
        public string WorkerClass { get; set; }

        [StringLength(5)]
        public string Machine { get; set; }

        public int Begin { get; set; }

        public int End { get; set; }

        public int RotateDuration { get; set; }

        [StringLength(10)]
        public string Operator { get; set; }

        [Column(TypeName = "date")]
        public DateTime? YieldDate { get; set; }

        public DateTime InputTime { get; set; }
    }
}
