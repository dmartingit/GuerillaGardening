using System.Collections.Generic;
using UnityEngine;

public static class GameStats {

	public enum GameState {MainMenu, InGame, Pause, GameOver, LevelComplete};

	public static GameState state;

	public static int wave;

	public static int seeds;

	public static Plant selectedPlant;

	public static List<Plant> plantList;

	public static List<Wave> waveList;

	public static List<Banana> bananaList;

}