#region License

/*
 
Copyright (c) 2010-2014 Danko Kozar

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 
*/

#endregion License

using System;
using System.Collections.Generic;
using UnityEngine;

namespace eDriven.Gui.Editor.Dialogs
{
    internal class EventHandlerScriptRetriever
    {
        public AddHandlerDataObject Data;
        public GameObject GameObject;

        private List<ScriptDesc> _scripts;
        
        public List<ScriptDesc> Process()
        {
            GameObjectEventHandlerScriptListBuilder builder = new GameObjectEventHandlerScriptListBuilder();
            var typeList = new List<Type>();
                    
            switch (Data.Action)
            {
                //case AddHandlerAction.MapExistingHandler:
                default:
                    // list all the handlers from all the scripts attached to this game object
                    MonoBehaviour[] components = GameObject.GetComponents<MonoBehaviour>();
                    foreach (MonoBehaviour component in components)
                    {
                        typeList.Add(component.GetType());
                    }
                    break;
                case AddHandlerAction.AttachExistingScriptAndMapHandler:
                    // list all handlers from the attached script
                    typeList.Add(Data.AttachedScriptType);
                    break;
                case AddHandlerAction.CreateNewHandlerInExistingScript:
                    // list all handlers from the attached script
                    typeList.Add(Data.AttachedScriptType);
                    break;
                case AddHandlerAction.CreateNewScriptAndHandler:
                    // display the additional dialog asking for a name of the handler to create
                    //typeList.Add(Data.AttachedScriptType);
                    break;
            }

            typeList.Sort(TypeComparison);

            builder.ComponentTypes = typeList;
            
            _scripts = builder.Run();
            //Debug.Log("Number of retrieved scripts: " + _scripts.Count);

            return _scripts;
        }

        private static readonly Comparison<Type> TypeComparison = delegate(Type eh1, Type eh2)
        {
            return eh1.Name.CompareTo(eh2.Name);
        };
    }
}