using System;
using System.Collections.Generic;

namespace Splice
{
	/// <summary>
	/// A slice which wraps a set of slices and plays them one at a time, in order.
	/// </summary>
	public class SequentialSlice : BaseSlice
	{
		private readonly List<ISlice> slices = new List<ISlice>();

		private int activeSliceIndex = 0;

		/// <summary>
		/// Add a slice onto the end of the set of slices to execute in order.
		/// </summary>
		/// <param name="slice">The slice to append.</param>
		public SequentialSlice Append(ISlice slice)
		{
			slice.Parent = this;
			this.slices.Add (slice);
			return this;
		}

		/// <inheritdoc />
		protected override void OnUpdate (float deltaTime)
		{
			if (this.slices.Count == 0)
			{
				this.State = SliceState.Completed;
				return;
			}

			ISlice activeSlice = this.slices [this.activeSliceIndex];
			activeSlice.Update (deltaTime);

			if (activeSlice.State == SliceState.Completed)
			{
				this.activeSliceIndex++;

				if (this.slices.Count == this.activeSliceIndex)
				{
					this.State = SliceState.Completed;
					return;
				}

				this.slices [this.activeSliceIndex].Play ();
			}
		}

		/// <inheritdoc />
		protected override void OnPlay ()
		{
			if (this.slices.Count == 0)
			{
				this.State = SliceState.Completed;
				return;
			}

			this.activeSliceIndex = 0;
			this.slices [this.activeSliceIndex].Play ();
		}

		/// <inheritdoc />
		protected override void OnPause ()
		{
			this.slices [this.activeSliceIndex].Pause ();
		}

		/// <inheritdoc />
		protected override void OnResume ()
		{
			this.slices [this.activeSliceIndex].Resume ();
		}

		/// <inheritdoc />
		protected override void OnStop ()
		{
			this.slices [this.activeSliceIndex].Stop ();
		}
	}
}
