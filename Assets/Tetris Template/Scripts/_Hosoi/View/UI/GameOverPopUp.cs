using UnityEngine;
using UnityEngine.UI;

namespace hosoi
{
public class GameOverPopUp : MonoBehaviour
{
	[SerializeField] private Text _gameOverScore;
	
	public void SetScore(int score)
	{
		_gameOverScore.text = score.ToString();
	}
}
}