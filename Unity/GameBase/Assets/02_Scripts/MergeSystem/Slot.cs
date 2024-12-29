using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public enum ESLOTSTATE
    {
        EMPTY,
        FULL
    }

    public int id;
    public Item itemObject;
    public ESLOTSTATE slotState = ESLOTSTATE.EMPTY;

    private void ChangeStateTo(ESLOTSTATE state)
    {
        slotState = state;
    }

    public void ItemGrabbed()
    {
        Destroy(itemObject.gameObject);
        ChangeStateTo(ESLOTSTATE.EMPTY);
    }

    public void CreateItem(int id)
    {
        string itemPath = "Merge/Item_" + id.ToString("000");
        var itemGo = (GameObject)Instantiate(Resources.Load(itemPath));

        itemGo.transform.SetParent(this.transform);
        itemGo.transform.localPosition = Vector3.zero;
        itemGo.transform.localScale = Vector3.one;

        itemObject = itemGo.GetComponent<Item>();
        itemObject.Init(id, this);

        ChangeStateTo(ESLOTSTATE.FULL);
    }

}
