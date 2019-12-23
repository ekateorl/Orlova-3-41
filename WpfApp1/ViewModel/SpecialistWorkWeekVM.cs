using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace WpfApp1.ViewModel
{
   
    public class SpecialistWorkWeekVM
    {
        IDbRepository crud;
        public User Specialist { get; set; }
        public WorkDayVM Monday { get; set; }
        public WorkDayVM Tuesday { get; set; }
        public WorkDayVM Wednesday { get; set; }
        public WorkDayVM Thursday { get; set; }
        public WorkDayVM Friday { get; set; }
        public WorkDayVM Saturday { get; set; }
        public WorkDayVM Sunday { get; set; }


        public SpecialistWorkWeekVM(IDbRepository crud, User Specialist, DateTime monday)
        {
            this.crud = crud;
            this.Specialist = Specialist;
            Monday = new WorkDayVM(crud.WorkDays.GetList().Where(i => i.SpecialistFk == Specialist.UserId && i.Date == monday).FirstOrDefault(), monday, Specialist);
            Tuesday = new WorkDayVM(crud.WorkDays.GetList().Where(i => i.SpecialistFk == Specialist.UserId && i.Date == monday.AddDays(1)).FirstOrDefault(), monday.AddDays(1), Specialist);
            Wednesday = new WorkDayVM(crud.WorkDays.GetList().Where(i => i.SpecialistFk == Specialist.UserId && i.Date == monday.AddDays(2)).FirstOrDefault(), monday.AddDays(2), Specialist);
            Thursday = new WorkDayVM(crud.WorkDays.GetList().Where(i => i.SpecialistFk == Specialist.UserId && i.Date == monday.AddDays(3)).FirstOrDefault(), monday.AddDays(3), Specialist);
            Friday = new WorkDayVM(crud.WorkDays.GetList().Where(i => i.SpecialistFk == Specialist.UserId && i.Date == monday.AddDays(4)).FirstOrDefault(), monday.AddDays(4), Specialist);
            Saturday = new WorkDayVM(crud.WorkDays.GetList().Where(i => i.SpecialistFk == Specialist.UserId && i.Date == monday.AddDays(5)).FirstOrDefault(), monday.AddDays(5), Specialist);
            Sunday = new WorkDayVM(crud.WorkDays.GetList().Where(i => i.SpecialistFk == Specialist.UserId && i.Date == monday.AddDays(6)).FirstOrDefault(), monday.AddDays(6), Specialist);
        }
    }
}
