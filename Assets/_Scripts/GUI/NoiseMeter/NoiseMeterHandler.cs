using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler.SceneObjectTypes;
using UnityEngine;
using UnityEngine.UI;

public class NoiseMeterHandler : UnityEngine.MonoBehaviour
{
    private int MaxNoise;
    public List<Sprite> NoiseSprites;

    private Dictionary<int, Sprite> noiseLevels = new Dictionary<int, Sprite>();
    private NoiseMeter noiseMeter;

    // Start is called before the first frame update
    private void Start()
    {
        MaxNoise = NoiseSprites.Count;
        for (int i = 0; i < MaxNoise; i++)
        {
            noiseLevels.Add(i, NoiseSprites[i]);
        }
        noiseMeter = PlayerInteractionHandler.SceneObjects.UI.NoiseMeter;
        MaxNoise = noiseLevels.Count;
    }

    public void AddNoise()
    {
        PlayerInteractionHandler.SceneObjects.UI.NoiseMeter.Image.sprite =
            noiseLevels[noiseMeter.NoiseProperties.CurrentNoise];
        
        //Current Noise += 1
        var currNoise = noiseMeter.NoiseProperties.CurrentNoise += 1;

        if (currNoise == MaxNoise)
        {
            //Set text color to red
            //GetComponent<Image>().sprite = noiseLevels[currNoise];
            //Lose
            PlayerInteractionHandler.GameStateManager.Lose();
        }
    }
}