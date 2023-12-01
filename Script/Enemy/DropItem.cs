using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private EnemyStat enemyData;
    [SerializeField] private GameObject collectablePrefab;
    void Start()
    {
        enemyData = GetComponent<EnemyStat>();
    }

    List<ItemData> GetDroppedItem()
    {
        List<ItemData> possibleItems = new List<ItemData>();
        foreach(ItemDropInfo itemDropInfo in enemyData.enemyData.itemDrops)
        {
            int randomNumber = Random.Range(1, 101);

            if (randomNumber <= itemDropInfo.dropRate)
            {
                Debug.Log(itemDropInfo.item.display_name);
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
