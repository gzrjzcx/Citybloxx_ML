using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreControl : MonoBehaviour
{

	public List<int> highScoreList = new List<int>();
	public List<string> nameList = new List<string>();

	private GameObject highScoreBoardPrefab;
	private GameObject highScore;
	private int listLength = 10;
	private string scoreKey = "highScore_";
	private string nameKey = "highScoreName_";

	void Start()
	{
		for(int i=listLength; i>0; i--)
		{
			highScoreList.Add(PlayerPrefs.GetInt(scoreKey + i.ToString()));
			nameList.Add(PlayerPrefs.GetString(nameKey + i.ToString()));
		}
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			SpawnHighScoreBoard("Alex");
		}
	}

	public void UpdateHighScore(int curScore, string curName)
	{
		for(int i = listLength-1; i>0; i--)
		{
			if(curScore > highScoreList[i])
			{
				PlayerPrefs.SetInt(scoreKey + i.ToString(), curScore);
				PlayerPrefs.SetString(nameKey + i.ToString(), curName);
				break;
			}
		}
	}

	public void ShowHighScore()
	{
		for(int i=listLength-1; i>0; i--)
		{
			if(GameControl.instance.stackedPieceNum == highScoreList[i])
			{
				SpawnHighScoreBoard(nameList[i]);
			}
		}
	}

	void SpawnHighScoreBoard(string name)
	{
		string pfbPath = "highScore/prefabs/HighScoreBoard";
		highScoreBoardPrefab = Resources.Load<GameObject>(pfbPath);

		Vector3 spawnPos = Camera.main.ScreenToWorldPoint(
			new Vector3(0, 0, 8));
		float offsetX = 3f;
		float offsetY = 2f;
		spawnPos.x += offsetX;
		spawnPos.y -= offsetY;

		highScore = Instantiate(highScoreBoardPrefab, spawnPos, Quaternion.identity);
		highScore.GetComponent<HighScore>().nameText.text = name;
		highScore.transform.SetParent(this.transform, true);
	}

}
