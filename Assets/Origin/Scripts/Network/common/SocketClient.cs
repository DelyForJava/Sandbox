using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NetworkInterface
{
    public abstract class SocketClient : IDisposable {

        public NetWorkState _netWorkState = NetWorkState.CLOSED;
        protected Socket _socket;

        public IPAddress GetIPv6(string host)
        {
            IPAddress ipAddress = null;
            try
            {
                //IPAddress[] addresses = Dns.GetHostEntry(host).AddressList;
                ipAddress = IPAddress.Parse(host);
            

                //foreach (var item in addresses)
                //{
                //    if (item.AddressFamily == AddressFamily.InterNetworkV6)
                //    {
                //        ipAddress = item;
                //        break;
                //    }
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
            }

            Console.WriteLine("IPV6 " + ipAddress);
            return ipAddress;
        }

        public IPAddress GetIPv4(string host)
        {
            IPAddress ipAddress = null;
            try
            {
                ipAddress = IPAddress.Parse(host);
                //IPAddress[] addresses = Dns.GetHostEntry(host).AddressList;
                //foreach (var item in addresses)
                //{
                //    if (item.AddressFamily == AddressFamily.InterNetwork)
                //    {
                //        ipAddress = item;
                //        break;
                //    }
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
            }

            Console.WriteLine("IPV4 " + ipAddress);

            return ipAddress;
        }

        public virtual void processMessage(Message msg) {}

        public virtual void Disconnect() {}

        public virtual void Dispose()
        {
            Dispose(true);
            // Requests that the common language runtime not call the finalizer for the specified object. 
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code
        public virtual void Dispose(bool disposing)
        {
        }
    }
}