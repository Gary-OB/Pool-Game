using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodedSpherePhysics : MonoBehaviour
{

    Vector3 acceleration;
    public Vector3 velocity;
    public float mass;
    private bool collisionOccurred = false;
    GameObject[] planes;
    private CodedSpherePhysics occurredWith;
    CodedSpherePhysics[] spheres;

    float cOR = 0.5f; //Coefficient of restitution
    float sphereCOR;
    // Use this for initialization
    void Start()
    {
        planes = GameObject.FindGameObjectsWithTag("Plane");
        spheres = FindObjectsOfType<CodedSpherePhysics>();
        mass = Random.Range(0.5f, 1.0f);
        sphereCOR = (mass * 2) / 1.5f;
        this.transform.localScale = new Vector3(mass, mass, mass);
    }


    // Update is called once per frame
    void Update()
    {
        collisionOccurred = false;

        acceleration = 9.8f * Vector3.down;
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        foreach (GameObject plane in planes)
        {

            Vector3 fromPlaneToSphere = transform.position - plane.transform.position;
            Vector3 planeNormal = plane.transform.up;



            if (parallelComponent(fromPlaneToSphere, planeNormal).magnitude < (this.transform.localScale.x / 2))
            {
                transform.position -= parallelComponent(velocity * Time.deltaTime, planeNormal);
                velocity = perpendicularComponent(velocity, planeNormal) - parallelComponent(velocity, planeNormal) * cOR;
            }

        }

        velocity -= velocity * 0.01f;
       
    }


    /// <summary>
    /// This returns the parallel component of the vector v parallel to n
    /// </summary>
    /// <param name="v"> Vector to Decompose</param>
    /// <param name="n"> Unit vector to decompose parallel to, this is usually a unit vector</param>
    /// <returns></returns>
    Vector3 parallelComponent(Vector3 v, Vector3 n)
    {
        Vector3 n1 = n.normalized;

        return Vector3.Dot(v, n1) * n1;

    }


    /// <summary>
    /// This returns the perpendicular component of the vector v parallel to n
    /// </summary>
    /// <param name="v">Vector to Decompose</param>
    /// <param name="n">Unit vector to decompose perpendicular to, this is usually a unit vector</param>
    /// <returns></returns>
    Vector3 perpendicularComponent(Vector3 v, Vector3 n)
    {

        return v - parallelComponent(v, n);


    }


    /// <summary>
    /// Detects collision and sets up for collision with only the ball colliding
    /// </summary>
    /// <param name="collision">Collider of other sphere</param>
    private void OnTriggerEnter(Collider collision)
    {
        

        if (!collisionOccurred)
        {
            

            CodedSpherePhysics otherSphere = collision.gameObject.GetComponent<CodedSpherePhysics>();

            

            if (otherSphere) collidesWith(otherSphere);
        }
    }


    /// <summary>
    /// Maths for collision between two spheres
    /// </summary>
    /// <param name="otherSphere">The sphere being collided with</param>
    private void collidesWith(CodedSpherePhysics otherSphere)
    {

        Vector3 n = (transform.position - otherSphere.transform.position).normalized;

        Vector3 thisPerp = perpendicularComponent(velocity, n);
        Vector3 otherPerp = perpendicularComponent(otherSphere.velocity, n);

        Vector3 u1 = parallelComponent(velocity, n) * sphereCOR;
        Vector3 u2 = parallelComponent(otherSphere.velocity, n) * sphereCOR;

        float M1 = mass, M2 = otherSphere.mass;

        Vector3 v1 = ((M1 - M2) / (M1 + M2)) * u1 + (2 * M2 / (M1 + M2)) * u2;
        Vector3 v2 = ((M2 - M1) / (M1 + M2)) * u2 + (2 * M1 / (M1 + M2)) * u1;

        velocity = thisPerp + v1;

        otherSphere.newVelocityAfterCollisionwith(this, otherPerp + v2);

    }


    /// <summary>
    /// Sets the new velocity of the other sphere being collided with
    /// </summary>
    /// <param name="codedSphere">The sphere being collided with</param>
    /// <param name="newVelocity">New velocity op the other sphere</param>
    private void newVelocityAfterCollisionwith(CodedSpherePhysics codedSphere, Vector3 newVelocity)
    {
        velocity = newVelocity;
        collisionOccurred = true;
        occurredWith = codedSphere;        
    }
}
