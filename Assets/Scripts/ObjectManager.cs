using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    [Header ("Objects")]
    [SerializeField] private GameObject[] objects;
    [SerializeField] private TMP_Dropdown objectSelector;
    [SerializeField] private GameObject animationsUI;

    [Header ("ColorControll")]
    [SerializeField] private Slider redSlider; 
    [SerializeField] private Slider greenSlider, blueSlider;
    private float red, green, blue;

    [Header ("Controls")]
    [SerializeField] private Button moveLeft; 
    [SerializeField] private Button moveRight, moveForward, moveBackward, rotateLeft, rotateRight;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 1000f;

    [Header ("Animations Control")]
    [SerializeField] TMP_Dropdown animationsSelector;

    [Header ("ReadOnly")]
    [SerializeField] private Material currentMaterial;
    [SerializeField] private GameObject currentlySelectedObject;

    private Animator currentAnimator;

    private void Start()
    {
        InitializeObjectsDropdown();
        InitializeColorSliders();
        InitializeButtons();

        animationsUI.SetActive(false);
        DeactivateAllObjects();
    }
#region Initialization
    private void InitializeObjectsDropdown()
    {
        //Initialize object selector dropdown
        objectSelector.onValueChanged.AddListener(ChangeObject);
        objectSelector.value = 0;
    }

    private void InitializeColorSliders()
    {
        //Sliders initialisation (connection)
        redSlider.onValueChanged.AddListener(value => UpdateColor("r", value));
        greenSlider.onValueChanged.AddListener(value => UpdateColor("g", value));
        blueSlider.onValueChanged.AddListener(value => UpdateColor("b", value));
    }

    private void InitializeButtons()
    {
        //Buttons initialisation (connection)
        moveLeft.onClick.AddListener(() => Move(Vector3.left));
        moveRight.onClick.AddListener(() => Move(Vector3.right));
        moveForward.onClick.AddListener(() => Move(Vector3.forward));
        moveBackward.onClick.AddListener(() => Move(Vector3.back));
        rotateLeft.onClick.AddListener(() => Rotate(-1));
        rotateRight.onClick.AddListener(() => Rotate(1));
    }
#endregion

#region Object Management
    public void ChangeObject(int newIndex)
    {
        //Prevent animation bug
        if (currentlySelectedObject != null && currentlySelectedObject.GetComponent<Animator>() != null)
        {
            Animator animator = currentlySelectedObject.GetComponent<Animator>();
            animator.Play("Idle"); // Assuming the idle animation is named "Idle"
            animator.Update(0);   // Immediately apply the animation frame
        }

        // Disable all objects
        DeactivateAllObjects();

        // If none selected, return early
        if (newIndex == 0 || objects[newIndex] == null)
            return;

        // Setup new object
        currentlySelectedObject = objects[newIndex];
        if (!currentlySelectedObject.TryGetComponent(out SelectedObject selectedObject))
        {
            Debug.LogWarning("Selected object is missing the SelectedObject component.");
            return;
        }

        //Apply material
        currentMaterial = selectedObject.myMaterial;
        //Return values of previously modifyed material to sliders
        ResetColorSliders(currentMaterial.GetColor("baseColorFactor"));

        //Try to get animator and animations
        InitializeAnimatorDropdown(currentlySelectedObject);
        //Setup finished enable selected GameObject
        currentlySelectedObject.SetActive(true);
    }
    public void ChangeObjectByImage(GameObject newObject)
    {
        if (currentlySelectedObject != null && currentlySelectedObject.GetComponent<Animator>() != null)
        {
            Animator animator = currentlySelectedObject.GetComponent<Animator>();
            animator.Play("Idle"); // Assuming the idle animation is named "Idle"
            animator.Update(0);   // Immediately apply the animation frame
        }

        currentlySelectedObject = newObject;
        if (!currentlySelectedObject.TryGetComponent(out SelectedObject selectedObject))
        {
            Debug.LogWarning("Selected object is missing the SelectedObject component.");
            return;
        }
        currentMaterial = selectedObject.myMaterial;
        ResetColorSliders(currentMaterial.GetColor("baseColorFactor"));
        InitializeAnimatorDropdown(currentlySelectedObject);
    }

    private void DeactivateAllObjects()
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null) obj.SetActive(false);
        }
    }

    private void ResetColorSliders(Color baseColor)
    {
        redSlider.value = baseColor.r;
        greenSlider.value = baseColor.g;
        blueSlider.value = baseColor.b;
    }

#endregion

#region  Color
    //If user touch sliders that will triggger UpdateColor function
    public void UpdateColor(string channel, float value)
    {
        if (currentMaterial == null)
            return;
        switch (channel)
        {
            case "r": red = value; break;
            case "g": green = value; break;
            case "b": blue = value; break;
        }

        Color updatedColor = new Color(red, green, blue);
        currentMaterial.SetColor("baseColorFactor", updatedColor);
    }

#endregion

#region Movement
    //Apply buttons press and translate then into movement
    private void Move(Vector3 direction)
    {
        if (currentlySelectedObject == null) return;
        currentlySelectedObject.transform.Translate(direction * movementSpeed * Time.deltaTime);
    }
    // and rotation
    private void Rotate(int direction)
    {
        if (currentlySelectedObject == null) return;
        currentlySelectedObject.transform.Rotate(Vector3.up, direction * rotationSpeed * Time.deltaTime);
    }
#endregion

#region Animations
    private void InitializeAnimatorDropdown(GameObject targetObject)
    {
        //Check does the object have an animator controller
        if (targetObject.TryGetComponent(out Animator animator))
        {
            currentAnimator = animator;
            //Clear drop down list
            animationsSelector.ClearOptions();
            List<string> animationNames = new List<string>();
            //Get all animations at the Animator controller and fill the list
            foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
                animationNames.Add(clip.name);
            //add newly created list to the dropdown options
            animationsSelector.AddOptions(animationNames);
            #region BUG, need to apply another solution to play animations.
            // umph, that a bit buggy - if we select the same animation it will not be played, 
            animationsSelector.onValueChanged.AddListener(PlaySelectedAnimation); 
            //guess will figure it out later.
            //yeah - i can fix that by adding a special button, to trigger seleected animation, but i decided to show - i'm cheking my code and looking for bugs before push it 
            #endregion
            animationsUI.SetActive(true);
        }
        else
        {
            animationsUI.SetActive(false);
            currentAnimator = null;
        }
    }

    private void PlaySelectedAnimation(int index)
    {
        if (currentAnimator == null) 
            return;
        //Animations playd by dropdown name
        string selectedAnimation = animationsSelector.options[index].text;
        currentAnimator.Play(selectedAnimation);
    }
#endregion
}
