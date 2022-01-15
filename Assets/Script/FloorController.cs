using System;
using TouchScript.Gestures;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField]
    private GameObject _floorObj;
    [SerializeField]
    private GameObject _wallObj;
    [SerializeField]
    private TapGesture _tap;

    private bool _isActive = true;

    public void Init()
    {
        _tap.Tapped += HandleTapped;
        _floorObj.SetActive(_isActive);
        _wallObj.SetActive(!_isActive);

    }
    private void HandleTapped(object sender, EventArgs e)
    {
        _isActive = !_isActive;
        _floorObj.SetActive(_isActive);
        _wallObj.SetActive(!_isActive);
        Debug.Log("Tapped:" + gameObject.name);
    }

}
