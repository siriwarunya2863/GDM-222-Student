using UnityEngine;

namespace Solution
{

    public class OOPWall : Identity
    {
        public int Damage;
        public bool IsIceWall;

        private new void SetUP()
        {
            IsIceWall = Random.Range(0, 100) < 20;
            if (IsIceWall)
            {
                GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }
        
        public override void Hit(Identity hitBy)
        {
            if (IsIceWall)
            {
                mapGenerator.playerScript.TakeDamage(Damage, IsIceWall);
            }
            else
            {
                mapGenerator.playerScript.TakeDamage(Damage);
            }
            mapGenerator.mapdata[positionX, positionY] = null;
            Destroy(gameObject);
        }
    }
}