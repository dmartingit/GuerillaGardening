using UnityEngine;

public class BulletBehavior : MonoBehaviour {

	public Bullet bullet;
	public string hitTag;

	private float destroyingRange;

	void Start () {
		this.destroyingRange = this.transform.position.x + (this.transform.forward.x * this.bullet.range);
	}

	void Update () {
		this.GetComponent<Rigidbody> ().velocity = (this.transform.forward * this.bullet.speed);
		if ((this.transform.forward.x > 0) && (this.transform.position.x > this.destroyingRange)) {
			Destroy (this.gameObject);
			return;
		} else if ((this.transform.forward.x < 0) && (this.transform.position.x < this.destroyingRange)) {
			Destroy (this.gameObject);
			return;
		}
	}

	void OnCollisionEnter (Collision col) {
		var go = col.gameObject;
		if ((go.tag == "Gorilla") && (go.tag == this.hitTag)) {
			var scriptGorilla = go.GetComponentInParent (typeof(GorillaBehavior)) as GorillaBehavior;
			scriptGorilla.gorilla.health -= this.bullet.damage;
			Destroy (this.gameObject);
		} else if ((go.tag == "Plant") && (go.tag == this.hitTag)) {
			var scriptPlant = go.GetComponentInParent (typeof(PlantBehavior)) as PlantBehavior;
			scriptPlant.plant.health -= this.bullet.damage;
			Destroy (this.gameObject);
		}
	}
}
