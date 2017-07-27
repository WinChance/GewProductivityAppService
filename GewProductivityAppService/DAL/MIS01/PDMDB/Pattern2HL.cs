using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GewProductivityAppService.DAL.MIS01.PDMDB
{
    public partial class Pattern2HL
    {
        [Key]
        public int nAutoID { get; set; }

        [Required]
        [StringLength(30)]
        public string strLBNo { get; set; }

        [StringLength(100)]
        public string Style_Name { get; set; }

        [StringLength(200)]
        public string strPatternName { get; set; }

        [StringLength(50)]
        public string Combo { get; set; }

        public short nDipSeq { get; set; }

        public int nWDensity { get; set; }

        public int nFDensity { get; set; }

        [Required]
        [StringLength(30)]
        public string strWYarn { get; set; }

        [Required]
        [StringLength(30)]
        public string strFYarn { get; set; }

        public byte bNeedYS { get; set; }

        public short nNeedHL { get; set; }

        public bool bNeedLC { get; set; }

        [Required]
        [StringLength(30)]
        public string strSgjn { get; set; }

        [Required]
        [StringLength(100)]
        public string strPatternRemark { get; set; }

        [Required]
        [StringLength(30)]
        public string strRefNo { get; set; }

        public DateTime tLogPattern { get; set; }

        public bool bKD { get; set; }

        [StringLength(30)]
        public string strKDNo { get; set; }

        public bool bDocGetSJS { get; set; }

        public DateTime? tDocGetSJS { get; set; }

        public bool bHLLogged { get; set; }

        [Required]
        [StringLength(30)]
        public string strHLNo { get; set; }

        public bool bGetOrgBan { get; set; }

        public DateTime? tGetOrgBan { get; set; }

        public DateTime? tPlanFinishYarn { get; set; }

        public DateTime? tReqFinish { get; set; }

        public DateTime? tPlanFinish { get; set; }

        public bool bArtGetSJS { get; set; }

        public bool bArtFinish { get; set; }

        public bool bMakeFinish { get; set; }

        public bool bBackFinish { get; set; }

        public bool bAllFinish { get; set; }

        public DateTime? tArtGetSJS { get; set; }

        public DateTime? tArtFinish { get; set; }

        public DateTime? tMakeFinish { get; set; }

        public DateTime? tBackFinish { get; set; }

        public DateTime? tAllFinish { get; set; }

        [StringLength(20)]
        public string strArtSender { get; set; }

        [StringLength(3000)]
        public string strHLRemark { get; set; }

        public short nFeedback { get; set; }

        public DateTime? tFeedback { get; set; }

        public short nRedipCause { get; set; }

        [Required]
        [StringLength(100)]
        public string strFeedbackRemark { get; set; }

        public bool bBackup { get; set; }

        [Required]
        [StringLength(100)]
        public string strBackupRemark { get; set; }

        public DateTime? tBackup { get; set; }

        [StringLength(20)]
        public string FabricType { get; set; }

        public DateTime? tDJSJS { get; set; }

        [StringLength(30)]
        public string Program { get; set; }

        [StringLength(30)]
        public string StyleNo { get; set; }

        [StringLength(15)]
        public string Season { get; set; }

        [StringLength(20)]
        public string series { get; set; }

        [StringLength(10)]
        public string PatternType { get; set; }

        [StringLength(20)]
        public string SONO { get; set; }

        public bool? bUpLoad { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? shrinkageL { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? shrinkageW { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RePeatH { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RepeatV { get; set; }

        public int? HandSequence { get; set; }

        [StringLength(100)]
        public string SJSRemark { get; set; }

        public decimal? Price { get; set; }

        [StringLength(10)]
        public string Unit { get; set; }

        public bool? Revise_status { get; set; }

        public int? Doc_Pattern_ID { get; set; }

        public bool? bGreyCloth { get; set; }

        public DateTime? tGreyCloth { get; set; }

        public short? Order_Type { get; set; }

        [StringLength(12)]
        public string Width { get; set; }

        public short? Quantity { get; set; }

        public short? Year { get; set; }

        public byte Copies { get; set; }

        public bool Is_Customer { get; set; }

        [StringLength(15)]
        public string Yarn_Skein_Ref { get; set; }

        [StringLength(20)]
        public string Pattern_Ref { get; set; }

        [StringLength(50)]
        public string Handfeel_Ref { get; set; }

        [StringLength(50)]
        public string Brushing_Ref { get; set; }

        [StringLength(50)]
        public string Finishing_Ref { get; set; }

        [StringLength(50)]
        public string Finishing { get; set; }

        [StringLength(100)]
        public string Washing_Type { get; set; }

        public DateTime? Require_Date { get; set; }

        [StringLength(500)]
        public string AC_Holder_Remarks { get; set; }

        public short Approval { get; set; }

        [StringLength(500)]
        public string Layout_Comments { get; set; }

        [StringLength(500)]
        public string Other_Comments { get; set; }

        [StringLength(6)]
        public string Pattern_Group { get; set; }

        public bool? Wait_HL { get; set; }

        public bool? bAGOA { get; set; }

        public short AddType { get; set; }

        public bool? Wait_Art { get; set; }

        [StringLength(50)]
        public string Finish_List { get; set; }

        public DateTime? send_wv_date { get; set; }

        [StringLength(5)]
        public string wv_dept { get; set; }

        public int? Manual_Width { get; set; }

        [StringLength(1)]
        public string Diffcult { get; set; }

        [StringLength(20)]
        public string Redip_Code_List { get; set; }

        public int? bAfter { get; set; }

        public bool bSpecial { get; set; }

        public DateTime? Special_Date { get; set; }

        public DateTime? tSendAC { get; set; }

        public DateTime? tReceiveAC { get; set; }

        public DateTime? tLabReceiveAC { get; set; }

        [StringLength(50)]
        public string component { get; set; }

        public int? IS_PD { get; set; }

        [StringLength(50)]
        public string Fab_Color_Name { get; set; }

        [StringLength(30)]
        public string Fab_Color_Code { get; set; }

        public DateTime? tApprove_Receive { get; set; }

        [StringLength(2)]
        public string Shade { get; set; }

        public int? nBubble { get; set; }

        [StringLength(50)]
        public string Fab_ID { get; set; }

        public int? Status { get; set; }

        public DateTime? tHLReceive_Art { get; set; }

        public DateTime? tHLReceive_Yarn { get; set; }

        public DateTime? tHLPackage { get; set; }

        public DateTime? Operate_time { get; set; }

        [StringLength(20)]
        public string Operator { get; set; }

        public int? is_jd { get; set; }

        public DateTime? jdjq { get; set; }

        public DateTime? tAllFinish_Last { get; set; }

        public DateTime? tBackup_Last { get; set; }

        public DateTime? tSendACFir { get; set; }

        public DateTime? tReceiveACFir { get; set; }

        public DateTime? tLabReceiveACFir { get; set; }

        public DateTime? tApprove_ReceiveFir { get; set; }

        [StringLength(20)]
        public string Affirm_Man { get; set; }

        [StringLength(50)]
        public string HandFeel_Desc { get; set; }

        [StringLength(50)]
        public string Request_List { get; set; }

        [StringLength(30)]
        public string HL_Designer { get; set; }

        [StringLength(50)]
        public string Affirm_List { get; set; }

        [StringLength(30)]
        public string HLChecker { get; set; }

        public DateTime? tHLCheck { get; set; }

        public DateTime? HL_Design_Time { get; set; }

        public DateTime? tSendYarn { get; set; }

        public DateTime? tLabjq { get; set; }

        public DateTime? tSendSpeFN { get; set; }

        public DateTime? tReceiveSpeFN { get; set; }

        [StringLength(20)]
        public string sSpeFnPlace { get; set; }

        public DateTime? tOweOrgban { get; set; }

        public bool? Is_Print_HLArt { get; set; }

        public bool? Is_FP { get; set; }

        [StringLength(50)]
        public string FP_color_code { get; set; }

        [StringLength(50)]
        public string FP_Remark { get; set; }

        [StringLength(20)]
        public string FP_Art_NO { get; set; }

        [StringLength(20)]
        public string FP_effect { get; set; }

        [StringLength(10)]
        public string FP_type { get; set; }

        [StringLength(30)]
        public string FP_Shape { get; set; }

        [StringLength(20)]
        public string strHBNo { get; set; }

        public int? HLArt_Add_Num { get; set; }

        public bool? IsBooking { get; set; }

        [StringLength(150)]
        public string customer_finishing { get; set; }

        public bool? Is_AutoArt { get; set; }

        [StringLength(50)]
        public string sel_blankrt_part { get; set; }

        [StringLength(30)]
        public string Before_hl_no { get; set; }

        [StringLength(30)]
        public string Redip_hlno { get; set; }

        public int? GEW_Print_NO_ID { get; set; }

        public bool? IsPrintYarnCard { get; set; }

        [StringLength(20)]
        public string Printer { get; set; }

        public DateTime? PrintTime { get; set; }

        public bool? Is_JQ_Updated { get; set; }

        public DateTime? Old_JDJQ { get; set; }

        public int? DayNum { get; set; }

        public bool? one_key_art { get; set; }

        [StringLength(100)]
        public string GEK_Color_Standard { get; set; }

        [StringLength(100)]
        public string GEK_Color_Code { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Cale_Width { get; set; }

        [StringLength(50)]
        public string Counter_Man { get; set; }

        public bool? Is_special_Customer_Width { get; set; }

        public bool? Is_change_Width { get; set; }
    }
}
