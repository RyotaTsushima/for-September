using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{
    [SerializeField] GameObject[] _catPrefab;
    [SerializeField] Vector2 _spawnPos1;
    [SerializeField] Vector2 _spawnPos2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CatAppearance()
    {
        int _random = Random.Range(0, 2);
        Instantiate(_catPrefab[_random]);
        _random = Random.Range(0, 2);
        Instantiate(_catPrefab[_random]);
    }
}
