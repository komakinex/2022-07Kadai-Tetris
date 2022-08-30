using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hosoi
{
public class BlockManager : MonoBehaviour
{
	[HideInInspector] public TetrisShape currentShape;
	[HideInInspector] public Transform blockHolder;
	[HideInInspector] public PlayerStats stats;
	public GameObject[] shapeTypes;

	public void Spawn()
	{
		// Random Shape
		int i = Random.Range(0, shapeTypes.Length);

		// Spawn Group at current Position
		GameObject temp =Instantiate(shapeTypes[i]);
		currentShape = temp.GetComponent<TetrisShape>();
		temp.transform.parent = blockHolder;
		// Managers.Input.isActive = true;
	}
}
}