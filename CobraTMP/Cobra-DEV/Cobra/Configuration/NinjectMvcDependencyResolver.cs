using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Web.Mvc;

namespace Cobra.Configuration
{
    public class NinjectMvcDependencyResolver : NinjectDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        public NinjectMvcDependencyResolver(IKernel kernel)
            : base(kernel)
        {
        }
    }
}