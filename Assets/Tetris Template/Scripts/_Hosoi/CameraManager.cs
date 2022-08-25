using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace hosoi
{
public class CameraManager : MonoBehaviour
{
	public Camera main;
	private float _mainMenuSize = 13.5f;
	private float _inGameSize = 11f;

	public void ZoomIn()
	{
		if (main.orthographicSize != _inGameSize)
		{
			main.DOOrthoSize(_inGameSize, 1f).SetEase(Ease.OutCubic).OnComplete(() =>
			{
				StartCoroutine(StartGamePlay());
			});
		}
		else
		{
			Managers.Spawner.Spawn();
			Managers.Game.isGameActive = true;
		}
	}

	public void ZoomOut()
	{
		if (main.orthographicSize != _mainMenuSize)
			main.DOOrthoSize(_mainMenuSize, 1f).SetEase(Ease.OutCubic); ;
	}

	IEnumerator StartGamePlay()
	{
		yield return new WaitForEndOfFrame();

		if (!Managers.Game.isGameActive)
		{
			Managers.Spawner.Spawn();
			Managers.Game.isGameActive = true;
		}
		yield break;
	}


}
}