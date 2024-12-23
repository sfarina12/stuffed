using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class chatController : MonoBehaviour
{
    [HideInInspector]
    public Dictionary<string, GameObject> favorites = new Dictionary<string, GameObject>();
    public GameObject meggasesInterface;
    public List<SceneUno_dialogueGraph> graphs;
    [Space]
    public GameObject THECAR_favoriteCard;
    public GameObject LContainer;
    public Animator THECAR_animator;
    

    List<string> blackList = new List<string>();
    string selectedCar;
    bool isTheCar = false;

    public void buildGraphics(string text,bool isYou) {
        GameObject msg;
        if (!isYou)
        {
            if (!isTheCar)
                msg = favorites[selectedCar].GetComponent<ADVnodeReader_NoCooroutine>().youPrefab;
            else
                msg = THECAR_favoriteCard.GetComponent<ADVnodeReader_NoCooroutine>().youPrefab;
        }
        else
        {
            if (!isTheCar)
                msg = favorites[selectedCar].GetComponent<ADVnodeReader_NoCooroutine>().hePrefab;
            else
                msg = THECAR_favoriteCard.GetComponent<ADVnodeReader_NoCooroutine>().hePrefab;
        }

        GameObject obj = GameObject.Instantiate(msg,msg.transform.parent);
        obj.transform.position = msg.transform.position;
        obj.transform.rotation = msg.transform.rotation;
        obj.transform.localScale = msg.transform.localScale;
        obj.SetActive(true);

        if(isYou)
            obj.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        else
            obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;

        LayoutRebuilder.ForceRebuildLayoutImmediate(obj.transform.parent.GetComponent<RectTransform>());
    }

    public void clickedisYes(bool yORn) {
        buildGraphics((yORn ? "yes" : "no"), true);
        if(!isTheCar)
            favorites[selectedCar].GetComponent<ADVnodeReader_NoCooroutine>().nextNode(yORn ? "yes" : "no");
        else
            THECAR_favoriteCard.GetComponent<ADVnodeReader_NoCooroutine>().nextNode(yORn ? "yes" : "no");
    }

    public void addFavorite(string name)
    {
        GameObject obj = GameObject.Instantiate(meggasesInterface);
        obj.transform.parent = meggasesInterface.transform.parent;
        obj.transform.position = meggasesInterface.transform.position;
        obj.transform.rotation = meggasesInterface.transform.rotation;
        obj.transform.localScale = meggasesInterface.transform.localScale;

        obj.GetComponent<ADVnodeReader_NoCooroutine>().graph = graphs[Random.RandomRange(0, graphs.Count-1)];
        obj.name = name;

        favorites.Add(name,obj);

        if (blackList.Contains(name))
            disableChat(name);
    }

    public void removeFavorite(string name) {
        GameObject.Destroy(favorites[name].gameObject);
        favorites.Remove(name);
    }

    public void disableChat() {
        if (isTheCar)
        {
            THECAR_favoriteCard.transform.GetChild(1).gameObject.SetActive(false);
            THECAR_favoriteCard.transform.GetChild(2).gameObject.SetActive(true);
            THECAR_animator.Play("end_day_1");
        }
        else
        {
            favorites[selectedCar].transform.GetChild(1).gameObject.SetActive(false);
            favorites[selectedCar].transform.GetChild(2).gameObject.SetActive(true);
            LContainer.transform.Find(selectedCar).GetComponent<Button>().interactable = false;

            blackList.Add(selectedCar);
        }
    }

    public void disableChat(string name)
    {
        favorites[name].transform.GetChild(1).gameObject.SetActive(false);
        favorites[name].transform.GetChild(2).gameObject.SetActive(true);
        LContainer.transform.Find(name).GetComponent<Button>().interactable = false;
    }

    public void openMessaging(GameObject car) {
        closeMessaging();
        favorites[car.name].SetActive(true);
        
        selectedCar = car.name;
        isTheCar = false;
    }

    public void openMessaging(TextMeshProUGUI car)
    {
        closeMessaging();
        favorites[car.text].SetActive(true);

        selectedCar = car.text;
        isTheCar = false;
    }

    public void openMessagingTHECAR()
    {
        closeMessaging();
        THECAR_favoriteCard.SetActive(true);
        isTheCar = true;
    }

    public void closeMessaging()
    {
        foreach (KeyValuePair<string, GameObject> obj in favorites)
            obj.Value.SetActive(false);
        
        THECAR_favoriteCard.SetActive(false);
    }
}
