using System;
using System.Net;

namespace Inha.Commons.Log.Utils
{
    public sealed class RemoteEndpointMessageProperty
    {
        string address;

        int port;

        IPEndPoint remoteEndPoint;

        IRemoteEndpointProvider remoteEndpointProvider;

        InitializationState state;

        public RemoteEndpointMessageProperty(string address,
                                             int port)
        {
            if (string.IsNullOrEmpty(address))
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (port < IPEndPoint.MinPort
               || port > IPEndPoint.MaxPort)
            {
                throw new ArgumentOutOfRangeException(nameof(port));
            }

            this.port = port;
            this.address = address;
            state = InitializationState.All;
        }

        internal RemoteEndpointMessageProperty(IRemoteEndpointProvider remoteEndpointProvider)
        {
            this.remoteEndpointProvider = remoteEndpointProvider;
        }

        internal RemoteEndpointMessageProperty(IPEndPoint remoteEndPoint)
        {
            this.remoteEndPoint = remoteEndPoint;
        }

        public static string Name
        {
            get { return "System.ServiceModel.Channels.RemoteEndpointMessageProperty"; }
        }

        public string Address
        {
            get
            {
                if ((state & InitializationState.Address) != InitializationState.Address)
                {
                    lock (ThisLock)
                    {
                        if ((state & InitializationState.Address) != InitializationState.Address)
                        {
                            Initialize(false);
                        }
                    }
                }

                return address;
            }
        }

        public int Port
        {
            get
            {
                if ((state & InitializationState.Port) != InitializationState.Port)
                {
                    lock (ThisLock)
                    {
                        if ((state & InitializationState.Port) != InitializationState.Port)
                        {
                            Initialize(true);
                        }
                    }
                }

                return port;
            }
        }

        private object ThisLock { get; } = new object();

        void Initialize(bool getHostedPort)
        {
            if (remoteEndPoint != null)
            {
                address = remoteEndPoint.Address.ToString();
                port = remoteEndPoint.Port;
                state = InitializationState.All;
                remoteEndPoint = null;
            }
            else
            {
                if ((state & InitializationState.Address) != InitializationState.Address)
                {
                    address = remoteEndpointProvider.GetAddress();
                    state |= InitializationState.Address;
                }

                if (getHostedPort)
                {
                    port = remoteEndpointProvider.GetPort();
                    state |= InitializationState.Port;
                    remoteEndpointProvider = null;
                }
            }
        }

        #region Nested type: InitializationState

        [Flags]
        enum InitializationState
        {
            None = 0,

            Address = 1,

            Port = 2,

            All = 3
        }

        #endregion

        #region Nested type: IRemoteEndpointProvider

        internal interface IRemoteEndpointProvider
        {
            string GetAddress();

            int GetPort();
        }

        #endregion
    }
}
