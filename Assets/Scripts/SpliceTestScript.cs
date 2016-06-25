using UnityEngine;
using Splice;
using Splice.Integrations;

public class SpliceTestScript : MonoBehaviour {

	void Start () {
		Vector3 oldPos = this.transform.position;
		Vector3 newPos = this.transform.position + new Vector3 (5.0f, 0.0f, 0.0f);

		Vector3 oldScale = this.transform.localScale;
		Vector3 newScale = this.transform.localScale * 2.0f;

		new SequentialSlice()
			.Append (new CallbackSlice(() => Debug.Log("Starting!")))
			.Append (
				new SimultaneousSlice()
					.Append (
						new LoopSlice(
							2,
							new SequentialSlice()
								.Append (new LTSlice (() => LeanTween.scale (this.gameObject, newScale, 2.0f)))
								.Append (new LTSlice (() => LeanTween.scale (this.gameObject, oldScale, 2.0f)))
								.Append (new CallbackSlice(() => Debug.Log("Done with one loop")))
						)
					)
					.Append (
						new ConditionalSlice(
							() => this.gameObject.transform.localScale.x > 1.5,
							new SequentialSlice()
								.Append (new CallbackSlice(() => Debug.Log("1.5x scale has been reached")))
								.Append (new LTSlice (() => LeanTween.move (this.gameObject, newPos, 2.0f)))
								.Append (new DelaySlice(2.0f))
								.Append (new LTSlice (() => LeanTween.move (this.gameObject, oldPos, 2.0f)))
								.Append (new CallbackSlice(() => Debug.Log("Done moving")))
						)
					)					
			)
			.Append(new CallbackSlice(() => Debug.Log("Complete!")))
		.Play ();
	}
}
