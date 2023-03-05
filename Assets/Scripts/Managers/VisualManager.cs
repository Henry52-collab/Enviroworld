using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualManager : MonoBehaviour
{
    [SerializeField] Color speaking, listening;
    [SerializeField] float spacing;
    [SerializeField] float scaleSpacing;
    [SerializeField] GameObject bg;
    [SerializeField] Sprite[] orderedBgs;
    private int sceneNum = 0;
    [SerializeField] GameObject[] characters;
    private List<GameObject> onStage = new List<GameObject>();
    [SerializeField] GameObject[] charactersR;
    private List<GameObject> onStageR = new List<GameObject>();

    void Start(){
        bg.GetComponent<Image>().sprite = orderedBgs[sceneNum++];
    }

    public void NextScene(){
        if(sceneNum < orderedBgs.Length){
            bg.GetComponent<Image>().sprite = orderedBgs[sceneNum++];
        }
    }

    public void AddCharacter(string characterName, string emotionName, bool lr){
        if(!lr){
            foreach(GameObject c in characters){
                if(c.GetComponent<Character>().GetCharacterName() == characterName){
                    GameObject i = Instantiate(c, bg.transform);
                    i.transform.position += new Vector3(spacing*onStage.Count*scaleSpacing, 0, onStage.Count*scaleSpacing);
                    onStage.Add(i);
                    if(emotionName == null){
                        emotionName = "idle";
                    }
                    CharacterDeliverLine(characterName, emotionName);
                    return;
                }
            }
        }else{
            foreach(GameObject c in charactersR){
                print(c.GetComponent<Character>().GetCharacterName());
                if(c.GetComponent<Character>().GetCharacterName() == characterName){
                    GameObject i = Instantiate(c, bg.transform);
                    i.transform.position -= new Vector3(spacing*onStageR.Count*scaleSpacing, 0, onStageR.Count*scaleSpacing);
                    onStageR.Add(i);
                    if(emotionName == null){
                        emotionName = "idle";
                    }
                    CharacterDeliverLine(characterName, emotionName);
                    return;
                }
            }
        }
    }

    public void CharacterDeliverLine(string characterName, string emotionName)
    { 
        foreach(GameObject i in onStage){
            if(i.GetComponent<Character>().GetCharacterName() == characterName){
                i.GetComponent<Image>().sprite = i.GetComponent<Character>().GetExpressionByExpressionName(emotionName);
                i.GetComponent<Image>().color = speaking;
            } else{
                i.GetComponent<Image>().color = listening;
            }
        }

        foreach(GameObject i in onStageR){
            if(i.GetComponent<Character>().GetCharacterName() == characterName){
                i.GetComponent<Image>().sprite = i.GetComponent<Character>().GetExpressionByExpressionName(emotionName);
                i.GetComponent<Image>().color = speaking;
            } else{
                i.GetComponent<Image>().color = listening;
            }
        }
    }

    public void RemoveCharacter(string characterName)
    {
        print(characterName);
        int iRemove = -1;
        bool found = false;

        foreach(GameObject i in onStage){
            if(found){
                i.transform.position -= new Vector3(spacing*scaleSpacing, 0, scaleSpacing);
                continue;
            }else{
                iRemove++;
            }
            if(i.GetComponent<Character>().GetCharacterName() == characterName){
                print("found in a");
                found = true;
            }
        }
        if(found){
            GameObject go = onStage[iRemove];
            onStage.Remove(go);
            Destroy(go);
            return;
        }
        print("check in b");

        iRemove = -1;

        foreach(GameObject i in onStageR){
            if(found){
                i.transform.position += new Vector3(spacing*scaleSpacing, 0, scaleSpacing);
                continue;
            }else{
                iRemove++;
            }
            if(i.GetComponent<Character>().GetCharacterName() == characterName){
                print("found in b");
                found = true;
            }
        }
        if(found){
            GameObject go = onStageR[iRemove];
            onStageR.Remove(go);
            Destroy(go);
        }
    }



    public bool CharacterInScene(string characterName)
    {
        foreach(GameObject i in onStage){
            if(i.GetComponent<Character>().GetCharacterName() == characterName){
                print("found in a");
                return true;
            }
        }
        foreach(GameObject i in onStageR){
            if(i.GetComponent<Character>().GetCharacterName() == characterName){
                print("found in b");
                return true;
            }
        }
        return false;
    }
    
}