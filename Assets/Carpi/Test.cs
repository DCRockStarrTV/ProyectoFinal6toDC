using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class Test : MonoBehaviour {
    float sliderValue;
    public XRSlider xRSlider;
    public GameObject chanchito;
    public GameObject bala;
    public Transform transformBala1;
    public Transform transformBala2;
    public Transform transformBala3;
    public Transform transformBala4;
    public float bulletSpeed;
    public GameObject almacen;

    private void Start() {
        xRSlider = GetComponent<XRSlider>();
    }

    private void Update() {
        //if (xRSlider.value == 1)
        //{
        //    print("Valor positivo");
        //}
        //else if (xRSlider.value == 0) 
        //{
        //    print("Valor negativo");
        //}
    }

    public void MovX() {
        Debug.Log("Me movi en X");
        almacen.SetActive(true);
    }

    public void MovY() {
        Debug.Log("Me movi en Y");
        almacen.SetActive(false);
    }

    public void ButtonPress() {
        Debug.Log("Presión");
        Instantiate(chanchito, transform.position, Quaternion.identity);
    }

    public void ValueChange() {
        //Debug.Log("No me creo, ni me destruyo solo me transformo");
    }

    public void Ammo1() {
        print("Fire");
        var bullet = Instantiate(bala, transformBala1.transform.position, transformBala1.rotation * Quaternion.Euler(90, 0, 0));
        bullet.GetComponent<Rigidbody>().velocity = transformBala1.forward * bulletSpeed;
    }

    public void Ammo2() {
        print("Fire");
        var bullet = Instantiate(bala, transformBala2.transform.position, transformBala2.rotation * Quaternion.Euler(90, 0, 0));
        bullet.GetComponent<Rigidbody>().velocity = transformBala2.forward * bulletSpeed;
    }

    public void Ammo3() {
        print("Fire");
        var bullet = Instantiate(bala, transformBala3.transform.position, transformBala3.rotation * Quaternion.Euler(90, 0, 0));
        bullet.GetComponent<Rigidbody>().velocity = transformBala3.forward * bulletSpeed;
    }

    public void Ammo4() {
        print("Fire");
        var bullet = Instantiate(bala, transformBala4.transform.position, transformBala4.rotation * Quaternion.Euler(90, 0, 0));
        bullet.GetComponent<Rigidbody>().velocity = transformBala4.forward * bulletSpeed;
    }
}
