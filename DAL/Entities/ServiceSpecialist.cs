namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ServiceSpecialist")]
    public partial class ServiceSpecialist
    {
        public int ServiceSpecialistId { get; set; }

        public int ServiceFk { get; set; }

        public int SpecialistFk { get; set; }

        public virtual Service Service { get; set; }

        public virtual User User { get; set; }
    }
}
