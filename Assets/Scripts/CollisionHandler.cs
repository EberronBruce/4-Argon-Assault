﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ok as long as this is the only script that loads scenes

public class CollisionHandler : MonoBehaviour {

	[Tooltip("In Seconds")][SerializeField] float levelLoadDelay = 1f;
	[Tooltip("FX prefab on player")][SerializeField] GameObject deathFX;


	private void OnTriggerEnter(Collider other) {
		StartDeathSequence();
	}

	private void StartDeathSequence() {
		SendMessage("OnPlayerDeath");
		deathFX.SetActive(true);
		Invoke("ReloadScene", levelLoadDelay);
	}

	private void ReloadScene() { //string reference
		SceneManager.LoadScene(1);
	}

}

