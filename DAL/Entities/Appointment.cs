namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Appointment")]
    public partial class Appointment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Appointment()
        {
            TimeSlot1 = new HashSet<TimeSlot>();
        }

        public int AppointmentId { get; set; }

        public bool Done { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int ClientFk { get; set; }

        public int ServiceFk { get; set; }

        public int ReceptionistFk { get; set; }

        public int TimeSlotFk { get; set; }

        public virtual Client Client { get; set; }

        public virtual Service Service { get; set; }

        public virtual TimeSlot TimeSlot { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeSlot> TimeSlot1 { get; set; }
    }
}
