using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killer_Kube.Inventory
{
    public delegate string GetPlayerStatHandler(PlayableCharacter character);

    public partial class PlayerStat
    {
        public string Name;
        public GetPlayerStatHandler GetPlayerStatHandler;
        public PlayerStat(string statName, GetPlayerStatHandler handler)
        {
            Name = statName;
            GetPlayerStatHandler = handler;
        }
    }
}
