using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroy.instance.gameObject.GetComponent<AudioSource>().Stop();
    }
}
