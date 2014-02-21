using Ninject;
using Ninject.Selection.Heuristics;

namespace Pulsar.Host
{
	/// <summary>
	/// Ninject host.
	/// </summary>
	internal sealed class PulsarHost
	{
		/// <summary>
		/// The instance.
		/// </summary>
		private static PulsarHost _instance;

		/// <summary>
		/// The sync root.
		/// </summary>
		private static readonly object _syncRoot = new object();

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		internal static PulsarHost Instance
		{
			get
			{
				lock (_syncRoot)
				{
					return _instance ?? (_instance = new PulsarHost());
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Environment.NinjectHost"/> class.
		/// </summary>
		private PulsarHost()
		{

		}

		/// <summary>
		/// True if initialized.
		/// </summary>
		private bool _isInitialized;

		/// <summary>
		/// Gets a value indicating whether this instance is initialized.
		/// </summary>
		/// <value><c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
		internal bool IsInitialized
		{
			get
			{
				lock (_syncRoot)
					return _isInitialized;
			}
			private set
			{
				lock(_syncRoot)
					_isInitialized = value;
			}
		}

		/// <summary>
		/// The kernel.
		/// </summary>
		private IKernel _kernel;

		/// <summary>
		/// Gets the kernel.
		/// </summary>
		/// <value>The kernel.</value>
		internal IKernel Kernel
		{
			get
			{
				lock (_syncRoot)
					return _kernel;
			}
			private set
			{
				lock (_syncRoot)
					_kernel = value;
			}
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		internal void Initialize()
		{
			lock (_syncRoot)
			{
				if (_isInitialized) return;

				Kernel = new StandardKernel();

				IsInitialized = true;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance has heuristic.
		/// </summary>
		/// <value><c>true</c> if this instance has heuristic; otherwise, <c>false</c>.</value>
		internal bool HasHeuristic { get; private set; }

		/// <summary>
		/// Adds the heuristic.
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		internal void AddHeuristic<T>() where T : PulsarInjection
		{
			if (HasHeuristic)
				return;

			Kernel.Components.Add<IInjectionHeuristic, T>();

			HasHeuristic = true;
		}
	}
}