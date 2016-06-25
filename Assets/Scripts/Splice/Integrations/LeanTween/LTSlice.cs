using System;
using Splice;

namespace Splice.Integrations
{
	/// <summary>
	/// A slice which wraps a LeanTween tween.
	/// </summary>
	public class LTSlice : BaseSlice
	{
		private Func<LTDescr> tweenFunc;
		private LTDescr tween;

		/// <summary>
		/// Initializes a new instance of the <see cref="Splicer.LTSlice"/> class.
		/// </summary>
		/// <param name="tweenFunc">A Func which creates a LeanTween tween.</param>
		public LTSlice (Func<LTDescr> tweenFunc)
		{
			this.tweenFunc = tweenFunc;
		}

		/// <inheritdoc />
		protected override void OnUpdate (float deltaTime)
		{
			// Check if the tween has completed.
			if (!LeanTween.isTweening (this.tween.id))
			{
				this.State = SliceState.Completed;
			}
		}

		/// <inheritdoc />
		protected override void OnPlay ()
		{
			// If the tween is already running, stop it.
			if (this.tween != null && LeanTween.isTweening(this.tween.id))
			{
				LeanTween.cancel (this.tween.id);
			}

			// Create the tween.
			this.tween = this.tweenFunc.Invoke ();
		}

		/// <inheritdoc />
		protected override void OnPause ()
		{
			if (this.tween != null)
			{
				this.tween.pause ();
			}
		}

		/// <inheritdoc />
		protected override void OnResume ()
		{
			if (this.tween != null)
			{
				this.tween.resume ();
			}
		}

		/// <inheritdoc />
		protected override void OnStop ()
		{
			if (this.tween != null)
			{
				LeanTween.cancel (this.tween.id);
			}
		}
	}
}

