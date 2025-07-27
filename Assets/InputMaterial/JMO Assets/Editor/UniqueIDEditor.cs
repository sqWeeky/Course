using System;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Logic
{
    [CustomEditor(inspectedType: typeof(UniqueID))]
    public class UniqueIDEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueID = (UniqueID)target;

            if (IsPrefab(uniqueID))
                return;

            if (string.IsNullOrEmpty(uniqueID.ID))
            {
                Generate(uniqueID);
            }
            else
            {
                UniqueID[] uniqueIDs = FindObjectsOfType<UniqueID>();

                if (uniqueIDs.Any(other => other == uniqueID && other.ID == uniqueID.ID))
                    Generate(uniqueID);
            }
        }

        private bool IsPrefab(UniqueID uniqueID) =>
            uniqueID.gameObject.scene.rootCount == 0;

        private void Generate(UniqueID uniqueID)
        {
            uniqueID.ID = $"{uniqueID.gameObject.scene.name}_{Guid.NewGuid().ToString()}";

            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(uniqueID);
                EditorSceneManager.MarkSceneDirty(uniqueID.gameObject.scene);
            }
        }
    }
}