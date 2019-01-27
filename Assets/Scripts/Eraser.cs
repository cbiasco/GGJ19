using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Eraser : MonoBehaviour
{
    public Transform player;
    private Tilemap map;
    private bool eraseable= false;
    // Start is called before the first frame update
    void Start()
    {
        map = GetComponent<Tilemap>();
    }

    void Update()
    {
        if (Input.GetButton("Action") && eraseable)
        {
            var tilePos = map.WorldToCell(player.transform.position);
            if (map.HasTile(tilePos))
            {
                Sprite s = map.GetSprite(tilePos);
                Debug.Log(s.name);
                map.SetTile(tilePos, null);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            eraseable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            eraseable = false;
        }
    }
}
