using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digger
{
    public class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    public class Player : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            CreatureCommand command = new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
            switch (Game.KeyPressed)
            {
                case System.Windows.Forms.Keys.Right:
                    command.DeltaX++;
                    break;
                case System.Windows.Forms.Keys.Left:
                    command.DeltaX--;
                    break;
                case System.Windows.Forms.Keys.Up:
                    command.DeltaY--;
                    break;
                case System.Windows.Forms.Keys.Down:
                    command.DeltaY++;
                    break;
                default:
                    break;
            }
            if (x + command.DeltaX < 0 || x + command.DeltaX >= Game.MapWidth || y + command.DeltaY < 0 ||
                    y + command.DeltaY >= Game.MapHeight) return new CreatureCommand();
            return command;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Digger.png";
        }
    }

    public class Sack : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            throw new NotImplementedException();
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }

    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            throw new NotImplementedException();
        }

        public int GetDrawingPriority()
        {
           return 0;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }
}
