using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    List<string> _items = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        _items.Add("チーズ");
        _items.Add("チーズ");
        _items.Add("チーズ");
        _items.Add("チーズ");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
