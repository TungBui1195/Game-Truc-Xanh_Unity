#define HARD
//#define NORMAL
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour
{
    public GameObject Button;
    public Transform Panel;
    GameObject btn;

#if HARD
    const int GAME_MODE = 20;
#elif NORMAL
    const int GAME_MODE = 16;
#else
    const int GAME_MODE = 12;
#endif
    void Awake()
    {
        for(int i = 0; i < GAME_MODE; i++)
        {
            btn = Instantiate(Button);
            btn.name = "" + i;
            btn.transform.SetParent(Panel, false); 
        }
    }
}
