using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GewProductivityAppService.DAL.MIS01.YDMDB
{
    [Table("ydBatchTrace")]
    public partial class ydBatchTrace
    {
        [Key]
        public int Iden { get; set; }

        [Required]
        [StringLength(8)]
        public string Batch_NO { get; set; }

        //public int Repair_Times { get; set; }

        //public int Inner_Repair_Times { get; set; }

        //[StringLength(20)]
        //public string Machine_ID { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Send_Lab_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Advance_Handle_Time { get; set; }

        //[Required]
        //[StringLength(20)]
        //public string Lab_Dip_Type { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Lab_Receive_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Recipe_OK_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Beam_Apply_Yarn_Plan { get; set; }

        //public bool Applied_Yarn { get; set; }

        //[StringLength(20)]
        //public string Apply_Yarn_Class { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Receive_Yarn_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Loose_Yarn_Time { get; set; }

        //public bool Trim_Arranged { get; set; }

        //[StringLength(20)]
        //public string Trim_Class { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Trim_Arranged_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Trim_OK_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Print_Card_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Dye_Schedule_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Cage_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? First_Dye_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Dye_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Check_Time { get; set; }

        //[Required]
        //[StringLength(10)]
        //public string Check_Result { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? First_Finish_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Finish_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Dehydrate_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Dry_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Send_LT_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Send_QC_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? QC_Receive_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? QC_Time { get; set; }

        //[StringLength(10)]
        //public string QC_Result { get; set; }

        //[StringLength(250)]
        //public string QC_Suggestion { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Winding_Time { get; set; }

        //[StringLength(2000)]
        //public string Remark { get; set; }

        //public bool Is_End { get; set; }

        //[Required]
        //[StringLength(10)]
        //public string Trace_Type { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? End_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime Begin_Time { get; set; }

        //[Required]
        //[StringLength(50)]
        //public string Repair_Type { get; set; }

        //[Column(TypeName = "timestamp")]
        //[MaxLength(8)]
        //[Timestamp]
        //public byte[] Record_Version { get; set; }

        //[Required]
        //[StringLength(50)]
        //public string QC_FeedBack { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Send_Rewinding_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Back_Production_Time { get; set; }

        //[StringLength(20)]
        //public string Inner_Yarn { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Inner_Yarn_Time { get; set; }

        //[StringLength(20)]
        //public string Outer_Yarn { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Outer_Yarn_Time { get; set; }

        //[StringLength(20)]
        //public string Color_Yarn { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Color_Yarn_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Final_LB_Delivery_Date { get; set; }

        //[StringLength(60)]
        //public string Recive_LT_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Send_Yarn_Time { get; set; }

        //public bool? IsPackAge { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Send_Overturn_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? SS_Overturn_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Send_TestColor_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? TestColor_Receive_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Sample_Receive_Time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Ts_Receive_time { get; set; }

        //public bool? lbApprove_Flag { get; set; }


        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Send_ChildWindingTime { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? Remove_cage_time { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? ChildWinding_GetTime { get; set; }

        [StringLength(2)]
        public string IsSongZhou { get; set; }
    }
}
