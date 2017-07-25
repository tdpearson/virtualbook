using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;

public class BodySourceManager : MonoBehaviour
{
    private KinectSensor _Sensor;
    private BodyFrameReader _Reader;
    private Body[] _Data = null;

    private List<CustomGestureManager> gestureDetectorList = null;

    public Body[] GetData()
    {
        return _Data;
    }


    void Start()
    {
        _Sensor = KinectSensor.GetDefault();

        if (_Sensor != null)
        {
            if (!_Sensor.IsOpen)
            {
                _Sensor.Open();
            }
            
            _Reader = _Sensor.BodyFrameSource.OpenReader();

            gestureDetectorList = new List<CustomGestureManager>();

            // get maximum number of bodies the sensor can track
            int maxBodies = _Sensor.BodyFrameSource.BodyCount;

            for (int i = 0; i < maxBodies; ++i)
            {
                // TODO: assign each body to a gesturemanager
            }

        }
    }

    void Update()
    {
        if (_Reader != null)
        {
            var frame = _Reader.AcquireLatestFrame();
            if (frame != null)
            {
                if (_Data == null)
                {
                    _Data = new Body[_Sensor.BodyFrameSource.BodyCount];
                }

                frame.GetAndRefreshBodyData(_Data);

                frame.Dispose();
                frame = null;
            }
        }
    }

    void OnApplicationQuit()
    {
        if (_Reader != null)
        {
            _Reader.Dispose();
            _Reader = null;
        }

        if (_Sensor != null)
        {
            if (_Sensor.IsOpen)
            {
                _Sensor.Close();
            }

            _Sensor = null;
        }
    }
}