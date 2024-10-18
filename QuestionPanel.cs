using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class QuestionPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI QuestionText;
    [SerializeField] TextMeshProUGUI FeedBackText;
    [SerializeField] Toggle[] Options;
    public int correctOption;
    public int selectedOption;
    // Start is called before the first frame update
    void Start()
    {
        Options[0].onValueChanged.AddListener(ToggelASelected);
        Options[1].onValueChanged.AddListener(ToggelBSelected);
        Options[2].onValueChanged.AddListener(ToggelCSelected);
        Options[3].onValueChanged.AddListener(ToggelDSelected);
    }

    private void ToggelDSelected(bool arg0)
    {
        if (arg0)
        {
          var  Index = 3;
            SetOption(Index);
        }
    }

    private void ToggelCSelected(bool arg0)
    {
        if (arg0)
        {
            var Index = 2;
            SetOption(Index);
        }
    }

    private void ToggelBSelected(bool arg0)
    {
        if (arg0)
        {
            var Index = 1;
            SetOption(Index);
        }
    }

    private void ToggelASelected(bool arg0)
    {
        if (arg0)
        {
            var Index = 0;
            SetOption(Index);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void isOptionCorrect()
    {
        for(int i = 0; i < Options.Length; i++)
        {
            if (Options[i].isOn)
            {
                selectedOption = i;
                Debug.Log(i);
            }
        }

        if (selectedOption == correctOption)
        {
            Debug.LogError("Correct");
            FeedBackText.text = "Correct";
            BirdMovement.instance.correctAnswers++;
            FeedBackText.gameObject.SetActive(true);
        }
        else
        {
            FeedBackText.text = $"Correct Answer is {Options[correctOption].transform.GetChild(1).GetComponent<Text>().text}";
            FeedBackText.gameObject.SetActive(true);
        }

    }



    public void SetOption(int selectedIndex)
    {
        for (int i = 0; i < Options.Length; i++)
        {
            if (selectedIndex != i)
            {
                Options[i].isOn = false;
            }

            if (selectedIndex == i)
            {
                Options[i].isOn = true;
            }
        }
    }




    private void OnDisable()
    {

            Options[0].onValueChanged.RemoveListener(ToggelASelected);
            Options[1].onValueChanged.RemoveListener(ToggelBSelected);
            Options[2].onValueChanged.RemoveListener(ToggelCSelected);
            Options[3].onValueChanged.RemoveListener(ToggelDSelected);
    }
}
