using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceHandler : MonoBehaviour {

    public List<Collision> boxes = new List<Collision>();
    public bool Uncontrollable;

    private bool isPlanted;
    private bool hasMoved;
    void OnEnable ()
    {
        if (Uncontrollable)
        {
            StartCoroutine(AllTheWayDown());
        }
        else
        {
            StartCoroutine(StepPerSec());
        }
	}
	
    public void Collided()
    {
        if (transform.position.x > 0)
        {
            ShiftLeft();
        }
        else
        {
            ShiftRight();
        }
    }
    void Update()
    {
        if (!isPlanted && !Uncontrollable)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ShiftLeft();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ShiftRight();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                RotatePiece();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                StopAllCoroutines();
                StartCoroutine(AllTheWayDown());
            }
        }
    }
    void RotatePiece()
    {
        transform.Rotate(Vector3.back, 90f);
    }

    void ShiftRight()
    {
        foreach (Collision box in boxes)
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(box.transform.position + Vector3.right * .26f, Vector3.right, .25f);
            if (hit && hit.transform.parent != transform)
            {
                return;
            }
        }
        transform.position += Vector3.right * .5f;
    }
    void ShiftLeft()
    {
        foreach (Collision box in boxes)
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(box.transform.position + Vector3.left * .26f, Vector3.left, .25f);
            if (hit && hit.transform.parent != transform)
            {
                return;
            }
        }
        transform.position -= Vector3.right * .5f;
    }

    private IEnumerator StepPerSec()
    {
        while (StepDown())
        {
            yield return new WaitForSecondsRealtime(1f);
        }
    }
    private IEnumerator AllTheWayDown()
    {
        while(StepDown())
        {
            yield return new WaitForSecondsRealtime(.025f);
        }
    }

	// Update is called once per frame
	bool StepDown () {
        foreach (Collision box in boxes)
        {
            if (box == null)
                return false;

            RaycastHit2D hit;
            hit = Physics2D.Raycast(box.transform.position + Vector3.down * .26f, Vector3.down, .25f);
            if (hit && hit.transform.parent != transform)
            {
                isPlanted = true;
                foreach(Collision orphanBlock in boxes)
                {
                    if (orphanBlock != null)
                        orphanBlock.transform.parent = null;
                }
                GameManager.instance.ReportPiecePlaced();
                if (!hasMoved)
                {
                    GameManager.instance.GameOver = true;
                    return false;
                }
                if (!Uncontrollable)
                    PieceGenerator.instance.Spawn();

                Destroy(gameObject);
                return false;
            }
        }
        transform.position += Vector3.down * .5f;
        hasMoved = true;
        return true;
	}
}
