using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCubes : MonoBehaviour
{
    /* This script is used to instantiate parametric cubes 
     * these cubes will represent the audio subdivision being 
     * visialized. These cubes will only move up and down. */


    //private static readonly int cubeCount = 512;
    private static readonly int cubeCount = 512;
    public GameObject _sampleCubePrefab;
    GameObject[] _sampleCube = new GameObject[cubeCount];
    public float _maxScale; //float for scaling Audio sample size


    public GameObject projectile; //shadow cube to be fired at the base



    public int _band;
    public float _startScale, _scaleMultiplyer;
    public bool _useBuffer;
    public int _cubeCount;

    // Start is called before the first frame update
    void Start()
    {



        for (int i = 0; i < cubeCount; i++)
        {
            GameObject _instaceSampleCube = (GameObject)Instantiate(_sampleCubePrefab); //create cube
            _instaceSampleCube.transform.position = this.transform.position; //set new cube's position
            _instaceSampleCube.transform.parent = this.transform; //make new cube a child
            _instaceSampleCube.name = "sampleCube" + i; //name the cube by it's index
            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0); //rotate the cube
            //this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0); //rotate the cube
            _instaceSampleCube.transform.position = Vector3.forward * 100; //move cube
            _sampleCube[i] = _instaceSampleCube;

        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cubeCount; i++)
        {
            if (_sampleCube != null && !_useBuffer)
            {
                _sampleCube[i].transform.localScale = new Vector3(10, AudioPeer._samplesLeft[i] * _maxScale + 2, 10);
            }
            if (_sampleCube != null && _useBuffer)
            {
                _sampleCube[i].transform.localScale = new Vector3(10, AudioPeer._freqBand[_band] * _maxScale + 2, 10);
            }
        }
        
    }
}
