using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sentientButton : MonoBehaviour
{
    private int _sentenceNumb;
    [SerializeField] private Text _text;
    [SerializeField] private string[] _sentences;

    // Start is called before the first frame update
    void Start()
    {
        _sentenceNumb = 0;
        _text.text = _sentences[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TalkingButton()
    {
        _sentenceNumb += 1;
        if (_sentenceNumb >= _sentences.Length)
        {
            _sentenceNumb = 0;
        }
        changeText(_sentences[_sentenceNumb]);
        
    }

    private void changeText(string a)
    {
        _text.text = a;        
    }


}
