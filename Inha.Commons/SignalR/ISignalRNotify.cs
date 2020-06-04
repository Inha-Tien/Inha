namespace Inha.Commons.SignalR
{
    using Microsoft.AspNetCore.SignalR.Client;
    using System;
    using System.Threading.Tasks;
    public interface ISignalRNotify : IDisposable
    {
        Task Notify(string serverSignalR, string functionName, string groupName, string user, string message);
    }
    public class SignalRNotify : ISignalRNotify
    {
        #region Dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;

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
        /// Distroy
        /// </summary>
        ~SignalRNotify()
        {
            Dispose(false);
        }
        #endregion

        public async Task Notify(string serverSignalR, string functionName, string groupName, string user, string message)
        {
            HubConnection connection;
            connection = new HubConnectionBuilder()
                .WithUrl(serverSignalR)
                .Build();
            await connection.StartAsync();

            await connection.InvokeAsync(functionName, groupName,
                    user, message);
        }
    }
}
