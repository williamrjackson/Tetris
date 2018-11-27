using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowChecker : MonoBehaviour {
    public Collision[] rowColliders;

    public static RowChecker instance;
	
	void Awake()
    {
	    if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
	}

    public void CheckRows()
    {
		foreach(Collision row in rowColliders)
        {
            if (row.GetOverlapCount() > 9)
            {
                print("ClearRow");
            }
        }
	}
}
