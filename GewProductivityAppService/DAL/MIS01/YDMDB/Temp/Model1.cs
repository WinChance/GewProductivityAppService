namespace GewProductivityAppService.DAL.MIS01.YDMDB.Temp
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<prdSongZhouinfo> SongZhouinfoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<prdSongZhouinfo>()
                .Property(e => e.machinetype)
                .IsUnicode(false);

            modelBuilder.Entity<prdSongZhouinfo>()
                .Property(e => e.batchno)
                .IsUnicode(false);

            modelBuilder.Entity<prdSongZhouinfo>()
                .Property(e => e.ydoperator)
                .IsUnicode(false);

            modelBuilder.Entity<prdSongZhouinfo>()
                .Property(e => e.properator)
                .IsUnicode(false);
        }
    }
}
