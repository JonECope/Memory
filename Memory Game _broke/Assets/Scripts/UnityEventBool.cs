using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;


// this class allows us to toggle on/off any boolean in the unity editor.
// functionality is in unity scene editor, not code
[System.Serializable] public class UnityEventBool : UnityEvent<bool> { }
