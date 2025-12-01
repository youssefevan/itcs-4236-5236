using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public Fighter fighter;
    public Transform bar;

    // public bool flipped = false;


    void Update()
    {
        /*
        if (flipped)
        {
            bar.localScale = new Vector3(-(float)fighter.currentHealth / (float)fighter.maxHealth, bar.localScale.y, bar.localScale.z);
        } else
        {
            bar.localScale = new Vector3((float)fighter.currentHealth / (float)fighter.maxHealth, bar.localScale.y, bar.localScale.z);
        }*/


        bar.localScale = new Vector3((float)fighter.currentHealth / (float)fighter.maxHealth, bar.localScale.y, bar.localScale.z);
        bar.localScale = new Vector3(Mathf.Clamp(bar.localScale.x, 0f, 1f), bar.localScale.y, bar.localScale.z);
        
    }
}
