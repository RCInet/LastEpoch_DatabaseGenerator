using UnityEngine;

namespace LastEpoch_GameExtractorMod.Data
{
    public class Classes
    {
        public struct Character_class
        {
            public int baseAttunement;
            public int baseDexterity;
            public float baseEndurance;
            public float baseEnduranceThreshold;
            public int baseHealth;
            public int baseIntelligence;
            public int baseMana;
            public int baseStrength;
            public int baseStunAvoidance;
            public int baseVitality;
            public string className;
            public System.Collections.Generic.List<Character_class_ability> defaultAbilities;
            public float enduranceThresholdPerHealth;
            public float enduranceThresholdPerLevel;
            public int firstLevelForMinionScaling;
            public float healthBarHeightOffset;
            public int healthPerLevel;
            public float healthRegen;
            public float healthRegenPerLevel;
            public bool hideThirdMasterySkillsInPanel;
            public System.Collections.Generic.List<Character_class_ability> knownAbilities;
            public float lessMinionDamageTakenPerLevel;
            public float manaPerLevel;
            public float manaRegen;
            public float moreMinionDamagePerLevel;
            public int specialTagForClassSpecificLevelOfSkillsStats;
            public System.Collections.Generic.List<Character_class_mastery> masteries;
            public string treeID;
            public System.Collections.Generic.List<Character_class_ability_and_level> unlockableAbilities;
            public string iconName;
        }
        public struct Character_class_ability
        {
            public string abilityName;
            public string playerAbilityID;
            public string iconName;
            public SkillTrees.Skill_Tree skilltree;
        }
        public struct Character_class_ability_and_level
        {
            public Character_class_ability ability;
            public int level;
        }
        public struct Character_class_mastery
        {
            public string name;
            public System.Collections.Generic.List<Character_class_ability_and_level> abilities;
            public string iconName;
        }        
        
        public class Icon
        {
            public static Sprite sprite = null;
            public static string GetSpriteName()
            {
                string filename = "";
                try { filename = sprite.TryCast<UnityEngine.Object>().name.Replace(" ", "_") + ".png"; }
                catch { Main.logger_instance.Msg("Error when trying to get Sprite Name"); }

                return filename;
            }            
            public static bool Save(Sprite spr, string path)
            {
                sprite = spr;
                bool result = false;
                string filename = GetSpriteName();
                if (filename != "")
                {
                    Functions.VerifyDirectory(path, filename);
                    if ((sprite.texture.width < 4097) && (sprite.texture.height < 4097))
                    {
                        try
                        {
                            UniverseLib.Runtime.TextureHelper.SaveTextureAsPNG(sprite.texture, path + filename);
                            result = true;
                        }
                        catch { Main.logger_instance.Msg("Error when Saving Texture : " + filename); }
                    }
                    else if ((sprite.texture.width < 8193) && (sprite.texture.height < 8193)) //SpriteSheet
                    {
                        try
                        {
                            Rect rectangle = new Rect
                            {
                                position = new UnityEngine.Vector2(sprite.textureRect.position.x, sprite.texture.height - sprite.textureRect.position.y - sprite.textureRect.height),
                                width = sprite.textureRect.width,
                                height = sprite.textureRect.height
                            };
                            UniverseLib.Runtime.TextureHelper.SaveTextureAsPNG(UniverseLib.Runtime.TextureHelper.CopyTexture(sprite.texture, rectangle), path + filename);
                            result = true;
                        }
                        catch { Main.logger_instance.Msg("Error when Saving Texture from SpriteSheet : " + filename); }
                    }
                    else { Main.logger_instance.Msg("Error Texture > 8k : " + filename); }
                }

                return result;
            }
        }

