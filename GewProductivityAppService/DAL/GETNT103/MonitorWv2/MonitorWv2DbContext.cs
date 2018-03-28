namespace GewProductivityAppService.DAL.GETNT103
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MonitorWv2DbContext : DbContext
    {
        public MonitorWv2DbContext()
            : base("name=MonitorWv2Db")
        {
        }

        public virtual DbSet<machine> machines { get; set; }
        public virtual DbSet<QiangDanTask> QiangDanTasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<machine>()
                .Property(e => e.LengthPerTurn)
                .HasPrecision(9, 8);

            modelBuilder.Entity<machine>()
                .Property(e => e.IP)
                .IsUnicode(false);

            modelBuilder.Entity<machine>()
                .Property(e => e.dwxs)
                .HasPrecision(5, 3);

            modelBuilder.Entity<machine>()
                .Property(e => e.RemainCloth)
                .HasPrecision(5, 1);

            modelBuilder.Entity<machine>()
                .Property(e => e.Version)
                .IsUnicode(false);

            modelBuilder.Entity<QiangDanTask>()
                .Property(e => e.CardNo)
                .IsUnicode(false);

            modelBuilder.Entity<QiangDanTask>()
                .Property(e => e.MachineName)
                .IsUnicode(false);

            modelBuilder.Entity<QiangDanTask>()
                .Property(e => e.Department)
                .IsUnicode(false);

            modelBuilder.Entity<QiangDanTask>()
                .Property(e => e.WeaverNo1)
                .IsUnicode(false);

            modelBuilder.Entity<QiangDanTask>()
                .Property(e => e.WeaverName1)
                .IsUnicode(false);

            modelBuilder.Entity<QiangDanTask>()
                .Property(e => e.WeaverNo2)
                .IsUnicode(false);

            modelBuilder.Entity<QiangDanTask>()
                .Property(e => e.WeaverName2)
                .IsUnicode(false);

            modelBuilder.Entity<QiangDanTask>()
                .Property(e => e.WeaverNo3)
                .IsUnicode(false);

            modelBuilder.Entity<QiangDanTask>()
                .Property(e => e.WeaverName3)
                .IsUnicode(false);

            modelBuilder.Entity<QiangDanTask>()
                .Property(e => e.WeaverClass)
                .IsUnicode(false);

            modelBuilder.Entity<QiangDanTask>()
                .Property(e => e.WeaverGroup)
                .IsUnicode(false);

            modelBuilder.Entity<QiangDanTask>()
                .Property(e => e.FeedBack)
                .IsUnicode(false);

            modelBuilder.Entity<QiangDanTask>()
                .Property(e => e.Remark)
                .IsUnicode(false);
        }
    }
}
