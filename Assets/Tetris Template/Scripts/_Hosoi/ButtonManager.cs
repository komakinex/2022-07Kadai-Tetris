using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace hosoi
{
public class ButtonManager : MonoBehaviour
{
	public Subject<string> OnButtonClicked = new Subject<string>();
	[SerializeField] private Button _playBtn, _pauseBtn, _restartBtn, _settingBtn, _statsBtn;

	void Start()
	{
		// ボタンの登録
		SubscribeButtons();
	}

	// ボタンを押したことを通知
	private void SubscribeButtons()
	{
		_playBtn.OnClickAsObservable()
			.Subscribe(_ =>{OnButtonClicked.OnNext("play");})
			.AddTo(this);
		_pauseBtn.OnClickAsObservable()
			.Subscribe(_ => {OnButtonClicked.OnNext("pause");})
			.AddTo(this);
		_restartBtn.OnClickAsObservable()
			.Subscribe(_ => {OnButtonClicked.OnNext("restart");})
			.AddTo(this);
		_settingBtn.OnClickAsObservable()
			.Subscribe(_ => {OnButtonClicked.OnNext("setting");})
			.AddTo(this);
		_statsBtn.OnClickAsObservable()
			.Subscribe(_ => {OnButtonClicked.OnNext("stats");})
			.AddTo(this);
	}
}
}