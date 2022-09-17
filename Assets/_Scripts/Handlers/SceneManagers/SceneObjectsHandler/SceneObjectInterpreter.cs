using System.Collections.Generic;
using System.Linq;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler.SceneObjectTypes;
using _Scripts.MonoBehaviour.Interactables.Pickup;
using _Scripts.MonoBehaviour.Interactables.Traps;
using UnityEngine;

namespace _Scripts.Handlers.SceneManagers.SceneObjectsHandler
{
    public class SceneObjectInterpreter
    {
        //OBJECTS
        //MainCamera
            //CameraScript
        //List of walls
        //Floor
        //Bed
        //Light
        //List of Pickups and Collision (Intractables)
        //List of Traps (ITrapCollision)

        //PROPERTIES (some given from interfaces)
        //Player speed
        //Timer
        //Noise
        
        
        //REVERT TO DEFAULT VALUES

        public RoomSceneObject GetRoom(List<GameObject> rootObjects)
        {
            RoomSceneObject room = new RoomSceneObject();

            Debug.Log("getroom");
            foreach (var gameObject in rootObjects.Where(x => x.CompareTag("RoomParent")))
            {
                foreach (Transform obj in gameObject.transform)
                {
                    var floor = obj.gameObject;
                    
                    Debug.Log("found floor");
                    room.Floor = floor;

                    foreach (Transform innerChild in floor.transform)
                    {
                        var child = innerChild.gameObject;

                        switch (child.tag)
                        {
                            case "Interactable":

                                switch (child.GetComponent<InteractableInitialize>().Type)
                                {
                                    case InteractType.Pickup:
                                        room.PickupObject.Add(new PickupSceneObject()
                                        {
                                            Collider = child.GetComponent<Collider>(),
                                            Script = child.GetComponent<InteractableInitialize>(),
                                            SpriteMask = child.GetComponent<SpriteMask>(),
                                            SpriteRenderer = child.GetComponent<SpriteRenderer>(),
                                            Transform = child.GetComponent<Transform>()
                                        });
                                        break;
                                    case InteractType.Collision:
                                        room.FurnitureObjects.Add(new FurnitureSceneObject()
                                        {
                                            Collider = child.GetComponent<Collider>(),
                                            MeshFilter = child.GetComponent<MeshFilter>(),
                                            MeshRenderer = child.GetComponent<MeshRenderer>(),
                                            Transform = child.GetComponent<Transform>()
                                        });
                                        break;
                                    case InteractType.Collision | InteractType.Pickup:
                                        break;
                                }

                                break;
                            case "Trap":
                                room.TrapObjects.Add(new TrapSceneObject()
                                {
                                    Collider = child.GetComponent<Collider>(),
                                    Script = child.GetComponent<TrapInitialize>(),
                                    SpriteRenderer = child.GetComponent<SpriteRenderer>(),
                                    Transform = child.GetComponent<Transform>(),

                                });
                                break;
                            case "RoomParent":
                                room.ParentObject = gameObject;
                                break;
                            case "RoomWall":
                                room.Walls.Add(child);
                                break;
                            case "Light":
                                Debug.Log("found light");
                                room.LightObject = new LightSceneObject()
                                {
                                    Light = child.GetComponent<Light>(),
                                    Transform = child.GetComponent<Transform>()
                                };
                                break;
                        }
                    }
                }
            }
            
            return room;
        }
    }
}