using UnityEngine;
using UniRx;
using System;

namespace hosoi
{
public class GameManager : MonoBehaviour
{
	private bool _isGameActive;
	public bool isGameActive
	{
		get{ return _isGameActive; }
		set{ _isGameActive = value; }
	}

	private float _timeSpent = 0;
	private int _numberOfGames = 0;
	private float _gamePlayDuration;

	private State _currentState;
	Subject<State> OnStateChange = new Subject<State>();
	public IObservable<State> OnStateChangeObservable { get { return OnStateChange; } }

	// テトリススタート！！
	void Start()
	{
		_isGameActive = false;

		this.ObserveEveryValueChanged(_ => _currentState)
			.Skip(1) // 再生したときの初回をスキップ
			.Subscribe(currentState =>
			{
				Debug.Log("State Changed");
				OnStateChange.OnNext(currentState);
			})
			.AddTo(this);

		SetState(State.Menu);
	}
	// ステート変更
	public void SetState(State nextState)
	{
		if (_currentState == State.Play)
		{
			_timeSpent += Time.time - _gamePlayDuration;
		}
		Debug.Log("time spent: " + _timeSpent);
		_currentState = nextState;
		Debug.Log("Now: " + _currentState);
	}

	public void StateAction(State state)
	{
		switch (state)
		{
			case State.Menu:

				break;
			case State.Play:
				_gamePlayDuration = Time.time;
				break;
			case State.Pause:
				break;
			case State.Gameover:
				_isGameActive = false;
				_numberOfGames++; // ゲームの回数記録
				Debug.Log("Number of Games: " + _numberOfGames);
				break;
			default:
				break;
		}
	}
	public void ButtonAction(string btn)
	{
		switch (btn)
		{
			case "play":
				SetState(State.Play);
				break;
			case "pause":
				SetState(State.Pause);
				break;
			case "restart":
				_isGameActive = false;
				SetState(State.Play);
				break;
			case "home":
				SetState(State.Menu);
				break;
			default:
				break;
		}
	}
}
}