using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawnBehavior : MonoBehaviour {

	private bool canPlant;

	void Start () {	
	}

	void Update () {
		var atTop = this.transform.TransformDirection (new Vector3 (0, 1, 0));
		if (Physics.Raycast (this.transform.position, atTop, 2)) {
			canPlant = false;
		} else {
			canPlant = true;
		}
	}

	void OnMouseDown() {
		if (!canPlant) {
			return;
		}

		Plant plant = GameStats.selectedPlant;

		// No plant was selected
		if (plant == null) {
			return;
		}

		// Check if playable
		if (plant.cost > GameStats.seeds) {
			return;
		}

		GameStats.seeds -= plant.cost;

		Vector3 topPos = this.transform.position;
		topPos.y += 1f;
		Instantiate (plant.model, topPos, Quaternion.identity);
	}

}