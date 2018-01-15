using UnityEngine;

public class GorillaBehavior : MonoBehaviour {

	public Gorilla gorilla;

	protected virtual void Start () {
	}

	protected virtual void Update () {
		if (this.gorilla.health <= 0) {
			Destroy (this.gameObject);
		}
	}

}