using System.Data.Entity;

namespace GewProductivityAppService.DAL.MIS01.PDMDB
{
    public partial class PdmDbContext : DbContext
    {
        public PdmDbContext()
            : base(ConnectionStrings.PdmDbConnectionString)
        {
            // 需要在EF 4.3上关闭数据库初始化策略
            Database.SetInitializer<PdmDbContext>(null);
        }

        public virtual DbSet<hlOutput> hlOutputs { get; set; }
        public virtual DbSet<hlProductionStaff> hlProductionStaffs { get; set; }
        public virtual DbSet<Pattern2HL> Pattern2HL { get; set; }
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

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.strLBNo)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Style_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.strPatternName)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Combo)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.strWYarn)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.strFYarn)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.strSgjn)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.strPatternRemark)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.strRefNo)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.strKDNo)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.strHLNo)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.strArtSender)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.strHLRemark)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.strFeedbackRemark)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.strBackupRemark)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.FabricType)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Program)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.StyleNo)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Season)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.series)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.PatternType)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.SONO)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.shrinkageL)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.shrinkageW)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.RePeatH)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.RepeatV)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.SJSRemark)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Unit)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Width)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Yarn_Skein_Ref)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Pattern_Ref)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Handfeel_Ref)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Brushing_Ref)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Finishing_Ref)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Finishing)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Washing_Type)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.AC_Holder_Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Layout_Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Other_Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Pattern_Group)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Finish_List)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.wv_dept)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Diffcult)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Redip_Code_List)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.component)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Fab_Color_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Fab_Color_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Shade)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Fab_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Operator)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Affirm_Man)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.HandFeel_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Request_List)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.HL_Designer)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Affirm_List)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.HLChecker)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.sSpeFnPlace)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.FP_color_code)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.FP_Remark)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.FP_Art_NO)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.FP_effect)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.FP_type)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.FP_Shape)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.strHBNo)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.customer_finishing)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.sel_blankrt_part)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Before_hl_no)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Redip_hlno)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Printer)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.GEK_Color_Standard)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.GEK_Color_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Cale_Width)
                .HasPrecision(10, 1);

            modelBuilder.Entity<Pattern2HL>()
                .Property(e => e.Counter_Man)
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

            modelBuilder.Entity<hlBasicInfo>()
                .Property(e => e.HealdingScore)
                .HasPrecision(10, 1);
        }
    }
}
