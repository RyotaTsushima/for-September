using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [SerializeField] GameObject[] _mouse;
    int _mouseIndex;
    List<GameObject> _hateList = new List<GameObject>();
    bool _commandPhase;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 挑発したネズミをリストに登録しておく
    /// </summary>
    /// <param name="index"></param>
    public void Hate(int index)
    {
        _hateList.Add(_mouse[index]);
    }
}
