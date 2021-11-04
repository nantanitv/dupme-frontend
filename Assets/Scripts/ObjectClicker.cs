using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class ObjectClicker : MonoBehaviour
{
    public AudioSource CN4;
    public AudioSource CS4;
    public AudioSource DN4;
    public AudioSource DS4;
    public AudioSource EN4;
    public AudioSource FN4;
    public AudioSource FS4;
    public AudioSource GN4;
    public AudioSource GS4;
    public AudioSource AN4;
    public AudioSource AS4;
    public AudioSource BN4;
    public AudioSource CN5;
    public AudioSource CS5;
    public AudioSource DN5;
    public AudioSource DS5;
    public AudioSource EN5;
    public AudioSource FN5;
    public AudioSource FS5;
    public AudioSource GN5;
    public AudioSource GS5;
    public AudioSource AN5;
    public AudioSource AS5;
    public AudioSource BN5;
    public AudioSource CN6;

    public string id;

    private void Start()
    {
        GameEvents.current.onNoteClick += OnNotePlay;
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

    public void OnNotePlay(string id)
    {
        PlayNote(id);
        GameSocketIO.EmitNote(id);
    }

    public void PlayNote(string id)
    {
        if (id == "CN4") CN4.Play();
        else if (id == "DN4") DN4.Play();
        else if (id == "DS4") DS4.Play();
        else if (id == "EN4") EN4.Play();
        else if (id == "CS4") CS4.Play();
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

        if (NotesReceiver.NoteIsValid(id))
        {
            Debug.Log("Valid Note");
            GameComponents.numKeys--;
            Debug.Log("[ObjClicker] Keys left: " + GameComponents.numKeys);
            Debug.Log($"[ObjClicker] Pressed {id}");
        }
    }
}
