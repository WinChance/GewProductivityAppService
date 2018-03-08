namespace GewProductivityAppService.DAL.MIS01.YDMDB.Temp
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model2 : DbContext
    {
        public Model2()
            : base("name=Model2")
        {
        }

        public virtual DbSet<ydBatchTrace> ydBatchTraces { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ydBatchTrace>()
                .Property(e => e.Batch_NO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ydBatchTrace>()
                .Property(e => e.Machine_ID)
                .IsUnicode(false);

            modelBuilder.Entity<ydBatchTrace>()
                .Property(e => e.Lab_Dip_Type)
                .IsUnicode(false);

            modelBuilder.Entity<ydBatchTrace>()
                .Property(e => e.Apply_Yarn_Class)
                .IsUnicode(false);

            modelBuilder.Entity<ydBatchTrace>()
                .Property(e => e.Trim_Class)
                .IsUnicode(false);

            modelBuilder.Entity<ydBatchTrace>()
                .Property(e => e.Check_Result)
                .IsUnicode(false);

            modelBuilder.Entity<ydBatchTrace>()
                .Property(e => e.QC_Result)
                .IsUnicode(false);

            modelBuilder.Entity<ydBatchTrace>()
                .Property(e => e.QC_Suggestion)
                .IsUnicode(false);

            modelBuilder.Entity<ydBatchTrace>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<ydBatchTrace>()
                .Property(e => e.Trace_Type)
                .IsUnicode(false);

            modelBuilder.Entity<ydBatchTrace>()
                .Property(e => e.Repair_Type)
                .IsUnicode(false);

            modelBuilder.Entity<ydBatchTrace>()
                .Property(e => e.Record_Version)
                .IsFixedLength();

            modelBuilder.Entity<ydBatchTrace>()
                .Property(e => e.QC_FeedBack)
                .IsUnicode(false);

            modelBuilder.Entity<ydBatchTrace>()
                .Property(e => e.Inner_Yarn)
                .IsUnicode(false);

            modelBuilder.Entity<ydBatchTrace>()
                .Property(e => e.Outer_Yarn)
                .IsUnicode(false);

            modelBuilder.Entity<ydBatchTrace>()
                .Property(e => e.Color_Yarn)
                .IsUnicode(false);

            modelBuilder.Entity<ydBatchTrace>()
                .Property(e => e.Recive_LT_Time)
                .IsUnicode(false);

            modelBuilder.Entity<ydBatchTrace>()
                .Property(e => e.fixed_color)
                .IsUnicode(false);

            modelBuilder.Entity<ydBatchTrace>()
                .Property(e => e.IsSongZhou)
                .IsUnicode(false);
        }
    }
}
