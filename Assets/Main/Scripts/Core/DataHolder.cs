using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    private static int passCountHolder = 0;

    public static int PassCountHolder => passCountHolder; 

    public void SetPassCountData(int passCount)
    {
        passCountHolder = passCount;
    }
}
