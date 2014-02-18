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

namespace Pulsar
{
	/// <summary>
	/// Base class for all Pulsar game components. 
	/// </summary>
	[Obsolete]
	public class GameComponent : IGameComponent, IUpdateable
	{
		/// <summary>
		/// Object use for lock operation
		/// </summary>
		private readonly object _syncRoot = new object();

		/// <summary>
		/// Indicates whether GameComponent.Update should be called when Game.Update is called.
		/// </summary>
		private bool _enabled;

		/// <summary>
		/// Indicates the order in which the GameComponent should be updated relative to other GameComponent instances. Lower values are updated first. 
		/// </summary>
		private int _updateOrder;

		/// <summary>
		/// Gets the Game associated with this GameComponent. 
		/// </summary>
		public Game Game { get; private set; }

		/// <summary>
		/// Indicates whether GameComponent.Update should be called when Game.Update is called.
		/// </summary>
		public bool Enabled
		{
			get 
			{
				return _enabled;
			}
			set
			{
			    if (_enabled == value) return;

			    _enabled = value;
			    OnEnabledChanged(EventArgs.Empty);
			}
		}

		/// <summary>
		/// Indicates the order in which the GameComponent should be updated relative to other GameComponent instances. Lower values are updated first. 
		/// </summary>
		public int UpdateOrder
		{
			get 
			{
				return _updateOrder;
			}
			set
			{
			    if (_updateOrder == value) return;

			    _updateOrder = value;
			    OnUpdateOrderChanged(EventArgs.Empty);
			}
		}

		/// <summary>
		/// Raised when the Enabled property changes.
		/// </summary>
		public event EventHandler<EventArgs> EnabledChanged;

		/// <summary>
		/// Raised when the UpdateOrder property changes.
		/// </summary>
		public event EventHandler<EventArgs> UpdateOrderChanged;

		/// <summary>
		/// Creates a new instance of GameComponent.
		/// </summary>
		/// <param name="game">Game that the game component should be attached to.</param>
		public GameComponent(Game game)
		{
			Game = game;
			_enabled = true;
		}

		/// <summary>
		/// Called when the GameComponent needs to be initialized.
		/// </summary>
		public virtual void Initialize()
		{

		}

		/// <summary>
		/// Called when the GameComponent needs to be updated. Override this method with component-specific update code.
		/// </summary>
		/// <param name="gameTime">Time elapsed since the last call to Update.</param>
		public virtual void Update(GameTime gameTime)
		{

		}

		/// <summary>
		/// Immediately releases the unmanaged resources used by this object. 
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Releases the unmanaged resources used by the GameComponent and optionally releases the managed resources. 
		/// </summary>
		/// <param name="isDisposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected virtual void Dispose(bool isDisposing)
		{
			lock (_syncRoot)
			{
				if (isDisposing)
				{
					if (Game != null && Game.Components != null && Game.Components.Count > 0)
					{
						Game.Components.Remove(this);
					}
				}
			}
		}

		/// <summary>
		/// Allows a GameComponent to attempt to free resources and perform other cleanup operations before garbage collection reclaims the GameComponent. 
		/// </summary>
		~GameComponent()
		{
			Dispose(false);
		}

		/// <summary>
		/// Called when the UpdateOrder property changes. Raises the UpdateOrderChanged event.
		/// </summary>
		/// <param name="args">Arguments to the UpdateOrderChanged event.</param>
		protected virtual void OnUpdateOrderChanged(EventArgs args)
		{
			var tmp = UpdateOrderChanged;

			if (tmp != null)
				tmp(this, args);
		}

		/// <summary>
		/// Called when the Enabled property changes. Raises the EnabledChanged event.
		/// </summary>
		/// <param name="args">Arguments to the EnabledChanged event.</param>
		protected virtual void OnEnabledChanged(EventArgs args)
		{
			var tmp = EnabledChanged;

			if(tmp != null)
				tmp(this, args);
		}
	}
}

