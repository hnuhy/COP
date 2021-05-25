using System;
using System.Collections.Generic;
using System.Text;

namespace openapi_client_pure
{
    public class OpenClientSecurityException : Exception
    {
		private static long serialVersionUID = 8231819681445578340L;

		public OpenClientSecurityException()
		{
		}

		public OpenClientSecurityException(string message):base(message)
		{
		}

	}
}
