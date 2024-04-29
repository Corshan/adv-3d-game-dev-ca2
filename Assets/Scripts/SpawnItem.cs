using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public Item _item;
    public bool isQuestItem;

    // Start is called before the first frame update
    void Start()
    {
        var go = Instantiate(_item.Prefab, transform.position, Quaternion.identity, transform);

        go.tag = (isQuestItem) ? "Collect" : "Untagged";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
