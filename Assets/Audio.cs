using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource source;
    
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void OnCollisionEnter2D (Collision2D coll) {
        source.Play();
    }

}