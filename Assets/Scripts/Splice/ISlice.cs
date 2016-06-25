using System;
using System.Collections.Generic;

namespace Splice
{

	/// <summary>
	/// A slice is an action, task or unit of work that can be composed with other slices.
	/// </summary>
	public interface ISlice {

		/// <summary>
		/// The parent of this slice.
		/// </summary>
		ISlice Parent { get; set; }

		/// <summary>
		/// Called once per frame.
		/// </summary>
		/// <param name="deltaTime">The amount of time that has elapsed since the last frame, in miliseconds.</param>
		void Update(float deltaTime);

		/// <summary>
		/// Plays the slice.
		/// </summary>
		void Play ();

		/// <summary>
		/// Pauses a playing slice.
		/// </summary>
		void Pause ();

		/// <summary>
		/// Resumes a paused slice.
		/// </summary>
		void Resume ();

		/// <summary>
		/// Stops a playing or paused slice.
		/// </summary>
		void Stop ();

		/// <summary>
		/// Returns the current state of the slice.
		/// </summary>
		/// <value>The state.</value>
		SliceState State { get; }
	}
}
