using static LastEpoch_GameExtractorMod.Data.SkillTrees;

namespace LastEpoch_GameExtractorMod.Data
{
    public class SkillTrees
    {
        public struct Skill_Tree
        {
            public string treeID;
            public System.Collections.Generic.List<Nodes.Node> nodes;           
        }
        internal static Skill_Tree Get(Ability ability)
        {
            Skill_Tree ability_Skilltree = new Skill_Tree();
            SkillTree skilltree = null;
            foreach (UnityEngine.Object obj in UniverseLib.RuntimeHelper.FindObjectsOfTypeAll(typeof(SkillTree)))
            {
                SkillTree temp_skillTree = obj.TryCast<SkillTree>();
                if (temp_skillTree.ability == ability)
                {
                    skilltree = temp_skillTree;
                    break;
                }
            }
            if (skilltree != null)
            {
                System.Collections.Generic.List<Nodes.Node> temp_nodes = new System.Collections.Generic.List<Nodes.Node>();
                foreach (SkillTreeNode skill_tree_node in skilltree.TryCast<Tree>().nodeList)
                {
                    UnityEngine.Vector3 position = Nodes.GetNodePosition(skilltree, skill_tree_node.id);
                    Nodes.Node temp_node = new Nodes.Node
                    {
                        id = skill_tree_node.id,
                        nodeName = skill_tree_node.nodeName,
                        description = skill_tree_node.description,
                        x = position.x,
                        y = position.y
                    };
                    temp_nodes.Add(temp_node);
                }
                ability_Skilltree = new Skill_Tree
                {
                    treeID = skilltree.TryCast<Tree>().treeID,
                    nodes = temp_nodes
                };
            }

            return ability_Skilltree;
        }
        public class Nodes
        {
            public struct Node
            {
                public string description;
                public byte id;
                public string nodeName;
                public float x;
                public float y;
            }
            internal static UnityEngine.Vector3 GetNodePosition(SkillTree skilltree, int nodeid)
            {
                UnityEngine.Vector3 result = UnityEngine.Vector3.zero;
                UnityEngine.GameObject tree = null;
                for (int i = 0; i < skilltree.gameObject.transform.childCount; i++)
                {
                    string name = skilltree.gameObject.transform.GetChild(i).gameObject.name;
                    if (name == "Tree")
                    {
                        tree = skilltree.gameObject.transform.GetChild(i).gameObject;
                        break;
                    }
                }
                if (tree != null)
                {
                    for (int i = 0; i < tree.transform.childCount; i++)
                    {
                        string name = tree.transform.GetChild(i).gameObject.name;
                        if ((name != "RootNodeBackground") && (name != "Ornaments") && (name != "Connections"))
                        {
                            UnityEngine.GameObject go = tree.transform.GetChild(i).gameObject;
                            try
                            {
                                SkillTreeNode skn = go.GetComponent<SkillTreeNode>();
                                if (skn.id == nodeid)
                                {
                                    UnityEngine.Vector3 local_position = go.transform.localPosition;
                                    result = local_position;
                                    break;
                                }
                            }
                            catch { }
                        }
                    }
                }

                return result;
            }            
        }
    }
}
