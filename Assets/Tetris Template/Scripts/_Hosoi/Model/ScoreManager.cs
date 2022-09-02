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
	[SerializeField] private int _score = 100;

	Subject<int> OnHighScoreChange = new Subject<int>();
	public IObservable<int> OnHighScoreObservable { get { return OnHighScoreChange; } }
	Subject<int> OnCurrentScoreChange = new Subject<int>();
	public IObservable<int> OnCurrentScoreObservable { get { return OnCurrentScoreChange; } }

	void Awake()
	{
		if (_bestHighScore != 0)
		{
			_highScore = _bestHighScore;
		}
		else
		{
			_highScore = 0;
		}
	}
	void Start()
	{
		// スコアの変更を登録
		this.ObserveEveryValueChanged(_ => _currentScore)
			.Subscribe(currentScore =>
			{
				OnCurrentScoreChange.OnNext(currentScore);
			})
			.AddTo(this);
		this.ObserveEveryValueChanged(_ => _highScore)
			.Subscribe(highScore =>
			{
				OnHighScoreChange.OnNext(highScore);
			})
			.AddTo(this);
	}
	public void StateAction(State state)
	{
		switch (state)
		{
			case State.Menu:

				break;
			case State.Play:
				break;
			case State.Pause:
				break;
			case State.Gameover:
				CurrentIsHighScore();
				break;
			default:
				break;
		}
	}


	public void CurrentIsHighScore()
	{
		_highScore = _currentScore;
	}

	public void OnScore()
	{
		Debug.Log("get score");
		_currentScore += _score;
		CheckHighScore();
		// _totalScore += _score;
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
	}

}
}