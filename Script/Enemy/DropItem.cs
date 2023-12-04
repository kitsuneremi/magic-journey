using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private EnemyStat enemyStat;
    [SerializeField] private GameObject collectablePrefab;
    void Start()
    {
        enemyStat = GetComponent<EnemyStat>();
    }

    List<ItemData> GetDroppedItem()
    {
        List<ItemData> possibleItems = new List<ItemData>();
        foreach(ItemDropInfo itemDropInfo in enemyStat.listPhase[enemyStat.CurrentPhase].itemDrops)
        {
            int randomNumber = Random.Range(1, 101);

            if (randomNumber <= itemDropInfo.dropRate)
            {
                possibleItems.Add(itemDropInfo.item);
            }
        }
        return possibleItems;
    }

    public void InstantiateItem(Transform spawnPoint)
    {
        List<ItemData> droppedItem = GetDroppedItem();
        List<GameObject> lootItems = new List<GameObject>();
        foreach (ItemData item in droppedItem)
        {
            collectablePrefab.GetComponent<DefaultItem>().itemData = item;
            lootItems.Add(Instantiate(collectablePrefab, spawnPoint.position, Quaternion.identity));
        }
    }
}
