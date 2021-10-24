using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;
    public Image faceImage;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;
    public List<string> textList = new List<string>();

    public float textSpeed;
    bool textFinished;//是否完成打字
    bool cancelTyping;

    [Header("头像")]
    public Sprite face01, face02;
    // Start is called before the first frame update
    void Awake()
    {
        textSpeed = 0.1f;
        GetTextFromFile(textFile);
        //index = 0;
    }

    void OnEnable()
    {
        //GetTextFromFile(textFile);
        //textLabel.text = textList[0];
        //index++;
        textFinished = true;
        StartCoroutine(SetTextUI());
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)&&index==textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        //else if(Input.GetKeyDown(KeyCode.R)&&textFinished)
        //{
        //    //textLabel.text = textList[index];
        //    //index++;
        //    StartCoroutine(SetTextUI());
        //}
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(textFinished&&!cancelTyping)
            {
                StartCoroutine(SetTextUI());
            }
            else if(!textFinished)
            {
                cancelTyping = !cancelTyping;
            }
        }
    }

    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        string[] lineData= file.text.Split('\n');
        foreach(var line in lineData)
        {
            textList.Add(line);
        }

    }

    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";

        switch(textList[index])
        {
            case "A":faceImage.sprite = face01;
                index++;
                break;
            case "B":faceImage.sprite = face02;
                index++;
                break;
        }

        //for(int i=0;i<textList[index].Length;i++)
        //{
        //    textLabel.text += textList[index][i];

        //    yield return new WaitForSeconds(textSpeed);
        //}
        int letter = 0;
        while(!cancelTyping&&letter<textList[index].Length-1)
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);

        }
        textLabel.text = textList[index];
        cancelTyping = false;
        textFinished = true;
        index++;
    }
}
