using _Scripts.Handlers;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Player
{
    public class Tutorial : UnityEngine.MonoBehaviour
    {
        public Material emissiveMat;
        public GameObject glowObject;
        private LineRenderer _lineRenderer;
        private bool _collectablesPickedUp = false;
        // Start is called before the first frame update
        private void Start()
        {
            emissiveMat.color = Color.green;
            
            _lineRenderer = PlayerInteractionHandler.SceneObjects.Player.Self.AddComponent<LineRenderer>();
            _lineRenderer.material = emissiveMat;
            _lineRenderer.startColor = Color.green;
            _lineRenderer.endColor = Color.green;
            _lineRenderer.widthMultiplier = 0.0001f;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_collectablesPickedUp)
            {
                Vector3 playerPos = PlayerInteractionHandler.SceneObjects.Player.Transform.position;
                Vector3 bedPos = PlayerInteractionHandler.SceneObjects.Room.BedObject.Transform.position;
                Vector3 normalizedPlayerPos = new Vector3(playerPos.x, playerPos.y - 0.1f, playerPos.z);
                Vector3 normalizedBedPos = new Vector3(bedPos.x, playerPos.y - 0.1f, bedPos.z);

                _lineRenderer.SetPosition(0, normalizedPlayerPos);
                _lineRenderer.SetPosition(1, normalizedBedPos);
                
            }
            if (PlayerInteractionHandler.SceneObjects.Room.PickupObject.Count != 0 || _collectablesPickedUp) return;

            var mRenderer = PlayerInteractionHandler.SceneObjects.Room.BedObject.MeshRenderer;
            //mRenderer.material = emissiveMat;

            _lineRenderer.widthMultiplier = 0.5f;
            
            _collectablesPickedUp = true;
            glowObject.SetActive(true);
        }
    }
}
