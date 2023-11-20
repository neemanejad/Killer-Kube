using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Scripts.Character
{
    internal interface IPlayableCharacter : ICharacter
    {
        public void ProcessMovementInput(double delta);
        public void ProcessMouseMovement(double delta);
        public void ViewportSizeChanged();
    }
}
