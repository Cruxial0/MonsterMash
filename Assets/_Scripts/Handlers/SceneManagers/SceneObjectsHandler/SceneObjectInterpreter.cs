using System.Collections.Generic;
using System.Linq;
using _Scripts.GUI.NoiseMeter;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler.SceneEventObjects;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler.SceneObjectTypes;
using _Scripts.MonoBehaviour.Camera;
using _Scripts.MonoBehaviour.Interactables.Pickup;
using _Scripts.MonoBehaviour.Interactables.Traps;
using _Scripts.MonoBehaviour.Player;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace _Scripts.Handlers.SceneManagers.SceneObjectsHandler
{
    /// <summary>
    ///     Gets usable instances of all objects and properties
    /// </summary>
    public class SceneObjectInterpreter
    {
        public PlayerSceneObject GetPlayer(List<GameObject> rootObjects)
        {
            var player = rootObjects.First(x => x.CompareTag("Player"));
            GameObject sprite = player.transform.GetChild(1).gameObject;

            return new PlayerSceneObject
            {
                Collider = player.GetComponent<Collider>(),
                InitializeScript = player.GetComponent<PlayerInitialize>(),
                MovmentController = player.GetComponent<PlayerMovmentController>(),
                Rigidbody = player.GetComponent<Rigidbody>(),
                Transform = player.GetComponent<Transform>(),
                PlayerStates = player.GetComponent<PlayerStates>(),
                PlayerLight = player.transform.GetChild(0).gameObject.GetComponent<Light>(),
                Sprites = player.GetComponent<AssetContainer>(),
                Self = player,
                Sprite = new PlayerSprite()
                {
                    Plane = sprite,
                    SpriteMask = sprite.GetComponent<SpriteMask>(),
                    SpriteRenderer = sprite.GetComponent<SpriteRenderer>()
                }
            };
        }

        public CameraSceneObject GetMainCamera(List<GameObject> rootObjects)
        {
            var camera = rootObjects.First(x => x.CompareTag("MainCamera"));

            return new CameraSceneObject { Script = camera.GetComponent<CameraPositionController>() };
        }

        public RoomSceneObject GetRoom(List<GameObject> rootObjects)
        {
            var room = new RoomSceneObject();

            room.ParentObject = rootObjects.First(x => x.CompareTag("RoomParent"));

            foreach (Transform child in room.ParentObject.transform)
            {
                switch (child.tag)
                {
                    case "RoomFloor":
                        room.Floor = child.gameObject;
                        break;
                    case "RoomWall":
                        room.Walls.Add(child.gameObject);
                        break;
                }
            }
            
            foreach (var child in rootObjects)
            {
                switch (child.tag)
                {
                    case "Interactable":
                        switch (child.GetComponent<InteractableInitialize>().Type)
                        {
                            case InteractType.Pickup:
                                room.PickupObject.Add(new PickupSceneObject
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
                                room.FurnitureObjects.Add(new FurnitureSceneObject
                                {
                                    Rigidbody = child.GetComponent<Rigidbody>(),
                                    Transform = child.GetComponent<Transform>(),
                                    Script = child.GetComponent<InteractableInitialize>(),
                                    Body = new FurnitureBody
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
                        room.TrapObjects.Add(new TrapSceneObject
                        {
                            Collider = child.GetComponent<Collider>(),
                            Script = child.GetComponent<TrapInitialize>(),
                            SpriteRenderer = child.GetComponent<SpriteRenderer>(),
                            Transform = child.GetComponent<Transform>()
                        });
                        break;
                    case "Light":
                        room.LightObject.Add(new LightSceneObject
                        {
                            Light = child.GetComponent<Light>(),
                            Transform = child.GetComponent<Transform>()
                        });
                        break;
                    case "Bed":
                        room.BedObject = new BedSceneObject
                        {
                            Transform = child.GetComponent<Transform>(),
                            MeshFilter = child.GetComponent<MeshFilter>(),
                            MeshRenderer = child.GetComponent<MeshRenderer>(),
                            Rigidbody = child.GetComponent<Rigidbody>(),
                            Collider = child.GetComponent<Collider>(),
                            Script = child.GetComponent<BedController>()
                        };
                        break;
                    case "Door":
                        GameObject main = child.transform.GetChild(0).gameObject;
                        room.DoorObject = new DoorSceneObject
                        {
                            Transform = main.transform,
                            DoorPivot = main,
                            MeshFilter = child.GetComponent<MeshFilter>(),
                            MeshRenderer = child.GetComponent<MeshRenderer>()
                        };
                        break;
                }

                if (child.transform.childCount > 0 && child.CompareTag("EventObject"))
                {
                    switch (child.transform.GetChild(0).GetComponent<UnityEngine.MonoBehaviour>())
                    {
                        case SecurityCameraHandler:
                            var ch = child.transform.GetChild(0);
                            room.EventObjects.Add(new CameraObject()
                            {
                                Animation = child.GetComponent<Animation>(),
                                Animator = child.GetComponent<Animator>(),
                                MeshFilter = child.GetComponent<MeshFilter>(),
                                MeshRenderer = child.GetComponent<MeshRenderer>(),
                                PlayableDirector = child.GetComponent<PlayableDirector>(),
                                Transform = child.GetComponent<Transform>(),
                                CameraView = new CameraViewArea()
                                {
                                    Collider = ch.GetComponent<Collider>(),
                                    MeshFilter = ch.GetComponent<MeshFilter>(),
                                    MeshRenderer = ch.GetComponent<MeshRenderer>(),
                                    Script = ch.GetComponent<SecurityCameraHandler>(),
                                    Transform = ch.GetComponent<Transform>()
                                }
                            });
                            break;
                    }
                }
                
            }
            
            return room;
        }

        public UISceneObject GetGUI(List<GameObject> rootObjects)
        {
            var gui = new UISceneObject();

            var uiParent = rootObjects.First(x => x.CompareTag("UI"));
            var canvas = uiParent.transform.GetChild(0).gameObject;

            gui.CanvasObject = new CanvasObject
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
                        gui.MobileJoystick = new MobileJoystick
                        {
                            CanvasRenderer = child.GetComponent<CanvasRenderer>(),
                            Image = child.GetComponent<Image>(),
                            OnScreenStick = child.GetComponent<FixedJoystick>(),
                            RectTransform = child.GetComponent<RectTransform>()
                        };
                        break;
                    case "CollectSprite":
                        gui.CollectableSprite = new CollectableSprite
                        {
                            CanvasRenderer = child.GetComponent<CanvasRenderer>(),
                            Image = child.GetComponent<Image>(),
                            RectTransform = child.GetComponent<RectTransform>()
                        };
                        break;
                    case "CollectCounter":
                        gui.CollectableCounter = new CollectableCounter
                        {
                            CanvasRenderer = child.GetComponent<CanvasRenderer>(),
                            RectTransform = child.GetComponent<RectTransform>(),
                            Text = child.GetComponent<TextMeshProUGUI>()
                        };
                        break;
                    case "NoiseMeter":
                        gui.NoiseMeterSceneObject = new NoiseMeterSceneObject()
                        {
                            CanvasRenderer = child.GetComponent<CanvasRenderer>(),
                            RectTransform = child.GetComponent<RectTransform>(),
                            Image = child.GetComponent<Image>(),
                            NoiseProperties = new NoiseProperties
                            {
                                CurrentNoise = 0,
                                MaxNoise = 0
                            },
                            Script = child.GetComponent<NoiseMeter>()
                        };
                        break;
                    case "Timer":
                        gui.Timer = new Timer
                        {
                            CanvasRenderer = child.GetComponent<CanvasRenderer>(),
                            RectTransform = child.GetComponent<RectTransform>(),
                            Text = child.GetComponent<TextMeshProUGUI>(),
                            TimerHandler = child.GetComponent<TimerHandler>()
                        };
                        break;
                    case "ParentWarning":
                        gui.ParentWarning = new ParentWarningObject()
                        {
                            CanvasRenderer = child.GetComponent<CanvasRenderer>(),
                            RectTransform = child.GetComponent<RectTransform>(),
                            Sprite = child.GetComponent<Image>()
                        };
                        break;
                    case "Debug":
                        gui.DebugGUI = child;
                        break;
                }
            }

            return gui;
        }
    }
}