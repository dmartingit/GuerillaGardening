using UnityEngine;

public class RangeGorillaBehavior : GorillaBehavior {

	private new Collider collider;
	private Animator animator;
	private float attackTimer;

	protected override void Start () {
		base.Start ();

		this.collider = GetComponentInChildren<Collider> ();
		this.animator = GetComponent<Animator> ();
		this.attackTimer = this.gorilla.attackspeed;
	}

	protected override void Update () {
		base.Update ();

		this.attackTimer -= Time.deltaTime;
		if (this.attackTimer < 0) {
			var centerPos = this.collider.bounds.center;
			var fwd = this.transform.forward;
			RaycastHit hit;
			if (Physics.Raycast (centerPos, fwd, out hit, this.gorilla.bullet.range, 1<<8) && (hit.collider.tag == "Plant")) {
				// Set Attack Animation
				this.animator.speed = 1 / this.gorilla.attackspeed;
				this.animator.SetBool ("Attack", true);

				// Should Attack
				var go = Instantiate (this.gorilla.bullet.model, centerPos, Quaternion.Euler (new Vector3 (0, -90, 0))).gameObject;
				var bulletScript = go.GetComponent (typeof(BulletBehavior)) as BulletBehavior;
				bulletScript.bullet = this.gorilla.bullet;
			} else {
				// Reset Attack Animation
				this.animator.SetBool ("Attack", false);
			}

			// Reset Timer
			this.attackTimer = this.gorilla.attackspeed;
		}
	}
}
