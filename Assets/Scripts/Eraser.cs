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

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButton("Action")) && eraseable)
        {
            var tilePos = map.WorldToCell(player.transform.position);
            if (map.GetTile(tilePos) !=null)
                map.SetTile(tilePos, null);
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
