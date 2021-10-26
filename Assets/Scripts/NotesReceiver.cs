using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesReceiver
{
    static List<string> notesEasy = new List<string>() { "CN4", "CS4", "DN4", "DS4", "EN4", "FN4", "FS4", "GN4", "GS4", "AN4", "AS4", "BN4", "CN5" };
    static List<string> notesHard = new List<string>() { "CS5", "DN5", "DS5", "EN5", "FN5", "FS5", "GN5", "GS5", "AN5", "AS5", "BN5", "CN6" };

    public class Receiver
    {
        List<string> correctSequence;   // List of notes coming from the first player
        List<string> replySequence;     // List of "answer" notes

        // Put a note in the corresponding list
        // isReply: Incoming note = false, Reply note = true
        public void inputNote(string n, bool isReply)
        {
            if (noteIsValid(n))
            {
                if (!isReply)
                    correctSequence.Add(n);
                else
                    replySequence.Add(n);
            }
        }

        // Check validity of the note value `n` to prevent processing other object names
        public static bool noteIsValid(string n)
        {
            // Easy Mode: Check if the note is in Easy range
            if (!GameProperties.isHardMode)
                return notesEasy.Contains(n);

            // Hard Mode: Check if the note is in either range
            return notesEasy.Contains(n) || notesHard.Contains(n);
        }

        // Count the number of correct valid notes
        public int calculateScore()
        {
            int score = 0;
            while (correctSequence.Count > 0)
            {
                if (noteMatched(correctSequence[0], replySequence[0]))
                    ++score;
                if (replySequence.Count == 0)
                    break;
                correctSequence.RemoveAt(0);
                replySequence.RemoveAt(0);
            }
            return score;
        }

        // Check validity and correctness of the reply note
        private bool noteMatched(string correctNote, string replyNote)
        {
            return correctNote.Equals(replyNote) && noteIsValid(correctNote) && noteIsValid(replyNote);
        }
    }
}
