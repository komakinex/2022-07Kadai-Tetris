using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;


namespace hosoi
{
public class ScoreManager : MonoBehaviour
{
	private int _currentScore = 0;
	private int _highScore = 0;
	// public int HighScore
	// {
	// 	get { return _highScore; }
	// }
	private int _bestHighScore = 0;
	private int _totalScore = 0;

	Subject<int> OnHighScoreChange = new Subject<int>();
	public IObservable<int> OnHighScoreObservable { get { return OnHighScoreChange; } }

	void Awake()
	{
		if (_bestHighScore != 0)
		{
			_highScore = _bestHighScore;
			// Managers.UI.inGameUI.UpdateScoreUI();
		}
		else
		{
			_highScore = 0;
			// Managers.UI.inGameUI.UpdateScoreUI();
		}
	}

	void Start()
	{
		// スコアの変更を登録
		this.ObserveEveryValueChanged(_ => _highScore)
			.Subscribe(highscore =>
			{
				OnHighScoreChange.OnNext(highscore);
			})
			.AddTo(this);
	}


	public void CurrentIsHighScore()
	{
		_highScore = _currentScore;
	}

	public void OnScore(int scoreIncreaseAmount)
	{	
		_currentScore += scoreIncreaseAmount;
		CheckHighScore();
		Managers.UI.inGameUI.UpdateScoreUI();
		_totalScore += scoreIncreaseAmount;
	}

	public void CheckHighScore()
	{
		if (_highScore < _currentScore)
		{
			_highScore = _currentScore;
		}
	}

	public void ResetScore()
	{
		_currentScore = 0;
		_highScore = _bestHighScore;
		Managers.UI.inGameUI.UpdateScoreUI();
	}

}
}