using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{

    Image background;
    public Sprite sprite, sprite1, sprite2;
    IEnumerator_Survive ies;

    // Start is called before the first frame update
    void Start()
    {
        ies = FindObjectOfType<IEnumerator_Survive>();
        background = GetComponent<Image>();
    }

    // Update is called once per frame

    private void Update()
    {



        if (ies.population > 0 && ies.population < 10)
        {
            //MjenjajSlike(0);

            background.sprite = sprite;
         
        }
        else if (ies.population >= 10 && ies.population < 100)
        {
            //	MjenjajSlike(1);
            background.sprite = sprite1;
           
        }
        else if (ies.population >= 100 && ies.population < 500)
        {
            //	MjenjajSlike(2);
            
            background.sprite = sprite2;

        }
    }
}
