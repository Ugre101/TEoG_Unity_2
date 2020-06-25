using System.Collections.Generic;
using UnityEngine;

public class SexCharsHandler : MonoBehaviour
{
    [SerializeField] private SexChar sexChar = null;
    [SerializeField] private Transform sortBtns = null;
    [SerializeField] private SortButton sortBtn = null;

    public void Setup(BasicChar basicChar) => Setup(new List<BasicChar>() { basicChar });

    public void Setup(List<BasicChar> chars)
    {
        if (chars.Count <= 1)
        {
            sortBtns.gameObject.SetActive(false);
        }
        else
        {
            sortBtns.gameObject.SetActive(true);
            sortBtns.KillChildren();
            // Instantiate(sortBtn, sortBtns).Setup("All", AllPartners);
            for (int i = 0; i < chars.Count; i++)
            {
                BasicChar basicChar = chars[i];
                Instantiate(sortBtn, sortBtns).Setup($"{i + 1}", () => ChoosePartner(basicChar));
            }
        }
        ChoosePartner(chars[0]);
    }

    private void AllPartners()
    {
    }

    private void ChoosePartner(BasicChar partner)
    {
        sexChar.Setup(partner);
    }
}