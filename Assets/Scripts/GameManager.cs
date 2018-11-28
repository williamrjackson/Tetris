using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public UnityAction OnPiecePlaced;
    public UnityAction RestartGame;

    private bool m_GameOver;
    private bool m_Pause;
    public bool GameOver
    {
        set
        {
            if (value == false && m_GameOver == true)
            {
                foreach(Collision c in FindObjectsOfType<Collision>())
                {
                    Destroy(c.gameObject);
                }
                foreach(PieceHandler ph in FindObjectsOfType<PieceHandler>())
                {
                    Destroy(ph.gameObject);
                }
                print("Restart");
                if (RestartGame != null)
                    RestartGame();
                m_GameOver = false;
                PieceGenerator.instance.Spawn();
            }
            m_GameOver = value;
            if (m_GameOver) print("GameOver");
        }
        get
        {
            return m_GameOver;
        }
    }
    public bool Pause
    {
        set
        {
            m_Pause = value;
            if (m_Pause)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        get
        {
            return m_Pause;
        }
    }

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
            // print("Blocks in row " + i + ": " + rowBlockCount);
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameOver = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause = true;
        }
    }
}
