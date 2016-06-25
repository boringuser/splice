using System;

namespace Splice
{
	/// <summary>
	/// A slice which wraps another slice and plays it a specified number of times, sequentially.
	/// </summary>
	public class LoopSlice : BaseSlice
	{
		private readonly uint loopCount;
		private uint remainingLoops;

		private readonly ISlice slice;

		/// <summary>
		/// Initializes a new instance of the <see cref="Splicer.Loop"/> class.
		/// </summary>
		/// <param name="loopCount">How many times to loop the provided slice.</param>
		/// <param name="slice">The slice to loop.</param>
		public LoopSlice (uint loopCount, ISlice slice)
		{
			this.loopCount = loopCount;
			this.slice = slice;
		}

		/// <inheritdoc />
		protected override void OnUpdate (float deltaTime)
		{
			// If the wrapped slice is completed, go to the next loop iteration / complete.
			if (this.slice.State == SliceState.Completed)
			{
				this.remainingLoops--;

				if (this.remainingLoops == 0)
				{
					this.State = SliceState.Completed;
				}
				else
				{
					this.slice.Play ();
				}

				return;
			}

			this.slice.Update (deltaTime);
		}

		/// <inheritdoc />
		protected override void OnPlay ()
		{
			this.remainingLoops = this.loopCount;
			this.slice.Play ();
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
