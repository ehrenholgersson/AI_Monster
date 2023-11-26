using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void AiTurn() 
    {
        if (GameController.main.Turn == this)
        {
            // decide on strategy (state)
                // weight each state based on factors (health, opponent health, buffs)
            // do Ai stuff
            PlayCards();
        }
    }

    void PlayCards()
    {

    }

}
