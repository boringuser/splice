using System;
using UnityEngine;
using System.Collections.Generic;

namespace Splice
{
	/// <summary>
	/// Maintains a list of slices and calls their update methods each frame.
	/// </summary>
	public class SpliceComponent : MonoBehaviour
	{
		public int sliceCount;

		private readonly List<ISlice> slices = new List<ISlice> ();

		private static volatile object lockObject = new object();

		private static SpliceComponent instance;

		/// <summary>
		/// Gets the singleton instance of this class.
		/// Adds a game object to the scene with the instance as a component.
		/// </summary>
		private static SpliceComponent GetInstance ()
		{
			if (SpliceComponent.instance != null)
			{
				return SpliceComponent.instance;
			}

			lock (SpliceComponent.lockObject)
			{
				if (SpliceComponent.instance == null)
				{
					GameObject gameObject = new GameObject ();
					gameObject.name = "~Splice";
					gameObject.AddComponent (typeof(SpliceComponent));

					SpliceComponent.instance = gameObject.GetComponent<SpliceComponent> ();
				}

				return SpliceComponent.instance;
			}
		}

		/// <summary>
		/// Add a slice to the set of slices which will be updated.
		/// </summary>
		/// <param name="slice">The slice to add.</param>
		public static void AddSliceForUpdate(ISlice slice)
		{
			SpliceComponent instance = SpliceComponent.GetInstance ();

			if (instance.slices.Contains (slice))
			{
				return;
			}

			instance.slices.Add (slice);
		}

		void Update ()
		{
			this.slices.ForEach (slice => slice.Update (Time.deltaTime));
			this.slices.RemoveAll (slice => slice.State == SliceState.Completed || slice.State == SliceState.Stopped);
			this.sliceCount = this.slices.Count;
		}
	}
}
