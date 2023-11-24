using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Scripts
{
    static internal class Globals
    {
        static private PlayableCharacter target;
        static public PlayableCharacter Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
            }
        }

        static private PackedScene selectedLevel;
        static public PackedScene SelectedLevel
        {
            get => selectedLevel;
            set
            {
                selectedLevel = value;
            }

        }
    }
}
