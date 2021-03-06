using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcebergManager : MonoBehaviour
{
    public List<GameObject> iceblocks = new List<GameObject>();
    List<GameObject> fineIceblocks = new List<GameObject>();
    List<GameObject> meltingIceblocks = new List<GameObject>();
    List<GameObject> meltedIceblocks = new List<GameObject>();

    private float temperature;
    public float startTime = 2.0f;
    public float meltRate = 3.0f;

    public GameObject iceCrystalPrefab;
    public float iceCrystalFrequency = 10.0f;
    public float iceCrystalDelay = 15.0f;

    private void Start()
    {
        for(int i=0; i<transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.GetComponent<Iceblock>() != null)
            {
                iceblocks.Add(transform.GetChild(i).gameObject);
            }
        }
        for(int i=0; i<iceblocks.Count; i++)
        {
            Animator anim = iceblocks[i].GetComponent<Animator>();
            anim.enabled = false;
        }

        //Pick an iceblock to melt
        //InvokeRepeating("MeltIceblock", startTime, meltRate);
        StartCoroutine("MeltIceblock", startTime);
        InvokeRepeating("SpawnIceCrystal", iceCrystalDelay, iceCrystalFrequency);
    }

    private void Update()
    {
        for (int i = 0; i < iceblocks.Count; i++)
        {
            Iceblock iceData = iceblocks[i].GetComponent<Iceblock>();
            if(iceData.currentState == IceState.Fine)
            {
                fineIceblocks.Add(iceblocks[i]);
            }else if(iceData.currentState == IceState.Melting)
            {
                meltingIceblocks.Add(iceblocks[i]);
            }else if(iceData.currentState == IceState.Melted)
            {
                meltedIceblocks.Add(iceblocks[i]);
            }
        }

        for(int i = 0; i < meltingIceblocks.Count; i++)
        {
            Animator iceAnimator = meltingIceblocks[i].GetComponent<Animator>();
            if (iceAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !iceAnimator.IsInTransition(0))
            {
                Iceblock iceData = meltingIceblocks[i].GetComponent<Iceblock>();
                iceData.currentState = IceState.Melted;
                meltingIceblocks.Remove(meltingIceblocks[i]);

            }
        }

    }

    IEnumerator MeltIceblock()
    {
        while (true)
        {
            if (fineIceblocks.Count > 0)
            {
                int chosenBlock = Random.Range(0, fineIceblocks.Count);
                Iceblock iceData = fineIceblocks[chosenBlock].GetComponent<Iceblock>();
                Animator anim = fineIceblocks[chosenBlock].GetComponent<Animator>();

                iceData.currentState = IceState.Melting;
                anim.enabled = true;
                fineIceblocks.Remove(fineIceblocks[chosenBlock]);
            }

            yield return new WaitForSeconds(meltRate); // Spencer added this line and changed this function to an IEnum
        }
    }

    void SpawnIceCrystal()
    {
        if (fineIceblocks.Count > 0)
        {
            int chosenBlock = Random.Range(0, fineIceblocks.Count);
            Iceblock spawnBlock = fineIceblocks[chosenBlock].GetComponent<Iceblock>();

            Instantiate(iceCrystalPrefab, spawnBlock.transform.position, Quaternion.identity);
        }
    }
}
