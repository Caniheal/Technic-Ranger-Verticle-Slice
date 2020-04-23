using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
	// Triggers
	public Renderer leftTriggerSphere;
	public Renderer rightTriggerSphere;

	// Bumpers
	public Renderer leftBumperBox;
	public Renderer rightBumperBox;

	// Buttons
	public Renderer aButtonSphere;
	public Renderer bButtonSphere;
	public Renderer xButtonSphere;
	public Renderer yButtonSphere;

	// DPad
	public Renderer dpadLeft;
	public Renderer dpadUp;
	public Renderer dpadRight;
	public Renderer dpadDown;

	// Start and Back
	public Renderer startBox;
	public Renderer backBox;

	// Joysticks
	public Transform cubeLS;
	public Transform cubeRS;
	float movementSpeed = 2.0f;

	void Update(){
		float rTriggerFloat = Input.GetAxis("Right Trigger");
		float lTriggerFloat = Input.GetAxis("Left Trigger");
		bool leftBumper = Input.GetButton("Left Bumper");
		bool rightBumper = Input.GetButton("Right Bumper");
		bool backButton = Input.GetButton("Back");
		bool startButton = Input.GetButton("Start");
		bool aButton = Input.GetButton("A Button");
		bool bButton = Input.GetButton("B Button");
		bool xButton = Input.GetButton("X Button");
		bool yButton = Input.GetButton("Y Button");
		float dpadHorizontal = Input.GetAxis("Dpad Horizontal") * movementSpeed;
		float dpadVertical = Input.GetAxis("Dpad Vertical") * movementSpeed;
		float moveHL = Input.GetAxis("Horizontal") * movementSpeed;
		float moveVL = Input.GetAxis("Vertical") * movementSpeed;
		float moveHR = Input.GetAxis("Mouse X") * movementSpeed;
		float moveVR = Input.GetAxis("Mouse Y") * movementSpeed;

		// Movement in per second measurements
		moveHL *= Time.deltaTime;
		moveVL *= Time.deltaTime;
		moveHR *= Time.deltaTime;
		moveVR *= Time.deltaTime;
	}
}
