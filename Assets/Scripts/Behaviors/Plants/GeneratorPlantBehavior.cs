using UnityEngine;

public class GeneratorPlantBehavior : PlantBehavior {

	private float gemSpawnTimer;
	private Renderer rend;
	
	protected override void Start () {
		base.Start ();
		this.rend = this.gameObject.GetComponentInChildren<Renderer> ();
		this.gemSpawnTimer = this.plant.gemSpawnRate;
	}

	protected override void Update () {
		base.Update ();

		this.gemSpawnTimer -= Time.deltaTime;

		if (this.gemSpawnTimer < 0) {
			Vector3 pos = this.rend.bounds.center;
			pos.x += this.rend.bounds.extents.x;
			var go = Instantiate (this.plant.gem.model, pos, Quaternion.identity).gameObject;
			var gemScript = go.GetComponent (typeof(GemBehavior)) as GemBehavior;
			gemScript.gem = this.plant.gem;

			// Reset Timer
			this.gemSpawnTimer = this.plant.gemSpawnRate;
		}
	}
}
