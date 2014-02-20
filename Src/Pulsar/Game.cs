using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Pulsar.Services.Implements.Content;
using Pulsar.Services.Implements.Graphics;

namespace Pulsar
{
	/// <summary>
	/// Game.
	/// </summary>
	public class Game
	{
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
		/// Get or define the collection of IGameComponent which need to be initialize.
		/// </summary>
		protected Collection<IGameComponent> InitWaitingComponents { get; set; }

		/// <summary>
		/// Get or define the collection of IUpdateable which need to be update.
		/// </summary>
		protected Collection<IUpdateable> UpdateableComponents { get; set; }

		/// <summary>
		/// Get or define the collection of IDrawable which need to be draw.
		/// </summary>
		protected Collection<IDrawable> DrawableComponents { get; set; }

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
			    if (!WindowContext.IsCreated) return;

			    _isFixedTimeStep = value;
			    WindowContext.Window.SetVerticalSyncEnabled(false);
			    WindowContext.Window.SetFramerateLimit((value) ? 120U : 0U);//uint force to multiple by 2 to obtain refresh at 60fps
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
			    if (!WindowContext.IsCreated) return;

			    _isMouseVisible = value;
			    WindowContext.Window.SetMouseCursorVisible(value);
			}
		}

		/// <summary>
		/// Gets the content manager.
		/// </summary>
		/// <value>The content manager.</value>
		//TODO no need to have the property here, ContentManager must have to be a service (next update to come)
		public ContentService Content { get; private set; }

		//TODO no need to have the property here, SpriteBatcher must have to be a service (next update to come)
		public SpriteBatchService SpriteBatcher { get; private set;}

		/// <summary>
		/// Gets the components.
		/// </summary>
		/// <value>The components.</value>
		//TODO, change components to module and limit the use of heavy game component (next update to come)
		public GameComponentCollection Components { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.Game"/> class.
		/// </summary>
		public Game()
		{
			Components = new GameComponentCollection();
			Components.ComponentAdded += GameComponentAdded;
			Components.ComponentRemoved += GameComponentRemoved;

			InitWaitingComponents = new Collection<IGameComponent>();
			UpdateableComponents = new Collection<IUpdateable>();
			DrawableComponents = new Collection<IDrawable>();
			Content = new ContentService();

			GameTime = new GameTime();
		}

		/// <summary>
		/// Run this instance.
		/// </summary>
		public void Run()
		{
			if (IsRunning)
				throw new InvalidOperationException("Game is running");

		    IsExiting = false;
			WindowContext.Created += RenderWindowCreated;
			WindowContext.Creating += RenderWindowCreating;

		    Initialize();

			if (!WindowContext.IsCreated)
				WindowContext.Create();

		    LoadContent();
		    IsRunning = true; 
				               
		    while (IsRunning && !IsExiting)
		    {
		        Tick();
		    }

		    UnloadContent();
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
		protected virtual void Tick()
		{
			if (IsExiting)
				return;

			Watch.Restart();

			if (WindowContext.IsCreated)
				WindowContext.Window.DispatchEvents();

			Update(GameTime);
			Draw(GameTime);

			GameTime.ElapsedGameTime = Watch.Elapsed;
			GameTime.TotalGameTime += Watch.Elapsed;
		}

		/// <summary>
		/// Update the game.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		protected virtual void Update(GameTime gameTime)
		{
			foreach (var updateable in UpdateableComponents)
			{
				if (updateable.Enabled)
					updateable.Update(gameTime);
			}
		}

		/// <summary>
		/// Initialize the game.
		/// </summary>
		protected virtual void Initialize()
		{
			Watch = new Stopwatch();

			foreach (var component in InitWaitingComponents)
				component.Initialize();

			InitWaitingComponents.Clear();
		}

		/// <summary>
		/// Draw the game.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		protected virtual void Draw(GameTime gameTime)
		{
			if (WindowContext.IsCreated) 
			{
				WindowContext.Window.Clear ();
				
				foreach (IDrawable drawable in DrawableComponents) {
					if (drawable.Visible)
						drawable.Draw (gameTime);
				}

				WindowContext.Window.Display ();
			}

		}

		/// <summary>
		/// Loads content.
		/// </summary>
		protected virtual void LoadContent()
		{

		}

		/// <summary>
		/// Unloads content.
		/// </summary>
		protected virtual void UnloadContent()
		{

		}

		/// <summary>
		/// Game component added.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="eventArgs">Event arguments.</param>
		[Obsolete]
		protected virtual void GameComponentAdded(object sender, GameComponentCollectionEventArgs eventArgs)
		{
			if (IsRunning)
				eventArgs.Component.Initialize();
			else //if the game is not running stack in the initialize waiting list
				InitWaitingComponents.Add(eventArgs.Component);

		    var updateable = eventArgs.Component as IUpdateable;
            if (updateable != null)
			{
                InsertUpdateable(updateable);
                updateable.UpdateOrderChanged += UpdateOrderChanged;
			}

		    var drawable = eventArgs.Component as IDrawable;
		    if (drawable == null) return;

		    InsertDrawable(drawable);
		    drawable.DrawOrderChanged += DrawOrderChanged;
		}

		/// <summary>
		/// Games component removed.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="eventArgs">Event arguments.</param>
		[Obsolete]
		protected virtual void GameComponentRemoved(object sender, GameComponentCollectionEventArgs eventArgs)
		{
			var component = eventArgs.Component;
			if (!IsRunning) //if the game is not running, initialize and loadcontent is'nt make, so remove the component from the InitWaitingGameComponent collection
				InitWaitingComponents.Remove(component);

            var updateable = component as IUpdateable;
            if (updateable != null)
			{
				UpdateableComponents.Remove(updateable);
				updateable.UpdateOrderChanged -= UpdateOrderChanged;                
			}

            var drawable = eventArgs.Component as IDrawable;
		    if (drawable == null) return;

		    DrawableComponents.Remove(drawable);
		    drawable.DrawOrderChanged -= DrawOrderChanged;
		}

		/// <summary>
		/// Updates order changed.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="eventArgs">Event arguments.</param>
		[Obsolete]
		protected void UpdateOrderChanged(object sender, EventArgs eventArgs)
		{
			var updateable = (IUpdateable)sender;
			UpdateableComponents.Remove(updateable);
			InsertUpdateable(updateable);            
		}

		/// <summary>
		/// Inserts the updateable.
		/// </summary>
		/// <param name="updateable">Updateable.</param>
		[Obsolete]
		protected void InsertUpdateable(IUpdateable updateable)
		{
			//find the follower and insert the updateable at the index of the follower
			var follower = (from u in UpdateableComponents where u.UpdateOrder >= updateable.UpdateOrder select u).FirstOrDefault();
			//if follower is null, updateable is the last in the collection, otherwise take the index of the follower
			var index = (follower == null) ? UpdateableComponents.Count : UpdateableComponents.IndexOf(follower); 
			UpdateableComponents.Insert(index, updateable);
		}

		/// <summary>
		/// Draws order changed.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="eventArgs">Event arguments.</param>
		[Obsolete]
		private void DrawOrderChanged(object sender, EventArgs eventArgs)
		{
			var drawable = (IDrawable)sender;
			DrawableComponents.Remove(drawable);
			InsertDrawable(drawable);
		}

		/// <summary>
		/// Inserts the drawable.
		/// </summary>
		/// <param name="drawable">Drawable.</param>
		[Obsolete]
		private void InsertDrawable(IDrawable drawable)
		{
			//find the follower and insert the drawable at the index of the follower
			var follower = (from u in DrawableComponents where u.DrawOrder >= drawable.DrawOrder select u).FirstOrDefault();
			//if follower is null, drawable is the last in the collection, otherwise take the index of the follower
			var index = (follower == null) ? DrawableComponents.Count : DrawableComponents.IndexOf(follower);
			DrawableComponents.Insert(index, drawable);
		}

		/// <summary>
		/// Gaineds the focus.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="eventArgs">Event arguments.</param>
		protected virtual void GainedFocus(object sender, EventArgs eventArgs)
		{
			IsActive = true;
		}

		/// <summary>
		/// Losts the focus.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="eventArgs">Event arguments.</param>
		protected virtual void LostFocus(object sender, EventArgs eventArgs)
		{
			IsActive = false;
		}

		/// <summary>
		/// Closed.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="eventArgs">Event arguments.</param>
		protected virtual void Closed(object sender, EventArgs eventArgs)
		{
			Exit();
		}

		/// <summary>
		/// window creating.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="eventArgs">Event arguments.</param>
		protected virtual void RenderWindowCreating(object sender, EventArgs eventArgs)
		{
			IsActive = false;
		}

		/// <summary>
		/// window created.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="eventArgs">Event arguments.</param>
		protected virtual void RenderWindowCreated(object sender, EventArgs eventArgs)
		{
			WindowContext.Window.GainedFocus += GainedFocus;
			WindowContext.Window.LostFocus += LostFocus;
			WindowContext.Window.Closed += Closed;
			IsActive = true;
		}
	}
}

