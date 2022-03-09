using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


#region Explicação

/* 
 * Script dos slots dos itens (tanto da lojo quanto do perfil) 
 * atualiza a UI de cada slot no jogo a partir do item que recebe pelo script itemUI.  
 * pode ser um slot do tipo Loja e do tipo Perfil. se for perfil, recebe a função de equipar o item que possui quando clicado
 * se for loja, abre a caixa de compra com o item que tem no slot 
 
*/

#endregion

public enum slotType
{
    Loja, Perfil
}
public class ItemSlot : MonoBehaviour
{
    public ItemSO item;
    public Image itemSprite;
    public float sizeMultiplier;
    public TextMeshProUGUI priceText;
    public slotType slotType;
    private void Start()
    {
        if (slotType == slotType.Perfil)
        {
            this.GetComponent<Button>().onClick.AddListener(delegate { PerfilManager.Instance.EquipItem(item, false); });
        }else
        {
            this.GetComponent<Button>().onClick.AddListener(delegate { LojaManager.Instance.AtivarJanela(item); });
        }
            
        
        
        
    }

    public void UpdateUI()
    {
        if (item != null)
        {
            
            itemSprite.sprite = item.itemSprite;
            itemSprite.rectTransform.sizeDelta = item.itemSprite.bounds.size * sizeMultiplier;
            if (priceText != null)
            {
                priceText.text = item.price.ToString();
            }
        }
        else
        {
            itemSprite.sprite = null;
            if (priceText != null)
                priceText.text = "0";
        }
        
       
    }
}