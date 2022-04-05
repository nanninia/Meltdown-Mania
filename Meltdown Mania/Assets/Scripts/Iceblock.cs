using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IceState
{
    Fine,
    Melting,
    Melted
}
public class Iceblock : MonoBehaviour
{
    public IceState currentState;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        currentState = IceState.Fine;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
