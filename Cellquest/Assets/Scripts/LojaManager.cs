using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


#region Explicação

/* 
 * Gerencia a aba loja e o sistema de compra. 
*/

#endregion

public class LojaManager : SingletonSingleScene<LojaManager>
{
    // Start is called before the first frame update
    public itemUI itemUI;
    public ItemSO itemToBuy;
    public Image image;
    public GameObject go;
    public TextMeshProUGUI priceBoxText; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AtivarJanela(ItemSO selectedItem)
    {
        if (selectedItem != null)
        {
            image.sprite = selectedItem.itemSprite;
            image.rectTransform.sizeDelta = selectedItem.itemSprite.bounds.size * 100f;
            priceBoxText.text = selectedItem.price.ToString();
            go.SetActive(true);
            itemToBuy = selectedItem;
        }
    }

    public void Comprar()
    {
       if (itemToBuy != null)
        {
            if (PlayerManager.Instance.GetCoins() >= itemToBuy.price)
            {
                if (itemToBuy.itemType == itemType.Vida)
                {
                    if (PlayerManager.Instance.PlayerData.HP < PlayerManager.Instance.PlayerData.MaxHP)
                    {
                        PlayerManager.Instance.UpdateCoins(-itemToBuy.price);
                        PlayerManager.Instance.UpdateLife(1);
                    }
                }else
                {
                    PlayerManager.Instance.UpdateCoins(-itemToBuy.price);
                    itemToBuy.UnlockItem(true);
                    itemUI.inventory.inventoryList.Remove(itemToBuy);
                    itemUI.SetItem();
                    PerfilManager.Instance.itemUI.SetItem();
                }
                
            }
        }


        go.SetActive(false);
        
    }

    public void Cancelar()
    {
        go.SetActive(false);
        itemToBuy = null;
    }
}
