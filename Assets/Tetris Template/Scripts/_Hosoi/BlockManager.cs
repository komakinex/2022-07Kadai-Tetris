using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using UniRx.Triggers;

namespace hosoi
{
public enum ShapeType
{
    I,
    T,
    O,
    L,
    J,
    S,
    Z
}
public class BlockManager : MonoBehaviour
{
	[HideInInspector] public TetrisShape currentShape;
	[HideInInspector] public Transform blockHolder;
	// [HideInInspector] public PlayerStats stats;
	public GameObject[] shapeTypes;

	[HideInInspector] public ShapeType type;
	[HideInInspector] public ShapeMovementController movementController;

	[SerializeField] ColorManager _colorManager;

	void Awake()
	{
		movementController = GetComponent<ShapeMovementController>();
		AssignRandomColor();
	}

	public void Spawn()
	{
		// Random Shape
		int i = Random.Range(0, shapeTypes.Length);

		// Spawn Group at current Position
		GameObject temp =Instantiate(shapeTypes[i]);
		currentShape = temp.GetComponent<TetrisShape>();
		temp.transform.parent = blockHolder;

		// 後で組み込む
		// Managers.Input.isActive = true;
	}

	void AssignRandomColor()
	{
		Color temp = _colorManager.TurnRandomColorFromTheme();
		foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>().ToList())
			renderer.color = temp;
	}
}
}