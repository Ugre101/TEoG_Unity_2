using SaveStuff;
using UnityEngine;

[System.Serializable]
public class Identity
{
    [SerializeField] private string firstName;

    public string FirstName { get => firstName; set => firstName = value; }

    [SerializeField] private string lastName;

    public string LastName { get => lastName; set => lastName = value; }

    public string FullName => $"{FirstName} {LastName}";

    [SerializeField] private string id;

    public string Id => id;

    public Identity()
    {
        // should always be uni
        id = UniqueId.GetNewId;
    }

    public IdentitySave Save() => new IdentitySave(FirstName, LastName, Id);

    public void Load(IdentitySave load)
    {
        firstName = load.FirstName;
        lastName = load.LastName;
        id = load.Id;
    }
}

namespace SaveStuff
{
    [System.Serializable]
    public struct IdentitySave
    {
        [SerializeField] private string firstName, lastName, id;

        public IdentitySave(string firstName, string lastName, string id)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.id = id;
        }

        public string FirstName { get => firstName; }
        public string LastName { get => lastName; }
        public string Id { get => id; }
    }
}