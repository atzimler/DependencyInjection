using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATZ.DependencyInjection
{
    public static class DependencyResolver
    {
        #region Private Variables
        private static IKernel _instance;
        #endregion

        #region Public Properties
        public static IKernel Instance
        {
            get { return _instance; }
        }
        #endregion

        #region Constructors
        static DependencyResolver()
        {
            _instance = new StandardKernel();
        }
        #endregion
    }
}
