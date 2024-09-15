using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScripts : MonoBehaviour
{
    [SerializeField] float AnimTime;
    [SerializeField] AudioClip _clip;

    void Start()
    {
        AudioSource.PlayClipAtPoint(_clip, gameObject.transform.position);
        Invoke("Destroyed", AnimTime);
    }

    void Destroyed()
    {
        Destroy(gameObject);
    }
}
