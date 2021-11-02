using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class ObjectClicker : MonoBehaviour
{
    public static AudioSource CN4;
    public static AudioSource CS4;
    public static AudioSource DN4;
    public static AudioSource DS4;
    public static AudioSource EN4;
    public static AudioSource FN4;
    public static AudioSource FS4;
    public static AudioSource GN4;
    public static AudioSource GS4;
    public static AudioSource AN4;
    public static AudioSource AS4;
    public static AudioSource BN4;
    public static AudioSource CN5;
    public static AudioSource CS5;
    public static AudioSource DN5;
    public static AudioSource DS5;
    public static AudioSource EN5;
    public static AudioSource FN5;
    public static AudioSource FS5;
    public static AudioSource GN5;
    public static AudioSource GS5;
    public static AudioSource AN5;
    public static AudioSource AS5;
    public static AudioSource BN5;
    public static AudioSource CN6;

    public string id;

    private void Start()
    {
        GameEvents.current.onNoteClick += onNotePlay;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !(PauseMenu.IsPaused) && GameComponents.mePlayable)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
                if (hit.transform) GameEvents.current.NoteClick(hit.transform.gameObject.name);
        }
    }

    public static void onNotePlay(string id)
    {
        #region Play note sound
        if (id == "CN4") CN4.Play();
        else if (id == "CS4") CS4.Play();
        else if (id == "DN4") DN4.Play();
        else if (id == "DS4") DS4.Play();
        else if (id == "EN4") EN4.Play();
        else if (id == "FN4") FN4.Play();
        else if (id == "FS4") FS4.Play();
        else if (id == "GN4") GN4.Play();
        else if (id == "GS4") GS4.Play();
        else if (id == "AN4") AN4.Play();
        else if (id == "AS4") AS4.Play();
        else if (id == "BN4") BN4.Play();

        else if (id == "CN5") CN5.Play();
        else if (id == "CS5") CS5.Play();
        else if (id == "DN5") DN5.Play();
        else if (id == "DS5") DS5.Play();
        else if (id == "EN5") EN5.Play();
        else if (id == "FN5") FN5.Play();
        else if (id == "FS5") FS5.Play();
        else if (id == "GN5") GN5.Play();
        else if (id == "GS5") GS5.Play();
        else if (id == "AN5") AN5.Play();
        else if (id == "AS5") AS5.Play();
        else if (id == "BN5") BN5.Play();
        else if (id == "CN6") CN6.Play();
        #endregion

        // GameSocket.SendNote(id);
        NotesReceiver.InputNote(id, false);
        // Debug.Log("[ObjClicker] Round: " + GameComponents.currentRound);
        GameComponents.numKeys--;
        Debug.Log("[ObjClicker] Keys left: " + GameComponents.numKeys);
    }
}
