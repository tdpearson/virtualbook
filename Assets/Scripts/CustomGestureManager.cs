using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using Microsoft.Kinect.VisualGestureBuilder;

public class CustomGestureManager : MonoBehaviour
{
    VisualGestureBuilderDatabase _gestureDatabase;
    VisualGestureBuilderFrameSource _gestureFrameSource;
    VisualGestureBuilderFrameReader _gestureFrameReader;
    KinectSensor _kinect;
    Gesture _forward;
    Gesture _back;

    enum pageflip {Forward, Back}

    private bool allowPageTurn = true;        // must be true or will block gesture detection
    public float throttlePageTurns = 1.5f;    // time to throttle in seconds
    public float confidenceThreshold = 0.98f; // confidence level (percent) needed to trigger detection
    public MegaBookBuilder book;              // book object to controll
    public string gesturefile = "HandWaves.gbd";
    public string gestureforward = "Hand_Right";
    public string gestureback = "Hand_Left";

    public void SetTrackingId(ulong id)
    {
        _gestureFrameReader.IsPaused = false;
        _gestureFrameSource.TrackingId = id;
        _gestureFrameReader.FrameArrived += _gestureFrameReader_FrameArrived;
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log("Started...");
        _kinect = KinectSensor.GetDefault();

        Debug.Log("Accessing Gesture Database: " + Application.streamingAssetsPath + "/GestureDBs/" + gesturefile);
        _gestureDatabase = VisualGestureBuilderDatabase.Create(Application.streamingAssetsPath + "/GestureDBs/" + gesturefile);
        _gestureFrameSource = VisualGestureBuilderFrameSource.Create(_kinect, 0);

        foreach (var gesture in _gestureDatabase.AvailableGestures)
        {
            Debug.Log("Found gesture: " + gesture.Name);
            _gestureFrameSource.AddGesture(gesture);

            if (gesture.Name == gestureforward)
            {
                Debug.Log("Assigning forward gesture");
                _forward = gesture;
            }
            if (gesture.Name == gestureback)
            {
                Debug.Log("Assigning back gesture");
                _back = gesture;
            }
        }

        _gestureFrameReader = _gestureFrameSource.OpenReader();
        _gestureFrameReader.IsPaused = true;
    }

    void _gestureFrameReader_FrameArrived(object sender, VisualGestureBuilderFrameArrivedEventArgs e)
    {
        VisualGestureBuilderFrameReference frameReference = e.FrameReference;
        using (VisualGestureBuilderFrame frame = frameReference.AcquireFrame())
        {
            if (frame != null && frame.DiscreteGestureResults != null)
            {
                if (frame.DiscreteGestureResults.Count > 0)
                {
                    if (!allowPageTurn) return;
                    DiscreteGestureResult forwardresult = frame.DiscreteGestureResults[_forward];
                    DiscreteGestureResult backresult = frame.DiscreteGestureResults[_back];
                    if ((forwardresult == null) && (backresult == null)) return;
                    if (forwardresult.Detected == true)
                    {
                        if (forwardresult.Confidence < confidenceThreshold) return;  // skip if too low on confidence

                        Debug.Log("Foward gesture detected");
                        StartCoroutine(flippage(pageflip.Forward));
                    }
                    else if (backresult.Detected == true)
                    {
                        if (backresult.Confidence < confidenceThreshold) return;  // skip if too low on confidence

                        Debug.Log("Back gesture detected");
                        StartCoroutine(flippage(pageflip.Back));
                    }
                }
            }
        }
    }

    IEnumerator flippage(pageflip direction)
    {
        allowPageTurn = false;
        // perform animation
        Debug.Log("Trigger flip page...");

        if (direction == pageflip.Forward)
        {
            Debug.Log("Flipping page Forward");
            book.NextPage();
        }
        else if (direction == pageflip.Back)
            {
            Debug.Log("Flipping page Back");
            book.PrevPage();
        }

        yield return new WaitForSeconds(throttlePageTurns);
        allowPageTurn = true;
    }
}