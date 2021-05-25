using System;
using System.Reflection;

namespace openapi_client_pure
{
	class HmacAlgorithmAttr : Attribute
	{
		internal HmacAlgorithmAttr(string algo)
		{
			this.Algo = algo;
		}
		public string Algo { get; private set; }
	}

	public static class HmacAlgorithms
	{
		public static string getAlgorithm(this HmacAlgorithm p)
		{
			HmacAlgorithmAttr attr = GetAttr(p);
			return attr.Algo;
		}


		private static HmacAlgorithmAttr GetAttr(HmacAlgorithm p)
		{
			return (HmacAlgorithmAttr)Attribute.GetCustomAttribute(ForValue(p), typeof(HmacAlgorithmAttr));
		}

		private static MemberInfo ForValue(HmacAlgorithm p)
		{
			return typeof(HmacAlgorithm).GetField(Enum.GetName(typeof(HmacAlgorithm), p));
		}

		/**
	 * Return supported hmac-algorithm.
	 * 
	 * @param algo
	 * @return value of HmacAlgorithm, return <tt>null</null> if the algorithm
	 *         is not supported.
	 * 
	 */
		public static HmacAlgorithm validate(string algo)
		{
			foreach (int idx in Enum.GetValues(typeof(HmacAlgorithm)))
            {
				HmacAlgorithm e = (HmacAlgorithm)Enum.Parse(typeof(HmacAlgorithm),idx.ToString());
                if (e.getAlgorithm().Equals(algo))
                {
					return e;
                }

			}

			return HmacAlgorithm.NONE;
		}

	}

	public enum HmacAlgorithm {
		[HmacAlgorithmAttr("None")] NONE,

		/**
		 * Represents HmacSHA1
		 */
		[HmacAlgorithmAttr("HmacSHA1")] HMAC_SHA1,
		/**
		 * Represents HmacSHA256
		 */
		[HmacAlgorithmAttr("HmacSHA256")] HMAC_SHA256,
		/**
		 * Represents HmacSHA384
		 */
		[HmacAlgorithmAttr("HmacSHA384")] HMAC_SHA384,
		/**
		 * Represents HmacSHA512
		 */
		[HmacAlgorithmAttr("HmacSHA512")] HMAC_SHA512

	}
}
