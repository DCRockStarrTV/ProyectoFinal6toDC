using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedThePlayer : MonoBehaviour
{
    
    Pet companionAgent;

    void Start()
    {
        companionAgent = GameObject.Find("Pet").GetComponent<Pet>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            companionAgent.feed(gameObject.transform);
        }
    }
}

