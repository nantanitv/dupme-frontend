using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesReceiver
{
    static readonly List<string> notesEasy = new List<string>() { "CN4", "CS4", "DN4", "DS4", "EN4", "FN4", "FS4", "GN4", "GS4", "AN4", "AS4", "BN4", "CN5" };
    static readonly List<string> notesHard = new List<string>() { "CS5", "DN5", "DS5", "EN5", "FN5", "FS5", "GN5", "GS5", "AN5", "AS5", "BN5", "CN6" };
    static List<string> correctSequence;   // List of notes coming from the first player
    static List<string> replySequence;     // List of "answer" notes

    public static ObjectClicker oc;

    public static void ResetSequences()
    {
        correctSequence = new List<string>();
        replySequence = new List<string>();
    }

    // Put a note in the corresponding list
    // isReply: Incoming note = false, Reply note = true
    public static void InputNote(string n, bool isReply)
    {
        Debug.Log("[InputNote] Started");
        // if (NoteIsValid(n))
        // {
            Debug.Log("[InputNote] " + n);
            if (!isReply) correctSequence.Add(n);
            else replySequence.Add(n);
        // }
    }

    // Check validity of the note value `n` to prevent processing other object names
    public static bool NoteIsValid(string n)
    {
        // Easy Mode: Check if the note is in Easy range
        if (!GameProperties.isHardMode) return notesEasy.Contains(n);

        // Hard Mode: Check if the note is in either range
        return notesEasy.Contains(n) || notesHard.Contains(n);
    }

    // Count the number of correct valid notes
    public static int CalculateScore()
    {
        Debug.Log("Calculating Score");
        int score = 0;
        while (correctSequence.Count > 0 && replySequence.Count > 0)
        {
            Debug.Log("Loop");
            if (NoteMatched(correctSequence[0], replySequence[0]))
            {
                ++score;
                Debug.Log("Matched: " + score);
            }
            correctSequence.RemoveAt(0);
            Debug.Log("Removed Correct");
            replySequence.RemoveAt(0);
            Debug.Log("Removed Reply");
        }
        Debug.Log("Calculated score");
        return score;
    }

    // Check validity and correctness of the reply note
    private static bool NoteMatched(string correctNote, string replyNote)
    {
        return correctNote.Equals(replyNote) && NoteIsValid(correctNote) && NoteIsValid(replyNote);
    }
}
