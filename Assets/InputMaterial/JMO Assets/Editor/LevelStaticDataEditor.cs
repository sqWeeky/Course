using Logic;
using Logic.EnemySpawners;
using StaticData;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        private const string InitialPointTag = "InitialPlayerPoint";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                levelData.EnemySpawners =
                    FindObjectsOfType<SpawnMarker>().Select(x =>
                            new EnemySpawnerData(x.GetComponent<UniqueID>().ID, x.MonsterTypeID, x.transform.position))
                        .ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;

                levelData.InitialHeroPosition = GameObject.FindWithTag(InitialPointTag).transform.position;
            }

            EditorUtility.SetDirty(target);
        }
    }
}