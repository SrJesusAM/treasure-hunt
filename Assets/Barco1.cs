using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Barco1 : MonoBehaviour, ITrackableEventHandler {

    // Variables
    protected TrackableBehaviour mTrackableBehaviour;


    void Start() {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }


    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,   TrackableBehaviour.Status newStatus) {

        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
            print("DETECTA");
        } else if (previousStatus == TrackableBehaviour.Status.TRACKED && newStatus == TrackableBehaviour.Status.NO_POSE) {
            print("PIERDE");
        } else {
            print("COMIENZO");
        }
    }

}
