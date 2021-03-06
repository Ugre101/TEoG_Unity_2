﻿using UnityEngine;

namespace EssenceMenuStuff
{
    public class EssenceBallsButtons : EssenceOrganButtons
    {
        [SerializeField] private AddBalls addBallsPrefab = null;
        [SerializeField] private GrowBalls growBallsPrefab = null;

        protected override void UpdateButtons()
        {
            transform.KillChildren();
            Instantiate(addBallsPrefab, transform).Setup().onClick.AddListener(UpdateButtons);
            foreach (Balls b in Player.SexualOrgans.Balls.List)
            {
                Instantiate(growBallsPrefab, transform).Setup(b);
            }
        }
    }
}