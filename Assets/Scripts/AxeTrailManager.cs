using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTrailManager : MonoBehaviour
{

    public ParticleSystem trailFx;
    Transform trailParent;
    Vector3 initialPosition;
    Quaternion initialRotation;
    Vector3 initialScale;

    private void Start()
    {
        trailFx.Stop();
        trailParent = trailFx.gameObject.transform.parent;
        initialPosition = trailFx.transform.position;
        initialRotation = trailFx.transform.rotation;
        initialScale = trailFx.transform.localScale;
    }

    private void Update()
    {
        print(initialPosition);
        print(initialRotation);
        print(initialScale);
    }

    void StartTrail()
    {
        trailFx.Play();
        trailFx.transform.SetParent(trailParent);
        trailFx.transform.position = initialPosition;
        trailFx.transform.rotation = initialRotation;
        trailFx.transform.localScale = initialScale;
    }

    void EndTrail()
    {
        trailFx.Stop();
        trailFx.transform.SetParent(gameObject.transform);
    }
}
