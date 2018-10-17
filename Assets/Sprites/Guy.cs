using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : MonoBehaviour {

	private float moveForce = 5f;
	private Rigidbody2D rb = null;
	private Animator animator = null;
	private SpriteRenderer spriteRenderer = null;

	void Start () {
		Input.simulateMouseWithTouches = true;
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Update () {
		// Handle native touch events
		foreach (Touch touch in Input.touches) {
			HandleTouch (touch.fingerId, Camera.main.ScreenToWorldPoint (touch.position), touch.phase);
		}

		// Simulate touch events from mouse events
		if (Input.touchCount == 0) {
			if (Input.GetMouseButtonDown (0)) {
				HandleTouch (10, Camera.main.ScreenToWorldPoint (Input.mousePosition), TouchPhase.Began);
			}
			if (Input.GetMouseButton (0)) {
				HandleTouch (10, Camera.main.ScreenToWorldPoint (Input.mousePosition), TouchPhase.Moved);
			}
			if (Input.GetMouseButtonUp (0)) {
				HandleTouch (10, Camera.main.ScreenToWorldPoint (Input.mousePosition), TouchPhase.Ended);
			}
		}

		animator.SetFloat ("HorizontalSpeed", rb.velocity.magnitude);

	}

	private void HandleTouch (int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase) {
		switch (touchPhase) {
			case TouchPhase.Began:
				// TODO

				HandleMove (touchPosition);
				break;
			case TouchPhase.Moved:
				// TODO

				HandleMove (touchPosition);
				break;
			case TouchPhase.Ended:
				// TODO
				HandleStop ();
				break;
		}
	}

	private void HandleMove (Vector3 touchPosition) {
		bool toLeft = this.transform.position.x >= touchPosition.x;
		if (toLeft) {
			spriteRenderer.flipX = false;
			rb.velocity = Vector2.left * moveForce;
		} else {
			spriteRenderer.flipX = true;
			rb.velocity = Vector2.right * moveForce;
		}

	}

	private void HandleStop () {
		rb.velocity = Vector2.zero;
	}
}
