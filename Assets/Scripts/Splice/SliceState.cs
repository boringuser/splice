using System;

namespace Splice
{
	/// <summary>
	/// Indicates the current state of a <see cref="ISlice"/>
	/// </summary>
	public enum SliceState {
		
		/// <summary>
		/// The slice has not yet started.
		/// </summary>
		NotStarted,

		/// <summary>
		/// The slice is currently playing.
		/// </summary>
		Playing,

		/// <summary>
		/// The slice is paused.
		/// </summary>
		Paused,

		/// <summary>
		/// The slice is stopped.
		/// </summary>
		Stopped,

		/// <summary>
		/// The slice has finished.
		/// </summary>
		Completed
	};
}
