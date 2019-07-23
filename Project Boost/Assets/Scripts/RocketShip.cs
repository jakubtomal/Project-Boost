using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketShip : MonoBehaviour
{
    [SerializeField] float flyingSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float levelLoadDelay;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip winSound;

    [SerializeField] ParticleSystem engineParticle;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem winParticle;

    private enum State {alive,dead,transcending}
    private State state = State.alive;

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(state != State.alive) { return; }
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                Win();
                break;
            default:
                Die();
                break;
        }
    }
    
    private void Win()
    {
        gameObject.GetComponent<AudioSource>().Stop();
        gameObject.GetComponent<AudioSource>().PlayOneShot(winSound);
        winParticle.Play();
        state = State.transcending;
        Invoke("LoadNextScene", levelLoadDelay);
    }

    private void Die()
    {
        gameObject.GetComponent<AudioSource>().Stop();
        gameObject.GetComponent<AudioSource>().PlayOneShot(deathSound);
        deathParticle.Play();
        state = State.dead;
        Invoke("LoadCurrentScene", levelLoadDelay);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ProcessInput()
    {
        if (state != State.alive) { return; }

        if (Input.GetKey(KeyCode.Space))
        {
            Fly(flyingSpeed);
        }

        else if (Input.GetKeyUp(KeyCode.Space))
        {
            StopFlying();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateOnZAxis(rotationSpeed);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateOnZAxis(-rotationSpeed);
        }
    }

    private void RotateOnZAxis(float speed)
    {
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        gameObject.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        gameObject.GetComponent<Rigidbody>().freezeRotation = false;

    }

    private void Fly(float speed)
    {
        if (!gameObject.GetComponent<AudioSource>().isPlaying) { gameObject.GetComponent<AudioSource>().PlayOneShot(mainEngine); }
        engineParticle.Play();
        gameObject.GetComponent<Rigidbody>().velocity += gameObject.transform.up * speed;
    }

    private void StopFlying()
    {
        gameObject.GetComponent<AudioSource>().Stop();
        engineParticle.Stop();
    }






}


