using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CutFx : MonoBehaviour
{

    PostProcessVolume CutProcessVolume;
    public AnimationCurve ProcessCurve;
    public float curveLength;
    public float slowMoFactor;
    public float maxSlowMoLength;
    float time;
    float initialFixedDeltaTime;

    void Start()
    {
        CutProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        initialFixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {

    }

    public IEnumerator OnTreeCut()
    {
        //début du slow mo
        Time.timeScale = slowMoFactor;
        Time.fixedDeltaTime = initialFixedDeltaTime * Time.timeScale;
        while (time <= curveLength)
        {
            //update du weight du post process
            CutProcessVolume.weight = ProcessCurve.Evaluate(time);
            time += Time.deltaTime;
            if (time >= maxSlowMoLength)
            {
                //retour smooth du temps à la normale
                Time.timeScale = slowMoFactor + (1 - slowMoFactor) * ((curveLength - time) / (curveLength - maxSlowMoLength));
                Time.fixedDeltaTime = initialFixedDeltaTime * Time.timeScale;
            }
            yield return null;
        }
        Time.timeScale = 1f;
        time = 0f;
        CutProcessVolume.weight = 0;
        yield break;
    }
}
