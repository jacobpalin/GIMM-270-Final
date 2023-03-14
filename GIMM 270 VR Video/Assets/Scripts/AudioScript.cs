using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource SethAudio;
    public AudioSource CarsonAudio;
    public AudioSource JacobAudio;

    private bool loop1;
    private bool loop2;

    // Start is called before the first frame update
    void Start()
    {
        SethAudio = GameObject.FindWithTag("Seth").GetComponent<AudioSource>();
        CarsonAudio = GameObject.FindWithTag("Carson").GetComponent<AudioSource>();
        JacobAudio = GameObject.FindWithTag("Jacob").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!SethAudio.isPlaying && !loop1 && !JacobAudio.isPlaying)
        {
            Debug.Log("carson audio");
            CarsonAudio.Play();
            loop1 = true;
        }
        if (!SethAudio.isPlaying && !loop2 && !CarsonAudio.isPlaying)
        {
            Debug.Log("jacob audio");
            JacobAudio.Play();
            loop2 = true;
        }
    }
}
