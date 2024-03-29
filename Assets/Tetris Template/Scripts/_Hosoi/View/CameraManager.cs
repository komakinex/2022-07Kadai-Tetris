using UnityEngine;
using DG.Tweening;
using UniRx;
using System;

namespace hosoi
{
public class CameraManager : MonoBehaviour
{
	[SerializeField] private Camera _mainCam;
	private float _mainMenuSize = 13.5f;
	private float _inGameSize = 11f;

	Subject<Unit> OnZoomInFin = new Subject<Unit>();
	public IObservable<Unit> OnZoomInFinObservable { get { return OnZoomInFin; } }
	Subject<Unit> OnNotZoomIn = new Subject<Unit>();
	public IObservable<Unit> OnNotZoomInObservable { get { return OnNotZoomIn; } }

	public void StateAction(State state)
	{
		switch (state)
		{
			case State.Menu:
				ZoomOut();
				break;
			case State.Play:
				ZoomIn();
				break;
			case State.Pause:
				ZoomOut();
				break;
			default:
				break;
		}
	}

	public void ZoomIn()
	{
		if (_mainCam.orthographicSize != _inGameSize)
		{
			_mainCam.DOOrthoSize(_inGameSize, 1f).SetEase(Ease.OutCubic).OnComplete(() =>
			{
				OnZoomInFin.OnNext(Unit.Default);
			});
		}
		else
		{
			OnNotZoomIn.OnNext(Unit.Default);
		}
		
	}

	public void ZoomOut()
	{
		if (_mainCam.orthographicSize != _mainMenuSize)
		{
			_mainCam.DOOrthoSize(_mainMenuSize, 1f).SetEase(Ease.OutCubic);
		}
	}
}
}