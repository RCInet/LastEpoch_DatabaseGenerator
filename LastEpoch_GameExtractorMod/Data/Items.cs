using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using UnityEngine;
using UniverseLib;

namespace LastEpoch_GameExtractorMod.Data
{
    public class Items
    {
        public static bool ShowDebug = false;
        public static void Get()
        {
            Main.logger_instance.Msg("");
            Main.logger_instance.Msg("Get All Items");
            Refs.Init();
            if ((Refs.player != null) && (Refs.MasterItemList != null) && (Refs.uniqueList != null))
            {
                int index = 0;
                foreach (Type.Type_Structure type_struct in Type.TypesArray)
                {
                    string path = Main.path + type_struct.Path;
                    if (type_struct.Id == 101) { Affixs.Get(path, type_struct.Name.Replace(' ', '_') + ".json"); }
                    else if (type_struct.Id == 102) { Materials.Runes.Get(path, type_struct.Name.Replace(' ', '_') + ".json"); }
                    else if (type_struct.Id == 103) { Materials.Glyphs.Get(path, type_struct.Name.Replace(' ', '_') + ".json"); }
                    else if (type_struct.Id == 104) { Keys.Get(path, type_struct.Name.Replace(' ', '_') + ".json"); }
                    else if (type_struct.Id == 105) { LostMemorys.Get(path, type_struct.Name.Replace(' ', '_') + ".json"); }
                    else if (type_struct.Id == 34) { Blessings.Get(path, type_struct.Name.Replace(' ', '_') + ".json"); }
                    else if (type_struct.Id < 34) { Equipables.Get(type_struct.Id, type_struct.Name, path); }
                    index++;
                }
                Main.logger_instance.Msg("Get All Items : Done");
            }
            else { Main.logger_instance.Msg("You have to be in Game to do this"); }
        }

