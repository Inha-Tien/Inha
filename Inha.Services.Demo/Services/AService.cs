using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inha.Services.Demo.Services
{
    public class AService: IAService
    {

        public string GetName()
        {
            return "tien";
        }
        #region Dispose

        // Flag: Has Dispose already been called?
        bool disposed;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }
        /// <summary>
        ///     Distroy
        /// </summary>
        ~AService()
        {
            Dispose(false);
        }

        #endregion
    }
    public interface IAService: IDisposable
    {
        string GetName();
    }
}
