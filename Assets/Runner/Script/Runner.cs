using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CakewalkIoC.Signal;

public class Runner : MonoBehaviour {

    public static Signal Dead = new Signal();

    public GameParams gameParams;

    Rigidbody2D body;

	void Start () {
        body = GetComponent<Rigidbody2D>();
        InputManager.Swipe.AddListener(OnSwipe);
	}

    private void OnSwipe(InputManager.SwipeDirection direction) {
        switch(direction) {
            case InputManager.SwipeDirection.Up:
                Jump();
                break;
            case InputManager.SwipeDirection.Down:
                Roll();
                break;
            case InputManager.SwipeDirection.Left:
                Block();
                break;
            case InputManager.SwipeDirection.Right:
                Attack();
                break;
            default:
                break;
        }


    }

    private void Attack() {

    }

    private void Block() {

    }

    private void Roll() {

    }

    private void Die() {
        Dead.Dispatch();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle")) Die();
    }

    private void Jump() {
        Debug.Log("jump!");
        body.AddForce(gameParams.jumpForce, ForceMode2D.Impulse);
    }
}