        public static void Get()
        {
            Main.logger_instance.Msg("");
            Main.logger_instance.Msg("Get All Classes");
            try
            {
                string path = Main.path + @"Database\Classes\";
                foreach (UnityEngine.Object obj in UniverseLib.RuntimeHelper.FindObjectsOfTypeAll(typeof(CharacterClassList)))
                {
                    CharacterClassList c = obj.TryCast<CharacterClassList>();
                    foreach (CharacterClass char_class in c.classes)
                    {
                        Main.logger_instance.Msg("");
                        Main.logger_instance.Msg("Classe Found : " + char_class.className);
                        Functions.SaveObject(char_class, char_class.className);
                        string class_path = path + char_class.className + @"\";
                        string class_filename = char_class.className + ".json";

                        System.Collections.Generic.List<Character_class_ability> temp_defaultAbilities = new System.Collections.Generic.List<Character_class_ability>();
                        int def_count = 0;
                        foreach (Ability ability in char_class.defaultAbilities)
                        {
                            if (ability.name != "NullAbility")
                            {
                                Icon.Save(ability.abilitySprite, class_path + @"defaultAbilities\");
                                Character_class_ability temp_ability = new Character_class_ability
                                {
                                    abilityName = ability.name,
                                    playerAbilityID = ability.playerAbilityID,
                                    iconName = Icon.GetSpriteName(),
                                    skilltree = SkillTrees.Get(ability)
                                };
                                temp_defaultAbilities.Add(temp_ability);
                                def_count++;
                            }
                        }
                        Main.logger_instance.Msg("Found " + def_count + " Default Ability");
                        System.Collections.Generic.List<Character_class_ability> temp_knownAbilities = new System.Collections.Generic.List<Character_class_ability>();
                        int know_count = 0;
                        foreach (Ability ability in char_class.knownAbilities)
                        {
                            Icon.Save(ability.abilitySprite, class_path + @"knownAbilities\");
                            Character_class_ability temp_ability = new Character_class_ability
                            {
                                abilityName = ability.name,
                                playerAbilityID = ability.playerAbilityID,
                                iconName = Icon.GetSpriteName(),
                                skilltree = SkillTrees.Get(ability)
                            };
                            temp_knownAbilities.Add(temp_ability);
                            know_count++;
                        }
                        Main.logger_instance.Msg("Found " + know_count + " Know Ability");
                        System.Collections.Generic.List<Character_class_mastery> temp_masteries = new System.Collections.Generic.List<Character_class_mastery>();
                        foreach (Mastery mastery in char_class.masteries)
                        {
                            int m_count = 0;
                            System.Collections.Generic.List<Character_class_ability_and_level> temp_abilities = new System.Collections.Generic.List<Character_class_ability_and_level>();
                            foreach (AbilityAndLevel ability_and_level in mastery.abilities)
                            {
                                Icon.Save(ability_and_level.ability.abilitySprite, class_path + @"Masteries\" + mastery.name + @"\");
                                Character_class_ability temp_ability = new Character_class_ability
                                {
                                    abilityName = ability_and_level.ability.name,
                                    playerAbilityID = ability_and_level.ability.playerAbilityID,
                                    iconName = Icon.GetSpriteName(),
                                    skilltree = SkillTrees.Get(ability_and_level.ability)
                                };
                                Character_class_ability_and_level temp_ability_and_level = new Character_class_ability_and_level
                                {
                                    ability = temp_ability,
                                    level = ability_and_level.level
                                };
                                temp_abilities.Add(temp_ability_and_level);
                                m_count++;
                            }
                            Main.logger_instance.Msg("Found Mastery : " + mastery.name + " with " + m_count + " Ability");
                            Icon.Save(mastery.icon, class_path + @"Masteries\" + mastery.name + @"\");
                            Character_class_mastery temp_mastery = new Character_class_mastery
                            {
                                name = mastery.name,
                                abilities = temp_abilities,
                                iconName = Icon.GetSpriteName()
                            };
                            temp_masteries.Add(temp_mastery);
                        }
                        System.Collections.Generic.List<Character_class_ability_and_level> temp_unlockableAbilities = new System.Collections.Generic.List<Character_class_ability_and_level>();
                        int unlock_count = 0;
                        foreach (AbilityAndLevel ability_and_level in char_class.unlockableAbilities)
                        {
                            Icon.Save(ability_and_level.ability.abilitySprite, class_path + @"unlockableAbilities\");
                            Character_class_ability temp_ability = new Character_class_ability
                            {
                                abilityName = ability_and_level.ability.name,
                                playerAbilityID = ability_and_level.ability.playerAbilityID,
                                iconName = Icon.GetSpriteName(),
                                skilltree = SkillTrees.Get(ability_and_level.ability)
                            };
                            Character_class_ability_and_level temp_ability_and_level = new Character_class_ability_and_level
                            {
                                ability = temp_ability,
                                level = ability_and_level.level
                            };
                            temp_unlockableAbilities.Add(temp_ability_and_level);
                            unlock_count++;
                        }
                        Main.logger_instance.Msg("Found " + unlock_count + " Unlockable(s) Ability");
                        Icon.Save(char_class.baseClassIcon, class_path);
                        Character_class temp_class = new Character_class
                        {
                            iconName = Icon.GetSpriteName(),
                            baseAttunement = char_class.baseAttunement,
                            baseDexterity = char_class.baseDexterity,
                            baseEndurance = char_class.baseEndurance,
                            baseEnduranceThreshold = char_class.baseEnduranceThreshold,
                            baseHealth = char_class.baseHealth,
                            baseIntelligence = char_class.baseIntelligence,
                            baseMana = char_class.baseMana,
                            baseStrength = char_class.baseStrength,
                            baseStunAvoidance = char_class.baseStunAvoidance,
                            baseVitality = char_class.baseVitality,
                            className = char_class.className,
                            defaultAbilities = temp_defaultAbilities,
                            enduranceThresholdPerHealth = char_class.enduranceThresholdPerHealth,
                            enduranceThresholdPerLevel = char_class.enduranceThresholdPerLevel,
                            firstLevelForMinionScaling = char_class.firstLevelForMinionScaling,
                            healthBarHeightOffset = char_class.healthBarHeightOffset,
                            healthPerLevel = char_class.healthPerLevel,
                            healthRegen = char_class.healthRegen,
                            healthRegenPerLevel = char_class.healthRegenPerLevel,
                            hideThirdMasterySkillsInPanel = char_class.hideThirdMasterySkillsInPanel,
                            knownAbilities = temp_knownAbilities,
                            lessMinionDamageTakenPerLevel = char_class.lessMinionDamageTakenPerLevel,
                            manaPerLevel = char_class.manaPerLevel,
                            manaRegen = char_class.manaRegen,
                            moreMinionDamagePerLevel = char_class.moreMinionDamagePerLevel,
                            specialTagForClassSpecificLevelOfSkillsStats = char_class.specialTagForClassSpecificLevelOfSkillsStats,
                            masteries = temp_masteries,
                            treeID = char_class.treeID,
                            unlockableAbilities = temp_unlockableAbilities
                        };
                        string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(temp_class);
                        Functions.VerifyDirectory(class_path, class_filename);
                        System.IO.File.WriteAllText(class_path + class_filename, jsonString);
                        Main.logger_instance.Msg("Save : " + class_filename);
                    }
                    break;
                }
                Main.logger_instance.Msg("");
                Main.logger_instance.Msg("Get All Classes : Done");
            }
            catch { Main.logger_instance.Error("Error Extracting Classes"); }
        }
    }
}
