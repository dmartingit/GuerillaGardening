using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehavior : MonoBehaviour {

	public List<Plant> plants;
	public List<Wave> waves;
	public List<Banana> bananas;

	private float timer;
	private UITooltipHelper tooltipHelper;

	void Start () {
		this.tooltipHelper = this.gameObject.AddComponent (typeof(UITooltipHelper)) as UITooltipHelper;

		GameStats.plantList = this.plants;
		GameStats.bananaList = this.bananas;

		if (this.waves.Count == 0) {
			return;
		}

		GameStats.wave = 0;
		GameStats.waveList = this.waves;
		this.timer = this.waves[0].time;

		this.tooltipHelper.SetText ("Wave 1");
		this.tooltipHelper.Draw ();
	}

	void Update () {
		if (this.waves.Count == 0) {
			return;
		}

		if (GameStats.wave == this.waves.Count) {
			return;
		}

		this.timer -= Time.deltaTime;
		if (this.timer < 0) {
			// Next Wave
			++GameStats.wave;

			// Do not do shit if last wave is done
			if (GameStats.wave == this.waves.Count) {
				// TODO(dmartin): load next level
				return;
			}

			// Show GUI
			this.tooltipHelper.SetText ("Wave " + (GameStats.wave + 1).ToString());
			this.tooltipHelper.Draw();

			// Reset Timer
			this.timer = this.waves[GameStats.wave].time;
		}
	}
}
