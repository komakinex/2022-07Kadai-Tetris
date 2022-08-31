//  /*********************************************************************************
//   *********************************************************************************
//   *********************************************************************************
//   * Produced by Skard Games										                  *
//   * Facebook: https://goo.gl/5YSrKw											      *
//   * Contact me: https://goo.gl/y5awt4								              *											
//   * Developed by Cavit Baturalp Gürdin: https://tr.linkedin.com/in/baturalpgurdin *
//   *********************************************************************************
//   *********************************************************************************
//   *********************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class InGameUI : MonoBehaviour {

	[SerializeField] private Text _score;
    [SerializeField] private Text _highScore;
    public Text scoreLabel;
    public Text highScoreLabel;  
    
    public GameObject gameOverPopUp;

    public void UpdateScoreUI()
	{
        _score.text = Managers.Score.currentScore.ToString();
        _highScore.text = Managers.Score.highScore.ToString();
    }
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
        scoreLabel.rectTransform.DOAnchorPosY(-334, 1, true);
        highScoreLabel.rectTransform.DOAnchorPosY(-334, 1, true);
        _score.rectTransform.DOAnchorPosY(-375, 1, true);
        _highScore.rectTransform.DOAnchorPosY(-375, 1, true);
    }

    public void InGameUIEndAnimation()
    {
        scoreLabel.rectTransform.DOAnchorPosY(-334+650, 0.3f, true);
        highScoreLabel.rectTransform.DOAnchorPosY(-334 + 650, 0.3f, true);
        _score.rectTransform.DOAnchorPosY(-375 + 650, 0.3f, true);
        _highScore.rectTransform.DOAnchorPosY(-375 + 650, 0.3f, true);
    }


}
