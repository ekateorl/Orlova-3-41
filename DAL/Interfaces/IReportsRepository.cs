using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IReportsRepository
    {
        List<Appointment> ApointmentsForPeriod(DateTime date1, DateTime date2);
        List<Appointment> ApointmentsForPeriod(DateTime date1, DateTime date2, int SpecialistId);
        List<SpecialistWorkTime> WorkTimes(DateTime date1, DateTime date2);
        List<ServiceInfo> ServiceInfos(DateTime date1, DateTime date2);
        List<ServiceInfo> ServiceInfos(DateTime date1, DateTime date2, int SpecialistId);
    }
}
