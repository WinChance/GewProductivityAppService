using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GewProductivityAppService.DAL.MIS01.YDMDB
{
    [Table("prdAppSarong")]
    public partial class prdAppSarong
    {
        public int ID { get; set; }

        [StringLength(20)]
        public string SarongNo { get; set; }

        [StringLength(5)]
        public string CageType { get; set; }

        [Required]
        [StringLength(5)]
        public string Department { get; set; }

        [Required]
        [StringLength(10)]
        public string JarType { get; set; }

        public int? CountOfCategory { get; set; }

        [StringLength(50)]
        public string Remark { get; set; }

        [StringLength(2)]
        public string IsUsed { get; set; }
    }
}
