﻿using UnityEngine;

public class RangePlantBehavior : PlantBehavior {
	
	private Collider collider;
	private Animator animator;
	private float attackTimer;

	protected override void Start () {
		base.Start ();

		this.collider = GetComponentInChildren<Collider> ();
		this.animator = GetComponent<Animator> ();
		this.attackTimer = this.plant.attackspeed;
	}

	protected override void Update () {
		base.Update ();

		this.attackTimer -= Time.deltaTime;
		if (this.attackTimer < 0) {
			var centerPos = this.collider.bounds.center;
			var fwd = this.transform.forward;
			RaycastHit hit;
			if (Physics.Raycast (centerPos, fwd, out hit, this.plant.bullet.range, 1<<9) && (hit.collider.tag == "Gorilla")) {
				// Set Attack Animation
				this.animator.speed = 1 / this.plant.attackspeed;
				this.animator.SetBool ("Attack", true);

				// Should Attack
				var go = Instantiate (this.plant.bullet.model, centerPos, Quaternion.Euler (new Vector3 (0, 90, 0))).gameObject;
				var bulletScript = go.GetComponent (typeof(BulletBehavior)) as BulletBehavior;
				bulletScript.bullet = this.plant.bullet;
			} else {
				// Reset Attack Animation
				this.animator.SetBool ("Attack", false);
			}

			// Reset Timer
			this.attackTimer = this.plant.attackspeed;
		}
	}
}
