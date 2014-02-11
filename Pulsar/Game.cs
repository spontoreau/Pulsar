/* License
 * 
 * The MIT License (MIT)
 *
 * Copyright (c) 2014, Sylvain PONTOREAU (pontoreau.sylvain@gmail.com)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Pulsar.Content;

namespace Pulsar
{
    /// <summary>
	/// Provides basic game logic. 
	/// </summary>
	public class Game : IDisposable
	{
		/// <summary>
		/// The Stopwatch for update the GameTime
		/// </summary>
		protected Stopwatch Watch { get; set; }

		/// <summary>
		/// Object use for lock operation
		/// </summary>
		protected object SyncRoot = new object();  

		/// <summary>
		/// The game loop time
		/// </summary>
		public GameTime GameTime { get; set; }

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
		/// True if the game is currently running.
		/// </summary>
		protected bool IsRunning { get; set; }

		/// <summary>
		/// True if the game is curretly exiting
		/// </summary>
		protected bool IsExiting { get; set; }

		/// <summary>
		/// Indicates whether the game is currently the active application.
		/// </summary>
		public bool IsActive { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating if the game stop when the application is unactive
		/// </summary>
		public bool IsInactiveStopThread { get; set; }

		/// <summary>
		/// IsFixedTimeStep attribute. By Default it's set to true (VSync active).
		/// </summary>
		private bool _isFixedTimeStep;

		/// <summary>
		/// Gets or sets a value indicating whether to use fixed time steps (VSync 60 FPS if true).
		/// </summary>
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
		/// IsMouseVisible attribute. By Default it's set to false.
		/// </summary>
		private bool _isMouseVisible;

		/// <summary>
		/// Gets or sets a value indicating whether the mouse cursor should be visible. 
		/// </summary>
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
		/// Get the content manager.
		/// </summary>
		public ContentManager Content { get; private set; }

		/// <summary>
		/// Gets the collection of GameComponents owned by the game.
		/// </summary>
		public GameComponentCollection Components { get; private set; }

		/// <summary>
		/// Initializes a new instance of this class, which provides basic game logic and a game loop.
		/// </summary>
		public Game()
		{
			Components = new GameComponentCollection();
			Components.ComponentAdded += GameComponentAdded;
			Components.ComponentRemoved += GameComponentRemoved;

			InitWaitingComponents = new Collection<IGameComponent>();
			UpdateableComponents = new Collection<IUpdateable>();
			DrawableComponents = new Collection<IDrawable>();
			Content = new ContentManager();

			GameTime = new GameTime();
		}

		/// <summary>
		/// Call this method to initialize the game, begin running the game loop, and start processing events for the game. 
		/// </summary>
		public void Run()
		{
			if (IsRunning)
				throw new InvalidOperationException("Game is running");

		    IsExiting = false;
		    BeginRun();
		    BeginInitialize();
		    Initialize();
		    EndInitialize();
		    LoadContent();
		    IsRunning = true; 
				               
		    while (IsRunning && !IsExiting)
		    {
		        Tick();
		    }
		    UnloadContent(); 
		    EndRun();
		}


		/// <summary>
		/// Exits the game.
		/// </summary>
		public void Exit()
		{
			IsExiting = true;
		}

		/// <summary>
		/// Allows a Game to attempt to free resources and perform other cleanup operations before garbage collection reclaims the Game. 
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Updates the game's clock and calls Update and Draw.
		/// </summary>
		protected virtual void Tick()
		{
			if (IsExiting)
				return;

			BeginUpdate();
			Update(GameTime);
			EndUpdate();
			BeginDraw();
			Draw(GameTime);
			EndDraw();
		}

		/// <summary>
		/// Called when the game has determined that game logic needs to be processed.
		/// </summary>
		/// <param name="gameTime">Time passed since the last call to Update.</param>
		protected virtual void Update(GameTime gameTime)
		{
			foreach (var updateable in UpdateableComponents)
			{
				if (updateable.Enabled)
					updateable.Update(gameTime);
			}
		}

		/// <summary>
		/// Allows a Game to attempt to free resources and perform other cleanup operations before garbage collection reclaims the Game. 
		/// </summary>
		~Game()
		{
			Dispose(false);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="isDisposing"></param>
		protected virtual void Dispose(bool isDisposing)
		{
			if (isDisposing)
			{
				lock (SyncRoot)
				{
					//Clear the GameComponant collection will raise an event for each component remove.
					Components.Clear();
				}
			}
		}

		/// <summary>
		/// Update the GameTime
		/// </summary>
		protected void UpdateGameTime()
		{
			GameTime.ElapsedGameTime = Watch.Elapsed;
			GameTime.TotalGameTime += Watch.Elapsed;
		}

		/// <summary>
		/// Called after all components are initialized but before the first update in the game loop. 
		/// </summary>
		protected virtual void BeginRun()
		{

		}

		/// <summary>
		/// Called after the game loop has stopped running before exiting.
		/// </summary>
		protected virtual void EndRun()
		{
			Environment.Exit(1);//Allways exit the application
		}

		/// <summary>
		/// Starts the update. This method is followed by calls to Update and EndUpdate. 
		/// </summary>
		protected virtual void BeginUpdate()
		{
			Watch.Restart();

			if (WindowContext.IsCreated)
				WindowContext.Window.DispatchEvents();
		}

		/// <summary>
		/// Ends the update. This method is preceeded by calls to Update and EndUpdate. 
		/// </summary>
		protected virtual void EndUpdate()
		{

		}

		/// <summary>
		/// Starts the initialize of the game. This method is followed by calls to Initialize and EndInitialize. 
		/// </summary>
		protected virtual void BeginInitialize()
		{
			WindowContext.Created += RenderWindowCreated;
			WindowContext.Creating += RenderWindowCreating;
		}

		/// <summary>
		/// End the initialize of the game. This method is preceeded calls to Initialize and BeginInitialize. 
		/// </summary>
		protected virtual void EndInitialize()
		{
			if (!WindowContext.IsCreated)
				WindowContext.Create();
		}

		/// <summary>
		/// Called after the Game and GraphicsDevice are created, but before LoadContent. 
		/// </summary>
		protected virtual void Initialize()
		{
			Watch = new Stopwatch();

			foreach (var component in InitWaitingComponents)
				component.Initialize();

			InitWaitingComponents.Clear();
		}

		/// <summary>
		/// Called when the game determines it is time to draw a frame.
		/// </summary>
		/// <param name="gameTime">Time passed since the last call to Draw.</param>
		protected virtual void Draw(GameTime gameTime)
		{
			foreach (IDrawable drawable in DrawableComponents)
			{
				if (drawable.Visible)
					drawable.Draw(gameTime);
			}
		}

		/// <summary>
		/// Starts the drawing of a frame. This method is followed by calls to Draw and EndDraw. 
		/// </summary>
		protected virtual void BeginDraw()
		{
			if (WindowContext.IsCreated)
				WindowContext.Window.Clear();
		}

		/// <summary>
		/// Ends the drawing of a frame. This method is preceeded by calls to Draw and BeginDraw. 
		/// </summary>
		protected virtual void EndDraw()
		{
			if (WindowContext.IsCreated)
				WindowContext.Window.Display();

			//Update the timer
			UpdateGameTime();
		}

		/// <summary>
		/// Called when graphics resources need to be loaded.
		/// </summary>
		protected virtual void LoadContent()
		{

		}

		/// <summary>
		/// Called when graphics resources need to be unloaded. Override this method to unload any game-specific graphics resources.
		/// </summary>
		protected virtual void UnloadContent()
		{

		}

		/// <summary>
		/// Handle the ComponentAdded event raise by the GameComponentCollection
		/// </summary>
		/// <param name="sender">The GameComponent collection</param>
		/// <param name="eventArgs">Arguments of the ComponentAdded event.</param>
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
		/// Handle the ComponentRemoved event raise by the GameComponentCollection
		/// </summary>
		/// <param name="sender">The GameComponent collection</param>
		/// <param name="eventArgs">Arguments of the ComponentRemoved event.</param>
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
		/// Handle the UpdateOrderChanged raise by a IUpdateable
		/// </summary>
		/// <param name="sender">The IUpdateable</param>
		/// <param name="eventArgs">Arguments of the UpdateOrderChanged event.</param>
		protected void UpdateOrderChanged(object sender, EventArgs eventArgs)
		{
			var updateable = (IUpdateable)sender;
			UpdateableComponents.Remove(updateable);
			InsertUpdateable(updateable);            
		}

		/// <summary>
		/// Insert updateable at the right index in the UpdateableComponents List.
		/// </summary>
		/// <param name="updateable">The updateable component to insert</param>
		protected void InsertUpdateable(IUpdateable updateable)
		{
			//find the follower and insert the updateable at the index of the follower
			var follower = (from u in UpdateableComponents where u.UpdateOrder >= updateable.UpdateOrder select u).FirstOrDefault();
			//if follower is null, updateable is the last in the collection, otherwise take the index of the follower
			var index = (follower == null) ? UpdateableComponents.Count : UpdateableComponents.IndexOf(follower); 
			UpdateableComponents.Insert(index, updateable);
		}

		/// <summary>
		/// Handle the DrawOrderChanged raise by a IDrawable
		/// </summary>
		/// <param name="sender">The IDrawable</param>
		/// <param name="eventArgs">Arguments of the DrawOrderChanged event.</param>
		private void DrawOrderChanged(object sender, EventArgs eventArgs)
		{
			var drawable = (IDrawable)sender;
			DrawableComponents.Remove(drawable);
			InsertDrawable(drawable);
		}

		/// <summary>
		/// Insert drawable at the right index in the DrawableComponents List.
		/// </summary>
		/// <param name="drawable">The drawable component to insert</param>
		private void InsertDrawable(IDrawable drawable)
		{
			//find the follower and insert the drawable at the index of the follower
			var follower = (from u in DrawableComponents where u.DrawOrder >= drawable.DrawOrder select u).FirstOrDefault();
			//if follower is null, drawable is the last in the collection, otherwise take the index of the follower
			var index = (follower == null) ? DrawableComponents.Count : DrawableComponents.IndexOf(follower);
			DrawableComponents.Insert(index, drawable);
		}

		/// <summary>
		/// Handle the GainedFocus event raise by the RenderWindow.
		/// </summary>
		/// <param name="sender">The RenderWindow</param>
		/// <param name="eventArgs">Arguments of the GainedFocus event.</param>
		protected virtual void GainedFocus(object sender, EventArgs eventArgs)
		{
			IsActive = true;
		}

		/// <summary>
		/// Handle the LostFocus event raise by the RenderWindow.
		/// </summary>
		/// <param name="sender">The RenderWindow</param>
		/// <param name="eventArgs">Arguments of the LostFocus event.</param>
		protected virtual void LostFocus(object sender, EventArgs eventArgs)
		{
			IsActive = false;
		}

		/// <summary>
		/// Handle the Closed event raise by the RenderWindow.
		/// </summary>
		/// <param name="sender">The RenderWindow</param>
		/// <param name="eventArgs">Arguments of the LostFocus event.</param>
		protected virtual void Closed(object sender, EventArgs eventArgs)
		{
			Exit();
		}

		/// <summary>
		/// Handle the Creating event raise by the RenderWindowManager.
		/// </summary>
		/// <param name="sender">The RenderWindowManager</param>
		/// <param name="eventArgs">Arguments of the Creating event.</param>
		protected virtual void RenderWindowCreating(object sender, EventArgs eventArgs)
		{
			IsActive = false;
		}

		/// <summary>
		/// Handle the Created event raise by the RenderWindowManager.
		/// </summary>
		/// <param name="sender">The RenderWindowManager</param>
		/// <param name="eventArgs">Arguments of the Created event.</param>
		protected virtual void RenderWindowCreated(object sender, EventArgs eventArgs)
		{
			WindowContext.Window.GainedFocus += GainedFocus;
			WindowContext.Window.LostFocus += LostFocus;
			WindowContext.Window.Closed += Closed;
			IsActive = true;
		}
	}
}

