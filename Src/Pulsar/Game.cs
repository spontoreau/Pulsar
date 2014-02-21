using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Pulsar.Services.Implements.Content;
using Pulsar.Services.Implements.Graphics;
using Pulsar.Services;
using Pulsar.Host;
using System.Collections.Generic;

namespace Pulsar
{
	/// <summary>
	/// Game.
	/// </summary>
	public sealed class Game
	{
		/// <summary>
		/// Gets or sets the window service.
		/// </summary>
		/// <value>The window service.</value>
		public IWindowService WindowService { get; set; }

		/// <summary>
		/// Gets or sets the watch.
		/// </summary>
		/// <value>The watch.</value>
		private Stopwatch Watch { get; set; }

		/// <summary>
		/// Gets or sets the game time.
		/// </summary>
		/// <value>The game time.</value>
		private GameTime GameTime { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is running.
		/// </summary>
		/// <value><c>true</c> if this instance is running; otherwise, <c>false</c>.</value>
		private bool IsRunning { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is exiting.
		/// </summary>
		/// <value><c>true</c> if this instance is exiting; otherwise, <c>false</c>.</value>
		private bool IsExiting { get; set; }

		/// <summary>
		/// Gets a value indicating whether this instance is active.
		/// </summary>
		/// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
		private bool IsActive { get; set; }

		/// <summary>
		/// The _is fixed time step.
		/// </summary>
		private bool _isFixedTimeStep;

		/// <summary>
		/// Gets or sets a value indicating whether this instance is fixed time step.
		/// </summary>
		/// <value><c>true</c> if this instance is fixed time step; otherwise, <c>false</c>.</value>
		public bool IsFixedTimeStep
		{
			get
			{
				return _isFixedTimeStep;
			}
			set
			{
				if (!WindowService.IsCreated)
					return;
				//if (!WindowContext.IsCreated) return;

			    _isFixedTimeStep = value;
				WindowService.Window.SetVerticalSyncEnabled(false);
				WindowService.Window.SetFramerateLimit((value) ? 120U : 0U);//uint force to multiple by 2 to obtain refresh at 60fps
			}
		}

		/// <summary>
		/// The _is mouse visible.
		/// </summary>
		private bool _isMouseVisible;

		/// <summary>
		/// Gets or sets a value indicating whether this instance is mouse visible.
		/// </summary>
		/// <value><c>true</c> if this instance is mouse visible; otherwise, <c>false</c>.</value>
		public bool IsMouseVisible
		{
			get
			{
				return _isMouseVisible;
			}
			set
			{
				if (!WindowService.IsCreated) return;

			    _isMouseVisible = value;
				WindowService.Window.SetMouseCursorVisible(value);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Game"/> class.
		/// </summary>
		public Game()
		{
			GameTime = new GameTime();
			Modules = new List<IModule>();
			Drawables = new List<IDrawable>();
		}

		/// <summary>
		/// Adds the service.
		/// </summary>
		/// <typeparam name="TInterface">The 1st type parameter.</typeparam>
		/// <typeparam name="TImplements">The 2nd type parameter.</typeparam>
		public Game AddService<TInterface, TImplements>()
			where TImplements : TInterface
		{
			if(IsRunning)
				throw new InvalidOperationException("Game is running");

			PulsarHost.Instance.Kernel.Bind<TInterface> ().To<TImplements>().InSingletonScope();

			return this;
		}

		/// <summary>
		/// Add custom heuristic if needed (must be use if need custom service injection)
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public Game AddHeuristic<T>() where T : PulsarInjection
		{
			if(IsRunning)
				throw new InvalidOperationException("Game is running");

			PulsarHost.Instance.Initialize();
			PulsarHost.Instance.AddHeuristic<T>();

			return this;
		}

		/// <summary>
		/// Add game module
		/// </summary>
		public Game AddModule<T>() where T : IModule
		{
			PulsarHost.Instance.Kernel.Bind<IModule> ().To<T> ().InSingletonScope();

			var module = PulsarHost.Instance.Kernel.GetService(typeof(T));

			var wasAdded = Modules.Any (x => x.GetType ().IsInstanceOfType (module));

			if(wasAdded)
				throw new InvalidOperationException(string.Format("Module {0} already added", module.GetType()));

			Modules.Add ((IModule)module);

			if (module is IDrawable)
				Drawables.Add ((IDrawable)module);

			return this;
		}

		/// <summary>
		/// Gets or sets the modules.
		/// </summary>
		/// <value>The modules.</value>
		private List<IModule> Modules { get; set; }

		/// <summary>
		/// Gets or sets the drawables.
		/// </summary>
		/// <value>The drawables.</value>
		private List<IDrawable> Drawables { get; set; }

		/// <summary>
		/// Run this instance.
		/// </summary>
		public void Run()
		{
			if (IsRunning)
				throw new InvalidOperationException("Game is running");

			WindowService = (IWindowService)PulsarHost.Instance.Kernel.GetService(typeof(IWindowService));

			if (WindowService == null)
				throw new InvalidOperationException ("Window service unavaible");
				
		    IsExiting = false;
			WindowService.Created += RenderWindowCreated;
			WindowService.Creating += RenderWindowCreating;

			if (!WindowService.IsCreated)
				WindowService.Create();

			Initialize();

		    IsRunning = true; 
				               
		    while (IsRunning && !IsExiting)
		    {
		        Tick();
		    }

			Environment.Exit(1);//Allways exit the application
		}

		/// <summary>
		/// Exit this instance.
		/// </summary>
		public void Exit()
		{
			IsExiting = true;
		}

		/// <summary>
		/// Tick.
		/// </summary>
		private void Tick()
		{
			if (IsExiting)
				return;

			Watch.Restart();

			if (WindowService.IsCreated)
				WindowService.Window.DispatchEvents();

			Update(GameTime);
			Draw(GameTime);

			GameTime.ElapsedGameTime = Watch.Elapsed;
			GameTime.TotalGameTime += Watch.Elapsed;
		}

		/// <summary>
		/// Update the game.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		private void Update(GameTime gameTime)
		{
			foreach (var m in Modules)
				m.Update (gameTime);
		}

		/// <summary>
		/// Initialize the game.
		/// </summary>
		private void Initialize()
		{
			Watch = new Stopwatch();

			foreach (var m in Modules)
				m.Initialize();
		}

		/// <summary>
		/// Draw the game.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		private void Draw(GameTime gameTime)
		{
			if (WindowService.IsCreated) 
			{
				WindowService.Window.Clear();

				foreach (var d in Drawables)
					d.Draw(gameTime);

				WindowService.Window.Display();
			}
		}

		/// <summary>
		/// Gaineds the focus.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="eventArgs">Event arguments.</param>
		private void GainedFocus(object sender, EventArgs eventArgs)
		{
			IsActive = true;
		}

		/// <summary>
		/// Losts the focus.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="eventArgs">Event arguments.</param>
		private void LostFocus(object sender, EventArgs eventArgs)
		{
			IsActive = false;
		}

		/// <summary>
		/// Closed.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="eventArgs">Event arguments.</param>
		private void Closed(object sender, EventArgs eventArgs)
		{
			Exit();
		}

		/// <summary>
		/// window creating.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="eventArgs">Event arguments.</param>
		private void RenderWindowCreating(object sender, EventArgs eventArgs)
		{
			IsActive = false;
		}

		/// <summary>
		/// window created.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="eventArgs">Event arguments.</param>
		private void RenderWindowCreated(object sender, EventArgs eventArgs)
		{
			WindowService.Window.GainedFocus += GainedFocus;
			WindowService.Window.LostFocus += LostFocus;
			WindowService.Window.Closed += Closed;
			IsActive = true;
		}
	}
}