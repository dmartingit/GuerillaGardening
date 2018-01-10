using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaSpawnBehavior : MonoBehaviour {

	private bool spawned;

	void Start () {
	}

	void Update () {
		if (this.spawned) {
			return;
		}

		var bananaList = GameStats.bananaList;
		if (bananaList == null) {
			return;
		}
		Vector3 topPos = this.transform.position;
		topPos.y += 1f;
		var model = bananaList [Random.Range (0, bananaList.Count - 1)].model;
		Instantiate (model, topPos, Quaternion.identity);
		this.spawned = true;
	}

}