using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesUI : MonoBehaviour
{
    public GameObject notesIcon;
    public GameObject noteCard;

    private bool notesOpen = false;
    
    // Start is called before the first frame update
    void Start()
    {
        notesIcon.SetActive(true);
        noteCard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!notesOpen && Input.GetButtonDown("Menu"))
        {
            OpenNoteCard();
            notesOpen = true;
        }
        else if (notesOpen && Input.GetButtonDown("Menu"))
        {
            CloseNoteCard();
            notesOpen = false;
        }
    }

    public void CloseNoteCard()
    {
        noteCard.SetActive(false);
        notesIcon.SetActive(true);
    }

    public void OpenNoteCard()
    {
        noteCard.SetActive(true);
        notesIcon.SetActive(false);
    }
}
