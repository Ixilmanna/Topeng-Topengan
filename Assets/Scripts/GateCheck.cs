using UnityEngine;
using System.Collections;

public class GateCheck : MonoBehaviour
{
    public GameObject pressECaption;
    public GameObject needMaskCaption;
    public GameObject gateOpenCaption;

    private bool gateOpened = false;
    private bool playerInside = false;

    private PlayerInventory playerInventory;

    void Start()
    {
        if (pressECaption != null)
            pressECaption.SetActive(false);

        if (needMaskCaption != null)
            needMaskCaption.SetActive(false);

        if (gateOpenCaption != null)
            gateOpenCaption.SetActive(false);
    }

    void Update()
    {
        if (playerInside && !gateOpened)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryOpenGate();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = other.GetComponent<PlayerInventory>();

            if (playerInventory != null)
            {
                playerInside = true;

                if (!gateOpened && pressECaption != null)
                    pressECaption.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            playerInventory = null;

            HideAllCaption();
        }
    }

    void TryOpenGate()
    {
        if (playerInventory == null) return;

        if (playerInventory.maskCount >= playerInventory.requiredMasks)
        {
            Debug.Log("Gate opened.");
            gateOpened = true;

            HideAllCaption();

            if (gateOpenCaption != null)
                gateOpenCaption.SetActive(true);

            // Animasi buka gate
        }
        else
        {
            Debug.Log("You need more masks to pass.");

            if (pressECaption != null)
                pressECaption.SetActive(false);

            if (needMaskCaption != null)
            {
                needMaskCaption.SetActive(true);
                StartCoroutine(HideNeedMask());
            }
        }
    }

    IEnumerator HideNeedMask()
    {
        yield return new WaitForSeconds(2f);

        if (needMaskCaption != null)
            needMaskCaption.SetActive(false);

        if (playerInside && !gateOpened && pressECaption != null)
            pressECaption.SetActive(true);
    }

    void HideAllCaption()
    {
        if (pressECaption != null)
            pressECaption.SetActive(false);

        if (needMaskCaption != null)
            needMaskCaption.SetActive(false);

        if (gateOpenCaption != null)
            gateOpenCaption.SetActive(false);
    }
}
