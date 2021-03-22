using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SelectCharacter : MonoBehaviour
{
    private GraphicRaycaster rc;
    private PointerEventData pt;
    private EventSystem eventSystem;

    public Image nameDisplay;               //The Image element that displays the selected character's name
    public Image descriptionDisplay;        //The Image element that displays the selected character's description
    public Image backgroundImage;           //The Image element that displays the background image based on selected character
    public Image characterDisplay;          //The Image element that displays the selected character's image

    public GameObject[] characters;         //Array with the clickable character images

    public Sprite[] characterNames;         //Array with the character name sprites
    public Sprite[] characterImages;        //Array with the character images
    public Sprite[] characterDescriptions;  //Array with the character descriptions
    public Sprite[] characterBackgrounds;   //Array with the character backgrounds

    // Start is called before the first frame update
    void Start()
    {
        //Obtain the Canvas' Raycaster and EventSystem Components
        rc = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        pt = new PointerEventData(eventSystem);
        pt.position = UnityEngine.InputSystem.Mouse.current.position.ReadValue();

        List<RaycastResult> results = new List<RaycastResult>();
        rc.Raycast(pt, results);

        if (Mouse.current.leftButton.isPressed)
        {
            foreach (RaycastResult target in results)
            {
                GameObject chr;
                for (int i = 0; i < characters.Length; i++)
                {
                    chr = characters[i];
                    if(target.gameObject == chr)
                    {
                        SwitchSelectedCharacter(i);
                    }
                }
            }
        }       
    }

    void SwitchSelectedCharacter(int index)
    {
        nameDisplay.sprite = characterNames[index];
        descriptionDisplay.sprite = characterDescriptions[index];
        backgroundImage.sprite = characterBackgrounds[index];
        characterDisplay.sprite = characterImages[index];
    }

    public void PreviousScene(int sceneIndex)
    {
        Loading.instance.LoadingScene(sceneIndex);
       
    }

    public void NextScene(int sceneIndex)
    {
        Loading.instance.LoadingScene(sceneIndex);
    }
}
