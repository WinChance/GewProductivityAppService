using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GewProductivityAppService.DAL.MIS01.YDMDB
{
    [Table("rtProduction")]
    public partial class rtProduction
    {
        [Key]
        public int Iden { get; set; }

        [Required]
        [StringLength(20)]
        public string Type { get; set; }

        [Required]
        [StringLength(20)]
        public string Machine_NO { get; set; }

        [Required]
        [StringLength(20)]
        public string Rack_NO { get; set; }

        [Required]
        [StringLength(8)]
        public string Batch_NO { get; set; }

        [Required]
        [StringLength(10)]
        public string Yarn_Type { get; set; }

        [Required]
        [StringLength(20)]
        public string Yarn_Count { get; set; }

        [Required]
        [StringLength(30)]
        public string Yarn_Lot { get; set; }

        [Required]
        [StringLength(25)]
        public string Color_Code { get; set; }

        public int? Cone_Num { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Unit_Weight { get; set; }

        [StringLength(2)]
        public string Is_QC { get; set; }

        [Required]
        [StringLength(12)]
        public string Material_Usage { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Output { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Weight { get; set; }

        [Required]
        [StringLength(8)]
        public string Pay_Type { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Unit_Price { get; set; }

        [Required]
        [StringLength(2)]
        public string Shift { get; set; }

        [Required]
        [StringLength(30)]
        public string Worker_ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Inputer_ID { get; set; }

        [StringLength(1000)]
        public string Input_Time { get; set; }

        [Required]
        [StringLength(30)]
        public string Remark { get; set; }

        [StringLength(20)]
        public string Way { get; set; }

        public bool Updated_Trace { get; set; }

        [StringLength(2)]
        public string Department { get; set; }

        [StringLength(20)]
        public string Sarong_No { get; set; }

        [StringLength(10)]
        public string SarongID { get; set; }

        [StringLength(10)]
        public string Place_Axis { get; set; }
    }
}
