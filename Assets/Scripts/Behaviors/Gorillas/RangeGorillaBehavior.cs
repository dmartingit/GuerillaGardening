using UnityEngine;

public class RangeGorillaBehavior : GorillaBehavior {

	private new Collider collider;
	private Animator animator;
	private GameObject hittedGameObject;
	private float attackTimer;

	protected override void Start () {
		base.Start ();

		this.collider = GetComponentInChildren<Collider> ();
		this.animator = GetComponent<Animator> ();
		this.attackTimer = this.gorilla.attackspeed;
	}

	protected override void Update () {
		base.Update ();

		// Get Objects infront
		var centerPos = this.collider.bounds.center;
		var fwd = this.transform.forward;
		RaycastHit hit;
		if (Physics.Raycast (centerPos, fwd, out hit, this.gorilla.bullet.range, 1<<8) && (hit.collider.tag == "Plant")) {
			this.hittedGameObject = hit.collider.gameObject;
		}

		if (this.hittedGameObject == null) {
			// Movement
			if (this.gorilla.movementspeed != 0) {
				this.transform.Translate ((Vector3.forward * Time.deltaTime) * this.gorilla.movementspeed);
			}

			// Reset Attack Animation
			this.animator.SetBool ("Attack", false);
		}

		this.attackTimer -= Time.deltaTime;
		if (this.attackTimer < 0) {
			// Reset Timer
			this.attackTimer = this.gorilla.attackspeed;

			if (this.hittedGameObject == null) {
				return;
			}

			// Set Attack Animation
			this.animator.speed = 1 / this.gorilla.attackspeed;
			this.animator.SetBool ("Attack", true);

			// Should Attack
			var go = Instantiate (this.gorilla.bullet.model, centerPos, Quaternion.Euler (new Vector3 (0, -90, 0))).gameObject;
			var bulletScript = go.GetComponent (typeof(BulletBehavior)) as BulletBehavior;
			bulletScript.bullet = this.gorilla.bullet;
			bulletScript.hitTag = "Plant";
		}
	}

}