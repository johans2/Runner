using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CakewalkIoC.Signal;

public class Runner : MonoBehaviour {

    public static Signal Dead = new Signal();

    public GameParams gameParams;
    
    Rigidbody2D body;

    private float elapsedRollTime = 0f;

	void Start () {
        body = GetComponent<Rigidbody2D>();
        InputManager.Swipe.AddListener(OnSwipe);
	}

    void OnSwipe(InputManager.SwipeDirection direction) {
        switch(direction) {
            case InputManager.SwipeDirection.Up:
                Jump();
                break;
            case InputManager.SwipeDirection.Down:
                StartCoroutine(Roll());
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

    void Update() {


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle")) Die();
    }

    void Attack() {

    }

    void Block() {

    }

    IEnumerator Roll() {
        float startYScale = transform.localScale.y;
        float startGravityScale = body.gravityScale;
        body.gravityScale = 50
            ;

        while (elapsedRollTime < gameParams.RollDuration)
        {
            elapsedRollTime += Time.deltaTime;
            float yScaleFactor = gameParams.RollCurve.Evaluate(elapsedRollTime / gameParams.RollDuration);
            transform.localScale = new Vector3(transform.localScale.x, startYScale * yScaleFactor, transform.localScale.z);
            yield return null;
        }

        transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        body.gravityScale = startGravityScale;
        elapsedRollTime = 0;
    }
    
    void Jump() {
        Debug.Log("jump!");
        body.AddForce(gameParams.jumpForce, ForceMode2D.Impulse);
    }

    void Die() {
        Dead.Dispatch();
    }
}
