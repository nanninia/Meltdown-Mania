using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCrystal : MonoBehaviour
{
    private Iceblock[] iceblocks;
    private List<Iceblock> meltedIce = new List<Iceblock>();
    public float lifeTime = 5.0f;
    public int restoreIce;
    public Sprite iceSprite;

    private void Awake()
    {
        iceblocks = FindObjectsOfType<Iceblock>();
        Destroy(this.gameObject, lifeTime);
    }
    private void Update()
    {
        foreach (Iceblock i in iceblocks)
        {
            if(i.currentState == IceState.Melted && !meltedIce.Contains(i))
            {
                meltedIce.Add(i);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RestoreIce();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Iceblock>() != null)
        {
            Iceblock iceData = collision.gameObject.GetComponent<Iceblock>();
            if (iceData.currentState == IceState.Melted)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void RestoreIce()
    {
        if (meltedIce.Count >= restoreIce)
        {
            for (int i = 0; i < restoreIce; i++)
            {
                Iceblock ice = meltedIce[Random.Range(0, meltedIce.Count)];
                ice.currentState = IceState.Fine;
                Animator iceAnim = ice.transform.gameObject.GetComponent<Animator>();
                SpriteRenderer iceSr = ice.transform.gameObject.GetComponent<SpriteRenderer>();
                iceSr.sprite = iceSprite;
                iceAnim.enabled = false;
                iceAnim.enabled = true;
                iceAnim.enabled = false;
                
            }
        }
        else if (meltedIce.Count > 0)
        {
            for (int i = 0; i < meltedIce.Count; i++)
            {
                Iceblock ice = meltedIce[Random.Range(0, meltedIce.Count)];
                ice.currentState = IceState.Fine;
                Animator iceAnim = ice.transform.gameObject.GetComponent<Animator>();
                SpriteRenderer iceSr = ice.transform.gameObject.GetComponent<SpriteRenderer>();
                iceSr.sprite = iceSprite;
                iceAnim.enabled = false;
                iceAnim.enabled = true;
                iceAnim.enabled = false;
            }
        }
    }
}
