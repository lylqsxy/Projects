using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cobra.Infrastructure.Data
{
    public static class DbWorkManager
    {
        #region Data Members
        private const string Key = "RootUnitOfWorkBacking"; // The key in a hashtable I'll use to index my UnitOfWork (effectively a singleton)

        private static IUnitOfWork _unitOfWorks;
        [ThreadStatic]
        private static IUnitOfWork _localUnitOfWork;

        #endregion

        // -------------------------- Public methods -------------------------- 
        /// <summary>
        /// Register the unit of work that'll be used to create root units of work.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public static void RegisterFactory(IUnitOfWork unitOfWork)
        {
            _unitOfWorks = unitOfWork;
        }
        // -------------------------- Utilities --------------------------


        // -------------------------- Setup/Start -------------------------- 
        public static IUnitOfWork Start()
        {
            UnitOfWork = _unitOfWorks;
            return _unitOfWorks;
        }

        // -------------------------- Transactions -------------------------- 
        public static void Commit()
        {
            var unitOfWork = UnitOfWork;
            if (unitOfWork == null)
            {
                throw new InvalidOperationException("Unit of work not started");
            }
            unitOfWork.Commit();
        }



        // -------------------------- Shutdown/Stop -------------------------- 

        public static void End()
        {
            //Trace.WriteLine("[Static DbWorkManager] Dispose");

            if (UnitOfWork == null)
            {
                //Trace.WriteLine("[Static DbWorkManager] Already disposed!");
                return;
            }

            //var unit = UnitOfWork;

            //UnitOfWork = null;

            //unit.Dispose();

            //Trace.WriteLine("");
        }


        // -------------------------- Internal utilities -------------------------- 
        private static IUnitOfWork UnitOfWork
        {
            get
            {
                //Trace.WriteLine( "[RootUnitOfWorkBacking] Get. Data length: {0} | Object: {1} | Thread Id: {2}".FormatInvariant( Data.Keys.Count, Data[Key] == null ? "NULL" : "OK", System.Threading.Thread.CurrentThread.ManagedThreadId ) );
                if (HttpContext.Current == null)
                {
                    return _localUnitOfWork;
                }
                return HttpContext.Current.Items[Key] as IUnitOfWork;
            }
            set
            {
                //Trace.WriteLine("[RootUnitOfWorkBacking] Set. Value: {0}".FormatInvariant(value == null ? "NULL" : "OK"));
                if (HttpContext.Current == null)
                {
                    _localUnitOfWork = value;
                }
                else
                {
                    HttpContext.Current.Items[Key] = value;
                }
            }
        }

    }
}
