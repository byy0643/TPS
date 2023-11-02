using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private GameObject selectedGun;
    // Start is called before the first frame update
    private void Awake()
    {
        selectedGun = GetComponent<GameObject>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void equipGun()
    {
        selectedGun.SetActive(false);
    }
}
