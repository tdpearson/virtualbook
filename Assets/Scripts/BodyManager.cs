using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class BodyManager : MonoBehaviour {

    public GameObject BodySrcManager;
    private BodySourceManager bodyManager;
    private Body[] bodies;
    public CustomGestureManager GestureManager;

    void Start()
    {
        if (BodySrcManager != null)
        {
            bodyManager = BodySrcManager.GetComponent<BodySourceManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bodyManager == null)
        {
            Debug.Log("No Body Manager");
            return;
        }
        bodies = bodyManager.GetData();

        if (bodies == null)
        {
            return;
        }
        foreach (var body in bodies)
        {
            if (body == null)
            {
                continue;
            }
            if (body.IsTracked)
            {
                // FIXME: This causes memory leak!
                if (GestureManager != null)
                {
                    GestureManager.SetTrackingId(body.TrackingId);
                }
            }
        }
    }
}
