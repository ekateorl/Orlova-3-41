using Ninject.Modules;
using DAL.Repository;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Util
{
    class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbRepository>().To<DbRepositorySQL>().InSingletonScope();

        }
    }
}
