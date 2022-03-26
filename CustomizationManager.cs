using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomizationManager : MonoBehaviour
{
    [SerializeField] public Text gender;
    private String male = "Male";
    private String female = "Female";

    Transform[] allItemChildrenHair;
    Transform[] allItemChildrenHead;
    Transform[] allItemChildrenTorso;
    Transform[] allItemChildrenLeg;

    const string namePrefix = "Set Character_";
    enum AppearanceDetail
    {
        HAIR_MODEL,
        HEAD_MODEL,
        TORSO_MODEL,
        LEG_MODEL,
        SWORD_MODEL
    }
    private Transform characterRoot;
    private Transform characterRightHandRoot;

    private Transform characterRootMale;
    [SerializeField] private Transform characterRightHandRootMale;

    [SerializeField] private Transform characterRootFemale;
    [SerializeField] private Transform characterRightHandRootFemale;

    private GameObject[] hairModels;
    private GameObject[] headModels;
    private GameObject[] torsoModels;
    private GameObject[] legModels;

    [SerializeField] private GameObject[] swordModels;

    //Male Items
    [SerializeField] private GameObject[] hairModelsMale;
    [SerializeField] private GameObject[] headModelsMale;
    [SerializeField] private GameObject[] torsoModelsMale;
    [SerializeField] private GameObject[] legModelsMale;

    //Female Items
    [SerializeField] private GameObject[] hairModelsFemale;
    [SerializeField] private GameObject[] headModelsFemale;
    [SerializeField] private GameObject[] torsoModelsFemale;
    [SerializeField] private GameObject[] legModelsFemale;

    GameObject activeHair;
    GameObject activeHead;
    GameObject activeTorso;
    GameObject activeLeg;
    GameObject activesword;

    bool isMale = true;
    int hairIndex = 0;
    int headIndex = 0;
    int torsoIndex = 0;
    int legIndex = 0;
    int swordIndex = 0;

    void Start()
    {
        characterRootMale=FindObjectOfType<MaleCharacter>().transform;//characterRootMale
        gender.text = male;
        ChangeGender();
        Randomize();
    }

    private void Randomize()
    {
        RandomIndex();
        ApplyModification(AppearanceDetail.HAIR_MODEL, hairIndex);
        ApplyModification(AppearanceDetail.HEAD_MODEL, headIndex);
        ApplyModification(AppearanceDetail.TORSO_MODEL, torsoIndex);
        ApplyModification(AppearanceDetail.LEG_MODEL, legIndex);
        ApplyModification(AppearanceDetail.SWORD_MODEL, swordIndex);
    }
    private void RandomIndex()
    {
        hairIndex = UnityEngine.Random.Range(0, hairModels.Length);
        headIndex = UnityEngine.Random.Range(0, headModels.Length);
        torsoIndex = UnityEngine.Random.Range(0, torsoModels.Length);
        legIndex = UnityEngine.Random.Range(0, legModels.Length);
        swordIndex =UnityEngine.Random.Range(0, swordModels.Length);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ChangeGender()
    {
        if (isMale )
        {
            gender.text = male;
            hairModels = hairModelsMale;
            headModels = headModelsMale;
            torsoModels = torsoModelsMale;
            legModels = legModelsMale;
            characterRoot = characterRootMale;
            characterRightHandRoot = characterRightHandRootMale;
          //  Destroy(characterRootFemale.gameObject);
        }
        else
        {
            gender.text = female;
            hairModels = hairModelsFemale;
            headModels = headModelsFemale;
            torsoModels = torsoModelsFemale;
            legModels = legModelsFemale;
            legModels = legModelsFemale;
            characterRoot = characterRootFemale;
            characterRightHandRoot = characterRightHandRootFemale;
        }
        Randomize();
    }
    public void GenderModelUp()
    {
        isMale = !isMale;
        if(isMale)
            gender.text = male;
        else
            gender.text = female;
        ChangeGender();
    }
    public void GenderModelDown()
    {
        isMale = !isMale;
        ChangeGender();
    }
    public void HairModelUp()
    {
        if (hairIndex < hairModels.Length-1)
            hairIndex++;
        else
            hairIndex = 0;
        ApplyModification(AppearanceDetail.HAIR_MODEL, hairIndex);
    }
    public void HairModelDown()
    {
        if (hairIndex > 0)
            hairIndex--;
        else
            hairIndex = hairModels.Length - 1;
        ApplyModification(AppearanceDetail.HAIR_MODEL, hairIndex);
    }

    public void headModelUp()
    {
        if (headIndex < headModels.Length - 1)
            headIndex++;
        else
            headIndex = 0;
        ApplyModification(AppearanceDetail.HEAD_MODEL, headIndex);
    }
    public void headModelDown()
    {
        if (headIndex > 0)
            headIndex--;
        else
            headIndex = headModels.Length - 1;
        ApplyModification(AppearanceDetail.HEAD_MODEL, headIndex);
    }

    public void torsoModelUp()
    {
        if (torsoIndex < torsoModels.Length - 1)
            torsoIndex++;
        else
            torsoIndex = 0;
        ClothingCheck(legModels, AppearanceDetail.LEG_MODEL);
        ApplyModification(AppearanceDetail.TORSO_MODEL, torsoIndex);
    }
    public void torsoModelDown()
    {
        if (torsoIndex > 0)
            torsoIndex--;
        else
            torsoIndex = torsoModels.Length - 1;
        ClothingCheck(legModels, AppearanceDetail.LEG_MODEL);
        ApplyModification(AppearanceDetail.TORSO_MODEL, torsoIndex);

    }


    public void legModelUp()
    {
        if (legIndex < legModels.Length - 1)
            legIndex++;
        else
            legIndex = 0;
        ClothingCheck(torsoModels, AppearanceDetail.TORSO_MODEL);
        ApplyModification(AppearanceDetail.LEG_MODEL, legIndex);
    }
    public void legModelDown()
    {
        if (legIndex > 0)
            legIndex--;
        else
            legIndex = legModels.Length - 1;
        ClothingCheck(torsoModels, AppearanceDetail.TORSO_MODEL);
        ApplyModification(AppearanceDetail.LEG_MODEL, legIndex);
    }

    public void SwordModelUp()
    {
        if (swordIndex < swordModels.Length - 1)
            swordIndex++;
        else
            swordIndex = 0;
        ApplyModification(AppearanceDetail.SWORD_MODEL, swordIndex);
    }
    public void SwordModelDown()
    {
        if (swordIndex > 0)
            swordIndex--;
        else
            swordIndex = swordModels.Length - 1;
        ApplyModification(AppearanceDetail.SWORD_MODEL, swordIndex);
    }
    void ApplyModification(AppearanceDetail detail, int id)
    {
        switch (detail)
        {
            case AppearanceDetail.HAIR_MODEL:
                if (activeHair != null)
                {
                    GameObject.Destroy(activeHair);
                }
                activeHair = GameObject.Instantiate(hairModels[id]);
                activeHair.name = activeHair.name.Substring(0, activeHair.name.Length - "(Clone)".Length);
                checkItem(activeHair, AppearanceDetail.HAIR_MODEL);
                activeHair.transform.SetParent(characterRoot);
                activeHair.transform.ResetTransform();
                break;

            case AppearanceDetail.HEAD_MODEL:
                if (activeHead != null)
                    GameObject.Destroy(activeHead);
                activeHead = GameObject.Instantiate(headModels[id]);
                activeHead.name = activeHead.name.Substring(0, activeHead.name.Length - "(Clone)".Length);
                checkItem(activeHead, AppearanceDetail.HEAD_MODEL);
                activeHead.transform.SetParent(characterRoot);
                activeHead.transform.ResetTransform();
                break;

            case AppearanceDetail.TORSO_MODEL:
                if (activeTorso != null)
                    GameObject.Destroy(activeTorso);
                activeTorso = GameObject.Instantiate(torsoModels[id]);
                activeTorso.name = activeTorso.name.Substring(0, activeTorso.name.Length - "(Clone)".Length);
                checkItem(activeTorso, AppearanceDetail.TORSO_MODEL);
                activeTorso.transform.SetParent(characterRoot);
                activeTorso.transform.ResetTransform();
                break;

            case AppearanceDetail.LEG_MODEL:
                if (activeLeg != null)
                    GameObject.Destroy(activeLeg);
                activeLeg = GameObject.Instantiate(legModels[id]);
                activeLeg.name = activeLeg.name.Substring(0, activeLeg.name.Length - "(Clone)".Length);
                checkItem(activeLeg, AppearanceDetail.LEG_MODEL);
                activeLeg.transform.SetParent(characterRoot);
                activeLeg.transform.ResetTransform();
                break;

            case AppearanceDetail.SWORD_MODEL:
                if (activesword != null)
                    GameObject.Destroy(activesword);
                activesword = GameObject.Instantiate(swordModels[id]);
                activesword.name = activesword.name.Substring(0, activesword.name.Length - "(Clone)".Length);
                activesword.transform.SetParent(characterRightHandRoot);
                activesword.transform.ResetTransform();
                break;
        }
    }

    public void ParentObjectAndBones(GameObject itemInstance, Transform[] allItemChildren)
    {
        Transform[] allCharacterChildren = GetAllCharacterChildren();
        itemInstance.transform.position = transform.position;
        itemInstance.transform.parent = transform;

        string[] allItemChildren_NewNames = new string[allItemChildren.Length];

        for (int i = 0; i < allItemChildren.Length; i++)
        {
            //Match and parent bones
            for (int n = 0; n < allCharacterChildren.Length; n++)
            {
                if (allItemChildren[i].name == allCharacterChildren[n].name)
                {
                    MatchTransform(allItemChildren[i], allCharacterChildren[n]);
                    allItemChildren[i].parent = allCharacterChildren[n];
                }
            }

            //Rename
            allItemChildren_NewNames[i] = allItemChildren[i].name;

            if (!allItemChildren[i].name.Contains(namePrefix))
            {
                allItemChildren_NewNames[i] = namePrefix + allItemChildren[i].name;
            }

            if (!allItemChildren[i].name.Contains(itemInstance.name))
            {
                allItemChildren_NewNames[i] += "_" + itemInstance.name.Substring(0, itemInstance.name.Length);// - "(Clone)".Length);
            }
        }

        for (int i = 0; i < allItemChildren.Length; i++)
        {
            allItemChildren[i].name = allItemChildren_NewNames[i];
        }
    }
    public Transform[] GetAllCharacterChildren()
    {
        Transform root = GetRoot();
        Transform[] allCharacterChildren = root.GetComponentsInChildren<Transform>();

        List<Transform> allCharacterChildren_List = new List<Transform>();

        for(int i = 0; i < allCharacterChildren.Length; i++){
            if(allCharacterChildren[i].GetComponent<SkinnedMeshRenderer>() != null || allCharacterChildren[i].GetComponent<Animator>() != null)
            {
                continue;
            }
            allCharacterChildren_List.Add(allCharacterChildren[i]);
        }

        allCharacterChildren = allCharacterChildren_List.ToArray();

        return allCharacterChildren;
    }
    public Transform GetRoot()
    {
        Transform root;
        if (characterRoot == null)
        {
            root = transform;
        }
        else
        {
            root = characterRoot;
        }
        return root;
    }
    public void MatchTransform(Transform obj, Transform target)
    {
        obj.position = target.position;
        obj.rotation = target.rotation;
    }

    private void checkItem(GameObject itemInstance, AppearanceDetail detail)
    {
        switch (detail)
        {
            case AppearanceDetail.HAIR_MODEL:
                if (allItemChildrenHair != null)
                {
                    for (int i = 0; i < allItemChildrenHair.Length; i++)
                    {
                        GameObject.Destroy(allItemChildrenHair[i].gameObject);
                    }
                }
                allItemChildrenHair = itemInstance.GetComponentsInChildren<Transform>();
                ParentObjectAndBones(itemInstance, allItemChildrenHair);
                break;
            case AppearanceDetail.HEAD_MODEL:
                if (allItemChildrenHead != null)
                {
                    for (int i = 0; i < allItemChildrenHead.Length; i++)
                    {
                        GameObject.Destroy(allItemChildrenHead[i].gameObject);
                    }
                }
                allItemChildrenHead = itemInstance.GetComponentsInChildren<Transform>();
                ParentObjectAndBones(itemInstance, allItemChildrenHead);
                break;
            case AppearanceDetail.TORSO_MODEL:
                if (allItemChildrenTorso != null)
                {
                    for (int i = 0; i < allItemChildrenTorso.Length; i++)
                    {
                        GameObject.Destroy(allItemChildrenTorso[i].gameObject);
                    }
                }
                allItemChildrenTorso = itemInstance.GetComponentsInChildren<Transform>();
                ParentObjectAndBones(itemInstance, allItemChildrenTorso);
                break;
            case AppearanceDetail.LEG_MODEL:
                if (allItemChildrenLeg != null)
                {
                    for (int i = 0; i < allItemChildrenLeg.Length; i++)
                    {
                        GameObject.Destroy(allItemChildrenLeg[i].gameObject);
                    }
                }
                allItemChildrenLeg = itemInstance.GetComponentsInChildren<Transform>();
                ParentObjectAndBones(itemInstance, allItemChildrenLeg);
                break;
        }
    }
    private void ClothingCheck(GameObject[] itemsModels , AppearanceDetail detail)
    {
        if (!isMale && legIndex == 2 && torsoIndex == 2)
        {
            int itemIndex = 0;
            switch (detail) {
                case AppearanceDetail.TORSO_MODEL:
                    torsoIndex = UnityEngine.Random.Range(0, itemsModels.Length - 1);
                    itemIndex = torsoIndex;
                    break;

                case AppearanceDetail.LEG_MODEL:
                    legIndex = UnityEngine.Random.Range(0, itemsModels.Length - 1);
                    itemIndex = legIndex;
                    break;
            }
            ApplyModification(detail, itemIndex);
        }
    }
    public void play()
    {
        if (isMale)
        {
            FindObjectOfType<FemaleCharacter>().DestroyMy();
        }
        else
            FindObjectOfType<MaleCharacter>().DestroyMy();
        SceneManager.LoadScene("Menu");
    }
}
