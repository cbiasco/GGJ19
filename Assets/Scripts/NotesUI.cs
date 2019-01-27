using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesUI : MonoBehaviour
{
    public GameObject notesIcon;
    public GameObject noteCard;

    private Vector2 iconStart;
    private Vector2 cardStart;

    private bool notesOpen = false;
    
    // Start is called before the first frame update
    void Start()
    {
        notesIcon.SetActive(true);
        noteCard.SetActive(false);

        iconStart = notesIcon.transform.position;
        cardStart = noteCard.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!notesOpen && Input.GetButtonDown("Menu"))
        {
            OpenNoteCard();
        }
        else if (notesOpen && Input.GetButtonDown("Menu"))
        {
            CloseNoteCard();
        }
    }

    public void CloseNoteCard()
    {
        noteCard.SetActive(false);
        notesIcon.SetActive(true);
        notesOpen = false;
    }

    public void OpenNoteCard()
    {
        noteCard.SetActive(true);
        notesIcon.SetActive(false);
        notesOpen = true;
    }
}
