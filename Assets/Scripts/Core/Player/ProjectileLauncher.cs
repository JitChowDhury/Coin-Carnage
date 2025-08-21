using System;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

public class ProjectileLauncher : NetworkBehaviour
{

    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject serverProjectilePrefab;
    [SerializeField] private GameObject clientProjectilePrefab;


    [Header("Settings")]
    [SerializeField] private float projectileSpeed;
    private bool shouldFire;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        inputReader.PrimaryFireEvent += HandlePrimaryFire;

    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) return;
        inputReader.PrimaryFireEvent += HandlePrimaryFire;
    }

    private void Update()
    {
        if (!IsOwner) return;
        if (!shouldFire) return;

        PrimaryFireServerRpc(projectileSpawnPoint.position, projectileSpawnPoint.up);
        spawnDummyProjectile(projectileSpawnPoint.position, projectileSpawnPoint.up);
    }

    private void HandlePrimaryFire(bool shouldFire)
    {
        this.shouldFire = shouldFire;
    }

    private void spawnDummyProjectile(Vector3 spawnPos, Vector3 direction)
    {
        GameObject projectileInstance = Instantiate(clientProjectilePrefab, spawnPos, quaternion.identity);
        projectileInstance.transform.up = direction;
    }
    [ClientRpc]
    private void spawnDummyProjectileClientRpc(Vector3 spawnPos, Vector3 direction)
    {
        if (!IsOwner) return;
        spawnDummyProjectile(spawnPos, direction);
    }
    [ServerRpc]
    private void PrimaryFireServerRpc(Vector3 spawnPos, Vector3 direction)
    {
        GameObject projectileInstance = Instantiate(serverProjectilePrefab, spawnPos, quaternion.identity);
        projectileInstance.transform.up = direction;

        spawnDummyProjectileClientRpc(spawnPos, direction);
    }

}
