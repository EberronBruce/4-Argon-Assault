using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;


public class PlayerController : MonoBehaviour {

	// todo work-out why sometimes slow play onf first play

	[Header("General")]
	[Tooltip("In ms^-1")][SerializeField] float controlSpeed = 20f;
	[Tooltip("In m")] [SerializeField] float xRange = 5f;
	[Tooltip("In m")] [SerializeField] float yRange= 3f;
	[SerializeField] GameObject[] guns;

	[Header("Screen-position Based")]
	[SerializeField] float positionPitchFactor = -5f;
	[SerializeField] float positionYawFactor = 5f;

	[Header("Control-throw Based")]
	[SerializeField] float controlPitchFactor = -20f;
	[SerializeField] float controllRollFactor = -20f;

	float xThrow, yThrow;
	bool isControlEnabled = true;

	//  Update is called once per frame
	void Update() {
		if(isControlEnabled) {
			ProcessTranslation();
			ProcessRotation();
			ProcessFiring();
		}
	}

	public void OnPlayerDeath() { // called by string reference
		isControlEnabled = false;
	}

	private void ProcessRotation() {
		float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
		float pitchDueToControlThrow = yThrow * controlPitchFactor;
		float pitch = pitchDueToPosition + pitchDueToControlThrow;

		float yaw = transform.localPosition.x * positionYawFactor;

		float roll = xThrow * controllRollFactor;
		transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
	}

	private void ProcessTranslation() {
		xThrow = Input.GetAxis("Horizontal");
		yThrow = Input.GetAxis("Vertical");  
		float xOffset = controlSpeed * xThrow * Time.deltaTime;
		float yOffset = controlSpeed * yThrow * Time.deltaTime;

		float rawXPos = transform.localPosition.x + xOffset;
		float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

		float rawYPos = transform.localPosition.y + yOffset;
		float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

		transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
	}

	private void ProcessFiring() {
		if(Input.GetButton("Fire")) {
			SetGunsActive(true);
		} else {
			SetGunsActive(false);
		}
	}

	private void SetGunsActive(bool isActive) {
		foreach (GameObject gun in guns) {
			var particleEmission = gun.GetComponent<ParticleSystem>().emission;
			particleEmission.enabled = isActive;
		}
	}



}
