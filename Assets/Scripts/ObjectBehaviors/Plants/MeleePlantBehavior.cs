using UnityEngine;

public class MeleePlantBehavior : PlantBehavior {

	private new Collider collider;
	private Animator animator;
	private GameObject hittedGameObject;
	private float attackTimer;

	protected override void Start () {
		base.Start ();

		this.collider = GetComponentInChildren<Collider> ();
		this.animator = GetComponent<Animator> ();
		this.attackTimer = this.plant.attackspeed;
	}

	protected override void Update () {
		base.Update ();

		// Get Objects infront
		var centerPos = this.collider.bounds.center;
		var fwd = this.transform.forward;
		RaycastHit hit;
		if (Physics.Raycast (centerPos, fwd, out hit, this.plant.bullet.range) && (hit.collider.tag == "Gorilla")) {
			this.hittedGameObject = hit.collider.gameObject;
		}

		if (this.hittedGameObject == null) {
			// Movement
			if (this.plant.movementspeed != 0) {
				this.transform.Translate ((Vector3.forward * Time.deltaTime) * this.plant.movementspeed);
			}

			// Reset Attack Animation
			this.animator.SetBool ("Attack", false);
		}

		this.attackTimer -= Time.deltaTime;
		if (this.attackTimer < 0) {
			// Reset Timer
			this.attackTimer = this.plant.attackspeed;

			if (this.hittedGameObject == null) {
				return;
			}

			// Set Attack Animation
			this.animator.speed = 1 / this.plant.attackspeed;
			this.animator.SetBool ("Attack", true);

			// Damage Calculation
			var gorillaScript = this.hittedGameObject.GetComponentInParent(typeof(GorillaBehavior)) as GorillaBehavior;
			gorillaScript.gorilla.health -= this.plant.bullet.damage;
		}
	}

}