using System.Collections.Generic;
using System.Linq;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler.SceneObjectTypes;
using _Scripts.MonoBehaviour.Camera;
using _Scripts.MonoBehaviour.Interactables.Pickup;
using _Scripts.MonoBehaviour.Interactables.Traps;
using _Scripts.MonoBehaviour.Player;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

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

        public PlayerSceneObject GetPlayer(List<GameObject> rootObjects)
        {
            GameObject player = rootObjects.First(x => x.CompareTag("Player"));

            return new PlayerSceneObject()
            {
                Collider = player.GetComponent<Collider>(),
                InitializeScript = player.GetComponent<PlayerInitialize>(),
                MeshFilter = player.GetComponent<MeshFilter>(),
                MeshRenderer = player.GetComponent<MeshRenderer>(),
                MovmentController = player.GetComponent<PlayerMovmentController>(),
                Rigidbody = player.GetComponent<Rigidbody>(),
                Transform = player.GetComponent<Transform>(),
                PlayerStates = player.GetComponent<PlayerStates>(),
                PlayerLight = player.transform.GetChild(0).gameObject.GetComponent<Light>(),
                Self = player
            };
        }

        public CameraSceneObject GetMainCamera(List<GameObject> rootObjects)
        {
            GameObject camera = rootObjects.First(x => x.CompareTag("MainCamera"));

            return new CameraSceneObject() { Script = camera.GetComponent<CameraPositionController>() };
        }
        
        public RoomSceneObject GetRoom(List<GameObject> rootObjects)
        {
            RoomSceneObject room = new RoomSceneObject();
            
            foreach (var gameObject in rootObjects.Where(x => x.CompareTag("RoomParent")))
            {
                foreach (Transform obj in gameObject.transform)
                {
                    var floor = obj.gameObject;
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
                                        var body = child.transform.GetChild(0).gameObject;
                                        room.FurnitureObjects.Add(new FurnitureSceneObject()
                                        {
                                            Rigidbody = child.GetComponent<Rigidbody>(),
                                            Transform = child.GetComponent<Transform>(),
                                            Script = child.GetComponent<InteractableInitialize>(),
                                            Body = new FurnitureBody()
                                            {
                                                Collider = body.GetComponent<Collider>(),
                                                MeshFilter = body.GetComponent<MeshFilter>(),
                                                MeshRenderer = body.GetComponent<MeshRenderer>(),
                                                Transform = body.GetComponent<Transform>()
                                            }
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
                                room.LightObject = new LightSceneObject()
                                {
                                    Light = child.GetComponent<Light>(),
                                    Transform = child.GetComponent<Transform>()
                                };
                                break;
                            case "Bed":
                                room.BedObject = new BedSceneObject()
                                {
                                    Transform = child.GetComponent<Transform>(),
                                    MeshFilter = child.GetComponent<MeshFilter>(),
                                    MeshRenderer = child.GetComponent<MeshRenderer>(),
                                    Rigidbody = child.GetComponent<Rigidbody>(),
                                    Collider = child.GetComponent<Collider>(),
                                    Script = child.GetComponent<BedController>()
                                };
                                break;
                        }
                    }
                }
            }
            
            return room;
        }

        public UISceneObject GetGUI(List<GameObject> rootObjects)
        {
            UISceneObject gui = new UISceneObject();

            var uiParent = rootObjects.First(x => x.CompareTag("UI"));
            var canvas = uiParent.transform.GetChild(0).gameObject;

            gui.CanvasObject = new CanvasObject()
            {
                Canvas = canvas.GetComponent<Canvas>(),
                CanvasScaler = canvas.GetComponent<CanvasScaler>(),
                GraphicRaycaster = canvas.GetComponent<GraphicRaycaster>(),
                RectTransform = canvas.GetComponent<RectTransform>()
            };

            foreach (Transform obj in canvas.transform)
            {
                var child = obj.gameObject;

                switch (child.tag)
                {
                    case "GameController":
                        gui.MobileJoystick = new MobileJoystick()
                        {
                            CanvasRenderer = child.GetComponent<CanvasRenderer>(),
                            Image = child.GetComponent<Image>(),
                            OnScreenStick = child.GetComponent<OnScreenStick>(),
                            RectTransform = child.GetComponent<RectTransform>()
                        };
                        break;
                    case "CollectSprite":
                        gui.CollectableSprite = new CollectableSprite()
                        {
                            CanvasRenderer = child.GetComponent<CanvasRenderer>(),
                            Image = child.GetComponent<Image>(),
                            RectTransform = child.GetComponent<RectTransform>()
                        };
                        break;
                    case "CollectCounter":
                        gui.CollectableCounter = new CollectableCounter()
                        {
                            CanvasRenderer = child.GetComponent<CanvasRenderer>(),
                            RectTransform = child.GetComponent<RectTransform>(),
                            Text = child.GetComponent<TextMeshProUGUI>()
                        };
                        break;
                    case "NoiseMeter":
                        gui.NoiseMeter = new NoiseMeter()
                        {
                            CanvasRenderer = child.GetComponent<CanvasRenderer>(),
                            RectTransform = child.GetComponent<RectTransform>(),
                            Text = child.GetComponent<TextMeshProUGUI>(),
                            NoiseProperties = new NoiseProperties()
                            {
                                CurrentNoise = 0,
                                MaxNoise = 0
                            },
                            Script = child.GetComponent<NoiseMeterHandler>()
                        };
                        break;
                    case "Timer":
                        gui.Timer = new Timer()
                        {
                            CanvasRenderer = child.GetComponent<CanvasRenderer>(),
                            RectTransform = child.GetComponent<RectTransform>(),
                            Text = child.GetComponent<TextMeshProUGUI>(),
                            TimerHandler = child.GetComponent<TimerHandler>()
                        };
                        break;
                }
            }

            return gui;
        }
    }
}