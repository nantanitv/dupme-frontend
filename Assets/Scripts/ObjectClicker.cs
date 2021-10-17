using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    public AudioSource C4;
    public AudioSource CS4;
    public AudioSource D4;
    public AudioSource DS4;
    public AudioSource E4;
    public AudioSource F4;
    public AudioSource FS4;
    public AudioSource G4;
    public AudioSource GS4;
    public AudioSource A4;
    public AudioSource AS4;
    public AudioSource B4;
    public AudioSource C5;
    public AudioSource CS5;
    public AudioSource D5;
    public AudioSource DS5;
    public AudioSource E5;
    public AudioSource F5;
    public AudioSource FS5;
    public AudioSource G5;
    public AudioSource GS5;
    public AudioSource A5;
    public AudioSource AS5;
    public AudioSource B5;
    public AudioSource C6;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform)
                {
                    playNote(hit.transform.gameObject);
                }
            }
        }
    }

    private void playNote(GameObject key)
    {
        if (key.name == "C4") C4.Play();
        else if (key.name == "C-sharp 4") CS4.Play();
        else if (key.name == "D4") D4.Play();
        else if (key.name == "D-sharp 4") DS4.Play();
        else if (key.name == "E4") E4.Play();
        else if (key.name == "F4") F4.Play();
        else if (key.name == "F-sharp 4") FS4.Play();
        else if (key.name == "G4") G4.Play();
        else if (key.name == "G-sharp 4") GS4.Play();
        else if (key.name == "A4") A4.Play();
        else if (key.name == "A-sharp 4") AS4.Play();
        else if (key.name == "B4") B4.Play();

        else if (key.name == "C5") C5.Play();
        else if (key.name == "C-sharp 5") CS5.Play();
        else if (key.name == "D5") D5.Play();
        else if (key.name == "D-sharp 5") DS5.Play();
        else if (key.name == "E5") E5.Play();
        else if (key.name == "F5") F5.Play();
        else if (key.name == "F-sharp 5") FS5.Play();
        else if (key.name == "G5") G5.Play();
        else if (key.name == "G-sharp 5") GS5.Play();
        else if (key.name == "A5") A5.Play();
        else if (key.name == "A-sharp 5") AS5.Play();
        else if (key.name == "B5") B5.Play();
        else if (key.name == "C6") C6.Play();
    }
}
