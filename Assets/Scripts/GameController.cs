using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public GameObject player;
	public GameObject[] bonuses;
	public GameObject knifeEnemy;
	public GameObject lightEnemy;
	public GameObject heavyEnemy;

	public Vector3 spawnValues;
	public Text ScoreText; 
	public Text LivesText;
	public Text SelectLevelText;
	public Text GameOverText;
	public Text RestartText;

	public int enemyCount;
	public int bonusCount;
	public int startWait;
	public int spawnWait;
	public int waveWait;

	private int score;

	private int lives;
	private bool gameOver;

	private bool restart;
	private bool levelSelected;

	void Start() 
	{
		restart = false;
		gameOver = false;
		levelSelected = true;
		RestartText.text = "";
		GameOverText.text = "";
		score = 0;
		UpdateScore ();
		//SetLives ();
		lives = 3;
		UpdateLivesText ();
		if (levelSelected) {
			 SelectLevelText.text = "";
			StartCoroutine (SpawnBonusWaves ());
			StartCoroutine (SpawnKnifeWaves ());
			StartCoroutine (SpawnLightWaves ());
		}
	} 

	void SetLives() 
	{
		SelectLevelText.text = "Press 'E' for Easy, \n 'M' for Medium, \n 'H' for hard"; 

		if (Input.GetKeyDown(KeyCode.E))
		{
			lives = 10;
			levelSelected = true;
			return;
		}

		if (Input.GetKeyDown(KeyCode.M))
		{
			lives = 7;
			levelSelected = true;
			return;
		}

		if (Input.GetKeyDown(KeyCode.H))
		{
			lives = 3;
			levelSelected = true;
			return;
		}

	}

	void GameOver()
	{
		GameOverText.text = "GAME OVER!";
		gameOver = true;
	}

	void Restart() 
	{
		RestartText.text = "Press 'R' to Restart";
		restart = true;
	}

	public void AddScore(int scoreValue)
	{
		score += scoreValue;
		UpdateScore();
	}

	public void DeductLives()
	{
		if (lives == 0)
		{
			Debug.Log("Game should be over");
		}
		else
		{
			lives--;
			UpdateLivesText();

			if (lives==0)
			{
				Destroy (player);
				GameOver ();
			}
		}
	}

	public void IncLives()
	{
		if (lives == 0)
		{
			Debug.Log("Game should be over");
		}
		else
		{
			lives++;
			UpdateLivesText();
		}
	}


	void UpdateLivesText()
	{
		LivesText.text = "Lives Remaining: " + lives.ToString();
	}


	void UpdateScore()
	{
		ScoreText.text = "Score: " + score.ToString();
	}

	IEnumerator SpawnBonusWaves() {
		while (true) {
			yield return new WaitForSeconds (2 * startWait);
			for (int i = 0; i < Random.Range(1, bonusCount); i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x+3, spawnValues.x-3), spawnValues.y, Random.Range(-spawnValues.z+4, spawnValues.z-4));
				Quaternion spawnRotation = Quaternion.identity;
				GameObject bonus = bonuses [Random.Range (0, bonuses.Length)];
				Instantiate (bonus, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (2 * spawnWait);
			}
			yield return new WaitForSeconds (2 * waveWait);

			if (gameOver) {
				Restart ();
				break;
			}
		}
	}

	IEnumerator SpawnKnifeWaves() {
		while (true) {
			yield return new WaitForSeconds (startWait);
			for (int i = 0; i < enemyCount; i++) {
				spawnEnemies (knifeEnemy);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) {
				Restart ();
				break;
			}
		}
	}

	IEnumerator SpawnLightWaves() {
		while (true) {
			yield return new WaitForSeconds (2*startWait);
			for (int i = 0; i < enemyCount/2; i++) {
				spawnEnemies (lightEnemy);
				yield return new WaitForSeconds (2*spawnWait);
			}
			yield return new WaitForSeconds (2*waveWait);

			if (gameOver) {
				Restart ();
				break;
			}
		}
	}

	IEnumerator SpawnHeavyWaves() {
		while (true) {
			yield return new WaitForSeconds (2*startWait);
			for (int i = 0; i < enemyCount/2; i++) {
				spawnEnemies (heavyEnemy);
				yield return new WaitForSeconds (2*spawnWait);
			}
			yield return new WaitForSeconds (2*waveWait);

			if (gameOver) {
				Restart ();
				break;
			}
		}
	}

	void spawnEnemies(GameObject enemy) {
		Vector3 topSpawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
		Vector3 bottomSpawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, -spawnValues.z);
		Vector3 leftSpawnPosition = new Vector3 (-spawnValues.x, spawnValues.y, Random.Range (-spawnValues.z, spawnValues.z));
		Vector3 rightSpawnPosition = new Vector3 (spawnValues.x, spawnValues.y, Random.Range (-spawnValues.z, spawnValues.z));
		Vector3 direction;
		int j = Random.Range (0, 3);
		if (j == 0) {
			direction = player.transform.position - topSpawnPosition;
			Quaternion spawnRotation = Quaternion.LookRotation (direction);
			Instantiate (enemy, topSpawnPosition, spawnRotation);
		}
		if (j == 1) {
			direction = player.transform.position - bottomSpawnPosition;
			Quaternion spawnRotation = Quaternion.LookRotation (direction);
			Instantiate (enemy, bottomSpawnPosition, spawnRotation);
		}
		if (j == 2) {
			direction = player.transform.position - leftSpawnPosition;
			Quaternion spawnRotation = Quaternion.LookRotation (direction);
			Instantiate (enemy, leftSpawnPosition, spawnRotation);
		}
		if (j == 3) {
			direction = player.transform.position - rightSpawnPosition;
			Quaternion spawnRotation = Quaternion.LookRotation (direction);
			Instantiate (enemy, rightSpawnPosition, spawnRotation);
		}
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R))
				Application.LoadLevel (Application.loadedLevel);
		}
	}


}