using System;
using BepInEx;
using GorillaLocomotion;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilla.Attributes;

namespace MiniInModded
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;
        public GameObject gameobject { get; private set; }
        public GameObject prefab;

        void Start()
        {

            /* A lot of Gorilla Tag systems will not be set up when start is called /*
			/* Put code in OnGameInitialized to avoid null references */

            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            Instantiate(gameobject, this.gameObject.transform);

            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {

            /* Undo mod setup here */
            /* This provides support for toggling mods with ComputerInterface, please implement it :) */
            /* Code here runs whenever your mod is disabled (including if it disabled on startup)*/
            Player.Instance.scale = 1f;
            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            Player.Instance.scale = 1f;
            /* Code here runs after the game initializes (i.e. GorillaLocomotion.Player.Instance != null) */
        }

        void Update()
        {

        }

        /* This attribute tells Utilla to call this method when a modded room is joined */
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;
            SceneManager.LoadScene("Basement", LoadSceneMode.Additive);
            GameObject.Find("Basement/DungeonRoomAnchor/DungeonBasement/BasementMusicSpeaker").SetActive(false);
            GameObject.Find("Basement/DungeonRoomAnchor/DungeonBasement/MazeSizeChangers/TinySizerEntrance");
            GameObject gameobject = GameObject.Find("Basement/DungeonRoomAnchor/DungeonBasement/MazeSizeChangers/TinySizerEntrance");
            gameobject.transform.localScale = new Vector3(9999f, 9999f, 9999f);
            RemoveAnnoyingAmbience();
                        Instantiate(gameobject, this.gameObject.transform);

            /* Activate your mod here */
            /* This code will run regardless of if the mod is enabled*/

        }

        /* This attribute tells Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            /* Deactivate your mod here */
            /* This code will run regardless of if the mod is enabled*/
            Player.Instance.scale = 1f;
            SetPlayerPosition();
            inRoom = false;
        }
        void SetPlayerPosition()
        {
            Player.Instance.transform.position = new Vector3(-68.5887f, 12.0883f, -83.958f);
        }
        void RemoveAnnoyingAmbience()
        {
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Environment/WeatherDayNight/AudioCrickets").SetActive(false);
            GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Environment/WeatherDayNight/rain").SetActive(false);
        }
        private void OnActiveSceneChanged(Scene previousScene, Scene newScene)
        {
            // Instantiate the gameobject when the active scene changes
            Instantiate(gameobject, this.gameObject.transform);
        }
    }
}
