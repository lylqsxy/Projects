using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cobra.Infrastructure.Contracts;
using Cobra.Infrastructure.Services;
using Ninject.Modules;

namespace Cobra.Infrastructure
{
    public class CobraInfrastructureNinjectModule: NinjectModule
    {
        public override void Load()
        {
             Bind<ILogService>().To<LogService>().InTransientScope();
        }
    }
}
