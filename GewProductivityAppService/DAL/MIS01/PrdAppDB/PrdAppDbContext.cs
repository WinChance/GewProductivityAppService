using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace GewProductivityAppService.DAL.MIS01.PrdAppDB
{
    public partial class PrdAppDbContext : DbContext
    {
        public PrdAppDbContext()
            : base(ConnectionStrings.PrdAppDbConnectionString)
        {
            // 需要在EF 4.3上关闭数据库初始化策略
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
