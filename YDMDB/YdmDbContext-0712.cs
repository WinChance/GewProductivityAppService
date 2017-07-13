namespace YDMDB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class YdmDbContext : DbContext
    {
        public YdmDbContext()
            : base(GEW_MIS01.ConnectionStrings.YdmDbConnectionString)
        {
            // 需要在EF 4.3上关闭数据库初始化策略
            Database.SetInitializer<YdmDbContext>(null);
        }

        public virtual DbSet<rtProduction> rtProductions { get; set; }
        public virtual DbSet<uvw_BatchInfo> uvw_BatchInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*
            DatabaseGeneratedOption枚举包括三个成员：

            (1) None：数据库不生成值

            (2) Identity：当插入行时，数据库生成值

            (3) Computed：当插入或更新行时，数据库生成值
            */
            //modelBuilder.Entity<peAppWvYieldCheck>().Property(e => e.input_time).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Machine_NO)
                .IsUnicode(false);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Rack_NO)
                .IsUnicode(false);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Batch_NO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Yarn_Type)
                .IsUnicode(false);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Yarn_Count)
                .IsUnicode(false);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Yarn_Lot)
                .IsUnicode(false);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Color_Code)
                .IsUnicode(false);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Is_QC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Material_Usage)
                .IsUnicode(false);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Output)
                .HasPrecision(10, 2);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Pay_Type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Unit_Price)
                .HasPrecision(18, 3);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Shift)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Worker_ID)
                .IsUnicode(false);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Inputer_ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Input_Time)
                .IsUnicode(false);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Way)
                .IsUnicode(false);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Department)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Sarong_No)
                .IsUnicode(false);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.SarongID)
                .IsUnicode(false);

            modelBuilder.Entity<rtProduction>()
                .Property(e => e.Place_Axis)
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Job_NO)
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Batch_NO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Color_Code)
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Yarn_Type)
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Yarn_Count)
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Yarn_Lot)
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Machine_Model)
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Machine_ID)
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Cone_Unit_Weight)
                .HasPrecision(9, 2);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Trim_Beam_Unit_Weight)
                .HasPrecision(9, 2);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Ratio)
                .HasPrecision(9, 2);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Factory)
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.GF_NO)
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Warp_Weft)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Weight)
                .HasPrecision(9, 2);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Total_Weight)
                .HasPrecision(9, 2);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Dye_Type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Department)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Is_NY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Raw_Yarn_Weight)
                .HasPrecision(9, 2);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Put_Dye_Weight)
                .HasPrecision(9, 2);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Material_Type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Material_Quality)
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Customer)
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Trim_Length)
                .HasPrecision(9, 2);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Destination)
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Pipe_Color)
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Mini_Pack)
                .IsUnicode(false);

            modelBuilder.Entity<uvw_BatchInfo>()
                .Property(e => e.Location)
                .IsUnicode(false);
        }
    }
}
