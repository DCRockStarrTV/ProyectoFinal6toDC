using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class GUN : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform spawner;
    [SerializeField] float bulletSpeed;



    void Start()
    {
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable _interactor = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        _interactor.activated.AddListener(FireGun);

    }

    private void FireGun(ActivateEventArgs arg)
    {
        GameObject tempBullet = Instantiate(bullet, spawner.position, Quaternion.identity);
        tempBullet.GetComponent<Rigidbody>().velocity = spawner.forward * bulletSpeed;
    }


}
