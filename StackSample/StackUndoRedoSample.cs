using System;
using System.Collections.Generic;

namespace UndoRedoStack
{
    // DTO class representing an action
    public class ActionDTO
    {
        public string ActionName { get; set; }
        public object ActionData { get; set; }
    }

    public class UndoRedoManager
    {
        private Stack<ActionDTO> undoStack = new Stack<ActionDTO>();
        private Stack<ActionDTO> redoStack = new Stack<ActionDTO>();

        // Push a new action to the undo stack
        public void ExecuteAction(ActionDTO action)
        {
            undoStack.Push(action);
            redoStack.Clear(); // Clear the redo stack whenever a new action is executed
            Console.WriteLine($"Action executed: {action.ActionName}");
        }

        // Undo the last action
        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                ActionDTO action = undoStack.Pop();
                redoStack.Push(action);
                Console.WriteLine($"Action undone: {action.ActionName}");
            }
            else
            {
                Console.WriteLine("No actions to undo.");
            }
        }

        // Redo the last undone action
        public void Redo()
        {
            if (redoStack.Count > 0)
            {
                ActionDTO action = redoStack.Pop();
                undoStack.Push(action);
                Console.WriteLine($"Action redone: {action.ActionName}");
            }
            else
            {
                Console.WriteLine("No actions to redo.");
            }
        }

        // Access the top action on the undo stack
        public ActionDTO PeekUndo()
        {
            return undoStack.Count > 0 ? undoStack.Peek() : null;
        }

        // Access the top action on the redo stack
        public ActionDTO PeekRedo()
        {
            return redoStack.Count > 0 ? redoStack.Peek() : null;
        }

        // Search for an action in the undo stack
        public bool SearchUndo(string actionName)
        {
            foreach (var action in undoStack)
            {
                if (action.ActionName == actionName)
                {
                    return true;
                }
            }
            return false;
        }

        // Search for an action in the redo stack
        public bool SearchRedo(string actionName)
        {
            foreach (var action in redoStack)
            {
                if (action.ActionName == actionName)
                {
                    return true;
                }
            }
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            UndoRedoManager manager = new UndoRedoManager();

            // Execute some actions
            manager.ExecuteAction(new ActionDTO { ActionName = "Action1", ActionData = "Data1" });
            manager.ExecuteAction(new ActionDTO { ActionName = "Action2", ActionData = "Data2" });

            // Undo the last action
            manager.Undo();

            // Redo the last undone action
            manager.Redo();

            // Access the top action on the undo stack
            ActionDTO topUndoAction = manager.PeekUndo();
            Console.WriteLine($"Top Undo Action: {topUndoAction?.ActionName}");

            // Search for an action in the undo stack
            bool found = manager.SearchUndo("Action1");
            Console.WriteLine($"Action1 found in undo stack: {found}");
        }
    }
}