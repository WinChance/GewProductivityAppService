namespace GewProductivityAppService.DAL.MIS01.FNMDB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FnmDbContext : DbContext
    {
        public FnmDbContext()
            : base(ConnectionStrings.FnmDbConnectionString)
        {
            // 需要在EF 4.3上关闭数据库初始化策略
            Database.SetInitializer<FnmDbContext>(null);
        }

        public virtual DbSet<fnHold> fnHolds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<fnHold>()
                .Property(e => e.Operation_Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<fnHold>()
                .Property(e => e.FN_Card)
                .IsUnicode(false);

            modelBuilder.Entity<fnHold>()
                .Property(e => e.Hold_Reason)
                .IsUnicode(false);

            modelBuilder.Entity<fnHold>()
                .Property(e => e.Holder)
                .IsUnicode(false);

            modelBuilder.Entity<fnHold>()
                .Property(e => e.Release_Reason)
                .IsUnicode(false);

            modelBuilder.Entity<fnHold>()
                .Property(e => e.Releaser)
                .IsUnicode(false);
        }
    }
}
