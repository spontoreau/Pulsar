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
	/// A game component that is notified when it needs to draw itself. 
	/// </summary>
	public class DrawableGameComponent : GameComponent, IDrawable
	{
		/// <summary>
		/// Indicates whether Draw should be called.
		/// </summary>
		private bool _visible;

		/// <summary>
		/// Indicates the order in which the DrawableGameComponent should be drawed relative to other DrawableGameComponent instances. Lower values are drawed first. 
		/// </summary>
		private int _drawOrder;

		/// <summary>
		/// Indicates whether Draw should be called.
		/// </summary>
		public bool Visible
		{
			get
			{
				return _visible;
			}
			set
			{
			    if (_visible == value) return;
                
			    _visible = value;
			    OnVisibleChanged(EventArgs.Empty);
			}
		}

		/// <summary>
		/// Indicates the order in which the DrawableGameComponent should be drawed relative to other DrawableGameComponent instances. Lower values are drawed first.
		/// </summary>
		public int DrawOrder
		{
			get
			{
				return _drawOrder;
			}
			set
			{
			    if (_drawOrder == value) return;

			    _drawOrder = value;
			    OnUpdateOrderChanged(EventArgs.Empty);
			}
		}

		/// <summary>
		/// Raised when the Visible property changes.
		/// </summary>
		public event EventHandler<EventArgs> VisibleChanged;

		/// <summary>
		/// Raised when the DrawOrder property changes.
		/// </summary>
		public event EventHandler<EventArgs> DrawOrderChanged;

		/// <summary>
		/// Creates a new instance of DrawableGameComponent.
		/// </summary>
		/// <param name="game">Game that the game component should be attached to.</param>
		public DrawableGameComponent(Game game)
			: base(game)
		{

		}

		/// <summary>
		/// Called when the DrawableGameComponent needs to be drawn. Override this method with component-specific drawing code. 
		/// </summary>
		/// <param name="gameTime">Time passed since the last call to Draw.</param>
		public virtual void Draw(GameTime gameTime)
		{

		}

		/// <summary>
		/// Releases the resources used by the DrawableGameComponent class.
		/// </summary>
		/// <param name="isDisposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		protected override void Dispose(bool isDisposing)
		{
			if (isDisposing)
			{
				UnloadContent();
			}
			base.Dispose(isDisposing);
		}

		public override void Initialize()
		{
			base.Initialize();
			LoadContent();
		}

		/// <summary>
		/// Called when graphics resources need to be loaded. Override this method to load any component-specific graphics resources.
		/// </summary>
		protected virtual void LoadContent()
		{

		}

		/// <summary>
		/// Called when graphics resources need to be unloaded. Override this method to unload any component-specific graphics resources. 
		/// </summary>
		protected virtual void UnloadContent()
		{

		}

		/// <summary>
		/// Called when the DrawOrder property changes. Raises the DrawOrderChanged event. 
		/// </summary>
		/// <param name="args">Arguments to the DrawOrderChanged event.</param>
		protected virtual void OnDrawOrderChanged(EventArgs args)
		{
			var tmp = DrawOrderChanged;

			if (tmp != null)
			{
				tmp(this, args);
			}
		}

		/// <summary>
		/// Called when the Visible property changes. Raises the VisibleChanged event. 
		/// </summary>
		/// <param name="args">Arguments to the VisibleChanged event.</param>
		protected virtual void OnVisibleChanged(EventArgs args)
		{
			var tmp = VisibleChanged;

			if (tmp != null)
			{
				tmp(this, args);
			}
		}
	}
}

