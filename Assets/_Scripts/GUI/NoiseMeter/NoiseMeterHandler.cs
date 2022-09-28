using _Scripts.Handlers;
using UnityEngine;

public class NoiseMeterHandler : MonoBehaviour
{
    public int MaxNoise;

    // Start is called before the first frame update
    private void Start()
    {
        //Assign MaxNoise to SceneObjects
        PlayerInteractionHandler.SceneObjects.UI.NoiseMeter.NoiseProperties.MaxNoise = MaxNoise;
        //Assign Text to SceneObjects
        PlayerInteractionHandler.SceneObjects.UI.NoiseMeter.Text.text =
            $"{PlayerInteractionHandler.SceneObjects.UI.NoiseMeter.NoiseProperties.CurrentNoise}/{MaxNoise}";
    }

    public void AddNoise()
    {
        //Current Noise += 1
        var currNoise = PlayerInteractionHandler.SceneObjects.UI.NoiseMeter.NoiseProperties.CurrentNoise += 1;

        if (currNoise == MaxNoise)
        {
            //Set text color to red
            PlayerInteractionHandler.SceneObjects.UI.NoiseMeter.Text.color = Color.red;
            //Lose
            PlayerInteractionHandler.GameStateManager.Lose();
        }

        //Change color if currNoise = (MaxNoise - 1)
        if (currNoise == MaxNoise - 1)
            PlayerInteractionHandler.SceneObjects.UI.NoiseMeter.Text.color = Color.yellow;

        //Update Text
        PlayerInteractionHandler.SceneObjects.UI.NoiseMeter.Text.text = $"{currNoise}/{MaxNoise}";
    }
}