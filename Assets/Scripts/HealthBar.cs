using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour 
{
    [SerializeField] Image imageFill;
    [SerializeField] Vector3 offSet;
    float hp; float maxHp;
    private Transform target;
    // Update is called once per frame
    void Update()
    {
        imageFill. fillAmount = Mathf.Lerp(imageFill.fillAmount, hp / maxHp, Time.deltaTime * 5f);
        transform.position = target.position + offSet;
    }
    public void OnInit(float maxhp, Transform target)
    {
        this.target = target;
        this.maxHp = maxhp;
        hp = maxHp;
        imageFill.fillAmount = 1;
    }
        public void setNewHp(float hp)
        {
            this.hp = hp;
        }
        
}
