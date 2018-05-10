using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GewProductivityAppService.DAL.MIS01.YDMDB
{
    [Table("prdSongZhouInfo")]
    public  class prdSongZhouInfo
    {
        public int id { get; set; }

        [Required]
        [StringLength(10)]
        public string machinetype { get; set; }

        [Required]
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

        [StringLength(20)]
        public string Location { get; set; }

        public DateTime? PredictInBatchTime { get; set; }

        [StringLength(2)]
        public string WorkerClass { get; set; }

        [StringLength(10)]
        public string Type { get; set; }

        [StringLength(10)]
        public string YdOperatorNo { get; set; }

        [StringLength(10)]
        public string PrOperatorNo { get; set; }

        [StringLength(5)]
        public string Factory { get; set; }
    }
}
