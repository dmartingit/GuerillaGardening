using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaSpawnBehavior : MonoBehaviour {
	
	void Start () {	
	}

	void Update () {	
	}

	public void Spawn() {
		var gorillaList = GameStats.waveList [GameStats.wave].gorillas;
		if (gorillaList.Count == 0) {
			return;
		}

		var model = gorillaList [Random.Range (0, gorillaList.Count - 1)].model;

		Vector3 topPos = this.transform.position;
		topPos.y += 1f;
		Instantiate (model, topPos, Quaternion.identity);
	}

}