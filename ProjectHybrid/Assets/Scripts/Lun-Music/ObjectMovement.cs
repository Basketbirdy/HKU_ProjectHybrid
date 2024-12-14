using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class ObjectMovement : MonoBehaviour
{
    public CheckPos checkPos;
    public Camera cam;

    [Header ("Position Parts")]
    public Transform beginPos;
    public Transform top, right;
    public float topDistance, rightDistance;

    [Header("Audio Parts")]
    public EventReference audioBit;
    private EventInstance sound;
    public string parameterVolume, parameterOther;

    void Start()
    {
        transform.position = beginPos.position;
        sound = AudioManager.instance.CreateEventInstance(audioBit);
    }


    void Update()
    {
        
    }

    public void OnMouseDrag()
    {
        // when dragging make sure the object follows the cursor
        // dont question the 9.5f i dont know either man but this is how it works 
        transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.5f));
        
        CheckDistances();
        CheckSound();
    }

    public void OnMouseUp()
    {
        // check if valid position
        if (checkPos.inValidPos == false)
        {
            ReturnToBegin();
        }
        else
        {
            CheckDistances();
        }

        CheckSound();
    }

    private void ReturnToBegin()
    {
        transform.position = beginPos.position;
    }

    private void CheckSound()
    {
        if (checkPos.inValidPos)
        {
            PLAYBACK_STATE playbackState;
            sound.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                sound.start();
            }
        }
        else if (!checkPos.inValidPos)
        {
            sound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    private void CheckDistances()
    {
        // return values between 0 and 10, 0 being at the top/right
        topDistance = top.position.z - transform.position.z;
        rightDistance = right.position.x - transform.position.x;

        ChangeSound();
    }

    private void ChangeSound()
    {
        // change the sound depends on how far away they are from the top and right
        AudioManager.instance.SetSoundParameter(sound, parameterVolume, topDistance); // top changes volume
        AudioManager.instance.SetSoundParameter(sound, parameterOther, rightDistance); // right changes chorus
    }
}
