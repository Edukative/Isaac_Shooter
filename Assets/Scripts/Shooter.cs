using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    public Animation shooterAnim;

    public AnimationClip[] shooterAnims; // list of anim clips

    [SerializeField]
    private AudioSource shooterAudio;

    public AudioClip[] shooterSounds; // list of sounds clips
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray disparo = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit[] hitInfoArray; // a list of all the objects the ray has collided with
        RaycastHit hit; // an object collided by the ray

        hitInfoArray = Physics.RaycastAll(disparo); // passing all the info

        if (Physics.Raycast(disparo, out hit))
        {
            if (hit.collider != null)
            {
                Debug.Log(hit);
            }
        }

        shooterAnim.clip = shooterAnims[0];
        shooterAnim.Play();
        shooterAudio.clip = shooterSounds[0];
        shooterAudio.Play();
    }
}
