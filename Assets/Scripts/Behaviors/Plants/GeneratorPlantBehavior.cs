using UnityEngine;

public class GeneratorPlantBehavior : PlantBehavior {

	private float gemSpawnTimer;
	private new Collider collider;
	private Animator animator;
	
	protected override void Start () {
		base.Start ();
		this.collider = GetComponentInChildren<Collider> ();
		this.animator = GetComponent<Animator> ();
		this.gemSpawnTimer = this.plant.gemSpawnRate;
	}

	protected override void Update () {
		base.Update ();

		this.gemSpawnTimer -= Time.deltaTime;

		// Reset Generate Animation
		this.animator.SetBool ("Generate", false);

		if (this.gemSpawnTimer < 0) {
			Vector3 pos = this.collider.bounds.center;
			pos.x += this.collider.bounds.extents.x;
			var go = Instantiate (this.plant.gem.model, pos, Quaternion.identity).gameObject;
			var gemScript = go.GetComponent (typeof(GemBehavior)) as GemBehavior;
			gemScript.gem = this.plant.gem;

			// Generate Animation
			this.animator.SetBool ("Generate", true);

			// Reset Timer
			this.gemSpawnTimer = this.plant.gemSpawnRate;
		}
	}
}
