using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

namespace hosoi
{
public class PlayerInputManager : MonoBehaviour
{
	// キー入力が有効か
	private bool _isActive;

	Subject<string> OnKeyInput = new Subject<string>();
	public IObservable<string> OnKeyInputObservable { get { return OnKeyInput; } }


	void Start()
	{
		// 仮のキー入力ON~~~~~~~
		// EnableKeyInput(true);

		// キー入力
		this.UpdateAsObservable()
			.Where(_ => _isActive)
			.Where(_ => Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.UpArrow))
			.Subscribe(_ =>
			{
				// Aか↑を押したよ通知
				// Debug.Log("push");
				OnKeyInput.OnNext("up");
			});
		this.UpdateAsObservable()
			.Where(_ => _isActive)
			.Where(_ => Input.GetKeyDown(KeyCode.D))
			.Subscribe(_ =>
			{
				OnKeyInput.OnNext("d");
			});
		this.UpdateAsObservable()
			.Where(_ => _isActive)
			.Where(_ => Input.GetKeyDown(KeyCode.LeftArrow))
			.Subscribe(_ =>
			{
				OnKeyInput.OnNext("left");
			});
		this.UpdateAsObservable()
			.Where(_ => _isActive)
			.Where(_ => Input.GetKeyDown(KeyCode.RightArrow))
			.Subscribe(_ =>
			{
				OnKeyInput.OnNext("right");
			});
		this.UpdateAsObservable()
			.Where(_ => _isActive)
			.Where(_ => Input.GetKeyDown(KeyCode.DownArrow))
			.Subscribe(_ =>
			{
				OnKeyInput.OnNext("down");
			});
	}

	public void EnableKeyInput(bool isActive)
	{
		Debug.Log(isActive);
		_isActive = isActive;
	}
}
}