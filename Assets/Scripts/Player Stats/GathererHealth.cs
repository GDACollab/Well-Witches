using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GathererHealth : MonoBehaviour
{
    //fields
    
    public void ChangeHealth(int amount) {
        StatsManager.Instance.GathererCurrentHealth += amount;
    }

}
