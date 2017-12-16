using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	[SerializeField]
	[Tooltip("Objetivo de la Camara")]
	/// Objetivo actual de camara
	private GameObject currentTarget;

	[SerializeField]
	[Tooltip("Tiempo para llegar al objetivo")]
	/// Duracion de tiempo para moverse a objetivo
	private float duration = 1f;

    [SerializeField]
    [Tooltip("Radio que Marca Estar en Objetivo")]
    /// Radio que Marca Estar en Objetivo
    private float targetRadius = 0.1f;

    [SerializeField]
    [Tooltip("Radio que Marca Estar Cerca del Objetivo")]
    /// Radio que Marca Estar Cerca del Objetivo
    private float slowRadius = 3f;

    #region Temporales
    private Vector3 dir;
    private float dist;
    #endregion

    #region Fisica
    /// Velocidad de Camara
    private Vector3 velocity = Vector3.zero;

    [SerializeField]
    [Tooltip("Velocidad Maxima")]
    /// Velocidad Maxima
    private float maxSpeed = 10f;

    [SerializeField]
    [Tooltip("Aceleracion Maxima")]
    /// Aceleracion Maxima
    private float maxAcc = 10f;
    #endregion

	public bool moveAxisY;

    void Start()
    {
        if (currentTarget == null)
        {
            currentTarget = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void Update()
    {
		if (!moveAxisY) velocity.y = 0;
        transform.position += velocity * Time.deltaTime;
    }

    void LateUpdate () {
        dir = currentTarget.transform.position - transform.position;
        dir.z = 0;
        dist = dir.magnitude;
        if (dist < targetRadius)
        {
            return;
        }

        dir.Normalize();
        dir *= maxSpeed;

        if (dist <= slowRadius)
        {
            dir *= dist / slowRadius;
        }

        dir -= velocity;

        dir /= duration;

        if (dir.sqrMagnitude > maxAcc * maxAcc)
        {
            dir.Normalize();
            dir *= maxAcc;
        }

        velocity += dir * Time.deltaTime;
        if (velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            velocity.Normalize();
            velocity *= maxSpeed;
        }
    }
}