        internal class Refs
        {
            internal static Actor player = null;
            internal static ItemList MasterItemList = null;
            internal static UniqueList uniqueList = null;
            internal static void Init()
            {
                try
                {
                    player = PlayerFinder.getPlayerActor();
                    //Functions.SaveObject(player);
                }
                catch { player = null; }                
                try
                {
                    MasterItemList = RuntimeHelper.FindObjectsOfTypeAll(typeof(ItemList))[0].TryCast<ItemList>();
                    Functions.SaveObject(MasterItemList, "");
                }
                catch { MasterItemList = null; }                
                try
                {
                    uniqueList = Functions.GetObject("UniqueList").TryCast<UniqueList>();
                    Functions.SaveObject(uniqueList, "");
                }
                catch { uniqueList = null; }                
            }
            /*internal static void SaveObject(Object obj)
            {
                string path = Main.path + @"Objects\";
                string jsonString = UnityEngine.JsonUtility.ToJson(obj, true);
                string filename = obj.name + ".json";
                Functions.VerifyDirectory(path, filename);
                System.IO.File.WriteAllText(path + filename, jsonString);
            }*/
        }
        internal class Icon
        {
            internal static Sprite sprite = null;
            internal static Sprite Get(ItemDataUnpacked item)
            {
                Sprite result = null;
                try { result = UITooltipItem.SetItemSprite(item); }
                catch { }

                return result;
            }
            internal static void Save(Sprite item_sprite, string item_name, string path)
            {
                string filename = item_name.Replace(' ', '_') + ".png";
                Functions.VerifyDirectory(path, filename);
                UniverseLib.Runtime.TextureHelper.SaveTextureAsPNG(item_sprite.texture, path + filename);
            }
        }
        public class Type
        {
            public struct Type_Structure
            {
                public int Id;
                public string Name;
                public string Path;
            }
            public static Type_Structure[] TypesArray = new Type_Structure[]
            {
                new Type_Structure { Id = 0, Name = "Helm", Path = @"Database\Items\Armors\Helms\" },
                new Type_Structure { Id = 1, Name = "Body", Path =  @"Database\Items\Armors\Bodys\" },
                new Type_Structure { Id = 2, Name = "Belt", Path =  @"Database\Items\Armors\Belts\" },
            new Type_Structure { Id = 3, Name = "Boot", Path = @"Database\Items\Armors\Boots\" },
            new Type_Structure { Id = 4, Name = "Glove", Path = @"Database\Items\Armors\Gloves\" },
            new Type_Structure { Id = 5, Name = "Axe", Path = @"Database\Items\Weapons\Axes\1H\" },
            new Type_Structure { Id = 6, Name = "Dagger", Path = @"Database\Items\Weapons\Daggers\" },
            new Type_Structure { Id = 7, Name = "Blunt", Path = @"Database\Items\Weapons\Blunts\1H\" },
            new Type_Structure { Id = 8, Name = "Scepter", Path = @"Database\Items\Weapons\Scepters\" },
            new Type_Structure { Id = 9, Name = "Sword", Path = @"Database\Items\Weapons\Swords\1H\" },
            new Type_Structure { Id = 10, Name = "Wand", Path = @"Database\Items\Weapons\Wands\" },
            new Type_Structure { Id = 11, Name = "Fist", Path = @"Database\Items\Weapons\Fist\" },
            new Type_Structure { Id = 12, Name = "Two-handed Axe", Path = @"Database\Items\Weapons\Axes\2H\" },
            new Type_Structure { Id = 13, Name = "Two-handed Blunt", Path = @"Database\Items\Weapons\Blunts\2H\" },
            new Type_Structure { Id = 14, Name = "Polearm", Path = @"Database\Items\Weapons\Polearms\" },
            new Type_Structure { Id = 15, Name = "Staff", Path = @"Database\Items\Weapons\Staffs\" },
            new Type_Structure { Id = 16, Name = "Two-handed Sword", Path = @"Database\Items\Weapons\Swords\2H\" },
            new Type_Structure { Id = 17, Name = "Quiver", Path = @"Database\Items\Weapons\Quivers\" },
            new Type_Structure { Id = 18, Name = "Shield", Path = @"Database\Items\Armors\Shields\" },
            new Type_Structure { Id = 19, Name = "Catalyst", Path = @"Database\Items\Accesories\Catalysts\" },
            new Type_Structure { Id = 20, Name = "Amulet", Path = @"Database\Items\Accesories\Amulets\" },
            new Type_Structure { Id = 21, Name = "Ring", Path = @"Database\Items\Accesories\Rings\" },
            new Type_Structure { Id = 22, Name = "Relic", Path = @"Database\Items\Accesories\Relics\" },
            new Type_Structure { Id = 23, Name = "Bow", Path = @"Database\Items\Weapons\Bows\" },
            new Type_Structure { Id = 24, Name = "CrossBow", Path = @"Database\Items\Weapons\CrossBows\" },
            new Type_Structure { Id = 25, Name = "Small", Path = @"Database\Items\Idols\Smalls\" },
            new Type_Structure { Id = 26, Name = "Small Lagonian", Path = @"Database\Items\Idols\Small_Lagonians\" },
            new Type_Structure { Id = 27, Name = "Humble Eterran", Path = @"Database\Items\Idols\Humble_Eterrans\" },
            new Type_Structure { Id = 28, Name = "Stout", Path = @"Database\Items\Idols\Stouts\" },
            new Type_Structure { Id = 29, Name = "Grand", Path = @"Database\Items\Idols\Grands\" },
            new Type_Structure { Id = 30, Name = "Large", Path = @"Database\Items\Idols\Larges\" },
            new Type_Structure { Id = 31, Name = "Ornate", Path = @"Database\Items\Idols\Ornates\" },
            new Type_Structure { Id = 32, Name = "Huge", Path = @"Database\Items\Idols\Huges\" },
            new Type_Structure { Id = 33, Name = "Adorned", Path = @"Database\Items\Idols\Adorneds\" },
            new Type_Structure { Id = 34, Name = "Blessings", Path = @"Database\Blessings\" },
            new Type_Structure { Id = 101, Name = "Affixs", Path = @"Database\Affixs\" },
            new Type_Structure { Id = 102, Name = "Runes", Path = @"Database\Materials\Runes\" },
            new Type_Structure { Id = 103, Name = "Glyphs", Path = @"Database\Materials\Glyphs\" },
            new Type_Structure { Id = 104, Name = "Key", Path = @"Database\Items\Keys\" },
            new Type_Structure { Id = 105, Name = "LostMemory", Path = @"Database\LostMemory\" }
            };
        }
        public class Equipables
        {
            public static System.Collections.Generic.List<string> GetItemImplicits(Il2CppSystem.Collections.Generic.List<ItemList.EquipmentImplicit> item_implicits)
            {
                System.Collections.Generic.List<string> list_implicits = new System.Collections.Generic.List<string>();
                foreach (ItemList.EquipmentImplicit item_implicit in item_implicits)
                {
                    string min_value = System.Convert.ToString(item_implicit.implicitValue);
                    string max_value = System.Convert.ToString(item_implicit.implicitMaxValue);
                    if ((item_implicit.implicitValue >= 0) && (item_implicit.implicitValue <= 1) &&
                        (item_implicit.implicitMaxValue >= 0) && (item_implicit.implicitMaxValue <= 1))
                    {
                        min_value = System.Convert.ToString(item_implicit.implicitValue * 100) + " %";
                        max_value = System.Convert.ToString(item_implicit.implicitMaxValue * 100) + " %";
                    }
                    string value = min_value;
                    if (item_implicit.implicitValue != item_implicit.implicitMaxValue)
                    {
                        value = "(" + min_value + " to " + max_value + ")";
                    }
                    string implicit_type = item_implicit.type.ToString().ToLower();
                    string implicit_tags = "";
                    if (item_implicit.tags.ToString() != "None") { implicit_tags = " " + item_implicit.tags.ToString(); }
                    string implicit_string = implicit_type + " " + value + implicit_tags + " " + item_implicit.property.ToString().ToLower();
                    if (implicit_type == "added")
                    {
                        implicit_string = "+" + value + implicit_tags + " " + item_implicit.property.ToString();
                    }
                    else if (implicit_type == "increased")
                    {
                        implicit_string = value + " " + implicit_type + implicit_tags + " " + item_implicit.property.ToString();
                    }
                    list_implicits.Add(implicit_string);
                }

                return list_implicits;
            }
            public static System.Collections.Generic.List<string> GetUniqueMods(Il2CppSystem.Collections.Generic.List<UniqueModDisplayListEntry> display_list, string description, Il2CppSystem.Collections.Generic.List<UniqueItemMod> unique_mods)
            {
                System.Collections.Generic.List<string> list_unique_affixs = new System.Collections.Generic.List<string>();
                for (int z = 0; z < 8; z++) { list_unique_affixs.Add(""); }
                //System.Collections.Generic.List<bool> list_roll = new System.Collections.Generic.List<bool>();
                //for (int z = 0; z < 8; z++) { list_roll.Add(true); }

                foreach (UniqueItemMod mod in unique_mods)
                {
                    string mod_string = TooltipItemManager.UniqueBasicModFormatter(null, mod.property, mod.type, mod.value, mod.tags, mod.specialTag, 0, TooltipItemManager.SlotType.STASH, 0);                          
                    string formated_mod = mod_string;
                    if (mod_string.Contains(">")) { formated_mod = mod_string.Split('>')[1]; }
                    if (mod.canRoll)
                    {
                        string values = "";
                        float min_value = 0;
                        float max_value = 0;
                        if ((mod.type == BaseStats.ModType.INCREASED) | (mod.type == BaseStats.ModType.MORE))
                        {
                            min_value = (mod.value * 100);
                            max_value = (mod.maxValue * 100);
                            values = "(" + min_value + "% to " + max_value + "%) ";
                        }
                        else if (mod.type == BaseStats.ModType.ADDED)
                        {
                            min_value = mod.value;
                            max_value = mod.maxValue;
                            if ((min_value < 1) && (max_value < 1))
                            {
                                min_value = min_value * 100;
                                max_value = max_value * 100;
                                values = "(" + min_value + "% to " + max_value + "%) ";
                            }
                            else { values = "+(" + min_value + " to " + max_value + ") "; }
                        }
                        else if (mod.type == BaseStats.ModType.QUOTIENT)
                        {
                            Main.logger_instance.Msg("Unknow Mod Type = QUOTIENT : mod_string : " + mod_string);
                        }
                        string formated_mod_without_value = formated_mod;
                        if (formated_mod.Contains(" "))
                        {
                            string temp = "";
                            int i = 0;
                            foreach (string s in formated_mod.Split(' '))
                            {
                                if (i != 0)
                                {
                                    if (temp == "") { temp = s; }
                                    else { temp += " " + s; }
                                }
                                i++;
                            }
                            formated_mod_without_value = temp;
                        }
                        string final_result = values + formated_mod_without_value;
                        if (mod.rollID < list_unique_affixs.Count)
                        {
                            if ((list_unique_affixs[mod.rollID] == "") | (list_unique_affixs[mod.rollID] == " "))
                            { list_unique_affixs[mod.rollID] = final_result; }
                            else { list_unique_affixs[mod.rollID] += System.Environment.NewLine + final_result; }                            
                        }
                    }
                    else
                    {
                        //string result = formated_mod; // + " (cannot roll)";
                        //if (mod.rollID < list_roll.Count) { list_roll[mod.rollID] = false; }
                        if (mod.rollID < list_unique_affixs.Count)
                        {
                            //if (list_unique_affixs[mod.rollID] == "") { list_unique_affixs[mod.rollID] = " "; }
                            if (list_unique_affixs[mod.rollID] == "") { list_unique_affixs[mod.rollID] = formated_mod; }
                            else { list_unique_affixs[mod.rollID] += System.Environment.NewLine + formated_mod; }
                        }
                    }
                }
                System.Collections.Generic.List<string> final_list = new System.Collections.Generic.List<string>();
                //int k = 0;
                foreach (string s in list_unique_affixs)
                {
                    if (s != "")
                    {
                        final_list.Add(s);

                        /*if (k < list_roll.Count)
                        {
                            if (list_roll[k]) { final_list.Add(s); }
                            else { final_list.Add(s + System.Environment.NewLine + " (cannot roll)"); }
                        }  */                      
                    }
                    //k++;
                }
                return final_list;
            }
            public static void Get(int type_id, string type_name, string path)
            {
                Json.Items new_list_item = new Json.Items
                {
                    Basic = new System.Collections.Generic.List<Json.Basic>(),
                    Unique = new System.Collections.Generic.List<Json.Unique>(),
                    Set = new System.Collections.Generic.List<Json.Set>()
                };
                //Il2CppSystem.Collections.Generic.List<ItemList.EquipmentItem> items = Refs.MasterItemList.GetEquipmentSubItems(type_id);
                string basic_icon_path = path + @"Basic\";
                if (type_id == 34) { basic_icon_path = path + @"Icons\"; }
                foreach (var item in Refs.MasterItemList.GetEquipmentSubItems(type_id))
                {
                    if (ShowDebug) { Main.logger_instance.Msg(type_name + " : Basic : " + item.name + ", Id : " + item.subTypeID); }
                    new_list_item.Basic.Add(new Json.Basic
                    {
                        BaseName = item.name,
                        DisplayName = item.displayName,
                        BaseId = item.subTypeID,
                        Level = item.levelRequirement,
                        Implicit = GetItemImplicits(item.implicits)
                    });

                    if (Refs.player != null)
                    {
                        Icon.sprite = Icon.Get(Refs.player.generateItems.initialiseRandomItemData(false, 100, false, ItemLocationTag.None, type_id, item.subTypeID, 1, 0, 0, false, 0));
                        if (Icon.sprite != null) { Icon.Save(Icon.sprite, item.name, basic_icon_path); }
                        Icon.sprite = null;
                    }
                }
                foreach (UniqueList.Entry ul_entry in Refs.uniqueList.uniques)
                {
                    string icon_path = path;
                    if (ul_entry.baseType == type_id)
                    {
                        string base_name = "";
                        System.Collections.Generic.List<string> prefixs = new System.Collections.Generic.List<string>();
                        foreach (Json.Basic basic in new_list_item.Basic)
                        {
                            if (basic.BaseId == ul_entry.subTypes[0])
                            {
                                base_name = basic.BaseName;
                                prefixs = basic.Implicit;
                                break;
                            }
                        }
                        string description = "";
                        if (ul_entry.tooltipDescriptions.Count > 0) { description = ul_entry.tooltipDescriptions[0].description; }
                        System.Collections.Generic.List<string> unique_mod_list = GetUniqueMods(ul_entry.tooltipEntries, description, ul_entry.mods);
                        int item_rarity = 7;
                        if (!ul_entry.isSetItem)
                        {
                            if (ShowDebug) { Main.logger_instance.Msg(type_name + " : Unique : " + ul_entry.name + ", Id = " + ul_entry.uniqueID); }
                            new_list_item.Unique.Add(new Json.Unique
                            {
                                BaseId = ul_entry.subTypes[0],
                                BaseName = base_name,
                                Implicit = prefixs,
                                UniqueId = ul_entry.uniqueID,
                                UniqueName = ul_entry.name,
                                Unique_Affixs = unique_mod_list,
                                LoreText = ul_entry.loreText,
                                Level = ul_entry.levelRequirement
                            });
                            icon_path += @"Unique\";
                        }
                        else
                        {
                            item_rarity = 8;
                            if (ShowDebug) { Main.logger_instance.Msg(type_name + " : Set : " + ul_entry.name + ", Id = " + ul_entry.uniqueID); }
                            new_list_item.Set.Add(new Json.Set
                            {
                                BaseId = ul_entry.subTypes[0],
                                BaseName = base_name,
                                Implicit = prefixs,
                                SetId = ul_entry.uniqueID,
                                SetName = ul_entry.name,
                                Set_Refs = new System.Collections.Generic.List<Json.Set_Ref>(),
                                Unique_Affixs = unique_mod_list,
                                LoreText = ul_entry.loreText,
                                Level = ul_entry.levelRequirement
                            });
                            icon_path += @"Set\";
                        }
                        if (Refs.player != null)
                        {
                            Icon.sprite = Icon.Get(Refs.player.generateItems.initialiseRandomItemData(false, 100, false, ItemLocationTag.None, type_id, ul_entry.subTypes[0], item_rarity, 0, ul_entry.uniqueID, false, 0));
                            if (Icon.sprite != null) { Icon.Save(Icon.sprite, ul_entry.name, icon_path); }
                            Icon.sprite = null;
                        }
                    }
                }
                Save(new_list_item, type_name, path);
            }
            public static void Save(Json.Items items, string type_name, string path)
            {
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(items, Formatting.Indented);
                int items_count = items.Basic.Count + items.Unique.Count + items.Set.Count;
                string filename = type_name.Replace(' ', '_') + ".json";
                Main.logger_instance.Msg("Found " + items_count + " " + type_name);
                Functions.VerifyDirectory(path, filename);
                System.IO.File.WriteAllText(path + filename, jsonString);
            }
        }
        public class Affixs
        {
            public static bool Data_init = false;
            public static void InitData()
            {
                DB_Single_Affixs = new Json.Affixs.Shards { List = new System.Collections.Generic.List<Json.Affixs.Affix>() };
                DB_Multi_Affixs = new Json.Affixs.Shards { List = new System.Collections.Generic.List<Json.Affixs.Affix>() };
                Data_init = true;
            }
            public static System.Collections.Generic.List<Json.Affixs.Affix> affixslist = new System.Collections.Generic.List<Json.Affixs.Affix>();
            public static Json.Affixs.Shards DB_Single_Affixs = new Json.Affixs.Shards();
            public static Json.Affixs.Shards DB_Multi_Affixs = new Json.Affixs.Shards();
            public static Json.Affixs.Affix SingleAffixToShard(AffixList.SingleAffix affix)
            {
                int x = affix.affixId / 256;
                int id = affix.affixId - (x * 256);
                string modifier = affix.modifierType.ToString();
                if (affix.tags.ToString() != "None") { modifier += " " + affix.tags.ToString(); }
                modifier += " " + affix.property.ToString();
                Json.Affixs.Affix shard = new Json.Affixs.Affix
                {
                    Single = true,
                    Modifier = modifier,
                    ModifierList = null,
                    DisplayName = affix.affixDisplayName,
                    Id = id,
                    X = x,
                    Name = affix.affixName,
                    Title = affix.affixTitle,
                    Class = affix.classSpecificity.ToString(),
                    DisplayCategory = affix.displayCategory.ToString(),
                    Group = affix.group.ToString(),
                    Level = affix.levelRequirement,
                    RollOn = affix.rollsOn.ToString(),
                    Tiers = affix.tiers.Capacity,
                    Type = affix.type.ToString().ToLower()
                };

                return shard;
            }
            public static Json.Affixs.Affix MultiAffixToShard(AffixList.MultiAffix affix)
            {
                int x = affix.affixId / 256;
                int id = affix.affixId - (x * 256);
                System.Collections.Generic.List<string> properties_list = new System.Collections.Generic.List<string>();
                Il2CppSystem.Collections.Generic.List<AffixList.AffixProperty> affix_properties_list = affix.affixProperties;
                foreach (AffixList.AffixProperty alp in affix_properties_list)
                {
                    string modifier = alp.modifierType.ToString();
                    if (alp.tags.ToString() != "None") { modifier += " " + alp.tags.ToString(); }
                    modifier += " " + alp.property.ToString();
                    properties_list.Add(modifier);
                }
                Json.Affixs.Affix shard = new Json.Affixs.Affix
                {
                    Single = false,
                    Modifier = "",
                    ModifierList = properties_list,
                    DisplayName = affix.affixDisplayName,
                    Id = id,
                    X = x,
                    Name = affix.affixName,
                    Title = affix.affixTitle,
                    Class = affix.classSpecificity.ToString(),
                    DisplayCategory = affix.displayCategory.ToString(),
                    Group = affix.group.ToString(),
                    Level = affix.levelRequirement,
                    RollOn = affix.rollsOn.ToString(),
                    Tiers = affix.tiers.Capacity,
                    Type = affix.type.ToString().ToLower()
                };

                return shard;
            }
            public static void Get(string path, string json_filename)
            {
                if (!Data_init) { InitData(); }
                AffixList affix_list = Refs.MasterItemList.affixList;
                int type_id = 101;
                UnhollowerBaseLib.Il2CppReferenceArray<AffixList.SingleAffix> single_affix = affix_list.singleAffixes;
                UnhollowerBaseLib.Il2CppReferenceArray<AffixList.MultiAffix> multi_affix = affix_list.multiAffixes;
                int i = 0;
                string icon_path = path + @"Icons\";
                //Sprite item_sprite;
                foreach (AffixList.SingleAffix a in single_affix)
                {
                    Json.Affixs.Affix shard = SingleAffixToShard(a);
                    DB_Single_Affixs.List.Add(shard);
                    if (Refs.player != null)
                    {
                        Icon.sprite = Icon.Get(Refs.player.generateItems.initialiseRandomItemData(false, 100, false, ItemLocationTag.None, type_id, a.affixId, 1, 0, 0, false, 0));
                        if (Icon.sprite != null) { Icon.Save(Icon.sprite, a.affixName, icon_path); }
                    }
                    i++;
                }
                i = 0;
                foreach (AffixList.MultiAffix a in multi_affix)
                {
                    Json.Affixs.Affix shard = MultiAffixToShard(a);
                    DB_Multi_Affixs.List.Add(shard);
                    if (Refs.player != null)
                    {
                        Icon.sprite = Icon.Get(Refs.player.generateItems.initialiseRandomItemData(false, 100, false, ItemLocationTag.None, type_id, a.affixId, 1, 0, 0, false, 0));
                        if (Icon.sprite != null) { Icon.Save(Icon.sprite, a.affixName, icon_path); }
                    }
                    i++;
                }
                affixslist = new System.Collections.Generic.List<Json.Affixs.Affix>();
                foreach (Data.Json.Affixs.Affix affix in DB_Single_Affixs.List) { affixslist.Add(affix); }
                foreach (Data.Json.Affixs.Affix affix in DB_Multi_Affixs.List) { affixslist.Add(affix); }
                Save(path, json_filename);
            }
            public static void Save(string path, string json_filename)
            {
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(affixslist, Formatting.Indented);
                string final_string = "{ \"affix\": " + jsonString + " }";
                Main.logger_instance.Msg("Found " + affixslist.Count + " Affixs");
                Functions.VerifyDirectory(path, json_filename);
                //if (System.IO.File.Exists(path + json_filename)) { System.IO.File.Delete(path + json_filename); }
                //if (!System.IO.Directory.Exists(path)) { System.IO.Directory.CreateDirectory(path); }
                System.IO.File.WriteAllText(path + json_filename, final_string);
            }
        }
        public class Materials
        {
            public class Runes
            {
                public static bool Data_init = false;
                public static void InitData()
                {
                    Db_Runes = new Json.Materials.Runes { List = new System.Collections.Generic.List<Json.Materials.Material>() };
                    Data_init = true;
                }
                public static Json.Materials.Runes Db_Runes = new Json.Materials.Runes();
                public static void Get(string path, string json_filename)
                {
                    if (!Data_init) { InitData(); }
                    int base_id = 102;
                    int index = 0;
                    bool found = false;
                    string icon_path = path + @"Icons\";
                    foreach (ItemList.BaseNonEquipmentItem n_item in Refs.MasterItemList.nonEquippableItems)
                    {
                        if (n_item.baseTypeID == base_id) { found = true; break; }
                        index++;
                    }
                    if (found)
                    {
                        Db_Runes.List = new System.Collections.Generic.List<Json.Materials.Material>();
                        foreach (ItemList.NonEquipmentItem item in Refs.MasterItemList.nonEquippableItems[index].subItems)
                        {
                            Db_Runes.List.Add(new Json.Materials.Material
                            {
                                Name = item.name,
                                Id = item.subTypeID,
                                Description = item.description
                            });

                            if (Refs.player != null)
                            {
                                Icon.sprite = Icon.Get(Refs.player.generateItems.initialiseRandomItemData(false, 100, false, ItemLocationTag.None, base_id, item.subTypeID, 1, 0, 0, false, 0));
                                if (Icon.sprite != null) { Icon.Save(Icon.sprite, item.name, icon_path); }
                            }
                        }
                        Save(path, json_filename);
                    }
                }
                public static void Save(string path, string json_filename)
                {
                    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Db_Runes.List, Formatting.Indented);
                    string final_string = "{ \"rune\": " + jsonString + " }";
                    Main.logger_instance.Msg("Found " + Db_Runes.List.Count + " Runes");
                    Functions.VerifyDirectory(path, json_filename);
                    //if (System.IO.File.Exists(path + json_filename)) { System.IO.File.Delete(path + json_filename); }
                    //if (!System.IO.Directory.Exists(path)) { System.IO.Directory.CreateDirectory(path); }
                    System.IO.File.WriteAllText(path + json_filename, final_string);
                }
            }
            public class Glyphs
            {
                public static bool Data_init = false;
                public static void InitData()
                {
                    Db_Glyphs = new Json.Materials.Glyphs { List = new System.Collections.Generic.List<Json.Materials.Material>() };
                    Data_init = true;
                }
                public static Json.Materials.Glyphs Db_Glyphs = new Json.Materials.Glyphs();
                public static void Get(string path, string json_filename)
                {
                    if (!Data_init) { InitData(); }
                    int base_id = 103;
                    string icon_path = path + @"Icons\";
                    int index = 0;
                    bool found = false;
                    foreach (ItemList.BaseNonEquipmentItem n_item in Refs.MasterItemList.nonEquippableItems)
                    {
                        if (n_item.baseTypeID == base_id) { found = true; break; }
                        index++;
                    }
                    if (found)
                    {
                        Db_Glyphs.List = new System.Collections.Generic.List<Json.Materials.Material>();
                        foreach (ItemList.NonEquipmentItem item in Refs.MasterItemList.nonEquippableItems[index].subItems)
                        {
                            Db_Glyphs.List.Add(new Json.Materials.Material
                            {
                                Name = item.name,
                                Id = item.subTypeID,
                                Description = item.description
                            });
                            if (Refs.player != null)
                            {
                                Icon.sprite = Icon.Get(Refs.player.generateItems.initialiseRandomItemData(false, 100, false, ItemLocationTag.None, base_id, item.subTypeID, 1, 0, 0, false, 0));
                                if (Icon.sprite != null) { Icon.Save(Icon.sprite, item.name, icon_path); }
                            }
                        }
                        Save(path, json_filename);
                    }
                }
                public static void Save(string path, string json_filename)
                {
                    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Db_Glyphs.List, Formatting.Indented);
                    string final_string = "{ \"glyph\": " + jsonString + " }";
                    Main.logger_instance.Msg("Found " + Db_Glyphs.List.Count + " Glyphs");
                    Functions.VerifyDirectory(path, json_filename);
                    //if (System.IO.File.Exists(path + json_filename)) { System.IO.File.Delete(path + json_filename); }
                    //if (!System.IO.Directory.Exists(path)) { System.IO.Directory.CreateDirectory(path); }
                    System.IO.File.WriteAllText(path + json_filename, final_string);
                }
            }
        }
        public class Blessings
        {
            public static bool Data_init = false;
            public static void InitData()
            {
                Db_Blessings = new Json.Blessings { List = new System.Collections.Generic.List<Json.Blessing>() };
                Data_init = true;
            }
            public static Json.Blessings Db_Blessings = new Json.Blessings();
            public static void Get(string path, string json_filename)
            {
                if (!Data_init) { InitData(); }
                string icon_path = path + @"Icons\";
                int base_id = 34;
                int index = 0;
                bool found = false;
                foreach (ItemList.BaseEquipmentItem n_item in Refs.MasterItemList.EquippableItems)
                {
                    if (n_item.baseTypeID == base_id) { found = true; break; }
                    index++;
                }
                if (found)
                {
                    Db_Blessings.List = new System.Collections.Generic.List<Json.Blessing>();
                    foreach (ItemList.EquipmentItem item in Refs.MasterItemList.EquippableItems[index].subItems)
                    {
                        Db_Blessings.List.Add(new Json.Blessing
                        {
                            Id = item.subTypeID,
                            Name = item.displayName,
                            Implicit = Equipables.GetItemImplicits(item.implicits)[0],
                            Timeline = 0,
                            Difficulty = 0
                        });
                        if (Refs.player != null)
                        {
                            Icon.sprite = Icon.Get(Refs.player.generateItems.initialiseRandomItemData(false, 100, false, ItemLocationTag.None, base_id, item.subTypeID, 1, 0, 0, false, 0));
                            if (Icon.sprite != null) { Icon.Save(Icon.sprite, item.displayName, icon_path); }
                        }
                    }
                    Save(path, json_filename);
                }
            }
            public static void Save(string path, string json_filename)
            {
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Db_Blessings.List, Formatting.Indented);
                string final_string = "{ \"blessings\": " + jsonString + " }";
                Main.logger_instance.Msg("Found " + Db_Blessings.List.Count + " Blessings");
                Functions.VerifyDirectory(path, json_filename);
                //if (System.IO.File.Exists(path + json_filename)) { System.IO.File.Delete(path + json_filename); }
                //if (!System.IO.Directory.Exists(path)) { System.IO.Directory.CreateDirectory(path); }
                System.IO.File.WriteAllText(path + json_filename, final_string);
            }
        }
        public class Keys
        {
            public static bool Data_init = false;
            public static void InitData()
            {
                Db_Keys = new Json.Keys { List = new System.Collections.Generic.List<Json.Key>() };
                Data_init = true;
            }
            public static Json.Keys Db_Keys = new Json.Keys();
            public static void Get(string path, string json_filename)
            {
                if (!Data_init) { InitData(); }
                string icon_path = path + @"Icons\";
                int base_id = 104;
                int index = 0;
                bool found = false;
                foreach (ItemList.BaseNonEquipmentItem n_item in Refs.MasterItemList.nonEquippableItems)
                {
                    if (n_item.baseTypeID == base_id) { found = true; break; }
                    index++;
                }
                if (found)
                {
                    Db_Keys.List = new System.Collections.Generic.List<Json.Key>();
                    foreach (ItemList.NonEquipmentItem item in Refs.MasterItemList.nonEquippableItems[index].subItems)
                    {
                        Db_Keys.List.Add(new Json.Key
                        {
                            Name = item.name,
                            Id = item.subTypeID,
                            Description = item.description
                        });
                        if (Refs.player != null)
                        {
                            Icon.sprite = Icon.Get(Refs.player.generateItems.initialiseRandomItemData(false, 100, false, ItemLocationTag.None, base_id, item.subTypeID, 1, 0, 0, false, 0));
                            if (Icon.sprite != null) { Icon.Save(Icon.sprite, item.name, icon_path); }
                        }
                    }
                    Save(path, json_filename);
                }
            }
            public static void Save(string path, string json_filename)
            {
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Db_Keys.List, Formatting.Indented);
                string final_string = "{ \"key\": " + jsonString + " }";
                Main.logger_instance.Msg("Found " + Db_Keys.List.Count + " Keys");
                Functions.VerifyDirectory(path, json_filename);
                //if (System.IO.File.Exists(path + json_filename)) { System.IO.File.Delete(path + json_filename); }
                //if (!System.IO.Directory.Exists(path)) { System.IO.Directory.CreateDirectory(path); }
                System.IO.File.WriteAllText(path + json_filename, final_string);
            }
        }
        public class LostMemorys
        {
            public static bool Data_init = false;
            public static void InitData()
            {
                Db_Memorys = new Json.LostMemorys { List = new System.Collections.Generic.List<Json.Memory>() };
                Data_init = true;
            }
            public static Json.LostMemorys Db_Memorys = new Json.LostMemorys();
            public static void Get(string path, string json_filename)
            {
                if (!Data_init) { InitData(); }
                int base_id = 105;
                string icon_path = path + @"Icons\";
                int index = 0;
                bool found = false;
                foreach (ItemList.BaseNonEquipmentItem n_item in Refs.MasterItemList.nonEquippableItems)
                {
                    if (n_item.baseTypeID == base_id) { found = true; break; }
                    index++;
                }
                if (found)
                {
                    Db_Memorys.List = new System.Collections.Generic.List<Json.Memory>();
                    foreach (ItemList.NonEquipmentItem item in Refs.MasterItemList.nonEquippableItems[index].subItems)
                    {
                        Db_Memorys.List.Add(new Json.Memory
                        {
                            Name = item.name,
                            Id = item.subTypeID,
                            Description = item.description
                        });
                        if (Refs.player != null)
                        {
                            Icon.sprite = Icon.Get(Refs.player.generateItems.initialiseRandomItemData(false, 100, false, ItemLocationTag.None, base_id, item.subTypeID, 1, 0, 0, false, 0));
                            if (Icon.sprite != null) { Icon.Save(Icon.sprite, item.name, icon_path); }
                        }
                    }
                    Save(path, json_filename);
                }
            }
            public static void Save(string path, string json_filename)
            {
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Db_Memorys.List, Formatting.Indented);
                string final_string = "{ \"memory\": " + jsonString + " }";
                Main.logger_instance.Msg("Found " + Db_Memorys.List.Count + " Lost Memorys");
                Functions.VerifyDirectory(path, json_filename);
                //if (System.IO.File.Exists(path + json_filename)) { System.IO.File.Delete(path + json_filename); }
                //if (!System.IO.Directory.Exists(path)) { System.IO.Directory.CreateDirectory(path); }
                System.IO.File.WriteAllText(path + json_filename, final_string);
            }
        }
    }
}
