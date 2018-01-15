using UnityEngine;

public class MeleeGorillaBehavior : GorillaBehavior {

	private new Renderer renderer;
	private Animator animator;
	private GameObject hittedGameObject;
	private float attackTimer;

	protected override void Start () {
		base.Start ();

		this.renderer = GetComponentInChildren<Renderer> ();
		this.animator = GetComponent<Animator> ();
		this.attackTimer = this.gorilla.attackspeed;
	}

	protected override void Update () {
		base.Update ();

		// Get Objects infront
		var centerPos = this.renderer.bounds.center;
		var fwd = this.transform.forward;
		RaycastHit hit;
		if (Physics.Raycast (centerPos, fwd, out hit, this.gorilla.attackrange) && (hit.collider.tag == "Plant")) {
			this.hittedGameObject = hit.collider.gameObject;
		}

		if (this.hittedGameObject == null) {
			// Movement
			this.transform.Translate ((Vector3.forward * Time.deltaTime) * this.gorilla.movementspeed);

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
			this.animator.SetBool ("Attack", true);

			// Damage Calculation
			var plantScript = this.hittedGameObject.GetComponentInParent(typeof(PlantBehavior)) as PlantBehavior;
			plantScript.plant.health -= this.gorilla.attackdamage;
			Debug.Log ("Health: " + plantScript.plant.health.ToString ());
		}
	}

}