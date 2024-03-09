using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    
    public static float _timeModifier=1f;

    public void SetTimeModifier(float modifier) 
    {
        _timeModifier =modifier;
    }

    public float GetTimeModifier()
    {
        return _timeModifier;
    }
}
