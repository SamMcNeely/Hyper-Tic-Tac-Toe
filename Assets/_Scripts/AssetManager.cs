using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetManager : MonoBehaviour
{

    public GameObject subboard;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setGameBoardColor() {
        subboard.GetComponent<Image>().color = UnityEngine.Color.red;
    }
}
