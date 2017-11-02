using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace GewProductivityAppService.DAL.MIS01.WVMDB
{
    public partial class WvmDbContext : DbContext
    {
        public WvmDbContext()
            : base(ConnectionStrings.WvmDbConnectionString)
        {
            // ��Ҫ��EF 4.3�Ϲر����ݿ��ʼ������
            Database.SetInitializer<WvmDbContext>(null);
        }

        public virtual DbSet<PrdAbandonYarn> PrdAbandonYarns { get; set; }
        public virtual DbSet<PrdMachineRotateRate> PrdMachineRotateRates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*
            DatabaseGeneratedOptionö�ٰ���������Ա��

            (1) None�����ݿⲻ����ֵ

            (2) Identity����������ʱ�����ݿ�����ֵ

            (3) Computed��������������ʱ�����ݿ�����ֵ
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
