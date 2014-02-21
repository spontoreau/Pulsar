using System;
using System.Collections.Generic;

namespace Pulsar.Host
{
	/// <summary>
	/// Default pulsar injection.
	/// </summary>
	public class DefaultPulsarInjection : PulsarInjection
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Host.DefaultPulsarInjection"/> class.
		/// </summary>
		public DefaultPulsarInjection ()
		{

		}

		/// <summary>
		/// The property type to inspect.
		/// </summary>
		private List<Type> _propertyTypeToInspect = new List<Type>();

		/// <summary>
		/// Gets the property type to inspect.
		/// </summary>
		/// <value>The property type to inspect.</value>
		public override List<Type> PropertyTypeToInspect 
		{
			get 
			{
				return _propertyTypeToInspect;
			}
		}
	}
}

