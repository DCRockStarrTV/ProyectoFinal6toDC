using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Contador : MonoBehaviour
{
    public TextMeshProUGUI contador;
    [SerializeField] GameObject cubo;

    private void Start() {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (cubo) {
            
        }
    }


}
