using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] string characterName ;
    [SerializeField] private string[] expressionNames = {"idle", "confused", "angery"};
    [SerializeField] Sprite[] expressions;

    public Sprite GetExpressionByExpressionName(string expressionName){
        for(int i = 0; i<expressionNames.Length; i++){
            if(expressionNames[i] == expressionName){
                return expressions[i];
            }
        }
        return null;
    }

    public string GetCharacterName(){
        return characterName;
    }
}
