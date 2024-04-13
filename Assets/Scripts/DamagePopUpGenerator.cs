using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUpGenerator : MonoBehaviour
{
    public static DamagePopUpGenerator current;
    public GameObject prefab;

    private void Awake() {
        current = this;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.F10)) {
            CreatePopUp(new Vector3(Random.Range(0, 10), 0, Random.Range(0, 10)), Random.Range(0, 1000), Color.red);
        }
    }
    public void CreatePopUp(Vector3 position, int damage, Color color) {
        var popup = Instantiate(prefab, new Vector3(position.x, position.y + 0.5f, position.z), Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        //temp.transform.position = Camera.main.WorldToScreenPoint(position);
        //temp.transform.position = position;
        temp.text = "-" + damage.ToString();
        temp.faceColor = color;
        Destroy(popup, 0.5f);
    }
}
