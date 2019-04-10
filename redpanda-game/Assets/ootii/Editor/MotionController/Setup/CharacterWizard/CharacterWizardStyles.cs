using UnityEngine;
using UnityEditor;

namespace com.ootii.Setup
{
    public class CharacterWizardStyles 
    {
        #region Icons
        
        /// <summary>
        /// Box used to group standard GUI elements
        /// </summary>
        private static GUIStyle mMMOIcon = null;
        public static GUIStyle MMOIcon
        {
            get
            {
                if (mMMOIcon == null)
                {
                    Texture2D lTexture = Resources.Load<Texture2D>(EditorGUIUtility.isProSkin ? "MMOIcon" : "MMOIcon");

                    mMMOIcon = new GUIStyle(GUI.skin.box);
                    mMMOIcon.normal.background = lTexture;
                    mMMOIcon.padding = new RectOffset(0, 0, 0, 0);
                    mMMOIcon.border = new RectOffset(0, 0, 0, 0);
                }

                return mMMOIcon;
            }
        }

        /// <summary>
        /// Box used to group standard GUI elements
        /// </summary>
        private static GUIStyle mAdventureIcon = null;
        public static GUIStyle AdventureIcon
        {
            get
            {
                if (mAdventureIcon == null)
                {
                    Texture2D lTexture = Resources.Load<Texture2D>(EditorGUIUtility.isProSkin ? "AdventureIcon" : "AdventureIcon");

                    mAdventureIcon = new GUIStyle(GUI.skin.button);
                    mAdventureIcon.normal.background = lTexture;
                   
                    mAdventureIcon.padding = new RectOffset(0, 0, 0, 0);
                    mAdventureIcon.border = new RectOffset(0, 0, 0, 0);
                }

                return mAdventureIcon;
            }
        }

        /// <summary>
        /// Box used to group standard GUI elements
        /// </summary>
        private static GUIStyle mShooterIcon = null;
        public static GUIStyle ShooterIcon
        {
            get
            {
                if (mShooterIcon == null)
                {
                    Texture2D lTexture = Resources.Load<Texture2D>(EditorGUIUtility.isProSkin ? "ShooterIcon" : "ShooterIcon");

                    mShooterIcon = new GUIStyle(GUI.skin.box);
                    mShooterIcon.normal.background = lTexture;
                    mShooterIcon.padding = new RectOffset(0, 0, 0, 0);
                    mShooterIcon.border = new RectOffset(0, 0, 0, 0);
                }

                return mShooterIcon;
            }
        }

        #endregion Icons
    }

}