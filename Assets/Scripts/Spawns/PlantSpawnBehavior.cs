using UnityEngine;

public class PlantSpawnBehavior : MonoBehaviour {

	private bool canPlant;

	void Update () {
		// Check if there is smth already planted
		if (Physics.Raycast (this.transform.position, this.transform.up, 1f)) {
			this.canPlant = false;
		} else {
			this.canPlant = true;
		}
	}

	void OnMouseDown() {
		if (!canPlant) {
			return;
		}

		Plant plant = GameStats.selectedPlant;

		// No plant was selected
		if (plant.Equals(default(Plant))) {
			return;
		}

		// Check if playable
		if (plant.cost > GameStats.seeds) {
			return;
		}

		GameStats.seeds -= plant.cost;

		// Instantiate plant
		Vector3 topPos = this.transform.position;
		topPos.y += 0.5f;
		var go = Instantiate (plant.model, topPos, Quaternion.Euler(new Vector3(0, 90, 0))).gameObject;
		var plantScript = plant.model.GetComponent(typeof(PlantBehavior)) as PlantBehavior;
		plantScript.plant = plant;
	}

}