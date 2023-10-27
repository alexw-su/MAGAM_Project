using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


//https://stackoverflow.com/questions/27802185/unity-4-6-editor-create-a-script-with-predefined-data

public class StateMachineCreatorEditor : EditorWindow
{
    #region StateMachine Fields
    public string stateMachineName;
    public string stateMachineStateName;
    public List<string> states = new List<string>();
    #endregion


    private string defaultStateName = "DEFAULT";


    //A Menu Item when clicked will bring up the Editor Window
    [MenuItem("Custom Tools/StateMachine/Create StateMachine")]
    public static void CreateNewChar()
    {
        EditorWindow.GetWindow(typeof(StateMachineCreatorEditor));
    }


    void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Label("Create a State Machine(Marciman Type).\nEnter a name and all the states you want. \nA Base State is already included.");
        GUILayout.Space(10);
        GUILayout.EndVertical();

        //Note on adding more fields
        //The code below is broken into groups, one group per variable
        //While it's relatively long, it keeps the Editor Window clean
        //Most of the code should be fairly obvious

        GUILayout.BeginHorizontal();
        GUILayout.Label("StateMachine Name", new GUILayoutOption[0]);
        stateMachineName = EditorGUILayout.TextField(stateMachineName, new GUILayoutOption[0]);
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Label("StateMachine State Name", new GUILayoutOption[0]);
        stateMachineStateName = EditorGUILayout.TextField(stateMachineStateName, new GUILayoutOption[0]);
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Label("States", new GUILayoutOption[0]);
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        
        for (int i = 0; i < states.Count; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label($"State {i}", new GUILayoutOption[0]);
            states[i] = EditorGUILayout.TextField(states[i], new GUILayoutOption[0]);
            GUILayout.EndHorizontal();
            GUILayout.Space(2);
        }
        
        GUILayout.Space(10);


        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add State", new GUILayoutOption[0]))
        {
            states.Add(defaultStateName);
        }
        if(GUILayout.Button("Remove State", new GUILayoutOption[0]))
        {
            states.RemoveAt(states.Count - 1);
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        GUI.color = Color.green;

        GUILayout.BeginHorizontal();
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        //If we click on the "Done!" button, let's create a new character
        if (GUILayout.Button("Done!", new GUILayoutOption[0]))
            CreateAStateMachine();

    }

    private void CreateAStateMachine()
    {
        AssetDatabase.CreateFolder("Assets/Scripts", stateMachineName);
        AssetDatabase.CreateFolder($"Assets/Scripts/{stateMachineName}", "States");


        //StateMachine ----------------------------
        string enumDeclaration = "";
        for(int i = 0; i < states.Count; i++)
        {
            enumDeclaration += $"\n\t{states[i]},";
        }

        string changeStateCases = "";

        for (int i = 0; i < states.Count; i++)
        {
            changeStateCases +=
                $"\n\t\t" +
                $"case {stateMachineStateName}.{states[i]}:\n" +
                $"\t\t\t" +
                $"SetState(new {stateMachineStateName}_{states[i]}(this));\n" +
                $"\t\t\t" +
                $"break;";
        }


        //Loading the template text file which has some code already in it.
        
        TextAsset templateTextFile = AssetDatabase.LoadAssetAtPath("Assets/EditorTemplates/StateMachineTemplate.txt", typeof(TextAsset)) as TextAsset;
        string contents = "";
        //If the text file is available, lets get the text in it
        //And start replacing the place holder data in it with the 
        //options we created in the editor window
        if (templateTextFile != null)
        {
            contents = templateTextFile.text;
            contents = contents.Replace("STATEMACHINE_NAME", stateMachineName);
            contents = contents.Replace("STATEMACHINESTATE_NAME", stateMachineStateName);
            contents = contents.Replace("STATEENUMS", enumDeclaration);
            contents = contents.Replace("CASE_STATE", changeStateCases);
        }
        else
        {
            Debug.LogError("Can't find the CharacterTemplate.txt file! Is it at the path YOUR_PROJECT/Assets/EditorTemplates/StateMachineTemplate.txt?");
        }

        using (StreamWriter sw = new StreamWriter(
                string.Format(Application.dataPath + $"/Scripts/{stateMachineName}/{stateMachineName}.cs",
                new object[] { stateMachineName.Replace(" ", "") })))
        {
            sw.Write(contents);
        }


        //State Machine Base ------------------

        templateTextFile = AssetDatabase.LoadAssetAtPath("Assets/EditorTemplates/StateMachineBaseStateTemplate.txt", typeof(TextAsset)) as TextAsset;
        contents = "";


        if (templateTextFile != null)
        {
            contents = templateTextFile.text;
            contents = contents.Replace("STATEMACHINE_NAME", stateMachineName);
            contents = contents.Replace("STATEMACHINESTATE_NAME", stateMachineStateName);
        }
        else
        {
            Debug.LogError("Can't find the CharacterTemplate.txt file! Is it at the path YOUR_PROJECT/Assets/EditorTemplates/StateMachineStateBaseTemplate.txt?");
        }

        using (StreamWriter sw = new StreamWriter(
                string.Format(Application.dataPath + $"/Scripts/{stateMachineName}/States/{stateMachineStateName}_Base.cs",
                new object[] { stateMachineName.Replace(" ", "") })))
        {
            sw.Write(contents);
        }


        //State Machine States
        templateTextFile = AssetDatabase.LoadAssetAtPath("Assets/EditorTemplates/StateMachineStateTemplate.txt", typeof(TextAsset)) as TextAsset;

        for (int i = 0; i < states.Count; i++)
        {
            contents = "";

            if (templateTextFile != null)
            {
                contents = templateTextFile.text;
                contents = contents.Replace("STATEMACHINE_NAME", stateMachineName);
                contents = contents.Replace("STATEMACHINESTATE_NAME", stateMachineStateName);
                contents = contents.Replace("STATENAME", states[i]);
            }
            else
            {
                Debug.LogError("Can't find the CharacterTemplate.txt file! Is it at the path YOUR_PROJECT/Assets/EditorTemplates/StateMachineStateBaseTemplate.txt?");
            }

            using (StreamWriter sw = new StreamWriter(
                string.Format(Application.dataPath + $"/Scripts/{stateMachineName}/States/{stateMachineStateName}_{states[i]}.cs",
                new object[] { states[i].Replace(" ", "") })))
            {
                sw.Write(contents);
            }
        }
        //Refresh the Asset Database
        AssetDatabase.Refresh();
    }

}