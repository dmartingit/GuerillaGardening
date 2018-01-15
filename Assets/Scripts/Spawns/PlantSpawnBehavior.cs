using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawnBehavior : MonoBehaviour {

	private bool canPlant;

	void Start () {	
	}

	void Update () {
		// Check if there is smth already planted
		var atTop = this.transform.TransformDirection (new Vector3 (0, 1, 0));
		if (Physics.Raycast (this.transform.position, atTop, 2)) {
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

		// Instantiate plant with values
		Vector3 topPos = this.transform.position;
		topPos.y += 1f;
		var go = Instantiate (plant.model, topPos, Quaternion.identity).gameObject;
		var plantScript = (PlantBehavior)go.AddComponent (typeof(PlantBehavior));
		plantScript.plant = plant;
	}

}