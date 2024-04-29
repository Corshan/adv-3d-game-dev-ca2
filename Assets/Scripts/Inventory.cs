using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private TextMeshProUGUI _message;
    public Dictionary<Item, int> _inventory = new();
    private Item _item;
    private GameObject _itemGameObject;

    // Start is called before the first frame update
    void Start()
    {
        _message = GameObject.Find("message").GetComponent<TextMeshProUGUI>();
        _message.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (_item != null)
        {
            bool canpick = true;
            _message.text = "Press E to pick up";

            if (_inventory.ContainsKey(_item))
            {
                if (_inventory[_item] == _item.MaxAmount)
                {
                    _message.text = "Inventory Full";
                    canpick = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.E) && canpick)
            {
                if (_inventory.ContainsKey(_item)) _inventory[_item] += 1;
                else _inventory[_item] = 1;

                _item = null;
                Destroy(_itemGameObject);
                _itemGameObject = null;
            }
        }
        else
        {
            _message.text = "";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Item")) return;

        _item = other.gameObject.GetComponent<SpawnItem>()._item;
        _itemGameObject = other.gameObject;

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Item")) return;

        _item = null;
        _itemGameObject = null;
    }
}
