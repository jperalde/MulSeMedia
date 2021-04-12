using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Uduino;

public class HoldFToInteract : MonoBehaviour
{
    /*public float startTime = 0.0f;
    public float holdTime = 2.0f;*/
    [SerializeField]
    [Tooltip("Probably just the main camera.. but referenced here to avoid Camera.main calls")]
    private Camera camera;
    [SerializeField]
    [Tooltip("The layers the items to pickup will be on")]
    private LayerMask layerMask;
    [SerializeField]
    [Tooltip("How long it takes to pick up an item.")]
    private float pickupTime = 2f;
    [SerializeField]
    private TextMeshProUGUI itemNameText;

    private Item itemBeingPickedUp;
    private float currentPickupTimerElapsed;


    int turnable = 0;

    // Update is called once per frame
    void Update()
    {
        SelectItemBeingPickedUpFromRay();

        if (HasItemTargetted())
        {
            itemNameText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("pilla la F");
                turnable = 1;
            }
            if (turnable == 1)
            {
                UduinoManager.Instance.sendCommand("turnOn");
                Debug.Log("hasta turnOn tambien tira");
                //UduinoManager.Instance.sendCommand("turnOff");
                turnable = 0;
                Debug.Log("turnable está a" + turnable);
            }
            else
            {
                currentPickupTimerElapsed = 0f;
            }

        }
        else
        {
            itemNameText.gameObject.SetActive(false);
            currentPickupTimerElapsed = 0f;
        }

    }  //Update
    private bool HasItemTargetted()
    {
        return itemBeingPickedUp != null;
    }

    private void IncrementPickupProgressAndTryComplete()
    {
        currentPickupTimerElapsed += Time.deltaTime;
        if (currentPickupTimerElapsed >= pickupTime)
        {
            TouchItem();
        }
    }

    private void SelectItemBeingPickedUpFromRay()
    {
        Ray ray = camera.ViewportPointToRay(Vector3.one / 2f);
        Debug.DrawRay(ray.origin, ray.direction * 2f, Color.red);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 2f, layerMask))
        {
            var hitItem = hitInfo.collider.GetComponent<Item>();

            if (hitItem == null)
            {
                itemBeingPickedUp = null;
            }
            else if (hitItem != null && hitItem != itemBeingPickedUp)
            {
                itemBeingPickedUp = hitItem;
                itemNameText.text = "Hold F to smell the tree";
            }
        }
        else
        {
            itemBeingPickedUp = null;
        }
    }

    private void TouchItem()
    {
        /*Destroy(itemBeingPickedUp.gameObject);
        itemBeingPickedUp = null;*/
        
    }
}
