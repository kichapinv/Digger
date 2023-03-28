using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if (Game.Map[x + command.DeltaX, y + command.DeltaY] is Digger.Sack) return new CreatureCommand();
            
            return command;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Digger.Terrain) return false;
            else if (conflictedObject is Digger.Sack) return true;
            else if (conflictedObject is Digger.Gold) return false;
            else throw new NotImplementedException();
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
        public int counterInFlight;

        public CreatureCommand Act(int x, int y)
        {
            CreatureCommand command = new CreatureCommand() { DeltaX = 0, DeltaY = 0 };

            if (y + 1 < Game.MapHeight) 
                if (Game.Map[x, y + 1] == null || (Game.Map[x, y + 1] is Digger.Player && counterInFlight > 0)) 
                {
                    counterInFlight++;
                    command.DeltaY++;
                    return command;
                }
            if (counterInFlight > 1) return new CreatureCommand() { TransformTo = new Gold()};
            counterInFlight = 0;
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Digger.Player) return false;
            else throw new NotImplementedException();
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
            if (conflictedObject is Digger.Player)
            {
                Game.Scores = Game.Scores + 10;
                return true;
            }
            else throw new NotImplementedException();
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
