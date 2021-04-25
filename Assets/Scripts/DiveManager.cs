using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveManager : MonoBehaviour
{
	[SerializeField] private float maxPsi;
	public float MaxPsi { get { return maxPsi; } }
    [SerializeField] private float psi;
	public float Psi { get { return psi; } }
	private float depth;
	public float Depth { get { return depth; } }
	[SerializeField] private float airlossRate;
	private PlayerController3 PlayerController;

    void Start()
    {
		psi = maxPsi;
        PlayerController = GetComponent<PlayerController3>();
    }

    
    void Update()
    {
		psi -= airlossRate * Time.deltaTime;;
        psi = Mathf.Clamp(psi, 0f, maxPsi);
		depth = this.transform.position.y;
    }
}
