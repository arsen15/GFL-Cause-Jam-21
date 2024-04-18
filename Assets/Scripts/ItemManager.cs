using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public int coconutCount;

    public Text coconutText;
    public GameObject door;
    private bool doorDestroyed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (coconutText != null)
        {
            coconutText.text = ": " + coconutCount.ToString();
        }
        else
        {
            Debug.LogWarning("coconutText is not assigned!");
        }

        if (coconutCount == 15 && !doorDestroyed)
        {
            doorDestroyed = true;
            Destroy(door);
        }

    }
}
