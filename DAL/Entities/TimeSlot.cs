namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TimeSlot")]
    public partial class TimeSlot
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimeSlot()
        {
            Appointment = new HashSet<Appointment>();
        }

        public int TimeSlotId { get; set; }

        public TimeSpan Beginning { get; set; }

        public TimeSpan Duration { get; set; }

        public int WorkDayFk { get; set; }

        public int? AppointmentFk { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointment { get; set; }

        public virtual Appointment Appointment1 { get; set; }

        public virtual WorkDay WorkDay { get; set; }
    }
}
