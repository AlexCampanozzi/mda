using Explorus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorus.Controller
{
    public class RemoteControl
    {
        public bool repeatDone = false;
        ICommand commandToBePerformed, lastCommandPerformed;
        List<ICommand> savedCommands = new List<ICommand>();
        List<CompoundGameObject> savedMaps = new List<CompoundGameObject>();
        CompoundGameObject gameObjects;

        GameView oView;
        int index = 0;
        /*
        RemoteControl()
        {
            oView = GameView.Instance;
            gameObjects = oView.getMap().GetCompoundGameObject();
        }*/
        public void SetCommand(ICommand command)
        {
            this.commandToBePerformed = command;
        }
        public void ExecuteCommand()
        {
            oView = GameView.Instance;
            gameObjects = oView.getMap().GetCompoundGameObject();

            commandToBePerformed.Execute();
            lastCommandPerformed = commandToBePerformed;
            gameObjects = oView.getMap().GetCompoundGameObject();

            if (savedCommands.Count > 1000000) // circular list need to calculate the right size for 5 sec
            {
                savedCommands.RemoveAt(0);
                savedMaps.RemoveAt(0);
            }

            savedCommands.Add(commandToBePerformed);
            savedMaps.Add(gameObjects);

        }

        public void UndoCommand()
        {
            lastCommandPerformed.Undo();
        }
        public void UndoAll()
        {
            for (int i = savedCommands.Count; i > 0; i--)
            {
                savedCommands[i - 1].Undo();
            }
        }

        public void ExecuteAll()
        {
            //oView.getMap().setMap(gameObjects);
            oView.getMap().setMap(savedMaps[index]);
            //savedCommands[index].Execute();
            index++;

            if (index == savedCommands.Count)
            {
                index = 0;
                repeatDone = true;
            }
        }
    }
}
