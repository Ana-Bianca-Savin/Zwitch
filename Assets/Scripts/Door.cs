using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public LayerMask player;
    public bool inArea;
    public Vector2 doorSize;
    public float timer;
    public GameObject lvlLoad;
    public GameObject doorClosed;
    public GameObject playerC;
    public bool doorSound;

    private void Awake()
    {
        doorSound = true;
        doorSize = GetComponent<BoxCollider2D>().size;
    }

    void FixedUpdate()
    {
        Vector2 pointA = (Vector2)transform.position + GetComponent<BoxCollider2D>().offset + Vector2.right * doorSize.x + Vector2.up * doorSize.y;
        Vector2 pointB = (Vector2)transform.position + GetComponent<BoxCollider2D>().offset + Vector2.left * doorSize.x  + Vector2.down * doorSize.y;
        inArea = Physics2D.OverlapArea(pointA, pointB, player);

        if(!playerC.GetComponent<Movement>().GameIsPaused)
        {
            if (inArea)
                timer -= Time.fixedDeltaTime;
            else
                timer = 2f;

            if (timer <= 1f)
            {
                if(doorSound)
                {
                    AudioManager.PlaySound("Door");
                    doorSound = false;
                }
                doorClosed.transform.position = new Vector3(doorClosed.transform.position.x, doorClosed.transform.position.y, 0f);
                transform.position = new Vector3(transform.position.x, transform.position.y, 4f);
                playerC.GetComponent<Movement>().rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            }
            if (timer <= 0f)
                lvlLoad.GetComponent<LevelLoader>().LoadNextLevel();
        }

    }

    void OnDrawGizmos()
    {
        Color meallow = new Color(0.97f, 0.87f, 0.49f, 1f);
        Gizmos.color = meallow;

        Vector2 R1 = (Vector2)transform.position + GetComponent<BoxCollider2D>().offset + Vector2.right * doorSize.x + Vector2.up * doorSize.y;
        Vector2 R2 = (Vector2)transform.position + GetComponent<BoxCollider2D>().offset + Vector2.left * doorSize.x + Vector2.down * doorSize.y;
        Vector2 R3 = (Vector2)transform.position + GetComponent<BoxCollider2D>().offset + Vector2.right * doorSize.x + Vector2.down * doorSize.y;
        Vector2 R4 = (Vector2)transform.position + GetComponent<BoxCollider2D>().offset + Vector2.left * doorSize.x + Vector2.up * doorSize.y;

        Gizmos.DrawLine(R1, R3);
        Gizmos.DrawLine(R3, R2);
        Gizmos.DrawLine(R2, R4);
        Gizmos.DrawLine(R4, R1);
    }
}
