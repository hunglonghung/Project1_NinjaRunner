using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textDisplay;
    public void OnInit(float damage)
    {
        textDisplay.text = damage.ToString();
        Invoke(nameof(OnDespawn), 1f);
    }
    public void OnDespawn()
    {
        Destroy(gameObject);
    }
}