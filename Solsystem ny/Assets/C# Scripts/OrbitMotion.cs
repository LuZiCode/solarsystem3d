using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMotion : MonoBehaviour
{

    public Transform orbitingObject;
    public Ellipse orbitPath;

    //Sørger for vi altid er mellem de to numre i orbitten
    [Range(0f, 1f)]
    //Basically hvor langt henne af vejen af orbitten er jeg
    public float orbitProgress = 0f;
    //Hvor langtid det tager i sekunder for planeten at gå hele vejen rundt om orbitten 1 gang
    public float orbitPeriod = 3f;
    //Lavet til hvis jeg vil fokusere på planeten, så kan jeg slå false til, og så vil den pause platten i orbitten.
    public bool orbitActive = true;

    void Start()
    {
        //Hvis der ingen orbit er, så kør denne
        if (orbitingObject == null)
        {
            //Sæt orbitactive til false (stop det)
            orbitActive = false;
            return;
        }
        //Hvis der er en orbit object, så gå til denne return type SetOrbitingObjectPosition

        SetOrbitingObjectPosition ();
        StartCoroutine(AnimateOrbit ());
    }

    void SetOrbitingObjectPosition()
    {
        Vector2 orbitPos = orbitPath.Evaluate(orbitProgress);
        orbitingObject.localPosition = new Vector3(orbitPos.x, 0, orbitPos.y);
    }

    IEnumerator AnimateOrbit()
    {
        //Hvis orbitperiod er mindre end 0, så kør denne
        if (orbitPeriod < 0.1f)
        {
            //Sætter orbitperiod til det mindst tilladte
            orbitPeriod = 0.1f;
        }
        float orbitSpeed = 1f / orbitPeriod;

        //Kør denne for hver frame orbit er aktiv.
        while (orbitActive == true)
        {
            orbitProgress += Time.deltaTime * orbitSpeed;
            //Hvis orbit progress overskrider 1, så går tilbage til et, så man ikke risikerer floaten til at blive for stor
            orbitProgress %= 1f;
            SetOrbitingObjectPosition ();
            yield return null;
        }
    }

}
