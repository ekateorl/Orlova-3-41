namespace DAL.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }

        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceSpecialist> ServiceSpecialist { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TimeSlot> TimeSlot { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<WorkDay> WorkDay { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Appointment>()
                .HasMany(e => e.TimeSlot1)
                .WithOptional(e => e.Appointment1)
                .HasForeignKey(e => e.AppointmentFk);

            modelBuilder.Entity<Category>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Service)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.CategoryFk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Appointment)
                .WithRequired(e => e.Client)
                .HasForeignKey(e => e.ClientFk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Service>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Service>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Appointment)
                .WithRequired(e => e.Service)
                .HasForeignKey(e => e.ServiceFk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.ServiceSpecialist)
                .WithRequired(e => e.Service)
                .HasForeignKey(e => e.ServiceFk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TimeSlot>()
                .HasMany(e => e.Appointment)
                .WithRequired(e => e.TimeSlot)
                .HasForeignKey(e => e.TimeSlotFk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Appointment)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.ReceptionistFk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ServiceSpecialist)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.SpecialistFk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.WorkDay)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.SpecialistFk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<UserType>()
                .HasMany(e => e.User)
                .WithRequired(e => e.UserType)
                .HasForeignKey(e => e.UserTypeFk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkDay>()
                .HasMany(e => e.TimeSlot)
                .WithRequired(e => e.WorkDay)
                .HasForeignKey(e => e.WorkDayFk)
                .WillCascadeOnDelete(false);
        }
    }
}
