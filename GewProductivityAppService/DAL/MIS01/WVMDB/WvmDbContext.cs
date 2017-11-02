using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace GewProductivityAppService.DAL.MIS01.WVMDB
{
    public partial class WvmDbContext : DbContext
    {
        public WvmDbContext()
            : base(ConnectionStrings.WvmDbConnectionString)
        {
            // 需要在EF 4.3上关闭数据库初始化策略
            Database.SetInitializer<WvmDbContext>(null);
        }

        public virtual DbSet<PrdAbandonYarn> PrdAbandonYarns { get; set; }
        public virtual DbSet<PrdMachineRotateRate> PrdMachineRotateRates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*
            DatabaseGeneratedOption枚举包括三个成员：

            (1) None：数据库不生成值

            (2) Identity：当插入行时，数据库生成值

            (3) Computed：当插入或更新行时，数据库生成值
            */
            modelBuilder.Entity<PrdAbandonYarn>().Property(e => e.InputTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            modelBuilder.Entity<PrdMachineRotateRate>().Property(e => e.InputTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            modelBuilder.Entity<PrdAbandonYarn>()
                .Property(e => e.Department)
                .IsUnicode(false);

            modelBuilder.Entity<PrdAbandonYarn>()
                .Property(e => e.Process)
                .IsUnicode(false);

            modelBuilder.Entity<PrdAbandonYarn>()
                .Property(e => e.WorkerClass)
                .IsUnicode(false);

            modelBuilder.Entity<PrdAbandonYarn>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<PrdMachineRotateRate>()
                .Property(e => e.Department)
                .IsUnicode(false);

            modelBuilder.Entity<PrdMachineRotateRate>()
                .Property(e => e.Process)
                .IsUnicode(false);

            modelBuilder.Entity<PrdMachineRotateRate>()
                .Property(e => e.WorkerClass)
                .IsUnicode(false);

            modelBuilder.Entity<PrdMachineRotateRate>()
                .Property(e => e.Machine)
                .IsUnicode(false);
        }
    }
}
