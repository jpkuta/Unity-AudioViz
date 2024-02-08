using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class AudioPeer2 : MonoBehaviour
{
    AudioSource _audioSource;
    public static float[] _samplesLeft = new float[512];//Samples from the audio file's left channel
    public static float[] _samplesRight = new float[512];//Samples from the audio file's right channel
    public static float[] _freqBand = new float[8]; // group samples into 8 main bands
    public static float[] _bandBuffer = new float[8];// Band buffer is used to smooth visualizer values for animations
    float[] _bufferDecrease = new float[8];

    float[] _freqBandHighest = new float[8];// Stores the value of the highset amplitue played for comparison
    public static float[] _audioBand = new float[8];
    public static float[] _audioBuffer = new float[8];
    public static float[] _audioBandBuffer = new float[8];

    //Drop down menu for selecting left, right or stereo sound
    public enum _channel { Stereo, Left, Right };
    public _channel channel = new _channel();

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
    }

    void GetSpectrumAudioSource()
    {
        /*Gets the data of the audio spectrum from the audio source, This is divided into _samples number of sample/subdivision
         * FFT stands for Fast Fourier Transfrom. This algorithm is what process the audio file as signal of frequencies and
         * allows the quantifying of audio samples over a range of frequencies.
        */
        //(sample, channel, FFT) 0 = left, 1 = right
        _audioSource.GetSpectrumData(_samplesLeft, 0, FFTWindow.Blackman);
        _audioSource.GetSpectrumData(_samplesRight, 1, FFTWindow.Blackman);
    }

    //Creates nomalized audio bands based of current and highest frequency bands
    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqBand[i] > _freqBandHighest[i])
            {
                _freqBandHighest[i] = _freqBand[i];
            }
            _audioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);
        }
    }

    void BandBuffer()
    {
        for (int g = 0; g < 8; g++)
        {
            if (_freqBand[g] > _bandBuffer[g])
            {
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease[g] = 0.005f;
            }
            if (_freqBand[g] < _bandBuffer[g])
            {
                _bandBuffer[g] -= _bufferDecrease[g];
                _bufferDecrease[g] *= 1.2f;
            }

        }

    }

    //Groups the 512 samples into 8 bands/groups
    void MakeFrequencyBands()
    {
        /*
         *22050 hertz / 512 samples = 43hertz per sample 
         * Tonal ramges
         * 20 - 60 hz
         * 60 - 250 hz
         * 250 - 500 hz
         * 500 - 2000 hz
         * 2000 - 4000 hz
         * 4000 - 6000 hz
         * 6000 - 20000 hz
         * 
         * Group samples
         * Group - Samples - frequency range
         * 0 - 2 = 86 hz
         * 1 - 4 = 172 hz
         * 2 - 8 = 344 hz
         * 3 - 16 = 688 hz
         * 4 - 32 = 1376 hz
         * 5 - 64 = 2752 hz
         * 6 - 128 = 5504 hz
         * 7 - 256 = 11008 hz
         * 
         */

        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2; //determines the number of samples being grouped

            if (i == 7)
            {
                sampleCount += 2; //without this only 510 of the 512 samples are used
            }
            for (int j = 0; j < sampleCount; j++)
            {
                if (channel == _channel.Stereo)
                {
                    average += _samplesLeft[count] + _samplesRight[count] * (count + 1);

                }
                if (channel == _channel.Right)
                {
                    average += _samplesRight[count] * (count + 1);

                }
                if (channel == _channel.Left)
                {
                    average += _samplesLeft[count] * (count + 1);

                }
                count++;
            }
            average /= count;
            _freqBand[i] = average * 10;

        }

    }

}
