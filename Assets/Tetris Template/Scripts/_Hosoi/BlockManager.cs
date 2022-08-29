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

}
}