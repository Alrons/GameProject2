using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tests 
{
    private string WereInCode;

    public Tests(string wereInCode)
    {
        WereInCode = wereInCode;
    }

    public void StringLength( string String, int MinimalLength, int MaximalLength = default) {
        if (MaximalLength == default) { MaximalLength = (String.Length + 1); }
        if (String.Length > MinimalLength && MaximalLength > String.Length)
        {
            Debug.Log($"<color=green>{WereInCode} String Length: OK</color>");
        }
        
        else { Debug.Log($"<color=red>{WereInCode} String Length: Error </color>"); }
    }
    public void IntRange(int Number, int MinimalRange, int MaximalRange = default)
    {
        if (MaximalRange == default) { MaximalRange = (Number + 1); }
        if ( MinimalRange < Number && MaximalRange > Number) {
            Debug.Log($"<color=green>{WereInCode} Int Range: OK</color>");
        }   
        else { Debug.Log($"<color=red>{WereInCode} Int Range: Error </color>"); }
    }
    public void ForStart(string NameOfMetod, int FirstCount, int SecondCount)
    {
        if (FirstCount == 0)
        {
            Debug.Log($"<color=green>{WereInCode} in {NameOfMetod} Start: OK</color>");
        }
        if (FirstCount == SecondCount-1)
        {
            Debug.Log($"<color=green>{WereInCode} in {NameOfMetod} End: OK</color>");
        }
        
    }
    public void Print(string NameOfMetod)
    {
        Debug.Log($"<color=orange>Script-{WereInCode} Print: {NameOfMetod}</color>");   
    }

}
