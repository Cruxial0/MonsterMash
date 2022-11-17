using System.Linq;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.Handlers.Events
{
    /// <summary>
    ///     Makes the level dark and adds a light around the player
    /// </summary>
    public class LightsOutEvent : IEvent
    {
        /// <summary>
        ///     Inherited from IEvent
        /// </summary>
        public string EventName => "Lights Out!";

        public string Description => "Where did everything go?..";
        public SceneObjects Objects => PlayerInteractionHandler.SceneObjects;

        public void ApplyEvent()
        {
            // //Change light color to black
            // Objects.Room.LightObject.First().Light.color = Color.black;
            //
            // //Change UI Colors to white
            // Objects.UI.Timer.Text.color = Color.white;
            // Objects.UI.CollectableCounter.Text.color = Color.white;

            // Add the player light component
            var light = Objects.Player.PlayerLight;
            // light.intensity = 2.65f;
            // light.range = 15f;
            light.enabled = true;
            Objects.Room.BedObject.Script.OnBedExit += () => light.gameObject.SetActive(true);
        }
    }
}