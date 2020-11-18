using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Globalization;

public class API : MonoBehaviour
{
    public string BaseURL; //= "https://war-service-live.foxholeservices.com/api";
    public string Mapname;

    public IDictionary<string, string> IconNames = new Dictionary<string, string>();
    public IDictionary<string, Image> maplist = new Dictionary<string, Image>();
    public IDictionary<string, string> MapLabels = new Dictionary<string, string>();
    public IDictionary<string, Image> MapId = new Dictionary<string, Image>();
    public IDictionary<string, string> RegionId = new Dictionary<string, string>();
    public IDictionary<string, string> MapIcons = new Dictionary<string, string>();

    public Text LocationTxt;
    public Canvas canvas;
    public Image ImageMap;
    //public Plane plane;
    //public Sprite spr;

    public int i = 0;
    public int j = 0;
    //public int IndexOf(string val);

    public JSONNode StaticInfo;
    public JSONNode DynamicPInfo;
    public string MapInfo;

    //Dictionary<string, Dictionary<string, string>> listS = new Dictionary<string, Dictionary<string, string>>();
    List<Dictionary<string, string>> listDP = new List<Dictionary<string, string>>();
    List<Dictionary<string, string>> listSS = new List<Dictionary<string, string>>();
    //List<Dictionary<string, string>> listSSS = new List<Dictionary<string, string>>();

    List<string> listM = new List<string>();
    List<string> listN = new List<string>();
    List<string> listP = new List<string>();
    List<string> listS = new List<string>();
    List<string> listLabels = new List<string>();
    List<string> listIcons = new List<string>();


    public static API instance;
    // Start is called before the first frame update
    /*
    float mapwidth = template.Width;
    float w = mapwidth / 5.5f;
    float o = (.5f * -(float)template.Width, .5f * (float)template.Height);
    float k = w * (float)Math.Sqrt(3) / 2.0f;
    float precision = 1.0f;

    Dictionary<string, Vector2> regionlist = new Dictionary<string, Vector2>() {
    {"GodcroftsHex", new Vector2(o.Item2 + .5f * k, o.Item1 + 2.25f * w) },
    {"DeadLandsHex", new Vector2(o.Item2, o.Item1) },
    {"ReachingTrailHex", new Vector2(o.Item2 + 2.0f * k,o.Item1) },
    {"CallahansPassageHex", new Vector2(o.Item2 + k, o.Item1) },
    {"MarbanHollowHex", new Vector2(o.Item2 + .5f * k, o.Item1 + .75f * w) },
    {"MarbanHollow", new Vector2(o.Item2 + .5f * k, o.Item1 + .75f * w) },
    {"UmbralWildwoodHex", new Vector2(o.Item2 - k, o.Item1) },
    {"MoorsHex", new Vector2(o.Item2 + 1.5f * k, o.Item1 - .75f * w) },
    {"MooringCountyHex", new Vector2(o.Item2 + 1.5f * k, o.Item1 - .75f * w) },
    {"HeartlandsHex", new Vector2(o.Item2 - 1.5f*k, o.Item1 - .75f*w)},
    {"LochMorHex" , new Vector2(o.Item2 - .5f * k, o.Item1 - .75f* w) },
    {"LinnOfMercyHex", new Vector2(o.Item2 + .5f * k, o.Item1 - .75f*w) },
    {"LinnMercyHex", new Vector2(o.Item2 + .5f * k, o.Item1 - .75f*w) },
    {"StonecradleHex", new Vector2(o.Item2 + k, o.Item1 - 1.5f * w) },
    {"FarranacCoastHex", new Vector2(o.Item2, o.Item1 - 1.5f * w) },
    {"WestgateHex", new Vector2(o.Item2 - k, o.Item1 - 1.5f * w) },
    {"FishermansRowHex",new Vector2 (o.Item2 - .5f * k, o.Item1 - 2.25f * w) },
    {"OarbreakerHex", new Vector2(o.Item2 + .5f * k, o.Item1 - 2.25f * w) },
    {"GreatMarchHex", new Vector2(o.Item2 - 2.0f * k, o.Item1) },
    {"TempestIslandHex", new Vector2(o.Item2 - .5f * k, o.Item1 + 2.25f * w) },
    {"EndlessShoreHex", new Vector2(o.Item2, o.Item1 + 1.5f * w) },
    {"AllodsBightHex", new Vector2(o.Item2 - k, o.Item1 + 1.5f * w) },
    {"WeatheredExpanseHex",new Vector2 (o.Item2 + k, o.Item1 + 1.5f * w) },
    {"DrownedValeHex",new Vector2 (o.Item2 - .5f * k, o.Item1 + .75f * w) },
    {"ShackledChasmHex",new Vector2 (o.Item2 - 1.5f * k, o.Item1 + .75f * w) },
    {"ViperPitHex",new Vector2 (o.Item2 + 1.5f * k, o.Item1 + .75f*w) }
    };*/

