using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : AllCharacters
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DikenOt"))
        {
            StartCoroutine(DotDamage());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DikenOt"))
        {
            StopCoroutine(DotDamage());
        }
    }

    IEnumerator DotDamage()
    {
        Debug.Log("Damaj dealt");
        yield return new WaitForSeconds(1);
    }
}
