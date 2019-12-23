using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModel
{
    public class AppConfDialogVM : DialogVM
    {
        public AppointmentVM Appointment { get; set; }
        public AppConfDialogVM(AppointmentVM Appointment) : base()
        {
            this.Appointment = Appointment;
        }
    }
}
