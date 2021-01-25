using Planetbase;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using UnityModManagerNet;

namespace JTLMods {
    public class ComponentManager {

        static readonly Dictionary<string, string> propNames = new Dictionary<string, string>() {
            {"mPowerGeneration", "int"},
            {"mOxygenGeneration", "int"},
            {"mWaterGeneration", "int"},
            {"mMaxUsers", "int"},
            {"mFlags", "int"},
            {"mHoldResourceCount", "int"},
            {"mSurveyedConstructionCount", "int"},
            {"mEmbeddedResourceCount", "int"},
            {"mUsageCooldown", "float"},
            {"mConditionDecayTime", "float"},
            {"mMaxUsageTime", "float"},
            {"mHeight", "float"},
            {"mRepairTime", "float"},
            {"mRadius" , "float"},
            {"mResourceProductionPeriod", "float"},
            {"mWallSeparation", "float"},
            {"mName", "string"},
            {"mPrefabName", "string"},
            {"mTooltip", "string"},
            {"mOperatedModuleType", "string"},
            {"mProduction", "List<ProductionItem>"}
        };
        static readonly Dictionary<string, string> itemSubtypes = new Dictionary<string, string>() {
            {"AnyMeat", "Vitromeat"},
            {"Chicken", "Vitromeat"},
            {"Pork", "Vitromeat"},
            {"Beef", "Vitromeat"},
            {"AnyVegetable", "Vegetables"},
            {"Wheat", "Vegetables"},
            {"Maize", "Vegetables"},
            {"Rice", "Vegetables"},
            {"Peas", "Vegetables"},
            {"Potatoes", "Vegetables"},
            {"Lettuce", "Vegetables"},
            {"Tomatoes", "Vegetables"},
            {"Onions", "Vegetables"},
            {"Radishes" , "Vegetables"},
            {"Mushrooms", "Vegetables"},
            {"Basic", "Meal"},
            {"Salad", "Meal"},
            {"Pasta", "Meal"},
            {"Burger", "Meal"}
        };

        static string configStr;
        static GameState gameState;
        static int repetitions;

        [LoaderOptimization(LoaderOptimization.NotSpecified)]
        public static void Init(UnityModManager.ModEntry modData) {
            repetitions = 0;
            modData.OnUpdate = Update;
        }

        public static void Update(UnityModManager.ModEntry modData, float tDelta) {
            if(repetitions < 1) {
                gameState = GameManager.getInstance().getGameState();

                if(gameState.ToString() != "Planetbase.GameStateGame") {
                    if(!File.Exists("./Mods/ComponentManager/settings.config")) CreateSettingsFile();

                    if(configStr == null) {
                        configStr = File.ReadAllText("./Mods/ComponentManager/settings.config");
                        configStr = configStr.Trim();
                    }

                    string[] compsStr = configStr.Substring(1).Split('/');

                    foreach(string compStr in compsStr) {
                        string compName = compStr.Substring(0, compStr.IndexOf(':'));
                        string[] compProps = compStr.Substring(compStr.IndexOf(':') + 1).Trim().Split(',');

                        ComponentType component = ComponentTypeList.find(compName);

                        foreach(string propStr in compProps) {
                            string key = propStr.Split(':')[0].Trim();
                            if(key == "") continue;

                            object value = null;
                            try {
                                switch(propNames[key]) {
                                    case "string":
                                        value = propStr.Split(':')[1].Trim();
                                        break;
                                    case "float":
                                        value = float.Parse(propStr.Split(':')[1].Trim(), CultureInfo.InvariantCulture);
                                        break;
                                    case "List<ProductionItem>":
                                        SetResourceProduction(component, propStr, key);
                                        break;
                                    default:
                                        value = int.Parse(propStr.Split(':')[1].Trim());
                                        break;
                                }

                                if(value != null) SetComponentProp(component, key, value);

                            } catch(KeyNotFoundException e) {
                                Console.WriteLine("KeyNotFoundException property: " + key);
                                throw e;
                            }
                        }
                    }
                }
                repetitions++;
            }
        }

        private static void SetResourceProduction(ComponentType component, string prop, string key) {
            ClearProductionItemList(component, key);

            string[] listContentStr = prop.Substring(prop.IndexOf(':') + 1).Split('|');

            foreach(string item in listContentStr) {
                string itemName = item.Split(':')[0].Trim();
                int itemCount = int.Parse(item.Split(':')[1].Trim());

                string itemTypeStr = itemName;
                ResourceSubtype itemSubtype = ResourceSubtype.None;

                if(itemSubtypes.ContainsKey(itemName)) {
                    itemTypeStr = itemSubtypes[itemName];
                    itemSubtype = (ResourceSubtype)Enum.Parse(typeof(ResourceSubtype), itemName);
                }

                Type itemType = Type.GetType("Planetbase." + itemTypeStr + ", Assembly-CSharp");
                for(int i = 0; i < itemCount; i++) {
                    AddResourceProduction(component, itemType, itemSubtype);
                }
            }
        }

        private static void ClearProductionItemList(ComponentType component, string key) {
            List<ProductionItem> pList = (List<ProductionItem>)typeof(ComponentType)
                                                        .GetField(key, BindingFlags.NonPublic | BindingFlags.Instance)
                                                        .GetValue(component);
            pList.Clear();
        }

        private static void SetComponentProp(ComponentType c, string key, object value) {

            typeof(ComponentType)
                .GetField(key, BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(c, value);
        }

        private static void AddResourceProduction(ComponentType component, Type mainType, ResourceSubtype subtype) {
            Type prodType = typeof(ComponentType);
            MethodInfo meth = prodType.GetMethod("addResourceProduction", new Type[] { typeof(ResourceSubtype) });
            MethodInfo genMeth = meth.MakeGenericMethod(mainType);
            object[] parameters = new object[] { subtype };
            genMeth.Invoke(component, parameters);
        }

        private static void CreateSettingsFile() {
            Directory.CreateDirectory("./Mods/ComponentManager/");

            string configSample = "/MaizePad:    mPowerGeneration: 111,    mResourceProductionPeriod: 1/WheatPad:    mPowerGeneration: 222,    mResourceProductionPeriod: 2/RicePad:    mPowerGeneration: 999,    mName: Arró amarillo,    mProduction:         Ore: 1 | Starch: 2 | Chicken: 1 ";

            Console.WriteLine("Creating settings.config");
            File.WriteAllText("./Mods/ComponentManager/settings.config", configSample);
        }
    }
}
