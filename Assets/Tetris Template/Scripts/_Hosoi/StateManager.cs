using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

namespace hosoi
{
public enum State
{
	None = 0, Menu, Play, Gameover
}

public class StateManager : MonoBehaviour
{
	// State通知用
	Subject<string> OnStateChange = new Subject<string>();
	public IObservable<string> OnStateChangeObservable { get { return OnStateChange; } }


	public State current;
	public State next;
	public State prev;
	public bool isFirst = false;

	public StateManager(State state)
	{
		current = state;
		next = state;
		prev = state;
	}

	void Start()
	{
		this.UpdateAsObservable()
			.Where(_ => Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.UpArrow))
			.Subscribe(_ =>
			{
				// Aか↑を押したよ通知
				// Debug.Log("push");
				OnStateChange.OnNext("up");
			});
	}

	public void Update()
	{
		if (next != current)
		{
			isFirst = true;
			prev = current;
			current = next;

			Debug.Log(current);
		}
		else
		{
			isFirst = false;
		}
	}

	public void Transition(State next)
	{
		this.next = next;
	}

}
}