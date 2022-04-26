using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintText : MonoBehaviour
{
    /// <summary>
    /// used to turn off hints to allow for text entry use of H key
    /// </summary>
    public bool isFrozen { set; private get; }
    
    /// <summary>
    /// List of puzzles found in the room. Used to determine current hint to display.
    /// </summary>
    public List<Puzzle> RoomPuzzles;

    /// <summary>
    /// TextPrompt script to display hints
    /// </summary>
    protected TextPrompt Text;

    /// <summary>
    /// Update the hint text
    /// </summary>
    /// <param name="text">New hint text</param>
    protected void UpdateHint(string text)
    {
        Text.UpdateTextBox(text);
    }

    /// <summary>
    /// Uses puzzles in room to determine which hint to display
    /// </summary>
    protected virtual void DetermineHint() { }

    // Start is called before the first frame update
    void Start()
    {
        isFrozen = false;
        Text = this.gameObject.GetComponent<TextPrompt>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && !isFrozen)
        {
            DetermineHint();
        }
    }
}
