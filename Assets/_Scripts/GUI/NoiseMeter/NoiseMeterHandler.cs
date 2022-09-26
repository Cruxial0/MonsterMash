using System.Collections;
using System.Collections.Generic;
using _Scripts.Handlers;
using UnityEngine;

public class NoiseMeterHandler : MonoBehaviour
{
    public int MaxNoise;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInteractionHandler.SceneObjects.UI.NoiseMeter.NoiseProperties.MaxNoise = MaxNoise;
        PlayerInteractionHandler.SceneObjects.UI.NoiseMeter.Text.text = 
            $"{PlayerInteractionHandler.SceneObjects.UI.NoiseMeter.NoiseProperties.CurrentNoise}/{MaxNoise}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNoise()
    {
        var currNoise = PlayerInteractionHandler.SceneObjects.UI.NoiseMeter.NoiseProperties.CurrentNoise += 1;
        if (currNoise == MaxNoise)
        {
            PlayerInteractionHandler.SceneObjects.UI.NoiseMeter.Text.color = Color.red;
            PlayerInteractionHandler.GameStateManager.Lose();
        }
        
        if(currNoise == MaxNoise - 1)
            PlayerInteractionHandler.SceneObjects.UI.NoiseMeter.Text.color = Color.yellow;
        
        PlayerInteractionHandler.SceneObjects.UI.NoiseMeter.Text.text = $"{currNoise}/{MaxNoise}";
    }
}
