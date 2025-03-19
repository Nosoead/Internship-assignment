using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    IInteractable currentInteractable;
    private int itemLayer;
    private int itemID;

    private void Awake()
    {
        itemLayer = LayerMask.NameToLayer("Item");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == itemLayer)
        {
            if (collision.TryGetComponent(out currentInteractable))
            {
                itemID = currentInteractable.Interact();
                currentInteractable = null;
            }
        }
    }
}
