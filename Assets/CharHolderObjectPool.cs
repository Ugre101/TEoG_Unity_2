using System.Collections.Generic;
using UnityEngine;

public class CharHolderObjectPool : MonoBehaviour
{
    public static CharHolderObjectPool Instance { get; private set; }
    [SerializeField] private Transform enemyHoldersContainer = null, bossHoldersContainer = null;
    [SerializeField] private EnemyHolder enemyHolderPrefab = null;
    [SerializeField] private BossHolder bossHolderPrefab = null;
    private Queue<EnemyHolder> enemyHolders;
    private Queue<BossHolder> bossHolders;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            enemyHolders = new Queue<EnemyHolder>(enemyHoldersContainer.GetComponentsInChildren<EnemyHolder>());
            bossHolders = new Queue<BossHolder>(bossHoldersContainer.GetComponentsInChildren<BossHolder>());
        }
        else
            Destroy(gameObject);
    }

    public EnemyHolder GetEnemyHolder() => enemyHolders.Count > 0 ? enemyHolders.Dequeue() : Instantiate(enemyHolderPrefab);

    public void ReturnEnemyHolder(EnemyHolder holder)
    {
        holder.transform.SetParent(enemyHoldersContainer);
        holder.gameObject.SetActive(false);
        enemyHolders.Enqueue(holder);
    }

    public BossHolder GetBossHolder() => bossHolders.Count > 0 ? bossHolders.Dequeue() : Instantiate(bossHolderPrefab);

    public void ReturnBossHolder(BossHolder holder)
    {
        holder.transform.SetParent(bossHoldersContainer);
        holder.gameObject.SetActive(false);
        bossHolders.Enqueue(holder);
    }
}