    void Start()
    {
        instance = this;
        IconNameLook();
        RegionIdLook();
        GetAPI(BaseURL);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public class GetStatic
    {
        public String regionId { get; set; }
        public List<Dictionary<string, string>> mapTextItems { get; set; }
    }

    public class GetDynamicP
    {
        public String regionId { get; set; }
        public List<Dictionary<string, string>> mapItems { get; set; }
    }

    public static Text AddTextToCanvas(string result2, Image imageGameObject)
    {
        GameObject newtext = new GameObject(result2.ToString() + " Label");
        newtext.transform.SetParent(imageGameObject.transform, false);

        RectTransform trans = newtext.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(-5, -0.664062f);
        Text text = newtext.AddComponent<Text>();
        text.text = result2;
        text.fontSize = 1;
        text.fontStyle = FontStyle.Bold;
        text.alignment = TextAnchor.MiddleCenter;
        trans.sizeDelta = new Vector2(20, 20);

        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        text.font = ArialFont;
        text.material = null;
        //Material None = (Material)Resources.GetBuiltinResource(typeof(Material),"None");
        //text.material = None;
        text.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
        text.transform.rotation = Quaternion.Euler(90, 0, 0);
        return text;
    }

    public static Image AddMap(string resultMap, Canvas canvasGameObject)
    {
        string name = resultMap.Trim(' ', '"');
        GameObject newmap = new GameObject(name.ToString() + " Map");
        newmap.transform.SetParent(canvasGameObject.transform, false);

        RectTransform trans = newmap.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(0, 0);
        trans.sizeDelta = new Vector2(10, 10);
        CanvasRenderer rend = newmap.AddComponent<CanvasRenderer>();
        rend.transform.rotation = Quaternion.Euler(90, 0, 0);
        //Aspect Ratio for the maps
        //X:1, Y:0.8671875
        rend.transform.localScale = new Vector3(1 * 0.15f, 0.8671875f * 0.15f, 1);
        Image img = newmap.AddComponent<Image>();
        //img.sprite = Resources.Load<Sprite>("Maps/" + resultMap);
        return img;
    }

    public SpriteRenderer AddMapIcon(string result3, Image imageGameObject, string regionId)
    {
        //Debug.Log(result3);
        GameObject newicon = new GameObject(result3.ToString() + " Icon" + j.ToString());
        newicon.transform.SetParent(imageGameObject.transform, false);
        newicon.transform.SetAsFirstSibling();

        RectTransform trans = newicon.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(-5, -0.664062f);
        trans.sizeDelta = new Vector2(10, 10);
        SpriteRenderer rend = newicon.AddComponent<SpriteRenderer>();
        rend.transform.rotation = Quaternion.Euler(0, 0, 0);
        rend.transform.localScale = new Vector3(1, 0.25f, 1);
        rend.sortingLayerName = "MapIcon";

        MapIcons.Add(newicon.name, regionId);
        return rend;
    }

    public void IconNameLook()
    {
        IconNames.Add("5", "MapIconStaticBase1"); //adding a key/value using the Add() method
        IconNames.Add("6", "MapIconStaticBase2");
        IconNames.Add("7", "MapIconStaticBase3");        
        IconNames.Add("8", "MapIconForwardBase1");
        IconNames.Add("9", "MapIconForwardBase2");
        IconNames.Add("10", "MapIconForwardBase3");
        IconNames.Add("11", "MapIconMedical");
        IconNames.Add("12", "MapIconVehicle");
        IconNames.Add("13", "MapIconStaticBase1");
        IconNames.Add("14", "MapIconSupplies");
        IconNames.Add("15", "MapIconWorkshop");
        IconNames.Add("16", "MapIconScorchedTown");
        IconNames.Add("17", "MapIconManufacturing");
        IconNames.Add("18", "MapIconShipyard");
        IconNames.Add("19", "MapIconTechCenter");
        IconNames.Add("20", "MapIconSalvage");
        IconNames.Add("21", "MapIconComponents");
        IconNames.Add("22", "MapIconFuel");
        IconNames.Add("23", "MapIconSulfur");
        //IconNames.Add("24", "MapIconStaticBase1");
        //IconNames.Add("25", "MapIconStaticBase1");
        //IconNames.Add("26", "MapIconStaticBase1");
        IconNames.Add("27", "MapIconKeep");
        IconNames.Add("28", "MapIconObservationTower");
        IconNames.Add("29", "MapIconFort");
        IconNames.Add("30", "MapIconTroopShip");
        IconNames.Add("32", "MapIconSulfurMine");
        IconNames.Add("33", "MapIconStorageFacility");
        IconNames.Add("34", "MapIconFactory");
        IconNames.Add("35", "MapIconSafehouse");
        IconNames.Add("36", "MapIconAmmoFactory");
        IconNames.Add("37", "MapIconRocketSite");
        IconNames.Add("38", "MapIconSalvageMine");
        IconNames.Add("39", "MapIconConstructionYard");
        IconNames.Add("40", "MapIconComponentMine");
        IconNames.Add("41", "MapIconOilWell");
        IconNames.Add("44", "MapIconFortCursed");
        IconNames.Add("45", "MapIconRelicBase");
        IconNames.Add("46", "MapIconRelicBase");
        IconNames.Add("47", "MapIconRelicBase");
        IconNames.Add("51", "MapIconMassProductionFactory");
        IconNames.Add("52", "MapIconSeaport");
        IconNames.Add("53", "MapIconCoastalGun");
    } 

    public void RegionIdLook()
    {
        RegionId.Add("4", "CallahansPassageHex Map");
        RegionId.Add("13", "FarranacCoastHex Map");
        RegionId.Add("3", "DeadLandsHex Map");
        RegionId.Add("16", "OarbreakerHex Map");
        RegionId.Add("5", "MarbanHollow Map");
        RegionId.Add("11", "ReachingTrailHex Map");
        RegionId.Add("14", "WestgateHex Map");
        RegionId.Add("15", "FishermansRowHex Map");
        RegionId.Add("10", "LinnMercyHex Map");
        RegionId.Add("25", "ViperPitHex Map");
        RegionId.Add("12", "StonecradleHex Map");
        RegionId.Add("18", "TempestIslandHex Map");
        RegionId.Add("19", "GodcroftsHex Map");
        RegionId.Add("20", "EndlessShoreHex Map");
        RegionId.Add("21", "AllodsBightHex Map");
        RegionId.Add("23", "DrownedValeHex Map");
        RegionId.Add("7", "MooringCountyHex Map");
        RegionId.Add("22", "WeatheredExpanseHex Map");
        RegionId.Add("9", "LochMorHex Map");
    }

    void GetAPI(string BaseURL)
    {
        IEnumerator CallM()
        {
            UnityWebRequest RequestMaps = new UnityWebRequest();
            RequestMaps.downloadHandler = new DownloadHandlerBuffer();
            // build the url and query
            RequestMaps.url = BaseURL + "/worldconquest/maps";
            // send the web request and wait for a returning result
            yield return RequestMaps.SendWebRequest();
            // convert the byte array and wait for a returning result
            MapInfo = Encoding.Default.GetString(RequestMaps.downloadHandler.data);
            CallMaps();
        }

        void CallMaps()
        {
            char[] trim = { '[', ']', ' ', '"'};
            string Minfo = MapInfo.Trim(trim);
            listM = Minfo.Split(',').ToList();

            int e = 0;
            foreach (var index1 in listM)
            {
                Mapname = listM[e].Trim(' ', '"');
                ImageMap = AddMap(listM[e], canvas);
                ImageMap.sprite = Resources.Load<Sprite>("Maps/" + "Map" + Mapname);
                maplist.Add(ImageMap.name, ImageMap);
                listN.Add(ImageMap.name);
                //Debug.Log("Name: " + ImageMap.name);
                //Debug.Log(maplist[ImageMap.name]);
                i = e;
                e++;
                //Debug.Log(i);
                StartCoroutine(CallS());
                new WaitForSecondsRealtime(1);
                StartCoroutine(CallD());
            }
        }

        IEnumerator CallS()
        {
            // create the web request and download handler
            UnityWebRequest RequestStatic = new UnityWebRequest();
            RequestStatic.downloadHandler = new DownloadHandlerBuffer();
            // build the url and query
            RequestStatic.url = BaseURL + "/worldconquest/maps/" + Mapname + "/static";
            // send the web request and wait for a returning result
            yield return RequestStatic.SendWebRequest();
            // convert the byte array and wait for a returning result
            string SJson = Encoding.Default.GetString(RequestStatic.downloadHandler.data);
            // parse the raw string into a json result we can easily read
            //Debug.Log("SJson: " + SJson);
            StaticInfo = JSON.Parse(SJson);
            CallStatic();
        }

        void CallStatic()
        {
            string Sinfo = StaticInfo.ToString();
            //Debug.Log("Sinfo " + Sinfo);

            GetStatic getStatic;

            getStatic = JsonConvert.DeserializeObject<GetStatic>(Sinfo);

            int q = 0;
            //Debug.Log("listN: " + listN.Count);
            foreach (var index in listN)
            {
                //MapId.Add(getStatic.regionId, imageResult);
                //Debug.Log("region: "+getStatic.regionId);
                string name;
                if (RegionId.TryGetValue(getStatic.regionId, out name))
                {
                    Image img;
                    if (maplist.TryGetValue(name, out img))
                    {
                        if (!MapId.ContainsKey(getStatic.regionId))
                        {
                            MapId.Add(getStatic.regionId, img);
                        }
                        //Debug.Log("region: " + getStatic.regionId);
                    }
                }
            }
            //Debug.Log("next");
            //MapId.Add(getStatic.regionId, );
            //Debug.Log(getStatic.mapTextItems);

            int t = 0;
            foreach (var i in getStatic.mapTextItems)
            {
                listSS.Add(getStatic.mapTextItems[t]);
                listS.Add(getStatic.regionId);
                t++;
            }
            //Debug.Log(listSS.Count);
            int e = 0;
            string x;
            string y;
            //listLabels.Clear();
            foreach (var index1 in listSS)
            {
                //Debug.Log(listSS[e]);
                string result2;
                if (listSS[e].TryGetValue("text", out result2))
                {
                    //Debug.Log("r2: "+result2);
                    //Debug.Log(listS[e]);
                    Image img;
                    if (MapId.TryGetValue(listS[e], out img))
                    {
                        if (!listLabels.Contains(result2))
                        {
                            Text text = AddTextToCanvas(result2, img);
                            listLabels.Add(result2);

                            if (listSS[e].TryGetValue("x", out x))
                            {
                                float X = float.Parse(x, CultureInfo.InvariantCulture.NumberFormat) * -10;
                                if (listSS[e].TryGetValue("y", out y))
                                {
                                    float Y = float.Parse(y, CultureInfo.InvariantCulture.NumberFormat) * 10;
                                    //Debug.Log(X + ", " + Y);    
                                    text.rectTransform.localPosition = new Vector3((X + 5) * -1, (Y - 5) * -1, -0.002f);
                                    //Debug.Log(text.rectTransform.localPosition);
                                }
                            }
                        }
                        e++;
                    }
                }
            }
        }

        IEnumerator CallD()
        {
            // create the web request and download handler
            UnityWebRequest RequestDynamicP = new UnityWebRequest();
            RequestDynamicP.downloadHandler = new DownloadHandlerBuffer();
            // build the url and query
            RequestDynamicP.url = BaseURL + "/worldconquest/maps/" + Mapname + "/dynamic/public";
            // send the web request and wait for a returning result
            yield return RequestDynamicP.SendWebRequest();
            // convert the byte array and wait for a returning result
            string DPJson = Encoding.Default.GetString(RequestDynamicP.downloadHandler.data);
            // parse the raw string into a json result we can easily read
            DynamicPInfo = JSON.Parse(DPJson);
            CallDynamic();
        }

        void CallDynamic()
        {
            string DPinfo = DynamicPInfo.ToString();
            //Debug.Log("DPinfo " + DPinfo);

            GetDynamicP getDynamicP;

            getDynamicP = JsonConvert.DeserializeObject<GetDynamicP>(DPinfo);

            int t = 0;
            foreach (var i in getDynamicP.mapItems)
            {
                listDP.Add(getDynamicP.mapItems[t]);
                listP.Add(getDynamicP.regionId);
                t++;
            }

            j = 0;
            string x;
            string y;

            foreach (var index2 in listDP)
            {
                string result3;
                if (listDP[j].TryGetValue("iconType", out result3))
                {
                    if (!listIcons.Contains(listP[j]))
                    {
                        Image img;
                        //Debug.Log("listP[e]: "+listP[e]);
                        if (MapId.TryGetValue(listP[j], out img))
                        {
                            if (!MapIcons.ContainsKey(result3.ToString() + " Icon" + j.ToString()))
                            {
                                //Debug.Log("result3: "+result3);
                                //Debug.Log("MapImage: " + img);
                                SpriteRenderer Icon = AddMapIcon(result3, img, getDynamicP.regionId);

                                if (listDP[j].TryGetValue("x", out x))
                                {
                                    float X = float.Parse(x, CultureInfo.InvariantCulture.NumberFormat) * -10;
                                    if (listDP[j].TryGetValue("y", out y))
                                    {
                                        float Y = float.Parse(y, CultureInfo.InvariantCulture.NumberFormat) * -10;
                                        //Debug.Log(X + ", " + Y);
                                        Icon.transform.localPosition = new Vector3((X + 5) * -1, (Y + 5) * 1, -0.03f);
                                        string IconName;
                                        if (IconNames.TryGetValue(result3, out IconName))
                                        {
                                            // Debug.Log("DictionaryI: " + IconName);
                                            string result4;
                                            if (listDP[j].TryGetValue("teamId", out result4))
                                            {
                                                if (result4 == "COLONIALS")
                                                {
                                                    Icon.sprite = Resources.Load<Sprite>("MapIconsOriginal/" + IconName);
                                                    Icon.color = new Color(0.396f, 0.529f, 0.369f);
                                                    //Debug.Log("MapIconsOriginal/" + IconName + "Colonial");
                                                }
                                                if (result4 == "WARDENS")
                                                {
                                                    Icon.sprite = Resources.Load<Sprite>("MapIconsOriginal/" + IconName);
                                                    Icon.color = new Color(0.176f, 0.424f, 0.631f);
                                                    //Debug.Log("MapIconsOriginal/" + IconName + "Warden");
                                                }
                                                if (result4 == "NONE")
                                                {
                                                    Icon.sprite = Resources.Load<Sprite>("MapIconsOriginal/" + IconName);
                                                    //Debug.Log("MapIconsOriginal/" + IconName);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            j++;
                        }
                    }
                }
            }
        }

        StartCoroutine(CallM());
    }
}
