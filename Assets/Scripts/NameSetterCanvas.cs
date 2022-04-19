using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;

public class NameSetterCanvas : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    
    // Start is called before the first frame update
    void Start()
    {
        _inputField.onSubmit.AddListener(Input_OnSubmit);
    }

    private void Input_OnSubmit(string text)
    {
        PlayerNameTracker.SetName(text);
    }
}
