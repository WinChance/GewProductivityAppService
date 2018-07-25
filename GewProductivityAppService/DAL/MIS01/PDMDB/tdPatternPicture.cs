namespace GewProductivityAppService.DAL.MIS01.PDMDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tdPatternPicture")]
    public partial class tdPatternPicture
    {
        [Key]
        public int Iden { get; set; }

        public int GF_ID { get; set; }

        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Create_Time { get; set; }

        [StringLength(512)]
        public string PictureURL { get; set; }
    }
}
