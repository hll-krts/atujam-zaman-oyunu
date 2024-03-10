using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class ZPolyp : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float speed, spdPer = 1, hoverAltitude, invisCD, invisDuration;
    private float _timeMod;

    public LayerMask ignoreL;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        StartCoroutine(NoInvis());
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray LandingRay = new Ray(transform.position, Vector3.down);
        //Debug.DrawRay(transform.position, Vector3.down * hoverAltitude);

        if(Physics.Raycast(LandingRay, out hit, hoverAltitude, ~ignoreL) || transform.position.y < target.transform.position.y)
        {
            this.transform.position += new Vector3(0, 0.1f, 0);
        }
        else if(transform.position.y > target.transform.position.y) this.transform.position -= new Vector3(0, 0.1f, 0);

        //polyp'in karaktere yaklaþmasýný engelleyen if
        if (Vector3.Distance(transform.position, target.position) > 5f)
        {
            _timeMod = GetComponent<SpeedController>().thisObjectsTimeScale;
            this.transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime * _timeMod * spdPer);
        }
    }


    //Görünmezlik
    IEnumerator Invis()
    {
        StopCoroutine(NoInvis());
        this.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(invisDuration);
        StartCoroutine(NoInvis());
    }
    IEnumerator NoInvis()
    {
        StopCoroutine(Invis());
        this.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(invisCD);
        StartCoroutine(Invis());
    }
}
