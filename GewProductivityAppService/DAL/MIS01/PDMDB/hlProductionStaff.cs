using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GewProductivityAppService.DAL.MIS01.PDMDB
{
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
