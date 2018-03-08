using System.Data.Entity;

namespace GewProductivityAppService.DAL.MIS01.YDMDB
{
    public partial class YdmDbContext : DbContext
    {
        public YdmDbContext()
            : base(ConnectionStrings.YdmDbConnectionString)
        {
            // 需要在EF 4.3上关闭数据库初始化策略
            Database.SetInitializer<YdmDbContext>(null);
        }

        public virtual DbSet<prdAppSarong> prdAppSarongs { get; set; }
        public virtual DbSet<rtProduction> rtProductions { get; set; }
        public virtual DbSet<SongZhouinfo> SongZhouinfoes { get; set; }
        public virtual DbSet<ydBatchTrace> ydBatchTraces { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
        }
    }
}
