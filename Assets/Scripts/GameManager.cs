using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public UnityAction OnPiecePlaced;
	// Use this for initialization
	void Awake () {
		if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
	}

    // Update is called once per frame
    public void ReportPiecePlaced()
    {
        if (OnPiecePlaced != null)
        {
            OnPiecePlaced();
        }
        List<Collision> blockList = new List<Collision>();
        foreach (Collision block in FindObjectsOfType<Collision>())
        {
            blockList.Add(block);
        }

        for (int i = 19; i >= 0; i--)
        {
            int rowBlockCount = 0;
            foreach(Collision block in blockList)
            {
                if (block.GetCurrentRow() == i)
                {
                    rowBlockCount++;
                }
            }
            print("Blocks in row " + i + ": " + rowBlockCount);
            if (rowBlockCount > 9)
            {
                GameObject newCollection = new GameObject();
                PieceHandler ph = newCollection.AddComponent<PieceHandler>();
                ph.gameObject.SetActive(false);
                ph.Uncontrollable = true;
                foreach (Collision block in blockList)
                {
                    if (block.GetCurrentRow() == i)
                    {
                        print("Destroying");
                        Destroy(block.gameObject);
                    }
                    else if (block.GetCurrentRow() < i)
                    {
                        block.transform.parent = newCollection.transform;
                        ph.boxes.Add(block);
                    }
                }
                ph.gameObject.SetActive(true);
            }
        }
	}
}
