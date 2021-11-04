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

    public AudioSource PCN4;
    public AudioSource PCS4;
    public AudioSource PDN4;
    public AudioSource PDS4;
    public AudioSource PEN4;
    public AudioSource PFN4;
    public AudioSource PFS4;
    public AudioSource PGN4;
    public AudioSource PGS4;
    public AudioSource PAN4;
    public AudioSource PAS4;
    public AudioSource PBN4;
    public AudioSource PCN5;
    public AudioSource PCS5;
    public AudioSource PDN5;
    public AudioSource PDS5;
    public AudioSource PEN5;
    public AudioSource PFN5;
    public AudioSource PFS5;
    public AudioSource PGN5;
    public AudioSource PGS5;
    public AudioSource PAN5;
    public AudioSource PAS5;
    public AudioSource PBN5;
    public AudioSource PCN6;

    public AudioSource ECN4;
    public AudioSource ECS4;
    public AudioSource EDN4;
    public AudioSource EDS4;
    public AudioSource EEN4;
    public AudioSource EFN4;
    public AudioSource EFS4;
    public AudioSource EGN4;
    public AudioSource EGS4;
    public AudioSource EAN4;
    public AudioSource EAS4;
    public AudioSource EBN4;
    public AudioSource ECN5;
    public AudioSource ECS5;
    public AudioSource EDN5;
    public AudioSource EDS5;
    public AudioSource EEN5;
    public AudioSource EFN5;
    public AudioSource EFS5;
    public AudioSource EGN5;
    public AudioSource EGS5;
    public AudioSource EAN5;
    public AudioSource EAS5;
    public AudioSource EBN5;
    public AudioSource ECN6;

    public enum Voice
    {
        Piano, Epiano, Putter
    }

    public Voice voice;

    public string id;

    private void Start()
    {
        voice = Voice.Piano;
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
        if (voice == Voice.Piano)
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
        }
        else if (voice == Voice.Putter)
        {
            if (id == "CN4") PCN4.Play();
            else if (id == "DN4") PDN4.Play();
            else if (id == "DS4") PDS4.Play();
            else if (id == "EN4") PEN4.Play();
            else if (id == "CS4") PCS4.Play();
            else if (id == "FN4") PFN4.Play();
            else if (id == "FS4") PFS4.Play();
            else if (id == "GN4") PGN4.Play();
            else if (id == "GS4") PGS4.Play();
            else if (id == "AN4") PAN4.Play();
            else if (id == "AS4") PAS4.Play();
            else if (id == "BN4") PBN4.Play();

            else if (id == "CN5") PCN5.Play();
            else if (id == "CS5") PCS5.Play();
            else if (id == "DN5") PDN5.Play();
            else if (id == "DS5") PDS5.Play();
            else if (id == "EN5") PEN5.Play();
            else if (id == "FN5") PFN5.Play();
            else if (id == "FS5") PFS5.Play();
            else if (id == "GN5") PGN5.Play();
            else if (id == "GS5") PGS5.Play();
            else if (id == "AN5") PAN5.Play();
            else if (id == "AS5") PAS5.Play();
            else if (id == "BN5") PBN5.Play();
            else if (id == "CN6") PCN6.Play();
        }
        else if (voice == Voice.Epiano)
        {
            if (id == "CN4") ECN4.Play();
            else if (id == "DN4") EDN4.Play();
            else if (id == "DS4") EDS4.Play();
            else if (id == "EN4") EEN4.Play();
            else if (id == "CS4") ECS4.Play();
            else if (id == "FN4") EFN4.Play();
            else if (id == "FS4") EFS4.Play();
            else if (id == "GN4") EGN4.Play();
            else if (id == "GS4") EGS4.Play();
            else if (id == "AN4") EAN4.Play();
            else if (id == "AS4") EAS4.Play();
            else if (id == "BN4") EBN4.Play();

            else if (id == "CN5") ECN5.Play();
            else if (id == "CS5") ECS5.Play();
            else if (id == "DN5") EDN5.Play();
            else if (id == "DS5") EDS5.Play();
            else if (id == "EN5") EEN5.Play();
            else if (id == "FN5") EFN5.Play();
            else if (id == "FS5") EFS5.Play();
            else if (id == "GN5") EGN5.Play();
            else if (id == "GS5") EGS5.Play();
            else if (id == "AN5") EAN5.Play();
            else if (id == "AS5") EAS5.Play();
            else if (id == "BN5") EBN5.Play();
            else if (id == "CN6") ECN6.Play();
        }

        Debug.Log("Valid Note");
        GameComponents.numKeys--;
        Debug.Log("[ObjClicker] Keys left: " + GameComponents.numKeys);
        Debug.Log($"[ObjClicker] Pressed {id}");
        if (!GameComponents.meGoesFirst) NotesReceiver.InputNote(id, true);
    }

    public void ChangeToPiano()
    {
        voice = Voice.Piano;
    }

    public void ChangeToPutter()
    {
        voice = Voice.Putter;
    }

    public void ChangeToEpiano()
    {
        voice = Voice.Epiano;
    }
}
