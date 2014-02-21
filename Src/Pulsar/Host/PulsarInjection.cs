using System;
using System.Collections.Generic;
using System.Reflection;
using Ninject.Components;
using Ninject.Selection.Heuristics;
using Pulsar.Services;

namespace Pulsar.Host
{
	/// <summary>
	/// Pulsar injection.
	/// </summary>
	public abstract class PulsarInjection : NinjectComponent, IInjectionHeuristic
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Host.PulsarInjection"/> class.
		/// </summary>
		public PulsarInjection()
		{

		}

		/// <summary>
		/// Gets the property type to inspect.
		/// </summary>
		/// <value>The property type to inspect.</value>
		public abstract List<Type> PropertyTypeToInspect { get; }

		/// <summary>
		/// Gets the pulsar type to inspect.
		/// </summary>
		/// <value>The pulsar type to inspect.</value>
		private readonly List<Type> PulsarTypeToInspect = new List<Type>
		{
			typeof(IContentService),
			typeof(IGraphicsBatchService),
			typeof(IShapeBatchService),
			typeof(ISpriteBatchService),
			typeof(ITextBatchService),
			typeof(IWindowService)
		};

		/// <summary>
		/// Returns a value indicating whether the specified member should be injected.
		/// </summary>
		/// <param name="member">The member in question.</param>
		/// <returns>True</returns>
		/// <c>false</c>
		public bool ShouldInject(MemberInfo member)
		{
			var propertyInfo = member as PropertyInfo;

			if (member == null || propertyInfo == null)
				return false;

			return propertyInfo.CanWrite && (PropertyTypeToInspect.Contains(propertyInfo.PropertyType) || PulsarTypeToInspect.Contains(propertyInfo.PropertyType));
		}
	}
}

