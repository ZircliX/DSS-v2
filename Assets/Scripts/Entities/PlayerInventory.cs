using UnityEngine;

namespace DSS.Entities
{
    public partial class Player
    {
        public int Gold;

        public void AddGold()
        {
            Gold += Random.Range(50, 100);
        }
    }
}