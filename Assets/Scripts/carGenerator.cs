using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class carGenerator : MonoBehaviour
{
    public TextAsset conditionFile;
    public TextAsset userFile;
    public TextAsset contentFile;
    public TextAsset carNameFile;
    public TextAsset imgFile;
    public List<Sprite> imgs;
    [Space]
    public chatController chat;
    [Space]
    public Animator favAnimator;

    List<carCardMessage> msgs;
    List<carCard> cards;
    List<int> msgDivision = new List<int>{ 4,3,3,3,3,3,2,2,2,2,2,2,1,1,1,1,2 };
    Dictionary<carCard, GameObject> favorites = new Dictionary<carCard, GameObject>();
    int divCount = 0;

    public Vector2 maxMessagesPerCar;
    public Vector2 maxCost;

    public int iterations;
    [Space]
    public GameObject tameplate;
    public GameObject tameplateFavorite;
    public Image image;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI nameCar;
    public GameObject tameplateMsg;
    [Space]
    public GameObject TheCarCard;
    public GameObject TheCarFavoriteCard;
    public GameObject TheCarFavoriteBtn;

    public void clickedCar(TextMeshProUGUI carName) {
        TheCarCard.SetActive(false);
        carCard car = cards[findCar(carName.text)];

        cost.text = car.cost.ToString();
        nameCar.text = car.name.ToString();
        image.sprite = car.img;

        buildGraphicsMsg(cards[findCar(carName.text)]);
    }

    public void clickerTHECAR()
    {
        TheCarCard.SetActive(true);
    }

    public void clickFav(TextMeshProUGUI carName) {

        int f = findCar(carName.text);

        if (favorites.ContainsKey(cards[f]))
        {
            favAnimator.Play("unfavorite");
            GameObject.Destroy(favorites[cards[f]].gameObject);
            favorites.Remove(cards[f]);
            
            chat.removeFavorite(cards[f].name);
        }
        else
        {
            favAnimator.Play("favorite");
            GameObject obj = buildGraphics(cards[f], tameplateFavorite,false);
            obj.name = cards[f].name;
            favorites.Add(cards[f], obj);

            chat.addFavorite(cards[f].name);
            chat.closeMessaging();
        }
    }

    bool isFave = false;
    public void clickFavTHECAR() {
        isFave = !isFave;
        TheCarFavoriteBtn.SetActive(isFave);
    }

    private int findCar(string text)
    {
        int i = 0;
        foreach (carCard c in cards)
        {
            if(c.name.Equals(text))
                return i;

            i++;
        }

        return -1;
    }

    void Start()
    {
        
        randomizeMsgDivision();
        
        msgs = new List<carCardMessage>();
        cards = new List<carCard>();
        msgs = generateMessages();
        
        genherateCards();

        foreach(carCard c in cards)
            buildGraphics(c,tameplate,true);

        TextMeshProUGUI txt = new TextMeshProUGUI();
        txt.text = cards[0].name;
        clickedCar(txt);
    }

    private GameObject buildGraphics(carCard c,GameObject templ,bool randomPos)
    {
        GameObject tmp = Instantiate(templ);
        tmp.transform.SetParent(templ.transform.parent,false);

        tmp.transform.Find("bg").Find("name").GetComponent<TextMeshProUGUI>().text = c.name;
        tmp.transform.Find("bg").Find("desc").GetComponent<TextMeshProUGUI>().text = c.condition;
        tmp.transform.Find("bg").Find("ImageContainer").Find("Image").GetComponent<Image>().sprite = c.img;

        System.Random rnd = new System.Random();
        int num = rnd.Next(100);

        if ((num%3==0) && randomPos)
            tmp.transform.SetAsFirstSibling();

        tmp.SetActive(true);

        return tmp;
    }

    private void buildGraphicsMsg(carCard c)
    {
        foreach (Transform m in tameplateMsg.transform.parent)
            if (!m.name.Equals(tameplateMsg.name))
                Destroy(m.gameObject);
        
        foreach (carCardMessage m in c.messages)
        {
            GameObject tmp = Instantiate(tameplateMsg);
            tmp.transform.SetParent(tameplateMsg.transform.parent, false);

            tmp.transform.Find("template").Find("usrName").GetComponent<TextMeshProUGUI>().text = m.usr;
            tmp.transform.Find("template").Find("usrMsg").GetComponent<TextMeshProUGUI>().text = m.content;

            tmp.SetActive(true);
        }
    }

    private void randomizeMsgDivision()
    {
        System.Random rnd = new System.Random();

        for (int i=0;i<iterations;i++) 
        {
            int appR_1 = rnd.Next(msgDivision.Count);
            int appV_1 = msgDivision[appR_1];

            int appR_2 = rnd.Next(msgDivision.Count);
            int appV_2 = msgDivision[appR_2];

            msgDivision[appR_2] = appV_1;
            msgDivision[appR_1] = appV_2;
        }
    }

    private void genherateCards()
    {
        string[] cond = conditionFile.text.Split('[');
        List<string> conds = new List<string>();
        foreach (string c in cond)
            conds.Add(c);

        string[] name = carNameFile.text.Split('[');
        List<string> names = new List<string>();
        foreach (string n in name)
            names.Add(n);

        for (int i = 0; i < 17; i++)
        {
            List<carCardMessage> mm = generateMsgForCard();

            carCard car = new carCard(imgs[i],names[i],conds[i],generateCost(),mm);
            cards.Add(car);
        }
    }

    int msgCounter = 0;
    private List<carCardMessage> generateMsgForCard()
    {
        List<carCardMessage> m = new List<carCardMessage>();
        
        for (int i = 0; i < msgDivision[divCount]; i++, msgCounter++)
            m.Add(msgs[msgCounter]); 
        
        divCount++;

        return m;
    }

    private int generateCost()
    {
        System.Random rnd = new System.Random();        
        return rnd.Next((int)maxCost.x, (int)maxCost.y);
    }

    private List<carCardMessage> generateMessages()
    {
        string[] msg=contentFile.text.Split('[');
        List<string> ctns = new List<string>();
        foreach (string m in msg)
            ctns.Add(m);
        
        int i = 0;

        string[] usr = userFile.text.Split('[');
        List<string> usrs = new List<string>();
        foreach (string m in usr)
            usrs.Add(m);
        
        foreach (string a in msg)
        {    
            string u = usrs[i];
            string m = ctns[i];

            carCardMessage mm = new carCardMessage(u, m);
            msgs.Add(mm);
            i++;
        }

        return msgs;
    }

    public class carCard {
        public Sprite img;
        public string name;
        public string condition;
        public int cost;
        public List<carCardMessage> messages;

        public carCard(Sprite img, string name, string condition, int cost, List<carCardMessage> messages)
        {
            this.img = img;
            this.name = name;
            this.condition = condition;
            this.cost = cost;
            this.messages = messages;
        }
    }

    public class carCardMessage {
        public string usr;
        public string content;

        public carCardMessage(string usr, string content)
        {
            this.usr = usr;
            this.content = content;
        }
    }
}
