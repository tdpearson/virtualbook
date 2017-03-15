using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBook : MonoBehaviour {

    public string bookpath;
    public float width = 1.0f;
    public float height = 0.0025f;
    public float length = 0.7f;

	// Use this for initialization
	void Start () {

        Debug.Log("Adding book to: " + this.gameObject.name);
        MegaBookBuilder book = this.gameObject.AddComponent<MegaBookBuilder>();

	//TODO: Create materials: https://docs.unity3d.com/ScriptReference/AssetDatabase.CreateAsset.html
	//TODO: Load textures (images): https://docs.unity3d.com/ScriptReference/Texture2D.LoadImage.html

        book.pagesectioncrv = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
        //book.basematerial = 
        //book.basematerial1 = 
        //book.basematerial2 = 

        //TODO: iterate over actual page count
	for (int i = 0; i < 10; i++)
        {
            MegaBookPageParams page = new MegaBookPageParams();

            if (i == 0)
            {
                page.front = (Texture2D)Resources.Load("Textures/001", typeof(Texture2D));
                page.back = (Texture2D)Resources.Load("Textures/001", typeof(Texture2D));
            }
            else
            {
                if (i == 10 - 1)
                {
                    page.front = (Texture2D)Resources.Load("Textures/001", typeof(Texture2D));
                    page.back = (Texture2D)Resources.Load("Textures/001", typeof(Texture2D));
                }
                else
                {
                    page.front = (Texture2D)Resources.Load("Textures/001", typeof(Texture2D));
                    page.back = (Texture2D)Resources.Load("Textures/001", typeof(Texture2D));
                }
            }

            book.pageparams.Add(page);
        }

        book.rebuild = true;
    }
	
}
