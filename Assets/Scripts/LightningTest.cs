using UnityEngine;
using System.Collections;

public class LightningTest : MonoBehaviour {

    float start;
    float startTime;
    ParticleSystem.Particle[] particles;
    public Vector3[] particleVelocity;
    ParticleSystem particleSys;
    int size;
    // Use this for initialization
    void Start ()
    {
        startTime = Time.time;
        particleSys = GetComponent<ParticleSystem>();
        particleSys.Stop();
        particleSys.Play();
        start = Time.time;
        particles = new ParticleSystem.Particle[particleSys.maxParticles];
        size = particleSys.GetParticles(particles);
        particleVelocity = new Vector3[size];
        for (int i = 0; i < size; i++)
        {
            particleVelocity[i] = particles[i].velocity;
        }
    }
	

    
    // Update is called once per frame
	void Update () {
        int newSize = particleSys.GetParticles(particles);
        if (newSize > size)
        {
            size = newSize;
            particleVelocity = new Vector3[size];
            for (int i = 0; i < size; i++)
            {
                particleVelocity[i] = particles[i].velocity;
            }
            
        }
        for (int i = 0; i < size; i++)
        {
            particles[i].velocity = Vector3.Lerp(particleVelocity[i], -particleVelocity[i], (Time.time - startTime)/particles[i].startLifetime);
        }
        particleSys.SetParticles(particles, size);
    }
}
