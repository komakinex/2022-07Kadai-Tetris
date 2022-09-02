using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

namespace hosoi
{
public class InGameUI : MonoBehaviour {

	[SerializeField] private Text _score;
	[SerializeField] private Text _highScore;
	[SerializeField] private Text _scoreLabel;
	[SerializeField] private Text _highScoreLabel;

	public GameObject gameOverPopUp;

	// public void UpdateScoreUI(int score)
	// {
	// 	_score.text = score.ToString();
	// 	_highScore.text = score.ToString();
	// }
	public void UpdateCurrentScoreUI(int score)
	{
		_score.text = score.ToString();
	}
	public void UpdateHighScoreUI(int highScore)
	{
		_highScore.text = highScore.ToString();
	}

	public void InGameUIStartAnimation()
	{
		_scoreLabel.rectTransform.DOAnchorPosY(-334, 1, true);
		_highScoreLabel.rectTransform.DOAnchorPosY(-334, 1, true);
		_score.rectTransform.DOAnchorPosY(-375, 1, true);
		_highScore.rectTransform.DOAnchorPosY(-375, 1, true);
	}

	public void InGameUIEndAnimation()
	{
		_scoreLabel.rectTransform.DOAnchorPosY(-334+650, 0.3f, true);
		_highScoreLabel.rectTransform.DOAnchorPosY(-334 + 650, 0.3f, true);
		_score.rectTransform.DOAnchorPosY(-375 + 650, 0.3f, true);
		_highScore.rectTransform.DOAnchorPosY(-375 + 650, 0.3f, true);
	}

}
}