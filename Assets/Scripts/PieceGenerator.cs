using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceGenerator : MonoBehaviour {
    public PieceHandler[] Pieces;

    public static PieceGenerator instance;

    void Awake () {
		if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
	}
    void Start()
    {
        Spawn();
    }

    public void Spawn () {
        GameObject randomPieceTemplate = Pieces[Random.Range(0, Pieces.Length - 1)].gameObject;
        GameObject newPiece = Instantiate(randomPieceTemplate, randomPieceTemplate.transform.position, randomPieceTemplate.transform.rotation);
        newPiece.SetActive(true);
	}
}
