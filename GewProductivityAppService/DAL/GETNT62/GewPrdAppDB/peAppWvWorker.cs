using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GewProductivityAppService.DAL.GETNT62.GewPrdAppDB
{
    [Table("peAppWvWorker")]
    public partial class peAppWvWorker
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string factory { get; set; }

        [Required]
        [StringLength(10)]
        public string name { get; set; }

        [Required]
        [StringLength(11)]
        public string cardno { get; set; }

        [Column("class")]
        [Required]
        [StringLength(40)]
        public string _class { get; set; }

        [StringLength(20)]
        public string classdes { get; set; }

        [StringLength(20)]
        public string jobs { get; set; }

        [StringLength(1)]
        public string Audit { get; set; }

        [StringLength(20)]
        public string WorkerType { get; set; }

        [StringLength(10)]
        public string GroupName { get; set; }

        [StringLength(50)]
        public string Remark { get; set; }
    }
}
