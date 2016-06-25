using System;

namespace Splice
{
	/// <summary>
	/// A slice that invokes a callback when played.
	/// </summary>
	public class CallbackSlice : BaseSlice
	{
		private readonly Action action;

		/// <summary>
		/// Initializes a new instance of the <see cref="Splicer.CallbackSlice"/> class.
		/// </summary>
		/// <param name="action">The action to invoke.</param>
		public CallbackSlice (Action action)
		{
			this.action = action;
		}

		/// <inheritdoc />
		protected override void OnPlay ()
		{
			this.action.Invoke ();
			this.State = SliceState.Completed;
		}
	}
}
