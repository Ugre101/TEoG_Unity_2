using SaveStuff;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RelationshipTracker
{
    [SerializeField] private List<RelationshipWith> relationshipWiths = new List<RelationshipWith>();

    public List<RelationshipWith> Relationships => relationshipWiths;
    public List<RelationshipWith> TempRelations { get; } = new List<RelationshipWith>();

    public RelationshipWith GetReleationWith(BasicChar with) => GetPerson(with);

    public RelationshipWith GetTempRelationshipWith(BasicChar with) => GetTempPerson(with);

    public void MoveFromTemp(BasicChar basicChar)
    {
        if (TempExist(basicChar))
        {
            Relationships.Add(GetTempPerson(basicChar));
            // It shouldn't be any problem leaving the inside temp
        }
        else
        {
            // Add new if it doesn't exist
            AddRelation(basicChar);
        }
    }

    private RelationshipWith GetPerson(BasicChar basicChar)
    {
        if (Exists(basicChar))
        {
            return Relationships.Find(r => r.With.Id == basicChar.Identity.Id);
        }
        else
        {
            AddRelation(basicChar);
            return Relationships.Find(r => r.With.Id == basicChar.Identity.Id);
        }
    }

    private RelationshipWith GetTempPerson(BasicChar basicChar)
    {
        if (TempExist(basicChar))
        {
            return TempRelations.Find(r => r.With.Id == basicChar.Identity.Id);
        }
        else
        {
            AddTempRelation(basicChar);
            return TempRelations.Find(r => r.With.Id == basicChar.Identity.Id);
        }
    }

    private void AddRelation(BasicChar basicChar)
    {
        if (!Exists(basicChar))
        {
            Relationships.Add(new RelationshipWith(basicChar.Identity));
        }
    }

    private void AddTempRelation(BasicChar basicChar)
    {
        if (!TempExist(basicChar))
        {
            TempRelations.Add(new RelationshipWith(basicChar.Identity));
        }
    }

    private bool Exists(BasicChar basicChar) => Relationships.Exists(r => r.With.Id == basicChar.Identity.Id);

    private bool TempExist(BasicChar basicChar) => TempRelations.Exists(r => r.With.Id == basicChar.Identity.Id);

    public ReletionShipSave Save() => new ReletionShipSave(Relationships);

    public void Load(ReletionShipSave load) => relationshipWiths = load.Relationships;
}

public enum Affection
{
    Hates,
    Dislikes,
    Neutral,
    Likes,
    Loves
}

public enum Obedience
{
    Slave, // they view themself as their slave
    Equals, // TODO add more and evolve
    Master // they view themself as their master
}

[System.Serializable]
public class RelationshipWith
{
    public RelationshipWith(Identity identity, int affection, int obedience)
    {
        this.identity = identity;
        this.affection = new CharStats(affection);
        this.obedience = new CharStats(obedience);
    }

    public RelationshipWith(Identity identity) : this(identity, 0, 0)
    {
    }

    [SerializeField] private Identity identity;

    // Have it be a charstat so I can add temp buffs and similar stuff
    [SerializeField] private CharStats affection;

    public CharStats AffectionStat => affection;
    [SerializeField] private CharStats obedience;
    public CharStats ObedienceStat => obedience;
    public Identity With => identity;
    public int AffectionValue => affection.Value;

    public Affection Affection
    {
        get
        {
            if (AffectionValue < -100)
            {
                return Affection.Hates;
            }
            else if (AffectionValue < -25)
            {
                return Affection.Dislikes;
            }
            else if (AffectionValue < 25)
            {
                return Affection.Neutral;
            }
            else if (AffectionValue < 100)
            {
                return Affection.Likes;
            }
            else
            {
                return Affection.Loves;
            }
        }
    }

    public int ObedienceValue => obedience.Value;

    public Obedience Obedience
    {
        get
        {
            if (ObedienceValue < -100)
            {
                return Obedience.Master;
            }
            else if (ObedienceValue < 100)
            {
                return Obedience.Equals;
            }
            else
            {
                return Obedience.Slave;
            }
        }
    }
}

namespace SaveStuff
{
    [System.Serializable]
    public struct ReletionShipSave
    {
        [SerializeField] private List<RelationshipWith> relationshipWiths;

        public ReletionShipSave(List<RelationshipWith> relationshipWiths)
        {
            this.relationshipWiths = relationshipWiths;
        }

        public List<RelationshipWith> Relationships => relationshipWiths;
    }
}