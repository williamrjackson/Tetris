using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {
    PieceHandler parentPiece;

    private int m_CurrentRow;
    void Start()
    {
        parentPiece = transform.parent.GetComponent<PieceHandler>();
        GameManager.instance.OnPiecePlaced += NewPlacement;
    }
    public int GetCurrentRow()
    {
        return m_CurrentRow;
    }
    private void NewPlacement()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Walls")
        {
            parentPiece.Collided();
        }
        else if (col.GetComponent<Row>())
        {
            m_CurrentRow = col.GetComponent<Row>().GetRowIndex();
        }
    }
}
