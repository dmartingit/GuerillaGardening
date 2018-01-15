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

		var gorilla = gorillaList [Random.Range (0, gorillaList.Count)];

		// Instantiate gorilla
		Vector3 topPos = this.transform.position;
		topPos.y += 0.5f;
		var go = Instantiate (gorilla.model, topPos, Quaternion.Euler(new Vector3(0, -90, 0))).gameObject;
		var gorillaScript = gorilla.model.GetComponent(typeof(GorillaBehavior)) as GorillaBehavior;
		gorillaScript.gorilla = gorilla;
	}

}