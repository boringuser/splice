using System;
using System.Collections.Generic;

namespace Splice
{
	/// <summary>
	/// A slice which wraps a set of slices and plays them simultaneously, all at once.
	/// </summary>
	public class SimultaneousSlice : BaseSlice
	{
		private readonly List<ISlice> slices = new List<ISlice>();

		/// <summary>
		/// Add a slice to the set of slices to play simultaneously.
		/// </summary>
		/// <param name="slice">The slice to append.</param>
		public SimultaneousSlice Append(ISlice slice)
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

			SliceState newState = SliceState.Completed;

			foreach (ISlice slice in this.slices)
			{
				slice.Update (deltaTime);

				if (slice.State == SliceState.Playing)
				{
					newState = SliceState.Playing;
				}
			}

			this.State = newState;
		}

		/// <inheritdoc />
		protected override void OnPlay ()
		{
			if (this.slices.Count == 0)
			{
				this.State = SliceState.Completed;
				return;
			}

			foreach (ISlice slice in this.slices)
			{
				slice.Play ();
			}
		}

		/// <inheritdoc />
		protected override void OnPause ()
		{
			foreach (ISlice slice in this.slices)
			{
				slice.Pause ();
			}
		}

		/// <inheritdoc />
		protected override void OnResume ()
		{
			foreach (ISlice slice in this.slices)
			{
				slice.Resume ();
			}
		}

		/// <inheritdoc />
		protected override void OnStop ()
		{
			foreach (ISlice slice in this.slices)
			{
				slice.Stop ();
			}
		}
	}
}
