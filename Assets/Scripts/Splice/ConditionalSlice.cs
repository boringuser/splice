using System;
using UnityEngine;

namespace Splice
{
	/// <summary>
	/// A slice which wraps another slice and only plays it once a specified condition is met.
	/// </summary>
	public class ConditionalSlice : BaseSlice
	{
		private readonly Func<bool> condition;
		private bool playedSlice;

		private readonly ISlice slice;

		/// <summary>
		/// Initializes a new instance of the <see cref="Splicer.ConditionalSlice"/> class.
		/// </summary>
		/// <param name="condition">The condition which must be met before the provided slice is played.</param>
		/// <param name="slice">The slice to play once the condition is met.</param>
		public ConditionalSlice (Func<bool> condition, ISlice slice)
		{
			this.condition = condition;
			this.slice = slice;
		}

		/// <inheritdoc />
		protected override void OnUpdate (float deltaTime)
		{
			// If we haven't played the decorated slice and the condition is met, play it.
			if (this.slice.State == SliceState.NotStarted && this.condition.Invoke ())
			{
				this.playedSlice = true;
				this.slice.Play ();
				return;
			}

			if (!this.playedSlice)
			{
				return;
			}

			this.slice.Update (deltaTime);
			this.State = this.slice.State;
		}

		/// <inheritdoc />
		protected override void OnPlay ()
		{
			this.playedSlice = false;
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
