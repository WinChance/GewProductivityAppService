namespace GewProductivityAppService.DAL.GETNT103
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("machine")]
    public partial class machine
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short MachineID { get; set; }

        [Required]
        [StringLength(4)]
        public string MachineName { get; set; }

        [StringLength(4)]
        public string GroupNo { get; set; }

        public float? Plus { get; set; }

        [StringLength(20)]
        public string ProductID { get; set; }

        public float? Correct { get; set; }

        public bool OnLine { get; set; }

        public int? TopLocation { get; set; }

        public int? LeftLocation { get; set; }

        [StringLength(50)]
        public string ProductInfo { get; set; }

        public short? DeptID { get; set; }

        public short? ComNo { get; set; }

        public int? MachineNo { get; set; }

        public int? MachineType { get; set; }

        public byte? Visible { get; set; }

        public int? TopLocation2 { get; set; }

        public int? LeftLocation2 { get; set; }

        public int? MaxLength { get; set; }

        public int? GroupID1 { get; set; }

        public int? GroupID2 { get; set; }

        public int? GroupID3 { get; set; }

        public int? GroupID4 { get; set; }

        public int? GroupID5 { get; set; }

        public int? GroupID6 { get; set; }

        public int? GroupID7 { get; set; }

        public int? GroupID8 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LengthPerTurn { get; set; }

        public short? TermNo { get; set; }

        public int? BeamTypeID { get; set; }

        [StringLength(15)]
        public string IP { get; set; }

        [Column(TypeName = "numeric")]
        public decimal dwxs { get; set; }

        [Column(TypeName = "numeric")]
        public decimal RemainCloth { get; set; }

        public int Status { get; set; }

        public short CurSpeed { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? LastChangeTime { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? LastStartTime { get; set; }

        public int? GroupID9 { get; set; }

        public int? GroupID10 { get; set; }

        public int? GroupID11 { get; set; }

        public int? GroupID12 { get; set; }

        public int? MainMachineID { get; set; }

        public byte YieldMode { get; set; }

        public int? LastWeaverID { get; set; }

        public int WorkCycleType { get; set; }

        public int? OracleNo { get; set; }

        public int? LeftLocation3 { get; set; }

        public int? TopLocation3 { get; set; }

        [StringLength(10)]
        public string Version { get; set; }

        public int RepeaterNo { get; set; }

        public int ChannelNo { get; set; }
    }
}
