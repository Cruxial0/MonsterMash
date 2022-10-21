using _Scripts.Handlers;
using UnityEngine;

namespace _Scripts.MonoBehaviour.CommonFunctionality
{
    public class NoiseObject : UnityEngine.MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            //Add noise on player collision
            if (collision.gameObject.CompareTag("Player"))
            {
                var player = PlayerInteractionHandler.SceneObjects.Player;
                var p = player.MovmentController.prevMovement;

                //(10,0,0)
                //(10,0,10)

                var speed = Vector3.Distance(p, player.Transform.position) / Time.deltaTime;
                var acceleration = speed / Time.deltaTime;
                
                

                print($"{speed}, {acceleration}, {speed / acceleration}");
                print(player.MovmentController.prevVelocity);
                
                PlayerInteractionHandler.SceneObjects.UI.NoiseMeterSceneObject.Script.AddHealth(0.01f);
                if (Application.platform != RuntimePlatform.WebGLPlayer)
                {
                    Handheld.Vibrate();
                }
            }
        }
    }
}