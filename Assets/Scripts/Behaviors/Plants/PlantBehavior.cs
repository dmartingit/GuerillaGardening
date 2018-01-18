using UnityEngine;

public class PlantBehavior : MonoBehaviour {

	public Plant plant;

	protected virtual void Start () {
	}

	protected virtual void Update () {
		if (this.plant.health <= 0) {
			Destroy (this.gameObject);
		}
	}

}