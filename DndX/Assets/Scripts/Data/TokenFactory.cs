using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenFactory : MonoBehaviour
{

    private static Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();

    // Start is called before the first frame update
    void Start()
    {

        //Load a Sprite (Assets/Resources/Sprites/sprite01.png)
        var sprite = Resources.Load<Sprite>("Sprites/Square");
        spriteDictionary.Add("Wall", sprite);

        // load the tokens

        // save them to a dictionary

        // make a loop up method that returns the token

    }

    public static Sprite GetToken(string tokenName) {
        Sprite result = null;
        if (spriteDictionary.ContainsKey(tokenName)) {
            result = spriteDictionary[tokenName];
        }
        return result;
    }
}
