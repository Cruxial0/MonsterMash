using _Scripts.Handlers;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Player
{
    public class Tutorial : UnityEngine.MonoBehaviour
    {
        public Material emissiveMat; // Color for the guideline
        public GameObject glowObject; // Object for the glow under the bed
        private LineRenderer _lineRenderer; // LineRenderer for guideline
        private bool _collectablesPickedUp = false; // Is all collectables picked up?
        // Start is called before the first frame update
        private void Start()
        {
            emissiveMat.color = Color.green; // Set color to green
            
            // Add and assign LineRenderer property
            _lineRenderer = PlayerInteractionHandler.SceneObjects.Player.Self.AddComponent<LineRenderer>();
            // Edit properties
            _lineRenderer.material = emissiveMat;
            _lineRenderer.startColor = Color.green;
            _lineRenderer.endColor = Color.green;
            _lineRenderer.widthMultiplier = 0.0001f; // Make it practically invisible for now
        }

        // Update is called once per frame
        private void Update()
        {
            // If all collectables are picked up and PlayerStates does not equal null (checking if player exists)
            if (_collectablesPickedUp && PlayerInteractionHandler.SceneObjects.Player.PlayerStates != null)
            {
                // Get start and end point for line
                Vector3 playerPos = PlayerInteractionHandler.SceneObjects.Player.Transform.position;
                Vector3 bedPos = PlayerInteractionHandler.SceneObjects.Room.BedObject.Transform.position;
                Vector3 normalizedPlayerPos = new Vector3(playerPos.x, playerPos.y - 0.1f, playerPos.z);
                Vector3 normalizedBedPos = new Vector3(bedPos.x, playerPos.y - 0.1f, bedPos.z);

                // Set start and end position
                _lineRenderer.SetPosition(0, normalizedPlayerPos);
                _lineRenderer.SetPosition(1, normalizedBedPos);
                
            }
            
            // If PickupObjects does not equal 0, or all collectables are picked up, return
            if (PlayerInteractionHandler.SceneObjects.Room.PickupObject.Count != 0 || _collectablesPickedUp) return;
            
            _lineRenderer.widthMultiplier = 0.5f; // Set width
            
            _collectablesPickedUp = true; // All collectables are picked up
            glowObject.SetActive(true); // Enable Bed glow
        }
    }
}
