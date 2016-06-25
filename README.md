# splice
A library for splicing together sequences of tweens, callbacks, etc. in Unity 3D.

Splice comes with an integration for LeanTween but could be extended to use any other tweening library, or perform tasks other than tweening.

Here's a simple example. It scales a game object up and back again, twice. At the same time, it waits for the scale to reach 1.5x and then moves the game object to a new position, waits for 2 seconds, and then moves it back.

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

This software is extremely alpha, but I'm releasing it in the hopes that others find it useful. Please let me know if you find any bugs or have any suggestions for improvement.
