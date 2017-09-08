using System;
using System.Net.Sockets;

namespace NetworkInterface
{
    class StateObject
    {
        public const int BufferSize = 1024;
        internal byte[] buffer = new byte[BufferSize];
    }

    public class Transporter
    {
		protected int HeadLength = 4;

        private Socket socket;
        private Action<byte[]> messageProcesser;

        //Used for get message
        private StateObject stateObject = new StateObject();
		protected TransportState transportState;
        private IAsyncResult asyncReceive;
        private IAsyncResult asyncSend;
        private bool onSending = false;
        private bool onReceiving = false;
		protected byte[] headBuffer;
		protected byte[] buffer;
		protected int bufferOffset = 0;
		protected int pkgLength = 0;
        internal Action onDisconnect = null;

        public Transporter(Socket socket, Action<byte[]> processer)
        {
			HeadLength = 4;//TYPE,LENGTH
            this.socket = socket;
            this.messageProcesser = processer;
            transportState = TransportState.readHead;
        }

        public void start()
		{	
			headBuffer = new byte[HeadLength];
            this.receive();
        }

        public void send(byte[] buffer)
        {
            if (this.transportState != TransportState.closed)
            {
                /*
                string str = "";
                foreach (byte code in buffer)
                {
                    str += code.ToString();
                }
                Console.WriteLine("send:" + buffer.Length + " " + str.Length + "  " + str);
                */
                try
                {
                    this.asyncSend = socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(sendCallback), socket);
                }
                catch(System.Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                this.onSending = true;
            }
        }

        private void sendCallback(IAsyncResult asyncSend)
        {   
            if (this.transportState == TransportState.closed) 
				return;
			
            socket.EndSend(asyncSend);
            this.onSending = false;
			//Console.WriteLine("sendCallback " + this.transportState + " , " + this.onSending);
        }

        public void receive()
        {
			//Console.WriteLine("receive state : " + this.transportState + " : " +socket.Available);
            this.asyncReceive = socket.BeginReceive(stateObject.buffer, 0, stateObject.buffer.Length, SocketFlags.None, new AsyncCallback(endReceive), stateObject);
            this.onReceiving = true;
            //Console.WriteLine("#############onReceiving -> " + this.onReceiving);
        }

        internal void close()
        {			
			if (this.transportState == TransportState.closed)
				return;
            //Console.WriteLine("transporter close " + this.socket.Connected);
            this.socket.Close();
			this.transportState = TransportState.closed;

//			// EndSend can only be called once per asynchronous operation
//            try {
//				global::UnityEngine.Debug.Log(socket.Connected);
//
//				if(this.onReceiving) {
//					global::UnityEngine.Debug.Log ("EndReceive");
//					socket.EndReceive (this.asyncReceive);
//					this.onReceiving = false;
//				}
//				if(this.onSending) {
//					global::UnityEngine.Debug.Log ("EndSend");
//					socket.EndSend(this.asyncSend);
//					this.onSending = false;
//				}
//            }
//			catch (Exception e) {
//				this.onSending = false;
//				this.onReceiving = false;
//				global::UnityEngine.Debug.LogError ("Transporter close -> " + e.Message.ToString ());
//				DebugInfo.Message (e.Message.ToString ());
//               // Console.WriteLine(e.Message);
//            }
        }

        private void endReceive(IAsyncResult asyncReceive)
        {
            if (this.transportState == TransportState.closed)
                return;
			
            StateObject state = (StateObject)asyncReceive.AsyncState;
            Socket socket = this.socket;
            int length = 0;

            try
            {
                length = socket.EndReceive(asyncReceive);

                this.onReceiving = false;

                if (length > 0) {
                    processBytes(state.buffer, 0, length);
                    if (this.transportState != TransportState.closed) {
						receive();
					}
                }
            }
            catch (System.Net.Sockets.SocketException e) {
				Console.WriteLine ("receive again exception error " + e.Message.ToString ());
                length = -1;
            }
			catch (System.Exception e) {
				Console.WriteLine ("code exception error " + e.Message.ToString ());
                length = -1;
			}
            finally
            {
                if(length <= 0)
                {
					Console.WriteLine("FIN OR RST " + this.onDisconnect);

                    this.close();

                    if (this.onDisconnect != null)
                        this.onDisconnect();
                }
            }
        }

        internal void processBytes(byte[] bytes, int offset, int limit)
        {
            if (this.transportState == TransportState.readHead)
            {
                readHead(bytes, offset, limit);
            }
            else if (this.transportState == TransportState.readBody)
            {
                readBody(bytes, offset, limit);
            }
        }

		virtual protected void OnParseHead()
		{	
			//Get package length
			pkgLength = (headBuffer[1] << 16) + (headBuffer[2] << 8) + headBuffer[3];
		}

		private bool readHead(byte[] bytes, int offset, int limit)
        {
            int length = limit - offset;
            int headNum = HeadLength - bufferOffset;
	
            if (length >= headNum)
            {
                //Write head buffer
                writeBytes(bytes, offset, headNum, bufferOffset, headBuffer);
				OnParseHead ();

                //Init message buffer
                buffer = new byte[HeadLength + pkgLength];
                writeBytes(headBuffer, 0, HeadLength, buffer);
                offset += headNum;
                bufferOffset = HeadLength;
                this.transportState = TransportState.readBody;

                if (offset <= limit) processBytes(bytes, offset, limit);
                return true;
            }
            else
            {
                writeBytes(bytes, offset, length, bufferOffset, headBuffer);
                bufferOffset += length;
                return false;
            }
        }

        private void readBody(byte[] bytes, int offset, int limit)
        {
            int length = pkgLength + HeadLength - bufferOffset;
            if ((offset + length) <= limit)
            {
                writeBytes(bytes, offset, length, bufferOffset, buffer);
                offset += length;

                //Invoke the protocol api to handle the message
                this.messageProcesser.Invoke(buffer);
                this.bufferOffset = 0;
                this.pkgLength = 0;

                if (this.transportState != TransportState.closed)
                    this.transportState = TransportState.readHead;
                if (offset < limit)
                    processBytes(bytes, offset, limit);
            }
            else
            {
                writeBytes(bytes, offset, limit - offset, bufferOffset, buffer);
                bufferOffset += limit - offset;
                this.transportState = TransportState.readBody;
            }
        }

		protected void writeBytes(byte[] source, int start, int length, byte[] target)
        {
            writeBytes(source, start, length, 0, target);
        }

		protected void writeBytes(byte[] source, int start, int length, int offset, byte[] target)
        {
            for (int i = 0; i < length; i++)
            {
                target[offset + i] = source[start + i];
            }
        }

        private void print(byte[] bytes, int offset, int length)
        {
            for (int i = offset; i < length; i++)
                Console.Write(Convert.ToString(bytes[i], 16) + " ");
            Console.WriteLine();
        }
    }
}