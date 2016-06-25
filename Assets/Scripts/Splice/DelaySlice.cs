using System;
using UnityEngine;

namespace Splice
{
	/// <summary>
	/// A slice which wraps another slice and delays for a specified amount of time before playing it.
	/// </summary>
	public class DelaySlice : BaseSlice
	{
		private readonly float delayTime;
		private float remainingDelayTime;

		private readonly ISlice slice;

		/// <summary>
		/// Initializes a new instance of the <see cref="Splicer.Delay"/> class which waits
		/// and then plays another slice.
		/// </summary>
		/// <param name="delayTime">How long to wait in miliseconds before playing the provided slice.</param>
		/// <param name="slice">The slice to play after the delay has elapsed.</param>
		public DelaySlice (float delayTime, ISlice slice)
		{
			this.delayTime = delayTime;
			this.slice = slice;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Splicer.DelaySlice"/> class which waits
		/// and then completes.
		/// </summary>
		/// <param name="delayTime">How long to wait in miliseconds before completing.</param>
		public DelaySlice(float delayTime)
			: this(delayTime, new NoOpSice())
		{
		}

		/// <inheritdoc />
		protected override void OnUpdate (float deltaTime)
		{
			// If we haven't started the decorated slice and enough time has elapsed, play it.
			if (this.slice.State == SliceState.NotStarted)
			{
				this.remainingDelayTime -= deltaTime;

				if (remainingDelayTime <= 0)
				{
					this.slice.Play ();
				}

				return;
			}

			this.slice.Update (deltaTime);
			this.State = this.slice.State;
		}

		/// <inheritdoc />
		protected override void OnPlay ()
		{
			this.remainingDelayTime = this.delayTime;
		}

		/// <inheritdoc />
		protected override void OnPause ()
		{
			this.slice.Pause ();
		}

		/// <inheritdoc />
		protected override void OnResume ()
		{
			this.slice.Resume ();
		}

		/// <inheritdoc />
		protected override void OnStop ()
		{
			this.slice.Stop ();
		}
	}
}
