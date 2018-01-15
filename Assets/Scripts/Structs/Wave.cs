using System.Collections.Generic;

[System.Serializable]
public struct Wave {
	public List<Gorilla> gorillas;
	public float spawnRateMin;
	public float spawnRateMax;
	public float time;
}