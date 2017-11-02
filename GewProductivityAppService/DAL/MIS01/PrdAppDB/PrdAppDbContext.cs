
namespace GewProductivityAppService.DAL.MIS01.Temp
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PrdAppDbContext : DbContext
    {
        public PrdAppDbContext()
            : base(ConnectionStrings.PrdAppDbConnectionString)
        {
            // ��Ҫ��EF 4.3�Ϲر����ݿ��ʼ������
            Database.SetInitializer<PrdAppDbContext>(null);
        }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(e => e.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Owner)
                .IsUnicode(false);
        }
    }
}
