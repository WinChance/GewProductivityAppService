namespace PDMDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hlBasicInfo")]
    public partial class hlBasicInfo
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string HL_No { get; set; }

        [StringLength(30)]
        public string LB_No { get; set; }

        [StringLength(180)]
        public string Pattern_Name { get; set; }

        public short? HL_Type { get; set; }

        [StringLength(20)]
        public string Warp_Density { get; set; }

        [StringLength(20)]
        public string Weft_Density { get; set; }

        [StringLength(20)]
        public string Grey_Theory_Warp_Density { get; set; }

        [StringLength(20)]
        public string Grey_Theory_Weft_Density { get; set; }

        [StringLength(20)]
        public string Grey_Actual_Warp_Density { get; set; }

        [StringLength(20)]
        public string Grey_Actual_Weft_Density { get; set; }

        [StringLength(25)]
        public string Yarn_Warp_Count { get; set; }

        [StringLength(25)]
        public string Yarn_Weft_Count { get; set; }

        [StringLength(10)]
        public string Mercerizing { get; set; }

        [StringLength(20)]
        public string Mercerize_GF_No { get; set; }

        [StringLength(1)]
        public string Flourecense { get; set; }

        [StringLength(1)]
        public string Blue_R { get; set; }

        [StringLength(20)]
        public string Fabric_Width { get; set; }

        [StringLength(20)]
        public string Fabric_Type { get; set; }

        [StringLength(20)]
        public string Original_GF_No { get; set; }

        [StringLength(20)]
        public string Finishing_No { get; set; }

        [StringLength(10)]
        public string Workshop { get; set; }

        [StringLength(20)]
        public string Machine_No { get; set; }

        [StringLength(20)]
        public string JieJing_GF_No { get; set; }

        [StringLength(20)]
        public string Suggestion_Reed { get; set; }

        [StringLength(20)]
        public string Original_Reed { get; set; }

        [StringLength(30)]
        public string Suggestion_Tooth { get; set; }

        [StringLength(30)]
        public string Original_Tooth { get; set; }

        [StringLength(20)]
        public string Finishing_Man { get; set; }

        [StringLength(1)]
        public string Is_Burn { get; set; }

        [StringLength(1)]
        public string Is_Sizing { get; set; }

        [StringLength(1)]
        public string Is_Mercerizing { get; set; }

        [StringLength(1)]
        public string Is_Blue_R { get; set; }

        [StringLength(1)]
        public string Is_Other { get; set; }

        [StringLength(100)]
        public string Other_Finishing { get; set; }

        [StringLength(20)]
        public string Designer { get; set; }

        public DateTime? Design_date { get; set; }

        [StringLength(20)]
        public string Maker { get; set; }

        public DateTime? Make_date { get; set; }

        [StringLength(20)]
        public string Sender { get; set; }

        public DateTime? Send_date { get; set; }

        [StringLength(20)]
        public string Finishing_Receiver { get; set; }

        public DateTime? Finishing_Date { get; set; }

        [StringLength(20)]
        public string HL_Receiver { get; set; }

        public DateTime? HL_Date { get; set; }

        [StringLength(5000)]
        public string Drawing { get; set; }

        [StringLength(100)]
        public string Inserting { get; set; }

        [StringLength(20)]
        public string Main_Structure { get; set; }

        [StringLength(20)]
        public string Side_Structure { get; set; }

        [StringLength(1)]
        public string Is_Left { get; set; }

        [StringLength(7)]
        public string RevVer { get; set; }

        [StringLength(800)]
        public string Remark { get; set; }

        [StringLength(10)]
        public string Axes { get; set; }

        [StringLength(500)]
        public string Side_Drawing { get; set; }

        [StringLength(50)]
        public string Pattern_Remarks { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool Is_AGOA { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool Is_Finishing { get; set; }

        [StringLength(30)]
        public string Reference_Code { get; set; }

        [StringLength(10)]
        public string Factory { get; set; }

        public bool? AutoGenerate { get; set; }

        public int? FactoryOrigin { get; set; }

        [StringLength(1)]
        public string Difficulty { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? grade_cent { get; set; }

        [StringLength(50)]
        public string Finish_List { get; set; }

        public int? nAxes { get; set; }

        [StringLength(30)]
        public string StructCoef { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Closeness { get; set; }

        public int? nRoyalType { get; set; }

        public int? nFreeFitType { get; set; }

        public bool? Is_doublefaceArt { get; set; }

        [StringLength(10)]
        public string warp_Density_ratio { get; set; }

        [StringLength(10)]
        public string weft_Density_ratio { get; set; }

        public bool? Is_manual_edit { get; set; }

        [StringLength(20)]
        public string virtual_weft_density { get; set; }

        public bool? Is_manual_factory { get; set; }

        [StringLength(50)]
        public string Reed_Ref_info { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Cale_Width { get; set; }
    }
}
