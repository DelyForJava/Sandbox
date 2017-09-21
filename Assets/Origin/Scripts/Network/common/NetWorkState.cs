using System;
using System.ComponentModel;

namespace NetworkInterface
{
	public enum NetWorkState
	{
		[Description("initial state")]
		CLOSED,

		[Description("connecting server")]
		CONNECTING,

		[Description("server connected")]
		CONNECTED,

		[Description("disconnecting with server")]
		DISCONNECTING,

		[Description("disconnected with server")]
		DISCONNECTED,

		[Description("connect timeout")]
		TIMEOUT,

		[Description("netwrok error")]
		ERROR,

        [Description("netwrok error get ipv6")]
        ERROR_GETIPV6,

        [Description("netwrok error get ipv4")]
        ERROR_GETIPV4,

        [Description("netwrok error to connect server")]
        ERROR_CONNECT,
    }
}
