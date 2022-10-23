using Explorus.Model;
using Explorus.Threads;
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

        CompoundGameObject gameObjects;
        public float remainingTime = 0;

        GameView oView;
        int index = 0;

        List<PlayMovement> savedMovementBuffer = new List<PlayMovement>() { };
        List<GameObject> savedRemoveBuffer = new List<GameObject>() { };
        List<CompoundGameObject> savedMaps = new List<CompoundGameObject>();

        public void SetCommand(ICommand command)
        {
            this.commandToBePerformed = command;
        }
        public void ExecuteCommand()
        {
            commandToBePerformed.Execute();


            /*oView = GameView.Instance;
            gameObjects = oView.getMap().GetCompoundGameObject();


            lastCommandPerformed = commandToBePerformed;
            gameObjects = oView.getMap().GetCompoundGameObject();*/

            if (savedCommands.Count > 300)
            {
                savedCommands.RemoveAt(0);
                //savedMaps.RemoveAt(0);
            }

            savedCommands.Add(commandToBePerformed);
            //savedMaps.Add(gameObjects);

        }

        public void UndoCommand()
        {
            lastCommandPerformed.Undo();
        }
        public void UndoAll()
        {
            for (int i = savedCommands.Count; i > 0; i--)
            {
                savedCommands[i - 1].Undo(); // does nothing for now
            }
        }

        public void ExecuteAll(List<CompoundGameObject> savedMaps)
        {
            PhysicsThread physics = PhysicsThread.GetInstance();
            oView = GameView.Instance;
            oView.getMap().generateMapFromCompound(savedMaps[index]);
            //oView.getMap().setMap(savedMaps[index]);
            //PhysicsThread physics = PhysicsThread.GetInstance();
            //physics.addMove(savedMovementBuffer[index]);
            //savedCommands[index].Execute();

            if (savedMaps[0] == savedMaps[savedMaps.Count - 1])
            {
                Console.WriteLine("REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
            }
            if (index == 0)
            {
                //physics.setBuffer(savedMovementBuffer);
            }
            if (index < savedCommands.Count - 1)
            {
                index++;
            }


            if (index == savedCommands.Count - 1)
            {
                index = 0;
                repeatDone = true;
                Console.WriteLine("repeatDone");
            }

            remainingTime = (savedCommands.Count - index) / 60;
        }

        public bool IsRepeatDone()
        { 
            return repeatDone; 
        }

        public void saveMovement(PlayMovement move)
        {
            /*if (savedMovementBuffer.Count > 300)
            {
                savedMovementBuffer.RemoveAt(0);
            }*/
            savedMovementBuffer.Add(move);
        }

        public void saveMap()
        {
            oView = GameView.Instance;
            gameObjects = oView.getMap().GetCompoundGameObject();

            if (savedCommands.Count > 300)
            {
                savedCommands.RemoveAt(0);
                savedMaps.RemoveAt(0);
            }

            savedMaps.Add(gameObjects);
        }


    }
}
