using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    private Dictionary<GameObject, GameObject> playerMatches = new Dictionary<GameObject, GameObject>();
    public List<GameObject> questions; // Assigned in Inspector
    public List<GameObject> answers;   // Assigned in Inspector
    public PlayerMovement player;
    public string abilityminigame;
    private Dictionary<GameObject, GameObject> correctMatches = new Dictionary<GameObject, GameObject>();

    void Start()
    {
        // Set correct pairs at the start
        if (questions.Count == answers.Count)
        {
            for (int i = 0; i < questions.Count; i++)
            {
                correctMatches.Add(questions[i], answers[i]);
            }
        }
    }
    public void AddMatch(GameObject ques, GameObject ans)
    {
        if (playerMatches.ContainsKey(ques))
        {
            playerMatches[ques] = ans; // 🔄 Update existing match
        }
        else
        {
            playerMatches.Add(ques, ans);
        }

        if (playerMatches.Count == correctMatches.Count) // ✅ Check only when all are matched
        {
            CheckMatches();
        }
    }


    private void CheckIfAllMatched()
    {
        if (playerMatches.Count == questions.Count) // All "ques" are matched
        {
            Debug.Log("All Matched! Checking result...");
            CheckMatches();
        }
    }

    public void CheckMatches()
    {
        foreach (var pair in correctMatches)
        {
            GameObject correctQues = pair.Key;
            GameObject correctAns = pair.Value;

            if (!playerMatches.ContainsKey(correctQues) || playerMatches[correctQues].name != correctAns.name)
            {
                Debug.Log("Loss");
                ResetLines(); // 🚫 Clear incorrect matches
                return;
            }
        }

        Debug.Log("Win"); // 🎉 If all matches are correct
        if (abilityminigame == "Interact")
        {
            player.canInteract = true;
        }
        else if (abilityminigame == "Jump")
        {
            player.canJump = true;
        }

    }

    public bool IsQuesMatched(GameObject ques)
    {
        return playerMatches.ContainsKey(ques); // ✅ Checks if ques is already matched
    }

    public void ResetLines()
    {
        foreach (var line in FindObjectsOfType<DrawLine>()) // ✅ Find all line objects
        {
            Destroy(line.gameObject); // 🚫 Delete the line
        }
        playerMatches.Clear(); // 🗑 Clear all stored matches
    }

}