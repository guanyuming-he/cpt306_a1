using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    public static float masterVolume = 1.0f;
    public static float musicVolume = 1.0f;
    public static float effectsVolume = 1.0f;

    /// <returns>master * music</returns>
    public static float musicStrength()
    {
        return masterVolume * musicVolume;
    }

    /// <returns>master * effects</returns>
    public static float effectsStrength()
    {
        return masterVolume * effectsVolume;
    }
}
