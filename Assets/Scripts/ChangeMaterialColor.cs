using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialColor : MonoBehaviour
{
    [SerializeField] private Renderer myObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !(PauseMenu.IsPaused))
        {
            hitChangeColor(Color.red);
        } else if (Input.GetMouseButtonUp(0))
        {
            if(myObject.gameObject.transform.parent.name == "Blacc keys")
            {
                hitChangeColor(Color.black);
                myObject.material.color = Color.black;
            } else if (myObject.gameObject.transform.parent.name == "White keys")
            {
                hitChangeColor(Color.white);
                myObject.material.color = Color.white;
            }
            
        }
    }

    private void hitChangeColor(Color color)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (myObject.gameObject.name == hit.transform.gameObject.name)
            {
                //print(hit.transform.gameObject.name);
                this.myObject.material.color = color;
            }
        }
        //playNote(myObject.gameObject);
    }

    

}
