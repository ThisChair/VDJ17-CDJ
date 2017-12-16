using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	[SerializeField]
	[Tooltip("Objetivo de la Camara")]
	/// <summary>
	/// Objetivo actual de camara
	/// </summary>
	private GameObject currentTarget;

	[SerializeField]
	[Tooltip("Tiempo para llegar al objetivo")]
	/// <summary>
	/// Duracion de tiempo para moverse a objetivo
	/// </summary>
	private float duration = 1f;

    [SerializeField]
    [Tooltip("Radio que Marca Estar en Objetivo")]
    /// <summary>
    /// Radio que Marca Estar en Objetivo
    /// </summary>
    private float targetRadius = 0.1f;

    [SerializeField]
    [Tooltip("Radio que Marca Estar Cerca del Objetivo")]
    /// <summary>
    /// Radio que Marca Estar Cerca del Objetivo
    /// </summary>
    private float slowRadius = 3f;

    #region Temporales
    private Vector3 dir;
    private float dist;
    #endregion

    #region Fisica
    /// <summary>
    /// Velocidad de Camara
    /// </summary>
    private Vector3 velocity = Vector3.zero;

    [SerializeField]
    [Tooltip("Velocidad Maxima")]
    /// <summary>
    /// Velocidad Maxima
    /// </summary>
    private float maxSpeed = 10f;

    [SerializeField]
    [Tooltip("Aceleracion Maxima")]
    /// <summary>
    /// Aceleracion Maxima
    /// </summary>
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
