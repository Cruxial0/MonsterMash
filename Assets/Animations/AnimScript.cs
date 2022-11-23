using System;
using _Scripts.Handlers;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Threading.Tasks;

public class AnimScript : MonoBehaviour
{
    public GameObject timeAnim;
    public GameObject noiseAnim;
    public ParticleSystem deathParticle;
    [NonSerialized] public Animator Anim;

    Vector2 playerVelocity;
    private bool _playerDead;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerVelocity = PlayerInteractionHandler.SceneObjects.Player.MovmentController.Joystick.Direction;
        PlayerInteractionHandler.Self.OnCollided += Self_OnCollided;

        if(!_playerDead)
            Anim.SetBool("Run", playerVelocity != Vector2.zero);
    }

    private void Self_OnCollided()
    {
        if(_playerDead) return;
        Anim.SetTrigger("bump");
    }

    public void deathAnim()
    {
        if(_playerDead) return;
        Object.Instantiate(deathParticle).transform.position = PlayerInteractionHandler.SceneObjects.Player.Transform.position;
        
        Anim.SetTrigger("Death");
        _playerDead = true;
    }


    public void BearTrapAnim()
    {
        if(_playerDead) return;
        Object.Instantiate(deathParticle).transform.position = PlayerInteractionHandler.SceneObjects.Player.Transform.position;
        Anim.SetTrigger("BearTrap");
        transform.localScale = new Vector3(0.32f, 0.32f, 0.32f);
        _playerDead = true;
    }


    public void EatAnim()
    {
        if(_playerDead) return;
        Anim.SetTrigger("Eat");
    }

    public void TimeAnim()
    {
        if(_playerDead) return;
        timeAnim.SetActive(true);
    }


    public void NoiseAnim()
    {
        if (_playerDead) return;
        noiseAnim.SetActive(true);
        _playerDead = true;
    }
}
