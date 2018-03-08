using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GewProductivityAppService.DAL.MIS01.YDMDB
{
    [Table("SongZhouinfo")]
    public partial class SongZhouinfo
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        [Column(Order = 1)]
        [StringLength(10)]
        public string machinetype { get; set; }

        [Column(Order = 2)]
        [StringLength(10)]
        public string batchno { get; set; }

        public int? nums { get; set; }

        public DateTime? plantime { get; set; }

        [StringLength(20)]
        public string ydoperator { get; set; }

        public DateTime? ydoperattime { get; set; }

        [StringLength(20)]
        public string properator { get; set; }

        public DateTime? properattime { get; set; }
    }
}
