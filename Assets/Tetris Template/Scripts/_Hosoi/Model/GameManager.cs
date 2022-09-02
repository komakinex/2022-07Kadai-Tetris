using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
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

	private State _currentState;
	public State State
	{
		get { return _currentState; }
	}

	Subject<State> OnStateChange = new Subject<State>();
	public IObservable<State> OnStateChangeObservable { get { return OnStateChange; } }

	void Awake()
	{
		EnablePlay(false);
	}

	// テトリススタート！！
	void Start()
	{
		this.ObserveEveryValueChanged(_ => _currentState)
			.Skip(1)
			.Subscribe(currentState =>
			{
				Debug.Log("State Changed");
				OnStateChange.OnNext(currentState);
				// ステート分岐の関数
			})
			.AddTo(this);

		SetState(State.Menu);
	}
	// void Update()
	// {
	// 	if (_currentState != null)
	// 	{
	// 		_currentState.OnUpdate();
	// 	}
	// }

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
				EnablePlay(false);
				GameCount();
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
				EnablePlay(false);
				SetState(State.Play);
				break;
			default:
				break;
		}
	}

	//Changes the current game state
	public void SetState(State nextState)
	{
		// if (_currentState != null)
		// {
		// 	_currentState.OnDeactivate();
		// }
		_currentState = nextState;
		Debug.Log(_currentState);

		// if (_currentState != null)
		// {
		// 	_currentState.OnActivate();
		// 	Debug.Log("setstate");
		// }
	}

	public void EnablePlay(bool isActive)
	{
		Debug.Log(isActive);
		_isGameActive = isActive;
	}

	public void GameCount()
	{
		_numberOfGames++;
	}
}
}