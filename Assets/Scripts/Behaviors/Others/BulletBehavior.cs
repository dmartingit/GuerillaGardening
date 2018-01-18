using UnityEngine;

public class BulletBehavior : MonoBehaviour {

	public Bullet bullet;

	private float destroyingRange;

	void Start () {
		this.destroyingRange = this.transform.position.x + (this.transform.forward.x * this.bullet.range);
	}

	void Update () {
		this.GetComponent<Rigidbody> ().velocity = (this.transform.forward * this.bullet.speed);
		if (this.transform.position.x > this.destroyingRange) {
			Destroy (this.gameObject);
			return;
		}
	}

	void OnCollisionEnter (Collision col) {
		var go = col.gameObject;
		if (go.tag == "Gorilla") {
			var scriptGorilla = go.GetComponentInParent (typeof(GorillaBehavior)) as GorillaBehavior;
			scriptGorilla.gorilla.health -= this.bullet.damage;
			Destroy (this.gameObject);
		}
	}
}
