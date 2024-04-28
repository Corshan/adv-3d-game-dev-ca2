using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private RawImage _image;
    [SerializeField] private TextMeshProUGUI _name;
    private int _index = 0;

    // Start is called before the first frame update
    void Start()
    {
        _index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        List<Item> items = _inventory._inventory.Keys.ToList();

        if(items.Count == 0) gameObject.SetActive(false);

        if (Input.GetKeyDown(KeyCode.E)) _index++;
        if (Input.GetKeyDown(KeyCode.Q)) _index--;

        _index = Mathf.Clamp(_index, 0, items.Count - 1);

        if (items.Count > 0)
        {
            var item = items[_index];
            _image.texture = item.Sprite.texture;
            _name.text = $"{item.Name} [{_inventory._inventory[item]}]";
        }
    }
}
