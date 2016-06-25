using System;

namespace Splice
{
	/// <summary>
	/// A base implementation of <see cref="ISlice"/> that handles state transitions, etc.
	/// </summary>
	public abstract class BaseSlice : ISlice
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Splicer.BaseSlice"/> class.
		/// </summary>
		protected BaseSlice ()
		{
			this.State = SliceState.NotStarted;
		}

		/// <inheritdoc />
		public ISlice Parent { get; set; }

		/// <inheritdoc />
		public SliceState State { get; protected set; }

		/// <inheritdoc />
		public void Update (float deltaTime)
		{
			// Only update when playing.
			if (this.State != SliceState.Playing)
			{
				return;
			}

			this.OnUpdate (deltaTime);
		}

		/// <summary>
		/// Subclasses can override this method with custom update logic.
		/// </summary>
		/// <param name="deltaTime">The amount of time that has elapsed since the last frame, in miliseconds.</param>
		protected virtual void OnUpdate (float deltaTime)
		{
			// Default implementation does nothing.
		}

		/// <inheritdoc />
		public void Play ()
		{
			// Don't play when already playing.
			if (this.State == SliceState.Playing)
			{
				return;
			}

			// A slice's update method should be called by its parent. For slices without parents,
			// we make sure their update methods are called via AddSliceForUpdate.
			if (this.Parent == null)
			{
				SpliceComponent.AddSliceForUpdate (this);
			}

			this.State = SliceState.Playing;
			this.OnPlay ();
		}

		/// <summary>
		/// Subclasses can override this method with custom play logic.
		/// </summary>
		protected virtual void OnPlay()
		{
			// Default implementation does nothing.
		}

		/// <inheritdoc />
		public void Pause ()
		{
			// We can only pause if we're playing.
			if (this.State != SliceState.Playing)
			{
				return;
			}

			this.State = SliceState.Paused;
			this.OnPause ();
		}

		/// <summary>
		/// Subclasses can override this method with custom pause logic.
		/// </summary>
		protected virtual void OnPause ()
		{
			// Default implementation does nothing.
		}

		/// <inheritdoc />
		public void Resume ()
		{
			// We can only resume if we're paused.
			if (this.State != SliceState.Paused)
			{
				return;
			}

			this.State = SliceState.Playing;
			this.OnResume ();
		}

		/// <summary>
		/// Subclasses can override this method with custom resume logic.
		/// </summary>
		protected virtual void OnResume ()
		{
			// Default implementation does nothing.
		}

		/// <inheritdoc />
		public void Stop ()
		{
			// We can only stop if we're playing or paused.
			if (this.State != SliceState.Playing && this.State != SliceState.Paused)
			{
				return;
			}

			this.State = SliceState.Stopped;
			this.OnStop ();
		}

		/// <summary>
		/// Subclasses can override this method with custom stop logic.
		/// </summary>
		protected virtual void OnStop ()
		{
			// Default implementation does nothing.
		}
	}
}
