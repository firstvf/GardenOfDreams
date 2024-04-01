using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }
    public readonly List<Enemy> EnemyList;

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _poolContainer;
    [SerializeField] private Enemy[] _enemyType;
    private MonoBehPooler<Enemy> _enemyPooler;

    public EnemySpawner()
    {
        EnemyList = new List<Enemy>();
    }

    private void Awake()
    => Init();

    private void Start()
    {
        for (int i = 0; i < _enemyType.Length; i++)
        {
            if (_enemyPooler == null)
                _enemyPooler = new MonoBehPooler<Enemy>(_enemyType[i], 2, _poolContainer);
            else _enemyPooler.AddExtraT(_enemyType[i], 2, _poolContainer);
        }
    }

    private void Init()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }

    private async void Spawn(int timeToSpawn = 0)
    {
        if (timeToSpawn < 0)
            await Task.Delay(1 * 1000);
        else await Task.Delay((int)(timeToSpawn * 1000));
        if (!Application.isPlaying) return;
        var enemy = _enemyPooler.GetFromPool();
        EnemyList.Add(enemy);
        enemy.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position; 
    }

    public void RemoveFromList(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        if (EnemyList.Count <= 0)
            Spawn(5);
    }
}