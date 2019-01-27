using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    private Dictionary<string, int> messes = new Dictionary<string, int>();

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
    }
}
