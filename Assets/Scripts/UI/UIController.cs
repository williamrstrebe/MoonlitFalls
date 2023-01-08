using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public UnityEngine.UI.Text currentSelectTxt;
    // Start is called before the first frame update
    void Start()
    {

        //currentSelectTxt = currentSelect.GetComponentInChildren<UnityEngine.UI.Text>();
        //currentSelectTxt.text = "haha";

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentSelectTxt);
        //currentSelectTxt.text = "haha";
    }

    public void ChangeText(string dest, string value)
    {

        if (dest.Equals("CurrentSelect"))
        {
            currentSelectTxt.text = "Equipamento selecionado: "+value;
        }

    }
}
