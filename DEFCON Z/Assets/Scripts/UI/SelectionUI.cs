using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ.UI
{
    public class SelectionUI : MonoBehaviour
    {
        public bool draw;

        public Vector3 initialMousePosition;

        public Color selectionColor;
        public Color selectionBorderColor;
        public float borderThickness;
        public Texture2D selectionTexture;

        public void Awake()
        {
            selectionTexture = new Texture2D(1, 1);
            selectionTexture.SetPixel(0, 0, selectionColor);
            selectionTexture.Apply();
        }

        void OnGUI()
        {
            if (draw)
            {
                DrawSelectionBox();
            }
        }

        /// <summary>
        /// Draws a selection box with border
        /// </summary>
        public void DrawSelectionBox()
        {
            Rect rect = GetScreenSpaceRectangle(initialMousePosition, Input.mousePosition);
            DrawRectangle(rect, selectionColor);
            DrawRectangleBorder(rect, selectionBorderColor);
        }

        /// <summary>
        /// Draws a rectangle
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="color"></param>
        public void DrawRectangle(Rect rect, Color color)
        {
            GUI.color = color;
            GUI.DrawTexture(rect, selectionTexture);
            GUI.color = Color.white;
        }

        /// <summary>
        /// Draws a rectangular border
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="color"></param>
        public void DrawRectangleBorder(Rect rect, Color color)
        {
            // Top border
            DrawRectangle(new Rect(rect.xMin, rect.yMin, rect.width, borderThickness), color);
            // Right border
            DrawRectangle(new Rect(rect.xMax - borderThickness, rect.yMin, borderThickness, rect.height), color);
            // Bottom border
            DrawRectangle(new Rect(rect.xMin, rect.yMax - borderThickness, rect.width, borderThickness), color);
            // Left border
            DrawRectangle(new Rect(rect.xMin, rect.yMin, borderThickness, rect.height), color);
        }

        /// <summary>
        /// Gets the screenspace coordinates for a rectangle
        /// </summary>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <returns></returns>
        public static Rect GetScreenSpaceRectangle(Vector3 pos1, Vector3 pos2)
        {
            pos1.y = Screen.height - pos1.y;
            pos2.y = Screen.height - pos2.y;

            Vector3 tl = Vector3.Min(pos1, pos2);
            Vector3 br = Vector3.Max(pos1, pos2);

            return Rect.MinMaxRect(tl.x, tl.y, br.x, br.y);
        }
    }
}