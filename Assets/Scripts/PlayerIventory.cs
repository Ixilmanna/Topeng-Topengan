using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int maskCount = 0;
    public int requiredMasks;


    void Start()
    {
        GameObject[] masks = GameObject.FindGameObjectsWithTag("Mask");
        requiredMasks = masks.Length;

        Debug.Log("Total Mask in Scene: " + requiredMasks);
    }
    public void AddMask()
    {
        maskCount++;
        Debug.Log("Mask collected: " + maskCount);

    }
}
