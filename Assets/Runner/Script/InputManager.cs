using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CakewalkIoC.Signal;

public class InputManager : MonoBehaviour {

    public enum SwipeDirection {
        Up,
        Down,
        Left,
        Right
    }

    public static Signal<SwipeDirection> Swipe = new Signal<SwipeDirection>();

    void Update() {

        if(Input.GetKeyDown(KeyCode.UpArrow )) {
            Swipe.Dispatch(SwipeDirection.Up);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)) {
            Swipe.Dispatch(SwipeDirection.Down);
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            Swipe.Dispatch(SwipeDirection.Left);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            Swipe.Dispatch(SwipeDirection.Right);
        }
    }

}
