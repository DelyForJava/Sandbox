using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using SimpleJson;

namespace NetworkInterface
{
	public class OdaoClient : SocketClient
	{
        public event Action<NetWorkState> NetWorkStateChangedEvent;

		private bool disposed = false;
		private uint reqId = 1;

		private OdaoProtocol protocol;
		private OdaoEventManager eventManager;

		private ManualResetEvent timeoutEvent = new ManualResetEvent(false);

		//connect timeout count in millisecond
		private int timeoutMSec = 8000;    

		public OdaoClient() {}

		/// <summary>
		/// initialize socket client
		/// </summary>
		/// <param name="host">server name or server ip (www.xxx.com/127.0.0.1/::1/localhost etc.)</param>
		/// <param name="port">server port</param>
		/// <param name="callback">socket successfully connected callback(in network thread)</param>
		public void initClient(string host, int port, Action callback = null)
		{
			disposed = false;

			timeoutEvent.Reset();
			eventManager = new OdaoEventManager();
			NetWorkChanged(NetWorkState.CONNECTING);

			IPAddress ipAddress = null;

			ipAddress = GetIPv6 (host);

			if (ipAddress != null) {
				_socket = new Socket (AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
			} 
			else {
				ipAddress = GetIPv4 (host);
				if (ipAddress != null) {
					_socket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				}
			}

			if (ipAddress == null) {
				CloseClient ();
				return;
			}

			IPEndPoint ie = new IPEndPoint(ipAddress, port);

			_socket.BeginConnect(ie, new AsyncCallback((result) =>
				{
					try
					{
						_socket.EndConnect(result);
						this.protocol = new OdaoProtocol(this, _socket);
						NetWorkChanged(NetWorkState.CONNECTED);

						if (callback != null)
						{
							callback();
						}
					}
					catch (SocketException e)
					{
						Console.WriteLine(e.ToString());
                        NetWorkChanged(NetWorkState.ERROR_CONNECT);
					}
					catch(System.Exception e)
					{
						Console.WriteLine(e.ToString());
                        NetWorkChanged(NetWorkState.ERROR_CONNECT);
                    }
					finally
					{
						timeoutEvent.Set();
					}
				}), _socket);

			if (timeoutEvent.WaitOne(timeoutMSec, false))
			{
				if (_netWorkState != NetWorkState.CONNECTED && _netWorkState != NetWorkState.ERROR && _netWorkState != NetWorkState.ERROR_CONNECT)
				{
					NetWorkChanged(NetWorkState.TIMEOUT);
				}
			}
		}
			
		private void NetWorkChanged(NetWorkState state)
		{
			_netWorkState = state;

			if (NetWorkStateChangedEvent != null)
			{
				NetWorkStateChangedEvent(state);
			}
		}

		public void connect()
		{
			connect(null, null);
		} 

		public void connect(JsonObject user)
		{
			connect(user, null);
		}

		public void connect(Action<JsonObject> handshakeCallback)
		{
			connect(null, handshakeCallback);
		}

		public bool connect(JsonObject user, Action<JsonObject> handshakeCallback)
		{
			try
			{
				protocol.start(user, handshakeCallback);
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				return false;
			}
		}

		public void notify(ushort route, byte[] msg)
		{
			protocol.send(route, 0, msg);
		}

		public void notifyMP(ushort route, byte[] msg)
		{
			protocol.sendMP(route, 0, msg);
		}

		public void on(ushort route, Action<Message> action)
		{
			eventManager.AddOnEvent(route, action);
		}

		override public void processMessage(Message obj)
		{
            OdaoMessage msg = (OdaoMessage)obj;
            eventManager.InvokeOnEvent(msg.route, msg);
        }

		public bool IsConnected()
		{
			return _netWorkState == NetWorkState.CONNECTED;
		}

		public override void Disconnect()
		{
			Console.WriteLine ("disconnect");
			if (_netWorkState == NetWorkState.DISCONNECTING) {
				Console.WriteLine ("断开连接中");
				return;
			}
			NetWorkChanged (NetWorkState.DISCONNECTING);
			Dispose();
			NetWorkChanged(NetWorkState.DISCONNECTED);
		}

		override public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		// The bulk of the clean-up code
		override public void Dispose(bool disposing)
		{
			if (this.disposed)
				return;

			if (disposing)
			{
				if (this.eventManager != null)
				{
					this.eventManager.Dispose();
					this.eventManager = null;
				}

				if (this.protocol != null)
				{
					this.protocol.close();
					this.protocol = null;
				}

				try
				{
					if(_socket!=null)
					{
					    _socket.Close();
						_socket = null;
					}
				}
				catch (Exception e)
				{
					Console.WriteLine (e.ToString ());
				}

				this.disposed = true;
			}
		}

		public void CloseClient()
		{
			this.Dispose();
			NetWorkChanged(NetWorkState.CLOSED);
		}
	}
}



