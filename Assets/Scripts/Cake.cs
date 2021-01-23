using UnityEngine;
using System;

public class Cake : MonoBehaviour
{
    private float radius;
    private int angle;
    private MeshFilter m_f;
    private MeshCollider m_c;
    private MeshRenderer m_r;
    private Mesh mesh;
    private Rigidbody rb;
    public GameObject MultiplierScorePrefab;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        m_f = GetComponent<MeshFilter>();
        m_c = GetComponent<MeshCollider>();
        m_r = GetComponent<MeshRenderer>();
        mesh = new Mesh();
    }

    public void InitCake(float thickness)
    {
        radius = transform.parent.gameObject.GetComponent<Platform>().radius;
        angle = transform.parent.gameObject.GetComponent<Platform>().angle;

        Vector3 v0 = new Vector3(0, 0, 0);
        Vector3 v1 = new Vector3(0, 0, radius);
        Vector3 v2 = new Vector3((float)(radius * Math.Sin(angle * Math.PI / 180)), 0, (float)(radius * Math.Cos(angle * Math.PI / 180)));

        Vector3 v3 = new Vector3(0, -thickness, 0);
        Vector3 v4 = new Vector3(0, -thickness, radius);
        Vector3 v5 = new Vector3((float)(radius * Math.Sin(angle * Math.PI / 180)), -thickness, (float)(radius * Math.Cos(angle * Math.PI / 180)));

        int[] triangles = new int[24];

        for (int i = 0; i < triangles.Length; i++)
            triangles[i] = i;

        mesh.vertices = new Vector3[]{
            v0, v1, v2,
            v3, v5, v4,
            v0, v4, v1,
            v3, v4, v0,
            v3, v2, v5,
            v0, v2, v3,
            v5, v1, v4,
            v2, v1, v5
        };
        
        mesh.triangles = triangles;

        Vector2 p1 = new Vector2(0.1f, 0.1f);
        Vector2 p2 = new Vector2(0.1f, 0.2f);
        Vector2 p3 = new Vector2(0.2f, 0.1f);

        Vector2 p4 = new Vector2(0.1f, 0.8f);
        Vector2 p5 = new Vector2(0.1f, 0.9f);
        Vector2 p6 = new Vector2(0.2f, 0.9f);

        Vector2 p7 = new Vector2(0.8f, 0.1f);
        Vector2 p8 = new Vector2(0.9f, 0.1f);
        Vector2 p9 = new Vector2(0.9f, 0.2f);

        mesh.uv = new Vector2[]{
            p1, p2, p3,
            p1, p2, p3,

            p4, p5, p6,
            p4, p5, p6,
            p4, p5, p6,
            p4, p5, p6,

            p7, p8, p9,
            p7, p8, p9
        };

        m_f.mesh = mesh;
        m_c.sharedMesh = mesh;
    }

    public void CreateMultiplierScorePowerUp()
    {
        GameObject pwrUp = Instantiate(MultiplierScorePrefab, transform.position, Quaternion.identity, transform);
        pwrUp.transform.localPosition = new Vector3(transform.localPosition.x + 0.5f, transform.localPosition.y + 0.5f, transform.localPosition.z + 1.85f);
    }

    public void ChangeColor()
    {
        m_r.material.color = Color.green;
    }

    private void FixedUpdate() 
    {
        if(transform.position.y < -20)
            Destroy(transform.parent.gameObject);
    }

    public void setAbyss()
    {
        m_r.enabled = false;
        m_c.convex = true;
        m_c.isTrigger = true;
        gameObject.tag = "Abyss";
    }

    public void setLet()
    {
        m_r.material.color = Color.red;
        gameObject.tag = "Let";
    }

    public void setGround()
    {
        gameObject.tag = "Ground";
    }
}