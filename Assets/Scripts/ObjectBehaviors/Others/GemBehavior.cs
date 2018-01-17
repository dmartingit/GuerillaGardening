using UnityEngine;

public class GemBehavior : MonoBehaviour {

	public Gem gem;

	void Start () {
		GameStats.seeds += this.gem.seeds;
		Destroy (this.gameObject, this.gem.lifeTime);
	}

}