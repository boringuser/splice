using System;

namespace Splice
{
	/// <summary>
	/// A slice which does nothing and completes instantly when played.
	/// </summary>
	public class NoOpSice : BaseSlice
	{
		/// <inheritdoc />
		protected override void OnPlay ()
		{
			this.State = SliceState.Completed;
		}
	}
}
