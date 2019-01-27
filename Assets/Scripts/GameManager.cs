using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Image fadeImage;

    private Dictionary<string, int> messes = new Dictionary<string, int>();

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance)
            {
                return instance;
            }
            else
            {
                return null;
            }
        }
    }

    public bool inputBlocked = false;

    public void AddMess(string messType)
    {
        if (!messes.ContainsKey(messType))
        {
            messes[messType] = 1;
        }
        else
        {
            ++messes[messType];
        }
    }

    public void RemoveMess(string messType)
    {
        if (!messes.ContainsKey(messType))
        {
            Debug.LogError("No mess of " + messType + " exists!");
        }
        else
        {
            --messes[messType];
            CheckGameState();
        }
    }

    void CheckGameState()
    {
        bool noMessesRemaining = true;
        foreach (KeyValuePair<string, int> mess in messes)
        {
            noMessesRemaining = noMessesRemaining && mess.Value == 0;
        }

        if (noMessesRemaining)
        {
            // TODO: End game!
            Debug.Log("All messes are gone!");
        }
    }

    void Awake()
    {
        if (instance)
        {
            Debug.Log("Trying to instantiate another instance! Can't do that.");
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += BeginLevel;
    }

    void BeginLevel(Scene scene, LoadSceneMode loadSceneMode)
    {
        StartCoroutine(LevelStartupSequence());
    }

    IEnumerator LevelStartupSequence()
    {
        GameObject instructions = GameObject.FindGameObjectWithTag("Instructions");

        while (fadeImage && fadeImage.color.a > .005)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.b, fadeImage.color.g, fadeImage.color.a - .0075f);
            yield return null;
        }

        if (!instructions)
        {
            Debug.LogError("Something's wrong, we can't find the instructions UI");
        }
        else
        {
            NotesUI notesUI = instructions.GetComponent<NotesUI>();
            if (notesUI)
            {
                notesUI.OpenNoteCard();
            }
        }
    }
}
