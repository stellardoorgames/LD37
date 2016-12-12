using UnityEngine;
using System.Collections;
using System;

namespace UnityCommon
{
	public class TypeUtils {

		public static System.Type FindType(string typeName, bool useFullName = false, bool ignoreCase = false)
		{
			if (string.IsNullOrEmpty(typeName)) return null;

			StringComparison e = (ignoreCase) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			if (useFullName)
			{
				foreach (var assemb in System.AppDomain.CurrentDomain.GetAssemblies())
				{
					foreach (var t in assemb.GetTypes())
					{
						if (string.Equals(t.FullName, typeName, e)) return t;
					}
				}
			}
			else
			{
				foreach (var assemb in System.AppDomain.CurrentDomain.GetAssemblies())
				{
					foreach (var t in assemb.GetTypes())
					{
						if (string.Equals(t.FullName, typeName, e)) return t;
					}
				}
			}
			return null;
		}
	}
}