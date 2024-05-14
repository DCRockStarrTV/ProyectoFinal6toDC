using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Palancas : MonoBehaviour
{
    public GameObject portalApagado;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable palancaInteractable;
    private bool portalEncendido = false; // Estado del objeto

    private void Start()
    {
        palancaInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();

        palancaInteractable.selectEntered.AddListener(ToggleObjeto);
    }

    private void ToggleObjeto(SelectEnterEventArgs args)
    {
        // Cambia el estado del objeto (encendido/apagado)
        portalEncendido = !portalEncendido;

        // Activa o desactiva el objeto seg�n su estado
        portalApagado.SetActive(portalEncendido);

    }
}
