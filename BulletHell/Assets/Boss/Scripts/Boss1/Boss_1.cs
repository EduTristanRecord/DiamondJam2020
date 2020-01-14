using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1 : Boss
{
    public P1_Boss_1 P1_B1;
    public P2_Boss_1 P2_B1;
    
   
    private void SwitchPhase()
    {
        switchPhase = true;
    }
    public void UpdatePhase(int dommage)
    {
        life -= dommage;
        if( life < lifeStats * maxLife)
        {
            SwitchPhase();
        }

    }
}
