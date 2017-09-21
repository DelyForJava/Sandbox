using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkInterface
{
	public class OdaoEventManager : IDisposable
	{
		private Dictionary<ushort, List<Action<Message>>> eventMap;

		public OdaoEventManager()
		{
			this.eventMap = new Dictionary<ushort, List<Action<Message>>>();
		}

		public void AddOnEvent(ushort eventName, Action<Message> callback)
		{
			List<Action<Message>> list = null;
			if (this.eventMap.TryGetValue(eventName, out list))
			{
				list.Add(callback);
			}
			else
			{
				list = new List<Action<Message>>();
				list.Add(callback);
				this.eventMap.Add(eventName, list);
			}
		}

		public void InvokeOnEvent(ushort route, Message msg)
		{
			if (!this.eventMap.ContainsKey(route)) 
				return;

			List<Action<Message>> list = eventMap [route];
			if (list.Count > 0) {
				for (int i = 0; i < list.Count; ++i) {
					list [i].Invoke (msg);
				}
			} else {
				Console.WriteLine (string.Format ("NotImplementedException : 0x{0:x}", route));
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing)
		{
			if (disposing) {
				this.eventMap.Clear ();
				this.eventMap = null;
			}
		}
	}
}
