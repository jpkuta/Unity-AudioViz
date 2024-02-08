using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public int _band;
    public float _startScale, _scaleMultiplyer;
    public bool _useBuffer;//bool to switch between regular frequency band or buffered band

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._bandBuffer[_band] * _scaleMultiplyer) + _startScale, transform.localScale.z);
        }
        if (!_useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._freqBand[_band] * _scaleMultiplyer) + _startScale, transform.localScale.z);
        }

    }
}
