namespace PDMDB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PdmDbContext : DbContext
    {
        public PdmDbContext()
            : base(GEW_MIS01.ConnectionStrings.PdmDbConnectionString)
        {
            // 需要在EF 4.3上关闭数据库初始化策略
            Database.SetInitializer<PdmDbContext>(null);
        }

        public virtual DbSet<hlOutput> hlOutputs { get; set; }
        public virtual DbSet<hlProductionStaff> hlProductionStaffs { get; set; }
        public virtual DbSet<hlBasicInfo> hlBasicInfoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<hlOutput>()
                .Property(e => e.HL_No)
                .IsUnicode(false);

            modelBuilder.Entity<hlOutput>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<hlOutput>()
                .Property(e => e.Class)
                .IsUnicode(false);

            modelBuilder.Entity<hlOutput>()
                .Property(e => e.Post)
                .IsUnicode(false);

            modelBuilder.Entity<hlOutput>()
                .Property(e => e.Sys_Score)
                .HasPrecision(10, 1);

            modelBuilder.Entity<hlOutput>()
                .Property(e => e.Dync_Score)
                .HasPrecision(10, 1);

            modelBuilder.Entity<hlOutput>()
                .Property(e => e.ProValue)
                .HasPrecision(10, 3);

            modelBuilder.Entity<hlOutput>()
                .Property(e => e.ModifyName)
                .IsUnicode(false);

            modelBuilder.Entity<hlOutput>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<hlProductionStaff>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<hlProductionStaff>()
                .Property(e => e.Post)
                .IsUnicode(false);

            modelBuilder.Entity<hlProductionStaff>()
                .Property(e => e.Class)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.HL_No)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.LB_No)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Pattern_Name)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Warp_Density)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Weft_Density)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Grey_Theory_Warp_Density)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Grey_Theory_Weft_Density)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Grey_Actual_Warp_Density)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Grey_Actual_Weft_Density)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Yarn_Warp_Count)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Yarn_Weft_Count)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Mercerizing)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Mercerize_GF_No)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Flourecense)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Blue_R)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Fabric_Width)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Fabric_Type)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Original_GF_No)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Finishing_No)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Workshop)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Machine_No)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.JieJing_GF_No)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Suggestion_Reed)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Original_Reed)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Suggestion_Tooth)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Original_Tooth)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Finishing_Man)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Is_Burn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Is_Sizing)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Is_Mercerizing)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Is_Blue_R)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Is_Other)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Other_Finishing)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Designer)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Maker)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Sender)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Finishing_Receiver)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.HL_Receiver)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Drawing)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Inserting)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Main_Structure)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Side_Structure)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Is_Left)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.RevVer)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Axes)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Side_Drawing)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Pattern_Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Reference_Code)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Factory)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Difficulty)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.grade_cent)
                .HasPrecision(5, 1);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Finish_List)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.StructCoef)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Closeness)
                .HasPrecision(5, 2);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.warp_Density_ratio)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.weft_Density_ratio)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.virtual_weft_density)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Reed_Ref_info)
                .IsUnicode(false);

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.Cale_Width)
                .HasPrecision(10, 2);
        }
    }
}
