namespace GewProductivityAppService.DAL.GETNT62.GewPrdAppDB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PrdAppDbContext : DbContext
    {
        public PrdAppDbContext()
            : base("name=GewPrdAppDB")
        {
            // ��Ҫ��EF 4.3�Ϲر����ݿ��ʼ������
            Database.SetInitializer<PrdAppDbContext>(null);
        }

        public virtual DbSet<peAppWvUser> peAppWvUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<peAppWvUser>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<peAppWvUser>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<peAppWvUser>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<peAppWvUser>()
                .Property(e => e.dept)
                .IsUnicode(false);

            modelBuilder.Entity<peAppWvUser>()
                .Property(e => e.NfcCardNo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<peAppWvUser>()
                .Property(e => e.SubDept)
                .IsUnicode(false);

            modelBuilder.Entity<peAppWvUser>()
                .Property(e => e.ConnectionId)
                .IsUnicode(false);
        }
    }
}
