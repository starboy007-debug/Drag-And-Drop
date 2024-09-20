using UnityEngine;
using System;

public class UMoveToTarget : MonoBehaviour
{
    const float default_speed = 5.0f;
    const float default_arrival_threshold = 0.08f;

    public float arrivalThreshold = default_arrival_threshold;
    public float speed = default_speed;

    public Func<UMoveToTarget, bool> OnArrival = delegate { return true; };

    Vector3 destination;
    bool inMotion = false;

    public static UMoveToTarget Go(GameObject toMove, Vector3 pos, float arrivalThreshold = default_arrival_threshold, float speed = default_speed)
    {
        UMoveToTarget moveTo = toMove.GetComponent<UMoveToTarget>() ?? toMove.AddComponent<UMoveToTarget>();
        moveTo.arrivalThreshold = arrivalThreshold;
        moveTo.speed = speed;
        moveTo.Go(pos);
        return moveTo;
    }

    public void Go(Vector3 pos)
    {
        if (Vector3.Distance(transform.position, pos) > arrivalThreshold)
        {
            destination = pos;
            inMotion = true;
        }
    }

    void Update()
    {
        if (inMotion)
        {
            float t = Mathf.Clamp01(speed * Time.fixedDeltaTime);
            t = t * t * (3f - 2f * t);
            transform.position = Vector3.Lerp(transform.position, destination, t);

            if (Vector3.Distance(transform.position, destination) < arrivalThreshold)
            {
                transform.position = destination;
                inMotion = false;
                if (OnArrival(this))
                    Destroy(this);
            }
        }
    }
}
