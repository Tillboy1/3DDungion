using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "Healing Word", menuName = "Scriptable Objects/Support/HealingWord")]
public class HealingWord : AbilitiesBase
{
    public int AmountToHeal;
    public override void Activate(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        if (stats.Targeting != null)
        {
            bool Abletosee = true;
            /*
            RaycastHit rayinfo;

            Vector3 rotation = stats.Targeting.transform.position - stats.AttackPoint.transform.position;

            float rotY = Mathf.Atan2(-rotation.z, rotation.x) * Mathf.Rad2Deg;
            stats.AttackPoint.transform.rotation = Quaternion.Euler(0, rotY, 0);

            if (Physics.Raycast(stats.AttackPoint.transform.position + new Vector3(0, 17f, 0), new Vector3(0, rotY, 0), out rayinfo, Vector3.Distance(stats.AttackPoint.transform.position, stats.Targeting.transform.position) + 4f))
            {
                Debug.DrawLine(stats.AttackPoint.transform.position, stats.Targeting.transform.position);

                if (rayinfo.collider.GetComponent<PlayerStats>() || rayinfo.collider.GetComponent<BaseEnemy>())
                {
                    Debug.Log("Working");
                    if (rayinfo.collider.gameObject == stats.Targeting.gameObject)
                    {
                        Debug.Log("Hitting target");
                        Abletosee = true;
                    }
                    else
                    {
                        Debug.Log(rayinfo.collider.name);
                    }
                }
                Debug.Log(rayinfo.collider.gameObject.name);
            }
            */

            float distance = Vector3.Distance(stats.AttackPoint.transform.position, stats.Targeting.transform.position);

            if (Abletosee)
            {
                if (distance <= AbilityRange)
                {
                    stats.Targeting.transform.GetComponent<PlayerStats>().BaseHeal(AmountToHeal);
                }
            }
            else
            {
                Debug.Log("didn't see them ");
            }
        }
    }
}
