using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public Slot[] slots;

    private Vector3 _target;
    private ItemInfo carryingItem;

    private Dictionary<int, Slot> slotDictionary;

    private void Start()
    {
        slotDictionary = new Dictionary<int, Slot>();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].id = i;
            slotDictionary.Add(i, slots[i]);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SendRayCast();
        }

        if (Input.GetMouseButton(0) && carryingItem)
        {
            OnItemSelected();
        }

        if (Input.GetMouseButtonUp(0))
        {
            SendRayCast();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaceRandomItem();
        }
    }

    private void SendRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var slot = hit.transform.GetComponent<Slot>();
            if (slot.slotState == Slot.ESLOTSTATE.FULL && carryingItem == null)
            {
                string itemPath = "Merge/Item_Grabbed_" + slot.itemObject.id.ToString("000");
                var itemGo = (GameObject)Instantiate(Resources.Load(itemPath));
                itemGo.transform.position = slot.transform.position;
                itemGo.transform.localScale = Vector3.one;
                carryingItem = itemGo.GetComponent<ItemInfo>();
                carryingItem.InitDummy(slot.id, slot.itemObject.id);
                slot.ItemGrabbed();
            }
            else if (slot.slotState == Slot.ESLOTSTATE.EMPTY && carryingItem != null)
            {
                slot.CreateItem(carryingItem.itemId);
                Destroy(carryingItem.gameObject);
            }
            else if (slot.slotState == Slot.ESLOTSTATE.FULL && carryingItem != null)
            {
                if (slot.itemObject.id == carryingItem.itemId)
                {
                    OnItemMergedWithTarget(slot.id);
                }
                else
                {
                    OnItemCarryFail();
                }
            }
        }
        else
        {
            if (!carryingItem)
            {
                return;
            }

            OnItemCarryFail();
        }
    }

    private void OnItemSelected()
    {
        _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _target.z = 0;


        var delta = 10 * Time.deltaTime;
        delta *= Vector3.Distance(transform.position, _target);
        carryingItem.transform.position = Vector3.MoveTowards(carryingItem.transform.position, _target, delta);
    }

    private void OnItemMergedWithTarget(int targetSlotId)
    {
        var slot = GetSlotById(targetSlotId);
        Destroy(slot.itemObject.gameObject);
        slot.CreateItem(carryingItem.itemId + 1);
        Destroy(carryingItem.gameObject);
    }

    private void OnItemCarryFail()
    {
        var slot = GetSlotById(carryingItem.slotId);
        slot.CreateItem(carryingItem.itemId);
        Destroy(carryingItem.gameObject);
    }

    private void PlaceRandomItem()
    {
        if (AllSlotsOccupied())
        {
            Debug.Log("슬롯이 다 꽉 찼습니다 => 생성 불가");
            return;
        }

        var rand = Random.Range(0, slots.Length);
        var slot = GetSlotById(rand);

        while (slot.slotState == Slot.ESLOTSTATE.FULL)
        {
            rand = Random.Range(0, slots.Length);
            slot = GetSlotById(rand);
        }

        slot.CreateItem(0);
    }

    private bool AllSlotsOccupied()
    {
        foreach (var slot in slots)
        {
            if (slot.slotState == Slot.ESLOTSTATE.EMPTY)
            {
                return false;
            }
        }

        return true;
    }

    private Slot GetSlotById(int id)
    {
        return slotDictionary[id];
    }
}
