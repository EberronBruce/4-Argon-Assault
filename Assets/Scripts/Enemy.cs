using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField] GameObject deathFX;
	[SerializeField] Transform parent;
	[SerializeField] int scorePerHit = 12;

	ScoreBoard scoreBoard;

	// Start is called before the first frame update
	void Start() {
		AddBoxCollider();
		scoreBoard = FindObjectOfType<ScoreBoard>();
	}

	private void AddBoxCollider() {
		Collider boxCollider = gameObject.AddComponent<BoxCollider>();
		boxCollider.isTrigger = false;
	}

	private void OnParticleCollision(GameObject other) {
		scoreBoard.ScoreHit(scorePerHit);
		GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
		fx.transform.parent = parent;
		Destroy(gameObject);
	}
}
