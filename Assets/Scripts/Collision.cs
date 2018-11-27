using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {
    PieceHandler parentPiece;

    int currentOverlaps = 0;
    void Awake()
    {
        parentPiece = transform.parent.GetComponent<PieceHandler>();
    }
	void OnTriggerEnter2D()
    {
        currentOverlaps++;
        parentPiece.Collided();
    }
    void OnTriggerExit2D()
    {
        currentOverlaps--;
    }
    public int GetOverlapCount()
    {
        return currentOverlaps;
    }
}
